﻿using System;
using System.Collections.Generic;

namespace AssemblyCSharp.Mod2.Xmap
{
	// Token: 0x020000CA RID: 202
	public struct GroupMap
	{
		// Token: 0x06000A41 RID: 2625 RVA: 0x000065F9 File Offset: 0x000047F9
		public GroupMap(string nameGroup, List<int> idMaps)
		{
			this.NameGroup = nameGroup;
			this.IdMaps = idMaps;
		}

		// Token: 0x0400133B RID: 4923
		public string NameGroup;

		// Token: 0x0400133C RID: 4924
		public List<int> IdMaps;
	}
}
