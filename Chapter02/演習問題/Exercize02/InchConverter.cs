using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercize02{
    public static class InchConverter{
        //定数
        private const double retio = 0.0254;
        //インチからメートルを求める
        public static double ToMeter(double meter) {
            return meter * retio;
        }
    }
}
