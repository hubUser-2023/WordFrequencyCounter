using System;
using System.Collections.Generic;
using Xunit;
using WordFrequencyCounter;

namespace WordFrequencyCounter.Tests
{
    public class WordFrequencyCalculatorTests
    {
        [Fact]
        public void CalculateFrequencies_SampleText_ReturnsCorrectFrequencies()
        {
            string sampleText = "Hello, world! Hello World.";
            IWordFrequencyCalculator calculator = new WordFrequencyCalculator();

            Dictionary<string, int> result = calculator.CalculateFrequencies(sampleText);

            Assert.Equal(2, result.Count);
            Assert.Contains("hello", result.Keys, StringComparer.OrdinalIgnoreCase);
            Assert.Contains("world", result.Keys, StringComparer.OrdinalIgnoreCase);
            Assert.Equal(2, result["hello"]);
            Assert.Equal(2, result["world"]);
        }

        [Fact]
        public void CalculateFrequencies_EmptyText_ReturnsEmptyDictionary()
        {
            // Arrange
            string emptyText = "";
            IWordFrequencyCalculator calculator = new WordFrequencyCalculator();

            // Act
            Dictionary<string, int> result = calculator.CalculateFrequencies(emptyText);

            // Assert
            Assert.Empty(result);
        }
    }
}
