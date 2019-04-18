using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4
{
    public abstract class FireArm:Gun
    {
        public GunClip Clip;
        public FireArm(string name, GunType gunType, int price, int damage, int clipSize, int fireRate) : base(name, gunType, price, damage)
        {
            Info = new FireArmInfo(name, price, damage, fireRate, clipSize);
            Clip = new GunClip(clipSize);
        }
        public FireArm()
        {
            Info = new FireArmInfo();
        }
        public virtual void Shoot()
        {
            if(Clip.Ammo > 0)
            {
                (Info as FireArmInfo).Ammo--;
                Clip.Ammo--;
            }
        }
        public virtual void Reload()
        {
            (Info as FireArmInfo).Ammo = (Info as FireArmInfo).ClipSize;
            Clip.Ammo = (Info as FireArmInfo).ClipSize;
        }
        public class GunClip
        {
            public int Size { get; set; }
            public int Ammo { get; set; }
            public GunClip(int size)
            {
                Size = size;
                Ammo = size;
            }
        }
    }
}
