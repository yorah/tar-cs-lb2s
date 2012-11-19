using System;
using System.Diagnostics;
using System.IO;

namespace tar_cs.Tests
{
    public class BaseFixture
    {
        protected static string GeneratedFilesFolder
        {
            get { return @"GeneratedFiles"; }
        }

        static BaseFixture()
        {
            // Do the setup in the static ctor, so it only happens once.
            // From https://github.com/libgit2/libgit2sharp/ (BaseFixture.cs)
            var generatedFilesFolder = new DirectoryInfo(GeneratedFilesFolder);

            if (generatedFilesFolder.Exists)
            {
                DirectoryHelper.DeleteSubdirectories(generatedFilesFolder.FullName);
            }
        }

        protected Stream BuildOutStream()
        {
#if GENERATETESTFILES
            var folder = new DirectoryInfo(GeneratedFilesFolder);
            var testFolder = folder.CreateSubdirectory(new StackTrace(false).GetFrame(1).GetMethod().Name);

            return new FileStream(Path.Combine(testFolder.FullName, Guid.NewGuid().ToString()) + ".tar", FileMode.Create);
#else
            return new MemoryStream();
#endif
        }
    }
}
