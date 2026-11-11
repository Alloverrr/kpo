using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace TextTokenizer
{
    public class TextProcessor
    {
        // Задание 1
        public static List<Sentence> SortByWordCount(Text text)
        {
            return text.Sentences.OrderBy(s => s.GetWordCount()).ToList();
        }

        // Задание 2
        public static List<Sentence> SortByLength(Text text)
        {
            return text.Sentences.OrderBy(s => s.GetLength()).ToList();
        }

        // Задание 3
        public static List<string> FindWordsInQuestions(Text text, int length)
        {
            HashSet<string> uniqueWords = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            foreach (var sentence in text.Sentences)
            {
                if (sentence.IsQuestion())
                {
                    foreach (var word in sentence.GetWords())
                    {
                        if (word.Value.Length == length)
                        {
                            uniqueWords.Add(word.Value.ToLower());
                        }
                    }
                }
            }
            return uniqueWords.ToList();
        }

        // Задание 4
        public static Text RemoveWordsStartingWithConsonant(Text text, int length)
        {
            Text newText = new Text();
            string consonants = "бвгджзйклмнпрстфхцчшщbcdfghjklmnpqrstvwxz";

            foreach (var sentence in text.Sentences)
            {
                Sentence newSentence = new Sentence();
                foreach (var token in sentence.Tokens)
                {
                    bool shouldRemove = false;

                    if (token is Word word)
                    {
                        if (word.Value.Length == length && word.Value.Length > 0)
                        {
                            char firstChar = char.ToLower(word.Value[0]);
                            if (consonants.Contains(firstChar))
                            {
                                shouldRemove = true;
                            }
                        }
                    }

                    if (!shouldRemove)
                    {
                        newSentence.AddToken(token);
                    }
                }
                if (newSentence.Tokens.Count > 0)
                {
                    newText.AddSentence(newSentence);
                }
            }
            return newText;
        }

        // Задание 5
        public static Text ReplaceWordsInSentence(Text text, int sentenceIndex, int wordLength, string replacement)
        {
            Text newText = new Text();

            for (int i = 0; i < text.Sentences.Count; i++)
            {
                Sentence oldSentence = text.Sentences[i];
                Sentence newSentence = new Sentence();

                foreach (var token in oldSentence.Tokens)
                {
                    if (i == sentenceIndex && token is Word word && word.Value.Length == wordLength)
                    {
                        newSentence.AddToken(new Word(replacement));
                    }
                    else
                    {
                        newSentence.AddToken(token);
                    }
                }
                newText.AddSentence(newSentence);
            }
            return newText;
        }

        // Задание 6
        public static Text RemoveStopWords(Text text, string stopWordsFilePath)
        {
            HashSet<string> stopWords = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            if (System.IO.File.Exists(stopWordsFilePath))
            {
                string[] lines = System.IO.File.ReadAllLines(stopWordsFilePath);
                foreach (var line in lines)
                {
                    string word = line.Trim();
                    if (!string.IsNullOrEmpty(word))
                    {
                        stopWords.Add(word);
                    }
                }
            }

            Text newText = new Text();

            foreach (var sentence in text.Sentences)
            {
                Sentence newSentence = new Sentence();

                foreach (var token in sentence.Tokens)
                {
                    bool isStopWord = false;

                    if (token is Word word)
                    {
                        if (stopWords.Contains(word.Value.ToLower()))
                        {
                            isStopWord = true;
                        }
                    }

                    if (!isStopWord)
                    {
                        newSentence.AddToken(token);
                    }
                }

                if (newSentence.Tokens.Count > 0)
                {
                    newText.AddSentence(newSentence);
                }
            }
            return newText;
        }

        // Задание 7
        public static void ExportToXml(Text text, string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Text));
            using (System.IO.FileStream fs = new System.IO.FileStream(filePath, System.IO.FileMode.Create))
            {
                serializer.Serialize(fs, text);
            }
        }
    }
}