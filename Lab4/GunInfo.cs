﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4
{
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
}
