using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AssemblyCSharp.Mod2.Xmap
{
    class GroupMapData
    {
    public List<int> datamap;
    public string mapname;
    public GroupMapData(List<int> var1, string var2)
    {
        this.datamap = var1;
        this.mapname = var2;
    }
    }
}
