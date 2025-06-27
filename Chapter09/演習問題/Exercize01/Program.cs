using System.Globalization;
using System.Text.RegularExpressions;

namespace Exercize01 {
    internal class Program {
        static void Main(string[] args) {
            var dateTime = DateTime.Now;
            DisPlayDatePattern1(dateTime);
            DisPlayDatePattern2(dateTime);
            DisPlayDatePattern3(dateTime);
        }

        private static void DisPlayDatePattern1(DateTime dateTime) {
            // 2024/03/09 19:03
            // string.Formatを使った例
            var str = string.Format($"{dateTime:yyyy/MM/dd HH:mm}");
            Console.WriteLine(str);
        }

        private static void DisPlayDatePattern2(DateTime dateTime) {
            // 2024年03月09日 19時03分09秒
            // DateTime.ToStringを使った例
            var str = dateTime.ToString($"{dateTime:yyyy年MM月dd日 HH時mm分ss秒}");
            Console.WriteLine(str);
        }

        private static void DisPlayDatePattern3(DateTime dateTime) {
            var culture = new CultureInfo("jp-JP");
            culture.DateTimeFormat.Calendar = new JapaneseCalendar();
            var str = dateTime.ToString("ggyy年MM月dd日", culture);
            Console.WriteLine(str + dateTime.ToString("ddd曜日", culture));

        }
    }
}
