namespace CentaurFactory.Data
{
    public class ProductType
    {
        private static int ID = 0;
        public int Id { get; set; }
        public string Name { get; set; }

        public ProductType(string productTypeName)
        {
            ID++;
            this.Id = ID;
            this.Name = productTypeName;
        }

        public override string ToString()
        {
            return this.Id + " " + this.Name;
        }
    }
}
