using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4
{
    [Serializable]
    public class Steelarm:Gun
    {
        public Steelarm(string name)
        {

            this.name = name;
            type = "Steel arm ";
        }
        public override void Shoot()
        {
            Console.WriteLine("Damaged by {0}", name);
            Console.WriteLine("=======================");
        }
        public override string GetFullType()
        {
            return name + " is " + type + base.GetFullType();
        }
    }
}
