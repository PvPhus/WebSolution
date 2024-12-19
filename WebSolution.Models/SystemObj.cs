using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;

namespace WebSolution.Models
{
    public class SystemObj
    {
        public ObjectId id { get; set; }
        public String Key { get; set; }
        public int Value { get; set; }

        public SystemObj(string key, int value)
        {
            Key = key;
            Value = value;
        }

        public SystemObj()
        {
            Key = "";
            Value = 0;
        }
    }
    public enum Language
    {
        English = 0,//0
        German = 1,//1
        French = 2,//2
        Spanish = 3,//3
        Portuguese = 4,//4
        Italian = 5,//5
        Dutch = 6,//6
        Chinese = 7,//7
        Japanese = 8,//8
        Korean = 9,//9
        Arabic = 10,//10
        Hindi = 11,//11
        Malay = 12,//12
        Vietnamese = 13,//13
        Russian = 14,//14
        Polish = 15,//15
        Persian = 16,//16
        Romanian = 17,//17
        Indonesian = 18,//18
        Turkish = 19,//19
        Thai = 20//19
    }

    ////Language EU
    public enum LanguageEU
    {
        English = 0,
        Catalan = 1,//e
        Ukrainian = 2,//e
        Norwegian = 3,//e
        Hungary = 4,//e
        Greek = 5,//e
        Czech = 6,//e
        Swedish = 7,//e
        Serbian = 8,
        Bulgarian = 9,//e
        Iceland = 10,
        Croatian = 11,//e
        Danish = 12,//e
        Albanian = 13,
        Finnish = 14,//e
        Slovak = 15,//e
        Lithuania = 16,//e
        Latvia = 17,
        Estonia = 18,
        Slovenia = 19,//e
        Galician = 20
    }
}
