using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4
{
    public class Pistol:FireArm
    {
        public Pistol(string name, int price, int damage, int clipSize, int fireRate)
            : base(name, GunType.Pistol, price, damage, clipSize, fireRate) { }
    }
}
