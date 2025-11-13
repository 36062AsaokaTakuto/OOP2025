using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TextFileProcessor;

namespace LineCounter {
    internal class LineCounterProcessor : TextProcessor {
        private int _count = 0;
        private string? input;

        protected override void Initialize(string fname) {
            Console.Write("検索したい文字列を入力:");
            input = Console.ReadLine();
            _count = 0;
        }

        protected override void Execute(string line) {
            int index = 0;
            while ((index = line.IndexOf(input, index, StringComparison.OrdinalIgnoreCase)) >= 0) {
                _count++;
                index += input.Length;
            }                   
        }

        protected override void Terminate() => Console.WriteLine($"{input}の個数{_count}つ");
    }
}
