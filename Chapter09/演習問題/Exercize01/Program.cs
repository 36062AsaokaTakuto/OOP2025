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

            //和暦が2桁表示（ゼロサブレスなし）
            var datestr = dateTime.ToString("ggyy", culture);
            var dayOfWeek = culture.DateTimeFormat.GetDayName(dateTime.DayOfWeek);

            var str = dateTime.ToString($"{datestr}年{dateTime.Month,2}月{dateTime.Day,2}日({dayOfWeek})");
            Console.WriteLine(str);

            //和暦が2桁表示（ゼロサブレスあり）
            var cul = dateTime.ToString("gg", culture);
            var year = int.Parse(dateTime.ToString("yy", culture));

            var str2 = dateTime.ToString($"{cul}{year,2}年{dateTime.Month,2}月{dateTime.Day,2}日({dayOfWeek})");
            Console.WriteLine(str2);
        }
    }
}
