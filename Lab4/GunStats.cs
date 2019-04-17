using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4
{
    public class GunStats
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public int Damage { get; set; }
        public GunStats(string name, int price, int damage)
        {
            Name = name;
            Price = price;
            Damage = damage;
        }

    }
}
