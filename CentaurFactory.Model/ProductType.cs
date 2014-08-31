using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentaurFactory.Model
{
    public class ProductType
    {
        public virtual string Name { get; set; }

        public virtual string ToString()
        {
            return "Product type name: " + this.Name;
        }
    }
}
