using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace TextTokenizer
{
    public class Sentence
    {
        // Настраиваем, как XML будет выводить токены
        [XmlElement("word", typeof(Word))]
        [XmlElement("punctuation", typeof(Punctuation))]
        public List<Token> Tokens { get; set; }

        public Sentence()
        {
            Tokens = new List<Token>();
        }

        public void AddToken(Token token)
        {
            Tokens.Add(token);
        }

        public List<Word> GetWords()
        {
            return Tokens.OfType<Word>().ToList();
        }

        public int GetWordCount()
        {
            return Tokens.Count(t => t is Word);
        }

        public int GetLength()
        {
            return Tokens.Sum(t => t.Value.Length);
        }

        public bool IsQuestion()
        {
            var lastToken = Tokens.LastOrDefault();
            return lastToken is Punctuation && lastToken.Value.Contains("?");
        }

        public override string ToString()
        {
            return string.Join(" ", Tokens.Select(t => t.Value));
        }
    }
}