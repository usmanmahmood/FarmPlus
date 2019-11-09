using System.Collections.Generic;
using System.IO;

namespace Framework.Core.Interfaces.Services
{
    public interface IDirectoryService : IService
    {
        //
        // Summary:
        //     Returns an enumerable collection of file names that match a search pattern in
        //     a specified path, and optionally searches subdirectories.
        //
        // Parameters:
        //   path:
        //     The relative or absolute path to the directory to search. This string is not
        //     case-sensitive.
        //
        //   searchPattern:
        //     The search string to match against the names of files in path. This parameter
        //     can contain a combination of valid literal path and wildcard (* and ?) characters,
        //     but it doesn&#39;t support regular expressions.
        //
        //   searchOption:
        //     One of the enumeration values that specifies whether the search operation should
        //     include only the current directory or should include all subdirectories. The
        //     default value is System.IO.SearchOption.TopDirectoryOnly.
        //
        // Returns:
        //     An enumerable collection of the full names (including paths) for the files in
        //     the directory specified by path and that match the specified search pattern and
        //     option.
        //
        // Exceptions:
        //   T:System.ArgumentException:
        //     path is a zero-length string, contains only white space, or contains invalid
        //     characters. You can query for invalid characters by using the System.IO.Path.GetInvalidPathChars
        //     method. - or - searchPattern does not contain a valid pattern.
        //
        //   T:System.ArgumentNullException:
        //     path is null. -or- searchPattern is null.
        //
        //   T:System.ArgumentOutOfRangeException:
        //     searchOption is not a valid System.IO.SearchOption value.
        //
        //   T:System.IO.DirectoryNotFoundException:
        //     path is invalid, such as referring to an unmapped drive.
        //
        //   T:System.IO.IOException:
        //     path is a file name.
        //
        //   T:System.IO.PathTooLongException:
        //     The specified path, file name, or combined exceed the system-defined maximum
        //     length. For example, on Windows-based platforms, paths must be less than 248
        //     characters and file names must be less than 260 characters.
        //
        //   T:System.Security.SecurityException:
        //     The caller does not have the required permission.
        //
        //   T:System.UnauthorizedAccessException:
        //     The caller does not have the required permission.
        IEnumerable<string> EnumerateFiles(string path, string searchPattern, SearchOption searchOption);
        //
        // Summary:
        //     Returns an enumerable collection of file names in a specified path.
        //
        // Parameters:
        //   path:
        //     The relative or absolute path to the directory to search. This string is not
        //     case-sensitive.
        //
        // Returns:
        //     An enumerable collection of the full names (including paths) for the files in
        //     the directory specified by path.
        //
        // Exceptions:
        //   T:System.ArgumentException:
        //     path is a zero-length string, contains only white space, or contains invalid
        //     characters. You can query for invalid characters by using the System.IO.Path.GetInvalidPathChars
        //     method.
        //
        //   T:System.ArgumentNullException:
        //     path is null.
        //
        //   T:System.IO.DirectoryNotFoundException:
        //     path is invalid, such as referring to an unmapped drive.
        //
        //   T:System.IO.IOException:
        //     path is a file name.
        //
        //   T:System.IO.PathTooLongException:
        //     The specified path, file name, or combined exceed the system-defined maximum
        //     length. For example, on Windows-based platforms, paths must be less than 248
        //     characters and file names must be less than 260 characters.
        //
        //   T:System.Security.SecurityException:
        //     The caller does not have the required permission.
        //
        //   T:System.UnauthorizedAccessException:
        //     The caller does not have the required permission.
        IEnumerable<string> EnumerateFiles(string path);
        //
        // Summary:
        //     Returns an enumerable collection of file names that match a search pattern in
        //     a specified path.
        //
        // Parameters:
        //   path:
        //     The relative or absolute path to the directory to search. This string is not
        //     case-sensitive.
        //
        //   searchPattern:
        //     The search string to match against the names of files in path. This parameter
        //     can contain a combination of valid literal path and wildcard (* and ?) characters,
        //     but it doesn&#39;t support regular expressions.
        //
        // Returns:
        //     An enumerable collection of the full names (including paths) for the files in
        //     the directory specified by path and that match the specified search pattern.
        //
        // Exceptions:
        //   T:System.ArgumentException:
        //     path is a zero-length string, contains only white space, or contains invalid
        //     characters. You can query for invalid characters by using the System.IO.Path.GetInvalidPathChars
        //     method. - or - searchPattern does not contain a valid pattern.
        //
        //   T:System.ArgumentNullException:
        //     path is null. -or- searchPattern is null.
        //
        //   T:System.IO.DirectoryNotFoundException:
        //     path is invalid, such as referring to an unmapped drive.
        //
        //   T:System.IO.IOException:
        //     path is a file name.
        //
        //   T:System.IO.PathTooLongException:
        //     The specified path, file name, or combined exceed the system-defined maximum
        //     length. For example, on Windows-based platforms, paths must be less than 248
        //     characters and file names must be less than 260 characters.
        //
        //   T:System.Security.SecurityException:
        //     The caller does not have the required permission.
        //
        //   T:System.UnauthorizedAccessException:
        //     The caller does not have the required permission.
        IEnumerable<string> EnumerateFiles(string path, string searchPattern);
        bool IsDirectoryExist(string path);
    }
}
