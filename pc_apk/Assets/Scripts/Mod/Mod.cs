using AssemblyCSharp.Mod2.PickMob;
using AssemblyCSharp.Mod2.Xmap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using UnityEngine;
namespace Mod
{
   public class Mod : IActionListener
{
		private static Mod Instance = new Mod();
		public static Mod gI()
        {
			bool flag = Mod.Instance == null;
			Mod result;
			if (flag)
			{
				result = (Mod.Instance = new Mod());
			}
			else
			{
				result = Mod.Instance;
			}
			return result;
		}
		public void perform(int id, object p) { }
		public static void MenuMod()
		{
			MyVector myVector = new MyVector();
			//myVector.addElement(new Command("Đổi Khu", 880));
			//myVector.addElement(new Command("Bông Tai", 881));
			//myVector.addElement(new Command("Capsule", 882));
            myVector.addElement(new Command("Auto Map", 883));
			myVector.addElement(new Command("Auto Pean", 14));
			//myVector.addElement(new Command("Button", 890));
			myVector.addElement(new Command("Auto Train", 45));
			myVector.addElement(new Command("Auto Skill", 89));
			myVector.addElement(new Command(Mod.doHoa, 15));
			myVector.addElement(new Command("Auto Pet", 16));
			myVector.addElement(new Command(Mod.upYardrat, 31));
			myVector.addElement(new Command("Auto Boss", 32));
			
			myVector.addElement(new Command(Mod.chucNangKhac, 33));
			GameCanvas.menu.startAt(myVector, 4);
		}
		public static void td()
		{
			if (TileMap.mapID == 21 || TileMap.mapID == 22 || TileMap.mapID == 23)
			{
				int num = 0;
				for (int i = 0; i < global::Char.myCharz().arrItemBox.Length; i++)
				{
					Item item = global::Char.myCharz().arrItemBox[i];
					if (item != null && item.template.type == 6)
					{
						num += item.quantity;
					}
				}
				if (num < 20 && GameCanvas.gameTick % 200 == 0)
				{
					for (int j = 0; j < global::Char.myCharz().arrItemBag.Length; j++)
					{
						Item item2 = global::Char.myCharz().arrItemBag[j];
						if (item2 != null && item2.template.type == 6)
						{
							Service.gI().getItem(1, (sbyte)j);
						}
					}
				}
				if ((GameScr.gI().magicTree.currPeas > 0 && GameScr.hpPotion < 10) || (num < 20 && GameCanvas.gameTick % 100 == 0))
				{
					Service.gI().openMenu(4);
					Service.gI().confirmMenu(4, 0);
				}
			}
		}
		public static void xd()
		{
			if (Mod.xindau && mSystem.currentTimeMillis() - Mod.currXinDau >= 30000L)
			{
				Mod.currXinDau = mSystem.currentTimeMillis();
				Service.gI().clanMessage(1, "", -1);
			}
		}
		public static void cd()
		{
			if (Mod.chodau && mSystem.currentTimeMillis() - Mod.currChoDau >= 500L)
			{
				Mod.currChoDau = mSystem.currentTimeMillis();
				for (int i = 0; i < ClanMessage.vMessage.size(); i++)
				{
					ClanMessage clanMessage = (ClanMessage)ClanMessage.vMessage.elementAt(i);
					if (clanMessage.maxCap != 0 && clanMessage.playerName != global::Char.myCharz().cName && clanMessage.recieve != clanMessage.maxCap)
					{
						Service.gI().clanDonate(clanMessage.id);
						return;
					}
				}
			}
		}
		public static void NeBoss()
		{
			for (int i = 0; i < GameScr.vCharInMap.size(); i++)
			{
				global::Char @char = (global::Char)GameScr.vCharInMap.elementAt(i);
				char c = char.Parse(@char.cName.Substring(0, 1));
				if (@char != null && c >= 'A' && c <= 'Z' && !@char.cName.StartsWith("Đệ tử") && @char.cTypePk == 5 && !@char.isMiniPet)
				{
					Service.gI().requestChangeZone(Res.random(0, GameScr.gI().zones.Length), -1);
					return;
				}
			}
		}
		private static long lastTimeCheckLag;

		// Token: 0x040012F2 RID: 4850
		public static int currentCheckLag = 30;

		public static void CheckLag()
		{
			if (mSystem.currentTimeMillis() - Mod.lastTimeCheckLag >= 1000L && GameCanvas.currentScreen == GameScr.instance)
			{
				Mod.currentCheckLag--;
				if (Mod.currentCheckLag <= 0)
				{
					Session_ME.gI().close();
					Session_ME2.gI().close();
					GameCanvas.gI().onDisconnected();
					Mod.currentCheckLag = 30;
				}
				Mod.lastTimeCheckLag = mSystem.currentTimeMillis();
			}
		}
		public static bool isGMT;
		public static global::Char charMT;
		public static void gmt()
		{
			if (Mod.isGMT && GameScr.findCharInMap(Mod.charMT.charID) != null)
			{
				global::Char.myCharz().focusManualTo(GameScr.findCharInMap(Mod.charMT.charID));
			}
		}
		public static bool isAK;
		private static int getSkill()
		{
			for (int i = 0; i < GameScr.keySkill.Length; i++)
			{
				if (GameScr.keySkill[i] == global::Char.myCharz().myskill)
				{
					return i;
				}
			}
			return 0;
		}
		public static long getTimeSkill(Skill s)
		{
			long result;
			if (s.template.id == 29 || s.template.id == 22 || s.template.id == 7 || s.template.id == 18 || s.template.id == 23)
			{
				result = (long)s.coolDown + 500L;
			}
			else
			{
				long num = (long)((double)s.coolDown * 1.2);
				if (num < 406L)
				{
					result = 406L;
				}
				else
				{
					result = num;
				}
			}
			return result;
		}
		public static long[] currTimeAK = new long[8];
		public static void Ak()
		{
			if (!global::Char.myCharz().stone && !global::Char.isLoadingMap && !global::Char.myCharz().meDead && global::Char.myCharz().statusMe != 14 && global::Char.myCharz().statusMe != 5 && global::Char.myCharz().myskill.template.type != 3 && global::Char.myCharz().myskill.template.id != 10 && global::Char.myCharz().myskill.template.id != 11 && !global::Char.myCharz().myskill.paintCanNotUseSkill)
			{
				int skill = Mod.getSkill();
				if (mSystem.currentTimeMillis() - Mod.currTimeAK[skill] > Mod.getTimeSkill(global::Char.myCharz().myskill))
				{
					if (GameScr.gI().isMeCanAttackMob(global::Char.myCharz().mobFocus) && (double)Res.abs(global::Char.myCharz().mobFocus.xFirst - global::Char.myCharz().cx) < (double)global::Char.myCharz().myskill.dx * 1.5)
					{
						global::Char.myCharz().myskill.lastTimeUseThisSkill = mSystem.currentTimeMillis();
						Mod.AkMob();
						Mod.currTimeAK[skill] = mSystem.currentTimeMillis();
						return;
					}
					if (global::Char.myCharz().charFocus != null && global::Char.myCharz().isMeCanAttackOtherPlayer(global::Char.myCharz().charFocus) && (double)Res.abs(global::Char.myCharz().charFocus.cx - global::Char.myCharz().cx) < (double)global::Char.myCharz().myskill.dx * 1.5)
					{
						global::Char.myCharz().myskill.lastTimeUseThisSkill = mSystem.currentTimeMillis();
						Mod.AkChar();
						Mod.currTimeAK[skill] = mSystem.currentTimeMillis();
					}
				}
			}
		}
		public static void AkChar()
		{
			try
			{
				MyVector myVector = new MyVector();
				myVector.addElement(global::Char.myCharz().charFocus);
				Service.gI().sendPlayerAttack(new MyVector(), myVector, 2);
				global::Char.myCharz().cMP -= global::Char.myCharz().myskill.manaUse;
			}
			catch
			{
			}
		}

		// Token: 0x060009DA RID: 2522 RVA: 0x0009F7D4 File Offset: 0x0009D9D4
		public static void AkMob()
		{
			try
			{
				MyVector myVector = new MyVector();
				myVector.addElement(global::Char.myCharz().mobFocus);
				Service.gI().sendPlayerAttack(myVector, new MyVector(), 1);
				global::Char.myCharz().cMP -= global::Char.myCharz().myskill.manaUse;
			}
			catch
			{
			}
		}
		public static bool isAutoAnNho;

		// Token: 0x040012F7 RID: 4855
		private static long currAnNho;
		public static void AnNho()
		{
			try
			{
				for (int i = 0; i < global::Char.myCharz().arrItemBag.Length; i++)
				{
					if (global::Char.myCharz().arrItemBag[i] != null && global::Char.myCharz().arrItemBag[i].template.id == 212)
					{
						Service.gI().useItem(0, 1, (sbyte)i, -1);
						break;
					}
				}
			}
			catch
			{
				try
				{
					for (int j = 0; j < global::Char.myCharz().arrItemBag.Length; j++)
					{
						if (global::Char.myCharz().arrItemBag[j] != null && global::Char.myCharz().arrItemBag[j].template.id == 211)
						{
							Service.gI().useItem(0, 1, (sbyte)j, -1);
							break;
						}
					}
				}
				catch
				{
					GameScr.info1.addInfo("Không có nho trong balo", 0);
				}
			}
		}
		public static bool isThuongDeThuong;
		public static string textAutoChat = string.Empty;
		public static bool isAutoCTG;
		public static void AutoCTG()
		{
			if (Mod.isAutoCTG && mSystem.currentTimeMillis() - Mod.currAutoCTG >= 5000L)
			{
				Mod.currAutoCTG = mSystem.currentTimeMillis();
				if (!string.IsNullOrEmpty(Mod.textAutoChatTG))
				{
					Service.gI().chatGlobal(Mod.textAutoChatTG);
					return;
				}
				GameScr.info1.addInfo("Chưa cài nội dung auto chat thế giới", 0);
			}
		}
		public static bool isVaoKhu;
		public static bool canUpdate = false;
		public static bool isNhanVang;

		// Token: 0x04001280 RID: 4736
		public static bool isDapDo;

		// Token: 0x04001281 RID: 4737
		public static Item doDeDap;

		// Token: 0x04001282 RID: 4738
		public static int soSaoCanDap = -1;

		// Token: 0x04001283 RID: 4739
		private static int saoHienTai = -1;
		public static void VaoKhu(object okhu)
		{
			try
			{
				Mod.isVaoKhu = true;
				int zoneID = TileMap.zoneID;
				int mapID = TileMap.mapID;
				int num = (int)okhu;
				if (Mod.isVaoKhu)
				{
					GameScr.info1.addInfo("Vào Khu: " + num, 0);
				}
				while (TileMap.zoneID == zoneID && TileMap.mapID == mapID && TileMap.zoneID != num)
				{
					if (Input.GetKey("q"))
					{
						GameScr.info1.addInfo("Đã dừng auto vào khu", 0);
						Mod.isVaoKhu = false;
						return;
					}
					if (!Mod.isVaoKhu)
					{
						return;
					}
					if (GameScr.gI().numPlayer[num] < GameScr.gI().maxPlayer[num])
					{
						Service.gI().requestChangeZone(num, -1);
					}
					Thread.Sleep(1);
				}
				Mod.isVaoKhu = false;
			}
			catch (Exception)
			{
				Mod.isVaoKhu = false;
			}
		}
		// Token: 0x0400128D RID: 4749
		private static long currAutoCTG;

		// Token: 0x0400128E RID: 4750
		public static string textAutoChatTG = string.Empty;
		public static void AutoChat()
		{
			if (!string.IsNullOrEmpty(Mod.textAutoChat))
			{
				Service.gI().chat(Mod.textAutoChat);
				return;
			}
			GameScr.info1.addInfo("Chưa cài nội dung auto chat", 0);
		}
		// Token: 0x040012C2 RID: 4802
		public static bool isThuongDeVip;

		// Token: 0x040012C3 RID: 4803
		private static long currThuongDe;

