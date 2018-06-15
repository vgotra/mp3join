using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Mp3join
{
    partial class Program
    {
        static void Main(string[] args)
        {
            if (args == null || !args.Any())
                return;

            var path = args.First();
            var dir = new Dir(path);

            var continueJoin = true;
            var expectedFile = $"{path}.mp3";

            while (continueJoin)
            {
                Iterate(dir);
                Convert(dir);
                dir = new Dir(path);

                continueJoin = !File.Exists(expectedFile);
            }

            Console.WriteLine("Completed mp3 join");
            Console.WriteLine($"Output file: {expectedFile}");
        }

        static void Iterate(Dir dir, string tab = "")
        {
            if (dir.Files != null && dir.Files.Any())
            {
                Console.WriteLine($"{tab}Dir: {dir.DirPath}");

                foreach (var file in dir.Files)
                {
                    Console.WriteLine($"{tab}- {Path.GetFileName(file)}");
                }
            }

            if (dir.SubDirs != null && dir.SubDirs.Any())
            {
                foreach (var subDir in dir.SubDirs)
                {
                    Iterate(subDir, tab + " ");
                }
            }
        }

        static void Convert(Dir dir)
        {
            if (dir.Files.Any())
            {
                var outputFile = $"{dir.DirPath}.mp3";

                using(var writer = new BinaryWriter(File.OpenWrite(outputFile)))
                {
                    writer.Seek(0, SeekOrigin.End);

                    foreach (var file in dir.Files)
                    {
                        using (var reader = new BinaryReader(File.OpenRead(file)))
                        {
                            var buffer = new byte[1024];

                            while(reader.BaseStream.Position < reader.BaseStream.Length)
                            {
                                var count = reader.Read(buffer, 0, buffer.Length);
                                writer.Write(buffer, 0, count);
                            }
                        }
                    }
                }

                Console.WriteLine($"Output file completed: {outputFile}");

                foreach (var file in dir.Files)
                {
                    File.Move(file, Path.ChangeExtension(file, ".mp_"));
                }
            }

            if (dir.SubDirs != null)
            {
                foreach (var subDir in dir.SubDirs)
                {
                    Convert(subDir);
                }
            }
        }
    }
}