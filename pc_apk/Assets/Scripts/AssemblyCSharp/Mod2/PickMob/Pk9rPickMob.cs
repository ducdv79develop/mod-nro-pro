using System;
using System.Collections.Generic;
using System.Text;

namespace AssemblyCSharp.Mod2.PickMob
{
	// Token: 0x020000C9 RID: 201
	public class Pk9rPickMob
	{
		// Token: 0x06000A36 RID: 2614 RVA: 0x000A32DC File Offset: 0x000A14DC
		public static bool Chat(string text)
		{
			bool flag = text == "add";
			if (flag)
			{
				Mob mobFocus = global::Char.myCharz().mobFocus;
				ItemMap itemFocus = global::Char.myCharz().itemFocus;
				bool flag2 = mobFocus != null;
				if (flag2)
				{
					bool flag3 = Pk9rPickMob.IdMobsTanSat.Contains(mobFocus.mobId);
					if (flag3)
					{
						Pk9rPickMob.IdMobsTanSat.Remove(mobFocus.mobId);
						GameScr.info1.addInfo("Đã xoá mob: " + mobFocus.mobId.ToString(), 0);
					}
					else
					{
						Pk9rPickMob.IdMobsTanSat.Add(mobFocus.mobId);
						GameScr.info1.addInfo("Đã thêm mob: " + mobFocus.mobId.ToString(), 0);
					}
				}
				else
				{
					bool flag4 = itemFocus != null;
					if (flag4)
					{
						bool flag5 = Pk9rPickMob.IdItemPicks.Contains(itemFocus.template.id);
						if (flag5)
						{
							Pk9rPickMob.IdItemPicks.Remove(itemFocus.template.id);
							GameScr.info1.addInfo(string.Format("Đã xoá khỏi danh sách chỉ tự động nhặt item: {0}[{1}]", itemFocus.template.name, itemFocus.template.id), 0);
						}
						else
						{
							Pk9rPickMob.IdItemPicks.Add(itemFocus.template.id);
							GameScr.info1.addInfo(string.Format("Đã thêm vào danh sách chỉ tự động nhặt item: {0}[{1}]", itemFocus.template.name, itemFocus.template.id), 0);
						}
					}
					else
					{
						GameScr.info1.addInfo("Cần trỏ vào quái hay vật phẩm cần thêm vào danh sách", 0);
					}
				}
			}
			else
			{
				bool flag6 = text == "addd";
				if (flag6)
				{
					Mob mobFocus2 = global::Char.myCharz().mobFocus;
					ItemMap itemFocus2 = global::Char.myCharz().itemFocus;
					bool flag7 = mobFocus2 != null;
					if (flag7)
					{
						bool flag8 = Pk9rPickMob.TypeMobsTanSat.Contains(mobFocus2.templateId);
						if (flag8)
						{
							Pk9rPickMob.TypeMobsTanSat.Remove(mobFocus2.templateId);
							GameScr.info1.addInfo(string.Format("Đã xoá loại mob: {0}[{1}]", mobFocus2.getTemplate().name, mobFocus2.templateId), 0);
						}
						else
						{
							Pk9rPickMob.TypeMobsTanSat.Add(mobFocus2.templateId);
							GameScr.info1.addInfo(string.Format("Đã thêm loại mob: {0}[{1}]", mobFocus2.getTemplate().name, mobFocus2.templateId), 0);
						}
					}
					else
					{
						bool flag9 = itemFocus2 != null;
						if (flag9)
						{
							bool flag10 = Pk9rPickMob.TypeItemPicks.Contains(itemFocus2.template.type);
							if (flag10)
							{
								Pk9rPickMob.TypeItemPicks.Remove(itemFocus2.template.type);
								GameScr.info1.addInfo("Đã xoá khỏi danh sách chỉ tự động nhặt loại item:" + itemFocus2.template.type.ToString(), 0);
							}
							else
							{
								Pk9rPickMob.TypeItemPicks.Add(itemFocus2.template.type);
								GameScr.info1.addInfo("Đã thêm vào danh sách chỉ tự động nhặt loại item:" + itemFocus2.template.type.ToString(), 0);
							}
						}
						else
						{
							GameScr.info1.addInfo("Cần trỏ vào quái hay vật phẩm cần thêm vào danh sách", 0);
						}
					}
				}
				else
				{
					bool flag11 = text == "anhat";
					if (flag11)
					{
						Pk9rPickMob.IsAutoPickItems = !Pk9rPickMob.IsAutoPickItems;
						GameScr.info1.addInfo("Auto nhặt của tàn sát: " + (Pk9rPickMob.IsAutoPickItems ? "Bật" : "Tắt"), 0);
					}
					else
					{
						bool flag12 = text == "itm";
						if (flag12)
						{
							Pk9rPickMob.IsItemMe = !Pk9rPickMob.IsItemMe;
							GameScr.info1.addInfo("Lọc không nhặt vật phẩm của người khác: " + (Pk9rPickMob.IsItemMe ? "Bật" : "Tắt"), 0);
						}
						else
						{
							bool flag13 = text == "sln";
							if (flag13)
							{
								Pk9rPickMob.IsLimitTimesPickItem = !Pk9rPickMob.IsLimitTimesPickItem;
								StringBuilder stringBuilder = new StringBuilder();
								stringBuilder.Append("Giới hạn số lần nhặt là ");
								stringBuilder.Append(Pk9rPickMob.TimesAutoPickItemMax);
								stringBuilder.Append(Pk9rPickMob.IsLimitTimesPickItem ? ": Bật" : ": Tắt");
								GameScr.info1.addInfo(stringBuilder.ToString(), 0);
							}
							else
							{
								bool flag14 = Pk9rPickMob.IsGetInfoChat<int>(text, "sln");
								if (flag14)
								{
									Pk9rPickMob.TimesAutoPickItemMax = Pk9rPickMob.GetInfoChat<int>(text, "sln");
									GameScr.info1.addInfo("Số lần nhặt giới hạn là: " + Pk9rPickMob.TimesAutoPickItemMax.ToString(), 0);
								}
								else
								{
									bool flag15 = Pk9rPickMob.IsGetInfoChat<short>(text, "addi");
									if (flag15)
									{
										short infoChat = Pk9rPickMob.GetInfoChat<short>(text, "addi");
										bool flag16 = Pk9rPickMob.IdItemPicks.Contains(infoChat);
										if (flag16)
										{
											Pk9rPickMob.IdItemPicks.Remove(infoChat);
											GameScr.info1.addInfo(string.Format("Đã xoá khỏi danh sách chỉ tự động nhặt item: {0}[{1}]", ItemTemplates.get(infoChat).name, infoChat), 0);
										}
										else
										{
											Pk9rPickMob.IdItemPicks.Add(infoChat);
											GameScr.info1.addInfo(string.Format("Đã thêm vào danh sách chỉ tự động nhặt item: {0}[{1}]", ItemTemplates.get(infoChat).name, infoChat), 0);
										}
									}
									else
									{
										bool flag17 = text == "blocki";
										if (flag17)
										{
											ItemMap itemFocus3 = global::Char.myCharz().itemFocus;
											bool flag18 = itemFocus3 != null;
											if (flag18)
											{
												bool flag19 = Pk9rPickMob.IdItemBlocks.Contains(itemFocus3.template.id);
												if (flag19)
												{
													Pk9rPickMob.IdItemBlocks.Remove(itemFocus3.template.id);
													GameScr.info1.addInfo(string.Format("Đã xoá khỏi danh sách không tự động nhặt item: {0}[{1}]", itemFocus3.template.name, itemFocus3.template.id), 0);
												}
												else
												{
													Pk9rPickMob.IdItemBlocks.Add(itemFocus3.template.id);
													GameScr.info1.addInfo(string.Format("Đã thêm vào danh sách không tự động nhặt item: {0}[{1}]", itemFocus3.template.name, itemFocus3.template.id), 0);
												}
											}
											else
											{
												GameScr.info1.addInfo("Cần trỏ vào vật phẩm cần chặn khi auto nhặt", 0);
											}
										}
										else
										{
											bool flag20 = Pk9rPickMob.IsGetInfoChat<short>(text, "blocki");
											if (flag20)
											{
												short infoChat2 = Pk9rPickMob.GetInfoChat<short>(text, "blocki");
												bool flag21 = Pk9rPickMob.IdItemBlocks.Contains(infoChat2);
												if (flag21)
												{
													Pk9rPickMob.IdItemBlocks.Remove(infoChat2);
													GameScr.info1.addInfo(string.Format("Đã thêm vào danh sách không tự động nhặt item: {0}[{1}]", ItemTemplates.get(infoChat2).name, infoChat2), 0);
												}
												else
												{
													Pk9rPickMob.IdItemBlocks.Add(infoChat2);
													GameScr.info1.addInfo(string.Format("Đã xoá khỏi danh sách không tự động nhặt item: {0}[{1}]", ItemTemplates.get(infoChat2).name, infoChat2), 0);
												}
											}
											else
											{
												bool flag22 = Pk9rPickMob.IsGetInfoChat<sbyte>(text, "addti");
												if (flag22)
												{
													sbyte infoChat3 = Pk9rPickMob.GetInfoChat<sbyte>(text, "addti");
													bool flag23 = Pk9rPickMob.TypeItemPicks.Contains(infoChat3);
													if (flag23)
													{
														Pk9rPickMob.TypeItemPicks.Remove(infoChat3);
														GameScr.info1.addInfo("Đã xoá khỏi danh sách chỉ tự động nhặt loại item: " + infoChat3.ToString(), 0);
													}
													else
													{
														Pk9rPickMob.TypeItemPicks.Add(infoChat3);
														GameScr.info1.addInfo("Đã thêm vào danh sách chỉ tự động nhặt loại item: " + infoChat3.ToString(), 0);
													}
												}
												else
												{
													bool flag24 = Pk9rPickMob.IsGetInfoChat<sbyte>(text, "blockti");
													if (flag24)
													{
														sbyte infoChat4 = Pk9rPickMob.GetInfoChat<sbyte>(text, "blockti");
														bool flag25 = Pk9rPickMob.TypeItemBlock.Contains(infoChat4);
														if (flag25)
														{
															Pk9rPickMob.TypeItemBlock.Remove(infoChat4);
															GameScr.info1.addInfo("Đã xoá khỏi danh sách không tự động nhặt loại item: " + infoChat4.ToString(), 0);
														}
														else
														{
															Pk9rPickMob.TypeItemBlock.Add(infoChat4);
															GameScr.info1.addInfo("Đã thêm vào danh sách không tự động nhặt loại item: " + infoChat4.ToString(), 0);
														}
													}
													else
													{
														bool flag26 = text == "clri";
														if (flag26)
														{
															Pk9rPickMob.IdItemPicks.Clear();
															Pk9rPickMob.TypeItemPicks.Clear();
															Pk9rPickMob.TypeItemBlock.Clear();
															Pk9rPickMob.IdItemBlocks.Clear();
															Pk9rPickMob.IdItemBlocks.AddRange(Pk9rPickMob.IdItemBlockBase);
															GameScr.info1.addInfo("Danh sách lọc item đã được đặt lại mặc định", 0);
														}
														else
														{
															bool flag27 = text == "cnn";
															if (flag27)
															{
																Pk9rPickMob.IdItemPicks.Clear();
																Pk9rPickMob.TypeItemPicks.Clear();
																Pk9rPickMob.TypeItemBlock.Clear();
																Pk9rPickMob.IdItemBlocks.Clear();
																Pk9rPickMob.IdItemBlocks.AddRange(Pk9rPickMob.IdItemBlockBase);
																Pk9rPickMob.IdItemPicks.Add(77);
																Pk9rPickMob.IdItemPicks.Add(861);
																GameScr.info1.addInfo("Đã cài đặt chỉ nhặt ngọc (của tàn sát)", 0);
															}
															else
															{
																bool flag28 = text == "ts";
																if (flag28)
																{
																	Pk9rPickMob.IsTanSat = !Pk9rPickMob.IsTanSat;
																	GameScr.info1.addInfo("Tự động đánh quái: " + (Pk9rPickMob.IsTanSat ? "Bật" : "Tắt"), 0);
																}
																else
																{
																	bool flag29 = text == "nsq";
																	if (flag29)
																	{
																		Pk9rPickMob.IsNeSieuQuai = !Pk9rPickMob.IsNeSieuQuai;
																		GameScr.info1.addInfo("Tàn sát né siêu quái: " + (Pk9rPickMob.IsNeSieuQuai ? "Bật" : "Tắt"), 0);
																	}
																	else
																	{
																		bool flag30 = Pk9rPickMob.IsGetInfoChat<int>(text, "addm");
																		if (flag30)
																		{
																			int infoChat5 = Pk9rPickMob.GetInfoChat<int>(text, "addm");
																			bool flag31 = Pk9rPickMob.IdMobsTanSat.Contains(infoChat5);
																			if (flag31)
																			{
																				Pk9rPickMob.IdMobsTanSat.Remove(infoChat5);
																				GameScr.info1.addInfo("Đã xoá mob: " + infoChat5.ToString(), 0);
																			}
																			else
																			{
																				Pk9rPickMob.IdMobsTanSat.Add(infoChat5);
																				GameScr.info1.addInfo("Đã thêm mob: " + infoChat5.ToString(), 0);
																			}
																		}
																		else
																		{
																			bool flag32 = Pk9rPickMob.IsGetInfoChat<int>(text, "addtm");
																			if (flag32)
																			{
																				int infoChat6 = Pk9rPickMob.GetInfoChat<int>(text, "addtm");
																				bool flag33 = Pk9rPickMob.TypeMobsTanSat.Contains(infoChat6);
																				if (flag33)
																				{
																					Pk9rPickMob.TypeMobsTanSat.Remove(infoChat6);
																					GameScr.info1.addInfo(string.Format("Đã xoá loại mob: {0}[{1}]", Mob.arrMobTemplate[infoChat6].name, infoChat6), 0);
																				}
																				else
																				{
																					Pk9rPickMob.TypeMobsTanSat.Add(infoChat6);
																					GameScr.info1.addInfo(string.Format("Đã thêm loại mob: {0}[{1}]", Mob.arrMobTemplate[infoChat6].name, infoChat6), 0);
																				}
																			}
																			else
																			{
																				bool flag34 = text == "clrm";
																				if (flag34)
																				{
																					Pk9rPickMob.IdMobsTanSat.Clear();
																					Pk9rPickMob.TypeMobsTanSat.Clear();
																					GameScr.info1.addInfo("Đã xoá danh sách đánh quái", 0);
																				}
																				else
																				{
																					bool flag35 = text == "skill";
																					if (flag35)
																					{
																						SkillTemplate template = global::Char.myCharz().myskill.template;
																						bool flag36 = Pk9rPickMob.IdSkillsTanSat.Contains(template.id);
																						if (flag36)
																						{
																							Pk9rPickMob.IdSkillsTanSat.Remove(template.id);
																							GameScr.info1.addInfo(string.Format("Đã xoá khỏi danh sách skill sử dụng tự động đánh quái skill: {0}[{1}]", template.name, template.id), 0);
																						}
																						else
																						{
																							Pk9rPickMob.IdSkillsTanSat.Add(template.id);
																							GameScr.info1.addInfo(string.Format("Đã thêm vào danh sách skill sử dụng tự động đánh quái skill: {0}[{1}]", template.name, template.id), 0);
																						}
																					}
																					else
																					{
																						bool flag37 = Pk9rPickMob.IsGetInfoChat<int>(text, "skill");
																						if (flag37)
																						{
																							int num = Pk9rPickMob.GetInfoChat<int>(text, "skill") - 1;
																							SkillTemplate skillTemplate = global::Char.myCharz().nClass.skillTemplates[num];
																							bool flag38 = Pk9rPickMob.IdSkillsTanSat.Contains(skillTemplate.id);
																							if (flag38)
																							{
																								Pk9rPickMob.IdSkillsTanSat.Remove(skillTemplate.id);
																								GameScr.info1.addInfo(string.Format("Đã xoá khỏi danh sách skill sử dụng tự động đánh quái skill: {0}[{1}]", skillTemplate.name, skillTemplate.id), 0);
																							}
																							else
																							{
																								Pk9rPickMob.IdSkillsTanSat.Add(skillTemplate.id);
																								GameScr.info1.addInfo(string.Format("Đã thêm vào danh sách skill sử dụng tự động đánh quái skill: {0}[{1}]", skillTemplate.name, skillTemplate.id), 0);
																							}
																						}
																						else
																						{
																							bool flag39 = Pk9rPickMob.IsGetInfoChat<sbyte>(text, "skillid");
																							if (flag39)
																							{
																								sbyte infoChat7 = Pk9rPickMob.GetInfoChat<sbyte>(text, "skillid");
																								bool flag40 = Pk9rPickMob.IdSkillsTanSat.Contains(infoChat7);
																								if (flag40)
																								{
																									Pk9rPickMob.IdSkillsTanSat.Remove(infoChat7);
																									GameScr.info1.addInfo("Đã xoá khỏi danh sách skill sử dụng tự động đánh quái skill: " + infoChat7.ToString(), 0);
																								}
																								else
																								{
																									Pk9rPickMob.IdSkillsTanSat.Add(infoChat7);
																									GameScr.info1.addInfo("Đã thêm vào danh sách skill sử dụng tự động đánh quái skill: " + infoChat7.ToString(), 0);
																								}
																							}
																							else
																							{
																								bool flag41 = text == "clrs";
																								if (flag41)
																								{
																									Pk9rPickMob.IdSkillsTanSat.Clear();
																									Pk9rPickMob.IdSkillsTanSat.AddRange(Pk9rPickMob.IdSkillsBase);
																									GameScr.info1.addInfo("Đã đặt danh sách skill sử dụng tự động đánh quái về mặc định", 0);
																								}
																								else
																								{
																									bool flag42 = text == "abf";
																									if (flag42)
																									{
																										bool flag43 = Pk9rPickMob.HpBuff == 0 && Pk9rPickMob.MpBuff == 0;
																										if (flag43)
																										{
																											GameScr.info1.addInfo("Tự động sử dụng đậu thần: Tắt", 0);
																										}
																										else
																										{
																											Pk9rPickMob.HpBuff = 20;
																											Pk9rPickMob.MpBuff = 20;
																											GameScr.info1.addInfo(string.Format("Tự động sử dụng đậu thần khi HP dưới {0}%, MP dưới {1}%", Pk9rPickMob.HpBuff, Pk9rPickMob.MpBuff), 0);
																										}
																									}
																									else
																									{
																										bool flag44 = Pk9rPickMob.IsGetInfoChat<int>(text, "abf");
																										if (flag44)
																										{
																											Pk9rPickMob.HpBuff = Pk9rPickMob.GetInfoChat<int>(text, "abf");
																											Pk9rPickMob.MpBuff = 0;
																											GameScr.info1.addInfo(string.Format("Tự động sử dụng đậu thần khi HP dưới {0}%", Pk9rPickMob.HpBuff), 0);
																										}
																										else
																										{
																											bool flag45 = Pk9rPickMob.IsGetInfoChat<int>(text, "abf", 2);
																											if (flag45)
																											{
																												int[] infoChat8 = Pk9rPickMob.GetInfoChat<int>(text, "abf", 2);
																												Pk9rPickMob.HpBuff = infoChat8[0];
																												Pk9rPickMob.MpBuff = infoChat8[1];
																												GameScr.info1.addInfo(string.Format("Tự động sử dụng đậu thần khi HP dưới {0}%, MP dưới {1}%", Pk9rPickMob.HpBuff, Pk9rPickMob.MpBuff), 0);
																											}
																											else
																											{
																												bool flag46 = !(text == "vdh");
																												if (flag46)
																												{
																													return false;
																												}
																												Pk9rPickMob.IsVuotDiaHinh = !Pk9rPickMob.IsVuotDiaHinh;
																												GameScr.info1.addInfo("Tự động đánh quái vượt địa hình: " + (Pk9rPickMob.IsVuotDiaHinh ? "Bật" : "Tắt"), 0);
																											}
																										}
																									}
																								}
																							}
																						}
																					}
																				}
																			}
																		}
																	}
																}
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
			return true;
		}

		// Token: 0x06000A37 RID: 2615 RVA: 0x000A41F4 File Offset: 0x000A23F4
		public static bool HotKeys()
		{
			int keyAsciiPress = GameCanvas.keyAsciiPress;
			bool flag = keyAsciiPress <= 98;
			if (flag)
			{
				bool flag2 = keyAsciiPress == 97;
				if (flag2)
				{
					Pk9rPickMob.Chat("add");
					return true;
				}
				bool flag3 = keyAsciiPress == 98;
				if (flag3)
				{
					Pk9rPickMob.Chat("abf");
					return true;
				}
			}
			else
			{
				bool flag4 = keyAsciiPress == 110;
				if (flag4)
				{
					Pk9rPickMob.Chat("anhat");
					return true;
				}
				bool flag5 = keyAsciiPress == 116;
				if (flag5)
				{
					Pk9rPickMob.Chat("ts");
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000A38 RID: 2616 RVA: 0x000065F0 File Offset: 0x000047F0
		public static void Update()
		{
			PickMobController.Update();
		}

		// Token: 0x06000A39 RID: 2617 RVA: 0x000A428C File Offset: 0x000A248C
		public static void MobStartDie(object obj)
		{
			Mob mob = (Mob)obj;
			bool flag = mob.status != 1 && mob.status != 0;
			if (flag)
			{
				mob.timeLastDie = mSystem.currentTimeMillis();
				mob.countDie++;
				bool flag2 = mob.countDie > 10;
				if (flag2)
				{
					mob.countDie = 0;
				}
			}
		}

		// Token: 0x06000A3A RID: 2618 RVA: 0x000A42EC File Offset: 0x000A24EC
		public static void UpdateCountDieMob(Mob mob)
		{
			bool flag = mob.levelBoss != 0;
			if (flag)
			{
				mob.countDie = 0;
			}
		}

		// Token: 0x06000A3B RID: 2619 RVA: 0x000A4310 File Offset: 0x000A2510
		private static bool IsGetInfoChat<T>(string text, string s)
		{
			bool flag = !text.StartsWith(s);
			bool result;
			if (flag)
			{
				result = false;
			}
			else
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
			return result;
		}

		// Token: 0x06000A3C RID: 2620 RVA: 0x000A4370 File Offset: 0x000A2570
		private static T GetInfoChat<T>(string text, string s)
		{
			return (T)((object)Convert.ChangeType(text.Substring(s.Length), typeof(T)));
		}

		// Token: 0x06000A3D RID: 2621 RVA: 0x000A43A4 File Offset: 0x000A25A4
		private static bool IsGetInfoChat<T>(string text, string s, int n)
		{
			bool flag = !text.StartsWith(s);
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				try
				{
					string[] array = text.Substring(s.Length).Split(new char[]
					{
						' '
					});
					for (int i = 0; i < n; i++)
					{
						Convert.ChangeType(array[i], typeof(T));
					}
				}
				catch
				{
					return false;
				}
				result = true;
			}
			return result;
		}

		// Token: 0x06000A3E RID: 2622 RVA: 0x000A442C File Offset: 0x000A262C
		private static T[] GetInfoChat<T>(string text, string s, int n)
		{
			T[] array = new T[n];
			string[] array2 = text.Substring(s.Length).Split(new char[]
			{
				' '
			});
			for (int i = 0; i < n; i++)
			{
				array[i] = (T)((object)Convert.ChangeType(array2[i], typeof(T)));
			}
			return array;
		}

		// Token: 0x04001325 RID: 4901
		private const int ID_ITEM_GEM = 77;

		// Token: 0x04001326 RID: 4902
		private const int ID_ITEM_GEM_LOCK = 861;

		// Token: 0x04001327 RID: 4903
		private const int DEFAULT_HP_BUFF = 20;

		// Token: 0x04001328 RID: 4904
		private const int DEFAULT_MP_BUFF = 20;

		// Token: 0x04001329 RID: 4905
		private static readonly sbyte[] IdSkillsBase = new sbyte[]
		{
			0,
			2,
			17,
			4,
			13
		};

		// Token: 0x0400132A RID: 4906
		public static readonly short[] IdItemBlockBase = new short[]
		{
			225,
			353,
			354,
			355,
			356,
			357,
			358,
			359,
			360,
			362
		};

		// Token: 0x0400132B RID: 4907
		public static bool IsTanSat = false;

		// Token: 0x0400132C RID: 4908
		public static bool IsNeSieuQuai = false;

		// Token: 0x0400132D RID: 4909
		public static bool IsVuotDiaHinh = true;

		// Token: 0x0400132E RID: 4910
		public static List<int> IdMobsTanSat = new List<int>();

		// Token: 0x0400132F RID: 4911
		public static List<int> TypeMobsTanSat = new List<int>();

		// Token: 0x04001330 RID: 4912
		public static List<sbyte> IdSkillsTanSat = new List<sbyte>(Pk9rPickMob.IdSkillsBase);

		// Token: 0x04001331 RID: 4913
		public static bool IsAutoPickItems = false;

		// Token: 0x04001332 RID: 4914
		public static bool IsItemMe = true;

		// Token: 0x04001333 RID: 4915
		public static bool IsLimitTimesPickItem = true;

		// Token: 0x04001334 RID: 4916
		public static int TimesAutoPickItemMax = 10;

		// Token: 0x04001335 RID: 4917
		public static List<short> IdItemPicks = new List<short>();

		// Token: 0x04001336 RID: 4918
		public static List<short> IdItemBlocks = new List<short>(Pk9rPickMob.IdItemBlockBase);

		// Token: 0x04001337 RID: 4919
		public static List<sbyte> TypeItemPicks = new List<sbyte>();

		// Token: 0x04001338 RID: 4920
		public static List<sbyte> TypeItemBlock = new List<sbyte>();

		// Token: 0x04001339 RID: 4921
		public static int HpBuff = 0;

		// Token: 0x0400133A RID: 4922
		public static int MpBuff = 0;
	}
}