		// Token: 0x040012C4 RID: 4804
		public static bool isPaintCrackBall;
		public static int khuVeLai;
		public static int dichLenXuongTraiPhai;
		public static void chat(string text) {
			//string text2 = GameCanvas.inputDlg.tfInput.getText();
			if (text == "info")
            {
                GameCanvas.startOKDlg("Thông tin người chơi: " + Char.myCharz().cName + "\n" + "HP: " + NinjaUtil.getMoneys(Char.myCharz().cHP) + "/" + NinjaUtil.getMoneys(Char.myCharz().cHPFull) + "\n" + "MP: " + NinjaUtil.getMoneys(Char.myCharz().cMP) + "/" + NinjaUtil.getMoneys(Char.myCharz().cMPFull) + "\n" + "Damage: " + NinjaUtil.getMoneys(Char.myCharz().cDamFull));
            }
			if (text == "ht")
			{
				Mod.BongTai();
				text = "";
			}
			if (text == "attnl")
			{
				Mod.isAutoTTNL = !Mod.isAutoTTNL;
				GameScr.info1.addInfo("Auto TTNL: " + (Mod.isAutoTTNL ? "Bật" : "Tắt"), 0);
				text = "";
			}
			if (text == "abfdt")
			{
				Mod.aDauDeTu = !Mod.aDauDeTu;
				GameScr.info1.addInfo("Auto buff đậu theo chỉ số đệ tử: " + (Mod.aDauDeTu ? "Bật" : "Tắt"), 0);
				text = "";
			}
			if (text.StartsWith("bhpdt "))
			{
				Mod.csHPDeTu = int.Parse(text.Split(new char[]
				{
				' '
				})[1]);
				GameScr.info1.addInfo("HP buff đậu đệ tử: " + NinjaUtil.getMoneys((long)Mod.csHPDeTu), 0);
				text = "";
			}
			if (text.StartsWith("bkidt "))
			{
				Mod.csKIDeTu = int.Parse(text.Split(new char[]
				{
				' '
				})[1]);
				GameScr.info1.addInfo("KI buff đậu đệ tử: " + NinjaUtil.getMoneys((long)Mod.csKIDeTu), 0);
				text = "";
			}
			if (text == "abf")
			{
				Mod.aBuffDau = !Mod.aBuffDau;
				GameScr.info1.addInfo("Auto buff đậu theo chỉ số: " + (Mod.aBuffDau ? "Bật" : "Tắt"), 0);
				text = "";
			}
			if (text.StartsWith("bhp "))
			{
				Mod.csHP = int.Parse(text.Split(new char[]
				{
				' '
				})[1]);
				GameScr.info1.addInfo("HP buff đậu: " + NinjaUtil.getMoneys((long)Mod.csHP), 0);
				text = "";
			}
			if (text.StartsWith("bki "))
			{
				Mod.csKI = int.Parse(text.Split(new char[]
				{
				' '
				})[1]);
				GameScr.info1.addInfo("KI buff đậu: " + NinjaUtil.getMoneys((long)Mod.csKI), 0);
				text = "";
			}
			if (text == "akhu")
			{
				Mod.isAutoVeKhu = !Mod.isAutoVeKhu;
				GameScr.info1.addInfo((Mod.isAutoVeKhu ? "Auto về khu cũ khi Login: Bật" : "Auto về khu cũ khi Login: Tắt") ?? "", 0);
				text = "";
			}
			if (text == "alogin")
			{
				Mod.isAutoLogin = !Mod.isAutoLogin;
				GameScr.info1.addInfo("Auto Login: " + (Mod.isAutoLogin ? "Bật" : "Tắt"), 0);
				text = "";
			}
			if (text == "kk")
			{
				Mod.khoakhu = !Mod.khoakhu;
				GameScr.info1.addInfo("Khóa chuyển khu: " + (Mod.khoakhu ? "Bật" : "Tắt"), 0);
				text = "";
			}
			if (text == "kmap")
			{
				Mod.khoamap = !Mod.khoamap;
				GameScr.info1.addInfo("Khóa map: " + (Mod.khoamap ? "Bật" : "Tắt"), 0);
				text = "";
			}
			if (text == "tdthuong")
			{
				Mod.isPaintCrackBall = false;
				Mod.isThuongDeThuong = !Mod.isThuongDeThuong;
				GameScr.info1.addInfo("Auto Quay Thượng Đế Thường : " + (Mod.isThuongDeThuong ? "Bật" : "Tắt"), 0);
				text = "";
			}
			if (text == "tdvip")
			{
				Mod.isPaintCrackBall = false;
				Mod.isThuongDeVip = !Mod.isThuongDeVip;
				GameScr.info1.addInfo("Auto Quay Thượng Đế Vip : " + (Mod.isThuongDeVip ? "Bật" : "Tắt"), 0);
				text = "";
			}
			if (text == "tgt")
			{
				Mod.isUpdateKhu = !Mod.isUpdateKhu;
				GameScr.info1.addInfo("Update khu theo thời gian thực: " + (Mod.isUpdateKhu ? "Bật" : "Tắt"), 0);
				text = "";
			}
			if (text == "fcb")
			{
				Mod.focusBoss = !Mod.focusBoss;
				GameScr.info1.addInfo("Auto chỉ vào boss: " + (Mod.focusBoss ? "Bật" : "Tắt"), 0);
				text = "";
			}
			if (text == "nmt")
			{
				if (Mod.getX(0) > 0 && Mod.getY(0) > 0)
				{
					Mod.GotoXY(Mod.getX(0), Mod.getY(0));
				}
				else
				{
					Mod.GotoXY(30, PickMobController.GetYsd(30));
				}
				text = "";
			}
			if (text == "nmp")
			{
				if (Mod.getX(2) > 0 && Mod.getY(2) > 0)
				{
					Mod.GotoXY(Mod.getX(2), Mod.getY(2));
				}
				else
				{
					Mod.GotoXY(TileMap.pxw - 30, PickMobController.GetYsd(TileMap.pxw - 30));
				}
				text = "";
			}
			if (text == "nmg")
			{
				if (Mod.getX(1) > 0 && Mod.getY(1) > 0)
				{
					Mod.GotoXY(Mod.getX(1), Mod.getY(1));
					Service.gI().getMapOffline();
					Service.gI().requestChangeMap();
				}
				else
				{
					Mod.GotoXY(TileMap.pxw / 2, PickMobController.GetYsd(TileMap.pxw / 2));
				}
				text = "";
			}
			if (text == "nmtr")
			{
				if (Mod.getX(3) > 0 && Mod.getY(3) > 0)
				{
					Mod.GotoXY(Mod.getX(3), Mod.getY(3));
				}
				text = "";
			}
			if (text.StartsWith("do "))
			{
				Mod.bossCanDo = text.Replace("do ", "");
				GameScr.info1.addInfo("Boss cần dò: " + Mod.bossCanDo, 0);
				text = "";
			}
			if (text.StartsWith("dk "))
			{
				Mod.zoneMacDinh = int.Parse(text.Replace("dk ", ""));
				GameScr.info1.addInfo("Dò boss từ khu " + Mod.zoneMacDinh, 0);
				text = "";
			}
			if (text == "clrz")
			{
				Mod.zoneMacDinh = 0;
				GameScr.info1.addInfo("Reset khu dò boss xuống", 0);
				text = "";
			}
			if (text == "doall")
			{
				Mod.doBoss = !Mod.doBoss;
				GameScr.info1.addInfo("Dò boss: " + (Mod.doBoss ? "Bật" : "Tắt"), 0);
				text = "";
			}
			if (text == "ahs")
			{
				Mod.hoiSinhNgoc = !Mod.hoiSinhNgoc;
				GameScr.info1.addInfo((Mod.hoiSinhNgoc ? "Auto hồi sinh bằng số ngọc được chỉ định: Bật" : "Auto hồi sinh bằng số ngọc được chỉ định: Tắt") ?? "", 0);
				text = "";
			}
			if (text.StartsWith("ngochs "))
			{
				Mod.ngocHienTai = global::Char.myCharz().luongKhoa + global::Char.myCharz().luong;
				Mod.ngocDuocDungDeHoiSinh = int.Parse(text.Replace("ngochs ", ""));
				GameScr.info1.addInfo("Ngọc được sử dụng để hồi sinh là " + Mod.ngocDuocDungDeHoiSinh, 0);
				text = "";
			}
			if (text == "ksbs5")
			{
				Mod.isKSBoss = false;
				Mod.isKSBossBangSkill5 = !Mod.isKSBossBangSkill5;
				GameScr.info1.addInfo((Mod.isKSBossBangSkill5 ? "KS Boss Bằng Skill 5: Bật" : "KS Boss Bằng Skill 5: Tắt") ?? "", 0);
				text = "";
			}
			if (text == "ksb")
			{
				Mod.isKSBossBangSkill5 = false;
				Mod.isKSBoss = !Mod.isKSBoss;
				GameScr.info1.addInfo((Mod.isKSBoss ? "KS Boss bằng đấm thường: Bật" : "KS Boss bằng đấm thường: Tắt") ?? "", 0);
				text = "";
			}
			if (text.StartsWith("hpboss "))
			{
				Mod.HPKSBoss = int.Parse(text.Replace("hpboss ", ""));
				GameScr.info1.addInfo("HP Boss khi đạt " + NinjaUtil.getMoneys((long)Mod.HPKSBoss) + " sẽ oánh bỏ con mẹ boss", 0);
				text = "";
			}
			if (text == "ttsp")
			{
				Mod.isThongTinSuPhu = !Mod.isThongTinSuPhu;
				GameScr.info1.addInfo("Thông Tin Sư Phụ: " + (Mod.isThongTinSuPhu ? "Bật" : "Tắt"), 0);
				text = "";
			}
			if (text == "ttdt")
			{
				Mod.isThongTinDeTu = !Mod.isThongTinDeTu;
				GameScr.info1.addInfo("Thông Tin Đệ Tử: " + (Mod.isThongTinDeTu ? "Bật" : "Tắt"), 0);
				text = "";
			}
			if (text == "xtb")
			{
				Mod.xoaTauBay = !Mod.xoaTauBay;
				GameScr.info1.addInfo("Xóa tàu bay: " + (Mod.xoaTauBay ? "Tắt" : "Bật"), 0);
				text = "";
			}
			if (text == "xht")
			{
				Mod.xoaHieuUngHopThe = !Mod.xoaHieuUngHopThe;
				GameScr.info1.addInfo("Hiệu ứng hợp thể: " + (Mod.xoaHieuUngHopThe ? "Bật" : "Tắt"), 0);
				text = "";
			}
			if (text == "kvt")
			{
				Mod.ghimX = global::Char.myCharz().cx;
				Mod.ghimY = global::Char.myCharz().cy;
				Mod.isKhoaViTri = !Mod.isKhoaViTri;
				GameScr.info1.addInfo("Khóa vị trí: " + (Mod.isKhoaViTri ? "Bật" : "Tắt"), 0);
				text = "";
			}
			if (text.StartsWith("l "))
			{
				Mod.dichLenXuongTraiPhai = int.Parse(text.Replace("l ", ""));
				global::Char.myCharz().cx -= Mod.dichLenXuongTraiPhai;
				Service.gI().charMove();
				GameScr.info1.addInfo("Dịch trái " + Mod.dichLenXuongTraiPhai, 0);
				text = "";
			}
			if (text.StartsWith("r "))
			{
				Mod.dichLenXuongTraiPhai = int.Parse(text.Replace("r ", ""));
				global::Char.myCharz().cx += Mod.dichLenXuongTraiPhai;
				Service.gI().charMove();
				GameScr.info1.addInfo("Dịch phải " + Mod.dichLenXuongTraiPhai, 0);
				text = "";
			}
			if (text.StartsWith("u "))
			{
				Mod.dichLenXuongTraiPhai = int.Parse(text.Replace("u ", ""));
				global::Char.myCharz().cy -= Mod.dichLenXuongTraiPhai;
				Service.gI().charMove();
				GameScr.info1.addInfo("Khinh công " + Mod.dichLenXuongTraiPhai, 0);
				text = "";
			}
			if (text.StartsWith("d "))
			{
				Mod.dichLenXuongTraiPhai = int.Parse(text.Replace("d ", ""));
				global::Char.myCharz().cy += Mod.dichLenXuongTraiPhai;
				Service.gI().charMove();
				GameScr.info1.addInfo("Đi vào lòng đất " + Mod.dichLenXuongTraiPhai, 0);
				text = "";
			}
			if (text == "line")
			{
				Mod.lineboss = !Mod.lineboss;
				GameScr.info1.addInfo("Đường kẻ tới boss: " + (Mod.lineboss ? "Bật" : "Tắt"), 0);
				text = "";
			}
			

			if (text == "ttnv")
			{
				Mod.isBossM = false;
				Mod.isPKM = false;
				Mod.trangThai = !Mod.trangThai;
				GameScr.info1.addInfo("Trạng thái nhân vật đang trỏ: " + (Mod.trangThai ? "Bật" : "Tắt"), 0);
				text = "";
			}
			
			if (text == "pkm")
			{
				Mod.isPKM = !Mod.isPKM;
				Mod.isBossM = false;
				Mod.trangThai = false;
				GameScr.info1.addInfo("Bọn đấm nhau được trong khu: " + (Mod.isPKM ? "Bật" : "Tắt"), 0);
				text = "";
			}
			if (text == "gdl")
			{
				Mod.giamDungLuong = !Mod.giamDungLuong;
				GameScr.info1.addInfo("Giảm dung lượng: " + (Mod.giamDungLuong ? "Bật" : "Tắt"), 0);
				text = "";
			}
			if (text == "xoamap")
			{
				Mod.xoamap = !Mod.xoamap;
				GameScr.info1.addInfo("Xóa map: " + (Mod.xoamap ? "Bật" : "Tắt"), 0);
				text = "";
			}
			if (text == "gmt")
			{
				Mod.isGMT = false;
				text = "";
			}
			if (text.StartsWith("gmt "))
			{
				int num = int.Parse(text.Remove(0, 4));
				if (num < GameScr.vCharInMap.size())
				{
					Mod.isGMT = true;
					Mod.charMT = (global::Char)GameScr.vCharInMap.elementAt(num);
				}
				text = "";
			}
			if (text == "abt")
			{
				Mod.isAutoBT = !Mod.isAutoBT;
				GameScr.info1.addInfo("Auto bông tai: " + (Mod.isAutoBT ? "Bật" : "Tắt"), 0);
				text = "";
			}
			if (text.StartsWith("bt "))
			{
				Mod.timeBT = int.Parse(text.Replace("bt ", ""));
				GameScr.info1.addInfo("Delay auto bông tai: " + Mod.timeBT + "s", 0);
				text = "";
			}
			if (text == "anz")
			{
				Mod.isAutoNhatXa = !Mod.isAutoNhatXa;
				if (Mod.isAutoNhatXa)
				{
					Mod.xNhatXa = global::Char.myCharz().cx;
					Mod.yNhatXa = global::Char.myCharz().cy;
					GameScr.info1.addInfo(string.Concat(new object[]
					{
					"Tọa Độ : ",
					global::Char.myCharz().cx,
					"|",
					global::Char.myCharz().cy
					}), 0);
				}
				GameScr.info1.addInfo("Auto Nhặt Xa : " + (Mod.isAutoNhatXa ? "Bật" : "Tắt"), 0);
				text = "";
			}
			if (text == "ak")
			{
				Mod.isAK = !Mod.isAK;
				GameScr.info1.addInfo("Auto đánh: " + (Mod.isAK ? "Bật" : "Tắt"), 0);
				text = "";
			}
			if (text.StartsWith("ndc "))
			{
				Mod.textAutoChat = text.Replace("ndc ", "");
				GameScr.info1.addInfo("Nội dung auto chat : " + Mod.textAutoChat, 0);
				text = "";
			}
			if (text.StartsWith("ndctg "))
			{
				Mod.textAutoChatTG = text.Replace("ndc ", "");
				GameScr.info1.addInfo("Nội dung auto chat thế giới : " + Mod.textAutoChatTG, 0);
				text = "";
			}
			if (text == "atchattg")
			{
				Mod.isAutoCTG = !Mod.isAutoCTG;
				GameScr.info1.addInfo("Auto Chat Thế Giới: " + (Mod.isAutoCTG ? "Bật" : "Tắt"), 0);
				text = "";
			}
			if (text == "atc")
			{
				Mod.achat = !Mod.achat;
				GameScr.info1.addInfo("Auto chat : " + (Mod.achat ? "Bật" : "Tắt"), 0);
				text = string.Empty;
			}
			if (text.StartsWith("go "))
			{
				int num2 = int.Parse(text.Remove(0, 3));
				if (num2 < GameScr.vCharInMap.size())
				{
					global::Char @char = (global::Char)GameScr.vCharInMap.elementAt(num2);
					Mod.GotoXY(@char.cx, @char.cy);
					global::Char.myCharz().focusManualTo(@char);
				}
				text = "";
			}
			if (text == "showhp")
			{
				Mod.nvat = !Mod.nvat;
				GameScr.info1.addInfo("Thông tin người chơi trong map: " + (Mod.nvat ? "Bật" : "Tắt"), 0);
				text = "";
			}
			if (text.StartsWith("kx "))
			{
				new Thread(new ParameterizedThreadStart(Mod.VaoKhu)).Start(int.Parse(text.Remove(0, 3)));
				text = "";
			}
			if (text.StartsWith("k "))
			{
				Service.gI().requestChangeZone(int.Parse(text.Replace("k ", "")), -1);
				text = "";
			}
			if (text.StartsWith("tdc "))
			{
				Mod.tocdochay = int.Parse(text.Replace("tdc ", ""));
				GameScr.info1.addInfo("Tốc độ phóng: " + Mod.tocdochay, 0);
				text = "";
			}
			if (text == "dapdo")
			{
				Mod.isDapDo = !Mod.isDapDo;
				new Thread(new ThreadStart(Mod.AutoDapDo)).Start();
				GameScr.info1.addInfo("Đập đồ: " + (Mod.isDapDo ? "Bật" : "Tắt"), 0);
				text = "";
			}
			if (text.StartsWith("speed "))
			{
				Time.timeScale = float.Parse(text.Remove(0, 6));
				GameScr.info1.addInfo("Tốc Độ Game: " + Time.timeScale, 0);
				text = "";
			}
		}
		public static void AutoDapDo()
		{
			while (Mod.isDapDo)
			{
				if (Input.GetKey("q"))
				{
					GameScr.info1.addInfo("Đồ để đập đã reset hãy add đồ", 0);
					Mod.soSaoCanDap = -1;
					Mod.doDeDap = null;
				}
				if (TileMap.mapID != 5 && !Xmap.isXmaping)
				{
					Xmap.StartRunToMapId(5);
				}
				while (TileMap.mapID != 5)
				{
					Thread.Sleep(100);
				}
				if (Mod.saoHienTai >= Mod.soSaoCanDap && Mod.doDeDap != null && Mod.saoHienTai >= 0 && Mod.soSaoCanDap > 0)
				{
					Sound.start(1f, Sound.l1);
					GameScr.info1.addInfo("Đồ Cần Đập Đã Đạt Số Sao Yêu Cầu", 0);
					Mod.soSaoCanDap = -1;
					Mod.doDeDap = null;
				}
				if (global::Char.myCharz().xu > 200000000L)
				{
					long xu = global::Char.myCharz().xu;
					Mod.GotoNpc(21);
					if (Mod.doDeDap != null && Mod.soSaoCanDap > 0)
					{
						while (!GameCanvas.menu.showMenu)
						{
							Service.gI().combine(1, GameCanvas.panel.vItemCombine);
							Thread.Sleep(100);
						}
						Service.gI().confirmMenu(21, 0);
						GameCanvas.menu.doCloseMenu();
						GameCanvas.panel.currItem = null;
						GameCanvas.panel.chatTField.isShow = false;
					}
				}
				else if (Mod.doDeDap != null)
				{
					Mod.BanVang();
				}
				Thread.Sleep(100);
			}
		}
		public static bool aDauDeTu;
		public static bool xoaHieuUngHopThe;
		// Token: 0x040012D7 RID: 4823
		private static long currDauDeTu;
		public static int csHPDeTu;
		public static int csHP;
		public static bool isUpdateKhu = false;
		// Token: 0x040012D3 RID: 4819
		public static int csKI;
		public static bool isKSBoss;

		// Token: 0x040012B0 RID: 4784
		public static int HPKSBoss = 0;

