using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GeneticSearch
{
    struct GeneticData
    {
        public string Protein;
        public string Organism;
        public string AminoAcids;
    }

    class Program
    {
        private const string SequencesFile = "sequences.1.txt";
        private const string CommandsFile = "commands.1.txt";
        private const string OutputFile = "genedata.txt";
        private const int SeparatorLength = 74;

        private static readonly List<GeneticData> Database = new List<GeneticData>();

        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;

            LoadSequences(SequencesFile);
            Console.WriteLine($"Загружено белков: {Database.Count}");

            ProcessCommands(CommandsFile, OutputFile);
            Console.WriteLine($"Результат записан в {OutputFile}");

            Console.WriteLine("\nНажмите любую клавишу для выхода...");
            Console.ReadKey();
        }

        //загрузка последовательностей
        static void LoadSequences(string path)
        {
            string[] lines = File.ReadAllLines(path);

            foreach (string line in lines)
            {
                string[] parts = line.Split('\t');

                GeneticData g = new GeneticData
                {
                    Protein = parts[0].Trim(),
                    Organism = parts[1].Trim(),
                    AminoAcids = RLDecoding(parts[2].Trim())
                };

                Database.Add(g);
            }
        }

        //обработка команд
        static void ProcessCommands(string commandsPath, string outputPath)
        {
            string[] commandLines = File.ReadAllLines(commandsPath);
            StringBuilder output = new StringBuilder();

            output.AppendLine("Волчкевич Алеся");
            output.AppendLine("Genetic Searching");

            int counter = 0;
            foreach (string line in commandLines)
            {
                counter++;
                string number = counter.ToString("D3");

                string[] parts = line.Split('\t');
                string operation = parts[0].Trim().ToLower();

                WriteSeparator(output);

                switch (operation)
                {
                    case "search":
                        HandleSearch(number, parts[1].Trim(), output);
                        break;

                    case "diff":
                        HandleDiff(number, parts[1].Trim(), parts[2].Trim(), output);
                        break;

                    case "mode":
                        HandleMode(number, parts[1].Trim(), output);
                        break;
                }
            }

            File.WriteAllText(outputPath, output.ToString(), Encoding.UTF8);
        }

        static void WriteSeparator(StringBuilder output)
        {
            output.AppendLine(new string('-', SeparatorLength));
        }

        //search
        static void HandleSearch(string number, string rawPattern, StringBuilder output)
        {
            string pattern = RLDecoding(rawPattern);

            output.AppendLine($"{number}   search   {pattern}");
            output.AppendLine("organism\t\t\t\tprotein");

            bool found = false;
            foreach (var g in Database)
            {
                if (g.AminoAcids.Contains(pattern))
                {
                    output.AppendLine($"{g.Organism}\t\t{g.Protein}");
                    found = true;
                }
            }

            if (!found)
                output.AppendLine("NOT FOUND");
        }

        //diff
        static void HandleDiff(string number, string protein1, string protein2, StringBuilder output)
        {
            output.AppendLine($"{number}   diff   {protein1}   {protein2}");
            output.AppendLine("amino-acids difference:");

            GeneticData? g1 = FindProtein(protein1);
            GeneticData? g2 = FindProtein(protein2);

            if (g1 == null || g2 == null)
            {
                var missing = new List<string>();
                if (g1 == null) missing.Add(protein1);
                if (g2 == null) missing.Add(protein2);
                output.AppendLine("MISSING: " + string.Join(", ", missing));
                return;
            }

            int diff = CountDifferences(g1.Value.AminoAcids, g2.Value.AminoAcids);
            output.AppendLine(diff.ToString());
        }

        static int CountDifferences(string a, string b)
        {
            int maxLen = Math.Max(a.Length, b.Length);
            int diff = 0;

            for (int i = 0; i < maxLen; i++)
            {
                char ca = i < a.Length ? a[i] : '\0';
                char cb = i < b.Length ? b[i] : '\0';
                if (ca != cb) diff++;
            }

            return diff;
        }

        //mode
        static void HandleMode(string number, string protein, StringBuilder output)
        {
            output.AppendLine($"{number}   mode   {protein}");
            output.AppendLine("amino-acid occurs:");

            GeneticData? g = FindProtein(protein);
            if (g == null)
            {
                output.AppendLine("MISSING: " + protein);
                return;
            }

            var (letter, count) = MostFrequentAminoAcid(g.Value.AminoAcids);
            output.AppendLine($"{letter}\t\t{count}");
        }

        static (char Letter, int Count) MostFrequentAminoAcid(string chain)
        {
            int[] counts = new int[26];

            foreach (char c in chain)
            {
                if (c >= 'A' && c <= 'Z')
                    counts[c - 'A']++;
            }

            int bestCount = -1;
            char bestChar = 'A';

            for (int i = 0; i < 26; i++)
            {
                if (counts[i] > bestCount)
                {
                    bestCount = counts[i];
                    bestChar = (char)('A' + i);
                }
            }

            return (bestChar, bestCount);
        }

        //поиск белка по имени
        static GeneticData? FindProtein(string name)
        {
            foreach (var g in Database)
            {
                if (g.Protein == name)
                    return g;
            }
            return null;
        }

        //rle декодирование
        static string RLDecoding(string s)
        {
            StringBuilder result = new StringBuilder(s.Length * 2);
            int i = 0;

            while (i < s.Length)
            {
                int count = 0;
                bool hasDigit = false;

                while (i < s.Length && char.IsDigit(s[i]))
                {
                    count = count * 10 + (s[i] - '0');
                    hasDigit = true;
                    i++;
                }

                if (i >= s.Length) break;

                char c = s[i];
                i++;

                if (hasDigit)
                    result.Append(c, count);
                else
                    result.Append(c);
            }

            return result.ToString();
        }
    }
}