using Framework.Core.Interfaces.Services;
using System.Collections.Generic;
using System.IO;

namespace Framework.Core.Services
{
    public class DirectoryService : IDirectoryService
    {
        public IEnumerable<string> EnumerateFiles(string path, string searchPattern, SearchOption searchOption)
        {
            return Directory.EnumerateFiles(path, searchPattern, searchOption);
        }

        public IEnumerable<string> EnumerateFiles(string path)
        {
            return Directory.EnumerateFiles(path);
        }

        public IEnumerable<string> EnumerateFiles(string path, string searchPattern)
        {
            return Directory.EnumerateFiles(path, searchPattern);
        }

        public bool IsDirectoryExist(string path)
        {
            return Directory.Exists(path);
        }
    }
}
