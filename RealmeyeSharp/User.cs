using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace RealmeyeSharp
{
    public class User
    {
        public string Name { get; set; }
        public string Chars { get; set; }
        public string Skins { get; set; }
        public string Fame { get; set; }
        public string Rank { get; set; }
        public string AccFame { get; set; }
        public string Guild { get; set; }
        public string GuildRank { get; set; }
        public string Created { get; set; }
        public string PetName { get; set; }
        public string Petstat1 { get; set; }
        public string Petstat2 { get; set; }
        public string Petstat3 { get; set; }
        public string Petlvl1 { get; set; }
        public string Petlvl2 { get; set; }
        public string Petlvl3 { get; set; }
        public string Desc1 { get; set; }
        public string Desc2 { get; set; }
        public string Desc3 { get; set; }
        public ObservableCollection<Class> Classes { get; set; } = new ObservableCollection<Class>();
    }
    
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

        /// <summary>
        /// creates a class
        /// </summary>
        /// <param name="classname"></param>
        /// <param name="lvl"></param>
        /// <param name="cqc"></param>
        /// <param name="fame"></param>
        /// <param name="eq1"></param>
        /// <param name="eq2"></param>
        /// <param name="eq3"></param>
        /// <param name="eq4"></param>
        /// <param name="backpack"></param>
        /// <param name="stats"></param>
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
