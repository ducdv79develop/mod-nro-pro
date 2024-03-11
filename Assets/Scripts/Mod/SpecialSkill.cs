using System;
using System.Threading;

// Token: 0x0200012F RID: 303
namespace Mod
{
	internal class SpecialSkill : IActionListener, IChatable
	{
		// Token: 0x06000BC7 RID: 3015 RVA: 0x000AFF5C File Offset: 0x000AE15C
		public static SpecialSkill gI()
		{
			SpecialSkill result;
			if (SpecialSkill.instance != null)
			{
				result = SpecialSkill.instance;
			}
			else
			{
				result = (SpecialSkill.instance = new SpecialSkill());
			}
			return result;
		}

		// Token: 0x06000BC8 RID: 3016 RVA: 0x000AFF88 File Offset: 0x000AE188
		public void open()
		{
			if (global::Char.myCharz().cPower < 10000000000L)
			{
				GameScr.info1.addInfo("Cần 10 tỉ sức mạnh để mở.", 0);
				return;
			}
			if (SpecialSkill.openMax && SpecialSkill.max == -1)
			{
				this.isnoitai = false;
				SpecialSkill.openMax = false;
				return;
			}
			do
			{
				if (global::Char.myCharz().luong < 1000)
				{
					if (this.cgender == 0)
					{
						this.isnoitai = false;
						Service.gI().openMenu(0);
						Service.gI().confirmMenu(0, 2);
						this.isnoitai = true;
					}
					if (this.cgender == 1)
					{
						this.isnoitai = false;
						Service.gI().openMenu(2);
						Service.gI().confirmMenu(2, 2);
						this.isnoitai = true;
					}
					if (this.cgender == 3)
					{
						this.isnoitai = false;
						Service.gI().openMenu(1);
						Service.gI().confirmMenu(1, 2);
						this.isnoitai = true;
					}
				}
				Service.gI().speacialSkill(0);
				if (Panel.specialInfo.Contains(SpecialSkill.tennoitaicanmo))
				{
					if (!SpecialSkill.openMax)
					{
						goto IL_18D;
					}
					int num = Panel.specialInfo.IndexOf("%");
					int num2 = Panel.specialInfo.Substring(0, num).LastIndexOf(' ');
					if (int.Parse(this.CutString(num2 + 1, num - 1, Panel.specialInfo)) == SpecialSkill.max)
					{
						goto IL_1A5;
					}
				}
				Thread.Sleep(200);
				Service.gI().confirmMenu(5, SpecialSkill.type);
				Thread.Sleep(200);
				Service.gI().confirmMenu(5, 0);
				Thread.Sleep(400);
			}
			while (this.isnoitai);
			return;
		IL_18D:
			this.isnoitai = false;
			GameScr.info1.addInfo("Xong", 0);
			return;
		IL_1A5:
			this.isnoitai = false;
			SpecialSkill.openMax = false;
			GameScr.info1.addInfo("Xong", 0);
		}

		// Token: 0x06000BC9 RID: 3017 RVA: 0x000B0158 File Offset: 0x000AE358
		public string CutString(int start, int end, string s)
		{
			string text = "";
			for (int i = start; i <= end; i++)
			{
				text += s[i].ToString();
			}
			return text;
		}

