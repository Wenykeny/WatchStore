namespace WatchStore.Data.CartServe
{
    public class CartItems
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public double Price { get; set; }

        public int Quantity { get; set; }

        public double SubTotal { get; set; }

        //public CartItems()
        //{
        //}

        public override string ToString()
        {
            return "ID" + Id + ", Name: " + Name + ", Price: " + Price + ", Qty: " + Quantity + ", SubTotal: " + SubTotal;
        }

    }
}
