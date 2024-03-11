using System;

// Token: 0x020000CF RID: 207
public class TransportScr : mScreen, IActionListener
{
	// Token: 0x06000A7B RID: 2683 RVA: 0x000A27F8 File Offset: 0x000A09F8
	public TransportScr()
	{
		this.posX = new int[this.n];
		this.posY = new int[this.n];
		for (int i = 0; i < this.n; i++)
		{
			this.posX[i] = Res.random(0, GameCanvas.w);
			this.posY[i] = i * (GameCanvas.h / this.n);
		}
		this.posX2 = new int[this.n];
		this.posY2 = new int[this.n];
		for (int j = 0; j < this.n; j++)
		{
			this.posX2[j] = Res.random(0, GameCanvas.w);
			this.posY2[j] = j * (GameCanvas.h / this.n);
		}
	}

	// Token: 0x06000A7C RID: 2684 RVA: 0x000A28E8 File Offset: 0x000A0AE8
	public static TransportScr gI()
	{
		bool flag = TransportScr.instance == null;
		if (flag)
		{
			TransportScr.instance = new TransportScr();
		}
		return TransportScr.instance;
	}

	// Token: 0x06000A7D RID: 2685 RVA: 0x000A2918 File Offset: 0x000A0B18
	public override void switchToMe()
	{
		bool flag = TransportScr.ship == null;
		if (flag)
		{
			TransportScr.ship = GameCanvas.loadImage("/mainImage/myTexture2dfutherShip.png");
		}
		bool flag2 = TransportScr.taungam == null;
		if (flag2)
		{
			TransportScr.taungam = GameCanvas.loadImage("/mainImage/taungam.png");
		}
		this.isSpeed = false;
		this.transNow = false;
		bool flag3 = global::Char.myCharz().checkLuong() > 0 && this.type == 0;
		if (flag3)
		{
			this.center = new Command(mResources.faster, this, 1, null);
		}
		else
		{
			this.center = null;
		}
		this.currSpeed = 0;
		base.switchToMe();
	}

	// Token: 0x06000A7E RID: 2686 RVA: 0x00003A11 File Offset: 0x00001C11
	public override void paint(mGraphics g)
	{
	}

	// Token: 0x06000A7F RID: 2687 RVA: 0x000A29BC File Offset: 0x000A0BBC
	public override void update()
	{
		this.currSpeed = 100000;
		Controller.isStopReadMessage = false;
		this.cmx = (((GameCanvas.w / 2 + this.cmx) / 2 + this.cmx) / 2 + this.cmx) / 2;
		bool flag = this.type == 1;
		if (flag)
		{
			this.cmx = 0;
		}
		for (int i = 0; i < this.n; i++)
		{
			this.posX[i] -= this.speed / 2;
			bool flag2 = this.posX[i] < -20;
			if (flag2)
			{
				this.posX[i] = GameCanvas.w;
			}
		}
		for (int j = 0; j < this.n; j++)
		{
			this.posX2[j] -= this.speed;
			bool flag3 = this.posX2[j] < -20;
			if (flag3)
			{
				this.posX2[j] = GameCanvas.w;
			}
		}
		bool flag4 = GameCanvas.gameTick % 3 == 0;
		if (flag4)
		{
			this.speed += ((!this.isSpeed) ? 1 : 2);
		}
		bool flag5 = this.speed > ((!this.isSpeed) ? 25 : 80);
		if (flag5)
		{
			this.speed = ((!this.isSpeed) ? 25 : 80);
		}
		this.curr = mSystem.currentTimeMillis();
		bool flag6 = this.curr - this.last >= 1000L;
		if (flag6)
		{
			this.time += 1;
			this.last = this.curr;
		}
		bool flag7 = this.isSpeed;
		if (flag7)
		{
			this.currSpeed += 18;
		}
		bool flag8 = this.currSpeed >= GameCanvas.w / 2 + 30 && !this.transNow;
		if (flag8)
		{
			this.transNow = true;
			Service.gI().transportNow();
		}
		base.update();
	}

	// Token: 0x06000A80 RID: 2688 RVA: 0x00006645 File Offset: 0x00004845
	public override void updateKey()
	{
		base.updateKey();
	}

	// Token: 0x06000A81 RID: 2689 RVA: 0x000A2BC0 File Offset: 0x000A0DC0
	public void perform(int idAction, object p)
	{
		bool flag = idAction == 1;
		if (flag)
		{
			GameCanvas.startYesNoDlg(mResources.fasterQuestion, new Command(mResources.YES, this, 2, null), new Command(mResources.NO, this, 3, null));
		}
		bool flag2 = idAction == 2 && global::Char.myCharz().checkLuong() > 0;
		if (flag2)
		{
			this.isSpeed = true;
			GameCanvas.endDlg();
			this.center = null;
		}
		bool flag3 = idAction == 3;
		if (flag3)
		{
			GameCanvas.endDlg();
		}
	}

	// Token: 0x06000A82 RID: 2690 RVA: 0x0000664F File Offset: 0x0000484F
	public void GotoFuture()
	{
		this.isSpeed = true;
		this.center = null;
	}

	// Token: 0x04001357 RID: 4951
	public static TransportScr instance;

	// Token: 0x04001358 RID: 4952
	public static Image ship;

	// Token: 0x04001359 RID: 4953
	public static Image taungam;

	// Token: 0x0400135A RID: 4954
	public sbyte type;

	// Token: 0x0400135B RID: 4955
	public int speed = 5;

	// Token: 0x0400135C RID: 4956
	public int[] posX;

	// Token: 0x0400135D RID: 4957
	public int[] posY;

	// Token: 0x0400135E RID: 4958
	public int[] posX2;

	// Token: 0x0400135F RID: 4959
	public int[] posY2;

	// Token: 0x04001360 RID: 4960
	private int cmx;

	// Token: 0x04001361 RID: 4961
	private int n = 20;

	// Token: 0x04001362 RID: 4962
	public short time;

	// Token: 0x04001363 RID: 4963
	public short maxTime;

	// Token: 0x04001364 RID: 4964
	public long last;

	// Token: 0x04001365 RID: 4965
	public long curr;

	// Token: 0x04001366 RID: 4966
	private bool isSpeed = true;

	// Token: 0x04001367 RID: 4967
	private bool transNow;

	// Token: 0x04001368 RID: 4968
	private int currSpeed;
}