		// Token: 0x06000BCA RID: 3018 RVA: 0x000B0190 File Offset: 0x000AE390
		public void perform(int idAction, object p)
		{
			if (idAction != 1)
			{
				if (idAction == 2)
				{
					string text = (string)p;
					int length = text.Substring(0, text.IndexOf('%')).LastIndexOf(' ');
					SpecialSkill.tennoitaicanmo = text.Substring(0, length);
					this.isnoitai = true;
					SpecialSkill.type = (sbyte)idAction;
					GameCanvas.panel.hide();
					new Thread(new ThreadStart(this.open)).Start();
				}
			}
			else
			{
				string text2 = (string)p;
				int length2 = text2.Substring(0, text2.IndexOf('%')).LastIndexOf(' ');
				SpecialSkill.tennoitaicanmo = text2.Substring(0, length2);
				this.isnoitai = true;
				SpecialSkill.type = (sbyte)idAction;
				GameCanvas.panel.hide();
				new Thread(new ThreadStart(this.open)).Start();
			}
			if (idAction == 3)
			{
				SpecialSkill.openMax = false;
				MyVector myVector = new MyVector();
				myVector.addElement(new Command("Mở Vip", SpecialSkill.gI(), 2, p));
				myVector.addElement(new Command("Mở Thường", SpecialSkill.gI(), 1, p));
				GameCanvas.menu.startAt(myVector, 3);
			}
			if (idAction == 4)
			{
				string text3 = (string)p;
				SpecialSkill.openMax = true;
				int num = text3.IndexOf("đến ");
				int length3 = text3.Substring(num + 4).IndexOf("%");
				SpecialSkill.max = int.Parse(text3.Substring(num + 4, length3));
				MyVector myVector2 = new MyVector();
				myVector2.addElement(new Command("Mở Vip", SpecialSkill.gI(), 2, p));
				myVector2.addElement(new Command("Mở Thường", SpecialSkill.gI(), 1, p));
				GameCanvas.menu.startAt(myVector2, 3);
			}
			if (idAction == 5)
			{
				MyVector myVector3 = new MyVector();
				for (int i = 1; i <= 8; i++)
				{
					myVector3.addElement(new Command(i.ToString() + " sao", this, 7, i));
				}
				GameCanvas.menu.startAt(myVector3, 3);
			}
			if (idAction == 6)
			{
				Service.gI().combine(1, GameCanvas.panel.vItemCombine);
			}
			if (idAction == 8)
			{
				string text4 = (string)p;
				int length4 = text4.Substring(0, text4.IndexOf('%')).LastIndexOf(' ');
				SpecialSkill.tennoitaicanmo = text4.Substring(0, length4);
				int num2 = text4.IndexOf("%");
				int num3 = text4.IndexOf("đến ");
				int start = text4.Substring(0, num2).LastIndexOf(' ');
				int num4 = text4.LastIndexOf('%');
				SpecialSkill.chiso[0] = int.Parse(this.CutString(start, num2 - 1, text4));
				SpecialSkill.chiso[1] = int.Parse(this.CutString(num3 + 4, num4 - 1, text4));
				string str = this.CutString(start, num4, text4);
				SpecialSkill.noitai = "Nhập chỉ số bạn muốn chọn trong khoảng " + str;
				MyVector myVector4 = new MyVector();
				myVector4.addElement(new Command("Mở Vip", SpecialSkill.gI(), 9, 2));
				myVector4.addElement(new Command("Mở Thường", SpecialSkill.gI(), 9, 1));
				GameCanvas.menu.startAt(myVector4, 3);
			}
			if (idAction == 9)
			{
				SpecialSkill.type = (sbyte)((int)p);
				SpecialSkill.startChat(1, SpecialSkill.noitai);
			}
		}

		// Token: 0x06000BCB RID: 3019 RVA: 0x000B04D4 File Offset: 0x000AE6D4
		public void onChatFromMe(string text, string to)
		{
			if (GameCanvas.panel.chatTField.strChat == SpecialSkill.noitai)
			{
				int num = -1;
				try
				{
					num = int.Parse(GameCanvas.panel.chatTField.tfChat.getText());
				}
				catch (Exception)
				{
					GameCanvas.startOKDlg(mResources.input_quantity_wrong);
					GameCanvas.panel.chatTField.isShow = false;
					GameCanvas.panel.chatTField.tfChat.setIputType(TField.INPUT_TYPE_ANY);
					return;
				}
				if (num != -1 && num >= SpecialSkill.chiso[0] && num <= SpecialSkill.chiso[1])
				{
					SpecialSkill.max = num;
					SpecialSkill.openMax = true;
					SpecialSkill.gI().isnoitai = true;
					GameCanvas.panel.hide();
					new Thread(new ThreadStart(SpecialSkill.gI().open)).Start();
					GameCanvas.panel.chatTField.isShow = false;
					GameCanvas.panel.chatTField.tfChat.setIputType(TField.INPUT_TYPE_ANY);
				}
				else
				{
					GameCanvas.startOKDlg(SpecialSkill.noitai);
				}
			}
			GameCanvas.panel.chatTField.parentScreen = GameCanvas.panel;
		}

		// Token: 0x06000BCC RID: 3020 RVA: 0x00008AAE File Offset: 0x00006CAE
		public void onCancelChat()
		{
			GameCanvas.panel.chatTField.tfChat.setIputType(TField.INPUT_TYPE_ANY);
		}

