using System;
using System.Collections.Generic;
using Xunit;
using WordFrequencyCounter;

namespace WordFrequencyCounter.Tests
{
    public class CsvGeneratorTests
    {
        [Fact]
        public void GenerateCsvContent_ValidDictionary_ReturnsCorrectCsv()
        {
            var wordCounts = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase)
            {
                { "hello", 2 },
                { "world", 2 },
                { "test", 1 }
            };
            ICsvGenerator csvGenerator = new CsvGenerator();

            string csvContent = csvGenerator.GenerateCsvContent(wordCounts);

            Assert.Contains("Слово,Частота,Частота (%)", csvContent);
            Assert.Contains("hello,2,", csvContent);
            Assert.Contains("world,2,", csvContent);
            Assert.Contains("test,1,", csvContent);
        }

        [Fact]
        public void GenerateCsvContent_NullDictionary_ThrowsArgumentNullException()
        {
            ICsvGenerator csvGenerator = new CsvGenerator();
            Assert.Throws<ArgumentNullException>(() => csvGenerator.GenerateCsvContent(null));
        }
    }
}
