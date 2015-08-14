using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class RX_RoomScript : MonoBehaviour {

	/// <summary>
	/// The scene title.
	/// </summary>
	public UILabel sceneTitle;
	
	/// <summary>
	/// Creates the room.
	/// </summary>
	public void CreateRoom()
	{
		LO_GameServer.DefaultServer.StartServer("Lewis's");
	}

	public UIGrid		room_grid;
	public GameObject	room_item;
	/// <summary>
	/// Gets the room list.
	/// </summary>
	public void GetRoomList()
	{
		LO_GameServer.DefaultServer.StartRequestRoom((HostData[] data)=>{

			NGUITools.DestroyChildren(room_grid.transform);

			foreach (HostData item in data) 
			{
				GameObject room = NGUITools.AddChild(room_grid.gameObject,room_item);	

				UILabel name_label 	= room.transform.GetChild(0).GetComponent<UILabel>();
				UILabel count_label = room.transform.GetChild(1).GetComponent<UILabel>();
				UIButton join_btn 	= room.transform.GetChild(2).GetComponent<UIButton>();

				name_label.text = item.gameName;
				count_label.text = item.connectedPlayers.ToString();

				join_btn.onClick.Add(new EventDelegate(() =>{

					LO_GameServer.DefaultServer.JoinHostRoom(item,(int state)=>{
						Debug.Log(state.ToString());
					});

				}));

				room_grid.enabled = true;
			}
		});
	}

	/// <summary>
	/// Joins the room.
	/// </summary>
	/// <param name="data">Data.</param>
	private void JoinRoom(HostData data)
	{

	}

	// Use this for initialization
	void Start () {
		LO_GameServer.DefaultServer.InitServer("115.28.227.1",23466);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
