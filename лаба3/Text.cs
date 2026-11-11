using System;
using System.Collections.Generic;

namespace TextTokenizer
{
    public class Text
    {
        public List<Sentence> Sentences { get; set; }

        public Text()
        {
            Sentences = new List<Sentence>();
        }

        public void AddSentence(Sentence sentence)
        {
            Sentences.Add(sentence);
        }

        public override string ToString()
        {
            return string.Join(" ", Sentences.Select(s => s.ToString()));
        }
    }
}