		// Token: 0x040012B1 RID: 4785
		public static bool isKSBossBangSkill5;
		private static long currBuffDau;
		// Token: 0x040012D9 RID: 4825
		public static int csKIDeTu;
		public static bool aBuffDau;
		public static bool dangNhanAll;
		public static void BanVang()
		{
			Mod.dangBanVang = true;
			if (TileMap.mapID != 5)
			{
				Xmap.StartRunToMapId(5);
				Thread.Sleep(1000);
			}
			while (TileMap.mapID != 5)
			{
				Thread.Sleep(500);
			}
			if (Input.GetKey("q"))
			{
				GameScr.info1.addInfo("Dừng bán vàng", 0);
				Mod.dangBanVang = false;
				return;
			}
			while (global::Char.myCharz().xu <= 1500000000L && !Input.GetKey("q"))
			{
				if (Mod.thoiVang() <= 0)
				{
					GameScr.info1.addInfo("Hết vàng", 0);
					if (Mod.isDapDo)
					{
						Mod.isDapDo = false;
						GameScr.info1.addInfo("Đập đồ đã tắt do bạn quá nghèo :v", 0);
					}
					Mod.dangBanVang = false;
					return;
				}
				Service.gI().saleItem(1, 1, (short)Mod.FindIndexItem(457));
				Thread.Sleep(500);
				Thread.Sleep(500);
			}
			GameScr.info1.addInfo("Đã bán xong", 0);
			Thread.Sleep(500);
			Mod.dangBanVang = false;
		}
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
		private static bool dangBanVang;
		public static int thoiVang()
		{
			int num = 0;
			for (int i = 0; i < global::Char.myCharz().arrItemBag.Length; i++)
			{
				Item item = global::Char.myCharz().arrItemBag[i];
				if (item != null && item.template.id == 457)
				{
					num += item.quantity;
				}
			}
			return num;
		}
		// Token: 0x040012C6 RID: 4806
		private static bool dangMoMenu;
		public static global::Char BossInMap()
		{
			for (int i = 0; i < GameScr.vCharInMap.size(); i++)
			{
				global::Char @char = (global::Char)GameScr.vCharInMap.elementAt(i);
				if (@char.cTypePk == 5 && @char != null && @char.cHP > 0 && global::Char.myCharz().isMeCanAttackOtherPlayer(@char))
				{
					return @char;
				}
			}
			return null;
		}
		public static void KSBossBangSkill5()
		{
			if (Mod.isKSBossBangSkill5)
			{
				global::Char @char = Mod.BossInMap();
				if ((!global::Char.myCharz().isStandAndCharge && !global::Char.myCharz().isCharge && !global::Char.myCharz().isFlyAndCharge && Mod.GetCoolDownSkill(Mod.GetSkillByID(Mod.ulti())) <= 0) || Mod.GetSkillByID(Mod.ulti()) != null || @char != null)
				{
					if (Mod.HPKSBoss == 0)
					{
						Mod.isKSBossBangSkill5 = false;
						GameScr.info1.addInfo("HP Boss = 0 thì ks sao ba =))", 0);
						GameScr.info1.addInfo("KS Boss đã tắt", 0);
						return;
					}
					if ((@char.cHP <= Mod.HPKSBoss || (Mod.DameToBoss() + global::Char.myCharz().cHPFull) / 2 >= @char.cHP) && Mod.GetSkillByID(Mod.ulti()) != null)
					{
						if (global::Math.abs(@char.cx - global::Char.myCharz().cx) >= 500)
						{
							Mod.GotoXY(@char.cx, @char.cy - 1);
						}
						global::Char.myCharz().focusManualTo(@char);
						Mod.UseSkill(Mod.GetSkillByID(Mod.ulti()));
					}
				}
			}
		}
		private static sbyte ulti()
		{
			sbyte result;
			if (global::Char.myCharz().cgender == 0)
			{
				result = 10;
			}
			else if (global::Char.myCharz().cgender == 1)
			{
				result = 11;
			}
			else
			{
				result = 14;
			}
			return result;
		}
		public static Skill GetSkillByID(sbyte idSkill)
		{
			for (int i = 0; i < GameScr.keySkill.Length; i++)
			{
				if (GameScr.keySkill[i] != null && GameScr.keySkill[i].template.id == idSkill)
				{
					return GameScr.keySkill[i];
				}
			}
			return null;
		}
		private static int DameToBoss()
		{
			int num = -global::Char.myCharz().cHPFull;
			for (int i = 0; i < GameScr.vCharInMap.size(); i++)
			{
				global::Char @char = (global::Char)GameScr.vCharInMap.elementAt(i);
				if (@char != null && @char.isStandAndCharge && global::Char.myCharz().cgender == 2 && Mod.BossInMap() != null)
				{
					if (num < 0)
					{
						num = 0;
					}
					num += @char.cHPFull;
				}
			}
			return num;
		}
		public static void KSBoss()
		{
			if (Mod.isKSBoss)
			{
				for (int i = 0; i < GameScr.vCharInMap.size(); i++)
				{
					global::Char @char = (global::Char)GameScr.vCharInMap.elementAt(i);
					if (Mod.HPKSBoss == 0)
					{
						Mod.isKSBoss = false;
						GameScr.info1.addInfo("HP Boss = 0 thì ks sao ba =))", 0);
						GameScr.info1.addInfo("KS Boss đã tắt", 0);
						return;
					}
					if (@char != null && @char.charID < 0 && @char.cTypePk == 5 && !@char.cName.StartsWith("Đ") && @char.cHP <= Mod.HPKSBoss && @char.cHP > 0)
					{
						Mod.GetSkillByIconID(539);
						if (global::Math.abs(@char.cx - global::Char.myCharz().cx) >= 25)
						{
							Mod.GotoXY(@char.cx, @char.cy - 1);
						}
						global::Char.myCharz().focusManualTo(@char);
						Mod.Ak();
					}
				}
			}
		}
		public static void quayThuongDe()
		{
			if (!Mod.dangBanVang && !Mod.dangNhanAll && !Mod.dangMoMenu)
			{
				if (!Mod.isPaintCrackBall && TileMap.mapID == 45)
				{
					new Thread(new ThreadStart(Mod.OpenMenuThuongDe)).Start();
					return;
				}
				if (TileMap.mapID != 45 && !Xmap.isXmaping)
				{
					Xmap.StartRunToMapId(45);
					return;
				}
				if (TileMap.mapID == 45)
				{
					if (Input.GetKey("q"))
					{
						GameScr.info1.addInfo("Auto đã tắt", 0);
						Mod.isThuongDeVip = false;
						Mod.isThuongDeThuong = false;
						return;
					}
					if (global::Char.myCharz().xu <= 175000000L && Mod.isThuongDeThuong)
					{
						new Thread(new ThreadStart(Mod.BanVang)).Start();
						return;
					}
					Mod.GotoNpc(19);
					Service.gI().openMenu(19);
					Service.gI().SendCrackBall(2, 7);
				}
			}
		}
		public static void GotoNpc(int npcID)
		{
			for (int i = 0; i < GameScr.vNpc.size(); i++)
			{
				Npc npc = (Npc)GameScr.vNpc.elementAt(i);
				if (npc.template.npcTemplateId == npcID && global::Math.abs(npc.cx - global::Char.myCharz().cx) >= 50)
				{
					Mod.GotoXY(npc.cx, npc.cy - 1);
					global::Char.myCharz().focusManualTo(npc);
					return;
				}
			}
		}
		public static string bossCanDo;

		// Token: 0x040012BD RID: 4797
		private static long currDoBoss;
		public static bool doBoss;
		public static bool xoaTauBay;
		// Token: 0x040012BB RID: 4795
		public static int zoneMacDinh;
		public static void DoBoss()
		{
			if (string.IsNullOrEmpty(Mod.bossCanDo))
			{
				GameScr.info1.addInfo("Chưa nhập boss cần tìm", 0);
				Mod.zoneMacDinh = 0;
				Mod.doBoss = false;
				return;
			}
			if (Input.GetKey("q"))
			{
				GameScr.info1.addInfo("Đã tắt auto dò boss", 0);
				Mod.doBoss = false;
				return;
			}
			for (int i = 0; i < GameScr.vCharInMap.size(); i++)
			{
				global::Char @char = (global::Char)GameScr.vCharInMap.elementAt(i);
				if (@char != null && @char.cName.ToLower().Contains(Mod.bossCanDo.ToLower()) && @char.cTypePk == 5)
				{
					Sound.start(1f, Sound.l1);
					GameScr.info1.addInfo("Đã tìm thấy boss", 0);
					Mod.zoneMacDinh = 0;
					Mod.doBoss = false;
					return;
				}
			}
			Service.gI().requestChangeZone(Mod.zoneMacDinh, -1);
			if (!global::Char.isLoadingMap && TileMap.zoneID == Mod.zoneMacDinh)
			{
				Mod.zoneMacDinh++;
			}
		}
		public static void NhanAllThuongDe()
		{
			Mod.dangNhanAll = true;
			Service.gI().openMenu(19);
			Service.gI().confirmMenu(19, 1);
			Service.gI().confirmMenu(19, 3);
			Service.gI().buyItem(2, 0, 0);
			Mod.dangNhanAll = false;
		}

		// Token: 0x06000A05 RID: 2565 RVA: 0x000A0C0C File Offset: 0x0009EE0C
		private static void OpenMenuThuongDe()
		{
			Mod.dangMoMenu = true;
			Service.gI().openMenu(19);
			Service.gI().confirmMenu(19, 1);
			if (Mod.isThuongDeThuong)
			{
				Service.gI().confirmMenu(19, 1);
			}
			if (Mod.isThuongDeVip)
			{
				Service.gI().confirmMenu(19, 2);
			}
			Mod.dangMoMenu = false;
		}
		private static long currUpdateKhu;
		public static void useItem()
		{
			if (Mod.listItemAuto.Count > 0)
			{
				for (int i = 0; i < global::Char.myCharz().arrItemBag.Length; i++)
				{
					Item item = global::Char.myCharz().arrItemBag[i];
					foreach (Mod.ItemAuto itemAuto in Mod.listItemAuto)
					{
						if (item != null && (int)item.template.iconID == itemAuto.iconID && (int)item.template.id == itemAuto.id && !ItemTime.isExistItem((int)item.template.iconID))
						{
							Service.gI().useItem(0, 1, (sbyte)Mod.FindIndexItem((int)item.template.id), -1);
							break;
						}
					}
				}
			}
		}
		public static int getX(sbyte type)
		{
			int i = 0;
			while (i < TileMap.vGo.size())
			{
				Waypoint waypoint = (Waypoint)TileMap.vGo.elementAt(i);
				int result;
				if (waypoint.maxX < 60 && type == 0)
				{
					result = 15;
				}
				else if ((int)waypoint.minX <= TileMap.pxw - 60 && waypoint.maxX >= 60 && type == 1)
				{
					result = (int)((waypoint.minX + waypoint.maxX) / 2);
				}
				else
				{
					if ((int)waypoint.minX <= TileMap.pxw - 60 || type != 2)
					{
						if (type == 3)
						{
							if (waypoint.maxX < 60)
							{
								return 15;
							}
							if ((int)waypoint.minX > TileMap.pxw - 60)
							{
								return TileMap.pxw - 15;
							}
						}
						i++;
						continue;
					}
					result = TileMap.pxw - 15;
				}
				return result;
			}
			return 0;
		}

		// Token: 0x06000A01 RID: 2561 RVA: 0x000A0978 File Offset: 0x0009EB78
		public static int getY(sbyte type)
		{
			int i = 0;
			while (i < TileMap.vGo.size())
			{
				Waypoint waypoint = (Waypoint)TileMap.vGo.elementAt(i);
				int maxY;
				if (waypoint.maxX < 60 && type == 0)
				{
					maxY = (int)waypoint.maxY;
				}
				else if ((int)waypoint.minX <= TileMap.pxw - 60 && waypoint.maxX >= 60 && type == 1)
				{
					maxY = (int)waypoint.maxY;
				}
				else
				{
					if ((int)waypoint.minX <= TileMap.pxw - 60 || type != 2)
					{
						if (type == 3 && (int)waypoint.maxY != global::Char.myCharz().cy)
						{
							if (waypoint.maxX < 60)
							{
								return (int)waypoint.maxY;
							}
							if ((int)waypoint.minX > TileMap.pxw - 60)
							{
								return (int)waypoint.maxY;
							}
						}
						i++;
						continue;
					}
					maxY = (int)waypoint.maxY;
				}
				return maxY;
			}
			return 0;
		}
		public static bool achat;

		// Token: 0x0400128A RID: 4746
		private static long currAutoChat;
		public static int soSao(Item item)
		{
			for (int i = 0; i < item.itemOption.Length; i++)
			{
				if (item.itemOption[i].optionTemplate.id == 107)
				{
					return item.itemOption[i].param;
				}
			}
			return 0;
		}
		public static Item findItemBagWithIndexUI(int index)
		{
			foreach (Item item in global::Char.myCharz().arrItemBag)
			{
				if (item != null && item.indexUI == index)
				{
					return item;
				}
			}
			return null;
		}
		private static void UseSkillAuto()
		{
			if (Input.GetKey("q") && Mod.listSkillsAuto.Count > 0)
			{
				Mod.listSkillsAuto.Clear();
				GameScr.info1.addInfo("Đã reset skill auto", 0);
				return;
			}
			foreach (Skill skill in Mod.listSkillsAuto)
			{
				if (global::Char.myCharz().isStandAndCharge || global::Char.myCharz().isCharge || global::Char.myCharz().isFlyAndCharge || global::Char.myCharz().stone)
				{
					break;
				}
				if (global::Char.myCharz().holdEffID != 0)
				{
					break;
				}
				if (global::Char.myCharz().blindEff)
				{
					break;
				}
				if (global::Char.myCharz().sleepEff)
				{
					break;
				}
				if (global::Char.myCharz().cHP <= 0)
				{
					break;
				}
				if (global::Char.myCharz().statusMe == 14)
				{
					break;
				}
				if (TileMap.mapID == global::Char.myCharz().cgender + 21)
				{
					break;
				}
				if ((Mod.GetCoolDownSkill(skill) <= 0 && skill.template.type == 3) || skill.lastTimeUseThisSkill == 0L)
				{
					Mod.UseSkill(skill);
				}
			}
		}
		public static void VeKhu()
		{
			if (Mod.isAutoVeKhu)
			{
				int num = -1;
				try
				{
					num = Mod.khuVeLai;
					goto IL_20;
				}
				catch
				{
					goto IL_20;
				}
			IL_14:
				Service.gI().requestChangeZone(num, -1);
			IL_20:
				if (num >= 0 && TileMap.zoneID != num)
				{
					goto IL_14;
				}
			}
		}
	
