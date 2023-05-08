/*
 * Copyright (c) 2023 Rod Barnes
 * See the LICENSE.txt file in the project root for specific restrictions.
 */
﻿using Common;
using System.Text;

namespace Permute;

internal class Program
{
    static int[] currentPtr;
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

        // Create an array of pointers, one for each line
        currentPtr = new int[lines.Count];

        // Start the process
        ShowChars(lines, 0);
    }

    /// <summary>
    /// Recursive method to process a list of character combinations
    /// </summary>
    /// <param name="list">List contains the string of characters in each line</param>
    /// <param name="listIndex">index into the list identifying the current line</param>
    static void ShowChars(List<string> list, int listIndex)
    {
        var line = list[listIndex];
        currentPtr[listIndex] = 0;

        // Iterate the charaters in the current line
        for (int i = 0; i < line.Length; i++)
        {
            // If not yet at the end of the list...
            if (listIndex < list.Count - 1)
            {
                // Move to the next line and do the same with it
                listIndex++;
                ShowChars(list, listIndex);

                // Upon return, reset this line's pointer
                // so it will be ready for the next run
                currentPtr[listIndex] = 0;

                // Back up to the previous line and continue
                listIndex--;
            }
            else
            {
                // Reached the end of the list, so
                // display the current character from each line
                StringBuilder sb = new();
                for (int j = 0; j < list.Count; j++)
                {
                    sb.Append(list[j][currentPtr[j]]);
                }
                Console.WriteLine(sb);
            }

            // If not yet at the end of the string...
            if (currentPtr[listIndex] < line.Length - 1)
            {
                // Advance the current pointer to the next character
                currentPtr[listIndex]++;
            }
        }
    }
}
