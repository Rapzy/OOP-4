using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4
{
    public class Rifle:FireArm
    {
        public Rifle(string name, int price, int damage, int clipSize, int fireRate)
            :base(name, GunType.Rifle, price, damage, clipSize, fireRate){}
    }
}