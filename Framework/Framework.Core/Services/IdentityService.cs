﻿using System;
using System.Configuration;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

using Microsoft.Identity.Client;
using Microsoft.Identity.Client.AppConfig;

using UWPApp.Core.Helpers;

namespace UWPApp.Core.Services
{
    public class IdentityService
    {
        //// For more information about using Identity, see
        //// https://github.com/Microsoft/WindowsTemplateStudio/blob/master/docs/features/identity.md
        ////
        //// Read more about Microsoft Identity Client here
        //// https://github.com/AzureAD/microsoft-authentication-library-for-dotnet/wiki
        //// https://docs.microsoft.com/azure/active-directory/develop/v2-overview
        private readonly string[] _scopes = new string[] { "user.read" };

        private bool _integratedAuthAvailable;
        private IPublicClientApplication _client;
        private AuthenticationResult _authenticationResult;

        // TODO WTS: The IdentityClientId in App.config is provided to test the project in development environments.
        // Please, follow these steps to create a new one with Azure Active Directory and replace it before going to production.
        // https://docs.microsoft.com/azure/active-directory/develop/quickstart-register-app
        private string ClientId => ConfigurationManager.AppSettings["IdentityClientId"];

        public event EventHandler LoggedIn;
        public event EventHandler LoggedOut;

        public void InitializeWithAadAndPersonalMsAccounts()
        {
            _integratedAuthAvailable = false;
            _client = PublicClientApplicationBuilder.Create(ClientId)
                                                    .WithAuthority(AadAuthorityAudience.AzureAdAndPersonalMicrosoftAccount)
                                                    .Build();
        }

        public void InitializeWithAadMultipleOrgs(bool integratedAuth = false)
        {
            _integratedAuthAvailable = integratedAuth;
            _client = PublicClientApplicationBuilder.Create(ClientId)
                                                    .WithAuthority(AadAuthorityAudience.AzureAdMultipleOrgs)
                                                    .Build();
        }

        public void InitializeWithAadSingleOrg(string tenant, bool integratedAuth = false)
        {
            _integratedAuthAvailable = integratedAuth;
            _client = PublicClientApplicationBuilder.Create(ClientId)
                                                    .WithAuthority(AzureCloudInstance.AzurePublic, tenant)
                                                    .Build();
        }

        public bool IsLoggedIn() => _authenticationResult != null;

        public async Task<LoginResultType> LoginAsync()
        {
            if (!NetworkInterface.GetIsNetworkAvailable())
            {
                return LoginResultType.NoNetworkAvailable;
            }

            try
            {
                var accounts = await _client.GetAccountsAsync();
                _authenticationResult = await _client.AcquireTokenInteractive(_scopes, null)
                                                     .WithAccount(accounts.FirstOrDefault())
                                                     .ExecuteAsync();
                if (!IsAuthorized())
                {
                    _authenticationResult = null;
                    return LoginResultType.Unauthorized;
                }

                LoggedIn?.Invoke(this, EventArgs.Empty);
                return LoginResultType.Success;
            }
            catch (MsalClientException ex)
            {
                if (ex.ErrorCode == "authentication_canceled")
                {
                    return LoginResultType.CancelledByUser;
                }

                return LoginResultType.UnknownError;
            }
            catch (Exception)
            {
                return LoginResultType.UnknownError;
            }
        }

        public bool IsAuthorized()
        {
            // TODO WTS: You can also add extra authorization checks here.
            // i.e.: Checks permisions of _authenticationResult.Account.Username in a database.
            return true;
        }

        public string GetAccountUserName()
        {
            return _authenticationResult?.Account?.Username;
        }

        public async Task LogoutAsync()
        {
            try
            {
                var accounts = await _client.GetAccountsAsync();
                var account = accounts.FirstOrDefault();
                if (account != null)
                {
                    await _client.RemoveAsync(account);
                }

                _authenticationResult = null;
                LoggedOut?.Invoke(this, EventArgs.Empty);
            }
            catch (MsalException)
            {
                // TODO WTS: LogoutAsync can fail please handle exceptions as appropriate to your scenario
                // For more info on MsalExceptions see
                // https://github.com/AzureAD/microsoft-authentication-library-for-dotnet/wiki/exceptions
            }
        }

        public async Task<string> GetAccessTokenAsync()
        {
            var acquireTokenSuccess = await AcquireTokenSilentAsync();
            if (acquireTokenSuccess)
            {
                return _authenticationResult.AccessToken;
            }
            else
            {
                try
                {
                    // Interactive authentication is required
                    var accounts = await _client.GetAccountsAsync();
                    _authenticationResult = await _client.AcquireTokenInteractive(_scopes, null)
                                                         .WithAccount(accounts.FirstOrDefault())
                                                         .ExecuteAsync();
                    return _authenticationResult.AccessToken;
                }
                catch (MsalException)
                {
                    // AcquireTokenSilent and AcquireTokenInteractive failed, the session will be closed.
                    _authenticationResult = null;
                    LoggedOut?.Invoke(this, EventArgs.Empty);
                    return string.Empty;
                }
            }
        }

        public async Task<bool> AcquireTokenSilentAsync()
        {
            if (!NetworkInterface.GetIsNetworkAvailable())
            {
                return false;
            }
            try
            {
                var accounts = await _client.GetAccountsAsync();
                _authenticationResult = await _client.AcquireTokenSilent(_scopes)
                                                     .WithAccount(accounts.FirstOrDefault())
                                                     .ExecuteAsync();
                return true;
            }
            catch (MsalUiRequiredException)
            {
                if (_integratedAuthAvailable)
                {
                    try
                    {

                        _authenticationResult = await _client.AcquireTokenByIntegratedWindowsAuth(_scopes)
                                                             .ExecuteAsync();
                        return true;
                    }
                    catch(MsalUiRequiredException)
                    {
                        // Interactive authentication is required
                        return false;
                    }
                }
                else
                {
                    // Interactive authentication is required
                    return false;
                }
            }
            catch (MsalException)
            {
                // TODO WTS: Silentauth failed, please handle this exception as appropriate to your scenario
                // For more info on MsalExceptions see
                // https://github.com/AzureAD/microsoft-authentication-library-for-dotnet/wiki/exceptions
                return false;
            }
        }
    }
}
