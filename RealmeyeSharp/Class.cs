using System;
using System.Collections.Generic;
using System.Text;

namespace RealmeyeSharp 
{
    public class Class
    {
        public string ClassName { get; set; }
        public int Lvl { get; set; }
        public string CQC { get; set; }
        public int Fame { get; set; }
        public string Eq1 { get; set; }
        public string Eq2 { get; set; }
        public string Eq3 { get; set; }
        public string Eq4 { get; set; }
        public bool Backpack { get; set; }
        public string Stats { get; set; }

        public Class(string classname, int lvl, string cqc, int fame, string eq1, string eq2, string eq3, string eq4, bool backpack, string stats)
        {
            ClassName = classname;
            Lvl = lvl;
            CQC = cqc;
            Fame = fame;
            Eq1 = eq1;
            Eq2 = eq2;
            Eq3 = eq3;
            Eq4 = eq4;
            Backpack = backpack;
            Stats = stats;
        }
    }
}
