// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 4.0.30319.1
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------
using System;
using System.Xml.Serialization;

namespace AssemblyCSharp
{
	[XmlRoot("card")]
	public class RX_Card
	{

		//等级
		private RX_CARD_LEVEL 	level;
		//姓名
		private RX_CARD_NAME 	name;

		[XmlElement("level")]
		public RX_CARD_LEVEL Level{
			set{
				level = value;
			}
			get{ 
				return level;
			}
		}

		[XmlElement("name")]
		public RX_CARD_NAME Name{
			set{ 
				name = value;
			}
			get{ 
				return name;
			}
		}

		private int index_value;

		[XmlElement("index")]
		private int Index_value{
			set{
				index_value = value;

				if(index_value >= 0 && index_value <= 12)
				{
					name = RX_CARD_NAME.RX_NAME_HOT;
				}
				if(index_value >= 13 && index_value <= 25)
				{
					name = RX_CARD_NAME.RX_NAME_FAP;
				}
				if(index_value >= 26 && index_value <= 38)
				{
					name = RX_CARD_NAME.RX_NAME_HET;
				}
				if(index_value >= 39 && index_value <= 51)
				{
					name = RX_CARD_NAME.RX_NAME_MEH;
				}
					
				switch (index_value % 13) {
					case 0:// 0 13 26 39
					{
						this.Level = RX_CARD_LEVEL.RX_LEVEL_A;
						break;
					}
					case 1:// 1 14 27 40
					{
						this.Level = RX_CARD_LEVEL.RX_LEVEL_2;
						break;
					}
					case 2:// 2 15 28 41
					{
						this.Level = RX_CARD_LEVEL.RX_LEVEL_3;
						break;
					}
					case 3:// 3 16 29 42
					{
						this.Level = RX_CARD_LEVEL.RX_LEVEL_4;
						break;
					}
					case 4:// 4 17 30 43
					{
						this.Level = RX_CARD_LEVEL.RX_LEVEL_5;
						break;
					}
					case 5:// 5 18 31 44
					{
						this.Level = RX_CARD_LEVEL.RX_LEVEL_6;
						break;
					}
					case 6:// 6 19 32 45
					{
						this.Level = RX_CARD_LEVEL.RX_LEVEL_7;
						break;
					}
					case 7:// 7 20 33 46
					{
						this.Level = RX_CARD_LEVEL.RX_LEVEL_8;
						break;
					}
					case 8:// 8 21 34 47
					{
						this.Level = RX_CARD_LEVEL.RX_LEVEL_9;
						break;
					}
					case 9:// 9 22 35 48
					{
						this.Level = RX_CARD_LEVEL.RX_LEVEL_0;
						break;
					}
					case 10:// 10 23 36 49
					{
						this.Level = RX_CARD_LEVEL.RX_LEVEL_J;
						break;
					}
					case 11:// 11 24 37 50
					{
						this.Level = RX_CARD_LEVEL.RX_LEVEL_Q;
						break;
					}
					case 12:// 12 25 38 51
					{
						this.Level = RX_CARD_LEVEL.RX_LEVEL_K;
						break;
					}
				}

				//小王的判断
				if(index_value == 52)
				{
					name = RX_CARD_NAME.RX_NAME_KIN;
					level = RX_CARD_LEVEL.RX_LEVEL_S;
				}
				//大王的判断
				if(index_value == 53)
				{
					name = RX_CARD_NAME.RX_NAME_KIN;
					level = RX_CARD_LEVEL.RX_LEVEL_B;
				}
			}
			get{
				return index_value;
			}
		}


		public RX_Card (int index)
		{
			this.Index_value = index;
			this.IsPop = false;
		}
		public RX_Card(){}

		public void SetIndex(int index)
		{
			this.Index_value = index;
		}

		public override string ToString ()
		{
			return this.Index_value.ToString();
		}

		#region 是否弹出
		//是否弹出
		private		bool 	isPop;

		[XmlElement("ispop")]
		public		bool	IsPop{
			set{ 
				this.isPop = value;
			}
			get{
				return this.isPop;
			}
		}	


		/// <summary>
		/// 只有Get函数的PositionY属性
		/// </summary>
		/// <value>The position y.</value>
		public float PositionY
		{
			get{ 
				if (this.IsPop) {
					return 10f;
				} else {
					return 0f;
				}
			}
		}
		#endregion


		#region 判断是否是相同的牌

		public bool Equals(RX_Card sender)
		{
			return ((int)this.Name == (int)sender.Name) && ((int)this.Level == (int)sender.Level);
		}

		#endregion
	}
}

