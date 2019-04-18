using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4
{
    [Serializable]
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
                return ! Object.Equals(gunType1, other);
            return ! gunType1.Equals(other);
        }

    }
}
