using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace Nade
{
    [Serializable]
    public class Nade
    {
        public string type { get; set; }
        public string name { get; set; }
        public int fuse_time { get; set; }
        private int ammo = 1;
        private System.Timers.Timer timer;
        public Nade(string type, string name, int fuse_time) {
            this.type = type;
            this.name = name;
            this.fuse_time = fuse_time;
            timer = new System.Timers.Timer();
            timer.Interval = fuse_time*1000;
            timer.Elapsed += OnTimedEvent;
            timer.AutoReset = false;
            timer.Enabled = false;
        }
        public void Shoot()
        {
            MessageBox.Show(
            "Time left: "+fuse_time+"s",
            "Nade",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information,
            MessageBoxDefaultButton.Button1,
            MessageBoxOptions.DefaultDesktopOnly);
            timer.Start();
            if (ammo > 0)
            {
                ammo -= 1;
            }
        }

        private static void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            MessageBox.Show(
            "Nade",
            "BOOM",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information,
            MessageBoxDefaultButton.Button1,
            MessageBoxOptions.DefaultDesktopOnly);
        }
        public void Reload()
        {
            ammo = 1;
        }
    }

}
