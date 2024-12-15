using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarathonSkills.Database
{
    public partial class ArrivalItem
    {
        public string ItemName
        {
            get
            {
                Item item = DB.entities.Item.FirstOrDefault(i => i.ItemId == ItemId);
                return item.ItemName;
            }
        }
    }
}
