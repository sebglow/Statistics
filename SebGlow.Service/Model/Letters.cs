using System;
using System.Collections.Generic;
using System.Linq;

namespace SebGlow.Service.Model
{
    public class Letters
    {
        private IDictionary<char, int> letters = new Dictionary<char, int>
        {
            { 'a', 0 },
            { 'b', 0 },
            { 'c', 0 },
            { 'd', 0 },
            { 'e', 0 },
            { 'f', 0 },
            { 'g', 0 },
            { 'h', 0 },
            { 'i', 0 },
            { 'j', 0 },
            { 'k', 0 },
            { 'l', 0 },
            { 'm', 0 },
            { 'o', 0 },
            { 'p', 0 },
            { 'q', 0 },
            { 'r', 0 },
            { 's', 0 },
            { 't', 0 },
            { 'u', 0 },
            { 'v', 0 },
            { 'w', 0 },
            { 'x', 0 },
            { 'y', 0 },
            { 'z', 0 },
        };

        public IDictionary<char, int> LetterOccurrences => letters;

        public Letters()
        {
        }

        public Letters(KeyValuePair<char, int>[] occurranceArray)
        {
            foreach (var occurrance in occurranceArray)
            {
                letters[occurrance.Key] = occurrance.Value;                    
            }
        }

        public override bool Equals(object obj)
        {
            var toCompare = obj as Letters;

            if (toCompare == null)
                return false;

            return letters.SequenceEqual(toCompare.letters);
        }

        public static implicit operator KeyValuePair<char, int>[] (Letters l) => l.letters.ToArray();
        public static explicit operator Letters(KeyValuePair<char, int>[] a) => new Letters(a);
    }
}
