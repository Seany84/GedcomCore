using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using GeneaGedcom.Parser;
using GeneaGedcom.Writer;

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

            GedcomParser parser = new GedcomParser();
            FileInfo file = new FileInfo(args[0]);
            LineageLinkedGedcom gedcom = parser.Parse(file.OpenRead());

            FileInfo outFile = new FileInfo("text-out.txt");
            GedcomWriter writer = new GedcomWriter();

            Stream s = outFile.OpenWrite();

            writer.Write(gedcom, s);

            s.Close();

            Console.WriteLine("read {0} objects", gedcom.Records.Count);
        }
    }
}
