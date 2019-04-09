using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4
{
    [Serializable]
    public class Rifle:Firearm
    {
        public int fire_rate { get; set; }
        public Rifle(string name, int clip_size, int fire_rate) : base(name, clip_size)
        {
            type = "Rifle ";
            this.fire_rate = fire_rate;
        }
    }
}
