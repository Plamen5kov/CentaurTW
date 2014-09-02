namespace CentaurFactory.ExcelModel
{
    using System;

    public class Dish
    {
        private decimal price;

        public int DishId { get; set; }

        public string Name { get; set; }

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
