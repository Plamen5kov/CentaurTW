namespace CentaurFactory.Data
{
    using MongoDB.Bson;
    using System;

    public class Dish
    {
        ObjectId Id;
        public string Name { get; set; }
        private decimal price;

        public decimal Price
        {
            get
            {
                return this.price;
            }
            set
            {
                if (value == null || value <= 0)
                {
                    throw new ArgumentNullException("Price cannot be null or less than zero.");
                }
                this.price = value;
            }
        }

    }
}
