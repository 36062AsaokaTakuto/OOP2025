﻿using System.Diagnostics.Metrics;
using System.Threading;

namespace DistanceConverter {
    internal class Program {
        static void Main(string[] args) {

            int start = int.Parse(args[1]);
            int end = int.Parse(args[2]);

            if ("-tom" == args[0]) { //if (args.length >= 1 && args[0] == "-tom") {
                PrintFeetToMeterList(start, end);
            } else {
                PrintMeterToFeetList(start, end);
            }
        }

        //フィートからメートルへの対応表を出力
        static void PrintFeetToMeterList(int start, int end) {
            for (int feet = start; feet <= end; feet++) {
                //double meter = feet * 0.3048;
                double meter = FeetToMeter(feet);
                Console.WriteLine($"{feet}ft = {meter:0.0000}m");
            }
        }

        //メートルからフィートへの対応表を出力
        static void PrintMeterToFeetList(int start, int end) {
            for (int meter = start; meter <= end; meter++) {
                //double meter = feet * 0.3048;
                double feet = MeterToFeet(meter);
                Console.WriteLine($"{meter}m = {feet:0.0000}ft");


            }
        }
        //フィートからメートル
        static double FeetToMeter(int feet) {
            return feet * 0.3048;
        }

        static double MeterToFeet(int meter) {
            return meter / 0.3048;
        }



    }
}