		// Token: 0x06000BCD RID: 3021 RVA: 0x000B0604 File Offset: 0x000AE804
		public static void startChat(int type, string caption)
		{
			if (GameCanvas.panel.chatTField == null)
			{
				GameCanvas.panel.chatTField = new ChatTextField();
				GameCanvas.panel.chatTField.tfChat.y = GameCanvas.h - 35 - ChatTextField.gI().tfChat.height;
				GameCanvas.panel.chatTField.initChatTextField();
				GameCanvas.panel.chatTField.parentScreen = SpecialSkill.gI();
			}
			GameCanvas.panel.chatTField.parentScreen = SpecialSkill.gI();
			ChatTextField chatTField = GameCanvas.panel.chatTField;
			chatTField.strChat = caption;
			chatTField.tfChat.name = mResources.input_quantity;
			chatTField.to = string.Empty;
			chatTField.isShow = true;
			chatTField.tfChat.isFocus = true;
			chatTField.tfChat.setIputType(type);
			if (GameCanvas.isTouch)
			{
				GameCanvas.panel.chatTField.tfChat.doChangeToTextBox();
			}
			if (!Main.isPC)
			{
				chatTField.startChat2(SpecialSkill.gI(), string.Empty);
			}
		}

		// Token: 0x06000BCE RID: 3022 RVA: 0x00008AC9 File Offset: 0x00006CC9
		public void In4Me(string s)
		{
			if (s.ToLower().Contains("bạn không đủ vàng"))
			{
				SpecialSkill.gI().isnoitai = false;
				SpecialSkill.openMax = false;
			}
		}

		// Token: 0x06000BD1 RID: 3025 RVA: 0x000B0710 File Offset: 0x000AE910
		public static void AddChatPopup(string[] s)
		{
			if (s.Length != 0 && s[s.Length - 1] != string.Empty)
			{
				string text = s[s.Length - 1].ToLower();
				int num = text.IndexOf("cần ");
				int num2 = text.IndexOf("tr");
				if (num != -1 && num2 != -1)
				{
					int.Parse(SpecialSkill.gI().CutString(num + 3, num2 - 1, text).Trim());
				}
			}
		}

		// Token: 0x06000BD2 RID: 3026 RVA: 0x000B0780 File Offset: 0x000AE980
		public static void startMenu()
		{
			MyVector myVector = new MyVector();
			myVector.addElement(new Command("Thường", SpecialSkill.gI(), 6, null));
			GameCanvas.menu.startAt(myVector, 3);
		}

		// Token: 0x06000BD3 RID: 3027 RVA: 0x000B07B8 File Offset: 0x000AE9B8
		public void onChatFromMee(string text, string to)
		{
			if (GameCanvas.panel.chatTField.strChat == SpecialSkill.noitai)
			{
				int num = -1;
				try
				{
					num = int.Parse(GameCanvas.panel.chatTField.tfChat.getText());
				}
				catch (Exception)
				{
					GameCanvas.startOKDlg(mResources.input_quantity_wrong);
					GameCanvas.panel.chatTField.isShow = false;
					GameCanvas.panel.chatTField.tfChat.setIputType(TField.INPUT_TYPE_ANY);
					return;
				}
				if (num != -1 && num >= SpecialSkill.chiso[0] && num <= SpecialSkill.chiso[1])
				{
					SpecialSkill.max = num;
					SpecialSkill.openMax = true;
					SpecialSkill.gI().isnoitai = true;
					GameCanvas.panel.hide();
					new Thread(new ThreadStart(SpecialSkill.gI().open)).Start();
					GameCanvas.panel.chatTField.isShow = false;
					GameCanvas.panel.chatTField.tfChat.setIputType(TField.INPUT_TYPE_ANY);
				}
				else
				{
					GameCanvas.startOKDlg(SpecialSkill.noitai);
				}
			}
			GameCanvas.panel.chatTField.parentScreen = GameCanvas.panel;
		}

		// Token: 0x0400166C RID: 5740
		private int cgender = global::Char.myCharz().cgender;

		// Token: 0x0400166D RID: 5741
		public static string noitai;

		// Token: 0x0400166E RID: 5742
		public static string tennoitaicanmo;

		// Token: 0x0400166F RID: 5743
		public static sbyte type = 1;

		// Token: 0x04001670 RID: 5744
		public static bool openMax = false;

		// Token: 0x04001671 RID: 5745
		public static int max = -1;

		// Token: 0x04001672 RID: 5746
		public static int[] chiso = new int[2];

		// Token: 0x04001673 RID: 5747
		private static SpecialSkill instance;

		// Token: 0x04001674 RID: 5748
		public bool isnoitai;
	}
}