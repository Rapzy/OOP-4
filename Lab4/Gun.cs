using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4
{
    [Serializable]
    public abstract class Gun
    {
        public string type { get; protected set; }
        public string name { get; set; }
        public abstract void Shoot();
        public Gun() { }
        public virtual string GetFullType()
        {
            return "Gun";
        }
    }
}
