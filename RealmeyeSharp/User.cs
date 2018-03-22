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
        public User() { }

        
    }
}
