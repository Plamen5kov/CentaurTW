namespace CentaurFactory.ExcelModel.Parsers
{
    using System;
using System.Collections.Generic;
using System.Data.OleDb;

    public class DeliveryParser
    {
        private ICollection<DeliveryInfo> deliveries;

        public DeliveryParser()
        {
            this.deliveries = new List<DeliveryInfo>();
        }

        public ICollection<DeliveryInfo> Deliveries
        {
            get
            {
                return this.deliveries;
            }
            set
            {
                this.deliveries = value;
            }
        }

        public void ReadFromExcelFiles()
        {
            OleDbConnectionStringBuilder excelConnectionString = new OleDbConnectionStringBuilder();
            excelConnectionString.Provider = "Microsoft.Jet.OLEDB.4.0"; ;
            excelConnectionString.DataSource = @"D:\Telerik\Repos\CentaurTW\CentaurFactory.ConsoleClient\Report-Jul-2013\20-Jul-2013\Deliveries-20-Jul-2013.xls";
            excelConnectionString.Add("Extended Properties", "Excel 8.0;HDR=Yes;IMEX=2");
            OleDbConnection excelConnection = new OleDbConnection(excelConnectionString.ConnectionString);

            excelConnection.Open();

            var cmd = new OleDbCommand("SELECT * FROM [Sheet1$]", excelConnection);

            var reader = cmd.ExecuteReader();
            using (reader)
            {
                while (reader.Read())
                {
                    var productId = int.Parse(reader["ProductId"].ToString());
                    var quantity = int.Parse(reader["Quantity"].ToString());
                    var pricePerUnit = decimal.Parse(reader["Price Per Unit"].ToString());
                    var month = int.Parse(reader["Month"].ToString());
                    var year = int.Parse(reader["Year"].ToString());

                    this.Deliveries.Add(new DeliveryInfo()
                    {
                        ProductId = productId,
                        Quantity = quantity,
                        PricePerUnit = pricePerUnit,
                        Month = month,
                        Year = year
                    });
                }
            }
        }
    }
}
