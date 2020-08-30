using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPTC
{
    [AttributeUsage(AttributeTargets.Struct)]
    public class PIDAttribute : Attribute
    {
        public ushort ID { get; private set; }
        public PIDAttribute(ushort id)
        {
            this.ID = id;
        }
    }
}
