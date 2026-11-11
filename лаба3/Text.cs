using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace TextTokenizer
{
    [XmlRoot("text")] // Корневой тег будет <text>
    public class Text
    {
        [XmlElement("sentence")] // Тег для предложений
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