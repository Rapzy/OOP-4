using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nade
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
        public int slot;
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
    [Serializable]
    public class GunInfo
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public int Damage { get; set; }
        public GunInfo(string name, int price, int damage)
        {
            Name = name;
            Price = price;
            Damage = damage;
        }
        public GunInfo() { }

    }
    [Serializable]
    public class FireArmInfo : GunInfo
    {
        public int Ammo { get; set; }
        public int ClipSize { get; set; }
        public int FireRate { get; set; }
        public FireArmInfo(string name, int price, int damage, int fireRate, int clipSize) :
            base(name, price, damage)
        {
            FireRate = fireRate;
            ClipSize = clipSize;
            Ammo = clipSize;
        }
        public FireArmInfo() { }
    }
    [Serializable]
    public class GunType : IEquatable<GunType>
    {
        public static List<GunType> TypeList = new List<GunType>();
        public static GunType Nade { get; } = new GunType("Knife");
        public string Type { get; set; }
        public GunType(string gunType)
        {
            Type = gunType;
            TypeList.Add(this);
        }
        public bool Equals(GunType other)
        {
            if (other == null)
                return false;
            if (Type == other.Type)
                return true;
            else
                return false;
        }
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            GunType gunTypeObj = obj as GunType;
            if (gunTypeObj == null)
                return false;
            else
                return Equals(gunTypeObj);
        }
        public override int GetHashCode()
        {
            return Type.GetHashCode();
        }
        public static bool operator ==(GunType gunType1, GunType other)
        {
            if (((object)gunType1) == null || ((object)other) == null)
                return Object.Equals(gunType1, other);
            return gunType1.Equals(other);
        }
        public static bool operator !=(GunType gunType1, GunType other)
        {
            if (gunType1 == null || other == null)
                return !Object.Equals(gunType1, other);
            return !gunType1.Equals(other);
        }

    }
}
