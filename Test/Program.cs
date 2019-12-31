using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using GedcomCore.Framework.Parser;
using GedcomCore.Framework.Writer;

namespace GeneaGedcom.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            args = new[] { "TGC55C.ged" };

            if (args.Length != 1)
            {
                Console.WriteLine("usage: gg-test.exe filename");
                return;
            }

            var parser = new GedcomParser();
            var file = new FileInfo(args[0]);
            var gedcom = parser.Parse(file.OpenRead());

            var outFile = new FileInfo("text-out.txt");
            var writer = new GedcomWriter();

            Stream s = outFile.OpenWrite();

            writer.Write(gedcom, s);

            s.Close();

            Console.WriteLine("read {0} objects", gedcom.Records.Count);
        }
    }
}
