using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace TextTokenizer
{
    public class TextParser
    {
        public static Text ParseFile(string filePath)
        {
            Text text = new Text();
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Файл {filePath} не найден!");
                return text;
            }

            string content = File.ReadAllText(filePath);
            return ParseText(content);
        }

        public static Text ParseText(string content)
        {
            Text text = new Text();
            string[] rawSentences = Regex.Split(content, @"(?<=[.!?])\s+");

            foreach (string rawSentence in rawSentences)
            {
                if (string.IsNullOrWhiteSpace(rawSentence)) continue;

                Sentence sentence = new Sentence();
                string pattern = @"([А-Яа-яA-Za-z0-9]+)|([^\sА-Яа-яA-Za-z0-9])";
                MatchCollection matches = Regex.Matches(rawSentence, pattern);

                foreach (Match match in matches)
                {
                    if (match.Groups[1].Success)
                    {
                        sentence.AddToken(new Word(match.Value));
                    }
                    else if (match.Groups[2].Success)
                    {
                        sentence.AddToken(new Punctuation(match.Value));
                    }
                }

                if (sentence.Tokens.Count > 0)
                {
                    text.AddSentence(sentence);
                }
            }

            return text;
        }
    }
}