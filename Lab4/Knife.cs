using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4
{
    public class Knife:Gun
    {
        public Knife(string name, int price, int damage) : base(name, GunType.Knife, price, damage){}
        public Knife() { }
    }
}
