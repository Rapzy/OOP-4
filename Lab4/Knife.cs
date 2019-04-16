using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4
{
    public class Knife:Gun
    {
        public static List<Knife> KnifeList = new List<Knife>();
        public static Knife DefaultKnife = new Knife("Knife", 0, 55);
        public Knife(string name, int price, int damage) : base(name, GunType.Rifle, price, damage)
        {
            KnifeList.Add(this);
        }
    }
}
