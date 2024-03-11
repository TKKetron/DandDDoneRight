namespace DirtyDandD.Classes
{
    public class Item
    {
        public int value { get; set; }
        public string name { get; set; }
        public string description { get; set; }

        public Item(string name, string description, int value = 0)
        {
            this.name = name;
            this.description = description;
            this.value = value;
        }
    }
}
