using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4
{
    [Serializable]
    public class Pistol : Firearm
    {
        public Pistol(string name="", int clip_size=0):base(name,clip_size)
        {
            ammo = clip_size;
            type = "Pistol";
        }
    }
}
