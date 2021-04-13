namespace WatchStore.Data.ProductServe
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public double Price { get; set; }

        public int Quantity { get; set; }

        public override string ToString()
        {
            return "ID" + Id + ", Name: " + Name + ", Price: " + Price + ", Qty: " + Quantity;
        }
    }
}
