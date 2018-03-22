using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace RealmeyeSharp
{
    public class User
    {
        public string Name { get; set; }
        public int Chars { get; set; }
        public int Skins { get; set; }
        public int Fame { get; set; }
        public int Rank { get; set; }
        public int AccFame { get; set; }
        public string Guild { get; set; }
        public string Created { get; set; }
        public string PetName { get; set; }
        public string Petstat1 { get; set; }
        public string Petstat2 { get; set; }
        public string Petstat3 { get; set; }
        public int Petlvl1 { get; set; }
        public int Petlvl2 { get; set; }
        public int Petlvl3 { get; set; }
        public string Desc1 { get; set; }
        public string Desc2 { get; set; }
        public string Desc3 { get; set; }
        public ObservableCollection<Class> Classes { get; set; } = new ObservableCollection<Class>();


        public User() { }

        
    }
}
