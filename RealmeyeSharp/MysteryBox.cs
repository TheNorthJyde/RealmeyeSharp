using System.Collections.ObjectModel;

namespace RealmeyeSharp
{
    public class MysteryBox
    {
        public string Name { get; set; }
        public string Price { get; set; }
        public string EndsAt { get; set; }
        public ObservableCollection<MPrize> Prizes { get; set; } = new ObservableCollection<MPrize>();
    }

    public class MPrize
    {
        public bool Jackpot { get; set; }
        public ObservableCollection<MItem> Items { get; set; } = new ObservableCollection<MItem>();
    }

    public class MItem
    {
        public string Name { get; set; }
        public string Quantity { get; set; }
    }
}
