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
            Stat = new Stats(name, price, damage, fireRate, clipSize);
            Clip = new GunClip(clipSize);
        }
        public new class Stats: Gun.Stats
        {
            public int ClipSize { get; set; }
            public int FireRate { get; set; }
            public Stats(string name, int price, int damage, int fireRate, int clipSize):
                base(name, price, damage)
            {
                FireRate = fireRate;
                ClipSize = clipSize;
            }
        }
        public virtual void Shoot()
        {
            if(Clip.Ammo > 0)
            {
                Clip.Ammo--;
            }
        }
        public virtual void Reload()
        {
            Clip.Ammo = Clip.Size;
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
