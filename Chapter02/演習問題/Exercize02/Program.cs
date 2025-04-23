namespace Exercize02 {
    internal class Program {
        static void Main(string[] args) {
            Console.Write("始め：");
            int start = int.Parse(Console.ReadLine());//数字入力
            Console.Write("終わり：");
            int end = int.Parse(Console.ReadLine());//数字入力
            Console.Write("変換：");
            string henkan = Console.ReadLine();
            if ("-tom" == henkan) { 
                PrintInchToMeterList(start,end);
            } else {
                PrintMeterToFeetList(start, end);
            }
        }
            // インチからメートルへの対応表を出力
            static void PrintInchToMeterList(int start, int end) {
                for (int Inch = start; Inch <= end; Inch++) {
                    //double meter = feet * 0.3048;
                    double meter = InchConverter.ToMeter(Inch);
                    Console.WriteLine($"{Inch}inch = {meter:0.0000}m");
                }
            }

        //メートルからフィートへの対応表を出力
        static void PrintMeterToFeetList(int start, int end) {
            for (int meter = start; meter <= end; meter++) {
                //double meter = feet * 0.3048;
                double inch = InchConverter.FromMeter(meter);
                Console.WriteLine($"{meter}m = {inch:0.0000}inch");

            }

        }
    }
}
