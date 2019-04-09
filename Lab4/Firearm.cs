using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4
{
    [Serializable]
    public abstract class Firearm : Gun
    {
        public int clip_size { get; set; }
        public int ammo { get; protected set; }
        public Firearm(string name="", int clip_size=0)
        {
            this.name = name;
            this.clip_size = clip_size;
            ammo = clip_size;
            type = "Firearm";
        }
        public override void Shoot()
        {
            if (ammo > 0)
            {
                ammo -= 1;
            }
        }
        public void Reload()
        {
            ammo = clip_size;
        }
        public override string GetFullType()
        {
            return name + " is " + type + base.GetFullType();
        }
    }
}
