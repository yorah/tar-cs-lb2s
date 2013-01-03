using System;
using System.IO;

namespace tar_cs.Tests.TestHelpers
{
    public class FileEntry : IDisposable
    {
        public FileEntry(string path, FileStream fileStream)
        {
            Path = path;
            Stream = fileStream;
        }

        public string Path { get; private set; }

        public FileStream Stream { get; private set; }

        #region Implementation of IDisposable

        public void Dispose()
        {
            Stream.Dispose();
        }

        #endregion
    }
}