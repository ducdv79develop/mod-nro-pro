using System;
using AssemblyCSharp.Mod2.Xmap;
using Assets.src.g;
using Mod;
using UnityEngine;

// Token: 0x020000B9 RID: 185
public class Panel : IActionListener, IChatable
{
	// Token: 0x06000870 RID: 2160 RVA: 0x000754A0 File Offset: 0x000738A0
	public Panel()
	{
		this.init();
		this.cmdClose = new Command(string.Empty, this, 1003, null);
		this.cmdClose.img = GameCanvas.loadImage("/mainImage/myTexture2dbtX.png");
		this.cmdClose.cmdClosePanel = true;
	}

	// Token: 0x06000871 RID: 2161 RVA: 0x00075888 File Offset: 0x00073C88
	public static void loadBg()
	{
		Panel.imgMap = GameCanvas.loadImage("/img/map" + TileMap.planetID + ".png");
		Panel.imgBantay = GameCanvas.loadImage("/mainImage/myTexture2dbantay.png");
		Panel.imgX = GameCanvas.loadImage("/mainImage/myTexture2dbtX.png");
		Panel.imgXu = GameCanvas.loadImage("/mainImage/myTexture2dimgMoney.png");
		Panel.imgLuong = GameCanvas.loadImage("/mainImage/myTexture2dimgDiamond.png");
		Panel.imgLuongKhoa = GameCanvas.loadImage("/mainImage/luongkhoa.png");
		Panel.imgUp = GameCanvas.loadImage("/mainImage/myTexture2dup.png");
		Panel.imgDown = GameCanvas.loadImage("/mainImage/myTexture2ddown.png");
		Panel.imgStar = GameCanvas.loadImage("/mainImage/star.png");
		Panel.imgMaxStar = GameCanvas.loadImage("/mainImage/starE.png");
		Panel.imgStar8 = GameCanvas.loadImage("/mainImage/star8.png");
		Panel.imgNew = GameCanvas.loadImage("/mainImage/new.png");
		Panel.imgTicket = GameCanvas.loadImage("/mainImage/ticket12.png");
	}

	// Token: 0x06000872 RID: 2162 RVA: 0x0007596C File Offset: 0x00073D6C
	public void init()
	{
		this.pX = GameCanvas.pxLast + this.cmxMap;
		this.pY = GameCanvas.pyLast + this.cmyMap;
		this.lastTabIndex = new int[this.tabName.Length];
		for (int i = 0; i < this.lastTabIndex.Length; i++)
		{
			this.lastTabIndex[i] = -1;
		}
	}

	// Token: 0x06000873 RID: 2163 RVA: 0x000759D4 File Offset: 0x00073DD4
	public int getXMap()
	{
		for (int i = 0; i < Panel.mapId[(int)TileMap.planetID].Length; i++)
		{
			if (TileMap.mapID == Panel.mapId[(int)TileMap.planetID][i])
			{
				return Panel.mapX[(int)TileMap.planetID][i];
			}
		}
		return -1;
	}

	// Token: 0x06000874 RID: 2164 RVA: 0x00075A28 File Offset: 0x00073E28
	public int getYMap()
	{
		for (int i = 0; i < Panel.mapId[(int)TileMap.planetID].Length; i++)
		{
			if (TileMap.mapID == Panel.mapId[(int)TileMap.planetID][i])
			{
				return Panel.mapY[(int)TileMap.planetID][i];
			}
		}
		return -1;
	}

	// Token: 0x06000875 RID: 2165 RVA: 0x00075A7C File Offset: 0x00073E7C
	public int getXMapTask()
	{
		if (global::Char.myCharz().taskMaint == null)
		{
			return -1;
		}
		for (int i = 0; i < Panel.mapId[(int)TileMap.planetID].Length; i++)
		{
			if (GameScr.mapTasks[global::Char.myCharz().taskMaint.index] == Panel.mapId[(int)TileMap.planetID][i])
			{
				return Panel.mapX[(int)TileMap.planetID][i];
			}
		}
		return -1;
	}

	// Token: 0x06000876 RID: 2166 RVA: 0x00075AF4 File Offset: 0x00073EF4
	public int getYMapTask()
	{
		if (global::Char.myCharz().taskMaint == null)
		{
			return -1;
		}
		for (int i = 0; i < Panel.mapId[(int)TileMap.planetID].Length; i++)
		{
			if (GameScr.mapTasks[global::Char.myCharz().taskMaint.index] == Panel.mapId[(int)TileMap.planetID][i])
			{
				return Panel.mapY[(int)TileMap.planetID][i];
			}
		}
		return -1;
	}

	// Token: 0x06000877 RID: 2167 RVA: 0x00075B6C File Offset: 0x00073F6C
	private void setType(int position)
	{
		this.typeShop = -1;
		this.W = Panel.WIDTH_PANEL;
		this.H = GameCanvas.h;
		this.X = 0;
		this.Y = 0;
		this.ITEM_HEIGHT = 24;
		this.position = position;
		if (position == 0)
		{
			this.xScroll = 2;
			this.yScroll = 80;
			this.wScroll = this.W - 4;
			this.hScroll = this.H - 96;
			this.cmx = this.wScroll;
			this.cmtoX = 0;
			this.X = 0;
		}
		else if (position == 1)
		{
			this.wScroll = this.W - 4;
			this.xScroll = GameCanvas.w - this.wScroll;
			this.yScroll = 80;
			this.hScroll = this.H - 96;
			this.X = this.xScroll - 2;
			this.cmx = -(GameCanvas.w + this.W);
			this.cmtoX = GameCanvas.w - this.W;
		}
		this.TAB_W = this.W / 5 - 1;
		this.currentTabIndex = 0;
		this.currentTabName = this.tabName[this.type];
		if (this.currentTabName.Length < 5)
		{
			this.TAB_W += 5;
		}
		this.startTabPos = this.xScroll + this.wScroll / 2 - this.currentTabName.Length * this.TAB_W / 2;
		this.lastSelect = new int[this.currentTabName.Length];
		this.cmyLast = new int[this.currentTabName.Length];
		for (int i = 0; i < this.currentTabName.Length; i++)
		{
			this.lastSelect[i] = ((!GameCanvas.isTouch) ? 0 : -1);
		}
		if (this.lastTabIndex[this.type] != -1)
		{
			this.currentTabIndex = this.lastTabIndex[this.type];
		}
		if (this.currentTabIndex < 0)
		{
			this.currentTabIndex = 0;
		}
		if (this.currentTabIndex > this.currentTabName.Length - 1)
		{
			this.currentTabIndex = this.currentTabName.Length - 1;
		}
		this.scroll = null;
	}

	// Token: 0x06000878 RID: 2168 RVA: 0x00075DA0 File Offset: 0x000741A0
	public void setTypeMapTrans()
	{
		this.type = 14;
		this.setType(0);
		this.setTabMapTrans();
		this.cmx = (this.cmtoX = 0);
	}

	// Token: 0x06000879 RID: 2169 RVA: 0x00075DD2 File Offset: 0x000741D2
	public void setTypeInfomatioin()
	{
		this.type = 6;
		this.cmx = this.wScroll;
		this.cmtoX = 0;
	}

	// Token: 0x0600087A RID: 2170 RVA: 0x00075DF0 File Offset: 0x000741F0
	public void setTypeMap()
	{
		if (GameScr.gI().isMapFize())
		{
			return;
		}
		if (!Panel.isPaintMap)
		{
			return;
		}
		if (Hint.isOnTask(2, 0))
		{
			Hint.isViewMap = true;
			GameScr.info1.addInfo(mResources.go_to_quest, 0);
		}
		if (Hint.isOnTask(3, 0))
		{
			Hint.isViewPotential = true;
		}
		this.type = 4;
		this.currentTabName = this.tabName[this.type];
		this.startTabPos = this.xScroll + this.wScroll / 2 - this.currentTabName.Length * this.TAB_W / 2;
		this.cmx = (this.cmtoX = 0);
		this.setTabMap();
	}

	// Token: 0x0600087B RID: 2171 RVA: 0x00075EA4 File Offset: 0x000742A4
	public void setTypeArchivement()
	{
		this.currentListLength = global::Char.myCharz().arrArchive.Length;
		this.setType(0);
		this.type = 9;
		this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
		this.cmy = (this.cmtoY = this.cmyLast[this.currentTabIndex]);
		if (this.cmyLim < 0)
		{
			this.cmyLim = 0;
		}
		if (this.cmy < 0)
		{
			this.cmy = (this.cmtoY = 0);
		}
		if (this.cmy > this.cmyLim)
		{
			this.cmy = (this.cmtoY = 0);
		}
		this.selected = ((!GameCanvas.isTouch) ? 0 : -1);
	}

	// Token: 0x0600087C RID: 2172 RVA: 0x00075F6F File Offset: 0x0007436F
	public void setTypeKiGuiOnly()
	{
		this.type = 17;
		this.setType(1);
		this.setTabKiGui();
		this.typeShop = 2;
		this.currentTabIndex = 0;
	}

	// Token: 0x0600087D RID: 2173 RVA: 0x00075F94 File Offset: 0x00074394
	public void setTabKiGui()
	{
		this.ITEM_HEIGHT = 24;
		this.currentListLength = global::Char.myCharz().arrItemShop[4].Length;
		this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
		if (this.cmyLim < 0)
		{
			this.cmyLim = 0;
		}
		this.cmy = (this.cmtoY = this.cmyLast[this.currentTabIndex]);
		if (this.cmy < 0)
		{
			this.cmy = (this.cmtoY = 0);
		}
		if (this.cmy > this.cmyLim)
		{
			this.cmy = (this.cmtoY = this.cmyLim);
		}
		this.selected = ((!GameCanvas.isTouch) ? 0 : -1);
	}

	// Token: 0x0600087E RID: 2174 RVA: 0x0007605F File Offset: 0x0007445F
	public void setTypeBodyOnly()
	{
		this.type = 7;
		this.setType(1);
		this.setTabInventory(true);
		this.currentTabIndex = 0;
	}

	// Token: 0x0600087F RID: 2175 RVA: 0x0007607D File Offset: 0x0007447D
	public void addChatMessage(InfoItem info)
	{
		this.logChat.insertElementAt(info, 0);
		if (this.logChat.size() > 20)
		{
			this.logChat.removeElementAt(this.logChat.size() - 1);
		}
	}

	// Token: 0x06000880 RID: 2176 RVA: 0x000760B6 File Offset: 0x000744B6
	public void addPlayerMenu(Command pm)
	{
		this.vPlayerMenu.addElement(pm);
	}

	// Token: 0x06000881 RID: 2177 RVA: 0x000760C4 File Offset: 0x000744C4
	public void setTabPlayerMenu()
	{
		this.ITEM_HEIGHT = 24;
		this.currentListLength = this.vPlayerMenu.size();
		this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
		if (this.cmyLim < 0)
		{
			this.cmyLim = 0;
		}
		this.cmy = (this.cmtoY = this.cmyLast[this.currentTabIndex]);
		if (this.cmy < 0)
		{
			this.cmy = (this.cmtoY = 0);
		}
		if (this.cmy > this.cmyLim)
		{
			this.cmy = (this.cmtoY = this.cmyLim);
		}
		this.selected = ((!GameCanvas.isTouch) ? 0 : -1);
	}

	// Token: 0x06000882 RID: 2178 RVA: 0x0007618C File Offset: 0x0007458C
	public void setTypeFlag()
	{
		this.type = 18;
		this.setType(0);
		this.ITEM_HEIGHT = 24;
		this.selected = ((!GameCanvas.isTouch) ? 0 : -1);
		this.setTabFlag();
	}

	// Token: 0x06000883 RID: 2179 RVA: 0x000761C4 File Offset: 0x000745C4
	public void setTabFlag()
	{
		this.currentListLength = this.vFlag.size();
		this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
		if (this.cmyLim < 0)
		{
			this.cmyLim = 0;
		}
		this.cmy = (this.cmtoY = this.cmyLast[this.currentTabIndex]);
		if (this.cmy < 0)
		{
			this.cmy = (this.cmtoY = 0);
		}
		if (this.cmy > this.cmyLim)
		{
			this.cmy = (this.cmtoY = this.cmyLim);
		}
		if (this.selected > this.currentListLength - 1)
		{
			this.selected = this.currentListLength - 1;
		}
		this.cmx = (this.cmtoX = 0);
	}

	// Token: 0x06000884 RID: 2180 RVA: 0x0007629E File Offset: 0x0007469E
	public void setTypePlayerMenu(global::Char c)
	{
		this.type = 10;
		this.setType(0);
		this.setTabPlayerMenu();
		this.charMenu = c;
	}

	// Token: 0x06000885 RID: 2181 RVA: 0x000762BC File Offset: 0x000746BC
	public void setTypeFriend()
	{
		this.type = 11;
		this.setType(0);
		this.ITEM_HEIGHT = 24;
		this.selected = ((!GameCanvas.isTouch) ? 0 : -1);
		this.setTabFriend();
	}

	// Token: 0x06000886 RID: 2182 RVA: 0x000762F2 File Offset: 0x000746F2
	public void setTypeEnemy()
	{
		this.type = 16;
		this.setType(0);
		this.ITEM_HEIGHT = 24;
		this.selected = ((!GameCanvas.isTouch) ? 0 : -1);
		this.setTabEnemy();
	}

	// Token: 0x06000887 RID: 2183 RVA: 0x00076328 File Offset: 0x00074728
	public void setTypeTop(sbyte t)
	{
		this.type = 15;
		this.setType(0);
		this.ITEM_HEIGHT = 24;
		this.selected = ((!GameCanvas.isTouch) ? 0 : -1);
		this.setTabTop();
		this.isThachDau = ((int)t != 0);
	}

	// Token: 0x06000888 RID: 2184 RVA: 0x00076380 File Offset: 0x00074780
	public void setTabTop()
	{
		this.currentListLength = this.vTop.size();
		this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
		if (this.cmyLim < 0)
		{
			this.cmyLim = 0;
		}
		this.cmy = (this.cmtoY = this.cmyLast[this.currentTabIndex]);
		if (this.cmy < 0)
		{
			this.cmy = (this.cmtoY = 0);
		}
		if (this.cmy > this.cmyLim)
		{
			this.cmy = (this.cmtoY = this.cmyLim);
		}
		if (this.selected > this.currentListLength - 1)
		{
			this.selected = this.currentListLength - 1;
		}
		this.cmx = (this.cmtoX = 0);
	}

	// Token: 0x06000889 RID: 2185 RVA: 0x0007645C File Offset: 0x0007485C
	public void setTabFriend()
	{
		this.currentListLength = this.vFriend.size();
		this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
		if (this.cmyLim < 0)
		{
			this.cmyLim = 0;
		}
		this.cmy = (this.cmtoY = this.cmyLast[this.currentTabIndex]);
		if (this.cmy < 0)
		{
			this.cmy = (this.cmtoY = 0);
		}
		if (this.cmy > this.cmyLim)
		{
			this.cmy = (this.cmtoY = this.cmyLim);
		}
		if (this.selected > this.currentListLength - 1)
		{
			this.selected = this.currentListLength - 1;
		}
		this.cmx = (this.cmtoX = 0);
	}

	// Token: 0x0600088A RID: 2186 RVA: 0x00076538 File Offset: 0x00074938
	public void setTabEnemy()
	{
		this.currentListLength = this.vEnemy.size();
		this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
		if (this.cmyLim < 0)
		{
			this.cmyLim = 0;
		}
		this.cmy = (this.cmtoY = this.cmyLast[this.currentTabIndex]);
		if (this.cmy < 0)
		{
			this.cmy = (this.cmtoY = 0);
		}
		if (this.cmy > this.cmyLim)
		{
			this.cmy = (this.cmtoY = this.cmyLim);
		}
		if (this.selected > this.currentListLength - 1)
		{
			this.selected = this.currentListLength - 1;
		}
		this.cmx = (this.cmtoX = 0);
	}

	// Token: 0x0600088B RID: 2187 RVA: 0x00076612 File Offset: 0x00074A12
	public void setTypeMessage()
	{
		this.type = 8;
		this.setType(0);
		this.setTabMessage();
		this.currentTabIndex = 0;
	}

	// Token: 0x0600088C RID: 2188 RVA: 0x0007662F File Offset: 0x00074A2F
	public void setTypeLockInventory()
	{
		this.type = 8;
		this.setType(0);
		this.setTabMessage();
		this.currentTabIndex = 0;
	}

	// Token: 0x0600088D RID: 2189 RVA: 0x0007664C File Offset: 0x00074A4C
	public void setTypeShop(int typeShop)
	{
		this.type = 1;
		this.setType(0);
		this.setTabShop();
		this.currentTabIndex = 0;
		this.typeShop = typeShop;
	}

	// Token: 0x0600088E RID: 2190 RVA: 0x00076670 File Offset: 0x00074A70
	public void setTypeBox()
	{
		this.type = 2;
		if (GameCanvas.w > 2 * Panel.WIDTH_PANEL)
		{
			Panel.boxTabName = new string[][]
			{
				mResources.chestt
			};
		}
		else
		{
			Panel.boxTabName = new string[][]
			{
				mResources.chestt,
				mResources.inventory
			};
		}
		this.tabName[2] = Panel.boxTabName;
		this.setType(0);
		if (this.currentTabIndex == 0)
		{
			this.setTabBox();
		}
		if (this.currentTabIndex == 1)
		{
			this.setTabInventory(true);
		}
		if (GameCanvas.w > 2 * Panel.WIDTH_PANEL)
		{
			GameCanvas.panel2 = new Panel();
			GameCanvas.panel2.tabName[7] = new string[][]
			{
				new string[]
				{
					string.Empty
				}
			};
			GameCanvas.panel2.setTypeBodyOnly();
			GameCanvas.panel2.show();
		}
	}

	// Token: 0x0600088F RID: 2191 RVA: 0x00076754 File Offset: 0x00074B54
	public void setTypeCombine()
	{
		this.type = 12;
		if (GameCanvas.w > 2 * Panel.WIDTH_PANEL)
		{
			Panel.boxCombine = new string[][]
			{
				mResources.combine
			};
		}
		else
		{
			Panel.boxCombine = new string[][]
			{
				mResources.combine,
				mResources.inventory
			};
		}
		this.tabName[this.type] = Panel.boxCombine;
		this.setType(0);
		if (this.currentTabIndex == 0)
		{
			this.setTabCombine();
		}
		if (this.currentTabIndex == 1)
		{
			this.setTabInventory(true);
		}
		if (GameCanvas.w > 2 * Panel.WIDTH_PANEL)
		{
			GameCanvas.panel2 = new Panel();
			GameCanvas.panel2.tabName[7] = new string[][]
			{
				new string[]
				{
					string.Empty
				}
			};
			GameCanvas.panel2.setTypeBodyOnly();
			GameCanvas.panel2.show();
		}
		this.combineSuccess = -1;
		this.isDoneCombine = true;
	}

	// Token: 0x06000890 RID: 2192 RVA: 0x0007684C File Offset: 0x00074C4C
	public void setTabCombine()
	{
		this.currentListLength = this.vItemCombine.size() + 1;
		this.ITEM_HEIGHT = 24;
		this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
		if (this.cmyLim < 0)
		{
			this.cmyLim = 9;
		}
		this.cmy = (this.cmtoY = this.cmyLast[this.currentTabIndex]);
		if (this.cmy < 0)
		{
			this.cmy = (this.cmtoY = 0);
		}
		if (this.cmy > this.cmyLim)
		{
			this.cmy = (this.cmtoY = this.cmyLim);
		}
		this.selected = ((!GameCanvas.isTouch) ? 0 : -1);
	}

	// Token: 0x06000891 RID: 2193 RVA: 0x00076918 File Offset: 0x00074D18
	public void setTypeAuto()
	{
		this.type = 22;
		this.setType(0);
		this.setTabAuto();
		this.cmx = (this.cmtoX = 0);
	}

	// Token: 0x06000892 RID: 2194 RVA: 0x0007694C File Offset: 0x00074D4C
	private void setTabAuto()
	{
		this.currentListLength = Panel.strAuto.Length;
		this.ITEM_HEIGHT = 24;
		this.selected = ((!GameCanvas.isTouch) ? 0 : -1);
		this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
		if (this.cmyLim < 0)
		{
			this.cmyLim = 0;
		}
		this.cmy = (this.cmtoY = this.cmyLast[this.currentTabIndex]);
		if (this.cmy < 0)
		{
			this.cmy = (this.cmtoY = 0);
		}
		if (this.cmy > this.cmyLim)
		{
			this.cmy = (this.cmtoY = this.cmyLim);
		}
	}

	// Token: 0x06000893 RID: 2195 RVA: 0x00076A10 File Offset: 0x00074E10
	public void setTypePetMain()
	{
		this.type = 21;
		if (GameCanvas.panel2 != null)
		{
			Panel.boxPet = mResources.petMainTab2;
		}
		else
		{
			Panel.boxPet = mResources.petMainTab;
		}
		this.tabName[21] = Panel.boxPet;
		if (global::Char.myCharz().cgender == 1)
		{
			this.strStatus = new string[]
			{
				mResources.follow,
				mResources.defend,
				mResources.attack,
				mResources.gohome,
				mResources.fusion,
				mResources.fusionForever
			};
		}
		else
		{
			this.strStatus = new string[]
			{
				mResources.follow,
				mResources.defend,
				mResources.attack,
				mResources.gohome,
				mResources.fusion
			};
		}
		this.setType(2);
		if (this.currentTabIndex == 0)
		{
			this.setTabPetInventory();
		}
		if (this.currentTabIndex == 1)
		{
			this.setTabPetStatus();
		}
		if (this.currentTabIndex == 2)
		{
			this.setTabInventory(true);
		}
	}

	// Token: 0x06000894 RID: 2196 RVA: 0x00076B18 File Offset: 0x00074F18
	public void setTypeMain()
	{
		this.type = 0;
		this.setType(0);
		if (this.currentTabIndex == 1)
		{
			this.setTabInventory(true);
		}
		if (this.currentTabIndex == 2)
		{
			this.setTabSkill();
		}
		if (this.currentTabIndex == 3)
		{
			if (this.mainTabName.Length == 4)
			{
				this.setTabTool();
			}
			else
			{
				this.setTabClans();
			}
		}
		if (this.currentTabIndex == 4)
		{
			this.setTabTool();
		}
	}

	// Token: 0x06000895 RID: 2197 RVA: 0x00076B98 File Offset: 0x00074F98
	public void setTypeZone()
	{
		this.type = 3;
		this.setType(0);
		this.setTabZone();
		this.cmx = (this.cmtoX = 0);
	}

	// Token: 0x06000896 RID: 2198 RVA: 0x00076BCC File Offset: 0x00074FCC
	public void addItemDetail(Item item)
	{
		try
		{
			this.cp = new ChatPopup();
			string text = string.Empty;
			string text2 = string.Empty;
			if ((int)item.template.gender != global::Char.myCharz().cgender)
			{
				if ((int)item.template.gender == 0)
				{
					text2 = text2 + "\n|7|1|" + mResources.from_earth;
				}
				else if ((int)item.template.gender == 1)
				{
					text2 = text2 + "\n|7|1|" + mResources.from_namec;
				}
				else if ((int)item.template.gender == 2)
				{
					text2 = text2 + "\n|7|1|" + mResources.from_sayda;
				}
			}
			string str = string.Empty;
			if (item.itemOption != null)
			{
				for (int i = 0; i < item.itemOption.Length; i++)
				{
					if (item.itemOption[i].optionTemplate.id == 72)
					{
						str = " [+" + item.itemOption[i].param + "]";
					}
				}
			}
			bool flag = false;
			if (item.itemOption != null)
			{
				for (int j = 0; j < item.itemOption.Length; j++)
				{
					if (item.itemOption[j].optionTemplate.id == 41)
					{
						flag = true;
						if (item.itemOption[j].param == 1)
						{
							text2 = text2 + "|0|1|" + item.template.name + str;
						}
						if (item.itemOption[j].param == 2)
						{
							text2 = text2 + "|2|1|" + item.template.name + str;
						}
						if (item.itemOption[j].param == 3)
						{
							text2 = text2 + "|8|1|" + item.template.name + str;
						}
						if (item.itemOption[j].param == 4)
						{
							text2 = text2 + "|7|1|" + item.template.name + str;
						}
					}
				}
			}
			if (!flag)
			{
				text2 = text2 + "|0|1|" + item.template.name + str;
			}
			if (item.itemOption != null)
			{
				for (int k = 0; k < item.itemOption.Length; k++)
				{
					bool flag2 = item.itemOption[k].optionTemplate.name.StartsWith("$");
					if (flag2)
					{
						text = item.itemOption[k].getOptiongColor();
						if (item.itemOption[k].param == 1)
						{
							text2 = text2 + "\n|1|1|" + text;
						}
						if (item.itemOption[k].param == 0)
						{
							text2 = text2 + "\n|0|1|" + text;
						}
					}
					else
					{
						text = item.itemOption[k].getOptionString();
						if (!text.Equals(string.Empty))
						{
							if (item.itemOption[k].optionTemplate.id != 72)
							{
								if (item.itemOption[k].optionTemplate.id == 102)
								{
									this.cp.starSlot = (sbyte)item.itemOption[k].param;
									Res.outz("STAR SLOT= " + this.cp.starSlot);
								}
								else if (item.itemOption[k].optionTemplate.id == 107)
								{
									this.cp.maxStarSlot = (sbyte)item.itemOption[k].param;
									Res.outz("STAR SLOT= " + this.cp.maxStarSlot);
								}
								else
								{
									text2 = text2 + "\n|1|1|" + text;
								}
							}
						}
					}
				}
			}
			if (this.currItem.template.strRequire > 1)
			{
				string str2 = mResources.pow_request + ": " + this.currItem.template.strRequire;
				if ((long)this.currItem.template.strRequire > global::Char.myCharz().cPower)
				{
					text2 = text2 + "\n|3|1|" + str2;
					string text3 = text2;
					text2 = string.Concat(new object[]
					{
						text3,
						"\n|3|1|",
						mResources.your_pow,
						": ",
						global::Char.myCharz().cPower
					});
				}
				else
				{
					text2 = text2 + "\n|6|1|" + str2;
				}
			}
			else
			{
				text2 += "\n|6|1|";
			}
			this.currItem.compare = this.getCompare(this.currItem);
			text2 += "\n--";
			text2 = text2 + "\n|6|" + item.template.description;
			if (!item.reason.Equals(string.Empty))
			{
				if (!item.template.description.Equals(string.Empty))
				{
					text2 += "\n--";
				}
				text2 = text2 + "\n|2|" + item.reason;
			}
			if ((int)this.cp.maxStarSlot > 0)
			{
				text2 += "\n\n";
			}
			this.popUpDetailInit(this.cp, text2);
			this.idIcon = (int)item.template.iconID;
			this.partID = null;
			this.charInfo = null;
		}
		catch (Exception ex)
		{
			Res.outz("ex " + ex.StackTrace);
		}
	}

	// Token: 0x06000897 RID: 2199 RVA: 0x00077180 File Offset: 0x00075580
	public void popUpDetailInit(ChatPopup cp, string chat)
	{
		cp.isClip = false;
		cp.sayWidth = 180;
		cp.cx = 3 + this.X - ((this.X != 0) ? (Res.abs(cp.sayWidth - this.W) + 8) : 0);
		cp.says = mFont.tahoma_7_red.splitFontArray(chat, cp.sayWidth - 10);
		cp.delay = 10000000;
		cp.c = null;
		cp.sayRun = 7;
		cp.ch = 15 - cp.sayRun + cp.says.Length * 12 + 10;
		if (cp.ch > GameCanvas.h - 80)
		{
			cp.ch = GameCanvas.h - 80;
			cp.lim = cp.says.Length * 12 - cp.ch + 17;
			if (cp.lim < 0)
			{
				cp.lim = 0;
			}
			ChatPopup.cmyText = 0;
			cp.isClip = true;
		}
		cp.cy = GameCanvas.menu.menuY - cp.ch;
		while (cp.cy < 10)
		{
			cp.cy++;
			GameCanvas.menu.menuY++;
		}
		cp.mH = 0;
		cp.strY = 10;
	}

	// Token: 0x06000898 RID: 2200 RVA: 0x000772D8 File Offset: 0x000756D8
	public void popUpDetailInitArray(ChatPopup cp, string[] chat)
	{
		cp.sayWidth = 160;
		cp.cx = 3 + this.X;
		cp.says = chat;
		cp.delay = 10000000;
		cp.c = null;
		cp.sayRun = 7;
		cp.ch = 15 - cp.sayRun + cp.says.Length * 12 + 10;
		cp.cy = GameCanvas.menu.menuY - cp.ch;
		cp.mH = 0;
		cp.strY = 10;
	}

	// Token: 0x06000899 RID: 2201 RVA: 0x00077364 File Offset: 0x00075764
	public void addMessageDetail(ClanMessage cm)
	{
		this.cp = new ChatPopup();
		string text = "|0|" + cm.playerName;
		text = text + "\n|1|" + Member.getRole((int)cm.role);
		for (int i = 0; i < this.myMember.size(); i++)
		{
			Member member = (Member)this.myMember.elementAt(i);
			if (cm.playerId == member.ID)
			{
				string text2 = text;
				text = string.Concat(new object[]
				{
					text2,
					"\n|5|",
					mResources.clan_capsuledonate,
					": ",
					member.clanPoint
				});
				text2 = text;
				text = string.Concat(new object[]
				{
					text2,
					"\n|5|",
					mResources.clan_capsuleself,
					": ",
					member.curClanPoint
				});
				text2 = text;
				text = string.Concat(new object[]
				{
					text2,
					"\n|4|",
					mResources.give_pea,
					": ",
					member.donate,
					mResources.time
				});
				text2 = text;
				text = string.Concat(new object[]
				{
					text2,
					"\n|4|",
					mResources.receive_pea,
					": ",
					member.receive_donate,
					mResources.time
				});
				this.partID = new int[]
				{
					(int)member.head,
					(int)member.leg,
					(int)member.body
				};
				break;
			}
		}
		text += "\n--";
		for (int j = 0; j < cm.chat.Length; j++)
		{
			text = text + "\n" + cm.chat[j];
		}
		if (cm.type == 1)
		{
			string text2 = text;
			text = string.Concat(new object[]
			{
				text2,
				"\n|6|",
				mResources.received,
				" ",
				cm.recieve,
				"/",
				cm.maxCap
			});
		}
		this.popUpDetailInit(this.cp, text);
		this.charInfo = null;
	}

	// Token: 0x0600089A RID: 2202 RVA: 0x000775AC File Offset: 0x000759AC
	public void addThachDauDetail(TopInfo t)
	{
		string text = "|0|1|" + t.name;
		text = text + "\n|1|Top " + t.rank;
		text = text + "\n|1|" + t.info;
		text = text + "\n|2|" + t.info2;
		this.cp = new ChatPopup();
		this.popUpDetailInit(this.cp, text);
		this.partID = new int[]
		{
			t.headID,
			(int)t.leg,
			(int)t.body
		};
		this.currItem = null;
		this.charInfo = null;
	}

	// Token: 0x0600089B RID: 2203 RVA: 0x00077654 File Offset: 0x00075A54
	public void addClanMemberDetail(Member m)
	{
		string text = "|0|1|" + m.name;
		string str = "\n|2|1|";
		if ((int)m.role == 0)
		{
			str = "\n|7|1|";
		}
		if ((int)m.role == 1)
		{
			str = "\n|1|1|";
		}
		if ((int)m.role == 2)
		{
			str = "\n|0|1|";
		}
		text = text + str + Member.getRole((int)m.role);
		string text2 = text;
		text = string.Concat(new string[]
		{
			text2,
			"\n|2|1|",
			mResources.power,
			": ",
			m.powerPoint
		});
		text += "\n--";
		text2 = text;
		text = string.Concat(new object[]
		{
			text2,
			"\n|5|",
			mResources.clan_capsuledonate,
			": ",
			m.clanPoint
		});
		text2 = text;
		text = string.Concat(new object[]
		{
			text2,
			"\n|5|",
			mResources.clan_capsuleself,
			": ",
			m.curClanPoint
		});
		text2 = text;
		text = string.Concat(new object[]
		{
			text2,
			"\n|4|",
			mResources.give_pea,
			": ",
			m.donate,
			mResources.time
		});
		text2 = text;
		text = string.Concat(new object[]
		{
			text2,
			"\n|4|",
			mResources.receive_pea,
			": ",
			m.receive_donate,
			mResources.time
		});
		text2 = text;
		text = string.Concat(new string[]
		{
			text2,
			"\n|6|",
			mResources.join_date,
			": ",
			m.joinTime
		});
		this.cp = new ChatPopup();
		this.popUpDetailInit(this.cp, text);
		this.partID = new int[]
		{
			(int)m.head,
			(int)m.leg,
			(int)m.body
		};
		this.currItem = null;
		this.charInfo = null;
	}

	// Token: 0x0600089C RID: 2204 RVA: 0x00077874 File Offset: 0x00075C74
	public void addClanDetail(Clan cl)
	{
		try
		{
			string text = "|0|" + cl.name;
			string[] array = mFont.tahoma_7_green.splitFontArray(cl.slogan, this.wScroll - 60);
			for (int i = 0; i < array.Length; i++)
			{
				text = text + "\n|2|" + array[i];
			}
			text += "\n--";
			string text2 = text;
			text = string.Concat(new string[]
			{
				text2,
				"\n|7|",
				mResources.clan_leader,
				": ",
				cl.leaderName
			});
			text2 = text;
			text = string.Concat(new string[]
			{
				text2,
				"\n|1|",
				mResources.power_point,
				": ",
				cl.powerPoint
			});
			text2 = text;
			text = string.Concat(new object[]
			{
				text2,
				"\n|4|",
				mResources.member,
				": ",
				cl.currMember,
				"/",
				cl.maxMember
			});
			text2 = text;
			text = string.Concat(new object[]
			{
				text2,
				"\n|4|",
				mResources.level,
				": ",
				cl.level
			});
			text2 = text;
			text = string.Concat(new string[]
			{
				text2,
				"\n|4|",
				mResources.clan_birthday,
				": ",
				NinjaUtil.getDate(cl.date)
			});
			this.cp = new ChatPopup();
			this.popUpDetailInit(this.cp, text);
			this.idIcon = (int)ClanImage.getClanImage((short)cl.imgID).idImage[0];
			this.currItem = null;
		}
		catch (Exception ex)
		{
			Res.outz("Throw  exception " + ex.StackTrace);
		}
	}

	// Token: 0x0600089D RID: 2205 RVA: 0x00077A78 File Offset: 0x00075E78
	public void addSkillDetail(SkillTemplate tp, Skill skill, Skill nextSkill)
	{
		string text = "|0|" + tp.name;
		for (int i = 0; i < tp.description.Length; i++)
		{
			text = text + "\n|4|" + tp.description[i];
		}
		text += "\n--";
		if (skill != null)
		{
			string text2 = text;
			text = string.Concat(new object[]
			{
				text2,
				"\n|2|",
				mResources.cap_do,
				": ",
				skill.point
			});
			text = text + "\n|5|" + NinjaUtil.replace(tp.damInfo, "#", skill.damage + string.Empty);
			text2 = text;
			text = string.Concat(new object[]
			{
				text2,
				"\n|5|",
				mResources.KI_consume,
				skill.manaUse,
				(tp.manaUseType != 1) ? string.Empty : "%"
			});
			text2 = text;
			text = string.Concat(new string[]
			{
				text2,
				"\n|5|",
				mResources.cooldown,
				": ",
				skill.strTimeReplay(),
				"s"
			});
			text += "\n--";
			if (skill.point == tp.maxPoint)
			{
				text = text + "\n|0|" + mResources.max_level_reach;
			}
			else
			{
				if (!skill.template.isSkillSpec())
				{
					text2 = text;
					text = string.Concat(new string[]
					{
						text2,
						"\n|1|",
						mResources.next_level_require,
						Res.formatNumber(nextSkill.powRequire),
						" ",
						mResources.potential
					});
				}
				text = text + "\n|4|" + NinjaUtil.replace(tp.damInfo, "#", nextSkill.damage + string.Empty);
			}
		}
		else
		{
			text = text + "\n|2|" + mResources.not_learn;
			string text2 = text;
			text = string.Concat(new string[]
			{
				text2,
				"\n|1|",
				mResources.learn_require,
				Res.formatNumber(nextSkill.powRequire),
				" ",
				mResources.potential
			});
			text = text + "\n|4|" + NinjaUtil.replace(tp.damInfo, "#", nextSkill.damage + string.Empty);
			text2 = text;
			text = string.Concat(new object[]
			{
				text2,
				"\n|4|",
				mResources.KI_consume,
				nextSkill.manaUse,
				(tp.manaUseType != 1) ? string.Empty : "%"
			});
			text2 = text;
			text = string.Concat(new string[]
			{
				text2,
				"\n|4|",
				mResources.cooldown,
				": ",
				nextSkill.strTimeReplay(),
				"s"
			});
		}
		this.currItem = null;
		this.partID = null;
		this.charInfo = null;
		this.cp = new ChatPopup();
		this.popUpDetailInit(this.cp, text);
		this.idIcon = 0;
	}

	// Token: 0x0600089E RID: 2206 RVA: 0x00077DC0 File Offset: 0x000761C0
	public void show()
	{
		if (GameCanvas.isTouch)
		{
			this.cmdClose.x = 156;
			this.cmdClose.y = 3;
		}
		else
		{
			this.cmdClose.x = GameCanvas.w - 19;
			this.cmdClose.y = GameCanvas.h - 19;
		}
		this.cmdClose.isPlaySoundButton = false;
		ChatPopup.currChatPopup = null;
		InfoDlg.hide();
		this.timeShow = 20;
		this.isShow = true;
		this.isClose = false;
		SoundMn.gI().panelOpen();
		if (this.isTypeShop())
		{
			global::Char.myCharz().setPartOld();
		}
	}

	// Token: 0x0600089F RID: 2207 RVA: 0x00077E6C File Offset: 0x0007626C
	public void chatTFUpdateKey()
	{
		if (this.chatTField != null && this.chatTField.isShow)
		{
			if (this.chatTField.left != null && (GameCanvas.keyPressed[12] || mScreen.getCmdPointerLast(this.chatTField.left)) && this.chatTField.left != null)
			{
				this.chatTField.left.performAction();
			}
			if (this.chatTField.right != null && (GameCanvas.keyPressed[13] || mScreen.getCmdPointerLast(this.chatTField.right)) && this.chatTField.right != null)
			{
				this.chatTField.right.performAction();
			}
			if (this.chatTField.center != null && (GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] || mScreen.getCmdPointerLast(this.chatTField.center)) && this.chatTField.center != null)
			{
				this.chatTField.center.performAction();
			}
			if (this.chatTField.isShow && GameCanvas.keyAsciiPress != 0)
			{
				this.chatTField.keyPressed(GameCanvas.keyAsciiPress);
				GameCanvas.keyAsciiPress = 0;
			}
			GameCanvas.clearKeyHold();
			GameCanvas.clearKeyPressed();
			return;
		}
	}

	// Token: 0x060008A0 RID: 2208 RVA: 0x00077FD8 File Offset: 0x000763D8
	public void updateKey()
	{
		if (this.chatTField != null && this.chatTField.isShow)
		{
			return;
		}
		if (!GameCanvas.panel.isDoneCombine)
		{
			return;
		}
		if (InfoDlg.isShow)
		{
			return;
		}
		if (this.tabIcon != null && this.tabIcon.isShow)
		{
			this.tabIcon.updateKey();
			return;
		}
		if (this.isClose)
		{
			return;
		}
		if (!this.isShow)
		{
			return;
		}
		if (this.cmdClose.isPointerPressInside())
		{
			this.cmdClose.performAction();
			return;
		}
		if (GameCanvas.keyPressed[13])
		{
			if (this.type != 4)
			{
				this.hide();
				return;
			}
			this.setTypeMain();
			this.cmx = (this.cmtoX = 0);
		}
		if (GameCanvas.keyPressed[12] || GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25])
		{
			if (this.left.idAction > 0)
			{
				this.perform(this.left.idAction, this.left.p);
			}
			else
			{
				this.waitToPerform = 2;
			}
		}
		if (this.Equals(GameCanvas.panel) && GameCanvas.panel2 == null && GameCanvas.isPointerJustRelease && !GameCanvas.isPointer(this.X, this.Y, this.W, this.H) && !this.pointerIsDowning)
		{
			this.hide();
			return;
		}
		if (!this.isClanOption)
		{
			this.updateKeyInTabBar();
		}
		switch (this.type)
		{
			case 0:
				if (this.currentTabIndex == 0)
				{
					this.updateKeyQuest();
					GameCanvas.clearKeyPressed();
					return;
				}
				if (this.currentTabIndex == 1)
				{
					this.updateKeyInventory();
				}
				if (this.currentTabIndex == 2)
				{
					this.updateKeySkill();
				}
				if (this.currentTabIndex == 3)
				{
					if (this.mainTabName.Length == 4)
					{
						this.updateKeyTool();
					}
					else
					{
						this.updateKeyClans();
					}
				}
				if (this.currentTabIndex == 4)
				{
					this.updateKeyTool();
				}
				break;
			case 1:
			case 17:
			case 25:
				if (this.currentTabIndex < this.currentTabName.Length - ((GameCanvas.panel2 == null) ? 1 : 0) && this.type != 17)
				{
					this.updateKeyScrollView();
				}
				else if (this.typeShop == 0)
				{
					this.updateKeyInventory();
				}
				else
				{
					this.updateKeyScrollView();
				}
				break;
			case 2:
				this.updateKeyInventory();
				break;
			case 3:
				this.updateKeyScrollView();
				break;
			case 4:
				this.updateKeyMap();
				GameCanvas.clearKeyPressed();
				return;
			case 7:
				this.updateKeyInventory();
				break;
			case 8:
				this.updateKeyScrollView();
				break;
			case 9:
				this.updateKeyScrollView();
				break;
			case 10:
				this.updateKeyScrollView();
				break;
			case 11:
			case 16:
				this.updateKeyScrollView();
				break;
			case 12:
				this.updateKeyCombine();
				break;
			case 13:
				this.updateKeyGiaoDich();
				break;
			case 14:
				this.updateKeyScrollView();
				break;
			case 15:
				this.updateKeyScrollView();
				break;
			case 18:
				this.updateKeyScrollView();
				break;
			case 19:
				this.updateKeyOption();
				break;
			case 20:
				this.updateKeyOption();
				break;
			case 21:
				if (this.currentTabIndex == 0)
				{
					this.updateKeyScrollView();
				}
				if (this.currentTabIndex == 1)
				{
					this.updateKeyPetStatus();
				}
				if (this.currentTabIndex == 2)
				{
					this.updateKeyScrollView();
				}
				break;
			case 22:
				this.updateKeyAuto();
				break;
			case 23:
			case 24:
				this.updateKeyScrollView();
				break;
		}
		GameCanvas.clearKeyHold();
		for (int i = 0; i < GameCanvas.keyPressed.Length; i++)
		{
			GameCanvas.keyPressed[i] = false;
		}
	}

	// Token: 0x060008A1 RID: 2209 RVA: 0x000783F3 File Offset: 0x000767F3
	private void updateKeyAuto()
	{
	}

	// Token: 0x060008A2 RID: 2210 RVA: 0x000783F5 File Offset: 0x000767F5
	private void updateKeyPetStatus()
	{
		this.updateKeyScrollView();
	}

	// Token: 0x060008A3 RID: 2211 RVA: 0x000783FD File Offset: 0x000767FD
	private void updateKeyPetSkill()
	{
	}

	// Token: 0x060008A4 RID: 2212 RVA: 0x000783FF File Offset: 0x000767FF
	private void keyGiaodich()
	{
		this.updateKeyScrollView();
	}

	// Token: 0x060008A5 RID: 2213 RVA: 0x00078408 File Offset: 0x00076808
	private void updateKeyGiaoDich()
	{
		if (this.currentTabIndex == 0)
		{
			if (this.Equals(GameCanvas.panel))
			{
				this.updateKeyInventory();
			}
			if (this.Equals(GameCanvas.panel2))
			{
				this.keyGiaodich();
			}
		}
		if (this.currentTabIndex == 1 || this.currentTabIndex == 2)
		{
			this.keyGiaodich();
		}
	}

	// Token: 0x060008A6 RID: 2214 RVA: 0x0007846A File Offset: 0x0007686A
	private void updateKeyTool()
	{
		this.updateKeyScrollView();
	}

	// Token: 0x060008A7 RID: 2215 RVA: 0x00078472 File Offset: 0x00076872
	private void updateKeySkill()
	{
		this.updateKeyScrollView();
	}

	// Token: 0x060008A8 RID: 2216 RVA: 0x0007847A File Offset: 0x0007687A
	private void updateKeyClanIcon()
	{
		this.updateKeyScrollView();
	}

	// Token: 0x060008A9 RID: 2217 RVA: 0x00078484 File Offset: 0x00076884
	public void setTabGiaoDich(bool isMe)
	{
		this.currentListLength = ((!isMe) ? (this.vFriendGD.size() + 3) : (this.vMyGD.size() + 3));
		this.ITEM_HEIGHT = 24;
		this.selected = ((!GameCanvas.isTouch) ? 0 : -1);
		this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
		if (this.cmyLim < 0)
		{
			this.cmyLim = 0;
		}
		this.cmy = (this.cmtoY = this.cmyLast[this.currentTabIndex]);
		if (this.cmy < 0)
		{
			this.cmy = (this.cmtoY = 0);
		}
		if (this.cmy > this.cmyLim)
		{
			this.cmy = (this.cmtoY = this.cmyLim);
		}
	}

	// Token: 0x060008AA RID: 2218 RVA: 0x00078568 File Offset: 0x00076968
	public void setTypeGiaoDich(global::Char cGD)
	{
		this.type = 13;
		this.tabName[this.type] = Panel.boxGD;
		this.isAccept = false;
		this.isLock = false;
		this.isFriendLock = false;
		this.vMyGD.removeAllElements();
		this.vFriendGD.removeAllElements();
		this.moneyGD = 0;
		this.friendMoneyGD = 0;
		if (GameCanvas.w > 2 * Panel.WIDTH_PANEL)
		{
			GameCanvas.panel2 = new Panel();
			GameCanvas.panel2.type = 13;
			GameCanvas.panel2.tabName[this.type] = new string[][]
			{
				mResources.item_receive
			};
			GameCanvas.panel2.setType(1);
			GameCanvas.panel2.setTabGiaoDich(false);
			GameCanvas.panel.tabName[this.type] = new string[][]
			{
				mResources.inventory,
				mResources.item_give
			};
			GameCanvas.panel2.show();
			GameCanvas.panel2.charMenu = cGD;
		}
		if (this.Equals(GameCanvas.panel))
		{
			this.setType(0);
		}
		if (this.currentTabIndex == 0)
		{
			this.setTabInventory(true);
		}
		if (this.currentTabIndex == 1)
		{
			this.setTabGiaoDich(true);
		}
		if (this.currentTabIndex == 2)
		{
			this.setTabGiaoDich(false);
		}
		this.charMenu = cGD;
	}

	// Token: 0x060008AB RID: 2219 RVA: 0x000786B8 File Offset: 0x00076AB8
	private void paintGiaoDich(mGraphics g, bool isMe)
	{
		g.setColor(16711680);
		g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
		g.translate(0, -this.cmy);
		MyVector myVector = (!isMe) ? this.vFriendGD : this.vMyGD;
		for (int i = 0; i < this.currentListLength; i++)
		{
			int num = this.xScroll + 36;
			int num2 = this.yScroll + i * this.ITEM_HEIGHT;
			int num3 = this.wScroll - 36;
			int num4 = this.ITEM_HEIGHT - 1;
			int num5 = this.xScroll;
			int num6 = this.yScroll + i * this.ITEM_HEIGHT;
			int num7 = 34;
			int num8 = this.ITEM_HEIGHT - 1;
			if (num2 - this.cmy <= this.yScroll + this.hScroll)
			{
				if (num2 - this.cmy >= this.yScroll - this.ITEM_HEIGHT)
				{
					if (i == this.currentListLength - 1)
					{
						if (isMe)
						{
							g.setColor(15196114);
							g.fillRect(num5, num2, this.wScroll, num4);
							if (!this.isLock)
							{
								if (!this.isFriendLock)
								{
									mFont.tahoma_7_grey.drawString(g, mResources.opponent + mResources.not_lock_trade, this.xScroll + this.wScroll / 2, num2 + num4 / 2 - 4, mFont.CENTER);
								}
								else
								{
									mFont.tahoma_7_grey.drawString(g, mResources.opponent + mResources.locked_trade, this.xScroll + this.wScroll / 2, num2 + num4 / 2 - 4, mFont.CENTER);
								}
							}
							else if (this.isFriendLock)
							{
								g.setColor(15196114);
								g.fillRect(num5, num2, this.wScroll, num4);
								g.drawImage((i != this.selected) ? GameScr.imgLbtn2 : GameScr.imgLbtnFocus2, this.xScroll + this.wScroll - 5, num2 + 2, StaticObj.TOP_RIGHT);
								((i != this.selected) ? mFont.tahoma_7b_dark : mFont.tahoma_7b_green2).drawString(g, mResources.done, this.xScroll + this.wScroll - 22, num2 + 7, 2);
								mFont.tahoma_7_grey.drawString(g, mResources.opponent + mResources.locked_trade, this.xScroll + 5, num2 + num4 / 2 - 4, mFont.LEFT);
							}
							else
							{
								mFont.tahoma_7_grey.drawString(g, mResources.opponent + mResources.not_lock_trade, this.xScroll + this.wScroll / 2, num2 + num4 / 2 - 4, mFont.CENTER);
							}
						}
					}
					else if (i == this.currentListLength - 2)
					{
						if (isMe)
						{
							g.setColor(15196114);
							g.fillRect(num5, num2, this.wScroll, num4);
							if (!this.isAccept)
							{
								if (!this.isLock)
								{
									g.drawImage((i != this.selected) ? GameScr.imgLbtn2 : GameScr.imgLbtnFocus2, this.xScroll + this.wScroll - 5, num2 + 2, StaticObj.TOP_RIGHT);
									((i != this.selected) ? mFont.tahoma_7b_dark : mFont.tahoma_7b_green2).drawString(g, mResources.mlock, this.xScroll + this.wScroll - 22, num2 + 7, 2);
									mFont.tahoma_7_grey.drawString(g, mResources.you + mResources.not_lock_trade, this.xScroll + 5, num2 + num4 / 2 - 4, mFont.LEFT);
								}
								else
								{
									g.drawImage((i != this.selected) ? GameScr.imgLbtn2 : GameScr.imgLbtnFocus2, this.xScroll + this.wScroll - 5, num2 + 2, StaticObj.TOP_RIGHT);
									((i != this.selected) ? mFont.tahoma_7b_dark : mFont.tahoma_7b_green2).drawString(g, mResources.CANCEL, this.xScroll + this.wScroll - 22, num2 + 7, 2);
									mFont.tahoma_7_grey.drawString(g, mResources.you + mResources.locked_trade, this.xScroll + 5, num2 + num4 / 2 - 4, mFont.LEFT);
								}
							}
						}
						else if (!this.isFriendLock)
						{
							mFont.tahoma_7b_dark.drawString(g, mResources.not_lock_trade_upper, this.xScroll + this.wScroll / 2, num2 + num4 / 2 - 4, mFont.CENTER);
						}
						else
						{
							mFont.tahoma_7b_dark.drawString(g, mResources.locked_trade_upper, this.xScroll + this.wScroll / 2, num2 + num4 / 2 - 4, mFont.CENTER);
						}
					}
					else if (i == this.currentListLength - 3)
					{
						if (this.isLock)
						{
							g.setColor(13748667);
						}
						else
						{
							g.setColor((i != this.selected) ? 15196114 : 16383818);
						}
						g.fillRect(num, num2, num3, num4);
						if (this.isLock)
						{
							g.setColor(13748667);
						}
						else
						{
							g.setColor((i != this.selected) ? 9993045 : 7300181);
						}
						g.fillRect(num5, num6, num7, num8);
						g.drawImage(Panel.imgXu, num5 + num7 / 2, num6 + num8 / 2, 3);
						mFont.tahoma_7_green2.drawString(g, NinjaUtil.getMoneys((long)((!isMe) ? this.friendMoneyGD : this.moneyGD)) + " " + mResources.XU, num + 5, num2 + 11, 0);
						mFont.tahoma_7_green.drawString(g, mResources.money_trade, num + 5, num2, 0);
					}
					else
					{
						if (myVector.size() == 0)
						{
							return;
						}
						if (this.isLock)
						{
							g.setColor(13748667);
						}
						else
						{
							g.setColor((i != this.selected) ? 15196114 : 16383818);
						}
						g.fillRect(num, num2, num3, num4);
						if (this.isLock)
						{
							g.setColor(13748667);
						}
						else
						{
							g.setColor((i != this.selected) ? 9993045 : 9541120);
						}
						Item item = (Item)myVector.elementAt(i);
						if (item != null)
						{
							for (int j = 0; j < item.itemOption.Length; j++)
							{
								if (item.itemOption[j].optionTemplate.id == 72 && item.itemOption[j].param > 0)
								{
									sbyte color_Item_Upgrade = Panel.GetColor_Item_Upgrade(item.itemOption[j].param);
									int color_ItemBg = Panel.GetColor_ItemBg((int)color_Item_Upgrade);
									if (color_ItemBg != -1)
									{
										if (this.isLock)
										{
											g.setColor(13748667);
										}
										else
										{
											g.setColor((i != this.selected) ? Panel.GetColor_ItemBg((int)color_Item_Upgrade) : Panel.GetColor_ItemBg((int)color_Item_Upgrade));
										}
									}
								}
							}
						}
						g.fillRect(num5, num6, num7, num8);
						if (item != null)
						{
							string str = string.Empty;
							mFont mFont = mFont.tahoma_7_green2;
							if (item.itemOption != null)
							{
								for (int k = 0; k < item.itemOption.Length; k++)
								{
									if (item.itemOption[k].optionTemplate.id == 72)
									{
										str = " [+" + item.itemOption[k].param + "]";
										item.paintColorItem(g, num5 + 18, num6 + 12, item.itemOption[k].param);
									}

									else if (item.itemOption[k].optionTemplate.id == 107)
									{

										mFont.tahoma_7b_red.drawString(g, item.itemOption[k].param.ToString(), num + 121, num2 - 1, 0);
									}
									if (item.itemOption[k].optionTemplate.id == 41)
									{
										if (item.itemOption[k].param == 1)
										{
											mFont = Panel.GetFont(0);
										}
										else if (item.itemOption[k].param == 2)
										{
											mFont = Panel.GetFont(2);
										}
										else if (item.itemOption[k].param == 3)
										{
											mFont = Panel.GetFont(8);
										}
										else if (item.itemOption[k].param == 4)
										{
											mFont = Panel.GetFont(7);
										}
									}
								}
							}
							mFont.drawString(g, item.template.name + str, num + 5, num2 + 1, 0);
							string text = string.Empty;
							if (item.itemOption != null)
							{
								if (item.itemOption.Length > 0 && item.itemOption[0] != null)
								{
									text += item.itemOption[0].getOptionString();
								}
								mFont mFont2 = mFont.tahoma_7_blue;
								if (item.compare < 0 && (int)item.template.type != 5)
								{
									mFont2 = mFont.tahoma_7_red;
								}
								if (item.itemOption.Length > 1)
								{
									for (int l = 1; l < item.itemOption.Length; l++)
									{
										if (item.itemOption[l] != null && item.itemOption[l].optionTemplate.id != 102 && item.itemOption[l].optionTemplate.id != 107)
										{
											text = text + "," + item.itemOption[l].getOptionString();
										}
									}
								}
								mFont2.drawString(g, text, num + 5, num2 + 11, mFont.LEFT);
							}
							SmallImage.drawSmallImage(g, (int)item.template.iconID, num5 + num7 / 2, num6 + num8 / 2, 0, 3);
							if (item.itemOption != null)
							{
								for (int m = 0; m < item.itemOption.Length; m++)
								{
									this.paintOptItem(g, item.itemOption[m].optionTemplate.id, item.itemOption[m].param, num5, num6, num7, num8);
								}
								for (int n = 0; n < item.itemOption.Length; n++)
								{
									this.paintOptSlotItem(g, item.itemOption[n].optionTemplate.id, item.itemOption[n].param, num5, num6, num7, num8);
								}
							}
							if (item.quantity > 1)
							{
								mFont.tahoma_7_yellow.drawString(g, string.Empty + item.quantity, num5 + num7, num6 + num8 - mFont.tahoma_7_yellow.getHeight(), 1);
							}
						}
					}
				}
			}
		}
		this.paintScrollArrow(g);
	}

	// Token: 0x060008AC RID: 2220 RVA: 0x00079178 File Offset: 0x00077578
	private void updateKeyMap()
	{
		if (GameCanvas.keyHold[(!Main.isPC) ? 2 : 21])
		{
			this.yMove -= 5;
			this.cmyMap = this.yMove - (this.yScroll + this.hScroll / 2);
			if (this.yMove < this.yScroll)
			{
				this.yMove = this.yScroll;
			}
		}
		if (GameCanvas.keyHold[(!Main.isPC) ? 8 : 22])
		{
			this.yMove += 5;
			this.cmyMap = this.yMove - (this.yScroll + this.hScroll / 2);
			if (this.yMove > this.yScroll + 200)
			{
				this.yMove = this.yScroll + 200;
			}
		}
		if (GameCanvas.keyHold[(!Main.isPC) ? 4 : 23])
		{
			this.xMove -= 5;
			this.cmxMap = this.xMove - this.wScroll / 2;
			if (this.xMove < 16)
			{
				this.xMove = 16;
			}
		}
		if (GameCanvas.keyHold[(!Main.isPC) ? 6 : 24])
		{
			this.xMove += 5;
			this.cmxMap = this.xMove - this.wScroll / 2;
			if (this.xMove > 250)
			{
				this.xMove = 250;
			}
		}
		if (GameCanvas.isPointerDown)
		{
			this.pointerIsDowning = true;
			if (!this.trans)
			{
				this.pa1 = this.cmxMap;
				this.pa2 = this.cmyMap;
				this.trans = true;
			}
			this.cmxMap = this.pa1 + (GameCanvas.pxLast - GameCanvas.px);
			this.cmyMap = this.pa2 + (GameCanvas.pyLast - GameCanvas.py);
		}
		if (GameCanvas.isPointerJustRelease)
		{
			this.trans = false;
			GameCanvas.pxLast = GameCanvas.px;
			GameCanvas.pyLast = GameCanvas.py;
			this.pX = GameCanvas.pxLast + this.cmxMap;
			this.pY = GameCanvas.pyLast + this.cmyMap;
		}
		if (GameCanvas.isPointerClick)
		{
			this.pointerIsDowning = false;
		}
		if (this.cmxMap < 0)
		{
			this.cmxMap = 0;
		}
		if (this.cmxMap > this.cmxMapLim)
		{
			this.cmxMap = this.cmxMapLim;
		}
		if (this.cmyMap < 0)
		{
			this.cmyMap = 0;
		}
		if (this.cmyMap > this.cmyMapLim)
		{
			this.cmyMap = this.cmyMapLim;
		}
	}

	// Token: 0x060008AD RID: 2221 RVA: 0x00079430 File Offset: 0x00077830
	private void updateKeyCombine()
	{
		if (this.currentTabIndex == 0)
		{
			this.updateKeyScrollView();
			this.keyTouchCombine = -1;
			if (this.selected == this.vItemCombine.size() && GameCanvas.isPointerClick)
			{
				GameCanvas.isPointerClick = false;
				this.keyTouchCombine = 1;
			}
		}
		if (this.currentTabIndex == 1)
		{
			this.updateKeyScrollView();
		}
	}

	// Token: 0x060008AE RID: 2222 RVA: 0x00079494 File Offset: 0x00077894
	private void updateKeyQuest()
	{
		if (GameCanvas.keyHold[(!Main.isPC) ? 2 : 21])
		{
			this.cmyQuest -= 5;
		}
		if (GameCanvas.keyHold[(!Main.isPC) ? 8 : 22])
		{
			this.cmyQuest += 5;
		}
		if (this.cmyQuest < 0)
		{
			this.cmyQuest = 0;
		}
		int num = this.indexRowMax * 12 - (this.hScroll - 60);
		if (num < 0)
		{
			num = 0;
		}
		if (this.cmyQuest > num)
		{
			this.cmyQuest = num;
		}
		if (this.scroll != null)
		{
			if (!GameCanvas.isTouch)
			{
				this.scroll.cmy = this.cmyQuest;
			}
			this.scroll.updateKey();
		}
		int num2 = this.xScroll + this.wScroll / 2 - 35;
		int num3 = (GameCanvas.h <= 300) ? 15 : 20;
		int num4 = this.yScroll + this.hScroll - num3 - 15;
		int px = GameCanvas.px;
		int py = GameCanvas.py;
		this.keyTouchMapButton = -1;
		if (Panel.isPaintMap && !GameScr.gI().isMapDocNhan() && px >= num2 && px <= num2 + 70 && py >= num4 && py <= num4 + 30)
		{
			if (this.scroll != null && this.scroll.pointerIsDowning)
			{
				return;
			}
			this.keyTouchMapButton = 1;
			if (GameCanvas.isPointerJustRelease)
			{
				SoundMn.gI().buttonClick();
				this.waitToPerform = 2;
				GameCanvas.clearAllPointerEvent();
			}
		}
	}

	// Token: 0x060008AF RID: 2223 RVA: 0x00079644 File Offset: 0x00077A44
	private void getCurrClanOtion()
	{
		this.isClanOption = false;
		if (this.type == 0 && this.mainTabName.Length == 5 && this.currentTabIndex == 3)
		{
			this.isClanOption = false;
			if (this.selected == 0)
			{
				this.currClanOption = new int[this.clansOption.Length];
				for (int i = 0; i < this.currClanOption.Length; i++)
				{
					this.currClanOption[i] = i;
				}
				if (!this.isViewMember)
				{
					this.isClanOption = true;
				}
			}
			else if (this.selected != 1)
			{
				if (this.isSearchClan)
				{
					return;
				}
				if (this.selected > 0)
				{
					this.currClanOption = new int[1];
					for (int j = 0; j < this.currClanOption.Length; j++)
					{
						this.currClanOption[j] = j;
					}
					this.isClanOption = true;
				}
			}
		}
	}

	// Token: 0x060008B0 RID: 2224 RVA: 0x0007973C File Offset: 0x00077B3C
	private void updateKeyClansOption()
	{
		if (this.currClanOption == null)
		{
			return;
		}
		if (GameCanvas.keyPressed[(!Main.isPC) ? 4 : 23])
		{
			this.currMess = this.getCurrMessage();
			this.cSelected--;
			if (this.selected == 0 && this.cSelected < 0)
			{
				this.cSelected = this.currClanOption.Length - 1;
			}
			if (this.selected > 1 && this.isMessage && this.currMess.option != null && this.cSelected < 0)
			{
				this.cSelected = this.currMess.option.Length - 1;
			}
		}
		else if (GameCanvas.keyPressed[(!Main.isPC) ? 6 : 24])
		{
			this.currMess = this.getCurrMessage();
			this.cSelected++;
			if (this.selected == 0 && this.cSelected > this.currClanOption.Length - 1)
			{
				this.cSelected = 0;
			}
			if (this.selected > 1 && this.isMessage && this.currMess.option != null && this.cSelected > this.currMess.option.Length - 1)
			{
				this.cSelected = 0;
			}
		}
	}

	// Token: 0x060008B1 RID: 2225 RVA: 0x000798A6 File Offset: 0x00077CA6
	private void updateKeyClans()
	{
		this.updateKeyScrollView();
		this.updateKeyClansOption();
	}

	// Token: 0x060008B2 RID: 2226 RVA: 0x000798B4 File Offset: 0x00077CB4
	private void checkOptionSelect()
	{
		try
		{
			if (this.type == 0 && this.currentTabIndex == 3 && this.mainTabName.Length == 5)
			{
				if (this.selected != -1)
				{
					int num = 0;
					if (this.selected == 0)
					{
						num = this.xScroll + this.wScroll / 2 - this.clansOption.Length * this.TAB_W / 2;
						this.cSelected = (GameCanvas.px - num) / this.TAB_W;
					}
					else
					{
						this.currMess = this.getCurrMessage();
						if (this.currMess != null && this.currMess.option != null)
						{
							num = this.xScroll + this.wScroll - 2 - this.currMess.option.Length * 40;
							this.cSelected = (GameCanvas.px - num) / 40;
						}
					}
					if (GameCanvas.px < num)
					{
						this.cSelected = -1;
					}
				}
			}
		}
		catch (Exception ex)
		{
			Res.outz("Throw err " + ex.StackTrace);
		}
	}

	// Token: 0x060008B3 RID: 2227 RVA: 0x000799DC File Offset: 0x00077DDC
	public void updateScroolMouse(int a)
	{
		bool flag = false;
		if (GameCanvas.pxMouse > this.wScroll)
		{
			return;
		}
		if (this.indexMouse == -1)
		{
			this.indexMouse = this.selected;
		}
		if (a > 0)
		{
			this.indexMouse -= a;
			flag = true;
		}
		else if (a < 0)
		{
			this.indexMouse += -a;
			flag = true;
		}
		if (this.indexMouse < 0)
		{
			this.indexMouse = 0;
		}
		if (!flag)
		{
			return;
		}
		this.cmtoY = this.indexMouse * 12;
		if (this.cmtoY > this.cmyLim)
		{
			this.cmtoY = this.cmyLim;
		}
		if (this.cmtoY < 0)
		{
			this.cmtoY = 0;
		}
	}

	// Token: 0x060008B4 RID: 2228 RVA: 0x00079AA4 File Offset: 0x00077EA4
	private void updateKeyScrollView()
	{
		if (this.currentListLength <= 0)
		{
			return;
		}
		bool flag = false;
		if (GameCanvas.keyPressed[(!Main.isPC) ? 2 : 21])
		{
			flag = true;
			this.selected--;
			if (this.type == 24)
			{
				this.selected -= 2;
				if (this.selected < 0)
				{
					this.selected = 0;
				}
			}
			else if (this.selected < 0)
			{
				if (this.Equals(GameCanvas.panel) && this.typeShop == 2 && this.currentTabIndex <= 3 && this.maxPageShop[this.currentTabIndex] > 1)
				{
					InfoDlg.showWait();
					if (this.currPageShop[this.currentTabIndex] <= 0)
					{
						Service.gI().kigui(4, -1, (sbyte)this.currentTabIndex, this.maxPageShop[this.currentTabIndex] - 1, -1);
					}
					else
					{
						Service.gI().kigui(4, -1, (sbyte)this.currentTabIndex, this.currPageShop[this.currentTabIndex] - 1, -1);
					}
					return;
				}
				this.selected = this.currentListLength - 1;
				if (this.isClanOption)
				{
					this.selected = -1;
				}
				if ((int)this.size_tab > 0)
				{
					this.selected = -1;
				}
			}
			this.lastSelect[this.currentTabIndex] = this.selected;
			this.cSelected = 0;
			this.getCurrClanOtion();
		}
		else if (GameCanvas.keyPressed[(!Main.isPC) ? 8 : 22])
		{
			flag = true;
			this.selected++;
			if (this.type == 24)
			{
				this.selected += 2;
				if (this.selected > this.currentListLength - 1)
				{
					this.selected = this.currentListLength - 1;
				}
			}
			else if (this.selected > this.currentListLength - 1)
			{
				if (this.Equals(GameCanvas.panel) && this.typeShop == 2 && this.currentTabIndex <= 3 && this.maxPageShop[this.currentTabIndex] > 1)
				{
					InfoDlg.showWait();
					if (this.currPageShop[this.currentTabIndex] >= this.maxPageShop[this.currentTabIndex] - 1)
					{
						Service.gI().kigui(4, -1, (sbyte)this.currentTabIndex, 0, -1);
					}
					else
					{
						Service.gI().kigui(4, -1, (sbyte)this.currentTabIndex, this.currPageShop[this.currentTabIndex] + 1, -1);
					}
					return;
				}
				this.selected = 0;
			}
			this.lastSelect[this.currentTabIndex] = this.selected;
			this.cSelected = 0;
			this.getCurrClanOtion();
		}
		if (flag)
		{
			this.cmtoY = this.selected * this.ITEM_HEIGHT - this.hScroll / 2;
			if (this.cmtoY > this.cmyLim)
			{
				this.cmtoY = this.cmyLim;
			}
			if (this.cmtoY < 0)
			{
				this.cmtoY = 0;
			}
			this.cmy = this.cmtoY;
		}
		if (GameCanvas.isPointerDown)
		{
			this.justRelease = false;
			if (!this.pointerIsDowning && GameCanvas.isPointer(this.xScroll, this.yScroll, this.wScroll, this.hScroll))
			{
				for (int i = 0; i < this.pointerDownLastX.Length; i++)
				{
					this.pointerDownLastX[0] = GameCanvas.py;
				}
				this.pointerDownFirstX = GameCanvas.py;
				this.pointerIsDowning = true;
				this.isDownWhenRunning = (this.cmRun != 0);
				this.cmRun = 0;
			}
			else if (this.pointerIsDowning)
			{
				this.pointerDownTime++;
				if (this.pointerDownTime > 5 && this.pointerDownFirstX == GameCanvas.py && !this.isDownWhenRunning)
				{
					this.pointerDownFirstX = -1000;
					this.selected = (this.cmtoY + GameCanvas.py - this.yScroll) / this.ITEM_HEIGHT;
					if (this.selected >= this.currentListLength)
					{
						this.selected = -1;
					}
					this.checkOptionSelect();
				}
				else
				{
					this.indexMouse = -1;
				}
				int num = GameCanvas.py - this.pointerDownLastX[0];
				if (num != 0 && this.selected != -1)
				{
					this.selected = -1;
					this.cSelected = -1;
				}
				for (int j = this.pointerDownLastX.Length - 1; j > 0; j--)
				{
					this.pointerDownLastX[j] = this.pointerDownLastX[j - 1];
				}
				this.pointerDownLastX[0] = GameCanvas.py;
				this.cmtoY -= num;
				if (this.cmtoY < 0)
				{
					this.cmtoY = 0;
				}
				if (this.cmtoY > this.cmyLim)
				{
					this.cmtoY = this.cmyLim;
				}
				if (this.cmy < 0 || this.cmy > this.cmyLim)
				{
					num /= 2;
				}
				this.cmy -= num;
				if (this.cmy < -(GameCanvas.h / 3))
				{
					this.wantUpdateList = true;
				}
				else
				{
					this.wantUpdateList = false;
				}
			}
		}
		if (GameCanvas.isPointerJustRelease && this.pointerIsDowning)
		{
			this.justRelease = true;
			int i2 = GameCanvas.py - this.pointerDownLastX[0];
			GameCanvas.isPointerJustRelease = false;
			if (Res.abs(i2) < 20 && Res.abs(GameCanvas.py - this.pointerDownFirstX) < 20 && !this.isDownWhenRunning)
			{
				this.cmRun = 0;
				this.cmtoY = this.cmy;
				this.pointerDownFirstX = -1000;
				this.selected = (this.cmtoY + GameCanvas.py - this.yScroll) / this.ITEM_HEIGHT;
				if (this.selected >= this.currentListLength)
				{
					this.selected = -1;
				}
				this.checkOptionSelect();
				this.pointerDownTime = 0;
				this.waitToPerform = 10;
				SoundMn.gI().panelClick();
			}
			else if (this.selected != -1 && this.pointerDownTime > 5)
			{
				this.pointerDownTime = 0;
				this.waitToPerform = 1;
			}
			else if (this.selected == -1 && !this.isDownWhenRunning)
			{
				if (this.cmy < 0)
				{
					this.cmtoY = 0;
				}
				else if (this.cmy > this.cmyLim)
				{
					this.cmtoY = this.cmyLim;
				}
				else
				{
					int num2 = GameCanvas.py - this.pointerDownLastX[0] + (this.pointerDownLastX[0] - this.pointerDownLastX[1]) + (this.pointerDownLastX[1] - this.pointerDownLastX[2]);
					if (num2 > 10)
					{
						num2 = 10;
					}
					else if (num2 < -10)
					{
						num2 = -10;
					}
					else
					{
						num2 = 0;
					}
					this.cmRun = -num2 * 100;
				}
			}
			if (this.isTabInven() && GameCanvas.py < this.yScroll + 21)
			{
				this.selected = 0;
				this.updateKeyInvenTab();
			}
			this.pointerIsDowning = false;
			this.pointerDownTime = 0;
			GameCanvas.isPointerJustRelease = false;
		}
	}

	// Token: 0x060008B5 RID: 2229 RVA: 0x0007A1F0 File Offset: 0x000785F0
	public string subArray(string[] str)
	{
		return null;
	}

	// Token: 0x060008B6 RID: 2230 RVA: 0x0007A1F4 File Offset: 0x000785F4
	private void updateKeyInTabBar()
	{
		if (this.scroll != null && this.scroll.pointerIsDowning)
		{
			return;
		}
		if (this.pointerIsDowning)
		{
			return;
		}
		int num = this.currentTabIndex;
		if (!this.IsTabOption())
		{
			if (GameCanvas.keyPressed[(!Main.isPC) ? 6 : 24])
			{
				if (this.isTabInven())
				{
					if (this.selected >= 0)
					{
						this.updateKeyInvenTab();
					}
					else
					{
						this.currentTabIndex++;
						if (this.currentTabIndex >= this.currentTabName.Length)
						{
							if (GameCanvas.panel2 != null)
							{
								this.currentTabIndex = this.currentTabName.Length - 1;
								GameCanvas.isFocusPanel2 = true;
							}
							else
							{
								this.currentTabIndex = 0;
							}
						}
						this.selected = this.lastSelect[this.currentTabIndex];
						this.lastTabIndex[this.type] = this.currentTabIndex;
					}
				}
				else
				{
					this.currentTabIndex++;
					if (this.currentTabIndex >= this.currentTabName.Length)
					{
						if (GameCanvas.panel2 != null)
						{
							this.currentTabIndex = this.currentTabName.Length - 1;
							GameCanvas.isFocusPanel2 = true;
						}
						else
						{
							this.currentTabIndex = 0;
						}
					}
					this.selected = this.lastSelect[this.currentTabIndex];
					this.lastTabIndex[this.type] = this.currentTabIndex;
				}
			}
			if (GameCanvas.keyPressed[(!Main.isPC) ? 4 : 23])
			{
				this.currentTabIndex--;
				if (this.currentTabIndex < 0)
				{
					this.currentTabIndex = this.currentTabName.Length - 1;
				}
				if (GameCanvas.isFocusPanel2)
				{
					GameCanvas.isFocusPanel2 = false;
				}
				this.selected = this.lastSelect[this.currentTabIndex];
				this.lastTabIndex[this.type] = this.currentTabIndex;
			}
		}
		this.keyTouchTab = -1;
		for (int i = 0; i < this.currentTabName.Length; i++)
		{
			if (GameCanvas.isPointer(this.startTabPos + i * this.TAB_W, 52, this.TAB_W - 1, 25))
			{
				this.keyTouchTab = i;
				if (GameCanvas.isPointerJustRelease)
				{
					this.currentTabIndex = i;
					this.lastTabIndex[this.type] = i;
					GameCanvas.isPointerJustRelease = false;
					this.selected = this.lastSelect[this.currentTabIndex];
					if (num == this.currentTabIndex && this.cmRun == 0)
					{
						this.cmtoY = 0;
						this.selected = ((!GameCanvas.isTouch) ? 0 : -1);
					}
					break;
				}
			}
		}
		if (num != this.currentTabIndex)
		{
			this.size_tab = 0;
			SoundMn.gI().panelClick();
			int num2 = this.type;
			switch (num2)
			{
				case 0:
					if (this.currentTabIndex == 0)
					{
						this.setTabTask();
					}
					if (this.currentTabIndex == 1)
					{
						this.setTabInventory(true);
					}
					if (this.currentTabIndex == 2)
					{
						this.setTabSkill();
					}
					if (this.currentTabIndex == 3)
					{
						if (this.mainTabName.Length > 4)
						{
							this.setTabClans();
						}
						else
						{
							this.setTabTool();
						}
					}
					if (this.currentTabIndex == 4)
					{
						this.setTabTool();
					}
					break;
				case 1:
					this.setTabShop();
					break;
				case 2:
					if (this.currentTabIndex == 0)
					{
						this.setTabBox();
					}
					if (this.currentTabIndex == 1)
					{
						this.setTabInventory(true);
					}
					break;
				case 3:
					this.setTabZone();
					break;
				default:
					if (num2 != 12)
					{
						if (num2 != 13)
						{
							if (num2 != 21)
							{
								if (num2 == 25)
								{
									this.setTabSpeacialSkill();
								}
							}
							else
							{
								if (this.currentTabIndex == 0)
								{
									this.setTabPetInventory();
								}
								if (this.currentTabIndex == 1)
								{
									this.setTabPetStatus();
								}
								if (this.currentTabIndex == 2)
								{
									this.setTabInventory(true);
								}
							}
						}
						else
						{
							if (this.currentTabIndex == 0)
							{
								if (this.Equals(GameCanvas.panel))
								{
									this.setTabInventory(true);
								}
								else if (this.Equals(GameCanvas.panel2))
								{
									this.setTabGiaoDich(false);
								}
							}
							if (this.currentTabIndex == 1)
							{
								this.setTabGiaoDich(true);
							}
							if (this.currentTabIndex == 2)
							{
								this.setTabGiaoDich(false);
							}
						}
					}
					else
					{
						if (this.currentTabIndex == 0)
						{
							this.setTabCombine();
						}
						if (this.currentTabIndex == 1)
						{
							this.setTabInventory(true);
						}
					}
					break;
			}
			this.selected = this.lastSelect[this.currentTabIndex];
		}
	}

	// Token: 0x060008B7 RID: 2231 RVA: 0x0007A6A8 File Offset: 0x00078AA8
	private void setTabPetStatus()
	{
		this.currentListLength = this.strStatus.Length;
		this.ITEM_HEIGHT = 24;
		this.selected = ((!GameCanvas.isTouch) ? 0 : -1);
		this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
		if (this.cmyLim < 0)
		{
			this.cmyLim = 0;
		}
		this.cmy = (this.cmtoY = this.cmyLast[this.currentTabIndex]);
		if (this.cmy < 0)
		{
			this.cmy = (this.cmtoY = 0);
		}
		if (this.cmy > this.cmyLim)
		{
			this.cmy = (this.cmtoY = this.cmyLim);
		}
	}

	// Token: 0x060008B8 RID: 2232 RVA: 0x0007A76D File Offset: 0x00078B6D
	private void setTabPetSkill()
	{
	}

	// Token: 0x060008B9 RID: 2233 RVA: 0x0007A770 File Offset: 0x00078B70
	private void setTabTool()
	{
		SoundMn.gI().getSoundOption();
		this.currentListLength = Panel.strTool.Length;
		this.ITEM_HEIGHT = 24;
		this.selected = ((!GameCanvas.isTouch) ? 0 : -1);
		this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
		if (this.cmyLim < 0)
		{
			this.cmyLim = 0;
		}
		this.cmy = (this.cmtoY = this.cmyLast[this.currentTabIndex]);
		if (this.cmy < 0)
		{
			this.cmy = (this.cmtoY = 0);
		}
		if (this.cmy > this.cmyLim)
		{
			this.cmy = (this.cmtoY = this.cmyLim);
		}
	}

	// Token: 0x060008BA RID: 2234 RVA: 0x0007A840 File Offset: 0x00078C40
	public void initTabClans()
	{
		if (this.isSearchClan)
		{
			this.currentListLength = ((this.clans != null) ? (this.clans.Length + 2) : 2);
			this.clanInfo = mResources.clan_list;
		}
		else if (this.isViewMember)
		{
			this.clanReport = string.Empty;
			this.currentListLength = ((this.member != null) ? this.member.size() : this.myMember.size()) + 2;
			this.clanInfo = mResources.member + " " + ((this.currClan == null) ? global::Char.myCharz().clan.name : this.currClan.name);
		}
		else if (this.isMessage)
		{
			this.currentListLength = ClanMessage.vMessage.size() + 2;
			this.clanInfo = mResources.msg;
			this.clanReport = string.Empty;
		}
		if (global::Char.myCharz().clan == null)
		{
			this.clansOption = new string[][]
			{
				mResources.findClan,
				mResources.createClan
			};
		}
		else if (!this.isViewMember)
		{
			if (this.myMember.size() > 1)
			{
				this.clansOption = new string[][]
				{
					mResources.chatClan,
					mResources.request_pea2,
					mResources.memberr
				};
			}
			else
			{
				this.clansOption = new string[][]
				{
					mResources.memberr
				};
			}
		}
		else if ((int)global::Char.myCharz().role > 0)
		{
			this.clansOption = new string[][]
			{
				mResources.msgg,
				mResources.leaveClan
			};
		}
		else if (this.myMember.size() > 1)
		{
			this.clansOption = new string[][]
			{
				mResources.msgg,
				mResources.leaveClan,
				mResources.khau_hieuu,
				mResources.bieu_tuongg
			};
		}
		else
		{
			this.clansOption = new string[][]
			{
				mResources.msgg,
				mResources.khau_hieuu,
				mResources.bieu_tuongg
			};
		}
		this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
		if (this.cmyLim < 0)
		{
			this.cmyLim = 0;
		}
		this.cmy = (this.cmtoY = this.cmyLast[this.currentTabIndex]);
		if (this.cmy < 0)
		{
			this.cmy = (this.cmtoY = 0);
		}
		if (this.cmy > this.cmyLim)
		{
			this.cmy = (this.cmtoY = this.cmyLim);
		}
	}

	// Token: 0x060008BB RID: 2235 RVA: 0x0007AAF8 File Offset: 0x00078EF8
	public void setTabClans()
	{
		GameScr.isNewClanMessage = false;
		this.ITEM_HEIGHT = 24;
		if (this.lastSelect != null && this.lastSelect[3] == 0)
		{
			this.lastSelect[3] = -1;
		}
		this.currentListLength = 2;
		if (global::Char.myCharz().clan != null)
		{
			this.isMessage = true;
			this.isViewMember = false;
			this.isSearchClan = false;
		}
		else
		{
			this.isMessage = false;
			this.isViewMember = false;
			this.isSearchClan = true;
		}
		if (global::Char.myCharz().clan != null)
		{
			this.currentListLength = ClanMessage.vMessage.size() + 2;
		}
		this.initTabClans();
		this.cSelected = -1;
		if (this.chatTField == null)
		{
			this.chatTField = new ChatTextField();
			this.chatTField.tfChat.y = GameCanvas.h - 35 - ChatTextField.gI().tfChat.height;
			this.chatTField.initChatTextField();
			this.chatTField.parentScreen = GameCanvas.panel;
		}
		if (global::Char.myCharz().clan == null)
		{
			this.clanReport = mResources.findingClan;
			Service.gI().searchClan(string.Empty);
		}
		this.selected = this.lastSelect[this.currentTabIndex];
		if (GameCanvas.isTouch)
		{
			this.selected = -1;
		}
	}

	// Token: 0x060008BC RID: 2236 RVA: 0x0007AC50 File Offset: 0x00079050
	public void initLogMessage()
	{
		this.currentListLength = this.logChat.size() + 1;
		this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
		if (this.cmyLim < 0)
		{
			this.cmyLim = 0;
		}
		this.cmy = (this.cmtoY = this.cmyLast[this.currentTabIndex]);
		if (this.cmy < 0)
		{
			this.cmy = (this.cmtoY = 0);
		}
		if (this.cmy > this.cmyLim)
		{
			this.cmy = (this.cmtoY = this.cmyLim);
		}
		this.cmx = (this.cmtoX = 0);
	}

	// Token: 0x060008BD RID: 2237 RVA: 0x0007AD0B File Offset: 0x0007910B
	private void setTabMessage()
	{
		this.ITEM_HEIGHT = 24;
		this.initLogMessage();
		this.selected = ((!GameCanvas.isTouch) ? 0 : -1);
	}

	// Token: 0x060008BE RID: 2238 RVA: 0x0007AD34 File Offset: 0x00079134
	public void setTabShop()
	{
		this.ITEM_HEIGHT = 24;
		if (this.currentTabIndex == this.currentTabName.Length - 1 && GameCanvas.panel2 == null && this.typeShop != 2)
		{
			this.currentListLength = this.checkCurrentListLength(global::Char.myCharz().arrItemBody.Length + global::Char.myCharz().arrItemBag.Length);
		}
		else
		{
			this.currentListLength = global::Char.myCharz().arrItemShop[this.currentTabIndex].Length;
		}
		this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
		if (this.cmyLim < 0)
		{
			this.cmyLim = 0;
		}
		this.cmy = (this.cmtoY = this.cmyLast[this.currentTabIndex]);
		if (this.cmy < 0)
		{
			this.cmy = (this.cmtoY = 0);
		}
		if (this.cmy > this.cmyLim)
		{
			this.cmy = (this.cmtoY = this.cmyLim);
		}
		this.selected = ((!GameCanvas.isTouch) ? 0 : -1);
	}

	// Token: 0x060008BF RID: 2239 RVA: 0x0007AE5C File Offset: 0x0007925C
	private void setTabSkill()
	{
		this.ITEM_HEIGHT = 30;
		this.currentListLength = global::Char.myCharz().nClass.skillTemplates.Length + 6;
		this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
		if (this.cmyLim < 0)
		{
			this.cmyLim = 0;
		}
		this.cmy = (this.cmtoY = this.cmyLast[this.currentTabIndex]);
		if (this.cmy < 0)
		{
			this.cmy = (this.cmtoY = 0);
		}
		if (this.cmy > this.cmyLim)
		{
			this.cmy = this.cmyLim;
		}
		this.selected = ((!GameCanvas.isTouch) ? 0 : -1);
	}

	// Token: 0x060008C0 RID: 2240 RVA: 0x0007AF24 File Offset: 0x00079324
	private void setTabMapTrans()
	{
		this.ITEM_HEIGHT = 24;
		this.currentListLength = this.mapNames.Length;
		this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
		this.cmy = (this.cmtoY = 0);
		this.selected = ((!GameCanvas.isTouch) ? 0 : -1);
	}

	// Token: 0x060008C1 RID: 2241 RVA: 0x0007AF88 File Offset: 0x00079388
	private void setTabZone()
	{
		this.ITEM_HEIGHT = 24;
		this.currentListLength = GameScr.gI().zones.Length;
		this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
		this.cmy = (this.cmtoY = 0);
		this.selected = ((!GameCanvas.isTouch) ? 0 : -1);
	}

	// Token: 0x060008C2 RID: 2242 RVA: 0x0007AFF0 File Offset: 0x000793F0
	private void setTabBox()
	{
		this.currentListLength = this.checkCurrentListLength(global::Char.myCharz().arrItemBox.Length);
		this.ITEM_HEIGHT = 24;
		this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
		if (this.cmyLim < 0)
		{
			this.cmyLim = 9;
		}
		this.cmy = (this.cmtoY = this.cmyLast[this.currentTabIndex]);
		if (this.cmy < 0)
		{
			this.cmy = (this.cmtoY = 0);
		}
		if (this.cmy > this.cmyLim)
		{
			this.cmy = (this.cmtoY = this.cmyLim);
		}
		this.selected = ((!GameCanvas.isTouch) ? 0 : -1);
	}

	// Token: 0x060008C3 RID: 2243 RVA: 0x0007B0C0 File Offset: 0x000794C0
	private void setTabPetInventory()
	{
		this.ITEM_HEIGHT = 30;
		Item[] arrItemBody = global::Char.myPetz().arrItemBody;
		Skill[] arrPetSkill = global::Char.myPetz().arrPetSkill;
		this.currentListLength = arrItemBody.Length + arrPetSkill.Length;
		this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
		this.cmy = (this.cmtoY = this.cmyLast[this.currentTabIndex]);
		if (this.cmyLim < 0)
		{
			this.cmyLim = 0;
		}
		if (this.cmy < 0)
		{
			this.cmy = (this.cmtoY = 0);
		}
		if (this.cmy > this.cmyLim)
		{
			this.cmy = (this.cmtoY = 0);
		}
		this.selected = ((!GameCanvas.isTouch) ? 0 : -1);
	}

	// Token: 0x060008C4 RID: 2244 RVA: 0x0007B198 File Offset: 0x00079598
	private void setTabInventory(bool resetSelect)
	{
		this.currentListLength = this.checkCurrentListLength(global::Char.myCharz().arrItemBody.Length + global::Char.myCharz().arrItemBag.Length);
		this.ITEM_HEIGHT = 24;
		this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
		this.cmy = (this.cmtoY = this.cmyLast[this.currentTabIndex]);
		if (this.cmyLim < 0)
		{
			this.cmyLim = 0;
		}
		if (this.cmy < 0)
		{
			this.cmy = (this.cmtoY = 0);
		}
		if (this.cmy > this.cmyLim)
		{
			this.cmy = (this.cmtoY = 0);
		}
		if (resetSelect)
		{
			this.selected = ((!GameCanvas.isTouch) ? 0 : -1);
		}
	}

	// Token: 0x060008C5 RID: 2245 RVA: 0x0007B278 File Offset: 0x00079678
	private void setTabMap()
	{
		if (!Panel.isPaintMap)
		{
			return;
		}
		if ((int)TileMap.lastPlanetId != (int)TileMap.planetID)
		{
			Res.outz("LOAD TAM HINH");
			Panel.imgMap = GameCanvas.loadImageRMS("/img/map" + TileMap.planetID + ".png");
			TileMap.lastPlanetId = TileMap.planetID;
		}
		this.cmxMap = this.getXMap() - this.wScroll / 2;
		this.cmyMap = this.getYMap() + this.yScroll - (this.yScroll + this.hScroll / 2);
		this.pa1 = this.cmxMap;
		this.pa2 = this.cmyMap;
		this.cmxMapLim = 250 - this.wScroll;
		this.cmyMapLim = 220 - this.hScroll;
		if (this.cmxMapLim < 0)
		{
			this.cmxMapLim = 0;
		}
		if (this.cmyMapLim < 0)
		{
			this.cmyMapLim = 0;
		}
		for (int i = 0; i < Panel.mapId[(int)TileMap.planetID].Length; i++)
		{
			if (TileMap.mapID == Panel.mapId[(int)TileMap.planetID][i])
			{
				this.xMove = Panel.mapX[(int)TileMap.planetID][i] + this.xScroll;
				this.yMove = Panel.mapY[(int)TileMap.planetID][i] + this.yScroll + 5;
				break;
			}
		}
		this.xMap = this.getXMap() + this.xScroll;
		this.yMap = this.getYMap() + this.yScroll;
		this.xMapTask = this.getXMapTask() + this.xScroll;
		this.yMapTask = this.getYMapTask() + this.yScroll;
		Resources.UnloadUnusedAssets();
		GC.Collect();
	}

	// Token: 0x060008C6 RID: 2246 RVA: 0x0007B43D File Offset: 0x0007983D
	private void setTabTask()
	{
		this.cmyQuest = 0;
	}

	// Token: 0x060008C7 RID: 2247 RVA: 0x0007B448 File Offset: 0x00079848
	public void moveCamera()
	{
		if (this.timeShow > 0)
		{
			this.timeShow--;
		}
		if (this.justRelease && this.Equals(GameCanvas.panel) && this.typeShop == 2 && this.maxPageShop[this.currentTabIndex] > 1)
		{
			if (this.cmy < -50)
			{
				InfoDlg.showWait();
				this.justRelease = false;
				if (this.currPageShop[this.currentTabIndex] <= 0)
				{
					Service.gI().kigui(4, -1, (sbyte)this.currentTabIndex, this.maxPageShop[this.currentTabIndex] - 1, -1);
				}
				else
				{
					Service.gI().kigui(4, -1, (sbyte)this.currentTabIndex, this.currPageShop[this.currentTabIndex] - 1, -1);
				}
			}
			else if (this.cmy > this.cmyLim + 50)
			{
				this.justRelease = false;
				InfoDlg.showWait();
				if (this.currPageShop[this.currentTabIndex] >= this.maxPageShop[this.currentTabIndex] - 1)
				{
					Service.gI().kigui(4, -1, (sbyte)this.currentTabIndex, 0, -1);
				}
				else
				{
					Service.gI().kigui(4, -1, (sbyte)this.currentTabIndex, this.currPageShop[this.currentTabIndex] + 1, -1);
				}
			}
		}
		if (this.cmx != this.cmtoX && !this.pointerIsDowning)
		{
			this.cmvx = this.cmtoX - this.cmx << 2;
			this.cmdx += this.cmvx;
			this.cmx += this.cmdx >> 3;
			this.cmdx &= 15;
		}
		if (global::Math.abs(this.cmtoX - this.cmx) < 10)
		{
			this.cmx = this.cmtoX;
		}
		if (this.isClose)
		{
			this.isClose = false;
			this.cmtoX = this.wScroll;
		}
		if (this.cmtoX >= this.wScroll - 10 && this.cmx >= this.wScroll - 10 && this.position == 0)
		{
			this.isShow = false;
			this.cleanCombine();
			if (this.isChangeZone)
			{
				this.isChangeZone = false;
				if (global::Char.myCharz().cHP > 0 && global::Char.myCharz().statusMe != 14)
				{
					InfoDlg.showWait();
					if (this.type == 3)
					{
						Service.gI().requestChangeZone(this.selected, -1);
					}
					else if (this.type == 14)
					{
						
						Pk9rXmap.SelectMapTrans(this.selected);
					}
				}
			}
			if (this.isSelectPlayerMenu)
			{
				this.isSelectPlayerMenu = false;
				Command command = (Command)this.vPlayerMenu.elementAt(this.selected);
				command.performAction();
			}
			this.vPlayerMenu.removeAllElements();
			this.charMenu = null;
		}
		if (this.cmRun != 0 && !this.pointerIsDowning)
		{
			this.cmtoY += this.cmRun / 100;
			if (this.cmtoY < 0)
			{
				this.cmtoY = 0;
			}
			else if (this.cmtoY > this.cmyLim)
			{
				this.cmtoY = this.cmyLim;
			}
			else
			{
				this.cmy = this.cmtoY;
			}
			this.cmRun = this.cmRun * 9 / 10;
			if (this.cmRun < 100 && this.cmRun > -100)
			{
				this.cmRun = 0;
			}
		}
		if (this.cmy != this.cmtoY && !this.pointerIsDowning)
		{
			this.cmvy = this.cmtoY - this.cmy << 2;
			this.cmdy += this.cmvy;
			this.cmy += this.cmdy >> 4;
			this.cmdy &= 15;
		}
		this.cmyLast[this.currentTabIndex] = this.cmy;
	}

	// Token: 0x060008C8 RID: 2248 RVA: 0x0007B868 File Offset: 0x00079C68
	public void paintDetail(mGraphics g)
	{
		if (this.cp != null)
		{
			if (this.cp.says == null)
			{
				return;
			}
			this.cp.paint(g);
			int num = this.cp.cx + 13;
			int num2 = this.cp.cy + 11;
			if (this.type == 15)
			{
				num += 5;
				num2 += 26;
			}
			if (this.type == 0 && this.currentTabIndex == 3)
			{
				if (this.isSearchClan)
				{
					num -= 5;
				}
				else if (this.partID != null || this.charInfo != null)
				{
					num = this.cp.cx + 21;
					num2 = this.cp.cy + 40;
				}
			}
			if (this.partID != null)
			{
				Part part = GameScr.parts[this.partID[0]];
				Part part2 = GameScr.parts[this.partID[1]];
				Part part3 = GameScr.parts[this.partID[2]];
				SmallImage.drawSmallImage(g, (int)part.pi[global::Char.CharInfo[0][0][0]].id, num + global::Char.CharInfo[0][0][1] + (int)part.pi[global::Char.CharInfo[0][0][0]].dx, num2 - global::Char.CharInfo[0][0][2] + (int)part.pi[global::Char.CharInfo[0][0][0]].dy, 0, 0);
				SmallImage.drawSmallImage(g, (int)part2.pi[global::Char.CharInfo[0][1][0]].id, num + global::Char.CharInfo[0][1][1] + (int)part2.pi[global::Char.CharInfo[0][1][0]].dx, num2 - global::Char.CharInfo[0][1][2] + (int)part2.pi[global::Char.CharInfo[0][1][0]].dy, 0, 0);
				SmallImage.drawSmallImage(g, (int)part3.pi[global::Char.CharInfo[0][2][0]].id, num + global::Char.CharInfo[0][2][1] + (int)part3.pi[global::Char.CharInfo[0][2][0]].dx, num2 - global::Char.CharInfo[0][2][2] + (int)part3.pi[global::Char.CharInfo[0][2][0]].dy, 0, 0);
			}
			else if (this.charInfo != null)
			{
				this.charInfo.paintCharBody(g, num + 5, num2 + 25, 1, 0, true);
			}
			else if (this.idIcon != -1)
			{
				SmallImage.drawSmallImage(g, this.idIcon, num, num2, 0, 3);
			}
			if (this.currItem != null && (int)this.currItem.template.type != 5)
			{
				if (this.currItem.compare > 0)
				{
					g.drawImage(Panel.imgUp, num - 7, num2 + 13, 3);
					mFont.tahoma_7b_green.drawString(g, Res.abs(this.currItem.compare) + string.Empty, num + 1, num2 + 8, 0);
				}
				else if (this.currItem.compare < 0 && this.currItem.compare != -1)
				{
					g.drawImage(Panel.imgDown, num - 7, num2 + 13, 3);
					mFont.tahoma_7b_red.drawString(g, Res.abs(this.currItem.compare) + string.Empty, num + 1, num2 + 8, 0);
				}
			}
		}
	}

	// Token: 0x060008C9 RID: 2249 RVA: 0x0007BBD8 File Offset: 0x00079FD8
	public void paintTop(mGraphics g)
	{
		g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
		g.translate(0, -this.cmy);
		g.setColor(0);
		if (this.currentListLength == 0)
		{
			return;
		}
		int num = (this.cmy + this.hScroll) / 24 + 1;
		if (num < this.hScroll / 24 + 1)
		{
			num = this.hScroll / 24 + 1;
		}
		if (num > this.currentListLength)
		{
			num = this.currentListLength;
		}
		int num2 = this.cmy / 24;
		if (num2 >= num)
		{
			num2 = num - 1;
		}
		if (num2 < 0)
		{
			num2 = 0;
		}
		for (int i = num2; i < num; i++)
		{
			int num3 = this.xScroll;
			int num4 = this.yScroll + i * this.ITEM_HEIGHT;
			int num5 = 24;
			int h = this.ITEM_HEIGHT - 1;
			int num6 = this.xScroll + num5;
			int num7 = this.yScroll + i * this.ITEM_HEIGHT;
			int num8 = this.wScroll - num5;
			int num9 = this.ITEM_HEIGHT - 1;
			g.setColor((i != this.selected) ? 15196114 : 16383818);
			g.fillRect(num6, num7, num8, num9);
			g.setColor((i != this.selected) ? 9993045 : 9541120);
			g.fillRect(num3, num4, num5, h);
			TopInfo topInfo = (TopInfo)this.vTop.elementAt(i);
			if (topInfo.headICON != -1)
			{
				SmallImage.drawSmallImage(g, topInfo.headICON, num3, num4, 0, 0);
			}
			else
			{
				Part part = GameScr.parts[topInfo.headID];
				SmallImage.drawSmallImage(g, (int)part.pi[global::Char.CharInfo[0][0][0]].id, num3 + (int)part.pi[global::Char.CharInfo[0][0][0]].dx, num4 + num9 - 1, 0, mGraphics.BOTTOM | mGraphics.LEFT);
			}
			g.setClip(this.xScroll, this.yScroll + this.cmy, this.wScroll, this.hScroll);
			if (topInfo.pId != global::Char.myCharz().charID)
			{
				mFont.tahoma_7b_green.drawString(g, topInfo.name, num6 + 5, num7, 0);
			}
			else
			{
				mFont.tahoma_7b_red.drawString(g, topInfo.name, num6 + 5, num7, 0);
			}
			mFont.tahoma_7_blue.drawString(g, topInfo.info, num6 + num8 - 5, num7 + 11, 1);
			mFont.tahoma_7_green2.drawString(g, string.Concat(new object[]
			{
				mResources.rank,
				": ",
				topInfo.rank,
				string.Empty
			}), num6 + 5, num7 + 11, 0);
		}
		this.paintScrollArrow(g);
	}

	// Token: 0x060008CA RID: 2250 RVA: 0x0007BEBC File Offset: 0x0007A2BC
	public void paint(mGraphics g)
	{
		g.translate(-g.getTranslateX(), -g.getTranslateY() + mGraphics.addYWhenOpenKeyBoard);
		g.translate(-this.cmx, 0);
		g.translate(this.X, this.Y);
		if ((int)GameCanvas.panel.combineSuccess != -1)
		{
			if (this.Equals(GameCanvas.panel))
			{
				this.paintCombineEff(g);
			}
		}
		else
		{
			GameCanvas.paintz.paintFrameSimple(this.X, this.Y, this.W, this.H, g);
			this.paintTopInfo(g);
			this.paintBottomMoneyInfo(g);
			this.paintTab(g);
			switch (this.type)
			{
				case 0:
					if (this.currentTabIndex == 0)
					{
						this.paintTask(g);
					}
					if (this.currentTabIndex == 1)
					{
						this.paintInventory(g);
					}
					if (this.currentTabIndex == 2)
					{
						this.paintSkill(g);
					}
					if (this.currentTabIndex == 3)
					{
						if (this.mainTabName.Length == 4)
						{
							this.paintTools(g);
						}
						else
						{
							this.paintClans(g);
						}
					}
					if (this.currentTabIndex == 4)
					{
						this.paintTools(g);
					}
					break;
				case 1:
					this.paintShop(g);
					break;
				case 2:
					if (this.currentTabIndex == 0)
					{
						this.paintBox(g);
					}
					if (this.currentTabIndex == 1)
					{
						this.paintInventory(g);
					}
					break;
				case 3:
					this.paintZone(g);
					break;
				case 4:
					this.paintMap(g);
					break;
				case 7:
					this.paintInventory(g);
					break;
				case 8:
					this.paintLogChat(g);
					break;
				case 9:
					this.paintArchivement(g);
					break;
				case 10:
					this.paintPlayerMenu(g);
					break;
				case 11:
					this.paintFriend(g);
					break;
				case 12:
					if (this.currentTabIndex == 0)
					{
						this.paintCombine(g);
					}
					if (this.currentTabIndex == 1)
					{
						this.paintInventory(g);
					}
					break;
				case 13:
					if (this.currentTabIndex == 0)
					{
						if (this.Equals(GameCanvas.panel))
						{
							this.paintInventory(g);
						}
						else
						{
							this.paintGiaoDich(g, false);
						}
					}
					if (this.currentTabIndex == 1)
					{
						this.paintGiaoDich(g, true);
					}
					if (this.currentTabIndex == 2)
					{
						this.paintGiaoDich(g, false);
					}
					break;
				case 14:
					this.paintMapTrans(g);
					break;
				case 15:
					this.paintTop(g);
					break;
				case 16:
					this.paintEnemy(g);
					break;
				case 17:
					this.paintShop(g);
					break;
				case 18:
					this.paintFlagChange(g);
					break;
				case 19:
					this.paintOption(g);
					break;
				case 20:
					this.paintAccount(g);
					break;
				case 21:
					if (this.currentTabIndex == 0)
					{
						this.paintPetInventory(g);
					}
					if (this.currentTabIndex == 1)
					{
						this.paintPetStatus(g);
					}
					if (this.currentTabIndex == 2)
					{
						this.paintInventory(g);
					}
					break;
				case 22:
					this.paintAuto(g);
					break;
				case 23:
					this.paintGameInfo(g);
					break;
				case 24:
					this.paintGameSubInfo(g);
					break;
				case 25:
					this.paintSpeacialSkill(g);
					break;
			}
			GameScr.resetTranslate(g);
			this.paintDetail(g);
			if (this.cmx == this.cmtoX)
			{
				this.cmdClose.paint(g);
			}
			if (this.tabIcon != null && this.tabIcon.isShow)
			{
				this.tabIcon.paint(g);
			}
		}
	}

	// Token: 0x060008CB RID: 2251 RVA: 0x0007C28C File Offset: 0x0007A68C
	private void paintShop(mGraphics g)
	{
		if (this.type == 1 && this.currentTabIndex == this.currentTabName.Length - 1 && GameCanvas.panel2 == null && this.typeShop != 2)
		{
			this.paintInventory(g);
			return;
		}
		g.setColor(16711680);
		g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
		if (this.typeShop == 2 && this.Equals(GameCanvas.panel))
		{
			if (this.currentTabIndex <= 3 && GameCanvas.isTouch)
			{
				if (this.cmy < -50)
				{
					GameCanvas.paintShukiren(this.xScroll + this.wScroll / 2, this.yScroll + 30, g);
				}
				else if (this.cmy < 0)
				{
					mFont.tahoma_7_grey.drawString(g, mResources.getDown, this.xScroll + this.wScroll / 2, this.yScroll + 15, 2);
				}
				else if (this.cmyLim >= 0)
				{
					if (this.cmy > this.cmyLim + 50)
					{
						GameCanvas.paintShukiren(this.xScroll + this.wScroll / 2, this.yScroll + this.hScroll - 30, g);
					}
					else if (this.cmy > this.cmyLim)
					{
						mFont.tahoma_7_grey.drawString(g, mResources.getUp, this.xScroll + this.wScroll / 2, this.yScroll + this.hScroll - 25, 2);
					}
				}
			}
			if (global::Char.myCharz().arrItemShop[this.currentTabIndex].Length == 0 && this.type != 17)
			{
				mFont.tahoma_7_grey.drawString(g, mResources.notYetSell, this.xScroll + this.wScroll / 2, this.yScroll + this.hScroll / 2 - 10, 2);
				return;
			}
		}
		g.translate(0, -this.cmy);
		Item[] array = global::Char.myCharz().arrItemShop[this.currentTabIndex];
		if (this.typeShop == 2 && (this.currentTabIndex == 4 || this.type == 17))
		{
			array = global::Char.myCharz().arrItemShop[4];
			if (array.Length == 0)
			{
				mFont.tahoma_7_grey.drawString(g, mResources.notYetSell, this.xScroll + this.wScroll / 2, this.yScroll + this.hScroll / 2 - 10, 2);
				return;
			}
		}
		int num = array.Length;
		for (int i = 0; i < num; i++)
		{
			int num2 = this.xScroll + 26;
			int num3 = this.yScroll + i * this.ITEM_HEIGHT;
			int num4 = this.wScroll - 26;
			int h = this.ITEM_HEIGHT - 1;
			int num5 = this.xScroll;
			int num6 = this.yScroll + i * this.ITEM_HEIGHT;
			int num7 = 24;
			int num8 = this.ITEM_HEIGHT - 1;
			if (num3 - this.cmy <= this.yScroll + this.hScroll)
			{
				if (num3 - this.cmy >= this.yScroll - this.ITEM_HEIGHT)
				{
					g.setColor((i != this.selected) ? 15196114 : 16383818);
					g.fillRect(num2, num3, num4, h);
					g.setColor((i != this.selected) ? 9993045 : 9541120);
					g.fillRect(num5, num6, num7, num8);
					Item item = array[i];
					if (item != null)
					{
						string str = string.Empty;
						mFont mFont = mFont.tahoma_7_green2;
						if ((int)item.isMe != 0 && this.typeShop == 2 && this.currentTabIndex <= 3 && !this.Equals(GameCanvas.panel2))
						{
							mFont = mFont.tahoma_7b_green;
						}
						if (item.itemOption != null)
						{
							for (int j = 0; j < item.itemOption.Length; j++)
							{
								if (item.itemOption[j].optionTemplate.id == 72)
								{
									str = " [+" + item.itemOption[j].param + "]";
								}
								if (item.itemOption[j].optionTemplate.id == 41)
								{
									if (item.itemOption[j].param == 1)
									{
										mFont = Panel.GetFont(0);
									}
									else if (item.itemOption[j].param == 2)
									{
										mFont = Panel.GetFont(2);
									}
									else if (item.itemOption[j].param == 3)
									{
										mFont = Panel.GetFont(8);
									}
									else if (item.itemOption[j].param == 4)
									{
										mFont = Panel.GetFont(7);
									}
								}
							}
						}
						mFont.drawString(g, item.template.name + str, num2 + 5, num3 + 1, 0);
						string text = string.Empty;
						if (item.itemOption != null && item.itemOption.Length >= 1)
						{
							if (item.itemOption[0] != null && item.itemOption[0].optionTemplate.id != 102 && item.itemOption[0].optionTemplate.id != 107)
							{
								text += item.itemOption[0].getOptionString();
							}
							mFont mFont2 = mFont.tahoma_7_blue;
							if (item.compare < 0 && (int)item.template.type != 5)
							{
								mFont2 = mFont.tahoma_7_red;
							}
							if (this.typeShop == 2 && item.itemOption.Length > 1 && (int)item.buyType != -1)
							{
								text += string.Empty;
							}
							if (this.typeShop != 2 || (this.typeShop == 2 && (int)item.buyType <= 1))
							{
								mFont2.drawString(g, text, num2 + 5, num3 + 11, 0);
							}
						}
						if (item.buySpec > 0)
						{
							SmallImage.drawSmallImage(g, (int)item.iconSpec, num2 + num4 - 7, num3 + 9, 0, 3);
							mFont.tahoma_7b_blue.drawString(g, Res.formatNumber((long)item.buySpec), num2 + num4 - 15, num3 + 1, mFont.RIGHT);
						}
						if (item.buyCoin != 0 || item.buyGold != 0)
						{
							if (this.typeShop != 2 && item.powerRequire == 0L)
							{
								if (item.buyCoin > 0 && item.buyGold > 0)
								{
									if (item.buyCoin > 0)
									{
										g.drawImage(Panel.imgXu, num2 + num4 - 7, num3 + 7, 3);
										mFont.tahoma_7b_yellow.drawString(g, Res.formatNumber((long)item.buyCoin), num2 + num4 - 15, num3 + 1, mFont.RIGHT);
									}
									if (item.buyGold > 0)
									{
										g.drawImage(Panel.imgLuong, num2 + num4 - 7, num3 + 7 + 11, 3);
										mFont.tahoma_7b_green.drawString(g, Res.formatNumber((long)item.buyGold), num2 + num4 - 15, num3 + 12, mFont.RIGHT);
									}
								}
								else
								{
									if (item.buyCoin > 0)
									{
										g.drawImage(Panel.imgXu, num2 + num4 - 7, num3 + 7, 3);
										mFont.tahoma_7b_yellow.drawString(g, Res.formatNumber((long)item.buyCoin), num2 + num4 - 15, num3 + 1, mFont.RIGHT);
									}
									if (item.buyGold > 0)
									{
										g.drawImage(Panel.imgLuong, num2 + num4 - 7, num3 + 7, 3);
										mFont.tahoma_7b_green.drawString(g, Res.formatNumber((long)item.buyGold), num2 + num4 - 15, num3 + 1, mFont.RIGHT);
									}
								}
							}
							if (this.typeShop == 2 && this.currentTabIndex <= 3 && !this.Equals(GameCanvas.panel2))
							{
								if (item.buyCoin > 0 && item.buyGold > 0)
								{
									if (item.buyCoin > 0)
									{
										SmallImage.drawSmallImage(g, 4028, num2 + num4 - 10, num3 + 2, 0, mFont.RIGHT);
										mFont.tahoma_7b_yellow.drawString(g, Res.formatNumber2(item.buyCoin), num2 + num4 - 24, num3 + 1, mFont.RIGHT);
									}
									if (item.buyGold > 0)
									{
										g.drawImage(Panel.imgLuong, num2 + num4 - 7, num3 + 7 + 11, 3);
										mFont.tahoma_7b_green.drawString(g, Res.formatNumber2((long)item.buyGold), num2 + num4 - 15, num3 + 12, mFont.RIGHT);
									}
								}
								else
								{
									if (item.buyCoin > 0)
									{
										SmallImage.drawSmallImage(g, 4028, num2 + num4 - 10, num3 + 2, 0, mFont.RIGHT);
										mFont.tahoma_7b_yellow.drawString(g, Res.formatNumber2(item.buyCoin), num2 + num4 - 24, num3 + 1, mFont.RIGHT);
									}
									if (item.buyGold > 0)
									{
										g.drawImage(Panel.imgLuong, num2 + num4 - 7, num3 + 7, 3);
										mFont.tahoma_7b_green.drawString(g, Res.formatNumber2((long)item.buyGold), num2 + num4 - 15, num3 + 1, mFont.RIGHT);
									}
								}
							}
						}
						SmallImage.drawSmallImage(g, (int)item.template.iconID, num5 + num7 / 2, num6 + num8 / 2, 0, 3);
						if (item.quantity > 1)
						{
							mFont.tahoma_7_yellow.drawString(g, string.Empty + item.quantity, num5 + num7, num6 + num8 - mFont.tahoma_7_yellow.getHeight(), 1);
						}
						if (item.newItem && GameCanvas.gameTick % 10 > 5)
						{
							g.drawImage(Panel.imgNew, num5 + num7 / 2, num3 + 19, 3);
						}
					}
					if (this.typeShop == 2 && (this.Equals(GameCanvas.panel2) || this.currentTabIndex == 4))
					{
						if ((int)item.buyType != 0)
						{
							if ((int)item.buyType == 1)
							{
								mFont.tahoma_7_green.drawString(g, mResources.dangban, num2 + num4 - 5, num3 + 1, mFont.RIGHT);
								if (item.buyCoin != -1)
								{
									SmallImage.drawSmallImage(g, 4028, num2 + num4 - 13, num3 + 14, 0, mFont.RIGHT);
									mFont.tahoma_7b_yellow.drawString(g, Res.formatNumber2(item.buyCoin), num2 + num4 - 26, num3 + 13, mFont.RIGHT);
								}
								else if (item.buyGold != -1)
								{
									g.drawImage(Panel.imgLuongKhoa, num2 + num4 - 7, num3 + 17, 3);
									mFont.tahoma_7b_red.drawString(g, Res.formatNumber2((long)item.buyGold), num2 + num4 - 15, num3 + 11, mFont.RIGHT);
								}
							}
							else if ((int)item.buyType == 2)
							{
								mFont.tahoma_7b_blue.drawString(g, mResources.daban, num2 + num4 - 5, num3 + 1, mFont.RIGHT);
								if (item.buyCoin != -1)
								{
									SmallImage.drawSmallImage(g, 4028, num2 + num4 - 13, num3 + 14, 0, mFont.RIGHT);
									mFont.tahoma_7b_yellow.drawString(g, Res.formatNumber2(item.buyCoin), num2 + num4 - 26, num3 + 13, mFont.RIGHT);
								}
								else if (item.buyGold != -1)
								{
									g.drawImage(Panel.imgLuongKhoa, num2 + num4 - 7, num3 + 17, 3);
									mFont.tahoma_7b_red.drawString(g, Res.formatNumber2((long)item.buyGold), num2 + num4 - 15, num3 + 11, mFont.RIGHT);
								}
							}
						}
					}
				}
			}
		}
		this.paintScrollArrow(g);
	}

	// Token: 0x060008CC RID: 2252 RVA: 0x0007CE73 File Offset: 0x0007B273
	private void paintAuto(mGraphics g)
	{
	}

	// Token: 0x060008CD RID: 2253 RVA: 0x0007CE78 File Offset: 0x0007B278
	private void paintPetStatus(mGraphics g)
	{
		g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
		g.translate(0, -this.cmy);
		for (int i = 0; i < this.strStatus.Length; i++)
		{
			int x = this.xScroll;
			int num = this.yScroll + i * this.ITEM_HEIGHT;
			int num2 = this.wScroll - 1;
			int h = this.ITEM_HEIGHT - 1;
			if (num - this.cmy <= this.yScroll + this.hScroll)
			{
				if (num - this.cmy >= this.yScroll - this.ITEM_HEIGHT)
				{
					g.setColor((i != this.selected) ? 15196114 : 16383818);
					g.fillRect(x, num, num2, h);
					mFont.tahoma_7b_dark.drawString(g, this.strStatus[i], this.xScroll + 10, num + 6, mFont.LEFT);
				}
			}
		}
		this.paintScrollArrow(g);
	}

	// Token: 0x060008CE RID: 2254 RVA: 0x0007CF89 File Offset: 0x0007B389
	private void paintPetSkill()
	{
	}

	// Token: 0x060008CF RID: 2255 RVA: 0x0007CF8C File Offset: 0x0007B38C
	private void paintPetInventory(mGraphics g)
	{
		g.setColor(16711680);
		g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
		g.translate(0, -this.cmy);
		Item[] arrItemBody = global::Char.myPetz().arrItemBody;
		Skill[] arrPetSkill = global::Char.myPetz().arrPetSkill;
		for (int i = 0; i < arrItemBody.Length  + arrPetSkill.Length; i++)
		{
			bool flag = i < arrItemBody.Length;
			int num = i;
			int num2 = i - arrItemBody.Length;
			int num3 = this.xScroll + 36;
			int num4 = this.yScroll + i * this.ITEM_HEIGHT;
			int num5 = this.wScroll - 36;
			int h = this.ITEM_HEIGHT - 1;
			int num6 = this.xScroll;
			int num7 = this.yScroll + i * this.ITEM_HEIGHT;
			int num8 = 34;
			int num9 = this.ITEM_HEIGHT - 1;
			if (num4 - this.cmy <= this.yScroll + this.hScroll)
			{
				if (num4 - this.cmy >= this.yScroll - this.ITEM_HEIGHT)
				{
					Item item = (!flag) ? null : arrItemBody[num];
					g.setColor((i != this.selected) ? ((!flag) ? 15723751 : 15196114) : 16383818);
					g.fillRect(num3, num4, num5, h);
					g.setColor((i != this.selected) ? ((!flag) ? 11837316 : 9993045) : 9541120);
					if (item != null)
					{
						for (int j = 0; j < item.itemOption.Length; j++)
						{
							if (item.itemOption[j].optionTemplate.id == 72 && item.itemOption[j].param > 0)
							{
								sbyte color_Item_Upgrade = Panel.GetColor_Item_Upgrade(item.itemOption[j].param);
								int color_ItemBg = Panel.GetColor_ItemBg((int)color_Item_Upgrade);
								if (color_ItemBg != -1)
								{
									g.setColor((i != this.selected) ? Panel.GetColor_ItemBg((int)color_Item_Upgrade) : Panel.GetColor_ItemBg((int)color_Item_Upgrade));
								}
							}
						}
					}
					g.fillRect(num6, num7, num8, num9);
					if (item != null && item.isSelect && GameCanvas.panel.type == 12)
					{
						g.setColor((i != this.selected) ? 6047789 : 7040779);
						g.fillRect(num6, num7, num8, num9);
					}
					if (item != null)
					{
						string str = string.Empty;
						mFont mFont = mFont.tahoma_7_green2;
						if (item.itemOption != null)
						{
							for (int k = 0; k < item.itemOption.Length; k++)
							{
								if (item.itemOption[k].optionTemplate.id == 72)
								{
									str = " [+" + item.itemOption[k].param + "]";
									item.paintColorItem(g, num6+ 18, num7 + 12, item.itemOption[k].param);
								}
								
								else if (item.itemOption[k].optionTemplate.id == 107)
								{

									mFont.tahoma_7b_red.drawString(g, item.itemOption[k].param.ToString(), num3 + 121, num4 + 5, 0);
								}
								if (item.itemOption[k].optionTemplate.id == 41)
								{
									if (item.itemOption[k].param == 1)
									{
										mFont = Panel.GetFont(0);
									}
									else if (item.itemOption[k].param == 2)
									{
										mFont = Panel.GetFont(2);
									}
									else if (item.itemOption[k].param == 3)
									{
										mFont = Panel.GetFont(8);
									}
									else if (item.itemOption[k].param == 4)
									{
										mFont = Panel.GetFont(7);
									}
								}
							}
						}
						mFont.drawString(g, item.template.name + str, num3 + 5, num4 + 1, 0);
						string text = string.Empty;
						if (item.itemOption != null)
						{
							if (item.itemOption.Length > 0 && item.itemOption[0] != null && item.itemOption[0].optionTemplate.id != 102 && item.itemOption[0].optionTemplate.id != 107)
							{
								text += item.itemOption[0].getOptionString();
							}
							mFont mFont2 = mFont.tahoma_7_blue;
							if (item.compare < 0 && (int)item.template.type != 5)
							{
								mFont2 = mFont.tahoma_7_red;
							}
							if (item.itemOption.Length > 1)
							{
								for (int l = 1; l < 2; l++)
								{
									if (item.itemOption[l] != null && item.itemOption[l].optionTemplate.id != 102 && item.itemOption[l].optionTemplate.id != 107)
									{
										text = text + "," + item.itemOption[l].getOptionString();
									}
								}
							}
							mFont2.drawString(g, text, num3 + 5, num4 + 11, mFont.LEFT);
						}
						SmallImage.drawSmallImage(g, (int)item.template.iconID, num6 + num8 / 2, num7 + num9 / 2, 0, 3);
						if (item.itemOption != null)
						{
							for (int m = 0; m < item.itemOption.Length; m++)
							{
								this.paintOptItem(g, item.itemOption[m].optionTemplate.id, item.itemOption[m].param, num6, num7, num8, num9);
							}
							for (int n = 0; n < item.itemOption.Length; n++)
							{
								this.paintOptSlotItem(g, item.itemOption[n].optionTemplate.id, item.itemOption[n].param, num6, num7, num8, num9);
							}
						}
						if (item.quantity > 1)
						{
							mFont.tahoma_7_yellow.drawString(g, string.Empty + item.quantity, num6 + num8, num7 + num9 - mFont.tahoma_7_yellow.getHeight(), 1);
						}
					}
					else if (!flag)
					{
						Skill skill = arrPetSkill[num2];
						g.drawImage(GameScr.imgSkill, num6 + num8 / 2, num7 + num9 / 2, 3);
						if (skill.template != null)
						{
							mFont.tahoma_7_blue.drawString(g, skill.template.name, num3 + 5, num4 + 1, 0);
							mFont.tahoma_7_green2.drawString(g, string.Concat(new object[]
							{
								mResources.level,
								": ",
								skill.point,
								string.Empty
							}), num3 + 5, num4 + 11, 0);
							SmallImage.drawSmallImage(g, skill.template.iconId, num6 + num8 / 2, num7 + num9 / 2, 0, 3);
						}
						else
						{
							mFont.tahoma_7_green2.drawString(g, skill.moreInfo, num3 + 5, num4 + 5, 0);
							SmallImage.drawSmallImage(g, GameScr.efs[98].arrEfInfo[0].idImg, num6 + num8 / 2, num7 + num9 / 2, 0, 3);
						}
					}
				}
			}
		}
		this.paintScrollArrow(g);
	}

	// Token: 0x060008D0 RID: 2256 RVA: 0x0007D6DC File Offset: 0x0007BADC
	private void paintScrollArrow(mGraphics g)
	{
		g.translate(-g.getTranslateX(), -g.getTranslateY());
		if ((this.cmy > 24 && this.currentListLength > 0) || (this.Equals(GameCanvas.panel) && this.typeShop == 2 && this.maxPageShop[this.currentTabIndex] > 1))
		{
			g.drawRegion(Mob.imgHP, 0, 0, 9, 6, 1, this.xScroll + this.wScroll - 12, this.yScroll + 3, 0);
		}
		if ((this.cmy < this.cmyLim && this.currentListLength > 0) || (this.Equals(GameCanvas.panel) && this.typeShop == 2 && this.maxPageShop[this.currentTabIndex] > 1))
		{
			g.drawRegion(Mob.imgHP, 0, 0, 9, 6, 0, this.xScroll + this.wScroll - 12, this.yScroll + this.hScroll - 8, 0);
		}
	}

	// Token: 0x060008D1 RID: 2257 RVA: 0x0007D7EC File Offset: 0x0007BBEC
	private void paintTools(mGraphics g)
	{
		g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
		g.translate(0, -this.cmy);
		for (int i = 0; i < Panel.strTool.Length; i++)
		{
			int num = this.xScroll;
			int num2 = this.yScroll + i * this.ITEM_HEIGHT;
			int num3 = this.wScroll - 1;
			int h = this.ITEM_HEIGHT - 1;
			if (num2 - this.cmy <= this.yScroll + this.hScroll)
			{
				if (num2 - this.cmy >= this.yScroll - this.ITEM_HEIGHT)
				{
					g.setColor((i != this.selected) ? 15196114 : 16383818);
					g.fillRect(num, num2, num3, h);

					mFont.tahoma_7b_dark.drawString(g, Panel.strTool[i], this.xScroll + this.wScroll / 2, num2 + 6, mFont.CENTER);
					if (Panel.strTool[i].Equals(mResources.gameInfo))
					{
						for (int j = 0; j < Panel.vGameInfo.size(); j++)
						{
							GameInfo gameInfo = (GameInfo)Panel.vGameInfo.elementAt(j);
							if (!gameInfo.hasRead)
							{
								
								break;
							}
						}
					}
				}
			}
		}
		this.paintScrollArrow(g);
	}

	// Token: 0x060008D2 RID: 2258 RVA: 0x0007D980 File Offset: 0x0007BD80
	private void paintGameSubInfo(mGraphics g)
	{
		g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
		g.translate(0, -this.cmy);
		for (int i = 0; i < Panel.contenInfo.Length; i++)
		{
			int num = this.xScroll;
			int num2 = this.yScroll + i * 15;
			int num3 = this.wScroll - 1;
			int num4 = this.ITEM_HEIGHT - 1;
			if (num2 - this.cmy <= this.yScroll + this.hScroll)
			{
				if (num2 - this.cmy >= this.yScroll - this.ITEM_HEIGHT)
				{
					mFont.tahoma_7b_dark.drawString(g, Panel.contenInfo[i], this.xScroll + 5, num2 + 6, mFont.LEFT);
				}
			}
		}
		this.paintScrollArrow(g);
	}

	// Token: 0x060008D3 RID: 2259 RVA: 0x0007DA60 File Offset: 0x0007BE60
	private void paintGameInfo(mGraphics g)
	{
		g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
		g.translate(0, -this.cmy);
		for (int i = 0; i < Mod.Mod.bossVip.size(); i++)
		{
			int num = this.xScroll;
			int num2 = this.yScroll + i * this.ITEM_HEIGHT;
			int num3 = this.wScroll - 1;
			int h = this.ITEM_HEIGHT - 1;
			if (num2 - this.cmy <= this.yScroll + this.hScroll)
			{
				if (num2 - this.cmy >= this.yScroll - this.ITEM_HEIGHT)
				{
					g.setColor((i != this.selected) ? 15196114 : 16383818);
					g.setColor(243229171, 0.5f);
					g.fillRect(num, num2, num3, h);
					Console.WriteLine(((ShowBoss)Mod.Mod.bossVip.elementAt(i)).nameBoss);
					((ShowBoss)Mod.Mod.bossVip.elementAt(i)).paintBoss(g, this.xScroll + this.wScroll / 2, num2 + 6, mFont.CENTER);
					num3 += 10;
					
					for (int j = 0; j < Mod.Mod.bossVip.size(); j++)
					{
						if (GameCanvas.isPointerHoldIn(this.xScroll + this.wScroll / 2 + 20, num2 + 6 + 10 * j, 28, 9))
						{
							GameCanvas.isPointerJustDown = false;
							GameScr.gI().isPointerDowning = false;
							if (GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
							{
								if (!Pk9rXmap.IsXmapRunning)
								{
									XmapController.StartRunToMapId(Mod.Mod.GetIDMap(((ShowBoss)Mod.Mod.bossVip.elementAt(j)).mapName));
								}
								else
								{
									XmapController.FinishXmap();
								}
								global::Char.myCharz().currentMovePoint = null;
								GameCanvas.clearAllPointerEvent();
								return;
							}
						}
					}
				}
			}
		}
		this.paintScrollArrow(g);
	}

	private void doFirePaintBoss()
    {
		if (this.selected == -1)
		{
			return;
		}
		this.infoSelect = this.selected;
	
		this.setTypeGameSubInfo();
	}

	// Token: 0x060008D4 RID: 2260 RVA: 0x0007DBB8 File Offset: 0x0007BFB8
	private void paintSkill(mGraphics g)
	{
		g.setColor(16711680);
		g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
		g.translate(0, -this.cmy);
		int num = global::Char.myCharz().nClass.skillTemplates.Length;
		for (int i = 0; i < num + 6; i++)
		{
			int num2 = this.xScroll + 30;
			int num3 = this.yScroll + i * this.ITEM_HEIGHT;
			int num4 = this.wScroll - 30;
			int h = this.ITEM_HEIGHT - 1;
			int num5 = this.xScroll;
			int num6 = this.yScroll + i * this.ITEM_HEIGHT;
			int num7 = this.ITEM_HEIGHT - 1;
			if (num3 - this.cmy <= this.yScroll + this.hScroll)
			{
				if (num3 - this.cmy >= this.yScroll - this.ITEM_HEIGHT)
				{
					g.setColor((i != this.selected) ? 15196114 : 16383818);
					if (i == 5)
					{
						g.setColor((i != this.selected) ? 16765060 : 16776068);
					}
					g.fillRect(num2, num3, num4, h);
					g.drawImage(GameScr.imgSkill, num5, num6, 0);
					if (i == 0)
					{
						SmallImage.drawSmallImage(g, 567, num5 + 4, num6 + 4, 0, 0);
						string st = string.Concat(new string[]
						{
							mResources.HP,
							" ",
							mResources.root,
							": ",
							NinjaUtil.getMoneys((long)global::Char.myCharz().cHPGoc)
						});
						mFont.tahoma_7b_blue.drawString(g, st, num2 + 5, num3 + 3, 0);
						mFont.tahoma_7_green2.drawString(g, string.Concat(new object[]
						{
							NinjaUtil.getMoneys((long)(global::Char.myCharz().cHPGoc + 1000)),
							" ",
							mResources.potential,
							": ",
							mResources.increase,
							" ",
							global::Char.myCharz().hpFrom1000TiemNang
						}), num2 + 5, num3 + 15, 0);
					}
					if (i == 1)
					{
						SmallImage.drawSmallImage(g, 569, num5 + 4, num6 + 4, 0, 0);
						string st2 = string.Concat(new string[]
						{
							mResources.KI,
							" ",
							mResources.root,
							": ",
							NinjaUtil.getMoneys((long)global::Char.myCharz().cMPGoc)
						});
						mFont.tahoma_7b_blue.drawString(g, st2, num2 + 5, num3 + 3, 0);
						mFont.tahoma_7_green2.drawString(g, string.Concat(new object[]
						{
							NinjaUtil.getMoneys((long)(global::Char.myCharz().cMPGoc + 1000)),
							" ",
							mResources.potential,
							": ",
							mResources.increase,
							" ",
							global::Char.myCharz().mpFrom1000TiemNang
						}), num2 + 5, num3 + 15, 0);
					}
					if (i == 2)
					{
						SmallImage.drawSmallImage(g, 568, num5 + 4, num6 + 4, 0, 0);
						string st3 = string.Concat(new string[]
						{
							mResources.hit_point,
							" ",
							mResources.root,
							": ",
							NinjaUtil.getMoneys((long)global::Char.myCharz().cDamGoc)
						});
						mFont.tahoma_7b_blue.drawString(g, st3, num2 + 5, num3 + 3, 0);
						mFont.tahoma_7_green2.drawString(g, string.Concat(new object[]
						{
							NinjaUtil.getMoneys((long)(global::Char.myCharz().cDamGoc * 100)),
							" ",
							mResources.potential,
							": ",
							mResources.increase,
							" ",
							global::Char.myCharz().damFrom1000TiemNang
						}), num2 + 5, num3 + 15, 0);
					}
					if (i == 3)
					{
						SmallImage.drawSmallImage(g, 721, num5 + 4, num6 + 4, 0, 0);
						string st4 = string.Concat(new string[]
						{
							mResources.armor,
							" ",
							mResources.root,
							": ",
							NinjaUtil.getMoneys((long)global::Char.myCharz().cDefGoc)
						});
						mFont.tahoma_7b_blue.drawString(g, st4, num2 + 5, num3 + 3, 0);
						mFont.tahoma_7_green2.drawString(g, string.Concat(new object[]
						{
							NinjaUtil.getMoneys((long)(500000 + global::Char.myCharz().cDefGoc * 100000)),
							" ",
							mResources.potential,
							": ",
							mResources.increase,
							" ",
							global::Char.myCharz().defFrom1000TiemNang
						}), num2 + 5, num3 + 15, 0);
					}
					if (i == 4)
					{
						SmallImage.drawSmallImage(g, 719, num5 + 4, num6 + 4, 0, 0);
						string st5 = string.Concat(new object[]
						{
							mResources.critical,
							" ",
							mResources.root,
							": ",
							global::Char.myCharz().cCriticalGoc,
							"%"
						});
						int num8 = global::Char.myCharz().cCriticalGoc;
						if (num8 > Panel.t_tiemnang.Length - 1)
						{
							num8 = Panel.t_tiemnang.Length - 1;
						}
						long num9 = Panel.t_tiemnang[num8];
						mFont.tahoma_7b_blue.drawString(g, st5, num2 + 5, num3 + 3, 0);
						long number = num9;
						mFont.tahoma_7_green2.drawString(g, string.Concat(new object[]
						{
							Res.formatNumber2(number),
							" ",
							mResources.potential,
							": ",
							mResources.increase,
							" ",
							global::Char.myCharz().criticalFrom1000Tiemnang
						}), num2 + 5, num3 + 15, 0);
					}
					if (i == 5)
					{
						if (Panel.specialInfo != null)
						{
							SmallImage.drawSmallImage(g, (int)Panel.spearcialImage, num5 + 4, num6 + 4, 0, 0);
							string[] array = mFont.tahoma_7.splitFontArray(Panel.specialInfo, 120);
							for (int j = 0; j < array.Length; j++)
							{
								mFont.tahoma_7_green2.drawString(g, array[j], num2 + 5, num3 + 3 + j * 12, 0);
							}
						}
						else
						{
							mFont.tahoma_7_green2.drawString(g, string.Empty, num2 + 5, num3 + 9, 0);
						}
					}
					if (i >= 6)
					{
						int num10 = i - 6;
						SkillTemplate skillTemplate = global::Char.myCharz().nClass.skillTemplates[num10];
						SmallImage.drawSmallImage(g, skillTemplate.iconId, num5 + 4, num6 + 4, 0, 0);
						Skill skill = global::Char.myCharz().getSkill(skillTemplate);
						if (skill != null)
						{
							mFont.tahoma_7b_blue.drawString(g, skillTemplate.name, num2 + 5, num3 + 3, 0);
							mFont.tahoma_7_blue.drawString(g, mResources.level + ": " + skill.point, num2 + num4 - 5, num3 + 3, mFont.RIGHT);
							if (skill.point == skillTemplate.maxPoint)
							{
								mFont.tahoma_7_green2.drawString(g, mResources.max_level_reach, num2 + 5, num3 + 15, 0);
							}
							else if (skill.template.isSkillSpec())
							{
								string text = mResources.proficiency + ": ";
								int x = mFont.tahoma_7_green2.getWidthExactOf(text) + num2 + 5;
								int num11 = num3 + 15;
								mFont.tahoma_7_green2.drawString(g, text, num2 + 5, num11, 0);
								mFont.tahoma_7_green2.drawString(g, "(" + skill.strCurExp() + ")", num2 + num4 - 5, num11, mFont.RIGHT);
								num11 += 4;
								g.setColor(7169134);
								g.fillRect(x, num11, 50, 5);
								int num12 = (int)(skill.curExp * 50 / 1000);
								g.setColor(11992374);
								g.fillRect(x, num11, num12, 5);
								if (skill.curExp >= 1000)
								{
								}
							}
							else
							{
								Skill skill2 = skillTemplate.skills[skill.point];
								mFont.tahoma_7_green2.drawString(g, string.Concat(new object[]
								{
									mResources.level,
									" ",
									skill.point + 1,
									" ",
									mResources.need,
									" ",
									Res.formatNumber2(skill2.powRequire),
									" ",
									mResources.potential
								}), num2 + 5, num3 + 15, 0);
							}
						}
						else
						{
							Skill skill3 = skillTemplate.skills[0];
							mFont.tahoma_7b_green.drawString(g, skillTemplate.name, num2 + 5, num3 + 3, 0);
							mFont.tahoma_7_green2.drawString(g, string.Concat(new string[]
							{
								mResources.need_upper,
								" ",
								Res.formatNumber2(skill3.powRequire),
								" ",
								mResources.potential_to_learn
							}), num2 + 5, num3 + 15, 0);
						}
					}
				}
			}
		}
		this.paintScrollArrow(g);
	}

	// Token: 0x060008D5 RID: 2261 RVA: 0x0007E4EC File Offset: 0x0007C8EC
	private void paintMapTrans(mGraphics g)
	{
		g.setColor(16711680);
		g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
		g.translate(0, -this.cmy);
		for (int i = 0; i < this.mapNames.Length; i++)
		{
			int num = this.xScroll + 36;
			int num2 = this.yScroll + i * this.ITEM_HEIGHT;
			int num3 = this.wScroll - 36;
			int h = this.ITEM_HEIGHT - 1;
			int num4 = this.xScroll;
			int num5 = this.yScroll + i * this.ITEM_HEIGHT;
			int num6 = this.ITEM_HEIGHT - 1;
			if (num2 - this.cmy <= this.yScroll + this.hScroll)
			{
				if (num2 - this.cmy >= this.yScroll - this.ITEM_HEIGHT)
				{
					g.setColor((i != this.selected) ? 15196114 : 16383818);
					g.fillRect(this.xScroll, num2, this.wScroll, h);
					mFont.tahoma_7b_blue.drawString(g, this.mapNames[i], 5, num2 + 1, 0);
					mFont.tahoma_7_grey.drawString(g, this.planetNames[i], 5, num2 + 11, 0);
				}
			}
		}
		this.paintScrollArrow(g);
	}

	// Token: 0x060008D6 RID: 2262 RVA: 0x0007E64C File Offset: 0x0007CA4C
	private void paintZone(mGraphics g)
	{
		g.setColor(16711680);
		g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
		g.translate(0, -this.cmy);
		int[] zones = GameScr.gI().zones;
		int[] pts = GameScr.gI().pts;
		for (int i = 0; i < pts.Length; i++)
		{
			int num = this.xScroll + 36;
			int num2 = this.yScroll + i * this.ITEM_HEIGHT;
			int num3 = this.wScroll - 36;
			int h = this.ITEM_HEIGHT - 1;
			int num4 = this.xScroll;
			int y = this.yScroll + i * this.ITEM_HEIGHT;
			int num5 = 34;
			int h2 = this.ITEM_HEIGHT - 1;
			if (num2 - this.cmy <= this.yScroll + this.hScroll)
			{
				if (num2 - this.cmy >= this.yScroll - this.ITEM_HEIGHT)
				{
					g.setColor((i != this.selected) ? 15196114 : 16383818);
					g.fillRect(num, num2, num3, h);
					g.setColor(this.zoneColor[pts[i]]);
					g.fillRect(num4, y, num5, h2);
					if (zones[i] != -1)
					{
						if (pts[i] != 1)
						{
							mFont.tahoma_7_yellow.drawString(g, zones[i] + string.Empty, num4 + num5 / 2, num2 + 6, mFont.CENTER);
						}
						else
						{
							mFont.tahoma_7_grey.drawString(g, zones[i] + string.Empty, num4 + num5 / 2, num2 + 6, mFont.CENTER);
						}
						mFont.tahoma_7_green2.drawString(g, GameScr.gI().numPlayer[i] + "/" + GameScr.gI().maxPlayer[i], num + 5, num2 + 6, 0);
					}
					if (GameScr.gI().rankName1[i] != null)
					{
						mFont.tahoma_7_grey.drawString(g, string.Concat(new object[]
						{
							GameScr.gI().rankName1[i],
							"(Top ",
							GameScr.gI().rank1[i],
							")"
						}), num + num3 - 2, num2 + 1, mFont.RIGHT);
						mFont.tahoma_7_grey.drawString(g, string.Concat(new object[]
						{
							GameScr.gI().rankName2[i],
							"(Top ",
							GameScr.gI().rank2[i],
							")"
						}), num + num3 - 2, num2 + 11, mFont.RIGHT);
					}
				}
			}
		}
		this.paintScrollArrow(g);
	}

	// Token: 0x060008D7 RID: 2263 RVA: 0x0007E918 File Offset: 0x0007CD18
	private void paintSpeacialSkill(mGraphics g)
	{
		g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
		g.translate(0, -this.cmy);
		g.setColor(0);
		if (this.currentListLength == 0)
		{
			return;
		}
		int num = (this.cmy + this.hScroll) / 24 + 1;
		if (num < this.hScroll / 24 + 1)
		{
			num = this.hScroll / 24 + 1;
		}
		if (num > this.currentListLength)
		{
			num = this.currentListLength;
		}
		int num2 = this.cmy / 24;
		if (num2 >= num)
		{
			num2 = num - 1;
		}
		if (num2 < 0)
		{
			num2 = 0;
		}
		for (int i = num2; i < num; i++)
		{
			int num3 = this.xScroll;
			int num4 = this.yScroll + i * this.ITEM_HEIGHT;
			int num5 = 24;
			int num6 = this.ITEM_HEIGHT - 1;
			int num7 = this.xScroll + num5;
			int num8 = this.yScroll + i * this.ITEM_HEIGHT;
			int num9 = this.wScroll - num5;
			int h = this.ITEM_HEIGHT - 1;
			g.setColor((i != this.selected) ? 15196114 : 16383818);
			g.fillRect(num7, num8, num9, h);
			g.setColor((i != this.selected) ? 9993045 : 9541120);
			g.fillRect(num3, num4, num5, num6);
			SmallImage.drawSmallImage(g, (int)global::Char.myCharz().imgSpeacialSkill[this.currentTabIndex][i], num3 + num5 / 2, num4 + num6 / 2, 0, 3);
			string[] array = mFont.tahoma_7_grey.splitFontArray(global::Char.myCharz().infoSpeacialSkill[this.currentTabIndex][i], 140);
			for (int j = 0; j < array.Length; j++)
			{
				mFont.tahoma_7_grey.drawString(g, array[j], num7 + 5, num8 + 1 + j * 11, 0);
			}
		}
		this.paintScrollArrow(g);
	}

	// Token: 0x060008D8 RID: 2264 RVA: 0x0007EB18 File Offset: 0x0007CF18
	private void paintBox(mGraphics g)
	{
		g.setColor(16711680);
		g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
		g.translate(0, -this.cmy);
		try
		{
			Item[] arrItemBox = global::Char.myCharz().arrItemBox;
			this.currentListLength = this.checkCurrentListLength(arrItemBox.Length);
			int num = arrItemBox.Length / 20 + ((arrItemBox.Length % 20 <= 0) ? 0 : 1);
			this.TAB_W_NEW = this.wScroll / num;
			for (int i = 0; i < this.currentListLength; i++)
			{
				int num2 = this.xScroll + 36;
				int num3 = this.yScroll + i * this.ITEM_HEIGHT;
				int num4 = this.wScroll - 36;
				int h = this.ITEM_HEIGHT - 1;
				int num5 = this.xScroll;
				int num6 = this.yScroll + i * this.ITEM_HEIGHT;
				int num7 = 34;
				int num8 = this.ITEM_HEIGHT - 1;
				if (num3 - this.cmy <= this.yScroll + this.hScroll)
				{
					if (num3 - this.cmy >= this.yScroll - this.ITEM_HEIGHT)
					{
						if (i == 0)
						{
							for (int j = 0; j < num; j++)
							{
								int num9 = (j != this.newSelected || this.selected != 0) ? 0 : ((GameCanvas.gameTick % 10 >= 7) ? 0 : -1);
								g.setColor((j != this.newSelected) ? 15723751 : 16383818);
								g.fillRect(this.xScroll + j * this.TAB_W_NEW, num3 + 9 + num9, this.TAB_W_NEW - 1, 14);
								mFont.tahoma_7_grey.drawString(g, string.Empty + j, this.xScroll + j * this.TAB_W_NEW + this.TAB_W_NEW / 2, this.yScroll + 11 + num9, mFont.CENTER);
							}
						}
						else
						{
							g.setColor((i != this.selected) ? 15196114 : 16383818);
							g.fillRect(num2, num3, num4, h);
							g.setColor((i != this.selected) ? 9993045 : 9541120);
							int inventorySelect_body = this.GetInventorySelect_body(i, this.newSelected);
							Item item = arrItemBox[inventorySelect_body];
							if (item != null)
							{
								for (int k = 0; k < item.itemOption.Length; k++)
								{
									if (item.itemOption[k].optionTemplate.id == 72 && item.itemOption[k].param > 0)
									{
										sbyte color_Item_Upgrade = Panel.GetColor_Item_Upgrade(item.itemOption[k].param);
										int color_ItemBg = Panel.GetColor_ItemBg((int)color_Item_Upgrade);
										if (color_ItemBg != -1)
										{
											g.setColor((i != this.selected) ? Panel.GetColor_ItemBg((int)color_Item_Upgrade) : Panel.GetColor_ItemBg((int)color_Item_Upgrade));
										}
									}
								}
							}
							g.fillRect(num5, num6, num7, num8);
							if (item != null)
							{
								string str = string.Empty;
								mFont mFont = mFont.tahoma_7_green2;
								if (item.itemOption != null)
								{
									for (int l = 0; l < item.itemOption.Length; l++)
									{
										if (item.itemOption[l].optionTemplate.id == 72)
										{
											str = " [+" + item.itemOption[l].getOptionString() + "]";
											item.paintColorItem(g, num5 + 18, num6 + 12, item.itemOption[l].param);
										}

										else if (item.itemOption[l].optionTemplate.id == 107)
										{

											mFont.tahoma_7b_red.drawString(g, item.itemOption[l].param.ToString(), num2 + 121, num3 - 1, 0);
										}
										if (item.itemOption[l].optionTemplate.id == 41)
										{
											if (item.itemOption[l].param == 1)
											{
												mFont = Panel.GetFont(0);
											}
											else if (item.itemOption[l].param == 2)
											{
												mFont = Panel.GetFont(2);
											}
											else if (item.itemOption[l].param == 3)
											{
												mFont = Panel.GetFont(8);
											}
											else if (item.itemOption[l].param == 4)
											{
												mFont = Panel.GetFont(7);
											}
										}
									}
								}
								mFont.drawString(g, item.template.name + str, num2 + 5, num3 + 1, 0);
								string text = string.Empty;
								if (item.itemOption != null)
								{
									if (item.itemOption.Length > 0 && item.itemOption[0] != null)
									{
										text += item.itemOption[0].getOptionString();
									}
									mFont mFont2 = mFont.tahoma_7_blue;
									if (item.compare < 0 && (int)item.template.type != 5)
									{
										mFont2 = mFont.tahoma_7_red;
									}
									if (item.itemOption.Length > 1)
									{
										for (int m = 1; m < item.itemOption.Length; m++)
										{
											if (item.itemOption[m] != null && item.itemOption[m].optionTemplate.id != 102 && item.itemOption[m].optionTemplate.id != 107)
											{
												text = text + "," + item.itemOption[m].getOptionString();
											}
										}
									}
									mFont2.drawString(g, text, num2 + 5, num3 + 11, mFont.LEFT);
								}
								SmallImage.drawSmallImage(g, (int)item.template.iconID, num5 + num7 / 2, num6 + num8 / 2, 0, 3);
								if (item.itemOption != null)
								{
									for (int n = 0; n < item.itemOption.Length; n++)
									{
										this.paintOptItem(g, item.itemOption[n].optionTemplate.id, item.itemOption[n].param, num5, num6, num7, num8);
									}
									for (int num10 = 0; num10 < item.itemOption.Length; num10++)
									{
										this.paintOptSlotItem(g, item.itemOption[num10].optionTemplate.id, item.itemOption[num10].param, num5, num6, num7, num8);
									}
								}
								if (item.quantity > 1)
								{
									mFont.tahoma_7_yellow.drawString(g, string.Empty + item.quantity, num5 + num7, num6 + num8 - mFont.tahoma_7_yellow.getHeight(), 1);
								}
							}
						}
					}
				}
			}
		}
		catch (Exception ex)
		{
		}
		this.paintScrollArrow(g);
	}

	// Token: 0x060008D9 RID: 2265 RVA: 0x0007F1E0 File Offset: 0x0007D5E0
	public Member getCurrMember()
	{
		if (this.selected < 2)
		{
			return null;
		}
		if (this.selected > ((this.member == null) ? this.myMember.size() : this.member.size()) + 1)
		{
			return null;
		}
		return (this.member == null) ? ((Member)this.myMember.elementAt(this.selected - 2)) : ((Member)this.member.elementAt(this.selected - 2));
	}

	// Token: 0x060008DA RID: 2266 RVA: 0x0007F270 File Offset: 0x0007D670
	public ClanMessage getCurrMessage()
	{
		if (this.selected < 2)
		{
			return null;
		}
		if (this.selected > ClanMessage.vMessage.size() + 1)
		{
			return null;
		}
		return (ClanMessage)ClanMessage.vMessage.elementAt(this.selected - 2);
	}

	// Token: 0x060008DB RID: 2267 RVA: 0x0007F2B0 File Offset: 0x0007D6B0
	public Clan getCurrClan()
	{
		if (this.selected < 2)
		{
			return null;
		}
		if (this.selected > this.clans.Length + 1)
		{
			return null;
		}
		return this.clans[this.selected - 2];
	}

	// Token: 0x060008DC RID: 2268 RVA: 0x0007F2E8 File Offset: 0x0007D6E8
	private void paintLogChat(mGraphics g)
	{
		g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
		g.translate(0, -this.cmy);
		g.setColor(0);
		if (this.logChat.size() == 0)
		{
			mFont.tahoma_7_green2.drawString(g, mResources.no_msg, this.xScroll + this.wScroll / 2, this.yScroll + this.hScroll / 2 - mFont.tahoma_7.getHeight() / 2 + 24, 2);
		}
		for (int i = 0; i < this.currentListLength; i++)
		{
			int num = this.xScroll;
			int num2 = this.yScroll + i * this.ITEM_HEIGHT;
			int num3 = 24;
			int h = this.ITEM_HEIGHT - 1;
			int num4 = this.xScroll + num3;
			int num5 = this.yScroll + i * this.ITEM_HEIGHT;
			int num6 = this.wScroll - num3;
			int num7 = this.ITEM_HEIGHT - 1;
			if (i == 0)
			{
				g.setColor(15196114);
				g.fillRect(num, num5, this.wScroll, num7);
				g.drawImage((i != this.selected) ? GameScr.imgLbtn2 : GameScr.imgLbtnFocus2, this.xScroll + this.wScroll - 5, num5 + 2, StaticObj.TOP_RIGHT);
				((i != this.selected) ? mFont.tahoma_7b_dark : mFont.tahoma_7b_green2).drawString(g, (!this.isViewChatServer) ? mResources.on : mResources.off, this.xScroll + this.wScroll - 22, num5 + 7, 2);
				mFont.tahoma_7_grey.drawString(g, (!this.isViewChatServer) ? mResources.onPlease : mResources.offPlease, this.xScroll + 5, num5 + num7 / 2 - 4, mFont.LEFT);
			}
			else
			{
				g.setColor((i != this.selected) ? 15196114 : 16383818);
				g.fillRect(num4, num5, num6, num7);
				g.setColor((i != this.selected) ? 9993045 : 9541120);
				g.fillRect(num, num2, num3, h);
				InfoItem infoItem = (InfoItem)this.logChat.elementAt(i - 1);
				if (infoItem.charInfo.headICON != -1)
				{
					SmallImage.drawSmallImage(g, infoItem.charInfo.headICON, num, num2, 0, 0);
				}
				else
				{
					Part part = GameScr.parts[infoItem.charInfo.head];
					SmallImage.drawSmallImage(g, (int)part.pi[global::Char.CharInfo[0][0][0]].id, num + (int)part.pi[global::Char.CharInfo[0][0][0]].dx, num2 + (int)part.pi[global::Char.CharInfo[0][0][0]].dy, 0, 0);
				}
				g.setClip(this.xScroll, this.yScroll + this.cmy, this.wScroll, this.hScroll);
				mFont mFont = mFont.tahoma_7b_dark;
				mFont = mFont.tahoma_7b_green2;
				mFont.drawString(g, infoItem.charInfo.cName, num4 + 5, num5, 0);
				if (!infoItem.isChatServer)
				{
					mFont.tahoma_7_blue.drawString(g, Res.split(infoItem.s, "|", 0)[2], num4 + 5, num5 + 11, 0);
				}
				else
				{
					mFont.tahoma_7_red.drawString(g, Res.split(infoItem.s, "|", 0)[2], num4 + 5, num5 + 11, 0);
				}
			}
		}
		this.paintScrollArrow(g);
	}

	// Token: 0x060008DD RID: 2269 RVA: 0x0007F68C File Offset: 0x0007DA8C
	private void paintFlagChange(mGraphics g)
	{
		g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
		g.translate(0, -this.cmy);
		g.setColor(0);
		for (int i = 0; i < this.currentListLength; i++)
		{
			int num = this.xScroll + 26;
			int num2 = this.yScroll + i * this.ITEM_HEIGHT;
			int num3 = this.wScroll - 26;
			int h = this.ITEM_HEIGHT - 1;
			int num4 = this.xScroll;
			int num5 = this.yScroll + i * this.ITEM_HEIGHT;
			int num6 = 24;
			int num7 = this.ITEM_HEIGHT - 1;
			if (num2 - this.cmy <= this.yScroll + this.hScroll)
			{
				if (num2 - this.cmy >= this.yScroll - this.ITEM_HEIGHT)
				{
					g.setColor((i != this.selected) ? 15196114 : 16383818);
					g.fillRect(num, num2, num3, h);
					g.setColor((i != this.selected) ? 9993045 : 9541120);
					g.fillRect(num4, num5, num6, num7);
					Item item = (Item)this.vFlag.elementAt(i);
					if (item != null)
					{
						mFont.tahoma_7_green2.drawString(g, item.template.name, num + 5, num2 + 1, 0);
						string text = string.Empty;
						if (item.itemOption != null && item.itemOption.Length >= 1)
						{
							if (item.itemOption[0] != null && item.itemOption[0].optionTemplate.id != 102 && item.itemOption[0].optionTemplate.id != 107)
							{
								text += item.itemOption[0].getOptionString();
							}
							mFont tahoma_7_blue = mFont.tahoma_7_blue;
							tahoma_7_blue.drawString(g, text, num + 5, num2 + 11, 0);
							SmallImage.drawSmallImage(g, (int)item.template.iconID, num4 + num6 / 2, num5 + num7 / 2, 0, 3);
						}
					}
				}
			}
		}
		this.paintScrollArrow(g);
	}

	// Token: 0x060008DE RID: 2270 RVA: 0x0007F8C8 File Offset: 0x0007DCC8
	private void paintEnemy(mGraphics g)
	{
		g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
		g.translate(0, -this.cmy);
		g.setColor(0);
		if (this.currentListLength == 0)
		{
			mFont.tahoma_7_green2.drawString(g, mResources.no_enemy, this.xScroll + this.wScroll / 2, this.yScroll + this.hScroll / 2 - mFont.tahoma_7.getHeight() / 2, 2);
			return;
		}
		for (int i = 0; i < this.currentListLength; i++)
		{
			int num = this.xScroll;
			int num2 = this.yScroll + i * this.ITEM_HEIGHT;
			int num3 = 24;
			int h = this.ITEM_HEIGHT - 1;
			int num4 = this.xScroll + num3;
			int num5 = this.yScroll + i * this.ITEM_HEIGHT;
			int num6 = this.wScroll - num3;
			int h2 = this.ITEM_HEIGHT - 1;
			g.setColor((i != this.selected) ? 15196114 : 16383818);
			g.fillRect(num4, num5, num6, h2);
			g.setColor((i != this.selected) ? 9993045 : 9541120);
			g.fillRect(num, num2, num3, h);
			InfoItem infoItem = (InfoItem)this.vEnemy.elementAt(i);
			if (infoItem.charInfo.headICON != -1)
			{
				SmallImage.drawSmallImage(g, infoItem.charInfo.headICON, num, num2, 0, 0);
			}
			else
			{
				Part part = GameScr.parts[infoItem.charInfo.head];
				SmallImage.drawSmallImage(g, (int)part.pi[global::Char.CharInfo[0][0][0]].id, num + (int)part.pi[global::Char.CharInfo[0][0][0]].dx, num2 + 3 + (int)part.pi[global::Char.CharInfo[0][0][0]].dy, 0, 0);
			}
			g.setClip(this.xScroll, this.yScroll + this.cmy, this.wScroll, this.hScroll);
			if (infoItem.isOnline)
			{
				mFont.tahoma_7b_green.drawString(g, infoItem.charInfo.cName, num4 + 5, num5, 0);
				mFont.tahoma_7_blue.drawString(g, infoItem.s, num4 + 5, num5 + 11, 0);
			}
			else
			{
				mFont.tahoma_7_grey.drawString(g, infoItem.charInfo.cName, num4 + 5, num5, 0);
				mFont.tahoma_7_grey.drawString(g, infoItem.s, num4 + 5, num5 + 11, 0);
			}
		}
		this.paintScrollArrow(g);
	}

	// Token: 0x060008DF RID: 2271 RVA: 0x0007FB74 File Offset: 0x0007DF74
	private void paintFriend(mGraphics g)
	{
		g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
		g.translate(0, -this.cmy);
		g.setColor(0);
		if (this.currentListLength == 0)
		{
			mFont.tahoma_7_green2.drawString(g, mResources.no_friend, this.xScroll + this.wScroll / 2, this.yScroll + this.hScroll / 2 - mFont.tahoma_7.getHeight() / 2, 2);
			return;
		}
		for (int i = 0; i < this.currentListLength; i++)
		{
			int num = this.xScroll;
			int num2 = this.yScroll + i * this.ITEM_HEIGHT;
			int num3 = 24;
			int h = this.ITEM_HEIGHT - 1;
			int num4 = this.xScroll + num3;
			int num5 = this.yScroll + i * this.ITEM_HEIGHT;
			int num6 = this.wScroll - num3;
			int h2 = this.ITEM_HEIGHT - 1;
			g.setColor((i != this.selected) ? 15196114 : 16383818);
			g.fillRect(num4, num5, num6, h2);
			g.setColor((i != this.selected) ? 9993045 : 9541120);
			g.fillRect(num, num2, num3, h);
			InfoItem infoItem = (InfoItem)this.vFriend.elementAt(i);
			if (infoItem.charInfo.headICON != -1)
			{
				SmallImage.drawSmallImage(g, infoItem.charInfo.headICON, num, num2, 0, 0);
			}
			else
			{
				Part part = GameScr.parts[infoItem.charInfo.head];
				SmallImage.drawSmallImage(g, (int)part.pi[global::Char.CharInfo[0][0][0]].id, num + (int)part.pi[global::Char.CharInfo[0][0][0]].dx, num2 + 3 + (int)part.pi[global::Char.CharInfo[0][0][0]].dy, 0, 0);
			}
			g.setClip(this.xScroll, this.yScroll + this.cmy, this.wScroll, this.hScroll);
			if (infoItem.isOnline)
			{
				mFont.tahoma_7b_green.drawString(g, infoItem.charInfo.cName, num4 + 5, num5, 0);
				mFont.tahoma_7_blue.drawString(g, infoItem.s, num4 + 5, num5 + 11, 0);
			}
			else
			{
				mFont.tahoma_7_grey.drawString(g, infoItem.charInfo.cName, num4 + 5, num5, 0);
				mFont.tahoma_7_grey.drawString(g, infoItem.s, num4 + 5, num5 + 11, 0);
			}
		}
		this.paintScrollArrow(g);
	}

	// Token: 0x060008E0 RID: 2272 RVA: 0x0007FE20 File Offset: 0x0007E220
	public void paintPlayerMenu(mGraphics g)
	{
		g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
		g.translate(0, -this.cmy);
		for (int i = 0; i < this.vPlayerMenu.size(); i++)
		{
			int x = this.xScroll;
			int num = this.yScroll + i * this.ITEM_HEIGHT;
			int num2 = this.wScroll - 1;
			int h = this.ITEM_HEIGHT - 1;
			if (num - this.cmy <= this.yScroll + this.hScroll)
			{
				if (num - this.cmy >= this.yScroll - this.ITEM_HEIGHT)
				{
					Command command = (Command)this.vPlayerMenu.elementAt(i);
					g.setColor((i != this.selected) ? 15196114 : 16383818);
					g.fillRect(x, num, num2, h);
					if (command.caption2.Equals(string.Empty))
					{
						mFont.tahoma_7b_dark.drawString(g, command.caption, this.xScroll + this.wScroll / 2, num + 6, mFont.CENTER);
					}
					else
					{
						mFont.tahoma_7b_dark.drawString(g, command.caption, this.xScroll + this.wScroll / 2, num + 1, mFont.CENTER);
						mFont.tahoma_7b_dark.drawString(g, command.caption2, this.xScroll + this.wScroll / 2, num + 11, mFont.CENTER);
					}
				}
			}
		}
		this.paintScrollArrow(g);
	}

	// Token: 0x060008E1 RID: 2273 RVA: 0x0007FFBC File Offset: 0x0007E3BC
	private void paintClans(mGraphics g)
	{
		g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
		g.translate(-this.cmx, -this.cmy);
		g.setColor(0);
		int num = this.xScroll + this.wScroll / 2 - this.clansOption.Length * this.TAB_W / 2;
		if (this.currentListLength == 2)
		{
			mFont.tahoma_7_green2.drawString(g, this.clanReport, this.xScroll + this.wScroll / 2, this.yScroll + 24 + this.hScroll / 2 - mFont.tahoma_7.getHeight() / 2, 2);
			if (this.isMessage && this.myMember.size() == 1)
			{
				for (int i = 0; i < mResources.clanEmpty.Length; i++)
				{
					mFont.tahoma_7b_dark.drawString(g, mResources.clanEmpty[i], this.xScroll + this.wScroll / 2, this.yScroll + 24 + this.hScroll / 2 - mResources.clanEmpty.Length * 12 / 2 + i * 12, mFont.CENTER);
				}
			}
		}
		if (this.isMessage)
		{
			this.currentListLength = ClanMessage.vMessage.size() + 2;
		}
		for (int j = 0; j < this.currentListLength; j++)
		{
			int num2 = this.xScroll;
			int num3 = this.yScroll + j * this.ITEM_HEIGHT;
			int num4 = 24;
			int num5 = this.ITEM_HEIGHT - 1;
			int num6 = this.xScroll + num4;
			int num7 = this.yScroll + j * this.ITEM_HEIGHT;
			int num8 = this.wScroll - num4;
			int num9 = this.ITEM_HEIGHT - 1;
			if (num7 - this.cmy <= this.yScroll + this.hScroll)
			{
				if (num7 - this.cmy >= this.yScroll - this.ITEM_HEIGHT)
				{
					if (j == 0)
					{
						for (int k = 0; k < this.clansOption.Length; k++)
						{
							g.setColor((k != this.cSelected || j != this.selected) ? 15723751 : 16383818);
							g.fillRect(num + k * this.TAB_W, num7, this.TAB_W - 1, 23);
							for (int l = 0; l < this.clansOption[k].Length; l++)
							{
								mFont.tahoma_7_grey.drawString(g, this.clansOption[k][l], num + k * this.TAB_W + this.TAB_W / 2, this.yScroll + l * 11, mFont.CENTER);
							}
						}
					}
					else if (j == 1)
					{
						g.setColor((j != this.selected) ? 15196114 : 16383818);
						g.fillRect(this.xScroll, num7, this.wScroll, num9);
						if (this.clanInfo != null)
						{
							mFont.tahoma_7b_dark.drawString(g, this.clanInfo, this.xScroll + this.wScroll / 2, num7 + 6, mFont.CENTER);
						}
					}
					else if (this.isSearchClan)
					{
						if (this.clans != null)
						{
							if (this.clans.Length != 0)
							{
								g.setColor((j != this.selected) ? 15196114 : 16383818);
								g.fillRect(num6, num7, num8, num9);
								g.setColor((j != this.selected) ? 9993045 : 9541120);
								g.fillRect(num2, num3, num4, num5);
								if (ClanImage.isExistClanImage(this.clans[j - 2].imgID))
								{
									if (ClanImage.getClanImage((short)this.clans[j - 2].imgID).idImage != null)
									{
										SmallImage.drawSmallImage(g, (int)ClanImage.getClanImage((short)this.clans[j - 2].imgID).idImage[0], num2 + num4 / 2, num3 + num5 / 2, 0, StaticObj.VCENTER_HCENTER);
									}
								}
								else
								{
									ClanImage clanImage = new ClanImage();
									clanImage.ID = this.clans[j - 2].imgID;
									if (!ClanImage.isExistClanImage(clanImage.ID))
									{
										ClanImage.addClanImage(clanImage);
									}
								}
								string st = (this.clans[j - 2].name.Length <= 23) ? this.clans[j - 2].name : (this.clans[j - 2].name.Substring(0, 23) + "...");
								mFont.tahoma_7b_green2.drawString(g, st, num6 + 5, num7, 0);
								g.setClip(num6, num7, num8 - 10, num9);
								mFont.tahoma_7_blue.drawString(g, this.clans[j - 2].slogan, num6 + 5, num7 + 11, 0);
								g.setClip(this.xScroll, this.yScroll + this.cmy, this.wScroll, this.hScroll);
								mFont.tahoma_7_green2.drawString(g, this.clans[j - 2].currMember + "/" + this.clans[j - 2].maxMember, num6 + num8 - 5, num7, mFont.RIGHT);
							}
						}
					}
					else if (this.isViewMember)
					{
						g.setColor((j != this.selected) ? 15196114 : 16383818);
						g.fillRect(num6, num7, num8, num9);
						g.setColor((j != this.selected) ? 9993045 : 9541120);
						g.fillRect(num2, num3, num4, num5);
						Member member = (this.member == null) ? ((Member)this.myMember.elementAt(j - 2)) : ((Member)this.member.elementAt(j - 2));
						if (member.headICON != -1)
						{
							SmallImage.drawSmallImage(g, (int)member.headICON, num2, num3, 0, 0);
						}
						else
						{
							Part part = GameScr.parts[(int)member.head];
							SmallImage.drawSmallImage(g, (int)part.pi[global::Char.CharInfo[0][0][0]].id, num2 + (int)part.pi[global::Char.CharInfo[0][0][0]].dx, num3 + 3 + (int)part.pi[global::Char.CharInfo[0][0][0]].dy, 0, 0);
						}
						g.setClip(this.xScroll, this.yScroll + this.cmy, this.wScroll, this.hScroll);
						mFont mFont = mFont.tahoma_7b_dark;
						if ((int)member.role == 0)
						{
							mFont = mFont.tahoma_7b_red;
						}
						else if ((int)member.role == 1)
						{
							mFont = mFont.tahoma_7b_green;
						}
						else if ((int)member.role == 2)
						{
							mFont = mFont.tahoma_7b_green2;
						}
						mFont.drawString(g, member.name, num6 + 5, num7, 0);
						mFont.tahoma_7_blue.drawString(g, mResources.power + ": " + member.powerPoint, num6 + 5, num7 + 11, 0);
						SmallImage.drawSmallImage(g, 7223, num6 + num8 - 7, num7 + 12, 0, 3);
						mFont.tahoma_7_blue.drawString(g, string.Empty + member.clanPoint, num6 + num8 - 15, num7 + 6, mFont.RIGHT);
					}
					else if (this.isMessage && ClanMessage.vMessage.size() != 0)
					{
						ClanMessage clanMessage = (ClanMessage)ClanMessage.vMessage.elementAt(j - 2);
						g.setColor((j != this.selected || clanMessage.option != null) ? 15196114 : 16383818);
						g.fillRect(num2, num3, num8 + num4, num9);
						clanMessage.paint(g, num2, num3);
						if (clanMessage.option != null)
						{
							int num10 = this.xScroll + this.wScroll - 2 - clanMessage.option.Length * 40;
							for (int m = 0; m < clanMessage.option.Length; m++)
							{
								if (m == this.cSelected && j == this.selected)
								{
									g.drawImage(GameScr.imgLbtnFocus2, num10 + m * 40 + 20, num7 + num9 / 2, StaticObj.VCENTER_HCENTER);
									mFont.tahoma_7b_green2.drawString(g, clanMessage.option[m], num10 + m * 40 + 20, num7 + 6, mFont.CENTER);
								}
								else
								{
									g.drawImage(GameScr.imgLbtn2, num10 + m * 40 + 20, num7 + num9 / 2, StaticObj.VCENTER_HCENTER);
									mFont.tahoma_7b_dark.drawString(g, clanMessage.option[m], num10 + m * 40 + 20, num7 + 6, mFont.CENTER);
								}
							}
						}
					}
				}
			}
		}
		this.paintScrollArrow(g);
	}

	// Token: 0x060008E2 RID: 2274 RVA: 0x000808E8 File Offset: 0x0007ECE8
	private void paintArchivement(mGraphics g)
	{
		g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
		g.translate(0, -this.cmy);
		g.setColor(0);
		if (this.currentListLength == 0)
		{
			mFont.tahoma_7_green2.drawString(g, mResources.no_mission, this.xScroll + this.wScroll / 2, this.yScroll + this.hScroll / 2 - mFont.tahoma_7.getHeight() / 2, 2);
			return;
		}
		if (global::Char.myCharz().arrArchive == null)
		{
			return;
		}
		if (global::Char.myCharz().arrArchive.Length != this.currentListLength)
		{
			return;
		}
		for (int i = 0; i < this.currentListLength; i++)
		{
			int num = this.xScroll;
			int num2 = this.yScroll + i * this.ITEM_HEIGHT;
			int num3 = this.wScroll;
			int num4 = this.ITEM_HEIGHT - 1;
			Archivement archivement = global::Char.myCharz().arrArchive[i];
			g.setColor((i != this.selected || ((archivement.isRecieve || archivement.isFinish) && (!archivement.isRecieve || !archivement.isFinish))) ? 15196114 : 16383818);
			g.fillRect(num, num2, num3, num4);
			if (archivement != null)
			{
				if (!archivement.isFinish)
				{
					mFont.tahoma_7.drawString(g, archivement.info1, num + 5, num2, 0);
					mFont.tahoma_7_green.drawString(g, archivement.money + " " + mResources.RUBY, num + num3 - 5, num2, mFont.RIGHT);
					mFont.tahoma_7_red.drawString(g, archivement.info2, num + 5, num2 + 11, 0);
				}
				else if (archivement.isFinish && !archivement.isRecieve)
				{
					mFont.tahoma_7.drawString(g, archivement.info1, num + 5, num2, 0);
					mFont.tahoma_7_blue.drawString(g, string.Concat(new object[]
					{
						mResources.reward_mission,
						archivement.money,
						" ",
						mResources.RUBY
					}), num + 5, num2 + 11, 0);
					if (i == this.selected)
					{
						mFont.tahoma_7b_green2.drawString(g, mResources.receive_upper, num + num3 - 20, num2 + 6, mFont.CENTER);
						mFont.tahoma_7b_dark.drawString(g, mResources.receive_upper, num + num3 - 20, num2 + 6, mFont.CENTER);
					}
					else
					{
						g.drawImage(GameScr.imgLbtn2, num + num3 - 20, num2 + num4 / 2, StaticObj.VCENTER_HCENTER);
						mFont.tahoma_7b_dark.drawString(g, mResources.receive_upper, num + num3 - 20, num2 + 6, mFont.CENTER);
					}
				}
				else if (archivement.isFinish && archivement.isRecieve)
				{
					mFont.tahoma_7_green.drawString(g, archivement.info1, num + 5, num2, 0);
					mFont.tahoma_7_green.drawString(g, archivement.info2, num + 5, num2 + 11, 0);
				}
			}
		}
		this.paintScrollArrow(g);
	}
	public static bool getboolOP(Item item)
	{
		if (item == null)
		{
			return false;
		}
		ItemOption[] itemOption = item.itemOption;
		for (int i = 0; i < itemOption.Length; i++)
		{
			int id = itemOption[i].optionTemplate.id;
			if (id <= 77)
			{
				if (id == 50 || id == 77)
				{
					return true;
				}
			}
			else
			{
				if (id - 80 <= 1)
				{
					return true;
				}
				switch (id)
				{
					case 94:
					case 95:
					case 96:
					case 97:
					case 101:
					case 103:
					case 104:
						return true;
				}
			}
		}
		return false;
	}
	public static string getStringOP(Item item)
	{
		if (item == null || !Panel.getboolOP(item) || (item.template.type > 5 && item.template.type != 32))
		{
			return "";
		}
		foreach (ItemOption itemOption2 in item.itemOption)
		{
			int id = itemOption2.optionTemplate.id;
			int param = itemOption2.param;
			if (id == 50)
			{
				return "SD " + param + "%";
			}
			switch (id)
			{
				case 77:
					return "HP " + param + "%";
				case 78:
				case 79:
					break;
				case 80:
					return "HP/30s "+param + "%";
				case 81:
					return "KI/30s "+ param + "%";
				default:
					switch (id)
					{
						case 94:
							return "Giáp "+ param + "%";
						case 95:
							return "HM "+ param + "%";
						case 96:
							return "HK "+ param + "%";
						case 97:
							return "PST " + param + "%";
						case 101:
							return "EXP "+ param + "%";
						case 103:
							return "KI "+ param + "%";
						case 104:
							return "Né" + param + "%";
					}
					break;
			}
		}
		return "";
	}
	// Token: 0x060008E3 RID: 2275 RVA: 0x00080C0C File Offset: 0x0007F00C
	private void paintCombine(mGraphics g)
	{
		g.setColor(16711680);
		g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
		g.translate(0, -this.cmy);
		if (this.vItemCombine.size() == 0)
		{
			if (this.combineInfo != null)
			{
				for (int i = 0; i < this.combineInfo.Length; i++)
				{
					mFont.tahoma_7b_dark.drawString(g, this.combineInfo[i], this.xScroll + this.wScroll / 2, this.yScroll + this.hScroll / 2 - this.combineInfo.Length * 14 / 2 + i * 14 + 5, 2);
				}
			}
			return;
		}
		for (int j = 0; j < this.vItemCombine.size() + 1; j++)
		{
			int num = this.xScroll + 36;
			int num2 = this.yScroll + j * this.ITEM_HEIGHT;
			int num3 = this.wScroll - 36;
			int num4 = this.ITEM_HEIGHT - 1;
			int num5 = this.xScroll;
			int num6 = this.yScroll + j * this.ITEM_HEIGHT;
			int num7 = 34;
			int num8 = this.ITEM_HEIGHT - 1;
			if (num2 - this.cmy <= this.yScroll + this.hScroll)
			{
				if (num2 - this.cmy >= this.yScroll - this.ITEM_HEIGHT)
				{
					if (j == this.vItemCombine.size())
					{
						if (this.vItemCombine.size() > 0)
						{
							if (!GameCanvas.isTouch && j == this.selected)
							{
								g.setColor(16383818);
								g.fillRect(num5, num2, this.wScroll, num4 + 2);
							}
							if ((j == this.selected && this.keyTouchCombine == 1) || (!GameCanvas.isTouch && j == this.selected))
							{
								g.drawImage(GameScr.imgLbtnFocus, this.xScroll + this.wScroll / 2, num2 + num4 / 2 + 1, StaticObj.VCENTER_HCENTER);
								mFont.tahoma_7b_green2.drawString(g, mResources.UPGRADE, this.xScroll + this.wScroll / 2, num2 + num4 / 2 - 4, mFont.CENTER);
							}
							else
							{
								g.drawImage(GameScr.imgLbtn, this.xScroll + this.wScroll / 2, num2 + num4 / 2 + 1, StaticObj.VCENTER_HCENTER);
								mFont.tahoma_7b_dark.drawString(g, mResources.UPGRADE, this.xScroll + this.wScroll / 2, num2 + num4 / 2 - 4, mFont.CENTER);
							}
						}
					}
					else
					{
						g.setColor((j != this.selected) ? 15196114 : 16383818);
						g.fillRect(num, num2, num3, num4);
						g.setColor((j != this.selected) ? 9993045 : 9541120);
						Item item = (Item)this.vItemCombine.elementAt(j);
						if (item != null)
						{
							for (int k = 0; k < item.itemOption.Length; k++)
							{
								if (item.itemOption[k].optionTemplate.id == 72 && item.itemOption[k].param > 0)
								{
									sbyte color_Item_Upgrade = Panel.GetColor_Item_Upgrade(item.itemOption[k].param);
									int color_ItemBg = Panel.GetColor_ItemBg((int)color_Item_Upgrade);
									if (color_ItemBg != -1)
									{
										g.setColor((j != this.selected) ? Panel.GetColor_ItemBg((int)color_Item_Upgrade) : Panel.GetColor_ItemBg((int)color_Item_Upgrade));
									}
								}
							}
						}
						g.fillRect(num5, num6, num7, num8);
						if (item != null)
						{
							string str = string.Empty;
							mFont mFont = mFont.tahoma_7_green2;
							if (item.itemOption != null)
							{
								for (int l = 0; l < item.itemOption.Length; l++)
								{
									if (item.itemOption[l].optionTemplate.id == 72)
									{
										str = " [+" + item.itemOption[l].param + "]";
										item.paintColorItem(g, num5 + 18, num6 + 12, item.itemOption[l].param);
									}

									else if (item.itemOption[l].optionTemplate.id == 107)
									{

										mFont.tahoma_7b_red.drawString(g, item.itemOption[l].param.ToString(), num + 121, num2 - 1, 0);
									}
									if (item.itemOption[l].optionTemplate.id == 41)
									{
										if (item.itemOption[l].param == 1)
										{
											mFont = Panel.GetFont(0);
										}
										else if (item.itemOption[l].param == 2)
										{
											mFont = Panel.GetFont(2);
										}
										else if (item.itemOption[l].param == 3)
										{
											mFont = Panel.GetFont(8);
										}
										else if (item.itemOption[l].param == 4)
										{
											mFont = Panel.GetFont(7);
										}
									}
								}
							}
							mFont.drawString(g, item.template.name + str, num + 5, num2 + 1, 0);
							string text = string.Empty;
							if (item.itemOption != null)
							{
								if (item.itemOption.Length > 0 && item.itemOption[0] != null && item.itemOption[0].optionTemplate.id != 102 && item.itemOption[0].optionTemplate.id != 107)
								{
									text += item.itemOption[0].getOptionString();
								}
								mFont mFont2 = mFont.tahoma_7_blue;
								if (item.compare < 0 && (int)item.template.type != 5)
								{
									mFont2 = mFont.tahoma_7_red;
								}
								if (item.itemOption.Length > 1)
								{
									for (int m = 1; m < item.itemOption.Length; m++)
									{
										if (item.itemOption[m] != null && item.itemOption[m].optionTemplate.id != 102 && item.itemOption[m].optionTemplate.id != 107)
										{
											text = text + "," + item.itemOption[m].getOptionString();
										}
									}
								}
								mFont2.drawString(g, text, num + 5, num2 + 11, mFont.LEFT);
							}
							SmallImage.drawSmallImage(g, (int)item.template.iconID, num5 + num7 / 2, num6 + num8 / 2, 0, 3);
							if (item.itemOption != null)
							{
								for (int n = 0; n < item.itemOption.Length; n++)
								{
									this.paintOptItem(g, item.itemOption[n].optionTemplate.id, item.itemOption[n].param, num5, num6, num7, num8);
								}
								for (int num9 = 0; num9 < item.itemOption.Length; num9++)
								{
									this.paintOptSlotItem(g, item.itemOption[num9].optionTemplate.id, item.itemOption[num9].param, num5, num6, num7, num8);
								}
							}
							if (item.quantity > 1)
							{
								mFont.tahoma_7_yellow.drawString(g, string.Empty + item.quantity, num5 + num7, num6 + num8 - mFont.tahoma_7_yellow.getHeight(), 1);
							}
						}
					}
				}
			}
		}
		this.paintScrollArrow(g);
	}

	// Token: 0x060008E4 RID: 2276 RVA: 0x0008136C File Offset: 0x0007F76C
	private void paintInventory(mGraphics g)
	{
		g.setColor(16711680);
		Item[] arrItemBody = global::Char.myCharz().arrItemBody;
		Item[] arrItemBag = global::Char.myCharz().arrItemBag;
		this.currentListLength = this.checkCurrentListLength(arrItemBody.Length + arrItemBag.Length);
		int num = (arrItemBody.Length + arrItemBag.Length) / 20 + (((arrItemBody.Length + arrItemBag.Length) % 20 <= 0) ? 0 : 1);
		this.TAB_W_NEW = this.wScroll / num;
		for (int i = 0; i < num; i++)
		{
			int num2 = (i != this.newSelected || this.selected != 0) ? 0 : ((GameCanvas.gameTick % 10 >= 7) ? 0 : -1);
			g.setColor((i != this.newSelected) ? 15723751 : 16383818);
			g.fillRect(this.xScroll + i * this.TAB_W_NEW, 89 + num2 - 10, this.TAB_W_NEW - 1, 21);
			if (i == this.newSelected)
			{
				g.setColor(13524492);
				int x = this.xScroll + i * this.TAB_W_NEW;
				int num3 = 89 + num2 - 10 + 21;
				g.fillRect(x, num3 - 3, this.TAB_W_NEW - 1, 3);
			}
			mFont.tahoma_7_grey.drawString(g, string.Empty + (i + 1), this.xScroll + i * this.TAB_W_NEW + this.TAB_W_NEW / 2, 91 + num2 - 10, mFont.CENTER);
		}
		g.setClip(this.xScroll, this.yScroll + 21, this.wScroll, this.hScroll - 21);
		g.translate(0, -this.cmy);
		try
		{
			for (int j = 1; j < this.currentListLength; j++)
			{
				int num4 = this.xScroll + 36;
				int num5 = this.yScroll + j * this.ITEM_HEIGHT;
				int num6 = this.wScroll - 36;
				int h = this.ITEM_HEIGHT - 1;
				int num7 = this.xScroll;
				int num8 = this.yScroll + j * this.ITEM_HEIGHT;
				int num9 = 34;
				int num10 = this.ITEM_HEIGHT - 1;
				if (num5 - this.cmy <= this.yScroll + this.hScroll)
				{
					if (num5 - this.cmy >= this.yScroll - this.ITEM_HEIGHT)
					{
						bool inventorySelect_isbody = this.GetInventorySelect_isbody(j, this.newSelected, global::Char.myCharz().arrItemBody);
						int inventorySelect_body = this.GetInventorySelect_body(j, this.newSelected);
						int inventorySelect_bag = this.GetInventorySelect_bag(j, this.newSelected, global::Char.myCharz().arrItemBody);
						g.setColor((j != this.selected) ? ((!inventorySelect_isbody) ? 15723751 : 15196114) : 16383818);
						g.fillRect(num4, num5, num6, h);
						g.setColor((j != this.selected) ? ((!inventorySelect_isbody) ? 11837316 : 9993045) : 9541120);
						Item item = (!inventorySelect_isbody) ? arrItemBag[inventorySelect_bag] : arrItemBody[inventorySelect_body];
						if (item != null)
						{
							for (int k = 0; k < item.itemOption.Length; k++)
							{
								if (item.itemOption[k].optionTemplate.id == 72 && item.itemOption[k].param > 0)
								{
									byte id = (byte)Panel.GetColor_Item_Upgrade(item.itemOption[k].param);
									int color_ItemBg = Panel.GetColor_ItemBg((int)id);
									if (color_ItemBg != -1)
									{
										g.setColor((j != this.selected) ? Panel.GetColor_ItemBg((int)id) : Panel.GetColor_ItemBg((int)id));
									}
								}
							}
							foreach (Mod.Mod.ItemAuto itemAuto in Mod.Mod.listItemAuto)
							{
								bool flag8 = (int)item.template.id == itemAuto.id && (int)item.template.iconID == itemAuto.iconID;
								if (flag8)
								{
									g.setColor((j != this.selected) ? Panel.color1[1] : Panel.color2[1]);
								}
							}
						}
						g.fillRect(num7, num8, num9, num10);
						if (item != null && item.isSelect && GameCanvas.panel.type == 12)
						{
							g.setColor((j != this.selected) ? 6047789 : 7040779);
							g.fillRect(num7, num8, num9, num10);
						}
						if (item != null)
						{
							string str = string.Empty;
							mFont mFont = mFont.tahoma_7_green2;
							if (item.itemOption != null)
							{
								for (int l = 0; l < item.itemOption.Length; l++)
								{
									if (item.itemOption[l].optionTemplate.id == 72)
									{
										str = " [+" + item.itemOption[l].param + "]";
										item.paintColorItem(g, num7 + 18, num8 + 12, item.itemOption[l].param);
									}
									else if (item.itemOption[l].optionTemplate.id == 107)
									{

										mFont.tahoma_7b_yellow.drawString(g, item.itemOption[l].param.ToString(), num4 + 121, num5 - 1, 0,mFont.tahoma_7_grey);
									}
									if (item.itemOption[l].optionTemplate.id == 41)
									{
										if (item.itemOption[l].param == 1)
										{
											mFont = Panel.GetFont(0);
										}
										else if (item.itemOption[l].param == 2)
										{
											mFont = Panel.GetFont(2);
										}
										else if (item.itemOption[l].param == 3)
										{
											mFont = Panel.GetFont(8);
										}
										else if (item.itemOption[l].param == 4)
										{
											mFont = Panel.GetFont(7);
										}
									}
								}
							}
							mFont.drawString(g, "["+item.template.id.ToString()+"]"+item.template.name + str, num4 + 5, num5 + 1, 0);
						
							string text = string.Empty;
							if (item.itemOption != null)
							{
								if (item.itemOption.Length > 0 && item.itemOption[0] != null && item.itemOption[0].optionTemplate.id != 102 && item.itemOption[0].optionTemplate.id != 107)
								{
									text += item.itemOption[0].getOptionString();
								}
								mFont mFont2 = mFont.tahoma_7_blue;
								if (getStringOP(item) != "")
								{
									mFont.tahoma_7_blue.drawString(g, getStringOP(item), num4 + 105, num5 + 10,0);
								}
								if (item.compare < 0 && (int)item.template.type != 5)
								{
									mFont2 = mFont.tahoma_7_red;
								}
								if (item.itemOption.Length > 1)
								{
									for (int m = 1; m < 2; m++)
									{
										if (item.itemOption[m] != null && item.itemOption[m].optionTemplate.id != 102 && item.itemOption[m].optionTemplate.id != 107 && item.itemOption[m].optionTemplate.id != 107)
										{
											text = text + string.Empty;
										}
									}
								}
								mFont2.drawString(g, text, num4 + 5, num5 + 11, mFont.LEFT);
							}
							SmallImage.drawSmallImage(g, (int)item.template.iconID, num7 + num9 / 2, num8 + num10 / 2, 0, 3);
							if (item.itemOption != null)
							{
								for (int n = 0; n < item.itemOption.Length; n++)
								{
									this.paintOptItem(g, item.itemOption[n].optionTemplate.id, item.itemOption[n].param, num7, num8, num9, num10);
								}
								for (int num11 = 0; num11 < item.itemOption.Length; num11++)
								{
									this.paintOptSlotItem(g, item.itemOption[num11].optionTemplate.id, item.itemOption[num11].param, num7, num8, num9, num10);
								}
							}
							if (item.quantity > 1)
							{
								mFont.tahoma_7_yellow.drawString(g, string.Empty + item.quantity, num7 + num9, num8 + num10 - mFont.tahoma_7_yellow.getHeight(), 1);
							}
						}
					}
				}
			}
		}
		catch (Exception ex)
		{
		}
		this.paintScrollArrow(g);
	}

	// Token: 0x060008E5 RID: 2277 RVA: 0x00081B7C File Offset: 0x0007FF7C
	private void paintTab(mGraphics g)
	{
		if (this.type == 23 || this.type == 24)
		{
			g.setColor(13524492);
			g.fillRect(this.X + 1, 78, this.W - 2, 1);
			mFont.tahoma_7b_dark.drawString(g, mResources.gameInfo, this.xScroll + this.wScroll / 2, 59, mFont.CENTER);
		}
		else if (this.type == 20)
		{
			g.setColor(13524492);
			g.fillRect(this.X + 1, 78, this.W - 2, 1);
			mFont.tahoma_7b_dark.drawString(g, mResources.account, this.xScroll + this.wScroll / 2, 59, mFont.CENTER);
		}
		else if (this.type == 22)
		{
			g.setColor(13524492);
			g.fillRect(this.X + 1, 78, this.W - 2, 1);
			mFont.tahoma_7b_dark.drawString(g, mResources.autoFunction, this.xScroll + this.wScroll / 2, 59, mFont.CENTER);
		}
		else if (this.type == 19)
		{
			g.setColor(13524492);
			g.fillRect(this.X + 1, 78, this.W - 2, 1);
			mFont.tahoma_7b_dark.drawString(g, mResources.option, this.xScroll + this.wScroll / 2, 59, mFont.CENTER);
		}
		else if (this.type == 18)
		{
			g.setColor(13524492);
			g.fillRect(this.X + 1, 78, this.W - 2, 1);
			mFont.tahoma_7b_dark.drawString(g, mResources.change_flag, this.xScroll + this.wScroll / 2, 59, mFont.CENTER);
		}
		else if (this.type == 13 && this.Equals(GameCanvas.panel2))
		{
			g.setColor(13524492);
			g.fillRect(this.X + 1, 78, this.W - 2, 1);
			mFont.tahoma_7b_dark.drawString(g, mResources.item_receive2, this.xScroll + this.wScroll / 2, 59, mFont.CENTER);
		}
		else if (this.type == 12 && GameCanvas.panel2 != null)
		{
			g.setColor(13524492);
			g.fillRect(this.X + 1, 78, this.W - 2, 1);
			mFont.tahoma_7b_dark.drawString(g, mResources.UPGRADE, this.xScroll + this.wScroll / 2, 59, mFont.CENTER);
		}
		else if (this.type == 11)
		{
			g.setColor(13524492);
			g.fillRect(this.X + 1, 78, this.W - 2, 1);
			mFont.tahoma_7b_dark.drawString(g, mResources.friend, this.xScroll + this.wScroll / 2, 59, mFont.CENTER);
		}
		else if (this.type == 16)
		{
			g.setColor(13524492);
			g.fillRect(this.X + 1, 78, this.W - 2, 1);
			mFont.tahoma_7b_dark.drawString(g, mResources.enemy, this.xScroll + this.wScroll / 2, 59, mFont.CENTER);
		}
		else if (this.type == 15)
		{
			g.setColor(13524492);
			g.fillRect(this.X + 1, 78, this.W - 2, 1);
			mFont.tahoma_7b_dark.drawString(g, this.topName, this.xScroll + this.wScroll / 2, 59, mFont.CENTER);
		}
		else if (this.type == 2 && GameCanvas.panel2 != null)
		{
			g.setColor(13524492);
			g.fillRect(this.X + 1, 78, this.W - 2, 1);
			mFont.tahoma_7b_dark.drawString(g, mResources.chest, this.xScroll + this.wScroll / 2, 59, mFont.CENTER);
		}
		else if (this.type == 9)
		{
			g.setColor(13524492);
			g.fillRect(this.X + 1, 78, this.W - 2, 1);
			mFont.tahoma_7b_dark.drawString(g, mResources.achievement_mission, this.xScroll + this.wScroll / 2, 59, mFont.CENTER);
		}
		else if (this.type == 3)
		{
			g.setColor(13524492);
			g.fillRect(this.X + 1, 78, this.W - 2, 1);
			mFont.tahoma_7b_dark.drawString(g, mResources.select_zone, this.startTabPos + this.TAB_W / 2, 59, mFont.CENTER);
		}
		else if (this.type == 14)
		{
			g.setColor(13524492);
			g.fillRect(this.X + 1, 78, this.W - 2, 1);
			mFont.tahoma_7b_dark.drawString(g, mResources.select_map, this.startTabPos + this.TAB_W / 2, 59, mFont.CENTER);
		}
		else if (this.type == 4)
		{
			mFont.tahoma_7b_dark.drawString(g, mResources.map, this.startTabPos + this.TAB_W / 2, 59, mFont.CENTER);
			g.setColor(13524492);
			g.fillRect(this.X + 1, 78, this.W - 2, 1);
		}
		else if (this.type == 7)
		{
			mFont.tahoma_7b_dark.drawString(g, mResources.trangbi, this.startTabPos + this.TAB_W / 2, 59, mFont.CENTER);
			g.setColor(13524492);
			g.fillRect(this.X + 1, 78, this.W - 2, 1);
		}
		else if (this.type == 17)
		{
			mFont.tahoma_7b_dark.drawString(g, mResources.kigui, this.startTabPos + this.TAB_W / 2, 59, mFont.CENTER);
			g.setColor(13524492);
			g.fillRect(this.X + 1, 78, this.W - 2, 1);
		}
		else if (this.type == 8)
		{
			mFont.tahoma_7b_dark.drawString(g, mResources.msg, this.startTabPos + this.TAB_W / 2, 59, mFont.CENTER);
			g.setColor(13524492);
			g.fillRect(this.X + 1, 78, this.W - 2, 1);
		}
		else if (this.type == 10)
		{
			mFont.tahoma_7b_dark.drawString(g, mResources.wat_do_u_want, this.startTabPos + this.TAB_W / 2, 59, mFont.CENTER);
			g.setColor(13524492);
			g.fillRect(this.X + 1, 78, this.W - 2, 1);
		}
		else
		{
			if (this.currentTabIndex == 3 && this.mainTabName.Length != 4)
			{
				g.translate(-this.cmx, 0);
			}
			for (int i = 0; i < this.currentTabName.Length; i++)
			{
				g.setColor((i != this.currentTabIndex) ? 16773296 : 6805896);
				PopUp.paintPopUp(g, this.startTabPos + i * this.TAB_W, 52, this.TAB_W - 1, 25, (i != this.currentTabIndex) ? 0 : 1, true);
				if (i == this.keyTouchTab)
				{
					g.drawImage(ItemMap.imageFlare, this.startTabPos + i * this.TAB_W + this.TAB_W / 2, 62, 3);
				}
				mFont mFont = (i != this.currentTabIndex) ? mFont.tahoma_7_grey : mFont.tahoma_7_green2;
				if (!this.currentTabName[i][1].Equals(string.Empty))
				{
					mFont.drawString(g, this.currentTabName[i][0], this.startTabPos + i * this.TAB_W + this.TAB_W / 2, 53, mFont.CENTER);
					mFont.drawString(g, this.currentTabName[i][1], this.startTabPos + i * this.TAB_W + this.TAB_W / 2, 64, mFont.CENTER);
				}
				else
				{
					mFont.drawString(g, this.currentTabName[i][0], this.startTabPos + i * this.TAB_W + this.TAB_W / 2, 59, mFont.CENTER);
				}
				if (this.type == 0 && this.currentTabName.Length == 5 && GameScr.isNewClanMessage && GameCanvas.gameTick % 4 == 0)
				{
					g.drawImage(ItemMap.imageFlare, this.startTabPos + 3 * this.TAB_W + this.TAB_W / 2, 77, mGraphics.BOTTOM | mGraphics.HCENTER);
				}
			}
			g.setColor(13524492);
			g.fillRect(1, 78, this.W - 2, 1);
		}
	}

	// Token: 0x060008E6 RID: 2278 RVA: 0x0008249C File Offset: 0x0008089C
	private void paintBottomMoneyInfo(mGraphics g)
	{
		if (this.type == 13 && (this.currentTabIndex == 2 || this.Equals(GameCanvas.panel2)))
		{
			return;
		}
		g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
		g.setColor(11837316);
		g.fillRect(this.X + 1, this.H - 15, this.W - 2, 14);
		g.setColor(13524492);
		g.fillRect(this.X + 1, this.H - 15, this.W - 2, 1);
		g.drawImage(Panel.imgXu, this.X + 11, this.H - 7, 3);
		g.drawImage(Panel.imgLuong, this.X + 75, this.H - 8, 3);
		mFont.tahoma_7_yellow.drawString(g, global::Char.myCharz().xuStr + string.Empty, this.X + 24, this.H - 13, mFont.LEFT, mFont.tahoma_7_grey);
		mFont.tahoma_7_yellow.drawString(g, global::Char.myCharz().luongStr + string.Empty, this.X + 85, this.H - 13, mFont.LEFT, mFont.tahoma_7_grey);
		g.drawImage(Panel.imgLuongKhoa, this.X + 130, this.H - 8, 3);
		mFont.tahoma_7_yellow.drawString(g, global::Char.myCharz().luongKhoaStr + string.Empty, this.X + 140, this.H - 13, mFont.LEFT, mFont.tahoma_7_grey);
	}

	// Token: 0x060008E7 RID: 2279 RVA: 0x0008264C File Offset: 0x00080A4C
	private void paintClanInfo(mGraphics g)
	{
		if (global::Char.myCharz().clan == null)
		{
			SmallImage.drawSmallImage(g, global::Char.myCharz().avatarz(), 25, 50, 0, 33);
			mFont.tahoma_7b_white.drawString(g, mResources.not_join_clan, (this.wScroll - 50) / 2 + 50, 20, mFont.CENTER);
		}
		else if (!this.isViewMember)
		{
			Clan clan = global::Char.myCharz().clan;
			if (clan != null)
			{
				SmallImage.drawSmallImage(g, global::Char.myCharz().avatarz(), 25, 50, 0, 33);
				mFont.tahoma_7b_white.drawString(g, clan.name, 60, 4, mFont.LEFT, mFont.tahoma_7b_dark);
				mFont.tahoma_7_yellow.drawString(g, mResources.achievement_point + ": " + clan.powerPoint, 60, 16, mFont.LEFT, mFont.tahoma_7_grey);
				mFont.tahoma_7_yellow.drawString(g, mResources.clan_point + ": " + clan.clanPoint, 60, 27, mFont.LEFT, mFont.tahoma_7_grey);
				mFont.tahoma_7_yellow.drawString(g, mResources.level + ": " + clan.level, 60, 38, mFont.LEFT, mFont.tahoma_7_grey);
				TextInfo.paint(g, clan.slogan, 60, 38, this.wScroll - 70, this.ITEM_HEIGHT, mFont.tahoma_7_yellow);
			}
		}
		else
		{
			Clan clan2 = (this.currClan == null) ? global::Char.myCharz().clan : this.currClan;
			SmallImage.drawSmallImage(g, global::Char.myCharz().avatarz(), 25, 51, 0, 33);
			mFont.tahoma_7b_white.drawString(g, clan2.name, 60, 4, mFont.LEFT, mFont.tahoma_7b_dark);
			mFont.tahoma_7_yellow.drawString(g, string.Concat(new object[]
			{
				mResources.member,
				": ",
				clan2.currMember,
				"/",
				clan2.maxMember
			}), 60, 16, mFont.LEFT, mFont.tahoma_7_grey);
			mFont.tahoma_7_yellow.drawString(g, mResources.clan_leader + ": " + clan2.leaderName, 60, 27, mFont.LEFT, mFont.tahoma_7_grey);
			TextInfo.paint(g, clan2.slogan, 60, 38, this.wScroll - 70, this.ITEM_HEIGHT, mFont.tahoma_7_yellow);
		}
	}

	// Token: 0x060008E8 RID: 2280 RVA: 0x000828B8 File Offset: 0x00080CB8
	private void paintToolInfo(mGraphics g)
	{
		mFont.tahoma_7b_white.drawString(g, mResources.dragon_ball + " " + GameMidlet.VERSION, 60, 4, mFont.LEFT, mFont.tahoma_7b_dark);
		mFont.tahoma_7_yellow.drawString(g, mResources.character + ": " + global::Char.myCharz().cName, 60, 16, mFont.LEFT, mFont.tahoma_7_grey);
		mFont.tahoma_7_yellow.drawString(g, mResources.account_server + " " + ServerListScreen.nameServer[ServerListScreen.ipSelect] + ":", 60, 27, mFont.LEFT, mFont.tahoma_7_grey);
		mFont.tahoma_7_yellow.drawString(g, "Mày nhìn cái chóa gì?", 60, 39, mFont.LEFT, mFont.tahoma_7_grey);
	}

	// Token: 0x060008E9 RID: 2281 RVA: 0x000829AC File Offset: 0x00080DAC
	private void paintGiaoDichInfo(mGraphics g)
	{
		mFont.tahoma_7_yellow.drawString(g, mResources.select_item, 60, 4, mFont.LEFT, mFont.tahoma_7_grey);
		mFont.tahoma_7_yellow.drawString(g, mResources.lock_trade, 60, 16, mFont.LEFT, mFont.tahoma_7_grey);
		mFont.tahoma_7_yellow.drawString(g, mResources.wait_opp_lock_trade, 60, 27, mFont.LEFT, mFont.tahoma_7_grey);
		mFont.tahoma_7_yellow.drawString(g, mResources.press_done, 60, 38, mFont.LEFT, mFont.tahoma_7_grey);
	}

	// Token: 0x060008EA RID: 2282 RVA: 0x00082A30 File Offset: 0x00080E30
	private void paintMyInfo(mGraphics g)
	{
		this.paintCharInfo(g, global::Char.myCharz());
	}

	// Token: 0x060008EB RID: 2283 RVA: 0x00082A40 File Offset: 0x00080E40
	private void paintPetInfo(mGraphics g)
	{
		mFont.tahoma_7_yellow.drawString(g, mResources.power + ": " + NinjaUtil.getMoneys(global::Char.myPetz().cPower), this.X + 60, 4, mFont.LEFT, mFont.tahoma_7_grey);
		if (global::Char.myPetz().cPower > 0L)
		{
			mFont.tahoma_7_yellow.drawString(g, (!global::Char.myPetz().me) ? global::Char.myPetz().currStrLevel : global::Char.myPetz().getStrLevel(), this.X + 60, 16, mFont.LEFT, mFont.tahoma_7_grey);
		}
		if (global::Char.myPetz().cDamFull > 0)
		{
			mFont.tahoma_7_yellow.drawString(g, mResources.hit_point + " :" + global::Char.myPetz().cDamFull, this.X + 60, 27, mFont.LEFT, mFont.tahoma_7_grey);
		}
		if (global::Char.myPetz().cMaxStamina > 0)
		{
			mFont.tahoma_7_yellow.drawString(g, mResources.vitality, this.X + 60, 38, mFont.LEFT, mFont.tahoma_7_grey);
			g.drawImage(GameScr.imgMPLost, this.X + 100, 41, 0);
			int num = global::Char.myPetz().cStamina * mGraphics.getImageWidth(GameScr.imgMP) / (int)global::Char.myPetz().cMaxStamina;
			g.setClip(100, this.X + 41, num, 20);
			g.drawImage(GameScr.imgMP, this.X + 100, 41, 0);
		}
		g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
	}

	// Token: 0x060008EC RID: 2284 RVA: 0x00082BDC File Offset: 0x00080FDC
	private void paintCharInfo(mGraphics g, global::Char c)
	{
		mFont.tahoma_7b_white.drawString(g, (((int)GameScr.isNewMember == 1) ? "       " : string.Empty) + c.cName, this.X + 60, 4, mFont.LEFT, mFont.tahoma_7b_dark);
		if ((int)GameScr.isNewMember == 1)
		{
			SmallImage.drawSmallImage(g, 5427, this.X + 55, 4, 0, 0);
		}
		if (c.cMaxStamina > 0)
		{
			mFont.tahoma_7_yellow.drawString(g, mResources.vitality, this.X + 60, 16, mFont.LEFT, mFont.tahoma_7_grey);
			g.drawImage(GameScr.imgMPLost, this.X + 95, 19, 0);
			int num = c.cStamina * mGraphics.getImageWidth(GameScr.imgMP) / (int)c.cMaxStamina;
			g.setClip(95, this.X + 19, num, 20);
			g.drawImage(GameScr.imgMP, this.X + 95, 19, 0);
		}
		g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
		if (c.cPower > 0L)
		{
			mFont.tahoma_7_yellow.drawString(g, (!c.me) ? c.currStrLevel : c.getStrLevel(), this.X + 60, 27, mFont.LEFT, mFont.tahoma_7_grey);
		}
		mFont.tahoma_7_yellow.drawString(g, mResources.power + ": " + NinjaUtil.getMoneys(c.cPower), this.X + 60, 38, mFont.LEFT, mFont.tahoma_7_grey);
	}

	// Token: 0x060008ED RID: 2285 RVA: 0x00082D74 File Offset: 0x00081174
	private void paintZoneInfo(mGraphics g)
	{
		mFont.tahoma_7b_white.drawString(g, mResources.zone + " " + TileMap.zoneID, 60, 4, mFont.LEFT, mFont.tahoma_7b_dark);
		mFont.tahoma_7_yellow.drawString(g, TileMap.mapName, 60, 16, mFont.LEFT, mFont.tahoma_7_grey);
		mFont.tahoma_7b_white.drawString(g, TileMap.zoneID + string.Empty, 25, 27, mFont.CENTER);
	}

	// Token: 0x060008EE RID: 2286 RVA: 0x00082DF8 File Offset: 0x000811F8
	public int getCompare(Item item)
	{
		if (item == null)
		{
			return -1;
		}
		if (!item.isTypeBody())
		{
			return 0;
		}
		if (item.itemOption == null)
		{
			return -1;
		}
		ItemOption itemOption = item.itemOption[0];
		if (itemOption.optionTemplate.id == 22)
		{
			itemOption.optionTemplate = GameScr.gI().iOptionTemplates[6];
			itemOption.param *= 1000;
		}
		if (itemOption.optionTemplate.id == 23)
		{
			itemOption.optionTemplate = GameScr.gI().iOptionTemplates[7];
			itemOption.param *= 1000;
		}
		Item item2 = null;
		for (int i = 0; i < global::Char.myCharz().arrItemBody.Length; i++)
		{
			Item item3 = global::Char.myCharz().arrItemBody[i];
			if (itemOption.optionTemplate.id == 22)
			{
				itemOption.optionTemplate = GameScr.gI().iOptionTemplates[6];
				itemOption.param *= 1000;
			}
			if (itemOption.optionTemplate.id == 23)
			{
				itemOption.optionTemplate = GameScr.gI().iOptionTemplates[7];
				itemOption.param *= 1000;
			}
			if (item3 != null && item3.itemOption != null && (int)item3.template.type == (int)item.template.type)
			{
				item2 = item3;
				break;
			}
		}
		if (item2 == null)
		{
			this.isUp = true;
			return itemOption.param;
		}
		int num;
		if (item2 != null && item2.itemOption != null)
		{
			num = itemOption.param - item2.itemOption[0].param;
		}
		else
		{
			num = itemOption.param;
		}
		if (num < 0)
		{
			this.isUp = false;
		}
		else
		{
			this.isUp = true;
		}
		return num;
	}

	// Token: 0x060008EF RID: 2287 RVA: 0x00082FD4 File Offset: 0x000813D4
	private void paintMapInfo(mGraphics g)
	{
		mFont.tahoma_7b_white.drawString(g, mResources.MENUGENDER[(int)TileMap.planetID], 60, 4, mFont.LEFT);
		string str = string.Empty;
		if (TileMap.mapID >= 135 && TileMap.mapID <= 138)
		{
			str = " " + mResources.tang + TileMap.zoneID;
		}
		mFont.tahoma_7_yellow.drawString(g, TileMap.mapName + str, 60, 16, mFont.LEFT);
		mFont.tahoma_7b_white.drawString(g, mResources.quest_place + ": ", 60, 27, mFont.LEFT);
		if (GameScr.getTaskMapId() >= 0 && GameScr.getTaskMapId() <= TileMap.mapNames.Length - 1)
		{
			mFont.tahoma_7_yellow.drawString(g, TileMap.mapNames[GameScr.getTaskMapId()], 60, 38, mFont.LEFT);
		}
		else
		{
			mFont.tahoma_7_yellow.drawString(g, mResources.random, 60, 38, mFont.LEFT);
		}
	}

	// Token: 0x060008F0 RID: 2288 RVA: 0x000830DC File Offset: 0x000814DC
	private void paintShopInfo(mGraphics g)
	{
		if (this.currentTabIndex == this.currentTabName.Length - 1 && GameCanvas.panel2 == null)
		{
			this.paintMyInfo(g);
			return;
		}
		if (this.selected < 0)
		{
			if (this.typeShop != 2)
			{
				mFont.tahoma_7_white.drawString(g, mResources.say_hello, this.X + 60, 14, 0);
				mFont.tahoma_7_white.drawString(g, Panel.strWantToBuy, this.X + 60, 26, 0);
			}
			else
			{
				mFont.tahoma_7_white.drawString(g, mResources.say_hello, this.X + 60, 5, 0);
				mFont.tahoma_7_white.drawString(g, Panel.strWantToBuy, this.X + 60, 17, 0);
				mFont.tahoma_7_white.drawString(g, string.Concat(new object[]
				{
					mResources.page,
					" ",
					this.currPageShop[this.currentTabIndex] + 1,
					"/",
					this.maxPageShop[this.currentTabIndex]
				}), this.X + 60, 29, 0);
			}
			return;
		}
		if (this.currentTabIndex >= 0 && this.currentTabIndex <= global::Char.myCharz().arrItemShop.Length - 1 && this.selected >= 0 && this.selected <= global::Char.myCharz().arrItemShop[this.currentTabIndex].Length - 1)
		{
			Item item = global::Char.myCharz().arrItemShop[this.currentTabIndex][this.selected];
			if (item != null)
			{
				if (this.Equals(GameCanvas.panel) && this.currentTabIndex <= 3 && this.typeShop == 2)
				{
					mFont.tahoma_7b_white.drawString(g, string.Concat(new object[]
					{
						mResources.page,
						" ",
						this.currPageShop[this.currentTabIndex] + 1,
						"/",
						this.maxPageShop[this.currentTabIndex]
					}), this.X + 55, 4, 0);
				}
				mFont.tahoma_7b_white.drawString(g, item.template.name, this.X + 55, 24, 0);
				string st = mResources.pow_request + " " + Res.formatNumber((long)item.template.strRequire);
				if ((long)item.template.strRequire > global::Char.myCharz().cPower)
				{
					mFont.tahoma_7_yellow.drawString(g, st, this.X + 55, 35, 0);
				}
				else
				{
					mFont.tahoma_7_green.drawString(g, st, this.X + 55, 35, 0);
				}
			}
		}
	}

	// Token: 0x060008F1 RID: 2289 RVA: 0x00083398 File Offset: 0x00081798
	private void paintItemBoxInfo(mGraphics g)
	{
		string st = string.Concat(new object[]
		{
			mResources.used,
			": ",
			this.hasUse,
			"/",
			global::Char.myCharz().arrItemBox.Length,
			" ",
			mResources.place
		});
		mFont.tahoma_7b_white.drawString(g, mResources.chest, 60, 4, 0);
		mFont.tahoma_7_yellow.drawString(g, st, 60, 16, 0);
	}

	// Token: 0x060008F2 RID: 2290 RVA: 0x00083420 File Offset: 0x00081820
	private void paintSkillInfo(mGraphics g)
	{
		mFont.tahoma_7_white.drawString(g, "Top " + global::Char.myCharz().rank, this.X + 45 + (this.W - 50) / 2, 2, mFont.CENTER);
		mFont.tahoma_7_yellow.drawString(g, mResources.potential_point, this.X + 45 + (this.W - 50) / 2, 14, mFont.CENTER);
		mFont.tahoma_7_white.drawString(g, string.Empty + NinjaUtil.getMoneys(global::Char.myCharz().cTiemNang), this.X + ((GameCanvas.gameTick % 20 <= 10) ? 0 : (GameCanvas.gameTick % 4 / 2)) + 45 + (this.W - 50) / 2, 26, mFont.CENTER);
		mFont.tahoma_7_yellow.drawString(g, mResources.active_point + ": " + NinjaUtil.getMoneys(global::Char.myCharz().cNangdong), this.X + ((GameCanvas.gameTick % 20 <= 10) ? 0 : (GameCanvas.gameTick % 4 / 2)) + 45 + (this.W - 50) / 2, 38, mFont.CENTER);
	}

	// Token: 0x060008F3 RID: 2291 RVA: 0x0008355C File Offset: 0x0008195C
	private void paintItemBodyBagInfo(mGraphics g)
	{
		mFont.tahoma_7_yellow.drawString(g, string.Concat(new object[]
		{
			mResources.HP,
			": ",
			global::Char.myCharz().cHP,
			" / ",
			global::Char.myCharz().cHPFull
		}), this.X + 60, 2, mFont.LEFT, mFont.tahoma_7_grey);
		mFont.tahoma_7_yellow.drawString(g, string.Concat(new object[]
		{
			mResources.KI,
			": ",
			global::Char.myCharz().cMP,
			" / ",
			global::Char.myCharz().cMPFull
		}), this.X + 60, 14, mFont.LEFT, mFont.tahoma_7_grey);
		mFont.tahoma_7_yellow.drawString(g, mResources.hit_point + ": " + global::Char.myCharz().cDamFull, this.X + 60, 26, mFont.LEFT, mFont.tahoma_7_grey);
		mFont.tahoma_7_yellow.drawString(g, string.Concat(new object[]
		{
			mResources.armor,
			": ",
			global::Char.myCharz().cDefull,
			", ",
			mResources.critical,
			": ",
			global::Char.myCharz().cCriticalFull,
			"%"
		}), this.X + 60, 38, mFont.LEFT, mFont.tahoma_7_grey);
	}

	// Token: 0x060008F4 RID: 2292 RVA: 0x000836F4 File Offset: 0x00081AF4
	private void paintTopInfo(mGraphics g)
	{
		g.setClip(this.X + 1, this.Y, this.W - 2, this.yScroll - 2);
		g.setColor(9993045);
		g.fillRect(this.X, this.Y, this.W - 2, 50);
		switch (this.type)
		{
			case 0:
				if (this.currentTabIndex == 0)
				{
					SmallImage.drawSmallImage(g, global::Char.myCharz().avatarz(), this.X + 25, 50, 0, 33);
					this.paintMyInfo(g);
				}
				if (this.currentTabIndex == 1)
				{
					SmallImage.drawSmallImage(g, global::Char.myCharz().avatarz(), this.X + 25, 50, 0, 33);
					this.paintItemBodyBagInfo(g);
				}
				if (this.currentTabIndex == 2)
				{
					SmallImage.drawSmallImage(g, global::Char.myCharz().avatarz(), this.X + 25, 50, 0, 33);
					this.paintSkillInfo(g);
				}
				if (this.currentTabIndex == 3)
				{
					if (this.mainTabName.Length == 5)
					{
						this.paintClanInfo(g);
					}
					else
					{
						SmallImage.drawSmallImage(g, global::Char.myCharz().avatarz(), this.X + 25, 50, 0, 33);
						this.paintToolInfo(g);
					}
				}
				if (this.currentTabIndex == 4)
				{
					SmallImage.drawSmallImage(g, global::Char.myCharz().avatarz(), this.X + 25, 50, 0, 33);
					this.paintToolInfo(g);
				}
				break;
			case 1:
				if (this.currentTabIndex == this.currentTabName.Length - 1 && GameCanvas.panel2 == null)
				{
					SmallImage.drawSmallImage(g, global::Char.myCharz().avatarz(), this.X + 25, 50, 0, 33);
				}
				else
				{
					SmallImage.drawSmallImage(g, global::Char.myCharz().npcFocus.avatar, this.X + 25, 50, 0, 33);
				}
				this.paintShopInfo(g);
				break;
			case 2:
				if (this.currentTabIndex == 0)
				{
					SmallImage.drawSmallImage(g, 526, this.X + 25, 50, 0, 33);
					this.paintItemBoxInfo(g);
				}
				if (this.currentTabIndex == 1)
				{
					SmallImage.drawSmallImage(g, global::Char.myCharz().avatarz(), this.X + 25, 50, 0, 33);
					this.paintItemBodyBagInfo(g);
				}
				break;
			case 3:
				SmallImage.drawSmallImage(g, 561, this.X + 25, 50, 0, 33);
				this.paintZoneInfo(g);
				break;
			case 4:
				SmallImage.drawSmallImage(g, global::Char.myCharz().avatarz(), this.X + 25, 50, 0, 33);
				this.paintMapInfo(g);
				break;
			case 7:
			case 17:
				SmallImage.drawSmallImage(g, global::Char.myCharz().avatarz(), this.X + 25, 50, 0, 33);
				this.paintMyInfo(g);
				break;
			case 8:
				SmallImage.drawSmallImage(g, global::Char.myCharz().avatarz(), this.X + 25, 50, 0, 33);
				this.paintMyInfo(g);
				break;
			case 9:
				SmallImage.drawSmallImage(g, global::Char.myCharz().avatarz(), this.X + 25, 50, 0, 33);
				this.paintMyInfo(g);
				break;
			case 10:
				if (this.charMenu != null)
				{
					SmallImage.drawSmallImage(g, this.charMenu.avatarz(), this.X + 25, 50, 0, 33);
					this.paintCharInfo(g, this.charMenu);
				}
				break;
			case 11:
			case 16:
			case 23:
			case 24:
				SmallImage.drawSmallImage(g, global::Char.myCharz().avatarz(), this.X + 25, 50, 0, 33);
				this.paintMyInfo(g);
				break;
			case 12:
				if (this.currentTabIndex == 0)
				{
					int id = 1410;
					for (int i = 0; i < GameScr.vNpc.size(); i++)
					{
						Npc npc = (Npc)GameScr.vNpc.elementAt(i);
						if (npc.template.npcTemplateId == this.idNPC)
						{
							id = npc.avatar;
						}
					}
					SmallImage.drawSmallImage(g, id, this.X + 25, 50, 0, 33);
					this.paintCombineInfo(g);
				}
				if (this.currentTabIndex == 1)
				{
					SmallImage.drawSmallImage(g, global::Char.myCharz().avatarz(), this.X + 25, 50, 0, 33);
					this.paintMyInfo(g);
				}
				break;
			case 13:
				if (this.currentTabIndex == 0 || this.currentTabIndex == 1)
				{
					if (this.Equals(GameCanvas.panel))
					{
						SmallImage.drawSmallImage(g, global::Char.myCharz().avatarz(), this.X + 25, 50, 0, 33);
						this.paintGiaoDichInfo(g);
					}
					if (this.Equals(GameCanvas.panel2) && this.charMenu != null)
					{
						SmallImage.drawSmallImage(g, this.charMenu.avatarz(), this.X + 25, 50, 0, 33);
						this.paintCharInfo(g, this.charMenu);
					}
				}
				if (this.currentTabIndex == 2 && this.charMenu != null)
				{
					SmallImage.drawSmallImage(g, this.charMenu.avatarz(), this.X + 25, 50, 0, 33);
					this.paintCharInfo(g, this.charMenu);
				}
				break;
			case 14:
				SmallImage.drawSmallImage(g, global::Char.myCharz().avatarz(), this.X + 25, 50, 0, 33);
				this.paintMapInfo(g);
				break;
			case 15:
				SmallImage.drawSmallImage(g, global::Char.myCharz().avatarz(), this.X + 25, 50, 0, 33);
				this.paintMyInfo(g);
				break;
			case 18:
				SmallImage.drawSmallImage(g, global::Char.myCharz().avatarz(), this.X + 25, 50, 0, 33);
				this.paintMyInfo(g);
				break;
			case 19:
				SmallImage.drawSmallImage(g, global::Char.myCharz().avatarz(), this.X + 25, 50, 0, 33);
				this.paintToolInfo(g);
				break;
			case 20:
				SmallImage.drawSmallImage(g, global::Char.myCharz().avatarz(), this.X + 25, 50, 0, 33);
				this.paintToolInfo(g);
				break;
			case 21:
				if (this.currentTabIndex == 0)
				{
					SmallImage.drawSmallImage(g, global::Char.myPetz().avatarz(), this.X + 25, 50, 0, 33);
					this.paintPetInfo(g);
				}
				if (this.currentTabIndex == 1)
				{
					SmallImage.drawSmallImage(g, global::Char.myPetz().avatarz(), this.X + 25, 50, 0, 33);
					this.paintPetStatusInfo(g);
				}
				if (this.currentTabIndex == 2)
				{
					SmallImage.drawSmallImage(g, global::Char.myCharz().avatarz(), this.X + 25, 50, 0, 33);
					this.paintItemBodyBagInfo(g);
				}
				break;
			case 22:
				SmallImage.drawSmallImage(g, global::Char.myCharz().avatarz(), this.X + 25, 50, 0, 33);
				this.paintToolInfo(g);
				break;
			case 25:
				SmallImage.drawSmallImage(g, global::Char.myCharz().avatarz(), this.X + 25, 50, 0, 33);
				this.paintMyInfo(g);
				break;
		}
	}

	// Token: 0x060008F5 RID: 2293 RVA: 0x00083E2C File Offset: 0x0008222C
	private string getStatus(int status)
	{
		switch (status)
		{
			case 0:
				return mResources.follow;
			case 1:
				return mResources.defend;
			case 2:
				return mResources.attack;
			case 3:
				return mResources.gohome;
			default:
				return "aaa";
		}
	}

	// Token: 0x060008F6 RID: 2294 RVA: 0x00083E68 File Offset: 0x00082268
	private void paintPetStatusInfo(mGraphics g)
	{
		mFont.tahoma_7b_white.drawString(g, string.Concat(new object[]
		{
			"HP: ",
			global::Char.myPetz().cHP,
			"/",
			global::Char.myPetz().cHPFull
		}), this.X + 60, 4, mFont.LEFT, mFont.tahoma_7b_dark);
		mFont.tahoma_7b_white.drawString(g, string.Concat(new object[]
		{
			"MP: ",
			global::Char.myPetz().cMP,
			"/",
			global::Char.myPetz().cMPFull
		}), this.X + 60, 16, mFont.LEFT, mFont.tahoma_7b_dark);
		mFont.tahoma_7_yellow.drawString(g, string.Concat(new object[]
		{
			mResources.critical,
			": ",
			global::Char.myPetz().cCriticalFull,
			"   ",
			mResources.armor,
			": ",
			global::Char.myPetz().cDefull
		}), this.X + 60, 27, mFont.LEFT, mFont.tahoma_7_grey);
		mFont.tahoma_7_yellow.drawString(g, mResources.status + " :" + this.strStatus[(int)global::Char.myPetz().petStatus], this.X + 60, 38, mFont.LEFT, mFont.tahoma_7_grey);
	}

	// Token: 0x060008F7 RID: 2295 RVA: 0x00083FEC File Offset: 0x000823EC
	private void paintCombineInfo(mGraphics g)
	{
		if (this.combineTopInfo != null)
		{
			for (int i = 0; i < this.combineTopInfo.Length; i++)
			{
				mFont.tahoma_7_white.drawString(g, this.combineTopInfo[i], this.X + 45 + (this.W - 50) / 2, 5 + i * 14, mFont.CENTER);
			}
		}
	}

	// Token: 0x060008F8 RID: 2296 RVA: 0x00084050 File Offset: 0x00082450
	private void paintInfomation(mGraphics g)
	{
	}

	// Token: 0x060008F9 RID: 2297 RVA: 0x00084054 File Offset: 0x00082454
	public void paintMap(mGraphics g)
	{
		g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
		g.translate(-this.cmxMap, -this.cmyMap);
		g.drawImage(Panel.imgMap, this.xScroll, this.yScroll, 0);
		int head = global::Char.myCharz().head;
		Part part = GameScr.parts[head];
		SmallImage.drawSmallImage(g, (int)part.pi[global::Char.CharInfo[0][0][0]].id, this.xMap, this.yMap + 5, 0, 3);
		int align = mFont.CENTER;
		if (this.xMap <= 40)
		{
			align = mFont.LEFT;
		}
		if (this.xMap >= 220)
		{
			align = mFont.RIGHT;
		}
		mFont.tahoma_7b_yellow.drawString(g, TileMap.mapName, this.xMap, this.yMap - 12, align, mFont.tahoma_7_grey);
		int num = -1;
		if (GameScr.getTaskMapId() != -1)
		{
			for (int i = 0; i < Panel.mapId[(int)TileMap.planetID].Length; i++)
			{
				if (Panel.mapId[(int)TileMap.planetID][i] == GameScr.getTaskMapId())
				{
					num = i;
					break;
				}
				num = 4;
			}
			if (GameCanvas.gameTick % 4 > 0)
			{
				g.drawImage(ItemMap.imageFlare, this.xScroll + Panel.mapX[(int)TileMap.planetID][num], this.yScroll + Panel.mapY[(int)TileMap.planetID][num], 3);
			}
		}
		if (!GameCanvas.isTouch)
		{
			g.drawImage(Panel.imgBantay, this.xMove, this.yMove, StaticObj.TOP_RIGHT);
			for (int j = 0; j < Panel.mapX[(int)TileMap.planetID].Length; j++)
			{
				int num2 = Panel.mapX[(int)TileMap.planetID][j] + this.xScroll;
				int num3 = Panel.mapY[(int)TileMap.planetID][j] + this.yScroll;
				if (Res.inRect(num2 - 15, num3 - 15, 30, 30, this.xMove, this.yMove))
				{
					align = mFont.CENTER;
					if (num2 <= 20)
					{
						align = mFont.LEFT;
					}
					if (num2 >= 220)
					{
						align = mFont.RIGHT;
					}
					mFont.tahoma_7b_yellow.drawString(g, TileMap.mapNames[Panel.mapId[(int)TileMap.planetID][j]], num2, num3 - 12, align, mFont.tahoma_7_grey);
					break;
				}
			}
		}
		else if (!this.trans)
		{
			for (int k = 0; k < Panel.mapX[(int)TileMap.planetID].Length; k++)
			{
				int num4 = Panel.mapX[(int)TileMap.planetID][k] + this.xScroll;
				int num5 = Panel.mapY[(int)TileMap.planetID][k] + this.yScroll;
				if (Res.inRect(num4 - 15, num5 - 15, 30, 30, this.pX, this.pY))
				{
					align = mFont.CENTER;
					if (num4 <= 30)
					{
						align = mFont.LEFT;
					}
					if (num4 >= 220)
					{
						align = mFont.RIGHT;
					}
					g.drawImage(Panel.imgBantay, num4, num5, StaticObj.TOP_RIGHT);
					mFont.tahoma_7b_yellow.drawString(g, TileMap.mapNames[Panel.mapId[(int)TileMap.planetID][k]], num4, num5 - 12, align, mFont.tahoma_7_grey);
					break;
				}
			}
		}
		g.translate(-g.getTranslateX(), -g.getTranslateY());
		if (num != -1)
		{
			if (Panel.mapX[(int)TileMap.planetID][num] + this.xScroll < this.cmxMap)
			{
				g.drawRegion(Mob.imgHP, 0, 0, 9, 6, 5, this.xScroll + 5, this.yScroll + this.hScroll / 2 - 4, 0);
			}
			if (this.cmxMap + this.wScroll < Panel.mapX[(int)TileMap.planetID][num] + this.xScroll)
			{
				g.drawRegion(Mob.imgHP, 0, 0, 9, 6, 6, this.xScroll + this.wScroll - 5, this.yScroll + this.hScroll / 2 - 4, StaticObj.TOP_RIGHT);
			}
			if (Panel.mapY[(int)TileMap.planetID][num] < this.cmyMap)
			{
				g.drawRegion(Mob.imgHP, 0, 0, 9, 6, 1, this.xScroll + this.wScroll / 2, this.yScroll + 5, StaticObj.TOP_CENTER);
			}
			if (Panel.mapY[(int)TileMap.planetID][num] > this.cmyMap + this.hScroll)
			{
				g.drawRegion(Mob.imgHP, 0, 0, 9, 6, 0, this.xScroll + this.wScroll / 2, this.yScroll + this.hScroll - 5, StaticObj.BOTTOM_HCENTER);
			}
		}
	}

	// Token: 0x060008FA RID: 2298 RVA: 0x0008451C File Offset: 0x0008291C
	public void paintTask(mGraphics g)
	{
		int num = (GameCanvas.h <= 300) ? 15 : 20;
		if (Panel.isPaintMap && !GameScr.gI().isMapDocNhan() && !GameScr.gI().isMapFize())
		{
			g.drawImage((this.keyTouchMapButton != 1) ? GameScr.imgLbtn : GameScr.imgLbtnFocus, this.xScroll + this.wScroll / 2, this.yScroll + this.hScroll - num, 3);
			mFont.tahoma_7b_dark.drawString(g, mResources.map, this.xScroll + this.wScroll / 2, this.yScroll + this.hScroll - (num + 5), mFont.CENTER);
		}
		this.xstart = this.xScroll + 5;
		this.ystart = this.yScroll + 14;
		this.yPaint = this.ystart;
		g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll - 35);
		if (this.scroll != null)
		{
			if (this.scroll.cmy > 0)
			{
				g.drawRegion(Mob.imgHP, 0, 0, 9, 6, 1, this.xScroll + this.wScroll - 12, this.yScroll + 3, 0);
			}
			if (this.scroll.cmy < this.scroll.cmyLim)
			{
				g.drawRegion(Mob.imgHP, 0, 0, 9, 6, 0, this.xScroll + this.wScroll - 12, this.yScroll + this.hScroll - 45, 0);
			}
			g.translate(0, -this.scroll.cmy);
		}
		this.indexRowMax = 0;
		if (this.indexMenu == 0)
		{
			bool flag = false;
			if (global::Char.myCharz().taskMaint != null)
			{
				for (int i = 0; i < global::Char.myCharz().taskMaint.names.Length; i++)
				{
					mFont.tahoma_7_grey.drawString(g, global::Char.myCharz().taskMaint.names[i], this.xScroll + this.wScroll / 2, this.yPaint - 5 + i * 12, mFont.CENTER);
					this.indexRowMax++;
				}
				this.yPaint += (global::Char.myCharz().taskMaint.names.Length - 1) * 12;
				int num2 = 0;
				string text = string.Empty;
				for (int j = 0; j < global::Char.myCharz().taskMaint.subNames.Length; j++)
				{
					if (global::Char.myCharz().taskMaint.subNames[j] != null)
					{
						num2 = j;
						text = "- " + global::Char.myCharz().taskMaint.subNames[j];
						if (global::Char.myCharz().taskMaint.counts[j] != -1)
						{
							if (global::Char.myCharz().taskMaint.index == j)
							{
								if (global::Char.myCharz().taskMaint.counts[j] != 1)
								{
									string text2 = text;
									text = string.Concat(new object[]
									{
										text2,
										" (",
										global::Char.myCharz().taskMaint.count,
										"/",
										global::Char.myCharz().taskMaint.counts[j],
										")"
									});
								}
								if (global::Char.myCharz().taskMaint.count == global::Char.myCharz().taskMaint.counts[j])
								{
									mFont.tahoma_7.drawString(g, text, this.xstart + 5, this.yPaint += 12, 0);
								}
								else
								{
									mFont mFont = mFont.tahoma_7_grey;
									if (!flag)
									{
										flag = true;
										mFont = mFont.tahoma_7_blue;
										mFont.drawString(g, text, this.xstart + 5 + ((mFont != mFont.tahoma_7_blue || GameCanvas.gameTick % 20 <= 10) ? 0 : (GameCanvas.gameTick % 4 / 2)), this.yPaint += 12, 0);
									}
									else
									{
										mFont.drawString(g, "- ...", this.xstart + 5 + ((mFont != mFont.tahoma_7_blue || GameCanvas.gameTick % 20 <= 10) ? 0 : (GameCanvas.gameTick % 4 / 2)), this.yPaint += 12, 0);
									}
								}
							}
							else if (global::Char.myCharz().taskMaint.index > j)
							{
								if (global::Char.myCharz().taskMaint.counts[j] != 1)
								{
									string text2 = text;
									text = string.Concat(new object[]
									{
										text2,
										" (",
										global::Char.myCharz().taskMaint.counts[j],
										"/",
										global::Char.myCharz().taskMaint.counts[j],
										")"
									});
								}
								mFont.tahoma_7_white.drawString(g, text, this.xstart + 5, this.yPaint += 12, 0);
							}
							else
							{
								if (global::Char.myCharz().taskMaint.counts[j] != 1)
								{
									text = text + " 0/" + global::Char.myCharz().taskMaint.counts[j];
								}
								mFont mFont2 = mFont.tahoma_7_grey;
								if (!flag)
								{
									flag = true;
									mFont2 = mFont.tahoma_7_blue;
									mFont2.drawString(g, text, this.xstart + 5 + ((mFont2 != mFont.tahoma_7_blue || GameCanvas.gameTick % 20 <= 10) ? 0 : (GameCanvas.gameTick % 4 / 2)), this.yPaint += 12, 0);
								}
								else
								{
									mFont2.drawString(g, "- ...", this.xstart + 5 + ((mFont2 != mFont.tahoma_7_blue || GameCanvas.gameTick % 20 <= 10) ? 0 : (GameCanvas.gameTick % 4 / 2)), this.yPaint += 12, 0);
								}
							}
						}
						else if (global::Char.myCharz().taskMaint.index > j)
						{
							mFont.tahoma_7_white.drawString(g, text, this.xstart + 5, this.yPaint += 12, 0);
						}
						else
						{
							mFont mFont3 = mFont.tahoma_7_grey;
							if (!flag)
							{
								flag = true;
								mFont3 = mFont.tahoma_7_blue;
								mFont3.drawString(g, text, this.xstart + 5 + ((mFont3 != mFont.tahoma_7_blue || GameCanvas.gameTick % 20 <= 10) ? 0 : (GameCanvas.gameTick % 4 / 2)), this.yPaint += 12, 0);
							}
							else
							{
								mFont3.drawString(g, "- ...", this.xstart + 5 + ((mFont3 != mFont.tahoma_7_blue || GameCanvas.gameTick % 20 <= 10) ? 0 : (GameCanvas.gameTick % 4 / 2)), this.yPaint += 12, 0);
							}
						}
						this.indexRowMax++;
					}
					else if (global::Char.myCharz().taskMaint.index <= j)
					{
						text = "- " + global::Char.myCharz().taskMaint.subNames[num2];
						mFont mFont4 = mFont.tahoma_7_grey;
						if (!flag)
						{
							flag = true;
							mFont4 = mFont.tahoma_7_blue;
						}
						mFont4.drawString(g, text, this.xstart + 5 + ((mFont4 != mFont.tahoma_7_blue || GameCanvas.gameTick % 20 <= 10) ? 0 : (GameCanvas.gameTick % 4 / 2)), this.yPaint += 12, 0);
					}
				}
				this.yPaint += 5;
				for (int k = 0; k < global::Char.myCharz().taskMaint.details.Length; k++)
				{
					mFont.tahoma_7_green2.drawString(g, global::Char.myCharz().taskMaint.details[k], this.xstart + 5, this.yPaint += 12, 0);
					this.indexRowMax++;
				}
			}
			else
			{
				int taskMapId = GameScr.getTaskMapId();
				sbyte taskNpcId = GameScr.getTaskNpcId();
				string src = string.Empty;
				if (taskMapId == -3 || (int)taskNpcId == -3)
				{
					src = mResources.DES_TASK[3];
				}
				else if (global::Char.myCharz().taskMaint == null && global::Char.myCharz().ctaskId == 9 && global::Char.myCharz().nClass.classId == 0)
				{
					src = mResources.TASK_INPUT_CLASS;
				}
				else
				{
					if ((int)taskNpcId < 0 || taskMapId < 0)
					{
						return;
					}
					src = string.Concat(new string[]
					{
						mResources.DES_TASK[0],
						Npc.arrNpcTemplate[(int)taskNpcId].name,
						mResources.DES_TASK[1],
						TileMap.mapNames[taskMapId],
						mResources.DES_TASK[2]
					});
				}
				string[] array = mFont.tahoma_7_white.splitFontArray(src, 150);
				for (int l = 0; l < array.Length; l++)
				{
					if (l == 0)
					{
						mFont.tahoma_7_white.drawString(g, array[l], this.xstart + 5, this.yPaint = this.ystart, 0);
					}
					else
					{
						mFont.tahoma_7_white.drawString(g, array[l], this.xstart + 5, this.yPaint += 12, 0);
					}
				}
			}
		}
		else if (this.indexMenu == 1)
		{
			this.yPaint = this.ystart - 12;
			for (int m = 0; m < global::Char.myCharz().taskOrders.size(); m++)
			{
				TaskOrder taskOrder = (TaskOrder)global::Char.myCharz().taskOrders.elementAt(m);
				mFont.tahoma_7_white.drawString(g, taskOrder.name, this.xstart + 5, this.yPaint += 12, 0);
				if (taskOrder.count == (int)taskOrder.maxCount)
				{
					mFont.tahoma_7_white.drawString(g, string.Concat(new object[]
					{
						(taskOrder.taskId != 0) ? mResources.KILLBOSS : mResources.KILL,
						" ",
						Mob.arrMobTemplate[taskOrder.killId].name,
						" (",
						taskOrder.count,
						"/",
						taskOrder.maxCount,
						")"
					}), this.xstart + 5, this.yPaint += 12, 0);
				}
				else
				{
					mFont.tahoma_7_blue.drawString(g, string.Concat(new object[]
					{
						(taskOrder.taskId != 0) ? mResources.KILLBOSS : mResources.KILL,
						" ",
						Mob.arrMobTemplate[taskOrder.killId].name,
						" (",
						taskOrder.count,
						"/",
						taskOrder.maxCount,
						")"
					}), this.xstart + 5, this.yPaint += 12, 0);
				}
				this.indexRowMax += 3;
				this.inforW = this.popupW - 25;
				this.paintMultiLine(g, mFont.tahoma_7_grey, taskOrder.description, this.xstart + 5, this.yPaint += 12, 0);
				this.yPaint += 12;
			}
		}
		if (this.scroll == null)
		{
			this.scroll = new Scroll();
			this.scroll.setStyle(this.indexRowMax, 12, this.xScroll, this.yScroll, this.wScroll, this.hScroll - num - 40, true, 1);
		}
	}

	// Token: 0x060008FB RID: 2299 RVA: 0x00085198 File Offset: 0x00083598
	public void paintMultiLine(mGraphics g, mFont f, string[] arr, string str, int x, int y, int align)
	{
		for (int i = 0; i < arr.Length; i++)
		{
			string text = arr[i];
			if (text.StartsWith("c"))
			{
				if (text.StartsWith("c0"))
				{
					text = text.Substring(2);
					f = mFont.tahoma_7b_dark;
				}
				else if (text.StartsWith("c1"))
				{
					text = text.Substring(2);
					f = mFont.tahoma_7b_yellow;
				}
				else if (text.StartsWith("c2"))
				{
					text = text.Substring(2);
					f = mFont.tahoma_7b_green;
				}
			}
			if (i == 0)
			{
				f.drawString(g, text, x, y, align);
			}
			else
			{
				if (i < this.indexRow + 30 && i > this.indexRow - 30)
				{
					f.drawString(g, text, x, y += 12, align);
				}
				else
				{
					y += 12;
				}
				this.yPaint += 12;
				this.indexRowMax++;
			}
		}
	}

	// Token: 0x060008FC RID: 2300 RVA: 0x000852A8 File Offset: 0x000836A8
	public void paintMultiLine(mGraphics g, mFont f, string str, int x, int y, int align)
	{
		int num = (!GameCanvas.isTouch || GameCanvas.w < 320) ? 10 : 20;
		string[] array = f.splitFontArray(str, this.inforW - num);
		for (int i = 0; i < array.Length; i++)
		{
			if (i == 0)
			{
				f.drawString(g, array[i], x, y, align);
			}
			else
			{
				if (i < this.indexRow + 15 && i > this.indexRow - 15)
				{
					f.drawString(g, array[i], x, y += 12, align);
				}
				else
				{
					y += 12;
				}
				this.yPaint += 12;
				this.indexRowMax++;
			}
		}
	}

	// Token: 0x060008FD RID: 2301 RVA: 0x00085374 File Offset: 0x00083774
	public void cleanCombine()
	{
		for (int i = 0; i < this.vItemCombine.size(); i++)
		{
			((Item)this.vItemCombine.elementAt(i)).isSelect = false;
		}
		this.vItemCombine.removeAllElements();
	}

	// Token: 0x060008FE RID: 2302 RVA: 0x000853C0 File Offset: 0x000837C0
	public void hideNow()
	{
		if (this.timeShow > 0)
		{
			this.isClose = false;
			return;
		}
		if (this.isTypeShop())
		{
			global::Char.myCharz().resetPartTemp();
		}
		if (this.chatTField != null && this.type == 13 && this.chatTField.isShow)
		{
			this.chatTField = null;
		}
		if (this.type == 13 && !this.isAccept)
		{
			Service.gI().giaodich(3, -1, -1, -1);
		}
		Res.outz("HIDE PANELLLLLLLLLLLLLLLLLLLLLL");
		SoundMn.gI().buttonClose();
		GameScr.isPaint = true;
		TileMap.lastPlanetId = -1;
		Panel.imgMap = null;
		mSystem.gcc();
		this.isClanOption = false;
		this.isClose = true;
		this.cleanCombine();
		Hint.clickNpc();
		GameCanvas.panel2 = null;
		GameCanvas.clearAllPointerEvent();
		GameCanvas.clearKeyPressed();
		this.pointerDownTime = (this.pointerDownFirstX = 0);
		this.pointerIsDowning = false;
		this.isShow = false;
		if ((global::Char.myCharz().cHP <= 0 || global::Char.myCharz().statusMe == 14 || global::Char.myCharz().statusMe == 5) && global::Char.myCharz().meDead)
		{
			Command center = new Command(mResources.DIES[0], 11038, GameScr.gI());
			GameScr.gI().center = center;
			global::Char.myCharz().cHP = 0;
		}
	}

	// Token: 0x060008FF RID: 2303 RVA: 0x0008552C File Offset: 0x0008392C
	public void hide()
	{
		if (this.timeShow > 0)
		{
			this.isClose = false;
			return;
		}
		if (this.isTypeShop())
		{
			global::Char.myCharz().resetPartTemp();
		}
		if (this.chatTField != null && this.type == 13 && this.chatTField.isShow)
		{
			this.chatTField = null;
		}
		if (this.type == 13 && !this.isAccept)
		{
			Service.gI().giaodich(3, -1, -1, -1);
		}
		if (this.type == 15)
		{
			Service.gI().sendThachDau(-1);
		}
		SoundMn.gI().buttonClose();
		GameScr.isPaint = true;
		TileMap.lastPlanetId = -1;
		if (Panel.imgMap != null)
		{
			Panel.imgMap.texture = null;
			Panel.imgMap = null;
		}
		mSystem.gcc();
		this.isClanOption = false;
		if (this.type != 4)
		{
			if (this.type == 24)
			{
				this.setTypeGameInfo();
			}
			else if (this.type == 23)
			{
				this.setTypeMain();
			}
			else if (this.type == 3 || this.type == 14)
			{
				if (this.isChangeZone)
				{
					this.isClose = true;
				}
				else
				{
					this.setTypeMain();
					this.cmx = (this.cmtoX = 0);
				}
			}
			else if (this.type == 18 || this.type == 19 || this.type == 20 || this.type == 21)
			{
				this.setTypeMain();
				this.cmx = (this.cmtoX = 0);
			}
			else if (this.type == 8 || this.type == 11 || this.type == 16)
			{
				this.setTypeAccount();
				this.cmx = (this.cmtoX = 0);
			}
			else
			{
				this.isClose = true;
			}
		}
		else
		{
			this.setTypeMain();
			this.cmx = (this.cmtoX = 0);
		}
		Hint.clickNpc();
		GameCanvas.panel2 = null;
		GameCanvas.clearAllPointerEvent();
		GameCanvas.clearKeyPressed();
		GameCanvas.isFocusPanel2 = false;
		this.pointerDownTime = (this.pointerDownFirstX = 0);
		this.pointerIsDowning = false;
		if ((global::Char.myCharz().cHP <= 0 || global::Char.myCharz().statusMe == 14 || global::Char.myCharz().statusMe == 5) && global::Char.myCharz().meDead)
		{
			Command center = new Command(mResources.DIES[0], 11038, GameScr.gI());
			GameScr.gI().center = center;
			global::Char.myCharz().cHP = 0;
		}
	}

	// Token: 0x06000900 RID: 2304 RVA: 0x000857E4 File Offset: 0x00083BE4
	public void update()
	{
		if (this.chatTField != null && this.chatTField.isShow)
		{
			this.chatTField.update();
			return;
		}
		if (this.isKiguiXu)
		{
			this.delayKigui++;
			if (this.delayKigui == 10)
			{
				this.delayKigui = 0;
				this.isKiguiXu = false;
				this.chatTField.tfChat.setText(string.Empty);
				this.chatTField.strChat = mResources.kiguiXuchat + " ";
				this.chatTField.tfChat.name = mResources.input_money;
				this.chatTField.to = string.Empty;
				this.chatTField.isShow = true;
				this.chatTField.tfChat.setIputType(TField.INPUT_TYPE_NUMERIC);
				this.chatTField.tfChat.setMaxTextLenght(9);
				if (GameCanvas.isTouch)
				{
					this.chatTField.tfChat.doChangeToTextBox();
				}
				if (Main.isWindowsPhone)
				{
					this.chatTField.tfChat.strInfo = this.chatTField.strChat;
				}
				if (!Main.isPC)
				{
					this.chatTField.startChat2(this, string.Empty);
				}
			}
			return;
		}
		if (this.isKiguiLuong)
		{
			this.delayKigui++;
			if (this.delayKigui == 10)
			{
				this.delayKigui = 0;
				this.isKiguiLuong = false;
				this.chatTField.tfChat.setText(string.Empty);
				this.chatTField.strChat = mResources.kiguiLuongchat + "  ";
				this.chatTField.tfChat.name = mResources.input_money;
				this.chatTField.to = string.Empty;
				this.chatTField.isShow = true;
				this.chatTField.tfChat.setIputType(TField.INPUT_TYPE_NUMERIC);
				this.chatTField.tfChat.setMaxTextLenght(9);
				if (GameCanvas.isTouch)
				{
					this.chatTField.tfChat.doChangeToTextBox();
				}
				if (Main.isWindowsPhone)
				{
					this.chatTField.tfChat.strInfo = this.chatTField.strChat;
				}
				if (!Main.isPC)
				{
					this.chatTField.startChat2(this, string.Empty);
				}
			}
			return;
		}
		if (this.scroll != null)
		{
			this.scroll.updatecm();
		}
		if (this.tabIcon != null && this.tabIcon.isShow)
		{
			this.tabIcon.update();
			return;
		}
		this.moveCamera();
		if (this.waitToPerform > 0)
		{
			this.waitToPerform--;
			if (this.waitToPerform == 0)
			{
				this.lastSelect[this.currentTabIndex] = this.selected;
				switch (this.type)
				{
					case 0:
						this.doFireMain();
						break;
					case 1:
					case 17:
						this.doFireShop();
						break;
					case 2:
						this.doFireBox();
						break;
					case 3:
						this.doFireZone();
						break;
					case 4:
						this.doFireMap();
						break;
					case 7:
						if (this.Equals(GameCanvas.panel2) && GameCanvas.panel.type == 2)
						{
							this.doFireBox();
							return;
						}
						this.doFireInventory();
						break;
					case 8:
						this.doFireLogMessage();
						break;
					case 9:
						this.doFireArchivement();
						break;
					case 10:
						this.doFirePlayerMenu();
						break;
					case 11:
						this.doFireFriend();
						break;
					case 12:
						this.doFireCombine();
						break;
					case 13:
						this.doFireGiaoDich();
						break;
					case 14:
						this.doFireMapTrans();
						break;
					case 15:
						this.doFireTop();
						break;
					case 16:
						this.doFireEnemy();
						break;
					case 18:
						this.doFireChangeFlag();
						break;
					case 19:
						this.doFireOption();
						break;
					case 20:
						this.doFireAccount();
						break;
					case 21:
						this.doFirePetMain();
						break;
					case 22:
						this.doFireAuto();
						break;
					case 23:
						this.doFirePaintBoss();
						break;
					case 25:
						this.doSpeacialSkill();
						break;
				}
			}
		}
		for (int i = 0; i < ClanMessage.vMessage.size(); i++)
		{
			((ClanMessage)ClanMessage.vMessage.elementAt(i)).update();
		}
		this.updateCombineEff();
	}

	// Token: 0x06000901 RID: 2305 RVA: 0x00085C8D File Offset: 0x0008408D
	private void doSpeacialSkill()
	{
		MyVector myVector = new MyVector();
		myVector.addElement(new Command("Mở max", SpecialSkill.gI(), 4, global::Char.myCharz().infoSpeacialSkill[this.currentTabIndex][this.selected]));
		GameCanvas.menu.startAt(myVector, 3);
	}

	// Token: 0x06000903 RID: 2307 RVA: 0x00085D06 File Offset: 0x00084106
	private void doFireAuto()
	{
	}

	// Token: 0x06000904 RID: 2308 RVA: 0x00085D08 File Offset: 0x00084108
	private void doFirePetMain()
	{
		if (this.currentTabIndex == 0)
		{
			if (this.selected == -1)
			{
				return;
			}
			if (this.selected > global::Char.myPetz().arrItemBody.Length - 1)
			{
				return;
			}
			MyVector myVector = new MyVector(string.Empty);
			Item item = global::Char.myPetz().arrItemBody[this.selected];
			this.currItem = item;
			if (this.currItem != null)
			{
				myVector.addElement(new Command(mResources.MOVEOUT, this, 2006, this.currItem));
				GameCanvas.menu.startAt(myVector, this.X, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
				this.addItemDetail(this.currItem);
			}
			else
			{
				this.cp = null;
			}
		}
		if (this.currentTabIndex == 1)
		{
			this.doFirePetStatus();
		}
		if (this.currentTabIndex == 2)
		{
			this.doFireInventory();
		}
	}

	// Token: 0x06000905 RID: 2309 RVA: 0x00085DFC File Offset: 0x000841FC
	private void doFirePetStatus()
	{
		if (this.selected == -1)
		{
			return;
		}
		if (this.selected == 5)
		{
			GameCanvas.startYesNoDlg(mResources.sure_fusion, new Command(mResources.YES, 888351), new Command(mResources.NO, 2001));
			return;
		}
		Service.gI().petStatus((sbyte)this.selected);
		if (this.selected < 4)
		{
			global::Char.myPetz().petStatus = (sbyte)this.selected;
		}
	}

	// Token: 0x06000906 RID: 2310 RVA: 0x00085E7C File Offset: 0x0008427C
	private void doFireTop()
	{
		if (this.selected < -1)
		{
			return;
		}
		if (this.isThachDau)
		{
			Service.gI().sendTop(this.topName, (sbyte)this.selected);
		}
		else
		{
			MyVector myVector = new MyVector(string.Empty);
			myVector.addElement(new Command(mResources.CHAR_ORDER[0], this, 9999, (TopInfo)this.vTop.elementAt(this.selected)));
			GameCanvas.menu.startAt(myVector, this.X, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
			this.addThachDauDetail((TopInfo)this.vTop.elementAt(this.selected));
		}
	}

	// Token: 0x06000907 RID: 2311 RVA: 0x00085F40 File Offset: 0x00084340
	private void doFireMapTrans()
	{
		this.doFireZone();
	}

	// Token: 0x06000908 RID: 2312 RVA: 0x00085F48 File Offset: 0x00084348
	private void doFireGiaoDich()
	{
		if (this.currentTabIndex == 0 && this.Equals(GameCanvas.panel))
		{
			this.doFireInventory();
			return;
		}
		if ((this.currentTabIndex == 0 && this.Equals(GameCanvas.panel2)) || this.currentTabIndex == 2)
		{
			if (this.Equals(GameCanvas.panel2))
			{
				this.currItem = (Item)GameCanvas.panel2.vFriendGD.elementAt(this.selected);
			}
			else
			{
				this.currItem = (Item)GameCanvas.panel.vFriendGD.elementAt(this.selected);
			}
			Res.outz2("toi day select= " + this.selected);
			MyVector myVector = new MyVector();
			myVector.addElement(new Command(mResources.CLOSE, this, 8000, this.currItem));
			if (this.currItem != null)
			{
				GameCanvas.menu.startAt(myVector, this.X, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
				this.addItemDetail(this.currItem);
			}
			else
			{
				this.cp = null;
			}
		}
		if (this.currentTabIndex == 1)
		{
			if (this.selected == this.currentListLength - 3)
			{
				if (this.isLock)
				{
					return;
				}
				this.putMoney();
			}
			else if (this.selected == this.currentListLength - 2)
			{
				if (!this.isAccept)
				{
					this.isLock = !this.isLock;
					if (this.isLock)
					{
						Service.gI().giaodich(5, -1, -1, -1);
					}
					else
					{
						this.hide();
						InfoDlg.showWait();
						Service.gI().giaodich(3, -1, -1, -1);
					}
				}
				else
				{
					this.isAccept = false;
				}
			}
			else if (this.selected == this.currentListLength - 1)
			{
				if (this.isLock && !this.isAccept && this.isFriendLock)
				{
					GameCanvas.startYesNoDlg(mResources.do_u_sure_to_trade, new Command(mResources.YES, this, 7002, null), new Command(mResources.NO, this, 4005, null));
				}
			}
			else
			{
				if (this.isLock)
				{
					return;
				}
				this.currItem = (Item)GameCanvas.panel.vMyGD.elementAt(this.selected);
				MyVector myVector2 = new MyVector();
				myVector2.addElement(new Command(mResources.CLOSE, this, 8000, this.currItem));
				if (this.currItem != null)
				{
					GameCanvas.menu.startAt(myVector2, this.X, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
					this.addItemDetail(this.currItem);
				}
				else
				{
					this.cp = null;
				}
			}
		}
		if (GameCanvas.isTouch)
		{
			this.selected = -1;
		}
	}

	// Token: 0x06000909 RID: 2313 RVA: 0x00086240 File Offset: 0x00084640
	private void doFireCombine()
	{
		if (this.currentTabIndex == 0)
		{
			if (this.selected == -1)
			{
				return;
			}
			if (this.vItemCombine.size() == 0)
			{
				return;
			}
			if (this.selected == this.vItemCombine.size())
			{
				this.keyTouchCombine = -1;
				this.selected = ((!GameCanvas.isTouch) ? 0 : -1);
				InfoDlg.showWait();
				Service.gI().combine(1, this.vItemCombine);
				SpecialSkill.startMenu();
				return;
			}
			if (this.selected > this.vItemCombine.size() - 1)
			{
				return;
			}
			this.currItem = (Item)GameCanvas.panel.vItemCombine.elementAt(this.selected);
			MyVector myVector = new MyVector();
			myVector.addElement(new Command(mResources.GETOUT, this, 6001, this.currItem));
			if (Mod.Mod.isDapDo)
			{
				myVector.addElement(new Command("Chọn Số Sao Cần Đập", this, 8010, this.currItem));
			}
			if (this.currItem != null)
			{
				GameCanvas.menu.startAt(myVector, this.X, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
				this.addItemDetail(this.currItem);
			}
			else
			{
				this.cp = null;
			}
		}
		if (this.currentTabIndex == 1)
		{
			this.doFireInventory();
		}
	}

	// Token: 0x0600090A RID: 2314 RVA: 0x0008637F File Offset: 0x0008477F
	private void doFirePlayerMenu()
	{
		if (this.selected == -1)
		{
			return;
		}
		this.isSelectPlayerMenu = true;
		this.hide();
	}

	// Token: 0x0600090B RID: 2315 RVA: 0x0008639C File Offset: 0x0008479C
	private void doFireShop()
	{
		this.currItem = null;
		if (this.selected < 0)
		{
			return;
		}
		MyVector myVector = new MyVector();
		if (this.currentTabIndex < this.currentTabName.Length - ((GameCanvas.panel2 == null) ? 1 : 0) && this.type != 17)
		{
			this.currItem = global::Char.myCharz().arrItemShop[this.currentTabIndex][this.selected];
			if (this.currItem != null)
			{
				if (this.currItem.isBuySpec)
				{
					if (this.currItem.buySpec > 0)
					{
						myVector.addElement(new Command(mResources.buy_with + "\n" + Res.formatNumber2((long)this.currItem.buySpec), this, 3005, this.currItem));
						myVector.addElement(new Command("Mua Nhiều\n" + Res.formatNumber2((long)this.currItem.buySpec) + "/1", Mod.Mod.gI(), 991, new string[]
							{
								"Nhập Số Item Cần Mua",
								"Chỉ Được Nhập Số"
							}));
					}
				}
				else if (this.typeShop == 4)
				{
					myVector.addElement(new Command(mResources.receive_upper, this, 30001, this.currItem));
					myVector.addElement(new Command(mResources.DELETE, this, 30002, this.currItem));
					myVector.addElement(new Command(mResources.receive_all, this, 30003, this.currItem));
				}
				else if (this.currItem.buyCoin == 0 && this.currItem.buyGold == 0)
				{
					if (this.currItem.powerRequire != 0L)
					{
						myVector.addElement(new Command(string.Concat(new string[]
						{
							mResources.learn_with,
							"\n",
							Res.formatNumber(this.currItem.powerRequire),
							" \n",
							mResources.potential
						}), this, 3004, this.currItem));
					}
					else
					{
						myVector.addElement(new Command(mResources.receive_upper + "\n" + mResources.free, this, 3000, this.currItem));
					}
				}
				else if (this.typeShop == 8)
				{
					if (this.currItem.buyCoin > 0)
					{
						myVector.addElement(new Command(string.Concat(new string[]
						{
							mResources.buy_with,
							"\n",
							Res.formatNumber2((long)this.currItem.buyCoin),
							"\n",
							mResources.XU
						}), this, 30001, this.currItem));

					}
					if (this.currItem.buyGold > 0)
					{
						myVector.addElement(new Command(string.Concat(new string[]
						{
							mResources.buy_with,
							"\n",
							Res.formatNumber2((long)this.currItem.buyGold),
							"\n",
							mResources.LUONG
						}), this, 30002, this.currItem));
					}
				}
				else if (this.typeShop != 2)
				{
					if (this.currItem.buyCoin > 0)
					{
						myVector.addElement(new Command(string.Concat(new string[]
						{
							mResources.buy_with,
							"\n",
							Res.formatNumber2((long)this.currItem.buyCoin),
							"\n",
							mResources.XU
						}), this, 3000, this.currItem));
					}
					if (this.currItem.buyGold > 0)
					{
						myVector.addElement(new Command(string.Concat(new string[]
						{
							mResources.buy_with,
							"\n",
							Res.formatNumber2((long)this.currItem.buyGold),
							"\n",
							mResources.LUONG
						}), this, 3001, this.currItem));
					}
				}
				else
				{
					if (this.currItem.buyCoin != -1)
					{
						myVector.addElement(new Command(string.Concat(new string[]
						{
							mResources.buy_with,
							"\n",
							Res.formatNumber2((long)this.currItem.buyCoin),
							"\n",
							mResources.XU
						}), this, 10016, this.currItem));
					}
					if (this.currItem.buyGold != -1)
					{
						myVector.addElement(new Command(string.Concat(new string[]
						{
							mResources.buy_with,
							"\n",
							Res.formatNumber2((long)this.currItem.buyGold),
							"\n",
							mResources.LUONG
						}), this, 10017, this.currItem));
					}
				}
			}
		}
		else if (this.typeShop == 0)
		{
			if (this.selected == 0)
			{
				this.setNewSelected(global::Char.myCharz().arrItemBody.Length + global::Char.myCharz().arrItemBag.Length, false);
			}
			else
			{
				this.currItem = null;
				if (!this.GetInventorySelect_isbody(this.selected, this.newSelected, global::Char.myCharz().arrItemBody))
				{
					Item item = global::Char.myCharz().arrItemBag[this.GetInventorySelect_bag(this.selected, this.newSelected, global::Char.myCharz().arrItemBody)];
					if (item != null)
					{
						this.currItem = item;
					}
				}
				else
				{
					Item item2 = global::Char.myCharz().arrItemBody[this.GetInventorySelect_body(this.selected, this.newSelected)];
					if (item2 != null)
					{
						this.currItem = item2;
					}
				}
				if (this.currItem != null)
				{
					myVector.addElement(new Command(mResources.SALE, this, 3002, this.currItem));
				}
			}
		}
		else
		{
			if (this.type == 17)
			{
				this.currItem = global::Char.myCharz().arrItemShop[4][this.selected];
			}
			else
			{
				this.currItem = global::Char.myCharz().arrItemShop[this.currentTabIndex][this.selected];
			}
			if ((int)this.currItem.buyType == 0)
			{
				if (this.currItem.isHaveOption(87))
				{
					myVector.addElement(new Command(mResources.kiguiLuong, this, 10013, this.currItem));
				}
				else
				{
					myVector.addElement(new Command(mResources.kiguiXu, this, 10012, this.currItem));
				}
			}
			else if ((int)this.currItem.buyType == 1)
			{
				myVector.addElement(new Command(mResources.huykigui, this, 10014, this.currItem));
				myVector.addElement(new Command(mResources.upTop, this, 10018, this.currItem));
			}
			else if ((int)this.currItem.buyType == 2)
			{
				myVector.addElement(new Command(mResources.nhantien, this, 10015, this.currItem));
			}
		}
		if (this.currItem != null)
		{
			global::Char.myCharz().setPartTemp(this.currItem.headTemp, this.currItem.bodyTemp, this.currItem.legTemp, this.currItem.bagTemp);
			GameCanvas.menu.startAt(myVector, this.X, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
			this.addItemDetail(this.currItem);
		}
		else
		{
			this.cp = null;
		}
	}

	// Token: 0x0600090C RID: 2316 RVA: 0x00086AD4 File Offset: 0x00084ED4
	private void doFireArchivement()
	{
		if (this.selected < 0)
		{
			return;
		}
		if (global::Char.myCharz().arrArchive[this.selected].isFinish && !global::Char.myCharz().arrArchive[this.selected].isRecieve)
		{
			if (!GameCanvas.isTouch)
			{
				Service.gI().getArchivemnt(this.selected);
			}
			else if (GameCanvas.px > this.xScroll + this.wScroll - 40)
			{
				Service.gI().getArchivemnt(this.selected);
			}
		}
	}

	// Token: 0x0600090D RID: 2317 RVA: 0x00086B70 File Offset: 0x00084F70
	private void doFireInventory()
	{
		Res.outz("fire inventory");
		if (global::Char.myCharz().statusMe == 14)
		{
			GameCanvas.startOKDlg(mResources.can_not_do_when_die);
			return;
		}
		if (this.selected == -1)
		{
			return;
		}
		if (this.selected == 0)
		{
			this.setNewSelected(global::Char.myCharz().arrItemBody.Length + global::Char.myCharz().arrItemBag.Length, false);
		}
		else
		{
			this.currItem = null;
			MyVector myVector = new MyVector();
			if (!this.GetInventorySelect_isbody(this.selected, this.newSelected, global::Char.myCharz().arrItemBody))
			{
				Item item = global::Char.myCharz().arrItemBag[this.GetInventorySelect_bag(this.selected, this.newSelected, global::Char.myCharz().arrItemBody)];
				if (item != null)
				{
					this.currItem = item;
					if (GameCanvas.panel.type == 12)
					{
						myVector.addElement(new Command(mResources.use_for_combine, this, 6000, this.currItem));
					}
					else if (GameCanvas.panel.type == 13)
					{
						myVector.addElement(new Command(mResources.use_for_trade, this, 7000, this.currItem));
					}
					else if (item.isTypeBody())
					{
						myVector.addElement(new Command(mResources.USE, this, 2000, this.currItem));
						if (global::Char.myCharz().havePet)
						{
							myVector.addElement(new Command(mResources.MOVEFORPET, this, 2005, this.currItem));
						}
					}
					else
					{
						myVector.addElement(new Command(mResources.USE, this, 2001, this.currItem));
					}
				}
			}
			else
			{
				Item item2 = global::Char.myCharz().arrItemBody[this.GetInventorySelect_body(this.selected, this.newSelected)];
				if (item2 != null)
				{
					this.currItem = item2;
					myVector.addElement(new Command(mResources.GETOUT, this, 2002, this.currItem));
				}
			}
			if (this.currItem != null)
			{
				global::Char.myCharz().setPartTemp(this.currItem.headTemp, this.currItem.bodyTemp, this.currItem.legTemp, this.currItem.bagTemp);
				if (GameCanvas.panel.type != 12 && GameCanvas.panel.type != 13)
				{
					if (this.position == 0)
					{
						myVector.addElement(new Command(mResources.MOVEOUT, this, 2003, this.currItem));
						bool flag13 = this.currItem.template.type == 29 || this.currItem.template.type == 33 || this.currItem.template.id == 380 || this.currItem.quantity >= 2;
						if (flag13)
						{
							myVector.addElement(new Command("Thêm vào ListItem", this, 8009, this.currItem));
						}
					}
					if (this.position == 1)
					{
						myVector.addElement(new Command(mResources.SALE, this, 3002, this.currItem));
					}
				}
				GameCanvas.menu.startAt(myVector, this.X, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
				this.addItemDetail(this.currItem);
			}
			else
			{
				this.cp = null;
			}
		}
	}

	// Token: 0x0600090E RID: 2318 RVA: 0x00086E60 File Offset: 0x00085260
	private void doRada()
	{
		this.hide();
		if (RadarScr.list == null || RadarScr.list.size() == 0)
		{
			Service.gI().SendRada(0, -1);
			RadarScr.gI().switchToMe();
		}
		else
		{
			RadarScr.gI().switchToMe();
		}
	}

	// Token: 0x0600090F RID: 2319 RVA: 0x00086EB4 File Offset: 0x000852B4
	private void doFireTool()
	{
		if (this.selected < 0)
		{
			return;
		}
		if (SoundMn.IsDelAcc && this.selected == Panel.strTool.Length - 1)
		{
			Service.gI().sendDelAcc();
			return;
		}
		if (!global::Char.myCharz().havePet)
		{
			switch (this.selected)
			{
				case 0:
					this.doRada();
					break;
				case 1:
					this.hide();
					Service.gI().openMenu(54);
					break;
				case 2:
					this.setTypeGameInfo();
					break;
				case 3:
					Service.gI().getFlag(0, -1);
					InfoDlg.showWait();
					break;
				case 4:
					if (global::Char.myCharz().statusMe == 14)
					{
						GameCanvas.startOKDlg(mResources.can_not_do_when_die);
						return;
					}
					Service.gI().openUIZone();
					break;
				case 5:
					GameCanvas.endDlg();
					if (global::Char.myCharz().checkLuong() < 5)
					{
						GameCanvas.startOKDlg(mResources.not_enough_luong_world_channel);
						return;
					}
					if (this.chatTField == null)
					{
						this.chatTField = new ChatTextField();
						this.chatTField.tfChat.y = GameCanvas.h - 35 - ChatTextField.gI().tfChat.height;
						this.chatTField.initChatTextField();
						this.chatTField.parentScreen = GameCanvas.panel;
					}
					this.chatTField.strChat = mResources.world_channel_5_luong;
					this.chatTField.tfChat.name = mResources.CHAT;
					this.chatTField.to = string.Empty;
					this.chatTField.isShow = true;
					this.chatTField.tfChat.isFocus = true;
					this.chatTField.tfChat.setIputType(TField.INPUT_TYPE_ANY);
					if (Main.isWindowsPhone)
					{
						this.chatTField.tfChat.strInfo = this.chatTField.strChat;
					}
					if (!Main.isPC)
					{
						this.chatTField.startChat2(this, string.Empty);
					}
					else if (GameCanvas.isTouch)
					{
						this.chatTField.tfChat.doChangeToTextBox();
					}
					break;
				case 6:
					this.setTypeAccount();
					break;
				case 7:
					this.setTypeOption();
					break;
				case 8:
					GameCanvas.loginScr.backToRegister();
					break;
				case 9:
					if (GameCanvas.loginScr.isLogin2)
					{
						SoundMn.gI().backToRegister();
					}
					break;
			}
		}
		else
		{
			switch (this.selected)
			{
				case 0:
					this.doRada();
					break;
				case 1:
					this.hide();
					Service.gI().openMenu(54);
					break;
				case 2:
					this.setTypeGameInfo();
					break;
				case 3:
					this.doFirePet();
					break;
				case 4:
					Service.gI().getFlag(0, -1);
					InfoDlg.showWait();
					break;
				case 5:
					if (global::Char.myCharz().statusMe == 14)
					{
						GameCanvas.startOKDlg(mResources.can_not_do_when_die);
						return;
					}
					Service.gI().openUIZone();
					break;
				case 6:
					GameCanvas.endDlg();
					if (global::Char.myCharz().checkLuong() < 5)
					{
						GameCanvas.startOKDlg(mResources.not_enough_luong_world_channel);
						return;
					}
					if (this.chatTField == null)
					{
						this.chatTField = new ChatTextField();
						this.chatTField.tfChat.y = GameCanvas.h - 35 - ChatTextField.gI().tfChat.height;
						this.chatTField.initChatTextField();
						this.chatTField.parentScreen = GameCanvas.panel;
					}
					this.chatTField.strChat = mResources.world_channel_5_luong;
					this.chatTField.tfChat.name = mResources.CHAT;
					this.chatTField.to = string.Empty;
					this.chatTField.isShow = true;
					this.chatTField.tfChat.isFocus = true;
					this.chatTField.tfChat.setIputType(TField.INPUT_TYPE_ANY);
					if (Main.isWindowsPhone)
					{
						this.chatTField.tfChat.strInfo = this.chatTField.strChat;
					}
					if (!Main.isPC)
					{
						this.chatTField.startChat2(this, string.Empty);
					}
					else if (GameCanvas.isTouch)
					{
						this.chatTField.tfChat.doChangeToTextBox();
					}
					break;
				case 7:
					this.setTypeAccount();
					break;
				case 8:
					this.setTypeOption();
					break;
				case 9:
					GameCanvas.loginScr.backToRegister();
					break;
				case 10:
					if (GameCanvas.loginScr.isLogin2)
					{
						SoundMn.gI().backToRegister();
					}
					break;
			}
		}
	}

	// Token: 0x06000910 RID: 2320 RVA: 0x00087384 File Offset: 0x00085784
	private void setTypeGameSubInfo()
	{
		ShowBoss boss = (ShowBoss)Mod.Mod.bossVip.elementAt(this.infoSelect);
		string content = "BOSS: " + boss.nameBoss +"\nMap: " + boss.mapName + "\nNgười tiêu diệt: " + (string.IsNullOrEmpty(boss.playerKill) ? "Chưa có thông tin" : boss.playerKill);
		Panel.contenInfo = mFont.tahoma_7_grey.splitFontArray(content, this.wScroll - 40);
		this.currentListLength = Panel.contenInfo.Length;
		this.ITEM_HEIGHT = 40;
		this.selected = ((!GameCanvas.isTouch) ? 0 : -1);
		this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
		if (this.cmyLim < 0)
		{
			this.cmyLim = 0;
		}
		if (this.cmy < 0)
		{
			this.cmy = (this.cmtoY = 0);
		}
		if (this.cmy > this.cmyLim)
		{
			this.cmy = (this.cmtoY = this.cmyLim);
		}
		this.type = 24;
		this.setType(0);
	}

	// Token: 0x06000911 RID: 2321 RVA: 0x00087470 File Offset: 0x00085870
	private void setTypeGameInfo()
	{
		this.currentListLength = Panel.vGameInfo.size();
		this.ITEM_HEIGHT = 24;
		this.selected = ((!GameCanvas.isTouch) ? 0 : -1);
		this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
		if (this.cmyLim < 0)
		{
			this.cmyLim = 0;
		}
		if (this.cmy < 0)
		{
			this.cmy = (this.cmtoY = 0);
		}
		if (this.cmy > this.cmyLim)
		{
			this.cmy = (this.cmtoY = this.cmyLim);
		}
		this.type = 23;
		this.setType(0);
	}

	// Token: 0x06000912 RID: 2322 RVA: 0x0008752A File Offset: 0x0008592A
	private void doFirePet()
	{
		InfoDlg.showWait();
		Service.gI().petInfo();
		this.timeShow = 20;
	}

	// Token: 0x06000913 RID: 2323 RVA: 0x00087544 File Offset: 0x00085944
	private void searchClan()
	{
		this.chatTField.strChat = mResources.input_clan_name;
		this.chatTField.tfChat.name = mResources.clan_name;
		this.chatTField.to = string.Empty;
		this.chatTField.isShow = true;
		this.chatTField.tfChat.isFocus = true;
		this.chatTField.tfChat.setIputType(TField.INPUT_TYPE_ANY);
		if (Main.isWindowsPhone)
		{
			this.chatTField.tfChat.strInfo = this.chatTField.strChat;
		}
		if (!Main.isPC)
		{
			this.chatTField.startChat2(this, string.Empty);
		}
	}

	// Token: 0x06000914 RID: 2324 RVA: 0x000875F8 File Offset: 0x000859F8
	private void chatClan()
	{
		this.chatTField.strChat = mResources.chat_clan;
		this.chatTField.tfChat.name = mResources.CHAT;
		this.chatTField.to = string.Empty;
		this.chatTField.isShow = true;
		this.chatTField.tfChat.isFocus = true;
		this.chatTField.tfChat.setIputType(TField.INPUT_TYPE_ANY);
		if (Main.isWindowsPhone)
		{
			this.chatTField.tfChat.strInfo = this.chatTField.strChat;
		}
		if (!Main.isPC)
		{
			this.chatTField.startChat2(this, string.Empty);
		}
	}

	// Token: 0x06000915 RID: 2325 RVA: 0x000876AC File Offset: 0x00085AAC
	public void creatClan()
	{
		this.chatTField.strChat = mResources.input_clan_name_to_create;
		this.chatTField.tfChat.name = mResources.input_clan_name;
		this.chatTField.to = string.Empty;
		this.chatTField.isShow = true;
		this.chatTField.tfChat.setIputType(TField.INPUT_TYPE_ANY);
		if (Main.isWindowsPhone)
		{
			this.chatTField.tfChat.strInfo = this.chatTField.strChat;
		}
		if (!Main.isPC)
		{
			this.chatTField.startChat2(this, string.Empty);
		}
	}

	// Token: 0x06000916 RID: 2326 RVA: 0x00087750 File Offset: 0x00085B50
	public void putMoney()
	{
		if (this.chatTField == null)
		{
			this.chatTField = new ChatTextField();
			this.chatTField.tfChat.y = GameCanvas.h - 35 - ChatTextField.gI().tfChat.height;
			this.chatTField.initChatTextField();
			this.chatTField.parentScreen = GameCanvas.panel;
		}
		this.chatTField.strChat = mResources.input_money_to_trade;
		this.chatTField.tfChat.name = mResources.input_money;
		this.chatTField.to = string.Empty;
		this.chatTField.isShow = true;
		this.chatTField.tfChat.setIputType(TField.INPUT_TYPE_NUMERIC);
		this.chatTField.tfChat.setMaxTextLenght(9);
		if (GameCanvas.isTouch)
		{
			this.chatTField.tfChat.doChangeToTextBox();
		}
		if (Main.isWindowsPhone)
		{
			this.chatTField.tfChat.strInfo = this.chatTField.strChat;
		}
		if (!Main.isPC)
		{
			this.chatTField.startChat2(this, string.Empty);
		}
	}

	// Token: 0x06000917 RID: 2327 RVA: 0x00087878 File Offset: 0x00085C78
	public void putQuantily()
	{
		if (this.chatTField == null)
		{
			this.chatTField = new ChatTextField();
			this.chatTField.tfChat.y = GameCanvas.h - 35 - ChatTextField.gI().tfChat.height;
			this.chatTField.initChatTextField();
			this.chatTField.parentScreen = GameCanvas.panel;
		}
		this.chatTField.strChat = mResources.input_quantity_to_trade;
		this.chatTField.tfChat.name = mResources.input_quantity;
		this.chatTField.to = string.Empty;
		this.chatTField.isShow = true;
		this.chatTField.tfChat.setIputType(TField.INPUT_TYPE_NUMERIC);
		if (GameCanvas.isTouch)
		{
			this.chatTField.tfChat.doChangeToTextBox();
		}
		if (Main.isWindowsPhone)
		{
			this.chatTField.tfChat.strInfo = this.chatTField.strChat;
		}
		if (!Main.isPC)
		{
			this.chatTField.startChat2(this, string.Empty);
		}
	}

	// Token: 0x06000918 RID: 2328 RVA: 0x00087990 File Offset: 0x00085D90
	public void chagenSlogan()
	{
		this.chatTField.strChat = mResources.input_clan_slogan;
		this.chatTField.tfChat.name = mResources.input_clan_slogan;
		this.chatTField.to = string.Empty;
		this.chatTField.isShow = true;
		this.chatTField.tfChat.isFocus = true;
		this.chatTField.tfChat.setIputType(TField.INPUT_TYPE_ANY);
		if (Main.isWindowsPhone)
		{
			this.chatTField.tfChat.strInfo = this.chatTField.strChat;
		}
		if (!Main.isPC)
		{
			this.chatTField.startChat2(this, string.Empty);
		}
	}

	// Token: 0x06000919 RID: 2329 RVA: 0x00087A44 File Offset: 0x00085E44
	public void changeIcon()
	{
		if (this.tabIcon == null)
		{
			this.tabIcon = new TabClanIcon();
		}
		this.tabIcon.text = this.chatTField.tfChat.getText();
		this.tabIcon.show(false);
		this.chatTField.isShow = false;
	}

	// Token: 0x0600091A RID: 2330 RVA: 0x00087A9C File Offset: 0x00085E9C
	private void addFriend(InfoItem info)
	{
		string text = "|0|1|" + info.charInfo.cName;
		text += "\n";
		if (info.isOnline)
		{
			text = text + "|4|1|" + mResources.is_online;
		}
		else
		{
			text = text + "|3|1|" + mResources.is_offline;
		}
		text += "\n--";
		string text2 = text;
		text = string.Concat(new string[]
		{
			text2,
			"\n|5|",
			mResources.power,
			": ",
			info.s
		});
		this.cp = new ChatPopup();
		this.popUpDetailInit(this.cp, text);
		this.charInfo = info.charInfo;
		this.currItem = null;
	}

	// Token: 0x0600091B RID: 2331 RVA: 0x00087B68 File Offset: 0x00085F68
	private void doFireEnemy()
	{
		if (this.selected < 0)
		{
			return;
		}
		if (this.vEnemy.size() == 0)
		{
			return;
		}
		MyVector myVector = new MyVector();
		this.currInfoItem = this.selected;
		myVector.addElement(new Command(mResources.REVENGE, this, 10000, (InfoItem)this.vEnemy.elementAt(this.currInfoItem)));
		myVector.addElement(new Command(mResources.DELETE, this, 10001, (InfoItem)this.vEnemy.elementAt(this.currInfoItem)));
		GameCanvas.menu.startAt(myVector, this.X, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
		this.addFriend((InfoItem)this.vEnemy.elementAt(this.selected));
	}

	// Token: 0x0600091C RID: 2332 RVA: 0x00087C48 File Offset: 0x00086048
	private void doFireFriend()
	{
		if (this.selected < 0)
		{
			return;
		}
		if (this.vFriend.size() == 0)
		{
			return;
		}
		MyVector myVector = new MyVector();
		this.currInfoItem = this.selected;
		myVector.addElement(new Command(mResources.CHAT, this, 8001, (InfoItem)this.vFriend.elementAt(this.currInfoItem)));
		myVector.addElement(new Command(mResources.DELETE, this, 8002, (InfoItem)this.vFriend.elementAt(this.currInfoItem)));
		myVector.addElement(new Command(mResources.den, this, 8004, (InfoItem)this.vFriend.elementAt(this.currInfoItem)));
		GameCanvas.menu.startAt(myVector, this.X, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
		this.addFriend((InfoItem)this.vFriend.elementAt(this.selected));
	}

	// Token: 0x0600091D RID: 2333 RVA: 0x00087D54 File Offset: 0x00086154
	private void doFireChangeFlag()
	{
		if (this.selected < 0)
		{
			return;
		}
		MyVector myVector = new MyVector();
		this.currInfoItem = this.selected;
		myVector.addElement(new Command(mResources.change_flag, this, 10030, null));
		myVector.addElement(new Command(mResources.BACK, this, 10031, null));
		GameCanvas.menu.startAt(myVector, this.X, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
	}

	// Token: 0x0600091E RID: 2334 RVA: 0x00087DDC File Offset: 0x000861DC
	private void doFireLogMessage()
	{
		if (this.selected == 0)
		{
			this.isViewChatServer = !this.isViewChatServer;
			Rms.saveRMSInt("viewchat", (!this.isViewChatServer) ? 0 : 1);
			if (GameCanvas.isTouch)
			{
				this.selected = -1;
			}
			return;
		}
		if (this.selected < 0)
		{
			return;
		}
		if (this.logChat.size() == 0)
		{
			return;
		}
		MyVector myVector = new MyVector();
		this.currInfoItem = this.selected - 1;
		myVector.addElement(new Command(mResources.CHAT, this, 8001, (InfoItem)this.logChat.elementAt(this.currInfoItem)));
		myVector.addElement(new Command(mResources.make_friend, this, 8003, (InfoItem)this.logChat.elementAt(this.currInfoItem)));
		GameCanvas.menu.startAt(myVector, this.X, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
		this.addLogMessage((InfoItem)this.logChat.elementAt(this.selected - 1));
	}

	// Token: 0x0600091F RID: 2335 RVA: 0x00087F08 File Offset: 0x00086308
	private void doFireClanOption()
	{
		try
		{
			this.partID = null;
			this.charInfo = null;
			Res.outz("cSelect= " + this.cSelected);
			if (this.selected < 0)
			{
				this.cSelected = -1;
			}
			else
			{
				if (global::Char.myCharz().clan == null)
				{
					if (this.selected == 0)
					{
						if (this.cSelected == 0)
						{
							this.searchClan();
						}
						else if (this.cSelected == 1)
						{
							InfoDlg.showWait();
							this.creatClan();
							Service.gI().getClan(1, -1, null);
						}
					}
					else if (this.selected != -1)
					{
						if (this.selected == 1)
						{
							if (this.isSearchClan)
							{
								Service.gI().searchClan(string.Empty);
							}
							else if (this.isViewMember && this.currClan != null)
							{
								GameCanvas.startYesNoDlg(mResources.do_u_want_join_clan + this.currClan.name, new Command(mResources.YES, this, 4000, this.currClan), new Command(mResources.NO, this, 4005, this.currClan));
							}
						}
						else if (this.isSearchClan)
						{
							this.currClan = this.getCurrClan();
							if (this.currClan != null)
							{
								MyVector myVector = new MyVector();
								myVector.addElement(new Command(mResources.request_join_clan, this, 4000, this.currClan));
								myVector.addElement(new Command(mResources.view_clan_member, this, 4001, this.currClan));
								GameCanvas.menu.startAt(myVector, this.X, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
								this.addClanDetail(this.getCurrClan());
							}
						}
						else if (this.isViewMember)
						{
							this.currMem = this.getCurrMember();
							if (this.currMem != null)
							{
								MyVector myVector2 = new MyVector();
								myVector2.addElement(new Command(mResources.CLOSE, this, 8000, this.currClan));
								GameCanvas.menu.startAt(myVector2, this.X, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
								GameCanvas.menu.startAt(myVector2, 0, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
								this.addClanMemberDetail(this.currMem);
							}
						}
					}
				}
				else if (this.selected == 0)
				{
					if (this.isMessage)
					{
						if (this.cSelected == 0)
						{
							if (this.myMember.size() > 1)
							{
								this.chatClan();
							}
							else
							{
								this.member = null;
								this.isSearchClan = false;
								this.isViewMember = true;
								this.isMessage = false;
								this.currentListLength = this.myMember.size() + 2;
								this.initTabClans();
							}
						}
						if (this.cSelected == 1)
						{
							Service.gI().clanMessage(1, null, -1);
						}
						if (this.cSelected == 2)
						{
							this.member = null;
							this.isSearchClan = false;
							this.isViewMember = true;
							this.isMessage = false;
							this.currentListLength = this.myMember.size() + 2;
							this.initTabClans();
							this.getCurrClanOtion();
						}
					}
					else if (this.isViewMember)
					{
						if (this.cSelected == 0)
						{
							this.isSearchClan = false;
							this.isViewMember = false;
							this.isMessage = true;
							this.currentListLength = ClanMessage.vMessage.size() + 2;
							this.initTabClans();
						}
						if (this.cSelected == 1)
						{
							if (this.myMember.size() > 1)
							{
								Service.gI().leaveClan();
							}
							else
							{
								this.chagenSlogan();
							}
						}
						if (this.cSelected == 2)
						{
							if (this.myMember.size() > 1)
							{
								this.chagenSlogan();
							}
							else
							{
								Service.gI().getClan(3, -1, null);
							}
						}
						if (this.cSelected == 3)
						{
							Service.gI().getClan(3, -1, null);
						}
					}
				}
				else if (this.selected == 1)
				{
					if (this.isSearchClan)
					{
						Service.gI().searchClan(string.Empty);
					}
				}
				else if (this.isSearchClan)
				{
					this.currClan = this.getCurrClan();
					if (this.currClan != null)
					{
						MyVector myVector3 = new MyVector();
						myVector3.addElement(new Command(mResources.view_clan_member, this, 4001, this.currClan));
						GameCanvas.menu.startAt(myVector3, this.X, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
						this.addClanDetail(this.getCurrClan());
					}
				}
				else if (this.isViewMember)
				{
					Res.outz("TOI DAY 1");
					this.currMem = this.getCurrMember();
					if (this.currMem != null)
					{
						MyVector myVector4 = new MyVector();
						Res.outz("TOI DAY 2");
						if (this.member != null)
						{
							myVector4.addElement(new Command(mResources.CLOSE, this, 8000, null));
							Res.outz("TOI DAY 3");
						}
						else if (this.myMember != null)
						{
							Res.outz("TOI DAY 4");
							Res.outz("my role= " + global::Char.myCharz().role);
							if (global::Char.myCharz().charID == this.currMem.ID || (int)global::Char.myCharz().role == 2)
							{
								myVector4.addElement(new Command(mResources.CLOSE, this, 8000, this.currMem));
							}
							if ((int)global::Char.myCharz().role < 2 && global::Char.myCharz().charID != this.currMem.ID)
							{
								Res.outz("TOI DAY");
								if ((int)this.currMem.role == 0 || (int)this.currMem.role == 1)
								{
									myVector4.addElement(new Command(mResources.CLOSE, this, 8000, this.currMem));
								}
								if ((int)this.currMem.role == 2)
								{
									myVector4.addElement(new Command(mResources.create_clan_co_leader, this, 5002, this.currMem));
								}
								if ((int)global::Char.myCharz().role == 0)
								{
									myVector4.addElement(new Command(mResources.create_clan_leader, this, 5001, this.currMem));
									if ((int)this.currMem.role == 1)
									{
										myVector4.addElement(new Command(mResources.disable_clan_mastership, this, 5003, this.currMem));
									}
								}
							}
							if ((int)global::Char.myCharz().role < (int)this.currMem.role)
							{
								myVector4.addElement(new Command(mResources.kick_clan_mem, this, 5004, this.currMem));
							}
						}
						GameCanvas.menu.startAt(myVector4, this.X, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
						this.addClanMemberDetail(this.currMem);
					}
				}
				else if (this.isMessage)
				{
					this.currMess = this.getCurrMessage();
					if (this.currMess != null)
					{
						if (this.currMess.type == 0)
						{
							MyVector myVector5 = new MyVector();
							myVector5.addElement(new Command(mResources.CLOSE, this, 8000, this.currMess));
							GameCanvas.menu.startAt(myVector5, this.X, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
							this.addMessageDetail(this.currMess);
						}
						else if (this.currMess.type == 1)
						{
							if (this.currMess.playerId != global::Char.myCharz().charID && this.cSelected != -1)
							{
								Service.gI().clanDonate(this.currMess.id);
							}
						}
						else if (this.currMess.type == 2 && this.currMess.option != null)
						{
							if (this.cSelected == 0)
							{
								Service.gI().joinClan(this.currMess.id, 1);
							}
							else if (this.cSelected == 1)
							{
								Service.gI().joinClan(this.currMess.id, 0);
							}
						}
					}
				}
				if (GameCanvas.isTouch)
				{
					this.cSelected = -1;
					this.selected = -1;
				}
			}
		}
		catch (Exception)
		{
			throw;
		}
	}

	// Token: 0x06000920 RID: 2336 RVA: 0x000887C4 File Offset: 0x00086BC4
	private void doFireMain()
	{
		try
		{
			if (this.currentTabIndex == 0)
			{
				this.setTypeMap();
			}
			if (this.currentTabIndex == 1)
			{
				this.doFireInventory();
			}
			if (this.currentTabIndex == 2)
			{
				this.doFireSkill();
			}
			if (this.currentTabIndex == 3)
			{
				if (this.mainTabName.Length == 4)
				{
					this.doFireTool();
				}
				else
				{
					this.doFireClanOption();
				}
			}
			if (this.currentTabIndex == 4)
			{
				this.doFireTool();
			}
		}
		catch (Exception ex)
		{
			Res.outz("Throw ex " + ex.StackTrace);
		}
	}

	// Token: 0x06000921 RID: 2337 RVA: 0x00088874 File Offset: 0x00086C74
	private void doFireSkill()
	{
		if (this.selected < 0)
		{
			return;
		}
		if (global::Char.myCharz().statusMe == 14)
		{
			GameCanvas.startOKDlg(mResources.can_not_do_when_die);
			return;
		}
		if (this.selected != 0 && this.selected != 1 && this.selected != 2 && this.selected != 3 && this.selected != 4 && this.selected != 5)
		{
			int num = this.selected - 6;
			SkillTemplate skillTemplate = global::Char.myCharz().nClass.skillTemplates[num];
			Skill skill = global::Char.myCharz().getSkill(skillTemplate);
			Skill skill2 = null;
			MyVector myVector = new MyVector(string.Empty);
			if (skill != null)
			{
				if (skill.point == skillTemplate.maxPoint)
				{
					myVector.addElement(new Command(mResources.make_shortcut, this, 9003, skill.template));
					myVector.addElement(new Command(mResources.CLOSE, 2));
				}
				else
				{
					skill2 = skillTemplate.skills[skill.point];
					myVector.addElement(new Command(mResources.UPGRADE, this, 9002, skill2));
					myVector.addElement(new Command(mResources.make_shortcut, this, 9003, skill.template));
				}
			}
			else
			{
				skill2 = skillTemplate.skills[0];
				myVector.addElement(new Command(mResources.learn, this, 9004, skill2));
			}
			GameCanvas.menu.startAt(myVector, this.X, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
			this.addSkillDetail(skillTemplate, skill, skill2);
			return;
		}
		long cTiemNang = global::Char.myCharz().cTiemNang;
		int cHPGoc = global::Char.myCharz().cHPGoc;
		int cMPGoc = global::Char.myCharz().cMPGoc;
		int cDamGoc = global::Char.myCharz().cDamGoc;
		int cDefGoc = global::Char.myCharz().cDefGoc;
		int cCriticalGoc = global::Char.myCharz().cCriticalGoc;
		int num2 = 1000;
		if (this.selected == 0)
		{
			if (cTiemNang < (long)(global::Char.myCharz().cHPGoc + num2))
			{
				GameCanvas.startOKDlg(string.Concat(new object[]
				{
					mResources.not_enough_potential_point1,
					global::Char.myCharz().cTiemNang,
					mResources.not_enough_potential_point2,
					global::Char.myCharz().cHPGoc + num2
				}));
				return;
			}
			if (cTiemNang > (long)cHPGoc && cTiemNang < (long)(10 * (2 * (cHPGoc + num2) + 180) / 2))
			{
				GameCanvas.startYesNoDlg(string.Concat(new object[]
				{
					mResources.use_potential_point_for1,
					cHPGoc + num2,
					mResources.use_potential_point_for2,
					global::Char.myCharz().hpFrom1000TiemNang,
					mResources.for_HP
				}), new Command(mResources.increase_upper, this, 9000, null), new Command(mResources.CANCEL, this, 4007, null));
				return;
			}
			if (cTiemNang >= (long)(10 * (2 * (cHPGoc + num2) + 180) / 2) && cTiemNang < (long)(100 * (2 * (cHPGoc + num2) + 1980) / 2))
			{
				MyVector myVector2 = new MyVector(string.Empty);
				myVector2.addElement(new Command(string.Concat(new object[]
				{
					mResources.increase_upper,
					"\n",
					global::Char.myCharz().hpFrom1000TiemNang,
					mResources.HP,
					"\n-",
					Res.formatNumber2((long)(cHPGoc + num2))
				}), this, 9000, null));
				myVector2.addElement(new Command(string.Concat(new object[]
				{
					mResources.increase_upper,
					"\n",
					10 * (int)global::Char.myCharz().hpFrom1000TiemNang,
					mResources.HP,
					"\n-",
					Res.formatNumber2((long)(10 * (2 * (cHPGoc + num2) + 180) / 2))
				}), this, 9006, null));
				GameCanvas.menu.startAt(myVector2, this.X, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
				this.addSkillDetail2(this.selected);
			}
			if (cTiemNang >= (long)(100 * (2 * (cHPGoc + num2) + 1980) / 2))
			{
				MyVector myVector3 = new MyVector(string.Empty);
				myVector3.addElement(new Command(string.Concat(new object[]
				{
					mResources.increase_upper,
					"\n",
					global::Char.myCharz().hpFrom1000TiemNang,
					mResources.HP,
					"\n-",
					Res.formatNumber2((long)(cHPGoc + num2))
				}), this, 9000, null));
				myVector3.addElement(new Command(string.Concat(new object[]
				{
					mResources.increase_upper,
					"\n",
					10 * (int)global::Char.myCharz().hpFrom1000TiemNang,
					mResources.HP,
					"\n-",
					Res.formatNumber2((long)(10 * (2 * (cHPGoc + num2) + 180) / 2))
				}), this, 9006, null));
				myVector3.addElement(new Command(string.Concat(new object[]
				{
					mResources.increase_upper,
					"\n",
					100 * (int)global::Char.myCharz().hpFrom1000TiemNang,
					mResources.HP,
					"\n-",
					Res.formatNumber2((long)(100 * (2 * (cHPGoc + num2) + 1980) / 2))
				}), this, 9007, null));
				GameCanvas.menu.startAt(myVector3, this.X, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
				this.addSkillDetail2(this.selected);
			}
		}
		if (this.selected == 1)
		{
			if (global::Char.myCharz().cTiemNang < (long)(global::Char.myCharz().cMPGoc + num2))
			{
				GameCanvas.startOKDlg(string.Concat(new object[]
				{
					mResources.not_enough_potential_point1,
					global::Char.myCharz().cTiemNang,
					mResources.not_enough_potential_point2,
					global::Char.myCharz().cMPGoc + num2
				}));
				return;
			}
			if (cTiemNang > (long)cMPGoc && cTiemNang < (long)(10 * (2 * (cMPGoc + num2) + 180) / 2))
			{
				GameCanvas.startYesNoDlg(string.Concat(new object[]
				{
					mResources.use_potential_point_for1,
					cMPGoc + num2,
					mResources.use_potential_point_for2,
					global::Char.myCharz().mpFrom1000TiemNang,
					mResources.for_KI
				}), new Command(mResources.increase_upper, this, 9000, null), new Command(mResources.CANCEL, this, 4007, null));
				return;
			}
			if (cTiemNang >= (long)(10 * (2 * (cMPGoc + num2) + 180) / 2) && cTiemNang < (long)(100 * (2 * (cMPGoc + num2) + 1980) / 2))
			{
				MyVector myVector4 = new MyVector(string.Empty);
				myVector4.addElement(new Command(string.Concat(new object[]
				{
					mResources.increase_upper,
					"\n",
					global::Char.myCharz().mpFrom1000TiemNang,
					mResources.KI,
					"\n-",
					Res.formatNumber2((long)(cHPGoc + num2))
				}), this, 9000, null));
				myVector4.addElement(new Command(string.Concat(new object[]
				{
					mResources.increase_upper,
					"\n",
					10 * (int)global::Char.myCharz().mpFrom1000TiemNang,
					mResources.KI,
					"\n-",
					Res.formatNumber2((long)(10 * (2 * (cHPGoc + num2) + 180) / 2))
				}), this, 9006, null));
				GameCanvas.menu.startAt(myVector4, this.X, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
				this.addSkillDetail2(this.selected);
			}
			if (cTiemNang >= (long)(100 * (2 * (cMPGoc + num2) + 1980) / 2))
			{
				MyVector myVector5 = new MyVector(string.Empty);
				myVector5.addElement(new Command(string.Concat(new object[]
				{
					mResources.increase_upper,
					"\n",
					global::Char.myCharz().mpFrom1000TiemNang,
					mResources.KI,
					"\n-",
					Res.formatNumber2((long)(cMPGoc + num2))
				}), this, 9000, null));
				myVector5.addElement(new Command(string.Concat(new object[]
				{
					mResources.increase_upper,
					"\n",
					10 * (int)global::Char.myCharz().mpFrom1000TiemNang,
					mResources.KI,
					"\n-",
					Res.formatNumber2((long)(10 * (2 * (cMPGoc + num2) + 180) / 2))
				}), this, 9006, null));
				myVector5.addElement(new Command(string.Concat(new object[]
				{
					mResources.increase_upper,
					"\n",
					100 * (int)global::Char.myCharz().mpFrom1000TiemNang,
					mResources.KI,
					"\n-",
					Res.formatNumber2((long)(100 * (2 * (cMPGoc + num2) + 1980) / 2))
				}), this, 9007, null));
				GameCanvas.menu.startAt(myVector5, this.X, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
				this.addSkillDetail2(this.selected);
			}
		}
		if (this.selected == 2)
		{
			if (global::Char.myCharz().cTiemNang < (long)(global::Char.myCharz().cDamGoc * (int)global::Char.myCharz().expForOneAdd))
			{
				GameCanvas.startOKDlg(string.Concat(new object[]
				{
					mResources.not_enough_potential_point1,
					global::Char.myCharz().cTiemNang,
					mResources.not_enough_potential_point2,
					cDamGoc * 100
				}));
				return;
			}
			if (cTiemNang > (long)cDamGoc && cTiemNang < (long)(10 * (2 * cDamGoc + 9) / 2 * (int)global::Char.myCharz().expForOneAdd))
			{
				GameCanvas.startYesNoDlg(string.Concat(new object[]
				{
					mResources.use_potential_point_for1,
					cDamGoc * 100,
					mResources.use_potential_point_for2,
					global::Char.myCharz().damFrom1000TiemNang,
					mResources.for_hit_point
				}), new Command(mResources.increase_upper, this, 9000, null), new Command(mResources.CANCEL, this, 4007, null));
				return;
			}
			if (cTiemNang >= (long)(10 * (2 * cDamGoc + 9) / 2 * (int)global::Char.myCharz().expForOneAdd) && cTiemNang < (long)(100 * (2 * cDamGoc + 99) / 2 * (int)global::Char.myCharz().expForOneAdd))
			{
				MyVector myVector6 = new MyVector(string.Empty);
				myVector6.addElement(new Command(string.Concat(new object[]
				{
					mResources.increase_upper,
					"\n",
					global::Char.myCharz().damFrom1000TiemNang,
					"\n",
					mResources.hit_point,
					"\n-",
					Res.formatNumber2((long)(cDamGoc * 100))
				}), this, 9000, null));
				myVector6.addElement(new Command(string.Concat(new object[]
				{
					mResources.increase_upper,
					"\n",
					10 * (int)global::Char.myCharz().damFrom1000TiemNang,
					"\n",
					mResources.hit_point,
					"\n-",
					Res.formatNumber2((long)(10 * (2 * cDamGoc + 9) / 2 * (int)global::Char.myCharz().expForOneAdd))
				}), this, 9006, null));
				GameCanvas.menu.startAt(myVector6, this.X, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
				this.addSkillDetail2(this.selected);
			}
			if (cTiemNang >= (long)(100 * (2 * cDamGoc + 99) / 2 * (int)global::Char.myCharz().expForOneAdd))
			{
				MyVector myVector7 = new MyVector(string.Empty);
				myVector7.addElement(new Command(string.Concat(new object[]
				{
					mResources.increase_upper,
					"\n",
					global::Char.myCharz().damFrom1000TiemNang,
					"\n",
					mResources.hit_point,
					"\n-",
					Res.formatNumber2((long)(cDamGoc * 100))
				}), this, 9000, null));
				myVector7.addElement(new Command(string.Concat(new object[]
				{
					mResources.increase_upper,
					"\n",
					10 * (int)global::Char.myCharz().damFrom1000TiemNang,
					"\n",
					mResources.hit_point,
					"\n-",
					Res.formatNumber2((long)(10 * (2 * cDamGoc + 9) / 2 * (int)global::Char.myCharz().expForOneAdd))
				}), this, 9006, null));
				myVector7.addElement(new Command(string.Concat(new object[]
				{
					mResources.increase_upper,
					"\n",
					100 * (int)global::Char.myCharz().damFrom1000TiemNang,
					"\n",
					mResources.hit_point,
					"\n-",
					Res.formatNumber2((long)(100 * (2 * cDamGoc + 99) / 2 * (int)global::Char.myCharz().expForOneAdd))
				}), this, 9007, null));
				GameCanvas.menu.startAt(myVector7, this.X, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
				this.addSkillDetail2(this.selected);
			}
		}
		if (this.selected == 3)
		{
			if (global::Char.myCharz().cTiemNang < (long)(50000 + global::Char.myCharz().cDefGoc * 1000))
			{
				GameCanvas.startOKDlg(mResources.not_enough_potential_point1 + NinjaUtil.getMoneys(global::Char.myCharz().cTiemNang) + mResources.not_enough_potential_point2 + NinjaUtil.getMoneys((long)(50000 + global::Char.myCharz().cDefGoc * 1000)));
				return;
			}
			long number = (long)(2 * (cDefGoc + 5)) / 2L * 100000L;
			long number2 = 10L * (long)(2 * (cDefGoc + 5) + 9) / 2L * 100000L;
			long number3 = 100L * (long)(2 * (cDefGoc + 5) + 99) / 2L * 100000L;
			mResources.use_potential_point_for1 = mResources.increase_upper;
			MyVector myVector8 = new MyVector(string.Empty);
			myVector8.addElement(new Command(string.Concat(new string[]
			{
				mResources.use_potential_point_for1,
				"\n1 ",
				mResources.armor,
				"\n",
				Res.formatNumber2(number)
			}), this, 9000, null));
			myVector8.addElement(new Command(string.Concat(new string[]
			{
				mResources.use_potential_point_for1,
				"\n10 ",
				mResources.armor,
				"\n",
				Res.formatNumber2(number2)
			}), this, 9006, null));
			myVector8.addElement(new Command(string.Concat(new string[]
			{
				mResources.use_potential_point_for1,
				"\n100 ",
				mResources.armor,
				"\n",
				Res.formatNumber2(number3)
			}), this, 9007, null));
			GameCanvas.menu.startAt(myVector8, this.X, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
			this.addSkillDetail2(this.selected);
			return;
		}
		else
		{
			if (this.selected != 4)
			{
				if (this.selected == 5)
				{
					Service.gI().speacialSkill(0);
				}
				return;
			}
			int num3 = global::Char.myCharz().cCriticalGoc;
			if (num3 > Panel.t_tiemnang.Length - 1)
			{
				num3 = Panel.t_tiemnang.Length - 1;
			}
			long num4 = Panel.t_tiemnang[num3];
			if (global::Char.myCharz().cTiemNang < num4)
			{
				GameCanvas.startOKDlg(mResources.not_enough_potential_point1 + Res.formatNumber2(global::Char.myCharz().cTiemNang) + mResources.not_enough_potential_point2 + Res.formatNumber2(num4));
				return;
			}
			GameCanvas.startYesNoDlg(string.Concat(new object[]
			{
				mResources.use_potential_point_for1,
				Res.formatNumber(num4),
				mResources.use_potential_point_for2,
				global::Char.myCharz().criticalFrom1000Tiemnang,
				mResources.for_crit
			}), new Command(mResources.increase_upper, this, 9000, null), new Command(mResources.CANCEL, this, 4007, null));
			return;
		}
	}

	// Token: 0x06000922 RID: 2338 RVA: 0x00089914 File Offset: 0x00087D14
	private void addLogMessage(InfoItem info)
	{
		string text = "|0|1|" + info.charInfo.cName;
		text += "\n";
		text += "\n--";
		text = text + "\n|5|" + Res.split(info.s, "|", 0)[2];
		this.cp = new ChatPopup();
		this.popUpDetailInit(this.cp, text);
		this.charInfo = info.charInfo;
		this.currItem = null;
	}

	// Token: 0x06000923 RID: 2339 RVA: 0x0008999C File Offset: 0x00087D9C
	private void addSkillDetail2(int type)
	{
		string text = string.Empty;
		int num = 0;
		if (this.selected == 0)
		{
			num = global::Char.myCharz().cHPGoc + 1000;
		}
		if (this.selected == 1)
		{
			num = global::Char.myCharz().cMPGoc + 1000;
		}
		if (this.selected == 2)
		{
			num = global::Char.myCharz().cDamGoc * (int)global::Char.myCharz().expForOneAdd;
		}
		if (this.selected == 3)
		{
			num = 500000 + global::Char.myCharz().cDefGoc * 100000;
		}
		string text2 = text;
		text = string.Concat(new object[]
		{
			text2,
			"|5|2|",
			mResources.USE,
			" ",
			num,
			" ",
			mResources.potential
		});
		if (type == 0)
		{
			text = text + "\n|5|2|" + mResources.to_gain_20hp;
		}
		if (type == 1)
		{
			text = text + "\n|5|2|" + mResources.to_gain_20mp;
		}
		if (type == 2)
		{
			text = text + "\n|5|2|" + mResources.to_gain_1pow;
		}
		if (type == 3)
		{
			text = text + "\n|5|2|" + mResources.to_gain_1pow;
		}
		this.currItem = null;
		this.partID = null;
		this.charInfo = null;
		this.idIcon = -1;
		this.cp = new ChatPopup();
		this.popUpDetailInit(this.cp, text);
	}

	// Token: 0x06000924 RID: 2340 RVA: 0x00089B05 File Offset: 0x00087F05
	private void doFireClanIcon()
	{
	}

	// Token: 0x06000925 RID: 2341 RVA: 0x00089B08 File Offset: 0x00087F08
	private void doFireMap()
	{
		if (Panel.imgMap != null)
		{
			Panel.imgMap.texture = null;
			Panel.imgMap = null;
		}
		TileMap.lastPlanetId = -1;
		mSystem.gcc();
		SmallImage.loadBigRMS();
		this.setTypeMain();
		this.cmx = (this.cmtoX = 0);
	}

	// Token: 0x06000926 RID: 2342 RVA: 0x00089B56 File Offset: 0x00087F56
	private void doFireZone()
	{
		if (this.selected == -1)
		{
			return;
		}
		Res.outz("FIRE ZONE");
		this.isChangeZone = true;
		GameCanvas.panel.hide();
	}

	// Token: 0x06000927 RID: 2343 RVA: 0x00089B80 File Offset: 0x00087F80
	public void updateRequest(int recieve, int maxCap)
	{
		this.cp.says[this.cp.says.Length - 1] = string.Concat(new object[]
		{
			mResources.received,
			" ",
			recieve,
			"/",
			maxCap
		});
	}

	// Token: 0x06000928 RID: 2344 RVA: 0x00089BE0 File Offset: 0x00087FE0
	private void doFireBox()
	{
		if (this.selected < 0)
		{
			return;
		}
		this.currItem = null;
		MyVector myVector = new MyVector();
		if (this.currentTabIndex == 0 && !this.Equals(GameCanvas.panel2))
		{
			if (this.selected == 0)
			{
				this.setNewSelected(global::Char.myCharz().arrItemBox.Length, false);
			}
			else
			{
				sbyte b = (sbyte)this.GetInventorySelect_body(this.selected, this.newSelected);
				Item item = global::Char.myCharz().arrItemBox[(int)b];
				if (item != null)
				{
					if (this.isBoxClan)
					{
						myVector.addElement(new Command(mResources.GETOUT, this, 1000, item));
						myVector.addElement(new Command(mResources.USE, this, 2010, item));
					}
					else if (item.isTypeBody())
					{
						myVector.addElement(new Command(mResources.GETOUT, this, 1000, item));
					}
					else
					{
						myVector.addElement(new Command(mResources.GETOUT, this, 1000, item));
					}
					this.currItem = item;
				}
			}
		}
		if (this.currentTabIndex == 1 || this.Equals(GameCanvas.panel2))
		{
			if (this.selected == 0)
			{
				this.setNewSelected(global::Char.myCharz().arrItemBody.Length + global::Char.myCharz().arrItemBag.Length, true);
			}
			else
			{
				Item[] arrItemBody = global::Char.myCharz().arrItemBody;
				if (!this.GetInventorySelect_isbody(this.selected, this.newSelected, arrItemBody))
				{
					sbyte b2 = (sbyte)this.GetInventorySelect_bag(this.selected, this.newSelected, arrItemBody);
					Item item2 = global::Char.myCharz().arrItemBag[(int)b2];
					if (item2 != null)
					{
						myVector.addElement(new Command(mResources.move_to_chest, this, 1001, item2));
						if (item2.isTypeBody())
						{
							myVector.addElement(new Command(mResources.USE, this, 2000, item2));
						}
						else
						{
							myVector.addElement(new Command(mResources.USE, this, 2001, item2));
						}
						this.currItem = item2;
					}
				}
				else
				{
					Item item3 = global::Char.myCharz().arrItemBody[this.GetInventorySelect_body(this.selected, this.newSelected)];
					if (item3 != null)
					{
						myVector.addElement(new Command(mResources.move_to_chest2, this, 1002, item3));
						this.currItem = item3;
					}
				}
			}
		}
		if (this.currItem != null)
		{
			global::Char.myCharz().setPartTemp(this.currItem.headTemp, this.currItem.bodyTemp, this.currItem.legTemp, this.currItem.bagTemp);
			if (this.isBoxClan)
			{
				myVector.addElement(new Command(mResources.MOVEOUT, this, 2011, this.currItem));
			}
			GameCanvas.menu.startAt(myVector, this.X, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
			this.addItemDetail(this.currItem);
		}
		else
		{
			this.cp = null;
		}
	}

	// Token: 0x06000929 RID: 2345 RVA: 0x00089EEC File Offset: 0x000882EC
	public void itemRequest(sbyte itemAction, string info, sbyte where, sbyte index)
	{
		GameCanvas.endDlg();
		ItemObject itemObject = new ItemObject();
		itemObject.type = (int)itemAction;
		itemObject.id = (int)index;
		itemObject.where = (int)where;
		GameCanvas.startYesNoDlg(info, new Command(mResources.YES, this, 2004, itemObject), new Command(mResources.NO, this, 4005, null));
	}

	// Token: 0x0600092A RID: 2346 RVA: 0x00089F48 File Offset: 0x00088348
	public void saleRequest(sbyte type, string info, short id)
	{
		ItemObject itemObject = new ItemObject();
		itemObject.type = (int)type;
		itemObject.id = (int)id;
		GameCanvas.startYesNoDlg(info, new Command(mResources.YES, this, 3003, itemObject), new Command(mResources.NO, this, 4005, null));
	}
	public void VuDangChatTextField(string strChat, string strName)
	{
		bool flag = this.chatTField == null;
		if (flag)
		{
			this.chatTField = new ChatTextField();
			this.chatTField.tfChat.y = GameCanvas.h - 35 - ChatTextField.gI().tfChat.height;
			this.chatTField.initChatTextField();
			this.chatTField.parentScreen = GameCanvas.panel;
		}
		this.chatTField.strChat = strChat;
		this.chatTField.tfChat.name = strName;
		this.chatTField.to = string.Empty;
		this.chatTField.isShow = true;
		this.chatTField.tfChat.setIputType(TField.INPUT_TYPE_NUMERIC);
		this.chatTField.tfChat.setMaxTextLenght(9);
		bool isTouch = GameCanvas.isTouch;
		if (isTouch)
		{
			this.chatTField.tfChat.doChangeToTextBox();
		}
		bool isWindowsPhone = Main.isWindowsPhone;
		if (isWindowsPhone)
		{
			this.chatTField.tfChat.strInfo = this.chatTField.strChat;
		}
		bool flag2 = !Main.isPC;
		if (flag2)
		{
			this.chatTField.startChat2(this, string.Empty);
		}
	}
	// Token: 0x0600092B RID: 2347 RVA: 0x00089F94 File Offset: 0x00088394
	public void perform(int idAction, object p)
	{
		bool flag22 = idAction == 8010;
		if (flag22)
		{
			this.VuDangChatTextField("Nhập Số Sao Cần Đập", "Chỉ Được Nhập Số");
		}
		bool flag21 = idAction == 8009;
		if (flag21)
		{
			Mod.Mod.AddItem(this.currItem);
		}
		if (idAction == 9999)
		{
			TopInfo topInfo = (TopInfo)p;
			Service.gI().sendThachDau(topInfo.pId);
		}
		if (idAction == 170391)
		{
			Rms.clearAll();
			if (mGraphics.zoomLevel > 1)
			{
				Rms.saveRMSInt("levelScreenKN", 1);
			}
			else
			{
				Rms.saveRMSInt("levelScreenKN", 0);
			}
			GameMidlet.instance.exit();
		}
		if (idAction == 6001)
		{
			Item item = (Item)p;
			item.isSelect = false;
			GameCanvas.panel.vItemCombine.removeElement(item);
			if (GameCanvas.panel.currentTabIndex == 0)
			{
				GameCanvas.panel.setTabCombine();
			}
			bool isDapDo = Mod.Mod.isDapDo;
			if (isDapDo)
			{
				Mod.Mod.doDeDap = null;
			}
		}
		if (idAction == 6000)
		{
			Item item2 = (Item)p;
			for (int i = 0; i < GameCanvas.panel.vItemCombine.size(); i++)
			{
				Item item3 = (Item)GameCanvas.panel.vItemCombine.elementAt(i);
				if (item3.template.id == item2.template.id)
				{
					GameCanvas.startOKDlg(mResources.already_has_item);
					return;
				}
			}
			item2.isSelect = true;
			GameCanvas.panel.vItemCombine.addElement(item2);
			if (GameCanvas.panel.currentTabIndex == 0)
			{
				GameCanvas.panel.setTabCombine();
			}
			bool isDapDo2 = Mod.Mod.isDapDo;
			if (isDapDo2)
			{
				Mod.Mod.doDeDap = item2;
			}
		}
		if (idAction == 7000)
		{
			if (this.isLock)
			{
				GameCanvas.startOKDlg(mResources.unlock_item_to_trade);
				return;
			}
			Item item4 = (Item)p;
			for (int j = 0; j < GameCanvas.panel.vMyGD.size(); j++)
			{
				Item item5 = (Item)GameCanvas.panel.vMyGD.elementAt(j);
				if (item5.indexUI == item4.indexUI)
				{
					GameCanvas.startOKDlg(mResources.already_has_item);
					return;
				}
			}
			if (item4.quantity > 1)
			{
				this.putQuantily();
				return;
			}
			item4.isSelect = true;
			Item item6 = new Item();
			item6.template = item4.template;
			item6.itemOption = item4.itemOption;
			item6.indexUI = item4.indexUI;
			GameCanvas.panel.vMyGD.addElement(item6);
			Service.gI().giaodich(2, -1, (sbyte)item6.indexUI, item6.quantity);
		}
		if (idAction == 7001)
		{
			Item item7 = (Item)p;
			item7.isSelect = false;
			GameCanvas.panel.vMyGD.removeElement(item7);
			if (GameCanvas.panel.currentTabIndex == 1)
			{
				GameCanvas.panel.setTabGiaoDich(true);
			}
			Service.gI().giaodich(4, -1, (sbyte)item7.indexUI, -1);
		}
		if (idAction == 7002)
		{
			this.isAccept = true;
			GameCanvas.endDlg();
			Service.gI().giaodich(7, -1, -1, -1);
			this.hide();
		}
		if (idAction == 8003)
		{
			InfoItem infoItem = (InfoItem)p;
			Service.gI().friend(1, infoItem.charInfo.charID);
			if (this.type == 8)
			{
			}
		}
		if (idAction == 8002)
		{
			InfoItem infoItem2 = (InfoItem)p;
			Service.gI().friend(2, infoItem2.charInfo.charID);
		}
		if (idAction == 8004)
		{
			InfoItem infoItem3 = (InfoItem)p;
			Service.gI().gotoPlayer(infoItem3.charInfo.charID);
		}
		if (idAction == 8001)
		{
			Res.outz("chat player");
			InfoItem infoItem4 = (InfoItem)p;
			if (this.chatTField == null)
			{
				this.chatTField = new ChatTextField();
				this.chatTField.tfChat.y = GameCanvas.h - 35 - ChatTextField.gI().tfChat.height;
				this.chatTField.initChatTextField();
				this.chatTField.parentScreen = GameCanvas.panel;
			}
			this.chatTField.strChat = mResources.chat_player;
			this.chatTField.tfChat.name = mResources.chat_with + " " + infoItem4.charInfo.cName;
			this.chatTField.to = string.Empty;
			this.chatTField.isShow = true;
			this.chatTField.tfChat.isFocus = true;
			this.chatTField.tfChat.setIputType(TField.INPUT_TYPE_ANY);
			if (Main.isWindowsPhone)
			{
				this.chatTField.tfChat.strInfo = this.chatTField.strChat;
			}
			if (!Main.isPC)
			{
				this.chatTField.startChat2(this, string.Empty);
			}
		}
		if (idAction == 1000)
		{
			Service.gI().getItem(Panel.BOX_BAG, (sbyte)this.GetInventorySelect_body(this.selected, this.newSelected));
		}
		if (idAction == 1001)
		{
			sbyte id = (sbyte)this.GetInventorySelect_bag(this.selected, this.newSelected, global::Char.myCharz().arrItemBody);
			Service.gI().getItem(Panel.BAG_BOX, id);
		}
		if (idAction == 1003)
		{
			this.hide();
		}
		if (idAction == 1002)
		{
			Service.gI().getItem(Panel.BODY_BOX, (sbyte)this.GetInventorySelect_body(this.selected, this.newSelected));
		}
		if (idAction == 2011)
		{
			Service.gI().useItem(1, 2, (sbyte)this.GetInventorySelect_body(this.selected, this.newSelected), -1);
		}
		if (idAction == 2010)
		{
			Service.gI().useItem(0, 2, (sbyte)this.GetInventorySelect_body(this.selected, this.newSelected), -1);
			Item item8 = (Item)p;
			if (item8 != null && (item8.template.id == 193 || item8.template.id == 194))
			{
				GameCanvas.panel.hide();
			}
		}
		if (idAction == 2000)
		{
			Item[] arrItemBody = global::Char.myCharz().arrItemBody;
			sbyte id2 = (sbyte)this.GetInventorySelect_bag(this.selected, this.newSelected, arrItemBody);
			Service.gI().getItem(Panel.BAG_BODY, id2);
		}
		if (idAction == 2001)
		{
			Res.outz("use item");
			Item item9 = (Item)p;
			bool inventorySelect_isbody = this.GetInventorySelect_isbody(this.selected, this.newSelected, global::Char.myCharz().arrItemBody);
			sbyte index;
			if (!inventorySelect_isbody)
			{
				index = (sbyte)this.GetInventorySelect_bag(this.selected, this.newSelected, global::Char.myCharz().arrItemBody);
			}
			else
			{
				index = (sbyte)this.GetInventorySelect_body(this.selected, this.newSelected);
			}
			Service.gI().useItem(0, (!inventorySelect_isbody) ? (sbyte)1 : (sbyte)0, index, -1);
			if (item9.template.id == 193 || item9.template.id == 194)
			{
				GameCanvas.panel.hide();
			}
		}
		if (idAction == 2002)
		{
			Service.gI().getItem(Panel.BODY_BAG, (sbyte)this.GetInventorySelect_body(this.selected, this.newSelected));
		}
		if (idAction == 2003)
		{
			Res.outz("remove item");
			bool inventorySelect_isbody2 = this.GetInventorySelect_isbody(this.selected, this.newSelected, global::Char.myCharz().arrItemBody);
			sbyte index2;
			if (!inventorySelect_isbody2)
			{
				index2 = (sbyte)this.GetInventorySelect_bag(this.selected, this.newSelected, global::Char.myCharz().arrItemBody);
			}
			else
			{
				index2 = (sbyte)this.GetInventorySelect_body(this.selected, this.newSelected);
			}
			Service.gI().useItem(1, (!inventorySelect_isbody2) ? (sbyte)1 : (sbyte)0, index2, -1);
		}
		if (idAction == 2004)
		{
			GameCanvas.endDlg();
			ItemObject itemObject = (ItemObject)p;
			sbyte where = (sbyte)itemObject.where;
			sbyte index3 = (sbyte)itemObject.id;
			Service.gI().useItem((itemObject.type != 0) ? (sbyte)2 : (sbyte)3, where, index3, -1);
		}
		if (idAction == 2005)
		{
			sbyte id3 = (sbyte)this.GetInventorySelect_bag(this.selected, this.newSelected, global::Char.myCharz().arrItemBody);
			Service.gI().getItem(Panel.BAG_PET, id3);
		}
		if (idAction == 2006)
		{
			Item[] arrItemBody2 = global::Char.myPetz().arrItemBody;
			sbyte id4 = (sbyte)this.selected;
			Service.gI().getItem(Panel.PET_BAG, id4);
		}
		if (idAction == 30001)
		{
			Res.outz("nhan do");
			Service.gI().buyItem(0, this.selected, 0);
		}
		if (idAction == 30002)
		{
			Res.outz("xoa do");
			Service.gI().buyItem(1, this.selected, 0);
		}
		if (idAction == 30003)
		{
			Res.outz("nhan tat");
			Service.gI().buyItem(2, this.selected, 0);
		}
		if (idAction == 3000)
		{
			Res.outz("mua do");
			Item item10 = (Item)p;
			Service.gI().buyItem(0, (int)item10.template.id, 0);
		}
		if (idAction == 3001)
		{
			Item item11 = (Item)p;
			GameCanvas.msgdlg.pleasewait();
			Service.gI().buyItem(1, (int)item11.template.id, 0);
		}
		if (idAction == 3002)
		{
			GameCanvas.endDlg();
			bool inventorySelect_isbody3 = this.GetInventorySelect_isbody(this.selected, this.newSelected, global::Char.myCharz().arrItemBody);
			sbyte b;
			if (!inventorySelect_isbody3)
			{
				b = (sbyte)this.GetInventorySelect_bag(this.selected, this.newSelected, global::Char.myCharz().arrItemBody);
			}
			else
			{
				b = (sbyte)this.GetInventorySelect_body(this.selected, this.newSelected);
			}
			Service.gI().saleItem(0, (!inventorySelect_isbody3) ? (sbyte)1 : (sbyte)0, (short)b);
		}
		if (idAction == 3003)
		{
			GameCanvas.endDlg();
			ItemObject itemObject2 = (ItemObject)p;
			Service.gI().saleItem(1, (sbyte)itemObject2.type, (short)itemObject2.id);
		}
		if (idAction == 3004)
		{
			Item item12 = (Item)p;
			Service.gI().buyItem(3, (int)item12.template.id, 0);
		}
		if (idAction == 3005)
		{
			Res.outz("mua do");
			Item item13 = (Item)p;
			Service.gI().buyItem(3, (int)item13.template.id, 0);
		}
		if (idAction == 4000)
		{
			Clan clan = (Clan)p;
			if (clan != null)
			{
				GameCanvas.endDlg();
				Service.gI().clanMessage(2, null, clan.ID);
			}
		}
		if (idAction == 4001)
		{
			Clan clan2 = (Clan)p;
			if (clan2 != null)
			{
				InfoDlg.showWait();
				this.clanReport = mResources.PLEASEWAIT;
				Service.gI().clanMember(clan2.ID);
			}
		}
		if (idAction == 4005)
		{
			GameCanvas.endDlg();
		}
		if (idAction == 4007)
		{
			GameCanvas.endDlg();
		}
		if (idAction == 4006)
		{
			ClanMessage clanMessage = (ClanMessage)p;
			Service.gI().clanDonate(clanMessage.id);
		}
		if (idAction == 5001)
		{
			Member member = (Member)p;
			Service.gI().clanRemote(member.ID, 0);
		}
		if (idAction == 5002)
		{
			Member member2 = (Member)p;
			Service.gI().clanRemote(member2.ID, 1);
		}
		if (idAction == 5003)
		{
			Member member3 = (Member)p;
			Service.gI().clanRemote(member3.ID, 2);
		}
		if (idAction == 5004)
		{
			Member member4 = (Member)p;
			Service.gI().clanRemote(member4.ID, -1);
		}
		if (idAction == 9000)
		{
			Service.gI().upPotential(this.selected, 1);
			GameCanvas.endDlg();
			InfoDlg.showWait();
		}
		if (idAction == 9006)
		{
			Service.gI().upPotential(this.selected, 10);
			GameCanvas.endDlg();
			InfoDlg.showWait();
		}
		if (idAction == 9007)
		{
			Service.gI().upPotential(this.selected, 100);
			GameCanvas.endDlg();
			InfoDlg.showWait();
		}
		if (idAction == 9002)
		{
			Skill skill = (Skill)p;
			if (skill.template.isSkillSpec())
			{
				GameCanvas.startOKDlg(mResources.updSkill);
			}
			else
			{
				GameCanvas.startOKDlg(string.Concat(new object[]
				{
					mResources.can_buy_from_Uron1,
					skill.powRequire,
					mResources.can_buy_from_Uron2,
					skill.moreInfo,
					mResources.can_buy_from_Uron3
				}));
			}
		}
		if (idAction == 9003)
		{
			if (GameCanvas.isTouch && !Main.isPC)
			{
				GameScr.gI().doSetOnScreenSkill((SkillTemplate)p);
			}
			else
			{
				GameScr.gI().doSetKeySkill((SkillTemplate)p);
			}
		}
		if (idAction == 9004)
		{
			Skill skill2 = (Skill)p;
			if (skill2.template.isSkillSpec())
			{
				GameCanvas.startOKDlg(mResources.learnSkill);
			}
			else
			{
				GameCanvas.startOKDlg(string.Concat(new object[]
				{
					mResources.can_buy_from_Uron1,
					skill2.powRequire,
					mResources.can_buy_from_Uron2,
					skill2.moreInfo,
					mResources.can_buy_from_Uron3
				}));
			}
		}
		if (idAction == 10000)
		{
			InfoItem infoItem5 = (InfoItem)p;
			Service.gI().enemy(1, infoItem5.charInfo.charID);
			GameCanvas.panel.hideNow();
		}
		if (idAction == 10001)
		{
			InfoItem infoItem6 = (InfoItem)p;
			Service.gI().enemy(2, infoItem6.charInfo.charID);
			InfoDlg.showWait();
		}
		if (idAction == 10021)
		{
		}
		if (idAction == 10012)
		{
			if (this.chatTField == null)
			{
				this.chatTField = new ChatTextField();
				this.chatTField.tfChat.y = GameCanvas.h - 35 - ChatTextField.gI().tfChat.height;
				this.chatTField.initChatTextField();
				this.chatTField.parentScreen = ((GameCanvas.panel2 != null) ? GameCanvas.panel2 : GameCanvas.panel);
			}
			if (this.currItem.quantity == 1)
			{
				this.chatTField.strChat = mResources.kiguiXuchat;
				this.chatTField.tfChat.name = mResources.input_money;
			}
			else
			{
				this.chatTField.strChat = mResources.input_quantity + " ";
				this.chatTField.tfChat.name = mResources.input_quantity;
			}
			this.chatTField.tfChat.setMaxTextLenght(9);
			this.chatTField.to = string.Empty;
			this.chatTField.isShow = true;
			this.chatTField.tfChat.setIputType(TField.INPUT_TYPE_NUMERIC);
			if (GameCanvas.isTouch)
			{
				this.chatTField.tfChat.doChangeToTextBox();
			}
			if (Main.isWindowsPhone)
			{
				this.chatTField.tfChat.strInfo = this.chatTField.strChat;
			}
			if (!Main.isPC)
			{
				this.chatTField.startChat2(this, string.Empty);
			}
		}
		if (idAction == 10013)
		{
			if (this.chatTField == null)
			{
				this.chatTField = new ChatTextField();
				this.chatTField.tfChat.y = GameCanvas.h - 35 - ChatTextField.gI().tfChat.height;
				this.chatTField.initChatTextField();
				this.chatTField.parentScreen = ((GameCanvas.panel2 != null) ? GameCanvas.panel2 : GameCanvas.panel);
			}
			if (this.currItem.quantity == 1)
			{
				this.chatTField.strChat = mResources.kiguiLuongchat;
				this.chatTField.tfChat.name = mResources.input_money;
			}
			else
			{
				this.chatTField.strChat = mResources.input_quantity + "  ";
				this.chatTField.tfChat.name = mResources.input_quantity;
			}
			this.chatTField.to = string.Empty;
			this.chatTField.isShow = true;
			this.chatTField.tfChat.setIputType(TField.INPUT_TYPE_NUMERIC);
			if (GameCanvas.isTouch)
			{
				this.chatTField.tfChat.doChangeToTextBox();
			}
			if (Main.isWindowsPhone)
			{
				this.chatTField.tfChat.strInfo = this.chatTField.strChat;
			}
			if (!Main.isPC)
			{
				this.chatTField.startChat2(this, string.Empty);
			}
		}
		if (idAction == 10014)
		{
			Item item14 = (Item)p;
			Service.gI().kigui(1, item14.itemId, -1, -1, -1);
			InfoDlg.showWait();
		}
		if (idAction == 10015)
		{
			Item item15 = (Item)p;
			Service.gI().kigui(2, item15.itemId, -1, -1, -1);
			InfoDlg.showWait();
		}
		if (idAction == 10016)
		{
			Item item16 = (Item)p;
			Service.gI().kigui(3, item16.itemId, 0, item16.buyCoin, -1);
			InfoDlg.showWait();
		}
		if (idAction == 10017)
		{
			Item item17 = (Item)p;
			Service.gI().kigui(3, item17.itemId, 1, item17.buyGold, -1);
			InfoDlg.showWait();
		}
		if (idAction == 10018)
		{
			Item item18 = (Item)p;
			Service.gI().kigui(5, item18.itemId, -1, -1, -1);
			InfoDlg.showWait();
		}
		if (idAction == 10019)
		{
			Session_ME.gI().close();
			Rms.saveRMSString("acc", string.Empty);
			Rms.saveRMSString("pass", string.Empty);
			GameCanvas.loginScr.tfPass.setText(string.Empty);
			GameCanvas.loginScr.tfUser.setText(string.Empty);
			GameCanvas.loginScr.isLogin2 = false;
			GameCanvas.loginScr.switchToMe();
			GameCanvas.endDlg();
			this.hide();
		}
		if (idAction == 10020)
		{
			GameCanvas.endDlg();
		}
		if (idAction == 10030)
		{
			Service.gI().getFlag(1, (sbyte)this.selected);
			GameCanvas.panel.hideNow();
		}
		if (idAction == 10031)
		{
			Session_ME.gI().close();
		}
		if (idAction == 11000)
		{
			Service.gI().kigui(0, this.currItem.itemId, 1, this.currItem.buyRuby, 1);
			GameCanvas.endDlg();
		}
		if (idAction == 11001)
		{
			Service.gI().kigui(0, this.currItem.itemId, 1, this.currItem.buyRuby, this.currItem.quantilyToBuy);
			GameCanvas.endDlg();
		}
		if (idAction == 11002)
		{
			this.chatTField.isShow = false;
			GameCanvas.endDlg();
		}
	}

	// Token: 0x0600092C RID: 2348 RVA: 0x0008B270 File Offset: 0x00089670
	public void onChatFromMe(string text, string to)
	{
		SpecialSkill.gI().onChatFromMe(text, to);
		bool flag29 = this.chatTField.strChat == "Nhập Số Sao Cần Đập";
		if (flag29)
		{
			Mod.Mod.soSaoCanDap = int.Parse(text);
			GameScr.info1.addInfo(string.Concat(new object[]
			{
				"Đập ",
			Mod.Mod.doDeDap.template.name,
				" Đến ",
				Mod.Mod.soSaoCanDap,
				" Sao"
			}), 0);
			this.chatTField.isShow = false;
		}
		if (this.chatTField.tfChat.getText() == null || this.chatTField.tfChat.getText().Equals(string.Empty) || text.Equals(string.Empty) || text == null)
		{
			this.chatTField.isShow = false;
			return;
		}
		if (this.chatTField.strChat.Equals(mResources.input_clan_name))
		{
			InfoDlg.showWait();
			this.chatTField.isShow = false;
			Service.gI().searchClan(text);
		}
		else if (this.chatTField.strChat.Equals(mResources.chat_clan))
		{
			InfoDlg.showWait();
			this.chatTField.isShow = false;
			Service.gI().clanMessage(0, text, -1);
		}
		else if (this.chatTField.strChat.Equals(mResources.input_clan_name_to_create))
		{
			if (this.chatTField.tfChat.getText() == string.Empty)
			{
				GameScr.info1.addInfo(mResources.clan_name_blank, 0);
			}
			else
			{
				if (this.tabIcon == null)
				{
					this.tabIcon = new TabClanIcon();
				}
				this.tabIcon.text = this.chatTField.tfChat.getText();
				this.tabIcon.show(false);
				this.chatTField.isShow = false;
			}
		}
		else if (this.chatTField.strChat.Equals(mResources.input_clan_slogan))
		{
			if (this.chatTField.tfChat.getText() == string.Empty)
			{
				GameScr.info1.addInfo(mResources.clan_slogan_blank, 0);
			}
			else
			{
				Service.gI().getClan(4, (sbyte)global::Char.myCharz().clan.imgID, this.chatTField.tfChat.getText());
				this.chatTField.isShow = false;
			}
		}
		else if (this.chatTField.strChat.Equals(mResources.input_Inventory_Pass))
		{
			try
			{
				int lockInventory = int.Parse(this.chatTField.tfChat.getText());
				if (this.chatTField.tfChat.getText().Length != 6 || this.chatTField.tfChat.getText().Equals(string.Empty))
				{
					GameCanvas.startOKDlg(mResources.input_Inventory_Pass_wrong);
				}
				else
				{
					Service.gI().setLockInventory(lockInventory);
					this.chatTField.isShow = false;
					this.chatTField.tfChat.setIputType(TField.INPUT_TYPE_ANY);
					this.hide();
				}
			}
			catch (Exception ex)
			{
				GameCanvas.startOKDlg(mResources.ALERT_PRIVATE_PASS_2);
			}
		}
		else if (this.chatTField.strChat.Equals(mResources.world_channel_5_luong))
		{
			if (this.chatTField.tfChat.getText().Equals(string.Empty))
			{
				return;
			}
			Service.gI().chatGlobal(this.chatTField.tfChat.getText());
			this.chatTField.isShow = false;
			this.hide();
		}
		else if (this.chatTField.strChat.Equals(mResources.chat_player))
		{
			this.chatTField.isShow = false;
			InfoItem infoItem = null;
			if (this.type == 8)
			{
				infoItem = (InfoItem)this.logChat.elementAt(this.currInfoItem);
			}
			else if (this.type == 11)
			{
				infoItem = (InfoItem)this.vFriend.elementAt(this.currInfoItem);
			}
			if (infoItem.charInfo.charID == global::Char.myCharz().charID)
			{
				return;
			}
			Service.gI().chatPlayer(text, infoItem.charInfo.charID);
		}
		else if (this.chatTField.strChat.Equals(mResources.input_quantity_to_trade))
		{
			int num = 0;
			try
			{
				num = int.Parse(this.chatTField.tfChat.getText());
			}
			catch (Exception ex2)
			{
				GameCanvas.startOKDlg(mResources.input_quantity_wrong);
				this.chatTField.isShow = false;
				this.chatTField.tfChat.setIputType(TField.INPUT_TYPE_ANY);
				return;
			}
			if (num <= 0 || num > this.currItem.quantity)
			{
				GameCanvas.startOKDlg(mResources.input_quantity_wrong);
				this.chatTField.isShow = false;
				this.chatTField.tfChat.setIputType(TField.INPUT_TYPE_ANY);
				return;
			}
			this.currItem.isSelect = true;
			Item item = new Item();
			item.template = this.currItem.template;
			item.quantity = num;
			item.indexUI = this.currItem.indexUI;
			item.itemOption = this.currItem.itemOption;
			GameCanvas.panel.vMyGD.addElement(item);
			Service.gI().giaodich(2, -1, (sbyte)item.indexUI, item.quantity);
			this.chatTField.isShow = false;
			this.chatTField.tfChat.setIputType(TField.INPUT_TYPE_ANY);
		}
		else if (this.chatTField.strChat == mResources.input_money_to_trade)
		{
			int num2 = 0;
			try
			{
				num2 = int.Parse(this.chatTField.tfChat.getText());
			}
			catch (Exception ex3)
			{
				GameCanvas.startOKDlg(mResources.input_money_wrong);
				this.chatTField.isShow = false;
				this.chatTField.tfChat.setIputType(TField.INPUT_TYPE_ANY);
				return;
			}
			if ((long)num2 > global::Char.myCharz().xu)
			{
				GameCanvas.startOKDlg(mResources.not_enough_money);
				this.chatTField.isShow = false;
				this.chatTField.tfChat.setIputType(TField.INPUT_TYPE_ANY);
				return;
			}
			this.moneyGD = num2;
			Service.gI().giaodich(2, -1, -1, num2);
			this.chatTField.isShow = false;
			this.chatTField.tfChat.setIputType(TField.INPUT_TYPE_ANY);
		}
		else if (this.chatTField.strChat.Equals(mResources.kiguiXuchat))
		{
			Service.gI().kigui(0, this.currItem.itemId, 0, int.Parse(this.chatTField.tfChat.getText()), 1);
			this.chatTField.isShow = false;
		}
		else if (this.chatTField.strChat.Equals(mResources.kiguiXuchat + " "))
		{
			Service.gI().kigui(0, this.currItem.itemId, 0, int.Parse(this.chatTField.tfChat.getText()), this.currItem.quantilyToBuy);
			this.chatTField.isShow = false;
		}
		else if (this.chatTField.strChat.Equals(mResources.kiguiLuongchat))
		{
			this.doNotiRuby(0);
			this.chatTField.isShow = false;
		}
		else if (this.chatTField.strChat.Equals(mResources.kiguiLuongchat + "  "))
		{
			this.doNotiRuby(1);
			this.chatTField.isShow = false;
		}
		else if (this.chatTField.strChat.Equals(mResources.input_quantity + " "))
		{
			this.currItem.quantilyToBuy = int.Parse(this.chatTField.tfChat.getText());
			if (this.currItem.quantilyToBuy > this.currItem.quantity)
			{
				GameCanvas.startOKDlg(mResources.input_quantity_wrong);
				return;
			}
			this.isKiguiXu = true;
			this.chatTField.isShow = false;
		}
		else if (this.chatTField.strChat.Equals(mResources.input_quantity + "  "))
		{
			this.currItem.quantilyToBuy = int.Parse(this.chatTField.tfChat.getText());
			if (this.currItem.quantilyToBuy > this.currItem.quantity)
			{
				GameCanvas.startOKDlg(mResources.input_quantity_wrong);
				return;
			}
			this.isKiguiLuong = true;
			this.chatTField.isShow = false;
		}
	}

	// Token: 0x0600092D RID: 2349 RVA: 0x0008BADC File Offset: 0x00089EDC
	public void onCancelChat()
	{
		this.chatTField.tfChat.setIputType(TField.INPUT_TYPE_ANY);
	}

	// Token: 0x0600092E RID: 2350 RVA: 0x0008BAF4 File Offset: 0x00089EF4
	public void setCombineEff(int type)
	{
		this.typeCombine = type;
		this.rS = 90;
		if (this.typeCombine == 0)
		{
			this.iDotS = 5;
			this.angleS = (this.angleO = 90);
			this.time = 2;
			for (int i = 0; i < this.vItemCombine.size(); i++)
			{
				Item item = (Item)this.vItemCombine.elementAt(i);
				if (item != null)
				{
					if ((int)item.template.type == 14)
					{
						this.iconID2 = item.template.iconID;
					}
					else
					{
						this.iconID1 = item.template.iconID;
					}
				}
			}
		}
		else if (this.typeCombine == 1)
		{
			this.iDotS = 2;
			this.angleS = (this.angleO = 0);
			this.time = 1;
			for (int j = 0; j < this.vItemCombine.size(); j++)
			{
				Item item2 = (Item)this.vItemCombine.elementAt(j);
				if (item2 != null)
				{
					if (j == 0)
					{
						this.iconID1 = item2.template.iconID;
					}
					else
					{
						this.iconID2 = item2.template.iconID;
					}
				}
			}
		}
		else if (this.typeCombine == 2)
		{
			this.iDotS = 7;
			this.angleS = (this.angleO = 25);
			this.time = 1;
			for (int k = 0; k < this.vItemCombine.size(); k++)
			{
				Item item3 = (Item)this.vItemCombine.elementAt(k);
				if (item3 != null)
				{
					this.iconID1 = item3.template.iconID;
				}
			}
		}
		else if (this.typeCombine == 3)
		{
			this.xS = GameCanvas.hw;
			this.yS = GameCanvas.hh;
			this.iDotS = 1;
			this.angleS = (this.angleO = 1);
			this.time = 4;
			for (int l = 0; l < this.vItemCombine.size(); l++)
			{
				Item item4 = (Item)this.vItemCombine.elementAt(l);
				if (item4 != null)
				{
					this.iconID1 = item4.template.iconID;
				}
			}
		}
		else if (this.typeCombine == 4)
		{
			this.iDotS = this.vItemCombine.size();
			this.iconID = new short[this.iDotS];
			this.angleS = (this.angleO = 25);
			this.time = 1;
			for (int m = 0; m < this.vItemCombine.size(); m++)
			{
				Item item5 = (Item)this.vItemCombine.elementAt(m);
				if (item5 != null)
				{
					this.iconID[m] = item5.template.iconID;
				}
			}
		}
		this.speed = 1;
		this.isSpeedCombine = true;
		this.isDoneCombine = false;
		this.isCompleteEffCombine = false;
		this.iAngleS = 360 / this.iDotS;
		this.xArgS = new int[this.iDotS];
		this.yArgS = new int[this.iDotS];
		this.xDotS = new int[this.iDotS];
		this.yDotS = new int[this.iDotS];
		this.setDotStar();
		this.isPaintCombine = true;
		this.countUpdate = 10;
		this.countR = 30;
		this.countWait = 10;
		this.addTextCombineNPC(this.idNPC, mResources.combineSpell);
	}

	// Token: 0x0600092F RID: 2351 RVA: 0x0008BE90 File Offset: 0x0008A290
	private void updateCombineEff()
	{
		this.countUpdate--;
		if (this.countUpdate < 0)
		{
			this.countUpdate = 0;
		}
		this.countR--;
		if (this.countR < 0)
		{
			this.countR = 0;
		}
		if (this.countUpdate == 0)
		{
			if (!this.isCompleteEffCombine)
			{
				if (this.time > 0)
				{
					if ((int)this.combineSuccess != -1)
					{
						if (this.typeCombine == 3)
						{
							if (GameCanvas.gameTick % 10 == 0)
							{
								Effect me = new Effect(21, this.xS - 10, this.yS + 25, 4, 1, 1);
								EffecMn.addEff(me);
								this.time--;
							}
						}
						else
						{
							if (GameCanvas.gameTick % 2 == 0)
							{
								if (this.isSpeedCombine)
								{
									if (this.speed < 40)
									{
										this.speed += 2;
									}
								}
								else if (this.speed > 10)
								{
									this.speed -= 2;
								}
							}
							if (this.countR == 0)
							{
								if (this.isSpeedCombine)
								{
									if (this.rS > 0)
									{
										this.rS -= 5;
									}
									else if (GameCanvas.gameTick % 10 == 0)
									{
										this.isSpeedCombine = false;
										this.time--;
										this.countR = 5;
										this.countWait = 10;
									}
								}
								else if (this.rS < 90)
								{
									this.rS += 5;
								}
								else if (GameCanvas.gameTick % 10 == 0)
								{
									this.isSpeedCombine = true;
									this.countR = 10;
								}
							}
							this.angleS = this.angleO;
							this.angleS -= this.speed;
							if (this.angleS >= 360)
							{
								this.angleS -= 360;
							}
							if (this.angleS < 0)
							{
								this.angleS = 360 + this.angleS;
							}
							this.angleO = this.angleS;
							this.setDotStar();
						}
					}
				}
				else if (GameCanvas.gameTick % 20 == 0)
				{
					this.isCompleteEffCombine = true;
				}
				if (GameCanvas.gameTick % 20 == 0)
				{
					if (this.typeCombine != 3)
					{
						EffectPanel.addServerEffect(132, this.xS, this.yS, 2);
					}
					EffectPanel.addServerEffect(114, this.xS, this.yS + 20, 2);
				}
			}
			else if (this.isCompleteEffCombine)
			{
				if ((int)this.combineSuccess == 1)
				{
					if (this.countWait == 10)
					{
						Effect me2 = new Effect(22, this.xS - 3, this.yS + 25, 4, 1, 1);
						EffecMn.addEff(me2);
					}
					this.countWait--;
					if (this.countWait < 0)
					{
						this.countWait = 0;
					}
					if (this.rS < 300)
					{
						this.rS = Res.abs(this.rS + 10);
						if (this.rS == 20)
						{
							this.addTextCombineNPC(this.idNPC, mResources.combineFail);
						}
					}
					else if (GameCanvas.gameTick % 20 == 0)
					{
						if (GameCanvas.w > 2 * Panel.WIDTH_PANEL)
						{
							GameCanvas.panel2 = new Panel();
							GameCanvas.panel2.tabName[7] = new string[][]
							{
								new string[]
								{
									string.Empty
								}
							};
							GameCanvas.panel2.setTypeBodyOnly();
							GameCanvas.panel2.show();
						}
						this.combineSuccess = -1;
						this.isDoneCombine = true;
						if (this.typeCombine == 4)
						{
							GameCanvas.panel.hideNow();
						}
					}
					this.setDotStar();
				}
				else if ((int)this.combineSuccess == 0)
				{
					if (this.countWait == 10)
					{
						if (this.typeCombine == 2)
						{
							Effect me3 = new Effect(20, this.xS - 3, this.yS + 15, 4, 2, 1);
							EffecMn.addEff(me3);
						}
						else
						{
							Effect me4 = new Effect(21, this.xS - 10, this.yS + 25, 4, 1, 1);
							EffecMn.addEff(me4);
						}
						this.addTextCombineNPC(this.idNPC, mResources.combineSuccess);
						this.isPaintCombine = false;
					}
					if (!this.isPaintCombine)
					{
						this.countWait--;
						if (this.countWait < -50)
						{
							this.countWait = -50;
							if (this.typeCombine < 3 && GameCanvas.w > 2 * Panel.WIDTH_PANEL)
							{
								GameCanvas.panel2 = new Panel();
								GameCanvas.panel2.tabName[7] = new string[][]
								{
									new string[]
									{
										string.Empty
									}
								};
								GameCanvas.panel2.setTypeBodyOnly();
								GameCanvas.panel2.show();
							}
							this.combineSuccess = -1;
							this.isDoneCombine = true;
							if (this.typeCombine == 4)
							{
								GameCanvas.panel.hideNow();
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x06000930 RID: 2352 RVA: 0x0008C3A0 File Offset: 0x0008A7A0
	public void paintCombineEff(mGraphics g)
	{
		GameScr.gI().paintBlackSky(g);
		this.paintCombineNPC(g);
		if (this.typeCombine == 0)
		{
			for (int i = 0; i < this.yArgS.Length; i++)
			{
				SmallImage.drawSmallImage(g, (int)this.iconID1, this.xS, this.yS, 0, mGraphics.VCENTER | mGraphics.HCENTER);
				if (this.isPaintCombine)
				{
					SmallImage.drawSmallImage(g, (int)this.iconID2, this.xDotS[i], this.yDotS[i], 0, mGraphics.VCENTER | mGraphics.HCENTER);
				}
			}
		}
		else if (this.typeCombine == 1)
		{
			if (!this.isPaintCombine)
			{
				SmallImage.drawSmallImage(g, (int)this.iconID3, this.xS, this.yS, 0, mGraphics.VCENTER | mGraphics.HCENTER);
			}
			else
			{
				for (int j = 0; j < this.yArgS.Length; j++)
				{
					SmallImage.drawSmallImage(g, (int)this.iconID1, this.xDotS[0], this.yDotS[0], 0, mGraphics.VCENTER | mGraphics.HCENTER);
					SmallImage.drawSmallImage(g, (int)this.iconID2, this.xDotS[1], this.yDotS[1], 0, mGraphics.VCENTER | mGraphics.HCENTER);
				}
			}
		}
		else if (this.typeCombine == 2)
		{
			if (!this.isPaintCombine)
			{
				SmallImage.drawSmallImage(g, (int)this.iconID3, this.xS, this.yS, 0, mGraphics.VCENTER | mGraphics.HCENTER);
			}
			else
			{
				for (int k = 0; k < this.yArgS.Length; k++)
				{
					SmallImage.drawSmallImage(g, (int)this.iconID1, this.xDotS[k], this.yDotS[k], 0, mGraphics.VCENTER | mGraphics.HCENTER);
				}
			}
		}
		else if (this.typeCombine == 3)
		{
			if (!this.isPaintCombine)
			{
				SmallImage.drawSmallImage(g, (int)this.iconID3, this.xS, this.yS, 0, mGraphics.VCENTER | mGraphics.HCENTER);
			}
			else
			{
				SmallImage.drawSmallImage(g, (int)this.iconID1, this.xS, this.yS, 0, mGraphics.VCENTER | mGraphics.HCENTER);
			}
		}
		else if (this.typeCombine == 4)
		{
			if (!this.isPaintCombine)
			{
				if (this.iconID3 != -1)
				{
					SmallImage.drawSmallImage(g, (int)this.iconID3, this.xS, this.yS, 0, mGraphics.VCENTER | mGraphics.HCENTER);
				}
			}
			else
			{
				for (int l = 0; l < this.iconID.Length; l++)
				{
					SmallImage.drawSmallImage(g, (int)this.iconID[l], this.xDotS[l], this.yDotS[l], 0, mGraphics.VCENTER | mGraphics.HCENTER);
				}
			}
		}
	}

	// Token: 0x06000931 RID: 2353 RVA: 0x0008C66C File Offset: 0x0008AA6C
	private void setDotStar()
	{
		for (int i = 0; i < this.yArgS.Length; i++)
		{
			if (this.angleS >= 360)
			{
				this.angleS -= 360;
			}
			if (this.angleS < 0)
			{
				this.angleS = 360 + this.angleS;
			}
			this.yArgS[i] = Res.abs(this.rS * Res.sin(this.angleS) / 1024);
			this.xArgS[i] = Res.abs(this.rS * Res.cos(this.angleS) / 1024);
			if (this.angleS < 90)
			{
				this.xDotS[i] = this.xS + this.xArgS[i];
				this.yDotS[i] = this.yS - this.yArgS[i];
			}
			else if (this.angleS >= 90 && this.angleS < 180)
			{
				this.xDotS[i] = this.xS - this.xArgS[i];
				this.yDotS[i] = this.yS - this.yArgS[i];
			}
			else if (this.angleS >= 180 && this.angleS < 270)
			{
				this.xDotS[i] = this.xS - this.xArgS[i];
				this.yDotS[i] = this.yS + this.yArgS[i];
			}
			else
			{
				this.xDotS[i] = this.xS + this.xArgS[i];
				this.yDotS[i] = this.yS + this.yArgS[i];
			}
			this.angleS -= this.iAngleS;
		}
	}

	// Token: 0x06000932 RID: 2354 RVA: 0x0008C840 File Offset: 0x0008AC40
	public void paintCombineNPC(mGraphics g)
	{
		g.translate(-GameScr.cmx, -GameScr.cmy);
		if (this.typeCombine < 3)
		{
			for (int i = 0; i < GameScr.vNpc.size(); i++)
			{
				Npc npc = (Npc)GameScr.vNpc.elementAt(i);
				if (npc.template.npcTemplateId == this.idNPC)
				{
					npc.paint(g);
					if (npc.chatInfo != null)
					{
						npc.chatInfo.paint(g, npc.cx, npc.cy - npc.ch - GameCanvas.transY, npc.cdir);
					}
				}
			}
		}
		GameCanvas.resetTrans(g);
		if (GameCanvas.gameTick % 4 == 0)
		{
			g.drawImage(ItemMap.imageFlare, this.xS - 5, this.yS + 15, mGraphics.BOTTOM | mGraphics.HCENTER);
			g.drawImage(ItemMap.imageFlare, this.xS + 5, this.yS + 15, mGraphics.BOTTOM | mGraphics.HCENTER);
			g.drawImage(ItemMap.imageFlare, this.xS, this.yS + 15, mGraphics.BOTTOM | mGraphics.HCENTER);
		}
		for (int j = 0; j < Effect2.vEffect3.size(); j++)
		{
			Effect2 effect = (Effect2)Effect2.vEffect3.elementAt(j);
			effect.paint(g);
		}
	}

	// Token: 0x06000933 RID: 2355 RVA: 0x0008C9A4 File Offset: 0x0008ADA4
	public void addTextCombineNPC(int idNPC, string text)
	{
		if (this.typeCombine < 3)
		{
			for (int i = 0; i < GameScr.vNpc.size(); i++)
			{
				Npc npc = (Npc)GameScr.vNpc.elementAt(i);
				if (npc.template.npcTemplateId == idNPC)
				{
					npc.addInfo(text);
				}
			}
		}
	}

	// Token: 0x06000934 RID: 2356 RVA: 0x0008CA04 File Offset: 0x0008AE04
	public void setTypeOption()
	{
		this.type = 19;
		this.setType(0);
		this.setTabOption();
		this.cmx = (this.cmtoX = 0);
	}

	// Token: 0x06000935 RID: 2357 RVA: 0x0008CA38 File Offset: 0x0008AE38
	private void setTabOption()
	{
		SoundMn.gI().getStrOption();
		this.currentListLength = Panel.strCauhinh.Length;
		this.ITEM_HEIGHT = 24;
		this.selected = ((!GameCanvas.isTouch) ? 0 : -1);
		this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
		if (this.cmyLim < 0)
		{
			this.cmyLim = 0;
		}
		this.cmy = (this.cmtoY = this.cmyLast[this.currentTabIndex]);
		if (this.cmy < 0)
		{
			this.cmy = (this.cmtoY = 0);
		}
		if (this.cmy > this.cmyLim)
		{
			this.cmy = (this.cmtoY = this.cmyLim);
		}
	}

	// Token: 0x06000936 RID: 2358 RVA: 0x0008CB08 File Offset: 0x0008AF08
	private void paintOption(mGraphics g)
	{
		g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
		g.translate(0, -this.cmy);
		for (int i = 0; i < Panel.strCauhinh.Length; i++)
		{
			int x = this.xScroll;
			int num = this.yScroll + i * this.ITEM_HEIGHT;
			int num2 = this.wScroll - 1;
			int h = this.ITEM_HEIGHT - 1;
			if (num - this.cmy <= this.yScroll + this.hScroll)
			{
				if (num - this.cmy >= this.yScroll - this.ITEM_HEIGHT)
				{
					g.setColor((i != this.selected) ? 15196114 : 16383818);
					g.fillRect(x, num, num2, h);
					mFont.tahoma_7b_dark.drawString(g, Panel.strCauhinh[i], this.xScroll + 10, num + 6, mFont.LEFT);
				}
			}
		}
		this.paintScrollArrow(g);
	}

	// Token: 0x06000937 RID: 2359 RVA: 0x0008CC18 File Offset: 0x0008B018
	private void doFireOption()
	{
		if (this.selected < 0)
		{
			return;
		}
		switch (this.selected)
		{
			case 0:
				SoundMn.gI().AuraToolOption();
				break;
			case 1:
				SoundMn.gI().AuraToolOption2();
				break;
			case 2:
				SoundMn.gI().soundToolOption();
				break;
			case 3:
				if (Main.isPC)
				{
					GameCanvas.startYesNoDlg(mResources.changeSizeScreen, new Command(mResources.YES, this, 170391, null), new Command(mResources.NO, this, 4005, null));
				}
				else
				{
					SoundMn.gI().CaseSizeScr();
				}
				break;
			case 4:
				if (Main.isPC)
				{
					GameCanvas.startYesNoDlg(mResources.changeSizeScreen, new Command(mResources.YES, this, 170391, null), new Command(mResources.NO, this, 4005, null));
				}
				else
				{
					SoundMn.gI().CaseSizeScr();
				}
				break;
			case 5:
				SoundMn.gI().CaseAnalog();
				break;
		}
	}

	// Token: 0x06000938 RID: 2360 RVA: 0x0008CD34 File Offset: 0x0008B134
	public void setTypeAccount()
	{
		this.type = 20;
		this.setType(0);
		this.setTabAccount();
		this.cmx = (this.cmtoX = 0);
	}

	// Token: 0x06000939 RID: 2361 RVA: 0x0008CD68 File Offset: 0x0008B168
	private void setTabAccount()
	{
		if (Main.IphoneVersionApp)
		{
			Panel.strAccount = new string[]
			{
				mResources.inventory_Pass,
				mResources.friend,
				mResources.enemy,
				mResources.msg
			};
			if (GameScr.canAutoPlay)
			{
				Panel.strAccount = new string[]
				{
					mResources.inventory_Pass,
					mResources.friend,
					mResources.enemy,
					mResources.msg,
					mResources.autoFunction
				};
			}
		}
		else
		{
			Panel.strAccount = new string[]
			{
				mResources.inventory_Pass,
				mResources.friend,
				mResources.enemy,
				mResources.msg,
				mResources.charger
			};
			if (GameScr.canAutoPlay)
			{
				Panel.strAccount = new string[]
				{
					mResources.inventory_Pass,
					mResources.friend,
					mResources.enemy,
					mResources.msg,
					mResources.charger,
					mResources.autoFunction
				};
			}
			if ((mSystem.clientType == 2 || mSystem.clientType == 7) && (int)mResources.language != 2)
			{
				Panel.strAccount = new string[]
				{
					mResources.inventory_Pass,
					mResources.friend,
					mResources.enemy,
					mResources.msg,
					mResources.charger
				};
				if (GameScr.canAutoPlay)
				{
					Panel.strAccount = new string[]
					{
						mResources.inventory_Pass,
						mResources.friend,
						mResources.enemy,
						mResources.msg,
						mResources.charger,
						mResources.autoFunction
					};
				}
			}
		}
		this.currentListLength = Panel.strAccount.Length;
		this.ITEM_HEIGHT = 24;
		this.selected = ((!GameCanvas.isTouch) ? 0 : -1);
		this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
		if (this.cmyLim < 0)
		{
			this.cmyLim = 0;
		}
		this.cmy = (this.cmtoY = this.cmyLast[this.currentTabIndex]);
		if (this.cmy < 0)
		{
			this.cmy = (this.cmtoY = 0);
		}
		if (this.cmy > this.cmyLim)
		{
			this.cmy = (this.cmtoY = this.cmyLim);
		}
	}

	// Token: 0x0600093A RID: 2362 RVA: 0x0008CFB8 File Offset: 0x0008B3B8
	private void paintAccount(mGraphics g)
	{
		g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
		g.translate(0, -this.cmy);
		for (int i = 0; i < Panel.strAccount.Length; i++)
		{
			int x = this.xScroll;
			int num = this.yScroll + i * this.ITEM_HEIGHT;
			int num2 = this.wScroll - 1;
			int h = this.ITEM_HEIGHT - 1;
			if (num - this.cmy <= this.yScroll + this.hScroll)
			{
				if (num - this.cmy >= this.yScroll - this.ITEM_HEIGHT)
				{
					g.setColor((i != this.selected) ? 15196114 : 16383818);
					g.fillRect(x, num, num2, h);
					mFont.tahoma_7b_dark.drawString(g, Panel.strAccount[i], this.xScroll + this.wScroll / 2, num + 6, mFont.CENTER);
				}
			}
		}
		this.paintScrollArrow(g);
	}

	// Token: 0x0600093B RID: 2363 RVA: 0x0008D0D0 File Offset: 0x0008B4D0
	private void doFireAccount()
	{
		if (this.selected < 0)
		{
			return;
		}
		switch (this.selected)
		{
			case 0:
				GameCanvas.endDlg();
				if (this.chatTField == null)
				{
					this.chatTField = new ChatTextField();
					this.chatTField.tfChat.y = GameCanvas.h - 35 - ChatTextField.gI().tfChat.height;
					this.chatTField.initChatTextField();
					this.chatTField.parentScreen = GameCanvas.panel;
				}
				this.chatTField.tfChat.setText(string.Empty);
				this.chatTField.strChat = mResources.input_Inventory_Pass;
				this.chatTField.tfChat.name = mResources.input_Inventory_Pass;
				this.chatTField.to = string.Empty;
				this.chatTField.isShow = true;
				this.chatTField.tfChat.isFocus = true;
				this.chatTField.tfChat.setIputType(TField.INPUT_TYPE_NUMERIC);
				if (GameCanvas.isTouch)
				{
					this.chatTField.tfChat.doChangeToTextBox();
				}
				if (!Main.isPC)
				{
					this.chatTField.startChat2(this, string.Empty);
				}
				if (Main.isWindowsPhone)
				{
					this.chatTField.tfChat.strInfo = this.chatTField.strChat;
				}
				break;
			case 1:
				Service.gI().friend(0, -1);
				InfoDlg.showWait();
				break;
			case 2:
				Service.gI().enemy(0, -1);
				InfoDlg.showWait();
				break;
			case 3:
				this.setTypeMessage();
				if (this.chatTField == null)
				{
					this.chatTField = new ChatTextField();
					this.chatTField.tfChat.y = GameCanvas.h - 35 - ChatTextField.gI().tfChat.height;
					this.chatTField.initChatTextField();
					this.chatTField.parentScreen = GameCanvas.panel;
				}
				break;
			case 4:
				if ((int)mResources.language == 2)
				{
					string url = "http://dragonball.indonaga.com/coda/?username=" + GameCanvas.loginScr.tfUser.getText();
					this.hideNow();
					try
					{
						GameMidlet.instance.platformRequest(url);
					}
					catch (Exception ex)
					{
						ex.StackTrace.ToString();
					}
				}
				else
				{
					this.hideNow();
					if (global::Char.myCharz().taskMaint.taskId <= 10)
					{
						GameCanvas.startOKDlg(mResources.finishBomong);
					}
					else
					{
						MoneyCharge.gI().switchToMe();
					}
				}
				break;
			case 5:
				this.setTypeAuto();
				break;
		}
	}

	// Token: 0x0600093C RID: 2364 RVA: 0x0008D388 File Offset: 0x0008B788
	private void updateKeyOption()
	{
		this.updateKeyScrollView();
	}

	// Token: 0x0600093D RID: 2365 RVA: 0x0008D390 File Offset: 0x0008B790
	public void setTypeSpeacialSkill()
	{
		this.type = 25;
		this.setType(0);
		this.setTabSpeacialSkill();
		this.currentTabIndex = 0;
	}

	// Token: 0x0600093E RID: 2366 RVA: 0x0008D3B0 File Offset: 0x0008B7B0
	private void setTabSpeacialSkill()
	{
		this.ITEM_HEIGHT = 24;
		this.currentListLength = global::Char.myCharz().infoSpeacialSkill[this.currentTabIndex].Length;
		this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
		if (this.cmyLim < 0)
		{
			this.cmyLim = 0;
		}
		this.cmy = (this.cmtoY = this.cmyLast[this.currentTabIndex]);
		if (this.cmy < 0)
		{
			this.cmy = (this.cmtoY = 0);
		}
		if (this.cmy > this.cmyLim)
		{
			this.cmy = (this.cmtoY = this.cmyLim);
		}
		this.selected = ((!GameCanvas.isTouch) ? 0 : -1);
	}

	// Token: 0x0600093F RID: 2367 RVA: 0x0008D480 File Offset: 0x0008B880
	public bool isTypeShop()
	{
		return this.type == 1;
	}

	// Token: 0x06000940 RID: 2368 RVA: 0x0008D494 File Offset: 0x0008B894
	private void doNotiRuby(int type)
	{
		try
		{
			this.currItem.buyRuby = int.Parse(this.chatTField.tfChat.getText());
		}
		catch (Exception ex)
		{
			GameCanvas.startOKDlg(mResources.input_money_wrong);
			this.chatTField.isShow = false;
			return;
		}
		Command cmdYes = new Command(mResources.YES, this, (type != 0) ? 11001 : 11000, null);
		Command cmdNo = new Command(mResources.NO, this, 11002, null);
		GameCanvas.startYesNoDlg(mResources.notiRuby, cmdYes, cmdNo);
	}

	// Token: 0x06000941 RID: 2369 RVA: 0x0008D534 File Offset: 0x0008B934
	public static void paintUpgradeEffect(int x, int y, int wItem, int hItem, int nline, int cl, mGraphics g)
	{
		try
		{
			int num = (wItem << 1) + (hItem << 1);
			int num2 = num / nline;
			Panel.nsize = Panel.sizeUpgradeEff.Length;
			if (nline > 4)
			{
				Panel.nsize = 2;
			}
			for (int i = 0; i < nline; i++)
			{
				for (int j = 0; j < Panel.nsize; j++)
				{
					int wSize = (Panel.sizeUpgradeEff[j] <= 1) ? 1 : ((Panel.sizeUpgradeEff[j] >> 1) + 1);
					int x2 = x + Panel.upgradeEffectX(num2 * i, GameCanvas.gameTick - j * 4, wItem, hItem, wSize);
					int y2 = y + Panel.upgradeEffectY(num2 * i, GameCanvas.gameTick - j * 4, wItem, hItem, wSize);
					g.setColor(Panel.colorUpgradeEffect[cl][j]);
					g.fillRect(x2, y2, Panel.sizeUpgradeEff[j], Panel.sizeUpgradeEff[j]);
				}
			}
		}
		catch (Exception ex)
		{
		}
	}

	// Token: 0x06000942 RID: 2370 RVA: 0x0008D62C File Offset: 0x0008BA2C
	private static int upgradeEffectX(int dk, int tick, int wItem, int hitem, int wSize)
	{
		int num = (tick + dk) % ((wItem << 1) + (hitem << 1));
		if (0 <= num && num < wItem)
		{
			return num % wItem;
		}
		if (wItem <= num && num < wItem + hitem)
		{
			return wItem - wSize;
		}
		if (wItem + hitem <= num && num < (wItem << 1) + hitem)
		{
			return wItem - (num - hitem) % wItem - wSize;
		}
		return 0;
	}

	// Token: 0x06000943 RID: 2371 RVA: 0x0008D68C File Offset: 0x0008BA8C
	private static int upgradeEffectY(int dk, int tick, int wItem, int hitem, int wSize)
	{
		int num = (tick + dk) % ((wItem << 1) + (hitem << 1));
		if (0 <= num && num < wItem)
		{
			return 0;
		}
		if (wItem <= num && num < wItem + hitem)
		{
			return num % wItem;
		}
		if (wItem + hitem <= num && num < (wItem << 1) + hitem)
		{
			return hitem - wSize;
		}
		return hitem - (num - (wItem << 1)) % hitem - wSize;
	}

	// Token: 0x06000944 RID: 2372 RVA: 0x0008D6F0 File Offset: 0x0008BAF0
	public static int GetColor_ItemBg(int id)
	{
		switch (id)
		{
			case 1:
				return 2786816;
			case 2:
				return 7078041;
			case 3:
				return 12537346;
			case 4:
				return 1269146;
			case 5:
				return 13279744;
			case 6:
				return 11599872;
			default:
				return -1;
		}
	}

	// Token: 0x06000945 RID: 2373 RVA: 0x0008D748 File Offset: 0x0008BB48
	public static sbyte GetColor_Item_Upgrade(int lv)
	{
		if (lv < 0)
		{
			return 0;
		}
		switch (lv)
		{
			case 0:
			case 1:
			case 2:
			case 3:
			case 4:
			case 5:
			case 6:
			case 7:
			case 8:
				return 0;
			case 9:
				return 4;
			case 10:
				return 1;
			case 11:
				return 5;
			case 12:
				return 3;
			case 13:
				return 2;
			default:
				return 6;
		}
	}

	// Token: 0x06000946 RID: 2374 RVA: 0x0008D7B0 File Offset: 0x0008BBB0
	public static mFont GetFont(int color)
	{
		mFont result = mFont.tahoma_7;
		switch (color + 1)
		{
			case 0:
				result = mFont.tahoma_7;
				break;
			case 1:
				result = mFont.tahoma_7b_dark;
				break;
			case 2:
				result = mFont.tahoma_7b_green;
				break;
			case 3:
				result = mFont.tahoma_7b_blue;
				break;
			case 4:
				result = mFont.tahoma_7_red;
				break;
			case 5:
				result = mFont.tahoma_7_green;
				break;
			case 6:
				result = mFont.tahoma_7_blue;
				break;
			case 8:
				result = mFont.tahoma_7b_red;
				break;
			case 9:
				result = mFont.tahoma_7b_yellow;
				break;
		}
		return result;
	}

	// Token: 0x06000947 RID: 2375 RVA: 0x0008D85C File Offset: 0x0008BC5C
	public void paintOptItem(mGraphics g, int idOpt, int param, int x, int y, int w, int h)
	{
		if (idOpt == 34)
		{
			if (this.imgo_0 != null)
			{
				g.drawImage(this.imgo_0, x, y + h - this.imgo_0.getHeight());
			}
			else
			{
				this.imgo_0 = mSystem.loadImage("/mainImage/o_0.png");
			}
			if (this.imgo_1 != null)
			{
				g.drawImage(this.imgo_1, x, y + h - this.imgo_1.getHeight());
			}
			else
			{
				this.imgo_1 = mSystem.loadImage("/mainImage/o_1.png");
			}
		}
		else if (idOpt == 35)
		{
			if (this.imgo_0 != null)
			{
				g.drawImage(this.imgo_0, x, y + h - this.imgo_0.getHeight());
			}
			else
			{
				this.imgo_0 = mSystem.loadImage("/mainImage/o_0.png");
			}
			if (this.imgo_2 != null)
			{
				g.drawImage(this.imgo_2, x, y + h - this.imgo_2.getHeight());
			}
			else
			{
				this.imgo_2 = mSystem.loadImage("/mainImage/o_2.png");
			}
		}
		else if (idOpt == 36)
		{
			if (this.imgo_0 != null)
			{
				g.drawImage(this.imgo_0, x, y + h - this.imgo_0.getHeight());
			}
			else
			{
				this.imgo_0 = mSystem.loadImage("/mainImage/o_0.png");
			}
			if (this.imgo_3 != null)
			{
				g.drawImage(this.imgo_3, x, y + h - this.imgo_3.getHeight());
			}
			else
			{
				this.imgo_3 = mSystem.loadImage("/mainImage/o_3.png");
			}
			
		}
		else if(idOpt == 107) {
					
                var logo = mSystem.loadImage("/mainImage/star.png");
			g.drawImage(logo, x + 166, y + h - 15 - logo.getHeight(), mFont.RIGHT);
		}

	}

	// Token: 0x06000948 RID: 2376 RVA: 0x0008DA08 File Offset: 0x0008BE08
	public void paintOptSlotItem(mGraphics g, int idOpt, int param, int x, int y, int w, int h)
	{
		if (idOpt == 102 && param > ChatPopup.numSlot)
		{
			sbyte color_Item_Upgrade = Panel.GetColor_Item_Upgrade(param);
			int nline = param - ChatPopup.numSlot;
			Panel.paintUpgradeEffect(x, y, w, h, nline, (int)color_Item_Upgrade, g);
		}
	}

	// Token: 0x06000949 RID: 2377 RVA: 0x0008DA48 File Offset: 0x0008BE48
	public static mFont setTextColor(int id, int type)
	{
		if (type == 0)
		{
			switch (id)
			{
				case 0:
					return mFont.bigNumber_While;
				case 1:
					return mFont.bigNumber_green;
				case 3:
					return mFont.bigNumber_orange;
				case 4:
					return mFont.bigNumber_blue;
				case 5:
					return mFont.bigNumber_yellow;
				case 6:
					return mFont.bigNumber_red;
			}
			return mFont.bigNumber_While;
		}
		switch (id)
		{
			case 0:
				return mFont.tahoma_7b_white;
			case 1:
				return mFont.tahoma_7b_green;
			case 3:
				return mFont.tahoma_7b_yellowSmall2;
			case 4:
				return mFont.tahoma_7b_blue;
			case 5:
				return mFont.tahoma_7b_yellow;
			case 6:
				return mFont.tahoma_7b_red;
			case 7:
				return mFont.tahoma_7b_dark;
		}
		return mFont.tahoma_7b_white;
	}

	// Token: 0x0600094A RID: 2378 RVA: 0x0008DB08 File Offset: 0x0008BF08
	private bool GetInventorySelect_isbody(int select, int subSelect, Item[] arrItem)
	{
		int num = select - 1 + subSelect * 20;
		return subSelect == 0 && num < arrItem.Length;
	}

	// Token: 0x0600094B RID: 2379 RVA: 0x0008DB34 File Offset: 0x0008BF34
	private int GetInventorySelect_body(int select, int subSelect)
	{
		return select - 1 + subSelect * 20;
	}

	// Token: 0x0600094C RID: 2380 RVA: 0x0008DB4C File Offset: 0x0008BF4C
	private int GetInventorySelect_bag(int select, int subSelect, Item[] arrItem)
	{
		int num = select - 1 + subSelect * 20;
		return num - arrItem.Length;
	}

	// Token: 0x0600094D RID: 2381 RVA: 0x0008DB69 File Offset: 0x0008BF69
	private bool isTabInven()
	{
		return (this.type == 0 && this.currentTabIndex == 1) || (this.type == 7 && this.currentTabIndex == 0);
	}

	// Token: 0x0600094E RID: 2382 RVA: 0x0008DB9C File Offset: 0x0008BF9C
	private void updateKeyInvenTab()
	{
		if (this.selected >= 0)
		{
			if (GameCanvas.keyPressed[(!Main.isPC) ? 4 : 23])
			{
				this.newSelected--;
				if (this.newSelected < 0)
				{
					this.newSelected = 0;
					if (GameCanvas.isFocusPanel2)
					{
						GameCanvas.isFocusPanel2 = false;
						GameCanvas.panel.selected = 0;
					}
				}
			}
			else if (GameCanvas.keyPressed[(!Main.isPC) ? 6 : 24])
			{
				this.newSelected++;
				if (this.newSelected > (int)this.size_tab - 1)
				{
					this.newSelected = (int)this.size_tab - 1;
					if (GameCanvas.panel2 != null)
					{
						GameCanvas.isFocusPanel2 = true;
						GameCanvas.panel2.selected = 0;
					}
				}
			}
		}
	}

	// Token: 0x0600094F RID: 2383 RVA: 0x0008DC7C File Offset: 0x0008C07C
	private void updateKeyInventory()
	{
		this.updateKeyScrollView();
		if (this.selected == 0)
		{
			this.updateKeyInvenTab();
		}
	}

	// Token: 0x06000950 RID: 2384 RVA: 0x0008DC95 File Offset: 0x0008C095
	private bool IsTabOption()
	{
		if ((int)this.size_tab > 0)
		{
			if (this.currentTabName.Length > 1)
			{
				if (this.selected == 0)
				{
					return true;
				}
			}
			else if (this.selected >= 0)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000951 RID: 2385 RVA: 0x0008DCD4 File Offset: 0x0008C0D4
	private int checkCurrentListLength(int arrLength)
	{
		int num = 20;
		int num2 = arrLength / 20 + ((arrLength % 20 <= 0) ? 0 : 1);
		this.size_tab = (sbyte)num2;
		if (this.newSelected > num2 - 1)
		{
			this.newSelected = num2 - 1;
		}
		if (arrLength % 20 > 0 && this.newSelected == num2 - 1)
		{
			num = arrLength % 20;
		}
		return num + 1;
	}

	// Token: 0x06000952 RID: 2386 RVA: 0x0008DD3C File Offset: 0x0008C13C
	private void setNewSelected(int arrLength, bool resetSelect)
	{
		int num = arrLength / 20 + ((arrLength % 20 <= 0) ? 0 : 1);
		int num2 = this.xScroll;
		this.newSelected = (GameCanvas.px - num2) / this.TAB_W_NEW;
		if (this.newSelected > num - 1)
		{
			this.newSelected = num - 1;
		}
		if (GameCanvas.px < num2)
		{
			this.newSelected = 0;
		}
		this.setTabInventory(resetSelect);
	}

	// Token: 0x04001012 RID: 4114
	public bool isShow;

	// Token: 0x04001013 RID: 4115
	public int X;

	// Token: 0x04001014 RID: 4116
	public int Y;

	// Token: 0x04001015 RID: 4117
	public int W;

	// Token: 0x04001016 RID: 4118
	public int H;

	// Token: 0x04001017 RID: 4119
	public int ITEM_HEIGHT;

	// Token: 0x04001018 RID: 4120
	public int TAB_W;

	// Token: 0x04001019 RID: 4121
	public int TAB_W_NEW;

	// Token: 0x0400101A RID: 4122
	public int cmtoY;

	// Token: 0x0400101B RID: 4123
	public int cmy;

	// Token: 0x0400101C RID: 4124
	public int cmdy;

	// Token: 0x0400101D RID: 4125
	public int cmvy;

	// Token: 0x0400101E RID: 4126
	public int cmyLim;

	// Token: 0x0400101F RID: 4127
	public int xc;

	// Token: 0x04001020 RID: 4128
	public int[] cmyLast;

	// Token: 0x04001021 RID: 4129
	public int cmtoX;

	// Token: 0x04001022 RID: 4130
	public int cmx;

	// Token: 0x04001023 RID: 4131
	public int cmxLim;

	// Token: 0x04001024 RID: 4132
	public int cmxMap;

	// Token: 0x04001025 RID: 4133
	public int cmyMap;

	// Token: 0x04001026 RID: 4134
	public int cmxMapLim;

	// Token: 0x04001027 RID: 4135
	public int cmyMapLim;

	// Token: 0x04001028 RID: 4136
	public int cmyQuest;

	// Token: 0x04001029 RID: 4137
	public static Image imgBantay;

	// Token: 0x0400102A RID: 4138
	public static Image imgX;

	// Token: 0x0400102B RID: 4139
	public static Image imgMap;

	// Token: 0x0400102C RID: 4140
	public TabClanIcon tabIcon;

	// Token: 0x0400102D RID: 4141
	public MyVector vItemCombine = new MyVector();

	// Token: 0x0400102E RID: 4142
	public int moneyGD;

	// Token: 0x0400102F RID: 4143
	public int friendMoneyGD;

	// Token: 0x04001030 RID: 4144
	public bool isLock;

	// Token: 0x04001031 RID: 4145
	public bool isFriendLock;

	// Token: 0x04001032 RID: 4146
	public bool isAccept;

	// Token: 0x04001033 RID: 4147
	public bool isFriendAccep;

	// Token: 0x04001034 RID: 4148
	public string topName;

	// Token: 0x04001035 RID: 4149
	public ChatTextField chatTField;

	// Token: 0x04001036 RID: 4150
	public static string specialInfo;

	// Token: 0x04001037 RID: 4151
	public static short spearcialImage;

	// Token: 0x04001038 RID: 4152
	public static Image imgStar;

	// Token: 0x04001039 RID: 4153
	public static Image imgMaxStar;

	// Token: 0x0400103A RID: 4154
	public static Image imgStar8;

	// Token: 0x0400103B RID: 4155
	public static Image imgNew;

	// Token: 0x0400103C RID: 4156
	public static Image imgXu;

	// Token: 0x0400103D RID: 4157
	public static Image imgTicket;

	// Token: 0x0400103E RID: 4158
	public static Image imgLuong;

	// Token: 0x0400103F RID: 4159
	public static Image imgLuongKhoa;

	// Token: 0x04001040 RID: 4160
	private static Image imgUp;

	// Token: 0x04001041 RID: 4161
	private static Image imgDown;

	// Token: 0x04001042 RID: 4162
	private int pa1;

	// Token: 0x04001043 RID: 4163
	private int pa2;

	// Token: 0x04001044 RID: 4164
	private bool trans;

	// Token: 0x04001045 RID: 4165
	private int pX;

	// Token: 0x04001046 RID: 4166
	private int pY;

	// Token: 0x04001047 RID: 4167
	private Command left = new Command(mResources.SELECT, 0);

	// Token: 0x04001048 RID: 4168
	public int type;

	// Token: 0x04001049 RID: 4169
	public int currentTabIndex;

	// Token: 0x0400104A RID: 4170
	public int startTabPos;

	// Token: 0x0400104B RID: 4171
	public int[] lastTabIndex;

	// Token: 0x0400104C RID: 4172
	public string[][] currentTabName;

	// Token: 0x0400104D RID: 4173
	private int[] currClanOption;

	// Token: 0x0400104E RID: 4174
	public int mainTabPos = 4;

	// Token: 0x0400104F RID: 4175
	public int shopTabPos = 50;

	// Token: 0x04001050 RID: 4176
	public int boxTabPos = 50;

	// Token: 0x04001051 RID: 4177
	public string[][] mainTabName;

	// Token: 0x04001052 RID: 4178
	public string[] mapNames;

	// Token: 0x04001053 RID: 4179
	public string[] planetNames;

	// Token: 0x04001054 RID: 4180
	public static string[] strTool = new string[]
	{
		mResources.gameInfo,
		mResources.change_flag,
		mResources.change_zone,
		mResources.chat_world,
		mResources.account,
		mResources.option,
		mResources.change_account
	};

	// Token: 0x04001055 RID: 4181
	public static string[] strCauhinh = new string[]
	{
		(!GameCanvas.isPlaySound) ? mResources.turnOnSound : mResources.turnOffSound,
		mResources.increase_vga,
		mResources.analog,
		(mGraphics.zoomLevel <= 1) ? mResources.x2Screen : mResources.x1Screen
	};

	// Token: 0x04001056 RID: 4182
	public static string[] strAccount = new string[]
	{
		mResources.inventory_Pass,
		mResources.friend,
		mResources.enemy,
		mResources.msg,
		mResources.charger
	};

	// Token: 0x04001057 RID: 4183
	public static string[] strAuto = new string[]
	{
		mResources.useGem
	};

	// Token: 0x04001058 RID: 4184
	public static int graphics = 0;

	// Token: 0x04001059 RID: 4185
	public string[][] shopTabName;

	// Token: 0x0400105A RID: 4186
	public int[] maxPageShop;

	// Token: 0x0400105B RID: 4187
	public int[] currPageShop;

	// Token: 0x0400105C RID: 4188
	private static string[][] boxTabName = new string[][]
	{
		mResources.chestt,
		mResources.inventory
	};

	// Token: 0x0400105D RID: 4189
	private static string[][] boxCombine = new string[][]
	{
		mResources.combine,
		mResources.inventory
	};

	// Token: 0x0400105E RID: 4190
	private static string[][] boxZone = new string[][]
	{
		mResources.zonee
	};

	// Token: 0x0400105F RID: 4191
	private static string[][] boxMap = new string[][]
	{
		mResources.mapp
	};

	// Token: 0x04001060 RID: 4192
	private static string[][] boxGD = new string[][]
	{
		mResources.inventory,
		mResources.item_give,
		mResources.item_receive
	};

	// Token: 0x04001061 RID: 4193
	private static string[][] boxPet = mResources.petMainTab;

	// Token: 0x04001062 RID: 4194
	public string[][][] tabName = new string[][][]
	{
		null,
		null,
		Panel.boxTabName,
		Panel.boxZone,
		Panel.boxMap,
		null,
		null,
		new string[][]
		{
			new string[]
			{
				string.Empty
			}
		},
		new string[][]
		{
			new string[]
			{
				string.Empty
			}
		},
		new string[][]
		{
			new string[]
			{
				string.Empty
			}
		},
		new string[][]
		{
			new string[]
			{
				string.Empty
			}
		},
		new string[][]
		{
			new string[]
			{
				string.Empty
			}
		},
		Panel.boxCombine,
		Panel.boxGD,
		new string[][]
		{
			new string[]
			{
				string.Empty
			}
		},
		new string[][]
		{
			new string[]
			{
				string.Empty
			}
		},
		new string[][]
		{
			new string[]
			{
				string.Empty
			}
		},
		new string[][]
		{
			new string[]
			{
				string.Empty
			}
		},
		new string[][]
		{
			new string[]
			{
				string.Empty
			}
		},
		new string[][]
		{
			new string[]
			{
				string.Empty
			}
		},
		new string[][]
		{
			new string[]
			{
				string.Empty
			}
		},
		Panel.boxPet,
		new string[][]
		{
			new string[]
			{
				string.Empty
			}
		},
		new string[][]
		{
			new string[]
			{
				string.Empty
			}
		},
		new string[][]
		{
			new string[]
			{
				string.Empty
			}
		},
		new string[][]
		{
			new string[]
			{
				string.Empty
			}
		},
		new string[][]
		{
			new string[]
			{
				string.Empty
			}
		}
	};

	// Token: 0x04001063 RID: 4195
	private static sbyte BOX_BAG = 0;

	// Token: 0x04001064 RID: 4196
	private static sbyte BAG_BOX = 1;

	// Token: 0x04001065 RID: 4197
	private static sbyte BOX_BODY = 2;

	// Token: 0x04001066 RID: 4198
	private static sbyte BODY_BOX = 3;

	// Token: 0x04001067 RID: 4199
	private static sbyte BAG_BODY = 4;

	// Token: 0x04001068 RID: 4200
	private static sbyte BODY_BAG = 5;

	// Token: 0x04001069 RID: 4201
	private static sbyte BAG_PET = 6;

	// Token: 0x0400106A RID: 4202
	private static sbyte PET_BAG = 7;

	// Token: 0x0400106B RID: 4203
	public int hasUse;

	// Token: 0x0400106C RID: 4204
	public int hasUseBag;

	// Token: 0x0400106D RID: 4205
	public int currentListLength;

	// Token: 0x0400106E RID: 4206
	private int[] lastSelect;

	// Token: 0x0400106F RID: 4207
	public static int[] mapIdTraidat = new int[]
	{
		21,
		0,
		1,
		2,
		24,
		3,
		4,
		5,
		6,
		27,
		28,
		29,
		30,
		42,
		47,
		46
	};

	// Token: 0x04001070 RID: 4208
	public static int[] mapXTraidat = new int[]
	{
		39,
		42,
		105,
		93,
		61,
		93,
		142,
		165,
		210,
		100,
		165,
		220,
		233,
		10,
		125,
		125
	};

	// Token: 0x04001071 RID: 4209
	public static int[] mapYTraidat = new int[]
	{
		28,
		60,
		48,
		96,
		88,
		131,
		136,
		95,
		32,
		200,
		189,
		167,
		120,
		110,
		20,
		20
	};

	// Token: 0x04001072 RID: 4210
	public static int[] mapIdNamek = new int[]
	{
		22,
		7,
		8,
		9,
		25,
		11,
		12,
		13,
		10,
		31,
		32,
		33,
		34,
		43
	};

	// Token: 0x04001073 RID: 4211
	public static int[] mapXNamek = new int[]
	{
		55,
		30,
		93,
		80,
		24,
		149,
		219,
		220,
		233,
		170,
		148,
		195,
		148,
		10
	};

	// Token: 0x04001074 RID: 4212
	public static int[] mapYNamek = new int[]
	{
		136,
		84,
		69,
		34,
		25,
		42,
		32,
		110,
		192,
		70,
		106,
		156,
		210,
		57
	};

	// Token: 0x04001075 RID: 4213
	public static int[] mapIdSaya = new int[]
	{
		23,
		14,
		15,
		16,
		26,
		17,
		18,
		20,
		19,
		35,
		36,
		37,
		38,
		44
	};

	// Token: 0x04001076 RID: 4214
	public static int[] mapXSaya = new int[]
	{
		90,
		95,
		144,
		234,
		231,
		122,
		176,
		158,
		205,
		54,
		105,
		159,
		231,
		27
	};

	// Token: 0x04001077 RID: 4215
	public static int[] mapYSaya = new int[]
	{
		10,
		43,
		20,
		36,
		69,
		87,
		112,
		167,
		160,
		151,
		173,
		207,
		194,
		29
	};

	// Token: 0x04001078 RID: 4216
	public static int[][] mapId = new int[][]
	{
		Panel.mapIdTraidat,
		Panel.mapIdNamek,
		Panel.mapIdSaya
	};

	// Token: 0x04001079 RID: 4217
	public static int[][] mapX = new int[][]
	{
		Panel.mapXTraidat,
		Panel.mapXNamek,
		Panel.mapXSaya
	};

	// Token: 0x0400107A RID: 4218
	public static int[][] mapY = new int[][]
	{
		Panel.mapYTraidat,
		Panel.mapYNamek,
		Panel.mapYSaya
	};

	// Token: 0x0400107B RID: 4219
	public Item currItem;

	// Token: 0x0400107C RID: 4220
	public Clan currClan;

	// Token: 0x0400107D RID: 4221
	public ClanMessage currMess;

	// Token: 0x0400107E RID: 4222
	public Member currMem;

	// Token: 0x0400107F RID: 4223
	public Clan[] clans;

	// Token: 0x04001080 RID: 4224
	public MyVector member;

	// Token: 0x04001081 RID: 4225
	public MyVector myMember;

	// Token: 0x04001082 RID: 4226
	public MyVector logChat = new MyVector();

	// Token: 0x04001083 RID: 4227
	public MyVector vPlayerMenu = new MyVector();

	// Token: 0x04001084 RID: 4228
	public MyVector vFriend = new MyVector();

	// Token: 0x04001085 RID: 4229
	public MyVector vMyGD = new MyVector();

	// Token: 0x04001086 RID: 4230
	public MyVector vFriendGD = new MyVector();

	// Token: 0x04001087 RID: 4231
	public MyVector vTop = new MyVector();

	// Token: 0x04001088 RID: 4232
	public MyVector vEnemy = new MyVector();

	// Token: 0x04001089 RID: 4233
	public MyVector vFlag = new MyVector();

	// Token: 0x0400108A RID: 4234
	public Command cmdClose;

	// Token: 0x0400108B RID: 4235
	public static bool CanNapTien = false;

	// Token: 0x0400108C RID: 4236
	public static int WIDTH_PANEL = 240;

	// Token: 0x0400108D RID: 4237
	private int position;

	// Token: 0x0400108E RID: 4238
	public global::Char charMenu;

	// Token: 0x0400108F RID: 4239
	private bool isThachDau;

	// Token: 0x04001090 RID: 4240
	public int typeShop = -1;

	// Token: 0x04001091 RID: 4241
	public int xScroll;

	// Token: 0x04001092 RID: 4242
	public int yScroll;

	// Token: 0x04001093 RID: 4243
	public int wScroll;

	// Token: 0x04001094 RID: 4244
	public int hScroll;

	// Token: 0x04001095 RID: 4245
	public ChatPopup cp;

	// Token: 0x04001096 RID: 4246
	public int idIcon;

	// Token: 0x04001097 RID: 4247
	public int[] partID;

	// Token: 0x04001098 RID: 4248
	private int timeShow;

	// Token: 0x04001099 RID: 4249
	public bool isBoxClan;

	// Token: 0x0400109A RID: 4250
	public int w;

	// Token: 0x0400109B RID: 4251
	private int pa;

	// Token: 0x0400109C RID: 4252
	public int selected;

	// Token: 0x0400109D RID: 4253
	private int cSelected;

	// Token: 0x0400109E RID: 4254
	private int newSelected;

	// Token: 0x0400109F RID: 4255
	private bool isClanOption;

	// Token: 0x040010A0 RID: 4256
	public bool isSearchClan;

	// Token: 0x040010A1 RID: 4257
	public bool isMessage;

	// Token: 0x040010A2 RID: 4258
	public bool isViewMember;

	// Token: 0x040010A3 RID: 4259
	public const int TYPE_MAIN = 0;

	// Token: 0x040010A4 RID: 4260
	public const int TYPE_SHOP = 1;

	// Token: 0x040010A5 RID: 4261
	public const int TYPE_BOX = 2;

	// Token: 0x040010A6 RID: 4262
	public const int TYPE_ZONE = 3;

	// Token: 0x040010A7 RID: 4263
	public const int TYPE_MAP = 4;

	// Token: 0x040010A8 RID: 4264
	public const int TYPE_CLANS = 5;

	// Token: 0x040010A9 RID: 4265
	public const int TYPE_INFOMATION = 6;

	// Token: 0x040010AA RID: 4266
	public const int TYPE_BODY = 7;

	// Token: 0x040010AB RID: 4267
	public const int TYPE_MESS = 8;

	// Token: 0x040010AC RID: 4268
	public const int TYPE_ARCHIVEMENT = 9;

	// Token: 0x040010AD RID: 4269
	public const int PLAYER_MENU = 10;

	// Token: 0x040010AE RID: 4270
	public const int TYPE_FRIEND = 11;

	// Token: 0x040010AF RID: 4271
	public const int TYPE_COMBINE = 12;

	// Token: 0x040010B0 RID: 4272
	public const int TYPE_GIAODICH = 13;

	// Token: 0x040010B1 RID: 4273
	public const int TYPE_MAPTRANS = 14;

	// Token: 0x040010B2 RID: 4274
	public const int TYPE_TOP = 15;

	// Token: 0x040010B3 RID: 4275
	public const int TYPE_ENEMY = 16;

	// Token: 0x040010B4 RID: 4276
	public const int TYPE_KIGUI = 17;

	// Token: 0x040010B5 RID: 4277
	public const int TYPE_FLAG = 18;

	// Token: 0x040010B6 RID: 4278
	public const int TYPE_OPTION = 19;

	// Token: 0x040010B7 RID: 4279
	public const int TYPE_ACCOUNT = 20;

	// Token: 0x040010B8 RID: 4280
	public const int TYPE_PET_MAIN = 21;

	// Token: 0x040010B9 RID: 4281
	public const int TYPE_AUTO = 22;

	// Token: 0x040010BA RID: 4282
	public const int TYPE_GAMEINFO = 23;

	// Token: 0x040010BB RID: 4283
	public const int TYPE_GAMEINFOSUB = 24;

	// Token: 0x040010BC RID: 4284
	public const int TYPE_SPEACIALSKILL = 25;

	// Token: 0x040010BD RID: 4285
	private int pointerDownTime;

	// Token: 0x040010BE RID: 4286
	private int pointerDownFirstX;

	// Token: 0x040010BF RID: 4287
	private int[] pointerDownLastX = new int[3];

	// Token: 0x040010C0 RID: 4288
	private bool pointerIsDowning;

	// Token: 0x040010C1 RID: 4289
	private bool isDownWhenRunning;

	// Token: 0x040010C2 RID: 4290
	private bool wantUpdateList;

	// Token: 0x040010C3 RID: 4291
	private int waitToPerform;

	// Token: 0x040010C4 RID: 4292
	private int cmRun;

	// Token: 0x040010C5 RID: 4293
	private int keyTouchLock = -1;

	// Token: 0x040010C6 RID: 4294
	private int keyToundGD = -1;

	// Token: 0x040010C7 RID: 4295
	private int keyTouchCombine = -1;

	// Token: 0x040010C8 RID: 4296
	private int keyTouchMapButton = -1;

	// Token: 0x040010C9 RID: 4297
	public int indexMouse = -1;

	// Token: 0x040010CA RID: 4298
	private bool justRelease;

	// Token: 0x040010CB RID: 4299
	private int keyTouchTab = -1;

	// Token: 0x040010CC RID: 4300
	public string[][] clansOption = new string[][]
	{
		mResources.findClan,
		mResources.createClan
	};

	// Token: 0x040010CD RID: 4301
	public string clanInfo = string.Empty;

	// Token: 0x040010CE RID: 4302
	public string clanReport = string.Empty;

	// Token: 0x040010CF RID: 4303
	private bool isHaveClan;

	// Token: 0x040010D0 RID: 4304
	private Scroll scroll;

	// Token: 0x040010D1 RID: 4305
	private int cmvx;

	// Token: 0x040010D2 RID: 4306
	private int cmdx;

	// Token: 0x040010D3 RID: 4307
	private bool isSelectPlayerMenu;

	// Token: 0x040010D4 RID: 4308
	private string[] strStatus = new string[]
	{
		mResources.follow,
		mResources.defend,
		mResources.attack,
		mResources.gohome,
		mResources.fusion,
		mResources.fusionForever
	};

	// Token: 0x040010D5 RID: 4309
	private int currentButtonPress;

	// Token: 0x040010D6 RID: 4310
	public static long[] t_tiemnang = new long[]
	{
		50000000L,
		250000000L,
		1250000000L,
		5000000000L,
		15000000000L,
		30000000000L,
		45000000000L,
		60000000000L,
		75000000000L,
		90000000000L,
		110000000000L,
		130000000000L,
		150000000000L,
		170000000000L
	};

	// Token: 0x040010D7 RID: 4311
	private int[] zoneColor = new int[]
	{
		43520,
		14743570,
		14155776
	};

	// Token: 0x040010D8 RID: 4312
	public string[] combineInfo;

	// Token: 0x040010D9 RID: 4313
	public string[] combineTopInfo;

	// Token: 0x040010DA RID: 4314
	public static int[] color1 = new int[]
	{
		2327248,
		8982199,
		16713222
	};

	// Token: 0x040010DB RID: 4315
	public static int[] color2 = new int[]
	{
		4583423,
		16719103,
		16714764
	};

	// Token: 0x040010DC RID: 4316
	private bool isUp;

	// Token: 0x040010DD RID: 4317
	private int compare;

	// Token: 0x040010DE RID: 4318
	public static string strWantToBuy = string.Empty;

	// Token: 0x040010DF RID: 4319
	public int xstart;

	// Token: 0x040010E0 RID: 4320
	public int ystart;

	// Token: 0x040010E1 RID: 4321
	public int popupW = 140;

	// Token: 0x040010E2 RID: 4322
	public int popupH = 160;

	// Token: 0x040010E3 RID: 4323
	public int cmySK;

	// Token: 0x040010E4 RID: 4324
	public int cmtoYSK;

	// Token: 0x040010E5 RID: 4325
	public int cmdySK;

	// Token: 0x040010E6 RID: 4326
	public int cmvySK;

	// Token: 0x040010E7 RID: 4327
	public int cmyLimSK;

	// Token: 0x040010E8 RID: 4328
	public int popupY;

	// Token: 0x040010E9 RID: 4329
	public int popupX;

	// Token: 0x040010EA RID: 4330
	public int isborderIndex;

	// Token: 0x040010EB RID: 4331
	public int isselectedRow;

	// Token: 0x040010EC RID: 4332
	public int indexSize = 28;

	// Token: 0x040010ED RID: 4333
	public int indexTitle;

	// Token: 0x040010EE RID: 4334
	public int indexSelect;

	// Token: 0x040010EF RID: 4335
	public int indexRow = -1;

	// Token: 0x040010F0 RID: 4336
	public int indexRowMax;

	// Token: 0x040010F1 RID: 4337
	public int indexMenu;

	// Token: 0x040010F2 RID: 4338
	public int columns = 6;

	// Token: 0x040010F3 RID: 4339
	public int rows;

	// Token: 0x040010F4 RID: 4340
	public int inforX;

	// Token: 0x040010F5 RID: 4341
	public int inforY;

	// Token: 0x040010F6 RID: 4342
	public int inforW;

	// Token: 0x040010F7 RID: 4343
	public int inforH;

	// Token: 0x040010F8 RID: 4344
	private int yPaint;

	// Token: 0x040010F9 RID: 4345
	private int xMap;

	// Token: 0x040010FA RID: 4346
	private int yMap;

	// Token: 0x040010FB RID: 4347
	private int xMapTask;

	// Token: 0x040010FC RID: 4348
	private int yMapTask;

	// Token: 0x040010FD RID: 4349
	private int xMove;

	// Token: 0x040010FE RID: 4350
	private int yMove;

	// Token: 0x040010FF RID: 4351
	public static bool isPaintMap = true;

	// Token: 0x04001100 RID: 4352
	public bool isClose;

	// Token: 0x04001101 RID: 4353
	private int infoSelect;

	// Token: 0x04001102 RID: 4354
	public static MyVector vGameInfo = new MyVector(string.Empty);

	// Token: 0x04001103 RID: 4355
	public static string[] contenInfo;

	// Token: 0x04001104 RID: 4356
	public bool isViewChatServer;

	// Token: 0x04001105 RID: 4357
	private int currInfoItem;

	// Token: 0x04001106 RID: 4358
	public global::Char charInfo;

	// Token: 0x04001107 RID: 4359
	private bool isChangeZone;

	// Token: 0x04001108 RID: 4360
	private bool isKiguiXu;

	// Token: 0x04001109 RID: 4361
	private bool isKiguiLuong;

	// Token: 0x0400110A RID: 4362
	private int delayKigui;

	// Token: 0x0400110B RID: 4363
	public sbyte combineSuccess = -1;

	// Token: 0x0400110C RID: 4364
	public int idNPC;

	// Token: 0x0400110D RID: 4365
	public int xS;

	// Token: 0x0400110E RID: 4366
	public int yS;

	// Token: 0x0400110F RID: 4367
	private int rS;

	// Token: 0x04001110 RID: 4368
	private int angleS;

	// Token: 0x04001111 RID: 4369
	private int angleO;

	// Token: 0x04001112 RID: 4370
	private int iAngleS;

	// Token: 0x04001113 RID: 4371
	private int iDotS;

	// Token: 0x04001114 RID: 4372
	private int speed;

	// Token: 0x04001115 RID: 4373
	private int[] xArgS;

	// Token: 0x04001116 RID: 4374
	private int[] yArgS;

	// Token: 0x04001117 RID: 4375
	private int[] xDotS;

	// Token: 0x04001118 RID: 4376
	private int[] yDotS;

	// Token: 0x04001119 RID: 4377
	private int time;

	// Token: 0x0400111A RID: 4378
	private int typeCombine;

	// Token: 0x0400111B RID: 4379
	private int countUpdate;

	// Token: 0x0400111C RID: 4380
	private int countR;

	// Token: 0x0400111D RID: 4381
	private int countWait;

	// Token: 0x0400111E RID: 4382
	private bool isSpeedCombine;

	// Token: 0x0400111F RID: 4383
	private bool isCompleteEffCombine = true;

	// Token: 0x04001120 RID: 4384
	private bool isPaintCombine;

	// Token: 0x04001121 RID: 4385
	public bool isDoneCombine = true;

	// Token: 0x04001122 RID: 4386
	public short iconID1;

	// Token: 0x04001123 RID: 4387
	public short iconID2;

	// Token: 0x04001124 RID: 4388
	public short iconID3;

	// Token: 0x04001125 RID: 4389
	public short[] iconID;

	// Token: 0x04001126 RID: 4390
	public string[][] speacialTabName;

	// Token: 0x04001127 RID: 4391
	public static int[] sizeUpgradeEff = new int[]
	{
		2,
		1,
		1
	};

	// Token: 0x04001128 RID: 4392
	public static int nsize = 1;

	// Token: 0x04001129 RID: 4393
	public const sbyte COLOR_WHITE = 0;

	// Token: 0x0400112A RID: 4394
	public const sbyte COLOR_GREEN = 1;

	// Token: 0x0400112B RID: 4395
	public const sbyte COLOR_PURPLE = 2;

	// Token: 0x0400112C RID: 4396
	public const sbyte COLOR_ORANGE = 3;

	// Token: 0x0400112D RID: 4397
	public const sbyte COLOR_BLUE = 4;

	// Token: 0x0400112E RID: 4398
	public const sbyte COLOR_YELLOW = 5;

	// Token: 0x0400112F RID: 4399
	public const sbyte COLOR_RED = 6;

	// Token: 0x04001130 RID: 4400
	public const sbyte COLOR_BLACK = 7;

	// Token: 0x04001131 RID: 4401
	public static int[][] colorUpgradeEffect = new int[][]
	{
		new int[]
		{
			16777215,
			15000805,
			13487823,
			11711155,
			9671828,
			7895160
		},
		new int[]
		{
			61952,
			58624,
			52224,
			45824,
			39168,
			32768
		},
		new int[]
		{
			13500671,
			12058853,
			10682572,
			9371827,
			7995545,
			6684800
		},
		new int[]
		{
			16744192,
			15037184,
			13395456,
			11753728,
			10046464,
			8404992
		},
		new int[]
		{
			37119,
			33509,
			28108,
			24499,
			21145,
			17536
		},
		new int[]
		{
			16776192,
			15063040,
			12635136,
			11776256,
			10063872,
			8290304
		},
		new int[]
		{
			16711680,
			15007744,
			13369344,
			11730944,
			10027008,
			8388608
		}
	};

	// Token: 0x04001132 RID: 4402
	public const int color_item_white = 15987701;

	// Token: 0x04001133 RID: 4403
	public const int color_item_green = 2786816;

	// Token: 0x04001134 RID: 4404
	public const int color_item_purple = 7078041;

	// Token: 0x04001135 RID: 4405
	public const int color_item_orange = 12537346;

	// Token: 0x04001136 RID: 4406
	public const int color_item_blue = 1269146;

	// Token: 0x04001137 RID: 4407
	public const int color_item_yellow = 13279744;

	// Token: 0x04001138 RID: 4408
	public const int color_item_red = 11599872;

	// Token: 0x04001139 RID: 4409
	public const int color_item_black = 2039326;

	// Token: 0x0400113A RID: 4410
	private Image imgo_0;

	// Token: 0x0400113B RID: 4411
	private Image imgo_1;

	// Token: 0x0400113C RID: 4412
	private Image imgo_2;

	// Token: 0x0400113D RID: 4413
	private Image imgo_3;

	// Token: 0x0400113E RID: 4414
	public const int numItem = 20;

	// Token: 0x0400113F RID: 4415
	public const sbyte INVENTORY_TAB = 1;

	// Token: 0x04001140 RID: 4416
	public sbyte size_tab;
}
