using Common;
using System.Text;

namespace Permute;

internal class Program
{
    static int[] ptrs;
    static List<string> lines = new();

    static void Main(string[] args)
    {
        var assy = new AssemblyInfo();

        // Get the path for the file to read containing the characters
        if (args.Length < 1)
        {
            Console.WriteLine($"{assy.Title} v{assy.MajorVersion}.{assy.MinorVersion}");
            Console.WriteLine($"Syntax: {assy.Title} <path_to_file>");
            Console.WriteLine("The specified file is read and a list of all possible combinations of the characters in the file is the output.  Output can be redirected to a file -- for example:  'permute C:\\Temp\\input.txt > permute.txt'.");
            Console.WriteLine("The specified file must contain a list of characters for each position, with each list on a separate line, each line representing the position in the string; i.e., 1st line are characters for 1st position, 2nd line are characters for 2nd position, etc.  Samples are included with the original program as 'simple.txt' and 'complext.txt'.");
            return;
        }

        var curdir = Directory.GetCurrentDirectory();
        var filepath = args[0];
        if (!File.Exists(filepath))
        {
            filepath = $"{curdir}\\{filepath}";
            if (!File.Exists(filepath))
            {
                throw new Exception($"File '{filepath}' does not exist.");
            }
        }

        // Read the contents of the file
        using (StreamReader sr = new StreamReader(filepath))
        {
            while (!sr.EndOfStream)
            {
                lines.Add(sr.ReadLine());
            }
        }
        ptrs = new int[lines.Count];

        Show(lines, 0);
    }

    static void Show(List<string> list, int listIndex)
    {
        var line = list[listIndex];
        ptrs[listIndex] = 0;
        for (int i = 0; i < line.Length; i++)
        {
            if (listIndex < list.Count - 1)
            {
                listIndex++;
                Show(list, listIndex);

                // Set the pointers for lower lines to zero
                for (int k = listIndex; k < list.Count; k++)
                {
                    ptrs[k] = 0;
                }
                // Next character
                listIndex--;
            }
            else
            {
                // We've reached the bottom of the list, so
                // display the results from each line
                StringBuilder sb = new();
                for (int j = 0; j < list.Count; j++)
                {
                    sb.Append(list[j][ptrs[j]]);
                }
                Console.WriteLine(sb);
            }

            if (ptrs[listIndex] < line.Length - 1)
            {
                // Advance the current pointer
                ptrs[listIndex]++;
            }
        }
    }

}
