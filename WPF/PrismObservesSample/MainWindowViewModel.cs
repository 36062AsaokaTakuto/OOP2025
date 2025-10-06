﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrismObservesSample {
    class MainWindowViewModel : BindableBase {
        private string _input1 = "";
        public string Input1 {
            get => _input1;
            set => SetProperty(ref _input1, value);
        }

        private string _input2 = "";
        public string Input2 {
            get => _input2;
            set => SetProperty(ref _input2, value);
        }

        private string _result = "";
        public string Result {
            get => _result;
            set => SetProperty(ref _result, value);
        }

        public MainWindowViewModel() {
            SumCommand = new DelegateCommand(ExcuteSum,canExcuteSum);
        }

        //足し算の処理
        private void ExcuteSum() {
            //足し算の処理を記述
            int num1 = int.Parse(_input1);
            int num2 = int.Parse(_input2);
            int result = num1 + num2;
            Result = result.ToString();
        }

        //足し算処理を実行できるか否かのチェック
        private bool canExcuteSum() {
            
        }

        public DelegateCommand SumCommand { get; }
    }
}