		public static void update()
		{
			Mod.BuyItems();
			if (isbufftl)
            {
				if(Char.myPetz().cStamina <= 100)
                {
					GameScr.gI().doUseHP();
				
                }
            }
			if (GameCanvas.isPointerHoldIn(15, 15, 60, 35) && GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
			{
				MenuMod();
				
				SoundMn.gI().buttonClick();
				GameCanvas.clearAllPointerEvent();
			}

			Xmap.Update();
            global::Char.myCharz().cspeed = Mod.tocdochay;
			Pk9rPickMob.Update();
			if (Input.GetKey("q") && SpecialSkill.gI().isnoitai)
			{
				SpecialSkill.gI().isnoitai = false;
				GameScr.info1.addInfo("Đã Dừng", 0);
				if (isPaintCrackBall)
				{
					isThuongDeVip = false;
					isThuongDeVip = false;
					isThuongDeThuong = false;
					GameScr.info1.addInfo("Đã Dừng", 0);
				}
			}
			if (Mod.isDapDo && Mod.doDeDap != null)
			{
				Mod.saoHienTai = Mod.soSao(Mod.findItemBagWithIndexUI(Mod.doDeDap.indexUI));
			}
			else
			{
				Mod.saoHienTai = -1;
			}
			if (Mod.achat && mSystem.currentTimeMillis() - Mod.currAutoChat >= 2000L)
			{
				Mod.AutoChat();
				Mod.currAutoChat = mSystem.currentTimeMillis();
			}
			if (Mod.doBoss && mSystem.currentTimeMillis() - Mod.currDoBoss >= 1000L)
			{
				Mod.DoBoss();
				Mod.currDoBoss = mSystem.currentTimeMillis();
			}
			if (mSystem.currentTimeMillis() - Mod.currUpdateKhu >= 1000L)
			{
				Mod.currUpdateKhu = mSystem.currentTimeMillis();
				Mod.useItem();
				if (Mod.isUpdateKhu)
				{
					Service.gI().openUIZone();
				}
				
			}
			if (Mod.aBuffDau && mSystem.currentTimeMillis() - Mod.currBuffDau >= 1000L)
			{
				if ((Mod.csHP > global::Char.myCharz().cHP || Mod.csKI > global::Char.myCharz().cMP) && global::Char.myCharz().cHP > 0)
				{
					GameScr.gI().doUseHP();
				}
				Mod.currBuffDau = mSystem.currentTimeMillis();
			}
			if (Mod.aDauDeTu && mSystem.currentTimeMillis() - Mod.currDauDeTu >= 1000L)
			{
				if ((Mod.csHPDeTu > global::Char.myPetz().cHP || Mod.csKIDeTu > global::Char.myPetz().cMP) && global::Char.myPetz().cHP > 0 && Mod.myPetInMap() != null)
				{
					GameScr.gI().doUseHP();
				}
				Mod.currDauDeTu = mSystem.currentTimeMillis();
			}
			if ((Mod.isThuongDeThuong || Mod.isThuongDeVip) && mSystem.currentTimeMillis() - Mod.currThuongDe >= 1000L)
			{
				Mod.quayThuongDe();
				Mod.currThuongDe = mSystem.currentTimeMillis();
			}
			Mod.AutoTTNL();
			Mod.AutoHoiSinh();
			Mod.xd();
			Mod.cd();
			Mod.UseSkillAuto();
			Mod.autoFocusBoss();
			Mod.KSBoss();
			Mod.KSBossBangSkill5();
			//Mod.BuyItems();
			Mod.khoaViTri();
			Mod.gmt();
			Mod.AutoBT();
			Mod.AutoCTG();
			Mod.AutoNhatXa();
			if (Mod.hoiSinhNgoc && mSystem.currentTimeMillis() - Mod.currHoiSinh >= 1000L)
			{
				Mod.HoiSinhTheoNgocChiDinh();
				Mod.currHoiSinh = mSystem.currentTimeMillis();
			}
			if (Mod.isAutoVeKhu && mSystem.currentTimeMillis() - Mod.currVeKhuCu >= 20000L)
			{
				Mod.currVeKhuCu = mSystem.currentTimeMillis();
				Mod.khuVeLai = TileMap.zoneID;
			}
			if (Mod.isAutoAnNho && global::Char.myCharz().cStamina <= 5 && mSystem.currentTimeMillis() - Mod.currAnNho >= 1000L)
			{
				Mod.AnNho();
				Mod.currAnNho = mSystem.currentTimeMillis();
			}
			if (Mod.isAK)
			{
				Mod.Ak();
			}
			if (Mod.isPKM && !Mod.isGMT && (global::Char.myCharz().charFocus == null || (global::Char.myCharz().charFocus != null && !global::Char.myCharz().isMeCanAttackOtherPlayer(global::Char.myCharz().charFocus))))
			{
				for (int i = 0; i < GameScr.vCharInMap.size(); i++)
				{
					global::Char @char = (global::Char)GameScr.vCharInMap.elementAt(i);
					if (@char != null && global::Char.myCharz().isMeCanAttackOtherPlayer(@char) && !@char.isPet && !@char.isMiniPet && !@char.cName.StartsWith("$") && !@char.cName.StartsWith("#") && @char.charID >= 0)
					{
						global::Char.myCharz().focusManualTo(@char);
						return;
					}
				}
			}
			if (Mod.isCheckLag)
			{
				Mod.CheckLag();
			}
			if (Mod.thudau && mSystem.currentTimeMillis() - Mod.currThuDau >= 500L)
			{
				Mod.td();
				Mod.currThuDau = mSystem.currentTimeMillis();
			}
			if (Mod.isAutoNeBoss && mSystem.currentTimeMillis() - Mod.currNeBoss >= 5000L)
			{
				Mod.NeBoss();
				Mod.currNeBoss = mSystem.currentTimeMillis();
			}
			if (Mod.isAutoCo && mSystem.currentTimeMillis() - Mod.currAutoFlag >= 500L)
			{
				if (!Mod.FlagInMap() && global::Char.myCharz().cFlag == 0)
				{
					Service.gI().getFlag(1, 8);
				}
				if (Mod.FlagInMap() && global::Char.myCharz().cFlag == 8)
				{
					Service.gI().getFlag(1, 0);
				}
				Mod.currAutoFlag = mSystem.currentTimeMillis();
			}
			if (Mod.isKOK && mSystem.currentTimeMillis() - Mod.currKOK >= 500L)
			{
				Mod.currKOK = mSystem.currentTimeMillis();
				if (global::Char.myCharz().isCharge || global::Char.myCharz().isFlyAndCharge || global::Char.myCharz().isStandAndCharge || global::Char.myCharz().statusMe == 14 || global::Char.myCharz().cHP <= 0)
				{
					return;
				}
				global::Char.myCharz().cy--;
				Service.gI().charMove();
				global::Char.myCharz().cy++;
				Service.gI().charMove();
			}
		}
		public static bool isKOK;

		// Token: 0x040012F0 RID: 4848
		private static long currKOK;
		public static bool isAutoNhatXa;
		public static int xNhatXa;

		// Token: 0x04001291 RID: 4753
		public static int yNhatXa;

		// Token: 0x04001292 RID: 4754
		private static long currNhatXa;
		public static void AutoNhatXa()
		{
			if (Mod.isAutoNhatXa && mSystem.currentTimeMillis() - Mod.currNhatXa >= 2000L)
			{
				Mod.currNhatXa = mSystem.currentTimeMillis();
				for (int i = 0; i < GameScr.vItemMap.size(); i++)
				{
					ItemMap itemMap = (ItemMap)GameScr.vItemMap.elementAt(i);
					if (itemMap != null && itemMap.itemMapID == global::Char.myCharz().charID)
					{
						Mod.GotoXY(itemMap.x, itemMap.y);
						Service.gI().pickItem(itemMap.itemMapID);
						Mod.GotoXY(Mod.xNhatXa, Mod.yNhatXa);
						return;
					}
					if (itemMap != null)
					{
						Mod.GotoXY(itemMap.x, itemMap.y);
						Service.gI().pickItem(itemMap.itemMapID);
						Mod.GotoXY(Mod.xNhatXa, Mod.yNhatXa);
						return;
					}
				}
			}
		}
		public static void GotoXY(int x, int y)
		{
			global::Char.myCharz().cx = x;
			global::Char.myCharz().cy = y;
			Service.gI().charMove();
			global::Char.myCharz().cx = x;
			global::Char.myCharz().cy = y +1;
			Service.gI().charMove();
			global::Char.myCharz().cx = x;
			global::Char.myCharz().cy = y;
			Service.gI().charMove();
		}
		public static void UseItem(int templateId)
		{
			for (int i = 0; i < global::Char.myCharz().arrItemBag.Length; i++)
			{
				Item item = global::Char.myCharz().arrItemBag[i];
				if (item != null && (int)item.template.id == templateId)
				{
					Service.gI().useItem(0, 1, (sbyte)item.indexUI, -1);
					return;
				}
			}
		}
		public static bool checkClickButton = false;
		public static int countBuyItem;

		// Token: 0x040012AD RID: 4781
		public static Item itemBuy = null;

		// Token: 0x040012AE RID: 4782
		private static long currBuyItem = 0L;
		public static sbyte typeBuyItem()
		{
			sbyte result;
			if (GameCanvas.panel.currItem.buyCoin > 0)
			{
				result = 0;
			}
			else if (GameCanvas.panel.currItem.buyGold > 0)
			{
				result = 1;
			}
			else if (GameCanvas.panel.currItem.buySpec > 0)
			{
				result = 3;
			}
			else
			{
				result = -1;
			}
			return result;
		}
		public static void BuyItems()
		{
			if (Mod.itemBuy != null && Mod.countBuyItem > 0 && mSystem.currentTimeMillis() - Mod.currBuyItem >= 500L)
			{
				Mod.currBuyItem = mSystem.currentTimeMillis();
				if (Input.GetKey("q"))
				{
					Mod.countBuyItem = 0;
					Mod.itemBuy = null;
					GameScr.info1.addInfo("Dừng Mua Item", 0);
					return;
				}
				Service.gI().buyItem(Mod.typeBuyItem(), (int)Mod.itemBuy.template.id, 0);
				Mod.countBuyItem--;
				if (Mod.countBuyItem == 0)
				{
					GameScr.info1.addInfo("Đã Mua Xong !", 0);
					Mod.itemBuy = null;
				}
			}
		}
		private static bool isbufftl;
		private static string ListMap = "Làng Aru,Đồi hoa cúc,Thung lũng tre,Rừng nấm,Rừng xương,Đảo Kamê,Đông Karin,Làng Mori,Đồi nấm tím,Thị trấn Moori,Thung lũng Namếc,Thung lũng Maima,Vực maima,Đảo Guru,Làng Kakarot,Đồi hoang,Làng Plant,Rừng nguyên sinh,Rừng thông Xayda,Thành phố Vegeta,Vách núi đen,Nhà Gôhan,Nhà Moori,Nhà Broly,Trạm tàu vũ trụ,Trạm tàu vũ trụ,Trạm tàu vũ trụ,Rừng Bamboo,Rừng dương xỉ,Nam Kamê,Đảo Bulông,Núi hoa vàng,Núi hoa tím,Nam Guru,Đông Nam Guru,Rừng cọ,Rừng đá,Thung lũng đen,Bờ vực đen,Vách núi Aru,Vách núi Moori,Vực Plant,Vách núi Aru,Vách núi Moori,Vách núi Kakarot,Thần điện,Tháp Karin,Rừng Karin,Hành tinh Kaio,Phòng tập thời gian,Thánh địa Kaio,Đấu trường,Đại hội võ thuật,Tường thành 1,Tầng 3,Tầng 1,Tầng 2,Tầng 4,Tường thành 2,Tường thành 3,Trại độc nhãn 1,Trại độc nhãn 2,Trại độc nhãn 3,Trại lính Fide,Núi dây leo,Núi cây quỷ,Trại qủy già,Vực chết,Thung lũng Nappa,Vực cấm,Núi Appule,Căn cứ Raspberry,Thung lũng Raspberry,Thung lũng chết,Đồi cây Fide,Khe núi tử thần,Núi đá,Rừng đá,Lãnh  địa Fize,Núi khỉ đỏ,Núi khỉ vàng,Hang quỷ chim,Núi khỉ đen,Hang khỉ đen,Siêu Thị,Hành tinh M-2,Hành tinh Polaris,Hành tinh Cretaceous,Hành tinh Monmaasu,Hành tinh Rudeeze,Hành tinh Gelbo,Hành tinh Tigere,Thành phố phía đông,Thành phố phía nam,Đảo Balê,95,Cao nguyên,Thành phố phía bắc,Ngọn núi phía bắc,Thung lũng phía bắc,Thị trấn Ginder,101,Nhà Bunma,Võ đài Xên bọ hung,Sân sau siêu thị,Cánh đồng tuyết,Rừng tuyết,Núi tuyết,Dòng sông băng,Rừng băng,Hang băng,Đông Nam Karin,Võ đài Hạt Mít,Đại hội võ thuật,Cổng phi thuyền,Phòng chờ,Thánh địa Kaio,Cửa Ải 1,Cửa Ải 2,Cửa Ải 3,Phòng chỉ huy,Đấu trường,Ngũ Hành Sơn,Ngũ Hành Sơn,Ngũ Hành Sơn,Võ đài Bang,Thành phố Santa,Cổng phi thuyền,Bụng Mabư,Đại hội võ thuật,Đại hội võ thuật Vũ Trụ,Hành Tinh Yardart,Hành Tinh Yardart 2,Hành Tinh Yardart 3,Đại hội võ thuật Vũ Trụ 6-7,Động hải tặc,Hang Bạch Tuộc,Động kho báu,Cảng hải tặc,Hành tinh Potaufeu,Hang động Potaufeu,Con đường rắn độc,Con đường rắn độc,Con đường rắn độc,Hoang mạc,Võ Đài Siêu Cấp,Tây Karin,Sa mạc,Lâu đài Lychee,Thành phố Santa,Lôi Đài,Hành tinh bóng tối,Vùng đất băng giá,Lãnh địa bang hội,Hành tinh Bill,Hành tinh ngục tù,Tây thánh địa,Đông thánh Địa,Bắc thánh địa,Nam thánh Địa,Khu hang động,Bìa rừng nguyên thủy,Rừng nguyên thủy,Làng Plant nguyên thủy,Tranh ngọc Namếc";
		public static void perform2(int idAction, object p)
		{
			
			if (idAction == 880) { Service.gI().openUIZone(); }
			//if (idAction == 890) { checkClickButton = !checkClickButton; }
			if (idAction == 881) { BongTai(); }
			if (idAction == 882) { UseItem(194); }
            if (idAction == 883) { XmapController.ShowXmapMenu(); }
			if(idAction == 991) 
		{
				Mod.itemBuy = GameCanvas.panel.currItem;
				string[] array5 = (string[])p;
				GameCanvas.panel.VuDangChatTextField(array5[0], array5[1]);
				
			}
			if (idAction == 3)
			{
				Mod.thudau = !Mod.thudau;
				GameScr.info1.addInfo("Thu Đậu: " + (Mod.thudau ? "Bật" : "Tắt"), 0);
				MenuMod();
			}
			if (idAction == 4)
			{
				Mod.xindau = !Mod.xindau;
				GameScr.info1.addInfo("Xin Đậu: " + (Mod.xindau ? "Bật" : "Tắt"), 0);
				MenuMod();
			}
			if (idAction == 5)
			{
				Mod.chodau = !Mod.chodau;
				new Thread(new ThreadStart(Mod.cd)).Start();
				GameScr.info1.addInfo("Cho Đậu: " + (Mod.chodau ? "Bật" : "Tắt"), 0);
				MenuMod();
			}
			if (idAction == 6)
			{
				Mod.nvat = !Mod.nvat;
				GameScr.info1.addInfo("Thông tin người chơi trong map: " + (Mod.nvat ? "Bật" : "Tắt"), 0);
				MenuMod();
			}
			if (idAction == 9)
			{
				Mod.xoamap = !Mod.xoamap;
				GameScr.info1.addInfo("Xóa map: " + (Mod.xoamap ? "Bật" : "Tắt"), 0);
				MenuMod();
			}
			if (idAction == 11)
			{
				Mod.giamDungLuong = !Mod.giamDungLuong;
				GameScr.info1.addInfo("Giảm dung lượng: " + (Mod.giamDungLuong ? "Bật" : "Tắt"), 0);
				MenuMod();
			}
			if (idAction == 12)
			{
				Mod.isAutoNeBoss = !Mod.isAutoNeBoss;
				GameScr.info1.addInfo("Auto né boss: " + (Mod.isAutoNeBoss ? "Bật" : "Tắt"), 0);
				MenuMod();
			}
			if (idAction == 13)
			{
				Mod.isAutoCo = !Mod.isAutoCo;
				GameScr.info1.addInfo("Auto cờ né súc vật: " + (Mod.isAutoCo ? "Bật" : "Tắt"), 0);
				MenuMod();
			}
			if(idAction == 900)
            {
				Mod.isbufftl = !Mod.isbufftl;
				GameScr.info1.addInfo("Auto buff đậu cho đệ theo thể lực: "+(isbufftl ? "Bật":"Tắt"),0);
				MenuMod();

			}
			if(idAction == 442)
            {

				Button = !Button;

            }
			switch (idAction)
			{
				case 14:
					{
						MyVector myVector = new MyVector();
						myVector.addElement(new Command("Auto Theo Thể Lực:\n"+(isbufftl ? "Bật":"Tắt"), 900));
						myVector.addElement(new Command(Mod.thudau ? "Auto Thu Đậu: Bật" : "Auto Thu Đậu: Tắt", 3));
						myVector.addElement(new Command(Mod.chodau ? "Auto Cho Đậu: Bật" : "Auto Cho Đậu: Bật", 5));
						myVector.addElement(new Command(Mod.xindau ? "Auto Xin Đậu: Bật" : "Auto Xin Đậu: Tắt", 4));
						GameCanvas.menu.startAt(myVector, 4);
						return;
					}
				case 15:
					{
						MyVector myVector2 = new MyVector();
						//myVector2.addElement(new Command(Mod.XoaBackground ? "Xóa Background: Bật" : "Xóa Background: Tắt", 7));
						myVector2.addElement(new Command(Mod.xoamap ? "Xóa Địa Hình: Bật" : "Xóa Địa Hình: Tắt", 9));
						myVector2.addElement(new Command(Mod.giamDungLuong ? "Giảm Dung Lượng: Bật" : "Giảm Dung Lượng: Tắt", 11));
						GameCanvas.menu.startAt(myVector2, 4);
						return;
					}
				case 16:
					{
						MyVector myVector3 = new MyVector();
						myVector3.addElement(new Command(Mod.isKOK ? "Auto Up Kaioken: Bật" : "Auto Up Kaioken: Tắt", 19));
						myVector3.addElement(new Command(Mod.isThongTinSuPhu ? "TT Sư Phụ: Bật" : "TT Sư Phụ: Tắt", 92));
						myVector3.addElement(new Command(Mod.isThongTinDeTu ? "TT Đệ Tử: Bật" : "TT Đệ Tử: Tắt", 93));
						myVector3.addElement(new Command(Mod.isAutoNeBoss ? "Né boss: Bật" : "Né boss: Tắt", 12));
						myVector3.addElement(new Command(Mod.isAutoCo ? "Auto cờ: Bật" : "Auto cờ: Tắt", 13));
						myVector3.addElement(new Command(Mod.isCheckLag ? "Checklag: Bật" : "Checklag: Tắt", 75));
						GameCanvas.menu.startAt(myVector3, 4);
						return;
					}
				default:
					if (idAction == 19)
					{
						Mod.isKOK = !Mod.isKOK;
						GameScr.info1.addInfo("Auto up đệ kok: " + (Mod.isKOK ? "Bật" : "Tắt"), 0);
						MenuMod();
					}
					
					if (idAction == 23)
					{
						Mod.autoHoiSinh = !Mod.autoHoiSinh;
						GameScr.info1.addInfo("Auto hồi sinh bằng ngọc: " + (Mod.autoHoiSinh ? "Bật" : "Tắt"), 0);
						MenuMod();
					}
					if (idAction == 25)
					{
						Mod.isAutoBT = !Mod.isAutoBT;
						GameScr.info1.addInfo("Auto bông tai: " + (Mod.isAutoBT ? "Bật" : "Tắt"), 0);
						MenuMod();
					}
					if (idAction == 26)
					{
						Mod.khoamap = !Mod.khoamap;
						GameScr.info1.addInfo("Khóa map: " + (Mod.khoamap ? "Bật" : "Tắt"), 0);
						MenuMod();
					}
					if (idAction == 27)
					{
						Mod.khoakhu = !Mod.khoakhu;
						GameScr.info1.addInfo("Khóa chuyển khu: " + (Mod.khoakhu ? "Bật" : "Tắt"), 0);
						MenuMod();
					}
					if (idAction == 28)
					{
						Mod.isBossM = false;
						Mod.isPKM = false;
						Mod.trangThai = !Mod.trangThai;
						GameScr.info1.addInfo("Trạng thái nhân vật đang trỏ: " + (Mod.trangThai ? "Bật" : "Tắt"), 0);
						MenuMod();
					}
					if (idAction == 29)
					{
						Mod.isAK = !Mod.isAK;
						GameScr.info1.addInfo("Tự động chém: " + (Mod.isAK ? "Bật" : "Tắt"), 0);
						MenuMod();
					}
					switch (idAction)
					{
						case 31:
							{
								MyVector myVector4 = new MyVector();
								myVector4.addElement(new Command(Mod.isAutoBT ? "Auto bông tai: Bật" : "Auto bông tai: Tắt", 25));
								myVector4.addElement(new Command(Mod.isAK ? "Tự động đánh: Bật" : "Tự động đánh: Tắt", 29));
								myVector4.addElement(new Command(Mod.isAutoTTNL ? "Auto TTNL: Bật" : "Auto TTNL: Tắt", 37));
								myVector4.addElement(new Command(Mod.isKhoaViTri ? "Khóa vị trí: Bật" : "Khóa vị trí: Tắt", 64));
								myVector4.addElement(new Command(Mod.isAutoAnNho ? "Auto ăn nho: Bật" : "Auto ăn nho: Tắt", 41));
								myVector4.addElement(new Command(Mod.isAutoNhatXa ? "Auto nhặt xa: Bật" : "Auto nhặt xa: Tắt", 91));
								GameCanvas.menu.startAt(myVector4, 4);
								return;
							}
						case 32:
							{
								MyVector myVector5 = new MyVector();
								
								myVector5.addElement(new Command(Mod.isPKM ? "Được đánh trong khu: Bật" : "Được đánh trong khu: Tắt", 50));

                                myVector5.addElement(new Command(Mod.thongBaoBoss ? "Thông báo boss: Bật" : "Thông báo boss: Tắt", 46));
                                myVector5.addElement(new Command(Mod.lineboss ? "Đường kẻ tới boss: Bật" : "Đường kẻ tới boss: Tắt", 47));
								myVector5.addElement(new Command(Mod.focusBoss ? "Focus boss: Bật" : "Focus boss: Tắt", 52));
								myVector5.addElement(new Command(Mod.khoamap ? "Khóa chuyển map: Bật" : "Khóa chuyển map: Tắt", 26));
								myVector5.addElement(new Command(Mod.khoakhu ? "Khóa chuyển khu: Bật" : "Khóa chuyển khu: Tắt", 27));
								GameCanvas.menu.startAt(myVector5, 4);
								return;
							}
						case 33:
							{
								MyVector myVector6 = new MyVector();
								//myVector6.addElement(new Command("Auto Yardrat", 31));
								myVector6.addElement(new Command(Mod.nvat ? "Danh Sách Nhân Vật:\nON" : "Danh Sách Nhân Vật:\nOFF", 6));
								myVector6.addElement(new Command(Mod.isAutoLogin ? "Auto Login: Bật" : "Auto Login: Tắt", 36));
								myVector6.addElement(new Command(Mod.isAutoVeKhu ? "Về Khu Cũ: Bật" : "Về Khu Cũ: Tắt", 39));
								
								myVector6.addElement(new Command(Mod.autoHoiSinh ? "Auto Hồi Sinh: Bật" : "Auto Hồi SInh: Tắt", 23));
								//if (mGraphics.zoomLevel > 2)
								//{
								//	myVector6.addElement(new Command("Phím Tắt", 442));
								//}
								//myVector6.addElement(new Command(Mod.hoiSinhNgoc ? "Auto HS ngọc 2: Bật" : "Auto HS ngọc 2: Tắt", 63));
								//myVector6.addElement(new Command(Application.runInBackground ? "Đóng băng cpu: Tắt" : "Đóng băng cpu: Bật", 24));
								GameCanvas.menu.startAt(myVector6, 4);
								return;
							}
						default:
							if (idAction == 36)
							{
								Mod.isAutoLogin = !Mod.isAutoLogin;
								GameScr.info1.addInfo("Auto login: " + (Mod.isAutoLogin ? "Bật" : "Tắt"), 0);
								MenuMod();
							}
							if (idAction == 37)
							{
								Mod.isAutoTTNL = !Mod.isAutoTTNL;
								GameScr.info1.addInfo("Auto TTNL: " + (Mod.isAutoTTNL ? "Bật" : "Tắt"), 0);
								MenuMod();
							}
							if (idAction == 38)
							{
								Mod.IdmapGB = TileMap.mapID;
								Mod.ZoneGB = TileMap.zoneID;
								Mod.xGB = global::Char.myCharz().cx;
								Mod.yGB = global::Char.myCharz().cy;
								Mod.IsGoBack = !Mod.IsGoBack;
								if (Mod.IsGoBack)
								{
									GameScr.info1.addInfo(string.Concat(new object[]
									{
						"Map Goback: ",
						TileMap.mapName,
						" | Khu: ",
						TileMap.zoneID
									}), 0);
									GameScr.info1.addInfo(string.Concat(new object[]
									{
						"Tọa độ X: ",
						Mod.xGB,
						" | Y: ",
						Mod.yGB
									}), 0);
									if (global::Char.myCharz().cHP <= 0 || global::Char.myCharz().statusMe == 14)
									{
										Service.gI().returnTownFromDead();
										new Thread(new ThreadStart(Mod.GoBack)).Start();
									}
								}
								GameScr.info1.addInfo("Goback tọa độ: " + (Mod.IsGoBack ? "Bật" : "Tắt"), 0);
								MenuMod();
							}
							if (idAction == 39)
							{
								Mod.isAutoVeKhu = !Mod.isAutoVeKhu;
								GameScr.info1.addInfo((Mod.isAutoVeKhu ? "Auto về khu cũ khi Login: Bật" : "Auto về khu cũ khi Login: Tắt") ?? "", 0);
								MenuMod();
							}
							if (idAction == 41)
							{
								Mod.isAutoAnNho = !Mod.isAutoAnNho;
								GameScr.info1.addInfo("Auto ăn nho: " + (Mod.isAutoAnNho ? "Bật" : "Tắt"), 0);
								MenuMod();
							}
							if (idAction == 43)
							{
								Pk9rPickMob.IsNeSieuQuai = !Pk9rPickMob.IsNeSieuQuai;
								GameScr.info1.addInfo("Tàn sát né siêu quái: " + (Pk9rPickMob.IsNeSieuQuai ? "Bật" : "Tắt"), 0);
								MenuMod();
							}
							if (idAction == 44)
							{
								Pk9rPickMob.IsTanSat = !Pk9rPickMob.IsTanSat;
								GameScr.info1.addInfo("Tự động đánh quái: " + (Pk9rPickMob.IsTanSat ? "Bật" : "Tắt"), 0);
								MenuMod();
							}
							if (idAction != 45)
							{
								
								if (idAction == 47)
								{
									Mod.lineboss = !Mod.lineboss;
									GameScr.info1.addInfo("Đường Kẻ Tới Boss: " + (Mod.lineboss ? "Bật" : "Tắt"), 0);
									MenuMod();
								}
								if (idAction == 50)
								{
									Mod.isPKM = !Mod.isPKM;
									Mod.isBossM = false;
									Mod.trangThai = false;
									GameScr.info1.addInfo("Bọn được đấm nhau trong khu: " + (Mod.isPKM ? "Bật" : "Tắt"), 0);
									MenuMod();
								}
								if (idAction == 52)
								{
									Mod.focusBoss = !Mod.focusBoss;
									GameScr.info1.addInfo("Focus Boss: " + (Mod.focusBoss ? "Bật" : "Tắt"), 0);
									MenuMod();
								}
								if (idAction == 63)
								{
									Mod.ngocHienTai = global::Char.myCharz().luongKhoa + global::Char.myCharz().luong;
									Mod.hoiSinhNgoc = !Mod.hoiSinhNgoc;
									GameScr.info1.addInfo((Mod.hoiSinhNgoc ? "Auto hồi sinh bằng số ngọc được chỉ định: Bật" : "Auto hồi sinh bằng số ngọc được chỉ định: Tắt") ?? "", 0);
									MenuMod();
								}
								if (idAction == 64)
								{
									Mod.ghimX = global::Char.myCharz().cx;
									Mod.ghimY = global::Char.myCharz().cy;
									Mod.isKhoaViTri = !Mod.isKhoaViTri;
									GameScr.info1.addInfo("Khóa vị trí: " + (Mod.isKhoaViTri ? "Bật" : "Tắt"), 0);
									MenuMod();
								}
								if (idAction == 75)
								{
									Mod.isCheckLag = !Mod.isCheckLag;
									GameScr.info1.addInfo("Checklag: " + (Mod.isCheckLag ? "Bật" : "Tắt"), 0);
									MenuMod();
								}
								if (idAction != 76)
								{
									if (idAction != 80)
									{
										switch (idAction)
										{
											case 89:
												{
													MyVector myVector7 = new MyVector();
													for (int i = 0; i < GameScr.keySkill.Length; i++)
													{
														myVector7.addElement(new Command((GameScr.keySkill[i] == null) ? "Chưa Gán Skill" : (GameScr.keySkill[i].template.name + " [" + (Mod.listSkillsAuto.Contains(GameScr.keySkill[i]) ? "Xóa" : "Thêm") + "]"), 90));
													}
													GameCanvas.menu.startAt(myVector7, 4);
													break;
												}
											case 90:
												Mod.AddRemoveSkill(GameCanvas.menu.menuSelectedItem);
												break;
											case 91:
												Mod.isAutoNhatXa = !Mod.isAutoNhatXa;
												if (Mod.isAutoNhatXa)
												{
													Mod.xNhatXa = global::Char.myCharz().cx;
													Mod.yNhatXa = global::Char.myCharz().cy;
													GameScr.info1.addInfo(string.Concat(new object[]
													{
									"Tọa Độ : ",
									global::Char.myCharz().cx,
									"|",
									global::Char.myCharz().cy
													}), 0);
												}
												GameScr.info1.addInfo("Auto Nhặt Xa : " + (Mod.isAutoNhatXa ? "Bật" : "Tắt"), 0);
												MenuMod();
												break;
											case 92:
												Mod.isThongTinSuPhu = !Mod.isThongTinSuPhu;
												GameScr.info1.addInfo("Thông Tin Sư Phụ: " + (Mod.isThongTinSuPhu ? "Bật" : "Tắt"), 0);
												MenuMod();
												break;
											case 93:
												Mod.isThongTinDeTu = !Mod.isThongTinDeTu;
												GameScr.info1.addInfo("Thông Tin Đệ Tử: " + (Mod.isThongTinDeTu ? "Bật" : "Tắt"), 0);
												MenuMod();
												break;
										}
									}
									else
									{
										Mod.dichChuyenPem = !Mod.dichChuyenPem;
										GameScr.info1.addInfo("Dịch Chuyển Đến Quái:\n " + (Mod.dichChuyenPem ? "Bật" : "Tắt"), 0);
										MenuMod();
									}
								}
								else
								{
									Pk9rPickMob.IsVuotDiaHinh = !Pk9rPickMob.IsVuotDiaHinh;
									GameScr.info1.addInfo("Vượt Địa Hình: " + (Pk9rPickMob.IsVuotDiaHinh ? "Bật" : "Tắt"), 0);
									MenuMod();
								}
							}
							else
							{
								MyVector myVector8 = new MyVector();
								myVector8.addElement(new Command(Pk9rPickMob.IsNeSieuQuai ? "Né Siêu Quái: Bật" : "Né Siêu Quái: Tắt", 43));
								myVector8.addElement(new Command(Pk9rPickMob.IsVuotDiaHinh ? "Vượt Địa Hình: Bật" : "Vượt Địa Hình: Tắt", 76));
								myVector8.addElement(new Command(Mod.dichChuyenPem ? "Dịch Chuyển Đến Quái:\n Bật" : "Dịch Chuyển Đến Quái:\n Tắt", 80));
								myVector8.addElement(new Command(Pk9rPickMob.IsTanSat ? "Tàn Sát: Bật" : "Tàn Sát: Tắt", 44));
								myVector8.addElement(new Command(Mod.IsGoBack ? "Goback Tọa Độ: Bật" : "Goback Tọa Độ: Tắt", 38));
								GameCanvas.menu.startAt(myVector8, 4);
							}
							return;
					}
					break;
			}
	}
		public static bool isThongTinDeTu;
		public static bool isCheckLag = false;
		public static bool isBossM;
		public static bool isPKM;
		public static bool autoHoiSinh;
		private static long currAutoHoiSinh;
		private static long currAutoBT;
		public static bool dichChuyenPem = true;
		public static int tocdochay = 6;
		public static string saoTrongBalo(Item item)
		{
			string result;
			if ((item != null && item.template.type <= 5) || item.template.type == 32)
			{
				result = Mod.soSao(item) + " sao";
			}
			else
			{
				result = "";
			}
			return result;
		}
		// Token: 0x04001294 RID: 4756
		public static sbyte petStatus;
		public static void LoadGame()
		{
			global::Char.myCharz().cspeed = Mod.tocdochay;
			Mod.petStatus = 3;
			Mod.listSkillsAuto.Clear();
			Mod.listItemAuto.Clear();
		}
		public static List<Mod.ItemAuto> listItemAuto = new List<Mod.ItemAuto>();
		public static void AddItem(Item item)
		{
			foreach (Mod.ItemAuto itemAuto in Mod.listItemAuto)
			{
				if (itemAuto.iconID == (int)item.template.iconID && itemAuto.id == (int)item.template.id)
				{
					Mod.listItemAuto.Remove(itemAuto);
					GameCanvas.startOKDlg("Đã xóa " + item.template.name + " khỏi list item auto");
					return;
				}
			}
			Mod.listItemAuto.Add(new Mod.ItemAuto((int)item.template.iconID, (int)item.template.id));
			GameCanvas.startOKDlg("Đã thêm " + item.template.name + " vào list item auto");
		}
		public struct ItemAuto
		{
			// Token: 0x06000A20 RID: 2592 RVA: 0x000065C6 File Offset: 0x000047C6
			public ItemAuto(int iconID, int id)
			{
				this.iconID = iconID;
				this.id = id;
			}
			public int iconID;

			// Token: 0x0400130C RID: 4876
			public int id;
		}
			// Token: 0x04001295 RID: 4757
			public static bool isAutoBT;

		// Token: 0x04001296 RID: 4758
		public static int timeBT = 1;
		// Token: 0x040012AB RID: 4779
		public static bool isThongTinSuPhu;
		public static void AutoBT()
		{
			if (Mod.isAutoBT && mSystem.currentTimeMillis() - Mod.currAutoBT >= (long)(Mod.timeBT * 1000))
			{
				Mod.currAutoBT = mSystem.currentTimeMillis();
				Mod.BongTai();
				if (global::Char.myCharz().isNhapThe)
				{
					Mod.BongTai();
				}
			}
		}
		public static void BongTai()
		{
			for (int i = 0; i < global::Char.myCharz().arrItemBag.Length; i++)
			{
				if (global::Char.myCharz().arrItemBag[i] != null && global::Char.myCharz().arrItemBag[i].template.id == 921)
				{
					Service.gI().useItem(0, 1, (sbyte)i, -1);
					Service.gI().petStatus(Mod.petStatus);
					return;
				}
			}
			for (int j = 0; j < global::Char.myCharz().arrItemBag.Length; j++)
			{
				if (global::Char.myCharz().arrItemBag[j] != null && global::Char.myCharz().arrItemBag[j].template.id == 454)
				{
					Service.gI().useItem(0, 1, (sbyte)j, -1);
					Service.gI().petStatus(Mod.petStatus);
					return;
				}
			}
			GameScr.info1.addInfo("Đéo có bông tai", 0);
		}
		public static bool khoamap;
		public static bool khoakhu;

		// Token: 0x040012C9 RID: 4809
		public static long canChangeZone;
		private static void AutoHoiSinh()
		{
			if (Mod.autoHoiSinh && (global::Char.myCharz().cHP <= 0 || global::Char.myCharz().meDead || global::Char.myCharz().statusMe == 14) && mSystem.currentTimeMillis() - Mod.currAutoHoiSinh >= 300L)
			{
				Mod.currAutoHoiSinh = mSystem.currentTimeMillis();
				Service.gI().wakeUpFromDead();
			}
		}
		public static bool thongBaoBoss = true;
        public static int GetIDMap(string mapName)
        {
            int result = -1;
            for (int i = 0; i < Mod.MapNames.Length; i++)
            {
                if (Mod.MapNames[i].Trim().ToLower().Equals(mapName.Trim().ToLower()))
                {
                    result = i;
                }
            }
            return result;
        }
        public static string[] MapNames = Mod.ListMap.Split(new char[]
        {
        ','
        });
        //private static string ListMap = "Làng Aru,Đồi hoa cúc,Thung lũng tre,Rừng nấm,Rừng xương,Đảo Kamê,Đông Karin,Làng Mori,Đồi nấm tím,Thị trấn Moori,Thung lũng Namếc,Thung lũng Maima,Vực maima,Đảo Guru,Làng Kakarot,Đồi hoang,Làng Plant,Rừng nguyên sinh,Rừng thông Xayda,Thành phố Vegeta,Vách núi đen,Nhà Gôhan,Nhà Moori,Nhà Broly,Trạm tàu vũ trụ,Trạm tàu vũ trụ,Trạm tàu vũ trụ,Rừng Bamboo,Rừng dương xỉ,Nam Kamê,Đảo Bulông,Núi hoa vàng,Núi hoa tím,Nam Guru,Đông Nam Guru,Rừng cọ,Rừng đá,Thung lũng đen,Bờ vực đen,Vách núi Aru,Vách núi Moori,Vực Plant,Vách núi Aru,Vách núi Moori,Vách núi Kakarot,Thần điện,Tháp Karin,Rừng Karin,Hành tinh Kaio,Phòng tập thời gian,Thánh địa Kaio,Đấu trường,Đại hội võ thuật,Tường thành 1,Tầng 3,Tầng 1,Tầng 2,Tầng 4,Tường thành 2,Tường thành 3,Trại độc nhãn 1,Trại độc nhãn 2,Trại độc nhãn 3,Trại lính Fide,Núi dây leo,Núi cây quỷ,Trại qủy già,Vực chết,Thung lũng Nappa,Vực cấm,Núi Appule,Căn cứ Raspberry,Thung lũng Raspberry,Thung lũng chết,Đồi cây Fide,Khe núi tử thần,Núi đá,Rừng đá,Lãnh  địa Fize,Núi khỉ đỏ,Núi khỉ vàng,Hang quỷ chim,Núi khỉ đen,Hang khỉ đen,Siêu Thị,Hành tinh M-2,Hành tinh Polaris,Hành tinh Cretaceous,Hành tinh Monmaasu,Hành tinh Rudeeze,Hành tinh Gelbo,Hành tinh Tigere,Thành phố phía đông,Thành phố phía nam,Đảo Balê,95,Cao nguyên,Thành phố phía bắc,Ngọn núi phía bắc,Thung lũng phía bắc,Thị trấn Ginder,101,Nhà Bunma,Võ đài Xên bọ hung,Sân sau siêu thị,Cánh đồng tuyết,Rừng tuyết,Núi tuyết,Dòng sông băng,Rừng băng,Hang băng,Đông Nam Karin,Võ đài Hạt Mít,Đại hội võ thuật,Cổng phi thuyền,Phòng chờ,Thánh địa Kaio,Cửa Ải 1,Cửa Ải 2,Cửa Ải 3,Phòng chỉ huy,Đấu trường,Ngũ Hành Sơn,Ngũ Hành Sơn,Ngũ Hành Sơn,Võ đài Bang,Thành phố Santa,Cổng phi thuyền,Bụng Mabư,Đại hội võ thuật,Đại hội võ thuật Vũ Trụ,Hành Tinh Yardart,Hành Tinh Yardart 2,Hành Tinh Yardart 3,Đại hội võ thuật Vũ Trụ 6-7,Động hải tặc,Hang Bạch Tuộc,Động kho báu,Cảng hải tặc,Hành tinh Potaufeu,Hang động Potaufeu,Con đường rắn độc,Con đường rắn độc,Con đường rắn độc,Hoang mạc,Võ Đài Siêu Cấp,Tây Karin,Sa mạc,Lâu đài Lychee,Thành phố Santa,Lôi Đài,Hành tinh bóng tối,Vùng đất băng giá,Lãnh địa bang hội,Hành tinh Bill,Hành tinh ngục tù,Tây thánh địa,Đông thánh Địa,Bắc thánh địa,Nam thánh Địa,Khu hang động,Bìa rừng nguyên thủy,Rừng nguyên thủy,Làng Plant nguyên thủy,Tranh ngọc Namếc";

        // Token: 0x040012A1 RID: 4769
        public static MyVector bossVip = new MyVector();
		public static List<Skill> listSkillsAuto = new List<Skill>();
		public static void AddRemoveSkill(int indexSkill)
		{
			if (Mod.listSkillsAuto.Contains(GameScr.keySkill[indexSkill]))
			{
				Mod.listSkillsAuto.Remove(GameScr.keySkill[indexSkill]);
				GameScr.info1.addInfo("Đã xóa " + GameScr.keySkill[indexSkill].template.name + " khỏi list skill auto", 0);
				MenuMod();
				return;
			}
			Mod.listSkillsAuto.Add(GameScr.keySkill[indexSkill]);
			GameScr.info1.addInfo("Đã thêm " + GameScr.keySkill[indexSkill].template.name + " vào list skill auto", 0);
			MenuMod();
		}
		public static void TeleportTo(int a, int b)
		{
			global::Char.myCharz().cx = a;
			global::Char.myCharz().cy = b;
			Service.gI().charMove();
			global::Char.myCharz().cx = a;
			global::Char.myCharz().cy = b + 1;
			Service.gI().charMove();
			global::Char.myCharz().cx = a;
			global::Char.myCharz().cy = b;
			Service.gI().charMove();
		}
		public static void TeleportToMyFocus()
		{
			if (global::Char.myCharz().charFocus != null)
			{
				TeleportTo(global::Char.myCharz().charFocus.cx, global::Char.myCharz().charFocus.cy);
				return;
			}
			if (global::Char.myCharz().itemFocus != null)
			{
				TeleportTo(global::Char.myCharz().itemFocus.x, global::Char.myCharz().itemFocus.y);
				return;
			}
			if (global::Char.myCharz().mobFocus != null)
			{
				TeleportTo(global::Char.myCharz().mobFocus.x, global::Char.myCharz().mobFocus.y);
				return;
			}
			if (global::Char.myCharz().npcFocus != null)
			{
				TeleportTo(global::Char.myCharz().npcFocus.cx, global::Char.myCharz().npcFocus.cy - 3);
				return;
			}
			GameScr.info1.addInfo("Không Có Mục Tiêu!", 0);
		}
		public static bool isMeInNRDMap()
		{
			return TileMap.mapID >= 85 && TileMap.mapID <= 91;
		}
		public static void AutoXHP()
		{
			if (!isMeInNRDMap())
			{
				GameScr.info1.addInfo("Vui Lòng Vào Map Ngọc Rồng Đen!", 0);
				return;
			}
			if (global::Char.myCharz().cFlag != 8)
			{
				GameScr.info1.addInfo("Vui Lòng Nhặt Ngọc Rồng Đen!", 0);
				return;
			}
			bool flag = false;
			if (global::Char.myCharz().bag >= 0 && ClanImage.idImages.containsKey(global::Char.myCharz().bag.ToString() + string.Empty))
			{
				ClanImage clanImage = (ClanImage)ClanImage.idImages.get(global::Char.myCharz().bag.ToString() + string.Empty);
				if (clanImage.idImage != null)
				{
					for (int i = 0; i < clanImage.idImage.Length; i++)
					{
						if (clanImage.idImage[i] == 2322)
						{
							flag = true;
							break;
						}
					}
				}
			}
			if (!flag)
			{
				GameScr.info1.addInfo("Vui Lòng Nhặt Ngọc Rồng Đen!", 0);
				return;
			}
			bool flag2 = true;
			Npc npc = (Npc)GameScr.vNpc.elementAt(0);
			int j = 0;
			while (j < GameScr.vNpc.size())
			{
				npc = (Npc)GameScr.vNpc.elementAt(j);
				if (npc.template.npcTemplateId < 30 || npc.template.npcTemplateId > 36 || global::Char.myCharz().cx <= npc.cx - 60 || global::Char.myCharz().cx >= npc.cx + 60)
				{
					j++;
				}
				else
				{
					bool flag3 = false;
					if (global::Char.myCharz().holdEffID != 0 || global::Char.myCharz().sleepEff || global::Char.myCharz().blindEff || global::Char.myCharz().isFreez)
					{
						flag2 = false;
					}
					if (!flag2 && flag3)
					{
						GameScr.info1.addInfo("Đang Bị Không Chế, Không Thể Bay Đi Xhp", 0);
						return;
					}
					if (flag3)
					{
						TeleportTo(npc.cx, npc.cy - 3);
					}
					int cHPFull = global::Char.myCharz().cHPFull;
					int num = 35;
					if (TileMap.mapID == 91)
					{
						num = 35;
					}
					if (TileMap.mapID == 90)
					{
						num = 34;
					}
					if (TileMap.mapID == 89)
					{
						num = 33;
					}
					if (TileMap.mapID == 88)
					{
						num = 32;
					}
					if (TileMap.mapID == 87)
					{
						num = 31;
					}
					if (TileMap.mapID == 86)
					{
						num = 30;
					}
					if (TileMap.mapID == 85)
					{
						num = 36;
					}
					Service.gI().openMenu(num);
					Service.gI().menu(num, 0, 2);
					Service.gI().menuId(2);
					Service.gI().confirmMenu((short)num, 2);
					if (cHPFull != global::Char.myCharz().cHPFull)
					{
						GameScr.info1.addInfo("Đã Xhp!", 0);
						return;
					}
					GameScr.info1.addInfo("Lỗi Xhp!", 0);
					return;
				}
			}
		}
		public static void paint(mGraphics g)
        {
            if (Button )
            {
				if (GameCanvas.currentDialog != null || ChatPopup.currChatPopup != null || GameCanvas.menu.showMenu || GameScr.gI().isPaintPopup() || GameCanvas.panel.isShow || Char.myCharz().taskMaint.taskId == 0 || ChatTextField.gI().isShow || GameCanvas.currentScreen == MoneyCharge.instance)
				{
					return;
				}
				//var buttonC = mSystem.loadImage("/mainImage/clickC.png");
				//g.drawImage(buttonC, GameCanvas.w - 35, 100);
				//var buttonS = mSystem.loadImage("/mainImage/clickS.png");
				//g.drawImage(buttonS, GameCanvas.w - 35, 130);
				//var buttonM = mSystem.loadImage("/mainImage/clickM.png");
				//g.drawImage(buttonM, GameCanvas.w - 70, 100);
				//var buttonV = mSystem.loadImage("/mainImage/clickV.png");
				//g.drawImage(buttonV, GameCanvas.w - 70, 130);
				//var buttonH = mSystem.loadImage("/mainImage/clickH.png");
				//g.drawImage(buttonH, GameCanvas.w - 80, 162);
				//var buttonG = mSystem.loadImage("/mainImage/clickG.png");
				//g.drawImage(buttonG, GameCanvas.w - 130, 185);
				//var buttonA = mSystem.loadImage("/mainImage/clickA.png");
				//g.drawImage(buttonA, GameCanvas.w - 100, 185);
				//var buttonE = mSystem.loadImage("/mainImage/clickE.png");
				//g.drawImage(buttonE, GameCanvas.w - 160, 220 - 2);
				//var buttonF = mSystem.loadImage("/mainImage/clickF.png");
				//g.drawImage(buttonF, GameCanvas.w - 130, 220 - 2);
				//var buttonJ = mSystem.loadImage("/mainImage/clickJ.png");
				//g.drawImage(buttonJ, 40, 130, 220 - 2);
				//var buttonK = mSystem.loadImage("/mainImage/clickK.png");
				//g.drawImage(buttonK, 70, 130, 220 - 2);
				//var buttonL = mSystem.loadImage("/mainImage/clickL.png");
				//g.drawImage(buttonL, 100, 130, 220 - 2);
				PaintButton(g);
				if (GameCanvas.isPointerHoldIn(GameCanvas.w - 35, 100, 18, 35) && GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
				{
					for (int i = 0; i < global::Char.myCharz().arrItemBag.Length; i++)
					{
						if (global::Char.myCharz().arrItemBag[i] != null && global::Char.myCharz().arrItemBag[i].template.id == 193)
						{
							Service.gI().useItem(0, 1, (sbyte)i, -1);
							
							return;
						}
					}
					for (int j = 0; j < global::Char.myCharz().arrItemBag.Length; j++)
					{
						if (global::Char.myCharz().arrItemBag[j] != null && global::Char.myCharz().arrItemBag[j].template.id == 194)
						{
							Service.gI().useItem(0, 1, (sbyte)j, -1);
							
							return;
						}
					}
					GameScr.info1.addInfo("Đéo có capsule", 0);
					SoundMn.gI().buttonClick();
					GameCanvas.clearAllPointerEvent();
				}
				if (GameCanvas.isPointerHoldIn(GameCanvas.w - 35, 130, 18, 35) && GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
				{

					Mod.focusBoss = !Mod.focusBoss;
					
					SoundMn.gI().buttonClick();
					GameCanvas.clearAllPointerEvent();
				}
				if (GameCanvas.isPointerHoldIn(GameCanvas.w - 70, 100, 18, 35) && GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
				{

					Service.gI().openUIZone();
					GameScr.info1.addInfo("|5|Open Zone",0);
					SoundMn.gI().buttonClick();
					GameCanvas.clearAllPointerEvent();
				}
				if (GameCanvas.isPointerHoldIn(GameCanvas.w - 70, 130, 18, 35) && GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
				{

					TeleportToMyFocus();
					
					SoundMn.gI().buttonClick();
					GameCanvas.clearAllPointerEvent();
				}
				if (GameCanvas.isPointerHoldIn(GameCanvas.w - 80, 162, 18, 35) && GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
				{

					AutoXHP();

					SoundMn.gI().buttonClick();
					GameCanvas.clearAllPointerEvent();
				}
				if (GameCanvas.isPointerHoldIn(GameCanvas.w - 130, 185, 18, 35) && GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
				{

					if (global::Char.myCharz().charFocus == null)
					{
						GameScr.info1.addInfo("Vui Lòng Chọn Mục Tiêu!", 0);
					}
					else
					{
						Service.gI().giaodich(0, global::Char.myCharz().charFocus.charID, -1, -1);
						GameScr.info1.addInfo("Đã Gửi Lời Mời Giao Dịch Đến: " + global::Char.myCharz().charFocus.cName, 0);
					}

					SoundMn.gI().buttonClick();
					GameCanvas.clearAllPointerEvent();
				}
				if (GameCanvas.isPointerHoldIn(GameCanvas.w - 100, 185, 18, 35) && GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
				{


					Mod.isAK = !Mod.isAK;
					GameScr.info1.addInfo("|5|Tự Đánh: "+(isAK ? "ON":"OFF"),0);
					SoundMn.gI().buttonClick();
					GameCanvas.clearAllPointerEvent();
				}
				if (GameCanvas.isPointerHoldIn(GameCanvas.w - 160, 220 - 2, 18, 35) && GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
				{


					Mod.autoHoiSinh = !Mod.autoHoiSinh;
					GameScr.info1.addInfo("|5|Auto Hồi Sinh: " + (autoHoiSinh ? "ON" : "OFF"), 0);
					SoundMn.gI().buttonClick();
					GameCanvas.clearAllPointerEvent();
				}
				if (GameCanvas.isPointerHoldIn(GameCanvas.w - 130, 220 - 2, 18, 35) && GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
				{


					for (int i = 0; i < global::Char.myCharz().arrItemBag.Length; i++)
					{
						if (global::Char.myCharz().arrItemBag[i] != null && global::Char.myCharz().arrItemBag[i].template.id == 921)
						{
							Service.gI().useItem(0, 1, (sbyte)i, -1);
							Service.gI().petStatus(Mod.petStatus);
							return;
						}
					}
					for (int j = 0; j < global::Char.myCharz().arrItemBag.Length; j++)
					{
						if (global::Char.myCharz().arrItemBag[j] != null && global::Char.myCharz().arrItemBag[j].template.id == 454)
						{
							Service.gI().useItem(0, 1, (sbyte)j, -1);
							Service.gI().petStatus(Mod.petStatus);
							return;
						}
					}
					GameScr.info1.addInfo("Đéo có bông tai", 0);
					SoundMn.gI().buttonClick();
					GameCanvas.clearAllPointerEvent();
				}
				jkl = true;
				
			}
            else
            {
				jkl = false;
            }
			int num = 10;
			int num2 = 200;
		mFont.tahoma_7_red.drawStringBorder(g, "Dragon Blue Z", 15,30,mFont.LEFT,mFont.tahoma_7);
			mFont.tahoma_7b_red.drawStringBorder(g, "FPS: "+ Main.main.max, 85, 30, mFont.LEFT, mFont.tahoma_7);
			mFont.tahoma_7b_yellowSmall2.drawString(g, NinjaUtil.getMoneys((long)global::Char.myCharz().cHP), 90, 5, mFont.LEFT);
			mFont.tahoma_7b_yellowSmall2.drawString(g, NinjaUtil.getMoneys((long)global::Char.myCharz().cMP), 90, 17, mFont.LEFT);
			mFont.tahoma_7.drawString(g, "Time : " + DateTime.Now, num, GameCanvas.h - num2, mFont.LEFT);
			num2 -= 10;
			mFont.tahoma_7.drawString(g, string.Concat(new object[]
			{
		"Map : ",
		TileMap.mapName,
		" [",
		TileMap.mapID,
		"]  - Khu : ",
		TileMap.zoneID
			}), num, GameCanvas.h - num2, mFont.LEFT);
			num2 -= 10;
			mFont.tahoma_7.drawString(g, string.Concat(new object[]
			{
		"Tọa độ X : ",
		global::Char.myCharz().cx,
		" - Y : ",
		global::Char.myCharz().cy
			}), num, GameCanvas.h - num2, mFont.LEFT);
			num2 -= 10;
			if (Mod.isDapDo)
			{
				mFont.tahoma_7b_red.drawString(g, (Mod.doDeDap != null) ? Mod.doDeDap.template.name : "Chưa Có", GameCanvas.w / 2, 72, mFont.CENTER);
				mFont.tahoma_7b_red.drawString(g, (Mod.doDeDap != null) ? ("Số Sao : " + Mod.saoHienTai.ToString()) : "Số Sao : -1", GameCanvas.w / 2, 82, mFont.CENTER);
				mFont.tahoma_7b_red.drawString(g, "Số Sao Cần Đập : " + Mod.soSaoCanDap + " Sao", GameCanvas.w / 2, 92, mFont.CENTER);
			}
			if (Mod.isDapDo || Mod.isThuongDeThuong || Mod.isThuongDeVip)
			{
				mFont.tahoma_7b_red.drawString(g, "Ngọc Xanh : " + NinjaUtil.getMoneys((long)global::Char.myCharz().luong) + " Ngọc Hồng : " + NinjaUtil.getMoneys((long)global::Char.myCharz().luongKhoa), GameCanvas.w / 2, 102, mFont.CENTER);
				mFont.tahoma_7b_red.drawString(g, string.Concat(new object[]
				{
				"Vàng : ",
				NinjaUtil.getMoneys(global::Char.myCharz().xu),
				" Thỏi Vàng : ",
				Mod.thoiVang()
				}), GameCanvas.w / 2, 112, mFont.CENTER);
			}
		
			if (Mod.isPKM && !Mod.isGMT && (global::Char.myCharz().charFocus == null || (global::Char.myCharz().charFocus != null && !global::Char.myCharz().isMeCanAttackOtherPlayer(global::Char.myCharz().charFocus))))
		{
			for (int i = 0; i < GameScr.vCharInMap.size(); i++)
			{
				global::Char @char = (global::Char)GameScr.vCharInMap.elementAt(i);
				if (@char != null && global::Char.myCharz().isMeCanAttackOtherPlayer(@char) && !@char.isPet && !@char.isMiniPet && !@char.cName.StartsWith("$") && !@char.cName.StartsWith("#") && @char.charID >= 0)
				{
					global::Char.myCharz().focusManualTo(@char);
					return;
				}
			}
		}
			if (Mod.isPKM)
			{
				mFont.tahoma_7b_unfocus.drawString(g, "Địch trong khu:", GameCanvas.w / 2, 72, mFont.CENTER);
				int num6 = 82;
				global::Char char4 = null;
				for (int m = 0; m < GameScr.vCharInMap.size(); m++)
				{
					global::Char char5 = (global::Char)GameScr.vCharInMap.elementAt(m);
					if (char5 != null && global::Char.myCharz().isMeCanAttackOtherPlayer(char5))
					{
						if (global::Char.myCharz().charFocus != null && global::Char.myCharz().charFocus == char5)
						{
							char4 = char5;
						}
						g.setColor(Color.red);
						g.drawLine(global::Char.myCharz().cx - GameScr.cmx, global::Char.myCharz().cy - GameScr.cmy, char5.cx - GameScr.cmx, char5.cy - GameScr.cmy);
						mFont.tahoma_7b_red.drawString(g, string.Concat(new object[]
						{
						m,
						" - ",
						char5.isPet ? "$" : "",
						char5.isMiniPet ? "#" : "",
						char5.cName,
						"[",
						NinjaUtil.getMoneys((long)char5.cHP).ToString(),
						" / ",
						NinjaUtil.getMoneys((long)char5.cHPFull).ToString(),
						" ] [ ",
						Mod.hanhTinhNhanVat(char5),
						" ]"
						}), GameCanvas.w / 2, num6, mFont.CENTER);
						num6 += 10;
					}
				}
				if (char4 != null)
				{
					g.setColor(Color.green);
					g.drawLine(global::Char.myCharz().cx - GameScr.cmx, global::Char.myCharz().cy - GameScr.cmy, char4.cx - GameScr.cmx, char4.cy - GameScr.cmy);
				}
			}
			
			if (Mod.isThongTinSuPhu)
			{
				mFont.tahoma_7b_red.drawString(g, "Sư Phụ :", num, GameCanvas.h - 170, mFont.LEFT);
				mFont.tahoma_7b_yellowSmall2.drawString(g, "Sức Mạnh : " + NinjaUtil.getMoneys(global::Char.myCharz().cPower), num, GameCanvas.h - 160, mFont.LEFT);
				mFont.tahoma_7b_yellowSmall2.drawString(g, "Tiềm Năng : " + NinjaUtil.getMoneys(global::Char.myCharz().cTiemNang), num, GameCanvas.h - 150, mFont.LEFT);
				mFont.tahoma_7b_yellowSmall2.drawString(g, string.Concat(new object[]
				{
				"Sức Đánh : ",
				NinjaUtil.getMoneys((long)global::Char.myCharz().cDamFull),
				"  Giáp : ",
				global::Char.myCharz().cDefull
				}), num, GameCanvas.h - 140, mFont.LEFT);
				num += GameCanvas.w / 4;
			}
			if (Mod.isThongTinDeTu)
			{
				mFont.tahoma_7b_red.drawString(g, "Đệ Tử :", num, GameCanvas.h - 170, mFont.LEFT);
				mFont.tahoma_7b_yellowSmall2.drawString(g, "Sức Mạnh : " + NinjaUtil.getMoneys(global::Char.myPetz().cPower), num, GameCanvas.h - 160, mFont.LEFT);
				mFont.tahoma_7b_yellowSmall2.drawString(g, "Tiềm Năng : " + NinjaUtil.getMoneys(global::Char.myPetz().cTiemNang), num, GameCanvas.h - 150, mFont.LEFT);
				mFont.tahoma_7b_yellowSmall2.drawString(g, string.Concat(new object[]
				{
				"Sức Đánh : ",
				NinjaUtil.getMoneys((long)global::Char.myPetz().cDamFull),
				"  Giáp : ",
				global::Char.myPetz().cDefull
				}), num, GameCanvas.h - 140, mFont.LEFT);
				mFont.tahoma_7b_yellowSmall2.drawString(g, "HP : " + NinjaUtil.getMoneys((long)global::Char.myPetz().cHP), num, GameCanvas.h - 130, mFont.LEFT);
				mFont.tahoma_7b_yellowSmall2.drawString(g, "MP : " + NinjaUtil.getMoneys((long)global::Char.myPetz().cMP), num, GameCanvas.h - 120, mFont.LEFT);
				num += GameCanvas.w / 4;
			}
			if (Mod.thongBaoBoss)
			{
				int num3 = 35;
				for (int j = 0; j < Mod.bossVip.size(); j++)
				{
					g.setColor(2721889, 0.5f);
					g.fillRect(GameCanvas.w - 23, num3 + 2, 25, 9);
					((ShowBoss)Mod.bossVip.elementAt(j)).paintBoss(g, GameCanvas.w - 2, num3, mFont.RIGHT);
					num3 += 10;
				}
			}
			if (nvat)
			{
				int num7 = 95;
				for (int n = 0; n < chars.Length; n++)
				{
					chars[n] = null;
					
				}
				
				for (int num8 = 0; num8 < GameScr.vCharInMap.size(); num8++)
				{
					global::Char char6 = (global::Char)GameScr.vCharInMap.elementAt(num8);
					if (char6 != null)
					{
						g.fillRect(GameCanvas.w - 150, num7, 150, 10, 2721889, 90);
						if (char6 == global::Char.myCharz().charFocus && char6.cTypePk != 5 && char6.cName != null && char6.cName != "" && !char6.cName.StartsWith("#") && !char6.cName.StartsWith("$") && char6.cName != "Trọng tài")
						{
							mFont.tahoma_7b_red.drawString(g, string.Concat(new object[]
							{
						num8,
						".",
						char6.cName,
						"[ ",
						NinjaUtil.getMoneys((long)char6.cHP).ToString(),
						" ] [ ",
						hanhTinhNhanVat(char6),
						" ]"
							}), GameCanvas.w - 150, num7, mFont.LEFT);
						}
						else if (char6 == global::Char.myCharz().charFocus && char6.cTypePk == 5 && char6.cName != null && char6.cName != "" && !char6.cName.StartsWith("#") && !char6.cName.StartsWith("$") && char6.cName != "Trọng tài")
						{
							mFont.tahoma_7b_red.drawString(g, string.Concat(new object[]
							{
						num8,
						".",
						char6.cName,
						"[ ",
						NinjaUtil.getMoneys((long)char6.cHP).ToString(),
						" ] [ ",
						hanhTinhNhanVat(char6),
						" ]"
							}), GameCanvas.w - 150, num7, mFont.LEFT);
						}
						else if (char6.cTypePk == 5 || (char6.charID < 0 && char6.charID > -1000 && char6.charID != -114) && char6.cName != null && char6.cName != "" && !char6.cName.StartsWith("#") && !char6.cName.StartsWith("$") && char6.cName != "Trọng tài")
						{
							mFont.tahoma_7b_yellowSmall.drawString(g, string.Concat(new object[]
							{
						num8,
						".",
						char6.cName,
						"[ ",
						NinjaUtil.getMoneys((long)char6.cHP).ToString(),
						" ] [ ",
						hanhTinhNhanVat(char6),
						" ]"
							}), GameCanvas.w - 150, num7, mFont.LEFT);
						}
						else if (char6.clanID == global::Char.myCharz().clanID)
						{
							mFont.tahoma_7_blue1.drawString(g, string.Concat(new object[]
							{
						num8,
						".",
						char6.cName,
						"[ ",
						NinjaUtil.getMoneys((long)char6.cHP).ToString(),
						" ] [ ",
						hanhTinhNhanVat(char6),
						" ]"
							}), GameCanvas.w - 150, num7, mFont.LEFT);
						}
						else
						{
							switch (char6.cgender)
							{
								case 0:
									mFont.tahoma_7_blue1.drawStringBd(g, string.Concat(new object[]
							{
						num8,
						".",
						char6.cName,
						"[ ",
						NinjaUtil.getMoneys((long)char6.cHP).ToString(),
						" ] [ ",
						hanhTinhNhanVat(char6),
						" ]"
							}), GameCanvas.w - 150, num7, mFont.LEFT, mFont.tahoma_7_grey);

									break;
								case 1:
									mFont.tahoma_7_green.drawStringBd(g, string.Concat(new object[]
							{
						num8,
						".",
						char6.cName,
						"[ ",
						NinjaUtil.getMoneys((long)char6.cHP).ToString(),
						" ] [ ",
						hanhTinhNhanVat(char6),
						" ]"
							}), GameCanvas.w - 150, num7, mFont.LEFT, mFont.tahoma_7_grey);
									break;
								case 2:
									mFont.tahoma_7b_yellowSmall2.drawStringBd(g, string.Concat(new object[]
							{
						num8,
						".",
						char6.cName,
						"[ ",
						NinjaUtil.getMoneys((long)char6.cHP).ToString(),
						" ] [ ",
						hanhTinhNhanVat(char6),
						" ]"
							}), GameCanvas.w -150, num7, mFont.LEFT, mFont.tahoma_7_grey);
									break;
							}

							}
                     
                        
						chars[num8] = char6;
						num7 += 10;
					}
				}
			}

				for (int k = 0; k < GameScr.vCharInMap.size(); k++)
				{
					int num4 = 72;
					global::Char char2 = (global::Char)GameScr.vCharInMap.elementAt(k);
					if (char2 != null && char2 == global::Char.myCharz().charFocus)
					{
						mFont.tahoma_7b_red.drawString(g, string.Concat(new object[]
						{
						char2.cName,
						" [",
						NinjaUtil.getMoneys((long)char2.cHP),
						" / ",
						NinjaUtil.getMoneys((long)char2.cHPFull),
						"] [",
						Mod.hanhTinhNhanVat(char2),
						"]"
						}), GameCanvas.w / 2, num4, mFont.CENTER);
						num4 += 10;
						if (char2.protectEff)
						{
							mFont.tahoma_7b_red.drawString(g, "Đang khiên: " + char2.timeProtectEff.ToString(), GameCanvas.w / 2, num4, mFont.CENTER);
							num4 += 10;
						}
						if (char2.isMonkey == 1)
						{
							mFont.tahoma_7b_red.drawString(g, "Đang biến khỉ: " + char2.timeMonkey.ToString(), GameCanvas.w / 2, num4, mFont.CENTER);
							num4 += 10;
						}
						if (char2.sleepEff)
						{
							mFont.tahoma_7b_red.drawString(g, "Bị thôi miên: " + char2.timeSleep.ToString(), GameCanvas.w / 2, num4, mFont.CENTER);
							num4 += 10;
						}
						if (char2.holdEffID != 0)
						{
							mFont.tahoma_7b_red.drawString(g, "Bị trói: " + char2.timeBiTroi.ToString(), GameCanvas.w / 2, num4, mFont.CENTER);
							num4 += 10;
						}
						if (char2.isFreez)
						{
							mFont.tahoma_7b_red.drawString(g, "Bị TDHS: " + char2.freezSeconds.ToString(), GameCanvas.w / 2, num4, mFont.CENTER);
							num4 += 10;
						}
						if (char2.blindEff)
						{
							mFont.tahoma_7b_red.drawString(g, "Bị choáng: " + char2.timeBlind.ToString(), GameCanvas.w / 2, num4, mFont.CENTER);
							num4 += 10;
						}
						if (char2.timeHuytSao > 0)
						{
							mFont.tahoma_7b_red.drawString(g, "Có huýt sáo: " + char2.timeHuytSao.ToString(), GameCanvas.w / 2, num4, mFont.CENTER);
							num4 += 10;
						}
						if (char2.isNRD)
						{
							mFont.tahoma_7b_red.drawString(g, "Ôm NRD còn: " + char2.timeNRD.ToString(), GameCanvas.w / 2, num4, mFont.CENTER);
							num4 += 10;
						}
					}
				
			}
            

			var logo = mSystem.loadImage("/mainImage/logo1E.png");
            g.drawImage(logo, GameCanvas.w / 2, 35, 3);
            paintString(g);
            if (jkl)
            {

				
					int y = 90;
					mFont mFont1 = (!GameCanvas.lowGraphic ? mFont.tahoma_7b_white : mFont.tahoma_7);
					g.fillRect(440, y + 60, 18, 12, 0, 90);
					mFont.tahoma_7b_red.drawStringBorder(g, "J", 445, y + 60, 0,mFont.tahoma_7);

					g.fillRect(460, y + 60, 18, 12, 0, 90);
				mFont.tahoma_7b_red.drawStringBorder(g, "K", 465, y + 60, 0, mFont.tahoma_7);

					g.fillRect(480, y + 60, 18, 12, 0, 90);
				mFont.tahoma_7b_red.drawStringBorder(g, "L", 485, y + 60, 0, mFont.tahoma_7);

				

			}
		}
		public static bool jkl;
	public static void paintString(mGraphics g) {
            //mFont.tahoma_7b_green.drawStringBorder(g, "Ngọc Rồng Mabu", GameCanvas.w / 2, 41, 3, mFont.tahoma_7);
        }
		public static bool trangThai = true;
		public static bool MapNRD()
		{
			return TileMap.mapID >= 85 && TileMap.mapID <= 91;
		}
		public static string hanhTinhNhanVat(global::Char @char)
		{
			string result;
			if (@char.cTypePk == 5)
			{
				result = "BOSS";
			}
			else if (@char.cgender == 0)
			{
				result = "TD";
			}
			else if (@char.cgender == 1)
			{
				result = "NM";
			}
			else if (@char.cgender == 2)
			{
				result = "XD";
			}
			else
			{
				result = "";
			}
			return result;
		}
		public static bool FlagInMap()
		{
			for (int i = 0; i < GameScr.vCharInMap.size(); i++)
			{
				global::Char @char = (global::Char)GameScr.vCharInMap.elementAt(i);
				if (@char != null && @char.cFlag != 0 && @char.charID > 0 && ((global::Math.abs(@char.cx - global::Char.myCharz().cx) <= 500 && global::Math.abs(@char.cy - global::Char.myCharz().cy) <= 500) || (global::Math.abs(@char.cx - Mod.myPetInMap().cx) <= 500 && global::Math.abs(@char.cy - Mod.myPetInMap().cy) <= 500)))
				{
					return true;
				}
			}
			return false;
		}
		public static global::Char myPetInMap()
		{
			for (int i = 0; i < GameScr.vCharInMap.size(); i++)
			{
				global::Char @char = (global::Char)GameScr.vCharInMap.elementAt(i);
				if (@char != null && @char.charID == -global::Char.myCharz().charID)
				{
					return @char;
				}
			}
			return null;
		}
		public static bool Button;
		private static void PaintButton(mGraphics g) {

			var buttonC = mSystem.loadImage("/mainImage/clickC.png");
			g.drawImage(buttonC, GameCanvas.w - 35, 100 );
			var buttonS = mSystem.loadImage("/mainImage/clickS.png");
			g.drawImage(buttonS, GameCanvas.w - 35, 130);
			var buttonM = mSystem.loadImage("/mainImage/clickM.png");
			g.drawImage(buttonM, GameCanvas.w - 70, 100);
			var buttonV = mSystem.loadImage("/mainImage/clickV.png");
			g.drawImage(buttonV, GameCanvas.w - 70, 130);
			var buttonH = mSystem.loadImage("/mainImage/clickH.png");
			g.drawImage(buttonH, GameCanvas.w - 80, 162);
			var buttonG = mSystem.loadImage("/mainImage/clickG.png");
			g.drawImage(buttonG, GameCanvas.w - 130, 185);
			var buttonA = mSystem.loadImage("/mainImage/clickA.png");
			g.drawImage(buttonA, GameCanvas.w - 100, 185);
			var buttonE = mSystem.loadImage("/mainImage/clickE.png");
			g.drawImage(buttonE, GameCanvas.w - 160, 220 - 2);
			var buttonF = mSystem.loadImage("/mainImage/clickF.png");
			g.drawImage(buttonF, GameCanvas.w - 130, 220 - 2);
			var buttonJ = mSystem.loadImage("/mainImage/clickJ.png");
			
		}
		public static string dau = "Đậu";
		public static bool xoamap;
		public static bool isAutoNeBoss;
		public static global::Char[] chars = new global::Char[50];
		public static bool lineboss;
		// Token: 0x040012EC RID: 4844
		private static long currNeBoss;
		// Token: 0x040012DD RID: 4829
		public static string doHoa = "Đồ Họa";
		private static long currAutoFlag;

		// Token: 0x040012EE RID: 4846
		public static bool isAutoCo;
		// Token: 0x040012DE RID: 4830
		public static string hoTroUpDe = "Up đệ";
		public static bool nvat = true;
		// Token: 0x040012DF RID: 4831
		public static string upYardrat = "Auto Yardrat";

		// Token: 0x040012E0 RID: 4832
		public static string hoTroSanBoss = "Boss";

		// Token: 0x040012E1 RID: 4833
		public static string chucNangKhac = "Khác";
		public static bool dangLogin;
		public static bool hoiSinhNgoc;
		public static void HoiSinhTheoNgocChiDinh()
		{
			if (Mod.ngocHienTai == 0 || global::Char.myCharz().luongKhoa + global::Char.myCharz().luong == 0 || Mod.ngocDuocDungDeHoiSinh == 0)
			{
				GameScr.info1.addInfo("Không còn ngọc để hồi sinh hoặc chưa set up số ngọc được phép sử dụng hoặc số ngọc được hồi sinh đã dùng hết", 0);
				GameScr.info1.addInfo("Đã tắt tự hồi sinh với số ngọc chỉ định", 0);
				Mod.hoiSinhNgoc = false;
			}
			if (Mod.ngocDuocDungDeHoiSinh > 0 && global::Char.myCharz().cHP <= 0 && global::Char.myCharz().statusMe == 14)
			{
				Service.gI().wakeUpFromDead();
				Mod.ngocDuocDungDeHoiSinh--;
			}
		}
		private static long currHoiSinh;
		public static int ngocDuocDungDeHoiSinh;

		// Token: 0x040012B5 RID: 4789
		public static int ngocHienTai = 0;
		public static void AutoLogin()
		{
			Mod.dangLogin = true;
			Thread.Sleep(1000);
			GameCanvas.startOKDlg("Vui Lòng Đợi 25 Giây...");
			Thread.Sleep(23000);
			while (ServerListScreen.testConnect != 2)
			{
				GameCanvas.serverScreen.switchToMe();
				Thread.Sleep(1000);
			}
			if (GameCanvas.loginScr == null)
			{
				GameCanvas.loginScr = new LoginScr();
			}
			Thread.Sleep(1000);
			GameCanvas.loginScr.switchToMe();
			GameCanvas.loginScr.doLogin();
			Mod.dangLogin = false;
		}
		public static bool isAutoVeKhu;

		// Token: 0x040012D0 RID: 4816
		private static long currVeKhuCu;
		public static bool isAutoLogin;
		// Token: 0x040012E2 RID: 4834
		public static string tdLT = "TĐLT";
		public static bool giamDungLuong;
		public static bool focusBoss;
		// Token: 0x040012E3 RID: 4835
		public static string autoSkill = "Auto Skill";
		public static bool xindau;
		public static void autoFocusBoss()
		{
			if (Mod.focusBoss && mSystem.currentTimeMillis() - Mod.currFocusBoss >= 500L)
			{
				Mod.currFocusBoss = mSystem.currentTimeMillis();
				for (int i = 0; i < GameScr.vCharInMap.size(); i++)
				{
					global::Char @char = (global::Char)GameScr.vCharInMap.elementAt(i);
					if (@char != null && @char.cTypePk == 5 && !@char.cName.StartsWith("Đ"))
					{
						global::Char.myCharz().focusManualTo(@char);
						return;
					}
				}
			}
		}
		private static long currFocusBoss;
		// Token: 0x040012E6 RID: 4838
		public static bool thudau;
		public static int ghimX;
		public static bool IsGoBack;
		public static int IdmapGB;

		// Token: 0x040012FC RID: 4860
		public static int ZoneGB;
		// Token: 0x040012A5 RID: 4773
		public static int ghimY;

		// Token: 0x040012A6 RID: 4774
		public static bool isKhoaViTri;

		// Token: 0x040012A7 RID: 4775
		private static long currKhoaViTri;
		public static void khoaViTri()
		{
			if (Mod.isKhoaViTri && mSystem.currentTimeMillis() - Mod.currKhoaViTri >= 600L && global::Char.myCharz().statusMe != 14 && global::Char.myCharz().cHP > 0)
			{
				Mod.currKhoaViTri = mSystem.currentTimeMillis();
				global::Char.myCharz().cx = Mod.ghimX;
				global::Char.myCharz().cy = Mod.ghimY;
				Service.gI().charMove();
			}
		}
		public static void GoBack()
		{
			Thread.Sleep(5000);
			if (!GameScr.gI().magicTree.isUpdate && GameScr.gI().magicTree.currPeas > 0 && TileMap.mapID == global::Char.myCharz().cgender + 21)
			{
				Service.gI().magicTree(1);
				Thread.Sleep(500);
				GameCanvas.gI().keyPressedz(-5);
				Thread.Sleep(1000);
			}
			for (int i = 0; i < GameScr.vItemMap.size(); i++)
			{
				ItemMap itemMap = (ItemMap)GameScr.vItemMap.elementAt(i);
				global::Char.myCharz().cx = itemMap.x;
				Service.gI().charMove();
				Thread.Sleep(1000);
				Service.gI().pickItem(itemMap.itemMapID);
				Thread.Sleep(1000);
			}
			Xmap.StartRunToMapId(Mod.IdmapGB);
			while (TileMap.mapID != Mod.IdmapGB)
			{
				Thread.Sleep(100);
			}
			new Thread(new ThreadStart(Mod.DoiLaiKhu)).Start();
			GameScr.isAutoPlay = true;
		}
		public static void DoiLaiKhu()
		{
			while (TileMap.zoneID != Mod.ZoneGB)
			{
				Thread.Sleep(1000);
				Service.gI().requestChangeZone(Mod.ZoneGB, -1);
			}
			Thread.Sleep(2000);
			Mod.GotoXY(Mod.xGB, Mod.yGB);
			GameScr.isAutoPlay = true;
		}
		public static int xGB;

		// Token: 0x040012F9 RID: 4857
		public static int yGB;
		// Token: 0x040012E7 RID: 4839
		private static long currThuDau;

		// Token: 0x040012E8 RID: 4840
		public static bool chodau;
		private static long currXinDau;
		private static long currChoDau;
		public static bool isAutoTTNL;

		// Token: 0x040012DB RID: 4827
		private static long currAutoTTNL;
		public static void AutoTTNL()
		{
			if (Mod.isAutoTTNL && mSystem.currentTimeMillis() - Mod.currAutoTTNL >= 1000L)
			{
				Mod.currAutoTTNL = mSystem.currentTimeMillis();
				if (global::Char.myCharz().cgender != 2)
				{
					GameScr.info1.addInfo("Hành tinh acc này không phải xayda", 0);
					GameScr.info1.addInfo("Auto ttnl đang tắt", 0);
					Mod.isAutoTTNL = false;
					return;
				}
				if (!global::Char.myCharz().isStandAndCharge && !global::Char.myCharz().isCharge && !global::Char.myCharz().isFlyAndCharge && !global::Char.myCharz().stone && global::Char.myCharz().holdEffID == 0 && !global::Char.myCharz().blindEff && !global::Char.myCharz().sleepEff && global::Char.myCharz().cHP > 0 && global::Char.myCharz().statusMe != 14)
				{
					if ((global::Char.myCharz().cHP < global::Char.myCharz().cHPFull || global::Char.myCharz().cMP < global::Char.myCharz().cMPFull) && Mod.GetCoolDownSkill(Mod.GetSkillByIconID(720)) <= 0)
					{
						Mod.UseSkill(Mod.GetSkillByIconID(720));
						return;
					}
					if (global::Char.myCharz().myskill != Mod.GetSkillByIconID(539))
					{
						GameScr.gI().doSelectSkill(Mod.GetSkillByIconID(539), true);
					}
				}
			}
		}
		public static void UseSkill(Skill sk)
		{
			if (global::Char.myCharz().myskill != sk)
			{
				GameScr.gI().doSelectSkill(sk, true);
				GameScr.gI().doSelectSkill(sk, true);
				return;
			}
			GameScr.gI().doSelectSkill(sk, true);
		}
		public static int GetCoolDownSkill(Skill skill)
		{
			return (int)((long)skill.coolDown - mSystem.currentTimeMillis() + skill.lastTimeUseThisSkill);
		}
		public static Skill GetSkillByIconID(int iconID)
		{
			for (int i = 0; i < GameScr.keySkill.Length; i++)
			{
				if (GameScr.keySkill[i] != null && GameScr.keySkill[i].template.iconId == iconID)
				{
					return GameScr.keySkill[i];
				}
			}
			return null;
		}

	}
	
}
