namespace Labo8.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Value { get; set; }

        public Item(int id, string name, int value)
        {
            Id = id;
            Name = name;
            Value = value;
        }
        public Item() { }
    }
}
