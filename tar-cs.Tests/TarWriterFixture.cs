using System;
using System.IO;
using Xunit;

namespace tar_cs.Tests
{
    public class TarWriterFixture : BaseFixture
    {
        [Fact]
        public void CanCreateAnEmptyTarFile()
        {
            using (Stream outStream = BuildOutStream())
            {
                using (new TarWriter(outStream))
                { }

                outStream.Seek(0, SeekOrigin.Begin);

                using (var reader = new StreamReader(outStream))
                {
                    string content = reader.ReadToEnd();
                    Assert.Equal(new String('\0', 1024), content);
                }
            }
        }
    }
}
