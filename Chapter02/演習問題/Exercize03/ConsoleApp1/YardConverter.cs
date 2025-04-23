using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercize02{
    class YardConverter{
        //定数
        private const double retio = 0.9144;
        //ヤードからメートルを求める
        public static double ToMeter(double yard) {
            return yard * retio;
        }

        //メートルからヤードを求める
        public static double FromMeter(double meter) {
            return meter / retio;
        }
    }
}
