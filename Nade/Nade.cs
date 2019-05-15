using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using Lab4;

namespace Nade
{
    [Serializable]
    public class Nade:Gun
    {

        public Nade(string name, int price, int damage) : base(name, "Nade", price, damage) { slot = 4; }
        public Nade() { }
    }
}
