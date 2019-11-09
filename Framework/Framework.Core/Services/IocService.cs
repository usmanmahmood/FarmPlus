using Framework.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
//using Unity;

namespace Framework.Core.Services
{
    public class IocService : IIocService //, CommonServiceLocator.IServiceLocator
    {
        //private Lazy<UnityContainer> Container = new Lazy<UnityContainer>();
        public IocService()
        {
            Registation();
        }

        protected virtual void Registation()
        {
            RegisterSingleton<ISystemService, SystemService>();
            RegisterSingleton<IDirectoryService, DirectoryService>();
        }

        public void RegisterSingleton<TFrom, TTo>() where TTo : TFrom
        {
            //Container.Value.RegisterSingleton<TFrom, TTo>();
        }

        public void RegisterType<TFrom, TTo>() where TTo : TFrom
        {
            //Container.Value.RegisterType<TFrom, TTo>();
        }

        public void RegisterType<T>() 
        {
            //Container.Value.RegisterType<T>();
        }

        public void RegisterInstance<TInterface>(string name, TInterface instance)
        {
            //Container.Value.RegisterInstance<TInterface>(name, instance);
        }

        public void RegisterInstance<TInterface>(TInterface instance)
        {
            //Container.Value.RegisterInstance<TInterface>(instance);
        }

        public object GetInstance(Type serviceType)
        {
            throw new NotImplementedException();
            //return Container.Value.Resolve(serviceType);
        }

        public object GetInstance(Type serviceType, string key)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<object> GetAllInstances(Type serviceType)
        {
            throw new NotImplementedException();
            //return Container.Value.ResolveAll(serviceType);
        }

        public TService GetInstance<TService>()
        {
            throw new NotImplementedException();
            //return Container.Value.Resolve<TService>();
        }

        public TService GetInstance<TService>(string key)
        {
            throw new NotImplementedException();
            //return Container.Value.Resolve<TService>(key);
        }

        public IEnumerable<TService> GetAllInstances<TService>()
        {
            throw new NotImplementedException();
            //return Container.Value.ResolveAll<TService>();
        }

        public object GetService(Type serviceType)
        {
            throw new NotImplementedException();
            //return Container.Value.Resolve(serviceType);
        }
    }
}
