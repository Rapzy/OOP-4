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
        private GunType type;
        public GunType Type {
            get {
                return type;
            }
            set
            {
                if (GunType.TypeList.Contains(value))
                    type = value;
                else
                    type = new GunType(value.ToString());
            }
        }
        public static List<Gun> GunList = new List<Gun>();
        public GunInfo Info { get; set; }
        public Gun(string name, GunType gunType, int price, int damage)
        {
            Info = new GunInfo(name, price, damage);
            Type = gunType;
            GunList.Add(this);
        }
        public Gun()
        {
            Info = new GunInfo();
        }
    }
}
