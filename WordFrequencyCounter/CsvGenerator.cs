using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WordFrequencyCounter
{
    public interface ICsvGenerator
    {
        string GenerateCsvContent(Dictionary<string, int> wordCounts);
    }

    public class CsvGenerator : ICsvGenerator
    {
        public string GenerateCsvContent(Dictionary<string, int> wordCounts)
        {
            if (wordCounts == null)
            {
                throw new ArgumentNullException(nameof(wordCounts));
            }

            long totalWords = wordCounts.Values.Sum();
            var sortedWords = wordCounts.OrderByDescending(pair => pair.Value)
                                        .ThenBy(pair => pair.Key)
                                        .ToList();

            var sb = new StringBuilder();
            sb.AppendLine("Слово,Частота,Частота (%)");

            foreach (var pair in sortedWords)
            {
                double percentage = totalWords > 0 ? (double)pair.Value / totalWords * 100.0 : 0;
                sb.AppendLine($"{pair.Key},{pair.Value},{percentage:F2}");
            }
            return sb.ToString();
        }
    }
}