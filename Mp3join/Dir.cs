using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Mp3join
{
    partial class Program
    {
        class Dir
        {
            private List<Dir> _subDirs = null;
            private List<string> _files = null;

            public Dir(string dirPath, string pattern = "*.mp3")
            {
                DirPath = dirPath;
                Pattern = pattern;
            }

            public string DirPath { get; }
            public string Pattern { get; }

            public List<string> Files
            {
                get
                {
                    if (_files == null)
                    {
                        _files = Directory.EnumerateFiles(DirPath, Pattern, SearchOption.TopDirectoryOnly)
                            .OrderBy(x => x).ToList();
                    }

                    return _files;
                }
            }

            public List<Dir> SubDirs
            {
                get
                {
                    if (_subDirs == null)
                    {
                        _subDirs = Directory.EnumerateDirectories(DirPath).Select(x => new Dir(x, Pattern))
                            .OrderBy(x => x.DirPath).ToList();
                    }
                    return _subDirs;
                }
            }
        }
    }
}