using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4
{
    public class Rifle:Gun
    {
        public static List<Rifle> RifleList = new List<Rifle>();
        public static Rifle AK47 = new Rifle("AK-47", 2700, 100);
        public Rifle(string name, int price, int damage):base(name, GunType.Rifle, price, damage)
        {
            RifleList.Add(this);
        }
    }
}