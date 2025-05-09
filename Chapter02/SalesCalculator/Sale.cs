using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesCalculator{
    //売り上げクラス
    public class Sale{
        //店舗名
        public string ShopName { get; set; } = string.Empty;//(= string.Empty)空を入れてる。("")と一緒
        //商品カテゴリ
        public string ProductCategory { get; set; } = string.Empty;
        //売上高
        public int Amount { get; set; }

    }
}
