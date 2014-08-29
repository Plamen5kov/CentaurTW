namespace CentaurFactory.Data
{
    public class UnitType
    {
        private static int ID = 0;
        public int Id { get; set; }
        public string Name { get; set; }

        public UnitType(string unitName)
        {
            ID++;
            this.Id = ID;
            this.Name = unitName;
        }
        public override string ToString()
        {
            return this.Id + " " + this.Name;
        }
    }
}
