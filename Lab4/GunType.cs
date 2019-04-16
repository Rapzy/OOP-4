using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4
{
    public class GunType : IEquatable<GunType>
    {
        public static List<GunType> TypeList = new List<GunType>();
        public static GunType Knife { get; } = new GunType("Knife");
        public static GunType Pistol { get; } = new GunType("Pistol");
        public static GunType Rifle { get; } = new GunType("Rifle");
        public string Type { get; }
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
        public static bool operator ==(GunType gunType1, GunType other)
        {
            return gunType1.Type == other.Type;
        }
        public static bool operator !=(GunType gunType1, GunType other)
        {
            return gunType1.Type != other.Type;
        }

    }
}
