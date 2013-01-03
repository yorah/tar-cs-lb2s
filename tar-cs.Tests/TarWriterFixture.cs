using System;
using System.IO;
using Xunit;
using tar_cs.Tests.TestHelpers;

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

        [Fact]
        public void CanCreateATarFileWithOneFileEntry()
        {
            using (Stream outStream = BuildOutStream())
            {
                using (var writer = new TarWriter(outStream))
                using (var entry = GetFileEntryFrom(@"single\1.txt"))
                {
                    writer.Write(entry.Path, entry.Stream, 511, new DateTime(2013, 4, 1, 13, 12, 58, 548).ToUniversalTime());
                }

                outStream.Seek(0, SeekOrigin.Begin);

                using (var reader = new StreamReader(outStream))
                using (var expectedContentStream = GetExpectedStream("CanCreateATarFileWithOneFileEntry"))
                {
                    string content = reader.ReadToEnd();
                    string expectedContent = expectedContentStream.ReadToEnd();

                    Assert.Equal(expectedContent, content);
                }
            }
        }

        [Fact]
        public void CanCreateATarFileWithMultipleFileEntries()
        {
            using (Stream outStream = BuildOutStream())
            {
                using (var writer = new TarWriter(outStream))
                using (var entry1 = GetFileEntryFrom(@"multiple\1.txt"))
                using (var entry2 = GetFileEntryFrom(@"multiple\2.txt"))
                {
                    writer.Write(entry1.Path, entry1.Stream, 511, new DateTime(2013, 4, 1, 13, 12, 58, 548).ToUniversalTime());
                    writer.Write(entry2.Path, entry2.Stream, 511, new DateTime(2013, 4, 1, 13, 12, 58, 548).ToUniversalTime());
                }

                outStream.Seek(0, SeekOrigin.Begin);

                using (var reader = new StreamReader(outStream))
                using (var expectedContentStream = GetExpectedStream("CanCreateATarFileWithMultipleFileEntries"))
                {
                    string content = reader.ReadToEnd();
                    string expectedContent = expectedContentStream.ReadToEnd();

                    Assert.Equal(expectedContent, content);
                }
            }
        }

        [Fact]
        public void CanCreateATarFileWithMultipleFileEntriesWithADeepHierarchy()
        {
            using (Stream outStream = BuildOutStream())
            {
                using (var writer = new TarWriter(outStream))
                using (var entry1 = GetFileEntryFrom(@"deep\1.txt"))
                using (var entry2 = GetFileEntryFrom(@"deep\2\fox.txt"))
                using (var entry3 = GetFileEntryFrom(@"deep\2\3\dog.txt"))
                {
                    writer.Write(entry1.Path, entry1.Stream, 511, new DateTime(2013, 4, 1, 13, 12, 58, 548).ToUniversalTime());
                    writer.Write(entry2.Path, entry2.Stream, 511, new DateTime(2013, 4, 1, 13, 12, 58, 548).ToUniversalTime());
                    writer.Write(entry3.Path, entry3.Stream, 511, new DateTime(2013, 4, 1, 13, 12, 58, 548).ToUniversalTime());
                }

                outStream.Seek(0, SeekOrigin.Begin);

                using (var reader = new StreamReader(outStream))
                using (var expectedContentStream = GetExpectedStream("CanCreateATarFileWithMultipleFileEntriesWithADeepHierarchy"))
                {
                    string content = reader.ReadToEnd();
                    string expectedContent = expectedContentStream.ReadToEnd();

                    Assert.Equal(expectedContent, content);
                }
            }
        }
    }
}
