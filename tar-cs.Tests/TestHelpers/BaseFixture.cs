using System;
using System.Diagnostics;
using System.IO;

namespace tar_cs.Tests.TestHelpers
{
    public class BaseFixture
    {
        protected static DirectoryInfo GeneratedFilesFolder
        {
            get { return new DirectoryInfo(@"GeneratedFiles"); }
        }

        protected static DirectoryInfo ResourcesFolder
        {
            get { return new DirectoryInfo(@"Resources"); }
        }

        static BaseFixture()
        {
            // Do the setup in the static ctor, so it only happens once.
            // From https://github.com/libgit2/libgit2sharp/ (BaseFixture.cs)
            var sourceResourcesFolder = new DirectoryInfo(@"..\..\Resources");

            if (GeneratedFilesFolder.Exists)
            {
                DirectoryHelper.DeleteSubdirectories(GeneratedFilesFolder.FullName);
            }

            if (ResourcesFolder.Exists)
            {
                DirectoryHelper.DeleteSubdirectories(ResourcesFolder.FullName);
            }

            DirectoryHelper.CopyFilesRecursively(sourceResourcesFolder, ResourcesFolder);
        }

        protected Stream BuildOutStream()
        {
#if GENERATETESTFILES
            var testFolder = GeneratedFilesFolder.CreateSubdirectory(new StackTrace(false).GetFrame(1).GetMethod().Name);

            return new FileStream(Path.Combine(testFolder.FullName, Guid.NewGuid().ToString()) + ".tar", FileMode.Create);
#else
            return new MemoryStream();
#endif
        }

        protected FileEntry GetFileEntryFrom(string path)
        {
            return new FileEntry(path, new FileStream(Path.Combine(ResourcesFolder.FullName, path), FileMode.Open));
        }

        protected StreamReader GetExpectedStream(string expectedFileName)
        {
            var resultFolder = Path.Combine(ResourcesFolder.FullName, "results");
            return new StreamReader(Path.Combine(resultFolder, expectedFileName) + ".tar");
        }
    }
}
