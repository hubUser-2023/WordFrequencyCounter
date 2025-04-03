using System;
using System.Collections.Generic;
using System.Text;

namespace WordFrequencyCounter
{
    public interface IWordFrequencyCalculator
    {
        Dictionary<string, int> CalculateFrequencies(string text);
    }

    public class WordFrequencyCalculator : IWordFrequencyCalculator
    {
        public Dictionary<string, int> CalculateFrequencies(string text)
        {
            var wordCounts = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            var sb = new StringBuilder();

            foreach (char currentChar in text)
            {
                if (char.IsLetterOrDigit(currentChar))
                {
                    sb.Append(currentChar);
                }
                else
                {
                    if (sb.Length > 0)
                    {
                        string word = sb.ToString().ToLowerInvariant();
                        if (wordCounts.ContainsKey(word))
                        {
                            wordCounts[word]++;
                        }
                        else
                        {
                            wordCounts[word] = 1;
                        }
                        sb.Clear();
                    }
                }
            }

            if (sb.Length > 0)
            {
                string word = sb.ToString().ToLowerInvariant();
                if (wordCounts.ContainsKey(word))
                {
                    wordCounts[word]++;
                }
                else
                {
                    wordCounts[word] = 1;
                }
            }

            return wordCounts;
        }
    }
}