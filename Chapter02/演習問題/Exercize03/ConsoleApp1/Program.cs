namespace Exercize02 {
    internal class YardProgram {
        static void Main(string[] args) {
            Console.Write("変換前：");
            int num = int.Parse(Console.ReadLine());//数字入力
            Console.Write("変換：");
            string henkan = Console.ReadLine();
            if ("-tom" == henkan) {
                PrintYardToMeterList(num);
            } else {
                PrintMeterToYardList(num);
            }
        }
        // ヤードからメートルへの対応表を出力
        static void PrintYardToMeterList(int num) {
            int yard = num;
            double meter = YardConverter.ToMeter(yard);
            Console.WriteLine($"{yard}yard = {meter:0.000}m");

        }

        //メートルからヤードへの対応表を出力
        static void PrintMeterToYardList(int num) {
            int meter = num;
            double yard = YardConverter.FromMeter(meter);
            Console.WriteLine($"{meter}m = {yard:0.000}yard");
        }

    }
}