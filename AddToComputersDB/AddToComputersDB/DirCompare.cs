using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AddToComputersDB
{
    class DirCompare
    {
        private string dest;
        private string source;

        /// <summary>
        /// Function that get two directories and check which files in source not exacly exist in dest
        /// </summary>
        /// <param name="source">Source directory who have all the files</param>
        /// <param name="dest">Destination directory who we not sure that have all files</param>
        /// <returns>Collection of files that not exacly exist in two directories</returns>
        public static IEnumerable<string> Compare(string source, string dest)
        {
            if (!Directory.Exists(source) || !Directory.Exists(dest))
                throw new DirectoryNotFoundException();

            DirectoryInfo sourceDir = new DirectoryInfo(source);
            DirectoryInfo destDir = new DirectoryInfo(dest);

            IEnumerable<FileInfo> sourceFiles = sourceDir.GetFiles();
            IEnumerable<FileInfo> destFiles = destDir.GetFiles();

            IEnumerable<string> filesNotExactInDest =
                sourceFiles.Except(destFiles, new FileCompared()).Select(f => $"File: {f.FullName}");

            IEnumerable<DirectoryInfo> sourceDirs = sourceDir.GetDirectories();
            IEnumerable<DirectoryInfo> destDirs = destDir.GetDirectories();

            filesNotExactInDest =
                filesNotExactInDest.Concat(sourceDirs.Except(destDirs, new DirectoryCompared())
                    .Select(d => $"Dir: {d.FullName}"));

            foreach (string directoryName in sourceDirs.Intersect(destDirs, new DirectoryCompared()).Select(d => d.Name))
            {
                filesNotExactInDest = filesNotExactInDest.Concat(
                    Compare(Path.Combine(source, directoryName), Path.Combine(dest, directoryName)));
            }

            return filesNotExactInDest;
        }

        public bool RequestFolders()
        {
            this.source = RequestFolder();
            this.dest = RequestFolder();

            return source != null && dest != null;
        }

        public static string RequestFolder()
        {
            Console.WriteLine("Enter directory path: ");
            string path = Console.ReadLine();
            if (!String.IsNullOrWhiteSpace(path) && Directory.Exists(path))
            {
                return path;
            }

            return null;
        }

        public IEnumerable<string> GetDifferences()
        {
            return Compare(source, dest);
        }

        public string GetSource() => this.source;
        public string GetDest() => this.dest;
    }

    internal class DirectoryCompared : IEqualityComparer<DirectoryInfo>
    {
        public bool Equals(DirectoryInfo x, DirectoryInfo y)
        {
            return x.Name.Equals(y.Name);
        }

        public int GetHashCode(DirectoryInfo obj)
        {
            //Console.WriteLine(obj.Name.GetHashCode());
            return obj.Name.GetHashCode();
        }
    }

    internal class FileCompared : IEqualityComparer<FileInfo>
    {
        public bool Equals(FileInfo x, FileInfo y)
        {
            return x.Name.Equals(y.Name) && x.Length == y.Length;
        }

        public int GetHashCode(FileInfo obj)
        {
            return $"{obj.Name}{obj.Length}".GetHashCode();
        }
    }
}
