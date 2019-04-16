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
        public string Name { get; set; }
        public int Price { get; set; }
        public int Damage { get; set; }
        //public abstract void Shoot();
        public Gun(string name ,GunType gunType, int price, int damage)
        {
            Name = name;
            Type = gunType;
            Price = price;
            Damage = damage;
            GunList.Add(this);
        }
    }
}
