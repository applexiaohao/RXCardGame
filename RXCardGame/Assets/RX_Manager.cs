using UnityEngine;
using System.Collections;
using AssemblyCSharp;
using System.Collections.Generic;

public class RX_Manager : MonoBehaviour {


	public Camera world_camera;
	public Camera gui_camera;

	//public prefab link
	public Object prefab;

	//public bottom link
	public UISprite bottom_pool;
	//public left	link
	public UISprite left_pool;
	//public right	link
	public UISprite right_pool;

	// Use this for initialization

	private RX_SeatInfo bottom_seat;


	public UILabel label;
	void Start () 
	{
		this.Reshuffle ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void PopSet()
	{
		RX_CardSet cardset = bottom_seat.PopCardSet ();

		if (cardset == null) {
			label.text = "出牌失败";
			Debug.Log ("出牌失败");
		} else {
			label.text = "成功出牌" + cardset.ToString ();
			Debug.Log ("成功出牌" + cardset.ToString ());
		}
	}

	public void Reshuffle()
	{
		//shuffle the card
		List<RX_Card> list = RX_CardManager.DefaultManager().Reshuffle ();


		bottom_seat = new RX_SeatInfo(RX_SEAT_POSITION.RX_SEAT_BOTTOM,this.bottom_pool);
		bottom_seat.CardList = list.GetRange(0,17);
	}
}
