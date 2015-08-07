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
using UnityEngine;
using System.Collections.Generic;

namespace AssemblyCSharp
{
	public enum RX_ROLE_TYPE
	{
		RX_ROLE_DIZHU 	= 0,
		RX_ROLE_NONGMIN = 1,
		RX_ROLE_NORMAL 	= 2,
	}
	public enum RX_SEAT_POSITION
	{
		RX_SEAT_BOTTOM 	= 0,
		RX_SEAT_LEFT 	= 1,
		RX_SEAT_RIGHT 	= 2,
	}

	public class RX_SeatInfo
	{
		#region Game UI Property

		private RX_ROLE_TYPE seat_type;
		public RX_ROLE_TYPE Type{
			set{
				this.seat_type = value;
			}
			get{
				return seat_type;
			}
		}

		private RX_SEAT_POSITION seat_pos;
		public RX_SEAT_POSITION Position{
			set{
				this.seat_pos = value;

				switch (this.seat_pos) 
				{
					case RX_SEAT_POSITION.RX_SEAT_BOTTOM:
					{
						break;
					}
					case RX_SEAT_POSITION.RX_SEAT_LEFT:
					{
						break;
					}
					case RX_SEAT_POSITION.RX_SEAT_RIGHT:
					{
						break;
					}
				}
			}
			get{
				return this.seat_pos;
			}
		}

		private Vector3 ui_pos;
		public Vector3 UIPosition{
			get{
				return ui_pos;
			}
		}

		#endregion


		public RX_SeatInfo (RX_SEAT_POSITION pos,UISprite pool)
		{
			this.Type 	= RX_ROLE_TYPE.RX_ROLE_NORMAL;
			this.Position	= pos;
			this.seat_container = pool;
		}

		private UISprite	seat_container;

		private List<RX_Card> card_list;
		public List<RX_Card> CardList{
			set
			{
				this.card_list = value;

				//设置布局..
				this.LayoutCardList();
			}
			get{
				return this.card_list;
			}
		}


		/// <summary>
		/// Layouts the card list.
		/// </summary>
		private void LayoutCardList()
		{
			this.CardList.Sort((RX_Card x, RX_Card y) => {
				return (int)y.Level - (int)x.Level;
			});

			RX_CardManager.RefreshPool ();
				
			int width 	= this.seat_container.width;
			int height	= this.seat_container.height;

			//每个人最多20张牌,计算出最小的间距
			int margin = width / 20;
			//求出现在剩余多少张牌
			int count = this.card_list.Count;

			int minx = 0 - count / 2 * margin;

			int temp = minx;
			for (int i = 0; i < count; i++) 
			{
				RX_CardManager.CreateSpriteBy (this.card_list [i], this.seat_container, temp);
				temp += margin;
			}
		}

		public RX_CardSet PopCardSet()
		{
			List<RX_Card> list = this.CardList.FindAll ((RX_Card obj) => {
				return obj.IsPop;
			});

			RX_CardSet card_set = new RX_CardSet ();
			card_set.Lister = list;



			if (RX_CardType.IsDan (card_set) ||
			    RX_CardType.IsDui (card_set) ||
			    RX_CardType.IsShunzi (card_set) ||
			    RX_CardType.IsLianDui (card_set) ||
			    RX_CardType.IsBoom (card_set) ||
			    RX_CardType.IsBigBoom (card_set) ||
			    RX_CardType.IsFeijibudai (card_set) ||
			    RX_CardType.IsFeijidai (card_set) ||
			    RX_CardType.IsSanBuDai (card_set) ||
			    RX_CardType.IsSandaiyi (card_set) ||
			    RX_CardType.IsSidaier (card_set)) 
			{
				this.CardList.RemoveAll ((RX_Card obj) => {
					return obj.IsPop;
				});

				this.LayoutCardList ();

				return card_set;
			} else {
				return null;
			}
		}
	}
}

