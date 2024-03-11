using System.Collections.Generic;
using System.Data;

namespace DirtyDandD.Classes
{
    public class Feature
    {
        public string name { init; get; }
        public int level { init; get; }
        public List<string> info { init; get; }
        public List<DataTable> tables { init; get; }

        public Feature()
        {
        }
    }
}
