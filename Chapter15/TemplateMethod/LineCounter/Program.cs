using TextFileProcessor;

namespace LineCounter {
    internal class Program {
        static void Main(string[] args) {
            Console.Write("指定したいファイル：");
            string? path = Console.ReadLine()?.Trim().Trim('"');
            TextProcessor.Run<LineCounterProcessor>(path);
        }
    }
}
