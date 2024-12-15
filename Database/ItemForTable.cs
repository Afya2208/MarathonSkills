using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarathonSkills.Database
{
    internal class ItemForTable
    {
        public string Name { get; set; }
        public int NumA {  get; set; }
        public int NumB { get; set; }

        public int NumC { get; set; }
        public int NumSum {
            get
            {
                return NumA + NumB + NumC;
            }
                }
        public int Remains { get; set; }
    }
}
