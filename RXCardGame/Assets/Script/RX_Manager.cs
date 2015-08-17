using UnityEngine;
using System.Collections;
using AssemblyCSharp;
using System.Collections.Generic;

public class RX_Manager : MonoBehaviour {

	//public bottom link
	public UISprite bottom_pool;
	//public left	link
	public UISprite left_pool;
	//public right	link
	public UISprite right_pool;
	//
	public UISprite top_pool;


	public UISprite bottom_pop_pool;
	public UISprite left_pop_pool;
	public UISprite right_pop_pool;
	public UILabel	bottom_pop_label;
	public UILabel	left_pop_label;
	public UILabel	right_pop_label;


	// Use this for initialization
	private RX_SeatInfo bottom_seat;
	private RX_SeatInfo left_seat;
	private RX_SeatInfo right_seat;
	private RX_SeatInfo top_seat;


	void Start () 
	{
		this.Reshuffle ();
	}

	/// <summary>
	/// 弹出选中的牌型
	/// </summary>
	/// 
	/// 
	private RX_SeatInfo my_seat = null;
	public void PopSet()
	{

		RX_CardSet cardset = my_seat.PopCardSet ();

		if (cardset == null) {
			return;
		}
			
		//当前牌型是否可以出牌
		bool is_successed = true;
		//当牌型不属于斗地主牌型时,cardset则为null
		//例如,选中的牌是3,4
		//例如,选中的牌是qq,kk,aa,22s
		RX_PopCardSetManager.AddCardSet(cardset,out is_successed);

		//当前牌型可以出牌时,判断消息
		if (is_successed) 
		{
			NGUITools.DestroyChildren(bottom_pop_pool.transform);
			my_seat.RemoveCardSet(cardset,bottom_pop_pool);
			bottom_pop_label.text = "";

			LO_GameServer.DefaultServer.PopCardSet (cardset, ((RX_UserInfo user) => {

				//如果下一个可以出牌的玩家是自己的话,将出牌按钮显示出来
				if (RX_UserManager.DefaultUser.Equals(user)) 
				{
					
				}

			}));
		}

	
	}

	public void DontPop()
	{
		RX_CardSet cardset = new RX_CardSet();
		cardset.Typer = RX_CARD_SET.RX_TYPE_BUCHU;

		bool isSuccessed = true;
		RX_PopCardSetManager.AddCardSet(cardset,out isSuccessed);

//		if (seat == bottom_seat) 
//		{
//			bottom_pop_label.text = "Pass";
//
//			NGUITools.DestroyChildren(bottom_pop_pool.transform);
//
//			seat = this.right_seat;
//
//			return;
//		}
//		if (seat == this.right_seat) {
//
//			right_pop_label.text = "Pass";
//		
//			NGUITools.DestroyChildren(right_pop_pool.transform);
//
//			seat = this.left_seat;
//
//			return;
//		}
//
//		if (seat == this.left_seat) {
//
//			left_pop_label.text = "Pass";
//
//			NGUITools.DestroyChildren(left_pop_pool.transform);
//
//			seat = this.bottom_seat;
//		}
	}

	/// <summary>
	/// 洗牌函数
	/// </summary>
	public void Reshuffle()
	{
		//shuffle the card
		List<RX_Card> list = RX_CardManager.DefaultManager().Reshuffle ();

		//创建底下的座位对象
		bottom_seat = new RX_SeatInfo(RX_SEAT_POSITION.RX_SEAT_BOTTOM,this.bottom_pool);
		bottom_seat.CardList = list.GetRange(0,17);
		bottom_seat.User = RX_UserManager.DefaultUser;

		//创建左边的座位对象
		left_seat = new RX_SeatInfo(RX_SEAT_POSITION.RX_SEAT_LEFT,this.left_pool);
		left_seat.CardList = list.GetRange (17, 17);
		left_seat.User = LO_GameServer.DefaultServer.LeftUser;

		//创建右边的座位对象
		right_seat = new RX_SeatInfo(RX_SEAT_POSITION.RX_SEAT_RIGHT,this.right_pool);
		right_seat.CardList = list.GetRange (34, 17);
		right_seat.User = LO_GameServer.DefaultServer.RightUser;

		//创建上边的座位对象
		top_seat = new RX_SeatInfo(RX_SEAT_POSITION.RX_SEAT_TOP,this.top_pool);
		top_seat.CardList = list.GetRange (51, 3);

		my_seat = bottom_seat;
	}
}
