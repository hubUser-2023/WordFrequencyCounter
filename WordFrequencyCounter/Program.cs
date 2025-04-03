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
            // 1. Определение входного файла:
            string inputFilePath = GetInputFilePath(args);
            if (!File.Exists(inputFilePath))
            {
                Console.Error.WriteLine($"Ошибка: Файл не найден: {inputFilePath}");
                return;
            }

            // 2. Чтение содержимого файла
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

            // 3. Подсчёт частот слов
            IWordFrequencyCalculator calculator = new WordFrequencyCalculator();
            Dictionary<string, int> wordCounts = calculator.CalculateFrequencies(text);

            // 4. Генерация CSV контента
            ICsvGenerator csvGenerator = new CsvGenerator();
            string csvContent = csvGenerator.GenerateCsvContent(wordCounts);

            // 5. Определение пути для CSV-файла: тот же каталог, что и входной файл, с расширением .csv
            string directory = Path.GetDirectoryName(inputFilePath);
            if (string.IsNullOrEmpty(directory))
                directory = Directory.GetCurrentDirectory();
            string outputFilePath = Path.Combine(directory, Path.GetFileNameWithoutExtension(inputFilePath) + ".csv");

            // 6. Запись CSV-файла
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
