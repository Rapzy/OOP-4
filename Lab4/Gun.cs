using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4
{
    [Serializable]
    public abstract class Gun
    {
        public GunType Type {
            get {
                return Type;
            }
            set
            {
                if (GunType.TypeList.Contains(value))
                    Type = value;
                else
                    Type = new GunType(value.ToString());
            }
        }
        public static List<Gun> GunList = new List<Gun>();
        public Stats Stat { get; set; }
        public Gun(string name ,GunType gunType, int price, int damage)
        {
            Stat = new Stats(name, price, damage);
            Type = gunType;
            GunList.Add(this);
        }
        public class Stats
        {
            public string Name { get; set; }
            public int Price { get; set; }
            public int Damage { get; set; }
            public Stats(string name, int price, int damage)
            {
                Name = name;
                Price = price;
                Damage = damage;
            }
        }
    }
}
