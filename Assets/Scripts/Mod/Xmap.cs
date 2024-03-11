using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mod
{
	using System;
	using System.Collections.Generic;
	using System.Text;

	// Token: 0x020000C9 RID: 201
	public class Xmap : IActionListener
	{
		// Token: 0x06000A4C RID: 2636 RVA: 0x00008591 File Offset: 0x00006791
		public static Xmap getInstance()
		{
			if (Xmap._Instance == null)
			{
				Xmap._Instance = new Xmap();
			}
			return Xmap._Instance;
		}

		// Token: 0x06000A4D RID: 2637 RVA: 0x0009AF7C File Offset: 0x0009917C
		public static void Update()
		{
			if (global::Char.myCharz().meDead)
			{
				Xmap.lastWaitTime = mSystem.currentTimeMillis() + 1000L;
			}
			if (TileMap.mapID == Xmap.IdMapEnd)
			{
				Xmap.FinishXmap();
				return;
			}
			bool flag = false;
			if (TileMap.mapID == 21 || TileMap.mapID == 22 || TileMap.mapID == 23)
			{
				if (Xmap.isEatChicken)
				{
					for (int i = 0; i < GameScr.vItemMap.size(); i++)
					{
						ItemMap itemMap = (ItemMap)GameScr.vItemMap.elementAt(i);
						if ((itemMap.playerId == global::Char.myCharz().charID || itemMap.playerId == -1) && itemMap.template.id == 74)
						{
							flag = true;
							global::Char.myCharz().itemFocus = itemMap;
							if (mSystem.currentTimeMillis() - Xmap.lastWaitTime > 600L)
							{
								Xmap.lastWaitTime = mSystem.currentTimeMillis();
								Service.gI().pickItem(global::Char.myCharz().itemFocus.itemMapID);
								return;
							}
						}
					}
				}
				if (Xmap.isXmaping && Xmap.isHarvestPean && GameScr.hpPotion < 10 && GameScr.gI().magicTree.currPeas > 0 && mSystem.currentTimeMillis() - Xmap.lastWaitTime > 500L)
				{
					Xmap.lastWaitTime = mSystem.currentTimeMillis();
					Service.gI().openMenu(4);
					Service.gI().menu(4, 0, 0);
				}
			}
			if (Xmap.isXmaping && !flag && mSystem.currentTimeMillis() - Xmap.lastWaitTime > 1000L && GameCanvas.gameTick % 20 == 0)
			{
				bool flag2 = true;
				if (Xmap.isFutureMap(Xmap.IdMapEnd))
				{
					if (flag2 && TileMap.mapID == 27 && GameScr.findNPCInMap(38) == null)
					{
						flag2 = false;
						Xmap.UpdateXmap(28);
					}
					if (flag2 && TileMap.mapID == 29 && GameScr.findNPCInMap(38) == null)
					{
						flag2 = false;
						Xmap.UpdateXmap(28);
					}
					if (flag2 && TileMap.mapID == 28 && GameScr.findNPCInMap(38) == null)
					{
						flag2 = false;
						if (global::Char.myCharz().cx < TileMap.pxw / 2)
						{
							Xmap.UpdateXmap(29);
						}
						else
						{
							Xmap.UpdateXmap(27);
						}
					}
				}
				if (flag2)
				{
					Xmap.UpdateXmap(Xmap.IdMapEnd);
				}
			}
		}

		// Token: 0x06000A4E RID: 2638 RVA: 0x0009B1A0 File Offset: 0x000993A0
		public void perform(int idAction, object p)
		{
			switch (idAction)
			{
				case 999:
					Mod.jkl = !Mod.jkl;
					GameScr.info1.addInfo("|0|J "+"|1|K"+"|2|L\n"+(Mod.jkl ? "[ON]":"[OFF]"),0);
					break;
				case 1:
					Xmap.ShowPlanetMenu();
					ChatPopup.addChatPopup("|3|Bạn Muốn Đến Hành Tinh Nào?", 100000, new Npc(-1, 0, 0, 0, 0, 0)
					{
						avatar = 8025
					});
					ChatPopup.serverChatPopUp.cmdMsg1 = new Command("", ChatPopup.serverChatPopUp, 1000, null);
					ChatPopup.serverChatPopUp.cmdMsg1.x = GameCanvas.w / 2 - 75;
					ChatPopup.serverChatPopUp.cmdMsg1.y = GameCanvas.h - 35;
					ChatPopup.serverChatPopUp.cmdMsg2 = new Command("", ChatPopup.serverChatPopUp, 1001, null);
					ChatPopup.serverChatPopUp.cmdMsg2.x = GameCanvas.w / 2 + 11;
					ChatPopup.serverChatPopUp.cmdMsg2.y = GameCanvas.h - 35;
					return;
				case 2:
					Xmap.isEatChicken = !Xmap.isEatChicken;
					GameScr.info1.addInfo("Ăn Đùi Gà\n" + (Xmap.isEatChicken ? "[STATUS: ON]" : "[STATUS: OFF]"), 0);
					if (Xmap.isSaveData)
					{
						Rms.saveRMSInt("AutoMapIsEatChicken", Xmap.isEatChicken ? 1 : 0);
						return;
					}
					break;
				case 3:
					Xmap.isHarvestPean = !Xmap.isHarvestPean;
					GameScr.info1.addInfo("Thu Đậu\n" + (Xmap.isHarvestPean ? "[STATUS: ON]" : "[STATUS: OFF]"), 0);
					if (Xmap.isSaveData)
					{
						Rms.saveRMSInt("AutoMapIsHarvestPean", Xmap.isHarvestPean ? 1 : 0);
						return;
					}
					break;
				case 4:
					Xmap.isUseCapsule = !Xmap.isUseCapsule;
					GameScr.info1.addInfo("Sử Dụng Capsule\n" + (Xmap.isUseCapsule ? "[STATUS: ON]" : "[STATUS: OFF]"), 0);
					if (Xmap.isSaveData)
					{
						Rms.saveRMSInt("AutoMapIsUseCsb", Xmap.isUseCapsule ? 1 : 0);
						return;
					}
					break;
				case 5:
					Xmap.isSaveData = !Xmap.isSaveData;
					GameScr.info1.addInfo("Lưu Cài Đặt Auto Map\n" + (Xmap.isSaveData ? "[STATUS: ON]" : "[STATUS: OFF]"), 0);
					Rms.saveRMSInt("AutoMapIsSaveRms", Xmap.isSaveData ? 1 : 0);
					if (Xmap.isSaveData)
					{
						Xmap.SaveData();
						return;
					}
					break;
				case 6:
					Xmap.ShowMapsMenu((int[])p);
					ChatPopup.addChatPopup("|4|Bạn Muốn Đến Map Nào?", 100000, new Npc(-1, 0, 0, 0, 0, 0)
					{
						avatar = 8025
					});
					ChatPopup.serverChatPopUp.cmdMsg1 = new Command("", ChatPopup.serverChatPopUp, 1000, null);
					ChatPopup.serverChatPopUp.cmdMsg1.x = GameCanvas.w / 2 - 75;
					ChatPopup.serverChatPopUp.cmdMsg1.y = GameCanvas.h - 35;
					ChatPopup.serverChatPopUp.cmdMsg2 = new Command("", ChatPopup.serverChatPopUp, 1001, null);
					ChatPopup.serverChatPopUp.cmdMsg2.x = GameCanvas.w / 2 + 11;
					ChatPopup.serverChatPopUp.cmdMsg2.y = GameCanvas.h - 35;
					return;
				case 7:
					Xmap.isXmaping = true;
					Xmap.IdMapEnd = (int)p;
					GameScr.info1.addInfo("Go to " + TileMap.mapNames[Xmap.IdMapEnd], 0);
					return;
				default:
					return;
			}
		}

		// Token: 0x06000A4F RID: 2639 RVA: 0x0009B4F8 File Offset: 0x000996F8
		public static void ShowMenu()
		{
			Xmap.LoadData();
			MyVector myVector = new MyVector();
			myVector.addElement(new Command("Load Map", Xmap.getInstance(), 1, null));
			myVector.addElement(new Command("J K L", Xmap.getInstance(), 999, null));
			myVector.addElement(new Command("Ăn Đùi Gà\n" + (Xmap.isEatChicken ? "[STATUS: ON]" : "[STATUS: OFF]"), Xmap.getInstance(), 2, null));
			myVector.addElement(new Command("Thu Đậu\n" + (Xmap.isHarvestPean ? "[STATUS: ON]" : "[STATUS: OFF]"), Xmap.getInstance(), 3, null));
			myVector.addElement(new Command("Sử Dụng Capsule\n" + (Xmap.isUseCapsule ? "[STATUS: ON]" : "[STATUS: OFF]"), Xmap.getInstance(), 4, null));
			myVector.addElement(new Command("Lưu Cài Đặt\n" + (Xmap.isSaveData ? "[STATUS: ON]" : "[STATUS: OFF]"), Xmap.getInstance(), 5, null));
			GameCanvas.menu.startAt(myVector, 3);
		}

		// Token: 0x06000A50 RID: 2640 RVA: 0x0009B5F0 File Offset: 0x000997F0
		private static void ShowPlanetMenu()
		{
			MyVector myVector = new MyVector();
			foreach (KeyValuePair<string, int[]> keyValuePair in Xmap.planetDictionary)
			{
				myVector.addElement(new Command(keyValuePair.Key, Xmap.getInstance(), 6, keyValuePair.Value));
			}
			GameCanvas.menu.startAt(myVector, 3);
		}

		// Token: 0x06000A51 RID: 2641 RVA: 0x0009B66C File Offset: 0x0009986C
		private static void ShowMapsMenu(int[] mapIDs)
		{
			MyVector myVector = new MyVector();
			for (int i = 0; i < mapIDs.Length; i++)
			{
				if ((global::Char.myCharz().cgender != 0 || (mapIDs[i] != 22 && mapIDs[i] != 23)) && (global::Char.myCharz().cgender != 1 || (mapIDs[i] != 21 && mapIDs[i] != 23)) && (global::Char.myCharz().cgender != 2 || (mapIDs[i] != 21 && mapIDs[i] != 22)))
				{
					myVector.addElement(new Command(Xmap.GetMapName(mapIDs[i]), Xmap.getInstance(), 7, mapIDs[i]));
				}
			}
			GameCanvas.menu.startAt(myVector, 3);
		}

		// Token: 0x06000A52 RID: 2642 RVA: 0x000085A9 File Offset: 0x000067A9
		public static void StartRunToMapId(int mapID)
		{
			Xmap.isXmaping = true;
			Xmap.IdMapEnd = mapID;
		}

		// Token: 0x06000A53 RID: 2643 RVA: 0x000085B7 File Offset: 0x000067B7
		public static void FinishXmap()
		{
			Xmap.isXmaping = false;
			Xmap.isUsingCapsule = false;
			Xmap.isOpeningPanel = false;
		}

		// Token: 0x06000A54 RID: 2644 RVA: 0x0009B70C File Offset: 0x0009990C
		public static void UpdateXmap(int mapID)
		{
			if (Xmap.linkMaps.ContainsKey(84))
			{
				Xmap.linkMaps.Remove(84);
			}
			Xmap.linkMaps.Add(84, new List<Xmap.NextMap>());
			Xmap.linkMaps[84].Add(new Xmap.NextMap(24 + global::Char.myCharz().cgender, 10, 0));
			int[] array = Xmap.FindWay(mapID);
			if (array == null)
			{
				GameScr.info1.addInfo("Không thể tìm thấy đường đi", 0);
				return;
			}
			if (Xmap.isUseCapsule)
			{
				if (!Xmap.isUsingCapsule && array.Length > 3)
				{
					for (int i = 0; i < global::Char.myCharz().arrItemBag.Length; i++)
					{
						Item item = global::Char.myCharz().arrItemBag[i];
						if (item != null && (item.template.id == 194 || (item.template.id == 193 && item.quantity > 10)))
						{
							Xmap.isUsingCapsule = true;
							Xmap.isOpeningPanel = false;
							Xmap.lastTimeOpenedPanel = mSystem.currentTimeMillis();
							GameCanvas.panel.mapNames = null;
							Xmap.UseCapsuleVip();
							Xmap.isUsingCapsule = true;
							return;
						}
					}
				}
				if (Xmap.isUsingCapsule && !Xmap.isOpeningPanel && (GameCanvas.panel.mapNames == null || mSystem.currentTimeMillis() - Xmap.lastTimeOpenedPanel < 500L))
				{
					return;
				}
				if (Xmap.isUsingCapsule && !Xmap.isOpeningPanel)
				{
					for (int j = array.Length - 1; j >= 2; j--)
					{
						for (int k = 0; k < GameCanvas.panel.mapNames.Length; k++)
						{
							if (GameCanvas.panel.mapNames[k].Contains(TileMap.mapNames[array[j]]))
							{
								Xmap.isOpeningPanel = true;
								Service.gI().requestMapSelect(k);
								return;
							}
						}
					}
					Xmap.isOpeningPanel = true;
				}
			}
			if (TileMap.mapID == array[0] && !global::Char.ischangingMap && !Controller.isStopReadMessage)
			{
				Xmap.Goto(array[1]);
			}
		}

		// Token: 0x06000A55 RID: 2645 RVA: 0x000085CB File Offset: 0x000067CB
		public static void LoadMapLeft()
		{
			Xmap.LoadMap(0);
		}

		// Token: 0x06000A56 RID: 2646 RVA: 0x000085D3 File Offset: 0x000067D3
		public static void LoadMapCenter()
		{
			Xmap.LoadMap(2);
		}

		// Token: 0x06000A57 RID: 2647 RVA: 0x000085DB File Offset: 0x000067DB
		public static void LoadMapRight()
		{
			Xmap.LoadMap(1);
		}

		// Token: 0x06000A58 RID: 2648 RVA: 0x0009B8E4 File Offset: 0x00099AE4
		private static void LoadData()
		{
			Xmap.isSaveData = (Rms.loadRMSInt("AutoMapIsSaveRms") == 1);
			if (Xmap.isSaveData)
			{
				if (Rms.loadRMSInt("AutoMapIsEatChicken") == -1)
				{
					Xmap.isEatChicken = true;
				}
				else
				{
					Xmap.isEatChicken = (Rms.loadRMSInt("AutoMapIsEatChicken") == 1);
				}
				if (Rms.loadRMSInt("AutoMapIsUseCsb") == -1)
				{
					Xmap.isUseCapsule = true;
				}
				else
				{
					Xmap.isUseCapsule = (Rms.loadRMSInt("AutoMapIsUseCsb") == 1);
				}
				Xmap.isHarvestPean = (Rms.loadRMSInt("AutoMapIsHarvestPean") == 1);
			}
		}

		// Token: 0x06000A59 RID: 2649 RVA: 0x0009B96C File Offset: 0x00099B6C
		private static void SaveData()
		{
			Rms.saveRMSInt("AutoMapIsEatChicken", Xmap.isEatChicken ? 1 : 0);
			Rms.saveRMSInt("AutoMapIsHarvestPean", Xmap.isHarvestPean ? 1 : 0);
			Rms.saveRMSInt("AutoMapIsUseCsb", Xmap.isUseCapsule ? 1 : 0);
		}

		// Token: 0x06000A5A RID: 2650 RVA: 0x0009B9B8 File Offset: 0x00099BB8
		private static void LoadLinkMapsXmap()
		{
			Xmap.AddLinkMapsXmap(new int[]
			{
			0,
			21
			});
			Xmap.AddLinkMapsXmap(new int[]
			{
			1,
			47
			});
			Xmap.AddLinkMapsXmap(new int[]
			{
			47,
			111
			});
			Xmap.AddLinkMapsXmap(new int[]
			{
			2,
			24
			});
			Xmap.AddLinkMapsXmap(new int[]
			{
			5,
			29
			});
			Xmap.AddLinkMapsXmap(new int[]
			{
			7,
			22
			});
			Xmap.AddLinkMapsXmap(new int[]
			{
			9,
			25
			});
			Xmap.AddLinkMapsXmap(new int[]
			{
			13,
			33
			});
			Xmap.AddLinkMapsXmap(new int[]
			{
			14,
			23
			});
			Xmap.AddLinkMapsXmap(new int[]
			{
			16,
			26
			});
			Xmap.AddLinkMapsXmap(new int[]
			{
			20,
			37
			});
			Xmap.AddLinkMapsXmap(new int[]
			{
			39,
			21
			});
			Xmap.AddLinkMapsXmap(new int[]
			{
			40,
			22
			});
			Xmap.AddLinkMapsXmap(new int[]
			{
			41,
			23
			});
			Xmap.AddLinkMapsXmap(new int[]
			{
			109,
			105
			});
			Xmap.AddLinkMapsXmap(new int[]
			{
			109,
			106
			});
			Xmap.AddLinkMapsXmap(new int[]
			{
			106,
			107
			});
			Xmap.AddLinkMapsXmap(new int[]
			{
			108,
			105
			});
			Xmap.AddLinkMapsXmap(new int[]
			{
			80,
			105
			});
			Xmap.AddLinkMapsXmap(new int[]
			{
			3,
			27,
			28,
			29,
			30
			});
			Xmap.AddLinkMapsXmap(new int[]
			{
			11,
			31,
			32,
			33,
			34
			});
			Xmap.AddLinkMapsXmap(new int[]
			{
			17,
			35,
			36,
			37,
			38
			});
			Xmap.AddLinkMapsXmap(new int[]
			{
			109,
			108,
			107,
			110,
			106
			});
			Xmap.AddLinkMapsXmap(new int[]
			{
			47,
			46,
			45,
			48
			});
			Xmap.AddLinkMapsXmap(new int[]
			{
			131,
			132,
			133
			});
			Xmap.AddLinkMapsXmap(new int[]
			{
			42,
			0,
			1,
			2,
			3,
			4,
			5,
			6
			});
			Xmap.AddLinkMapsXmap(new int[]
			{
			43,
			7,
			8,
			9,
			11,
			12,
			13,
			10
			});
			Xmap.AddLinkMapsXmap(new int[]
			{
			52,
			44,
			14,
			15,
			16,
			17,
			18,
			20,
			19
			});
			Xmap.AddLinkMapsXmap(new int[]
			{
			53,
			58,
			59,
			60,
			61,
			62,
			55,
			56,
			54,
			57
			});
			Xmap.AddLinkMapsXmap(new int[]
			{
			68,
			69,
			70,
			71,
			72,
			64,
			65,
			63,
			66,
			67,
			73,
			74,
			75,
			76,
			77,
			81,
			82,
			83,
			79,
			80
			});
			Xmap.AddLinkMapsXmap(new int[]
			{
			102,
			92,
			93,
			94,
			96,
			97,
			98,
			99,
			100,
			103
			});
		}

		// Token: 0x06000A5B RID: 2651 RVA: 0x0009BC58 File Offset: 0x00099E58
		private static void LoadNPCLinkMapsXmap()
		{
			Xmap.AddNPCLinkMapsXmap(19, 68, 12, 1);
			Xmap.AddNPCLinkMapsXmap(19, 109, 12, 0);
			Xmap.AddNPCLinkMapsXmap(24, 25, 10, 0);
			Xmap.AddNPCLinkMapsXmap(24, 26, 10, 1);
			Xmap.AddNPCLinkMapsXmap(24, 84, 10, 2);
			Xmap.AddNPCLinkMapsXmap(25, 24, 11, 0);
			Xmap.AddNPCLinkMapsXmap(25, 26, 11, 1);
			Xmap.AddNPCLinkMapsXmap(25, 84, 11, 2);
			Xmap.AddNPCLinkMapsXmap(26, 24, 12, 0);
			Xmap.AddNPCLinkMapsXmap(26, 25, 12, 1);
			Xmap.AddNPCLinkMapsXmap(26, 84, 12, 2);
			Xmap.AddNPCLinkMapsXmap(27, 102, 38, 1);
			Xmap.AddNPCLinkMapsXmap(27, 53, 25, 0);
			Xmap.AddNPCLinkMapsXmap(28, 102, 38, 1);
			Xmap.AddNPCLinkMapsXmap(29, 102, 38, 1);
			Xmap.AddNPCLinkMapsXmap(45, 46, 19, 3);
			Xmap.AddNPCLinkMapsXmap(52, 127, 44, 0);
			Xmap.AddNPCLinkMapsXmap(52, 129, 23, 3);
			Xmap.AddNPCLinkMapsXmap(52, 113, 23, 2);
			Xmap.AddNPCLinkMapsXmap(68, 19, 12, 0);
			Xmap.AddNPCLinkMapsXmap(80, 131, 60, 0);
			Xmap.AddNPCLinkMapsXmap(102, 27, 38, 1);
			Xmap.AddNPCLinkMapsXmap(113, 52, 22, 4);
			Xmap.AddNPCLinkMapsXmap(127, 52, 44, 2);
			Xmap.AddNPCLinkMapsXmap(129, 52, 23, 3);
			Xmap.AddNPCLinkMapsXmap(131, 80, 60, 1);
		}

		// Token: 0x06000A5C RID: 2652 RVA: 0x0009BDAC File Offset: 0x00099FAC
		private static void AddPlanetXmap()
		{
			Xmap.planetDictionary.Add("Trái đất", Xmap.idMapsTraiDat);
			Xmap.planetDictionary.Add("Namếc", Xmap.idMapsNamek);
			Xmap.planetDictionary.Add("Xayda", Xmap.idMapsXayda);
			Xmap.planetDictionary.Add("Fide", Xmap.idMapsNappa);
			Xmap.planetDictionary.Add("Tương lai", Xmap.idMapsTuongLai);
			Xmap.planetDictionary.Add("Cold", Xmap.idMapsCold);
		}

		// Token: 0x06000A5D RID: 2653 RVA: 0x0009BE34 File Offset: 0x0009A034
		private static void AddLinkMapsXmap(params int[] link)
		{
			for (int i = 0; i < link.Length; i++)
			{
				if (!Xmap.linkMaps.ContainsKey(link[i]))
				{
					Xmap.linkMaps.Add(link[i], new List<Xmap.NextMap>());
				}
				if (i != 0)
				{
					Xmap.linkMaps[link[i]].Add(new Xmap.NextMap(link[i - 1], -1, -1));
				}
				if (i != link.Length - 1)
				{
					Xmap.linkMaps[link[i]].Add(new Xmap.NextMap(link[i + 1], -1, -1));
				}
			}
		}

		// Token: 0x06000A5E RID: 2654 RVA: 0x000085E3 File Offset: 0x000067E3
		private static void AddNPCLinkMapsXmap(int currentMapID, int nextMapID, int npcID, int select)
		{
			if (!Xmap.linkMaps.ContainsKey(currentMapID))
			{
				Xmap.linkMaps.Add(currentMapID, new List<Xmap.NextMap>());
			}
			Xmap.linkMaps[currentMapID].Add(new Xmap.NextMap(nextMapID, npcID, select));
		}

		// Token: 0x06000A5F RID: 2655 RVA: 0x0009BEB8 File Offset: 0x0009A0B8
		private static void Goto(int mapID)
		{
			foreach (Xmap.NextMap nextMap in Xmap.linkMaps[TileMap.mapID])
			{
				if (nextMap.MapID == mapID)
				{
					nextMap.GotoMap();
					return;
				}
			}
			GameScr.info1.addInfo("Không thể thực hiện", 0);
		}

		// Token: 0x06000A60 RID: 2656 RVA: 0x0000861A File Offset: 0x0000681A
		private static int[] FindWay(int mapID)
		{
			return Xmap.FindWay(mapID, new int[]
			{
			TileMap.mapID
			});
		}

		// Token: 0x06000A61 RID: 2657 RVA: 0x0009BF30 File Offset: 0x0009A130
		private static int[] FindWay(int mapIDEnd, int[] mapIDs)
		{
			List<int[]> list = new List<int[]>();
			List<int> list2 = new List<int>();
			list2.AddRange(mapIDs);
			foreach (Xmap.NextMap nextMap in Xmap.linkMaps[mapIDs[mapIDs.Length - 1]])
			{
				if (mapIDEnd == nextMap.MapID)
				{
					list2.Add(mapIDEnd);
					return list2.ToArray();
				}
				if (!list2.Contains(nextMap.MapID))
				{
					int[] array = Xmap.FindWay(mapIDEnd, new List<int>(list2)
				{
					nextMap.MapID
				}.ToArray());
					if (array != null)
					{
						list.Add(array);
					}
				}
			}
			int num = 9999;
			int[] result = null;
			foreach (int[] array2 in list)
			{
				if (!Xmap.hasWayGoFutureAndBack(array2) && (global::Char.myCharz().taskMaint.taskId > 30 || !Xmap.hasWayGoToColdMap(array2)) && array2.Length < num)
				{
					num = array2.Length;
					result = array2;
				}
			}
			return result;
		}

		// Token: 0x06000A62 RID: 2658 RVA: 0x0009C070 File Offset: 0x0009A270
		private static bool hasWayGoFutureAndBack(int[] ways)
		{
			for (int i = 1; i < ways.Length - 1; i++)
			{
				if (ways[i] == 102 && ways[i + 1] == 24 && (ways[i - 1] == 27 || ways[i - 1] == 28 || ways[i - 1] == 29))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000A63 RID: 2659 RVA: 0x0009C0BC File Offset: 0x0009A2BC
		private static bool hasWayGoToColdMap(int[] ways)
		{
			for (int i = 0; i < ways.Length; i++)
			{
				if (ways[i] >= 105 && ways[i] <= 110)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000A64 RID: 2660 RVA: 0x0009C0E8 File Offset: 0x0009A2E8
		private static string GetMapName(int mapID)
		{
			string result;
			if (mapID != 113)
			{
				if (mapID != 129)
				{
					result = TileMap.mapNames[mapID] + "\n[" + mapID.ToString() + "]";
				}
				else
				{
					result = TileMap.mapNames[mapID] + " 23\n[" + mapID.ToString() + "]";
				}
			}
			else
			{
				result = string.Concat(new object[]
				{
				"Siêu hạng\n[",
				mapID,
				"]"
				});
			}
			return result;
		}

		// Token: 0x06000A65 RID: 2661 RVA: 0x0009C168 File Offset: 0x0009A368
		private static void LoadWaypointsInMap()
		{
			Xmap.ResetSavedWaypoints();
			int num = TileMap.vGo.size();
			if (num != 2)
			{
				for (int i = 0; i < num; i++)
				{
					Waypoint waypoint = (Waypoint)TileMap.vGo.elementAt(i);
					if (waypoint.maxX < 60)
					{
						Xmap.wayPointMapLeft[0] = (int)(waypoint.minX + 15);
						Xmap.wayPointMapLeft[1] = (int)waypoint.maxY;
					}
					else if ((int)waypoint.maxX > TileMap.pxw - 60)
					{
						Xmap.wayPointMapRight[0] = (int)(waypoint.maxX - 15);
						Xmap.wayPointMapRight[1] = (int)waypoint.maxY;
					}
					else
					{
						Xmap.wayPointMapCenter[0] = (int)(waypoint.minX + 15);
						Xmap.wayPointMapCenter[1] = (int)waypoint.maxY;
					}
				}
				return;
			}
			Waypoint waypoint2 = (Waypoint)TileMap.vGo.elementAt(0);
			Waypoint waypoint3 = (Waypoint)TileMap.vGo.elementAt(1);
			if ((waypoint2.maxX < 60 && waypoint3.maxX < 60) || ((int)waypoint2.minX > TileMap.pxw - 60 && (int)waypoint3.minX > TileMap.pxw - 60))
			{
				Xmap.wayPointMapLeft[0] = (int)(waypoint2.minX + 15);
				Xmap.wayPointMapLeft[1] = (int)waypoint2.maxY;
				Xmap.wayPointMapRight[0] = (int)(waypoint3.maxX - 15);
				Xmap.wayPointMapRight[1] = (int)waypoint3.maxY;
				return;
			}
			if (waypoint2.maxX < waypoint3.maxX)
			{
				Xmap.wayPointMapLeft[0] = (int)(waypoint2.minX + 15);
				Xmap.wayPointMapLeft[1] = (int)waypoint2.maxY;
				Xmap.wayPointMapRight[0] = (int)(waypoint3.maxX - 15);
				Xmap.wayPointMapRight[1] = (int)waypoint3.maxY;
				return;
			}
			Xmap.wayPointMapLeft[0] = (int)(waypoint3.minX + 15);
			Xmap.wayPointMapLeft[1] = (int)waypoint3.maxY;
			Xmap.wayPointMapRight[0] = (int)(waypoint2.maxX - 15);
			Xmap.wayPointMapRight[1] = (int)waypoint2.maxY;
		}

		// Token: 0x06000A66 RID: 2662 RVA: 0x0009C344 File Offset: 0x0009A544
		private static int GetYGround(int x)
		{
			int num = 50;
			int i = 0;
			while (i < 30)
			{
				i++;
				num += 24;
				if (TileMap.tileTypeAt(x, num, 2))
				{
					if (num % 24 != 0)
					{
						num -= num % 24;
						break;
					}
					break;
				}
			}
			return num;
		}

		// Token: 0x06000A67 RID: 2663 RVA: 0x0009C380 File Offset: 0x0009A580
		private static void TeleportTo(int x, int y)
		{
			if (GameScr.canAutoPlay)
			{
				global::Char.myCharz().cx = x;
				global::Char.myCharz().cy = y;
				Service.gI().charMove();
				return;
			}
			global::Char.myCharz().cx = x;
			global::Char.myCharz().cy = y;
			Service.gI().charMove();
			global::Char.myCharz().cx = x;
			global::Char.myCharz().cy = y + 1;
			Service.gI().charMove();
			global::Char.myCharz().cx = x;
			global::Char.myCharz().cy = y;
			Service.gI().charMove();
		}

		// Token: 0x06000A68 RID: 2664 RVA: 0x00008630 File Offset: 0x00006830
		private static void ResetSavedWaypoints()
		{
			Xmap.wayPointMapLeft = new int[2];
			Xmap.wayPointMapCenter = new int[2];
			Xmap.wayPointMapRight = new int[2];
		}

		// Token: 0x06000A69 RID: 2665 RVA: 0x00008653 File Offset: 0x00006853
		private static bool isNRDMap(int mapID)
		{
			return mapID >= 85 && mapID <= 91;
		}

		// Token: 0x06000A6A RID: 2666 RVA: 0x0009C418 File Offset: 0x0009A618
		private static bool isFutureMap(int mapID)
		{
			for (int i = 0; i < Xmap.idMapsTuongLai.Length; i++)
			{
				if (Xmap.idMapsTuongLai[i] == mapID)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000A6B RID: 2667 RVA: 0x00008664 File Offset: 0x00006864
		private static bool isNRD(ItemMap mapID)
		{
			return mapID.template.id >= 372 && mapID.template.id <= 378;
		}

		// Token: 0x06000A6C RID: 2668 RVA: 0x0009C444 File Offset: 0x0009A644
		public static void LoadMap(int position)
		{
			if (Xmap.isNRDMap(TileMap.mapID))
			{
				Xmap.TeleportInNRDMap(position);
				return;
			}
			Xmap.LoadWaypointsInMap();
			switch (position)
			{
				case 0:
					if (Xmap.wayPointMapLeft[0] != 0 && Xmap.wayPointMapLeft[1] != 0)
					{
						Xmap.TeleportTo(Xmap.wayPointMapLeft[0], Xmap.wayPointMapLeft[1]);
					}
					else
					{
						Xmap.TeleportTo(60, Xmap.GetYGround(60));
					}
					break;
				case 1:
					if (Xmap.wayPointMapRight[0] != 0 && Xmap.wayPointMapRight[1] != 0)
					{
						Xmap.TeleportTo(Xmap.wayPointMapRight[0], Xmap.wayPointMapRight[1]);
					}
					else
					{
						Xmap.TeleportTo(TileMap.pxw - 60, Xmap.GetYGround(TileMap.pxw - 60));
					}
					break;
				case 2:
					if (Xmap.wayPointMapCenter[0] != 0 && Xmap.wayPointMapCenter[1] != 0)
					{
						Xmap.TeleportTo(Xmap.wayPointMapCenter[0], Xmap.wayPointMapCenter[1]);
					}
					else
					{
						Xmap.TeleportTo(TileMap.pxw / 2, Xmap.GetYGround(TileMap.pxw / 2));
					}
					break;
			}
			if (TileMap.mapID != 7 && TileMap.mapID != 14 && TileMap.mapID != 0)
			{
				Service.gI().requestChangeMap();
				return;
			}
			Service.gI().getMapOffline();
		}

		// Token: 0x06000A6D RID: 2669 RVA: 0x0009C56C File Offset: 0x0009A76C
		private static void TeleportInNRDMap(int position)
		{
			if (position == 0)
			{
				Xmap.TeleportTo(60, Xmap.GetYGround(60));
				return;
			}
			if (position != 2)
			{
				Xmap.TeleportTo(TileMap.pxw - 60, Xmap.GetYGround(TileMap.pxw - 60));
				return;
			}
			for (int i = 0; i < GameScr.vNpc.size(); i++)
			{
				Npc npc = (Npc)GameScr.vNpc.elementAt(i);
				if (npc.template.npcTemplateId >= 30 && npc.template.npcTemplateId <= 36)
				{
					global::Char.myCharz().npcFocus = npc;
					Xmap.TeleportTo(npc.cx, npc.cy - 3);
					return;
				}
			}
		}

		// Token: 0x06000A6E RID: 2670 RVA: 0x0009C610 File Offset: 0x0009A810
		static Xmap()
		{
			Xmap.LoadLinkMapsXmap();
			Xmap.LoadNPCLinkMapsXmap();
			Xmap.AddPlanetXmap();
			Xmap.LoadData();
		}

		// Token: 0x06000A70 RID: 2672 RVA: 0x0009C6DC File Offset: 0x0009A8DC
		public static int FindIndexItem(int idItem)
		{
			for (int i = 0; i < global::Char.myCharz().arrItemBag.Length; i++)
			{
				if (global::Char.myCharz().arrItemBag[i] != null && (int)global::Char.myCharz().arrItemBag[i].template.id == idItem)
				{
					return global::Char.myCharz().arrItemBag[i].indexUI;
				}
			}
			return -1;
		}

		// Token: 0x06000A71 RID: 2673 RVA: 0x0000868F File Offset: 0x0000688F
		public static void UseCapsuleVip()
		{
			Xmap.isOpeningPanel = false;
			Service.gI().useItem(0, 1, (sbyte)Xmap.FindIndexItem(194), -1);
		}

		// Token: 0x06000A72 RID: 2674 RVA: 0x0009C73C File Offset: 0x0009A93C
		public static void GotoNpc(int npcID, int npcID2, int npcID3)
		{
			for (int i = 0; i < GameScr.vNpc.size(); i++)
			{
				Npc npc = (Npc)GameScr.vNpc.elementAt(i);
				if (npc.template.npcTemplateId == npcID && global::Math.abs(npc.cx - global::Char.myCharz().cx) >= 50)
				{
					Xmap.GotoXY(npc.cx, npc.cy - 1);
					global::Char.myCharz().focusManualTo(npc);
					return;
				}
			}
		}

		// Token: 0x06000A73 RID: 2675 RVA: 0x000086AF File Offset: 0x000068AF
		public static void GotoXY(int x, int y)
		{
			global::Char.myCharz().cx = x;
			global::Char.myCharz().cy = y;
			Service.gI().charMove();
		}

		// Token: 0x04001346 RID: 4934
		public static Xmap _Instance;

		// Token: 0x04001347 RID: 4935
		private static Dictionary<int, List<Xmap.NextMap>> linkMaps = new Dictionary<int, List<Xmap.NextMap>>();

		// Token: 0x04001348 RID: 4936
		private static Dictionary<string, int[]> planetDictionary = new Dictionary<string, int[]>();

		// Token: 0x04001349 RID: 4937
		public static bool isXmaping;

		// Token: 0x0400134A RID: 4938
		public static int IdMapEnd;

		// Token: 0x0400134B RID: 4939
		private static int[] wayPointMapLeft;

		// Token: 0x0400134C RID: 4940
		private static int[] wayPointMapCenter;

		// Token: 0x0400134D RID: 4941
		private static int[] wayPointMapRight;

		// Token: 0x0400134E RID: 4942
		private static bool isEatChicken = true;

		// Token: 0x0400134F RID: 4943
		private static bool isHarvestPean;

		// Token: 0x04001350 RID: 4944
		private static bool isUseCapsule = true;

		// Token: 0x04001351 RID: 4945
		private static bool isUsingCapsule;

		// Token: 0x04001352 RID: 4946
		private static bool isOpeningPanel;

		// Token: 0x04001353 RID: 4947
		private static long lastTimeOpenedPanel;

		// Token: 0x04001354 RID: 4948
		private static bool isSaveData;

		// Token: 0x04001355 RID: 4949
		private static long lastWaitTime;

		// Token: 0x04001356 RID: 4950
		private static int[] idMapsNamek = new int[]
		{
		43,
		22,
		7,
		8,
		9,
		11,
		12,
		13,
		10,
		31,
		32,
		33,
		34,
		43,
		25
		};

		// Token: 0x04001357 RID: 4951
		private static int[] idMapsXayda = new int[]
		{
		44,
		23,
		14,
		15,
		16,
		17,
		18,
		20,
		19,
		35,
		36,
		37,
		38,
		52,
		44,
		26,
		84,
		113,
		127,
		129
		};

		// Token: 0x04001358 RID: 4952
		private static int[] idMapsTraiDat = new int[]
		{
		42,
		21,
		0,
		1,
		2,
		3,
		4,
		5,
		6,
		27,
		28,
		29,
		30,
		47,
		42,
		24,
		46,
		45,
		48,
		53,
		58,
		59,
		60,
		61,
		62,
		55,
		56,
		54,
		57
		};

		// Token: 0x04001359 RID: 4953
		private static int[] idMapsTuongLai = new int[]
		{
		102,
		92,
		93,
		94,
		96,
		97,
		98,
		99,
		100,
		103
		};

		// Token: 0x0400135A RID: 4954
		private static int[] idMapsCold = new int[]
		{
		109,
		108,
		107,
		110,
		106,
		105
		};

		// Token: 0x0400135B RID: 4955
		private static int[] idMapsNappa = new int[]
		{
		68,
		69,
		70,
		71,
		72,
		64,
		65,
		63,
		66,
		67,
		73,
		74,
		75,
		76,
		77,
		81,
		82,
		83,
		79,
		80,
		131,
		132,
		133
		};

		// Token: 0x020000CA RID: 202
		public class NextMap
		{
			// Token: 0x06000A74 RID: 2676 RVA: 0x000086D1 File Offset: 0x000068D1
			public NextMap(int mapID, int npcID, int index)
			{
				this.MapID = mapID;
				this.Npc = npcID;
				this.Index = index;
			}

			// Token: 0x06000A75 RID: 2677 RVA: 0x0009C7B8 File Offset: 0x0009A9B8
			public void GotoMap()
			{
				if (this.Index == -1 && this.Npc == -1)
				{
					Waypoint wayPoint = this.GetWayPoint();
					if (wayPoint != null)
					{
						this.Enter(wayPoint);
						return;
					}
				}
				else if (this.Npc != -1 && this.Index != -1)
				{
					Service.gI().openMenu(this.Npc);
					Service.gI().confirmMenu(0, (sbyte)this.Index);
				}
			}

			// Token: 0x06000A76 RID: 2678 RVA: 0x0009C820 File Offset: 0x0009AA20
			public Waypoint GetWayPoint()
			{
				for (int i = 0; i < TileMap.vGo.size(); i++)
				{
					Waypoint waypoint = (Waypoint)TileMap.vGo.elementAt(i);
					if (this.GetMapName().Equals(this.GetMapName(waypoint.popup)))
					{
						return waypoint;
					}
				}
				return null;
			}

			// Token: 0x06000A77 RID: 2679 RVA: 0x000086EE File Offset: 0x000068EE
			public string GetMapName()
			{
				return TileMap.mapNames[this.MapID];
			}

			// Token: 0x06000A78 RID: 2680 RVA: 0x0009C870 File Offset: 0x0009AA70
			public void Enter(Waypoint waypoint)
			{
				int num = (waypoint.maxX < 60) ? 15 : (((int)waypoint.minX <= TileMap.pxw - 60) ? ((int)((waypoint.minX + waypoint.maxX) / 2)) : (TileMap.pxw - 15));
				int maxY = (int)waypoint.maxY;
				if (num == -1 || maxY == -1)
				{
					GameScr.info1.addInfo("Có lỗi xảy ra", 0);
					return;
				}
				this.TeleportTo(num, maxY);
				if (waypoint.isOffline)
				{
					Service.gI().getMapOffline();
					return;
				}
				Service.gI().requestChangeMap();
			}

			// Token: 0x06000A79 RID: 2681 RVA: 0x0009C8FC File Offset: 0x0009AAFC
			public string GetMapName(PopUp popup)
			{
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 0; i < popup.says.Length; i++)
				{
					stringBuilder.Append(popup.says[i]);
					stringBuilder.Append(" ");
				}
				return stringBuilder.ToString().Trim();
			}

			// Token: 0x06000A7A RID: 2682 RVA: 0x0009C948 File Offset: 0x0009AB48
			public void TeleportTo(int x, int y)
			{
				if (GameScr.canAutoPlay)
				{
					global::Char.myCharz().cx = x;
					global::Char.myCharz().cy = y;
					Service.gI().charMove();
					return;
				}
				global::Char.myCharz().cx = x;
				global::Char.myCharz().cy = y;
				Service.gI().charMove();
				global::Char.myCharz().cx = x;
				global::Char.myCharz().cy = y + 1;
				Service.gI().charMove();
				global::Char.myCharz().cx = x;
				global::Char.myCharz().cy = y;
				Service.gI().charMove();
			}

			// Token: 0x0400135C RID: 4956
			public int MapID;

			// Token: 0x0400135D RID: 4957
			public int Npc;

			// Token: 0x0400135E RID: 4958
			public int Index;
		}
	}

}
