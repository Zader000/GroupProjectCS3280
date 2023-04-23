using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject.Items
{

    /// <summary>
    /// get the three columns that give information about specific items.
    /// </summary>
    public class Item
    {
        public string Code { get; set; }
        public double Cost { get; set; }
        public string Description { get; set; }
        
        public Item(string code, string description, string cost)
        {
            Code = code;
            Description = description;
            Cost = Convert.ToDouble(cost);
        }
    }
}
