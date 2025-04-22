namespace Exercize02 {
    internal class Program {
        static void Main(string[] args) {
            if ("-tom" == args[0]) { //if (args.length >= 1 && args[0] == "-tom") {
                PrintInchToMeterList(1, 10);
            }
        }
            // インチからメートルへの対応表を出力
            static void PrintInchToMeterList(int start, int end) {
                for (int Inch = 1; Inch <= 10; Inch++) {
                    //double meter = feet * 0.3048;
                    double meter = InchConverter.ToMeter(Inch);
                    Console.WriteLine($"{Inch}inch = {meter:0.0000}m");
                }
            }
        }
    }
