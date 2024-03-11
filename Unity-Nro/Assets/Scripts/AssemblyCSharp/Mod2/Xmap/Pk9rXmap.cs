using System;

namespace AssemblyCSharp.Mod2.Xmap
{
	// Token: 0x020000CC RID: 204
	public class Pk9rXmap
	{
		// Token: 0x06000A43 RID: 2627 RVA: 0x000A455C File Offset: 0x000A275C
		public static bool Chat(string text)
		{
			bool flag = text == "xmp";
			if (flag)
			{
				bool isXmapRunning = Pk9rXmap.IsXmapRunning;
				if (isXmapRunning)
				{
					XmapController.FinishXmap();
					GameScr.info1.addInfo("Đã hủy Xmap", 0);
				}
				else
				{
					XmapController.ShowXmapMenu();
				}
			}
			else
			{
				bool flag2 = Pk9rXmap.IsGetInfoChat<int>(text, "xmp");
				if (flag2)
				{
					bool isXmapRunning2 = Pk9rXmap.IsXmapRunning;
					if (isXmapRunning2)
					{
						XmapController.FinishXmap();
						GameScr.info1.addInfo("Đã hủy Xmap", 0);
					}
					else
					{
						XmapController.StartRunToMapId(Pk9rXmap.GetInfoChat<int>(text, "xmp"));
					}
				}
				else
				{
					bool flag3 = text == "csb";
					if (flag3)
					{
						Pk9rXmap.IsUseCapsuleNormal = !Pk9rXmap.IsUseCapsuleNormal;
						GameScr.info1.addInfo("Sử dụng capsule thường Xmap: " + (Pk9rXmap.IsUseCapsuleNormal ? "Bật" : "Tắt"), 0);
					}
					else
					{
						bool flag4 = !(text == "csdb");
						if (flag4)
						{
							return false;
						}
						Pk9rXmap.IsUseCapsuleVip = !Pk9rXmap.IsUseCapsuleVip;
						GameScr.info1.addInfo("Sử dụng capsule đặc biệt Xmap: " + (Pk9rXmap.IsUseCapsuleVip ? "Bật" : "Tắt"), 0);
					}
				}
			}
			return true;
		}

		// Token: 0x06000A44 RID: 2628 RVA: 0x000A46A0 File Offset: 0x000A28A0
		public static bool HotKeys()
		{
			int keyAsciiPress = GameCanvas.keyAsciiPress;
			bool flag = keyAsciiPress != 99;
			if (flag)
			{
				bool flag2 = keyAsciiPress != 120;
				if (flag2)
				{
					return false;
				}
				Pk9rXmap.Chat("xmp");
			}
			else
			{
				Pk9rXmap.Chat("csb");
			}
			return true;
		}

		// Token: 0x06000A45 RID: 2629 RVA: 0x000A46F4 File Offset: 0x000A28F4
		public static void Update()
		{
			bool isLoading = XmapData.Instance().IsLoading;
			if (isLoading)
			{
				XmapData.Instance().Update();
			}
			bool isXmapRunning = Pk9rXmap.IsXmapRunning;
			if (isXmapRunning)
			{
				XmapController.Update();
			}
		}

		// Token: 0x06000A46 RID: 2630 RVA: 0x000A4730 File Offset: 0x000A2930
		public static void Info(string text)
		{
			bool flag = text.Equals("Bạn chưa thể đến khu vực này");
			if (flag)
			{
				XmapController.FinishXmap();
			}
			bool flag2 = (text.ToLower().Contains("chức năng bảo vệ") || text.ToLower().Contains("đã hủy xmap")) && Pk9rXmap.IsXmapRunning;
			if (flag2)
			{
				XmapController.FinishXmap();
			}
		}

		// Token: 0x06000A47 RID: 2631 RVA: 0x000A4790 File Offset: 0x000A2990
		public static bool XoaTauBay(object obj)
		{
			Teleport teleport = (Teleport)obj;
			bool isMe = teleport.isMe;
			bool result;
			if (isMe)
			{
				global::Char.myCharz().isTeleport = false;
				bool flag = teleport.type == 0;
				if (flag)
				{
					Controller.isStopReadMessage = false;
					global::Char.ischangingMap = true;
				}
				Teleport.vTeleport.removeElement(teleport);
				result = true;
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06000A48 RID: 2632 RVA: 0x000A47EC File Offset: 0x000A29EC
		public static void SelectMapTrans(int selected)
		{
			bool isMapTransAsXmap = Pk9rXmap.IsMapTransAsXmap;
			if (isMapTransAsXmap)
			{
				XmapController.HideInfoDlg();
				XmapController.StartRunToMapId(XmapData.GetIdMapFromPanelXmap(GameCanvas.panel.mapNames[selected]));
			}
			else
			{
				XmapController.SaveIdMapCapsuleReturn();
				Service.gI().requestMapSelect(selected);
			}
		}

		// Token: 0x06000A49 RID: 2633 RVA: 0x000A4838 File Offset: 0x000A2A38
		public static void ShowPanelMapTrans()
		{
			Pk9rXmap.IsMapTransAsXmap = false;
			bool isShowPanelMapTrans = Pk9rXmap.IsShowPanelMapTrans;
			if (isShowPanelMapTrans)
			{
				GameCanvas.panel.setTypeMapTrans();
				GameCanvas.panel.show();
			}
			else
			{
				Pk9rXmap.IsShowPanelMapTrans = true;
			}
		}

		// Token: 0x06000A4A RID: 2634 RVA: 0x00006622 File Offset: 0x00004822
		public static void FixBlackScreen()
		{
			Controller.gI().loadCurrMap(0);
			Service.gI().finishLoadMap();
			global::Char.isLoadingMap = false;
		}

		// Token: 0x06000A4B RID: 2635 RVA: 0x000A4874 File Offset: 0x000A2A74
		private static bool IsGetInfoChat<T>(string text, string s)
		{
			bool flag = text.StartsWith(s);
			bool result;
			if (flag)
			{
				try
				{
					Convert.ChangeType(text.Substring(s.Length), typeof(T));
				}
				catch
				{
					return false;
				}
				result = true;
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06000A4C RID: 2636 RVA: 0x000A4370 File Offset: 0x000A2570
		private static T GetInfoChat<T>(string text, string s)
		{
			return (T)((object)Convert.ChangeType(text.Substring(s.Length), typeof(T)));
		}

		// Token: 0x04001340 RID: 4928
		public static bool IsXmapRunning = false;

		// Token: 0x04001341 RID: 4929
		public static bool IsMapTransAsXmap = false;

		// Token: 0x04001342 RID: 4930
		public static bool IsShowPanelMapTrans = true;

		// Token: 0x04001343 RID: 4931
		public static bool IsUseCapsuleNormal = true;

		// Token: 0x04001344 RID: 4932
		public static bool IsUseCapsuleVip = true;

		// Token: 0x04001345 RID: 4933
		public static int IdMapCapsuleReturn = -1;
	}
}
