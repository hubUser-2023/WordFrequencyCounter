using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace WordFrequencyCounter
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputFilePath = GetInputFilePath(args);
            if (!File.Exists(inputFilePath))
            {
                Console.Error.WriteLine($"Ошибка: Файл не найден: {inputFilePath}");
                return;
            }

            string text;
            try
            {
                text = File.ReadAllText(inputFilePath, Encoding.UTF8);
            }
            catch (IOException e)
            {
                Console.Error.WriteLine("Ошибка при чтении файла: " + e.Message);
                return;
            }

            IWordFrequencyCalculator calculator = new WordFrequencyCalculator();
            Dictionary<string, int> wordCounts = calculator.CalculateFrequencies(text);

            ICsvGenerator csvGenerator = new CsvGenerator();
            string csvContent = csvGenerator.GenerateCsvContent(wordCounts);

            string directory = Path.GetDirectoryName(inputFilePath);
            if (string.IsNullOrEmpty(directory))
                directory = Directory.GetCurrentDirectory();
            string outputFilePath = Path.Combine(directory, Path.GetFileNameWithoutExtension(inputFilePath) + ".csv");

            try
            {
                File.WriteAllText(outputFilePath, csvContent, Encoding.UTF8);
                Console.WriteLine($"CSV файл успешно создан: {outputFilePath}");
            }
            catch (IOException e)
            {
                Console.Error.WriteLine("Ошибка при записи CSV файла: " + e.Message);
            }
        }

        private static string GetInputFilePath(string[] args)
        {
            if (args.Length > 0)
            {
                return args[0];
            }
            else
            {
                string inputFilePath = Environment.GetEnvironmentVariable("INPUT_FILE");
                if (string.IsNullOrEmpty(inputFilePath))
                {
                    inputFilePath = "input.txt";
                    Console.WriteLine("Аргументы не заданы. Используется файл по умолчанию: input.txt");
                }
                return inputFilePath;
            }
        }
    }
}