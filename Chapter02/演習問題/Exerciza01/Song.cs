using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exerciza01 {
    //2.1.1
    public class Song {
        //歌のタイトル
        public string Title { get; private set; } = string.Empty;
        //アーティスト名
        public string ArtistName { get; private set; } = string.Empty;
        //演奏時間 単位は秒
        public int Length { get; private set; }

        //2.1.2
        public Song (string title,string artistName,int length) {
            Title = title;
            ArtistName = artistName;
            Length = length;
        }
    } 

}
