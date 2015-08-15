using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class RX_RoomScript : MonoBehaviour {

	/// <summary>
	/// 房间列表标题,用来显示用户昵称
	/// </summary>
	public UILabel sceneTitle;
	
	/// <summary>
	/// 通过用户昵称来创建斗地主房间
	/// </summary>
	public void CreateRoom()
	{
		//创建游戏房间
		LO_GameServer.DefaultServer.CreateRoom(RX_UserManager.DefaultUser.user_name + "'s DZZ");
	}

	/// <summary>
	/// 房间列表的容器
	/// </summary>
	public UIGrid		room_grid;

	/// <summary>
	/// 房间列表中的每个小房间的prefab
	/// </summary>
	public GameObject	room_item;


	/// <summary>
	/// 用户点击刷新按钮时触发的获取房间消息
	/// </summary>
	public void GetRoomList()
	{
		//通过LO_GameServer单例类开始请求房间列表
		LO_GameServer.DefaultServer.StartRequestRoom((HostData[] data)=>{

			//获取到新的房间列表后,将视图上旧的房间空间都删除掉
			NGUITools.DestroyChildren(room_grid.transform);

			//遍历循环添加新的房间控件
			foreach (HostData item in data) 
			{

				//通过NGUITools工具类,在Grid节点上添加对应的房间控件
				GameObject room = NGUITools.AddChild(room_grid.gameObject,room_item);	

				//获取每个房间上的需要修改的label和button控件
				UILabel name_label 	= room.transform.GetChild(0).GetComponent<UILabel>();
				UILabel count_label = room.transform.GetChild(1).GetComponent<UILabel>();
				UIButton join_btn 	= room.transform.GetChild(2).GetComponent<UIButton>();

				//游戏名称
				name_label.text = item.gameName;

				//人数
				count_label.text = item.connectedPlayers.ToString();

				//加入房间按钮
				if (item.connectedPlayers >= 3) {
					join_btn.enabled = false;
				}
				else
				{
					//点击加入房间按钮触发时执行的过程
					join_btn.onClick.Add(new EventDelegate(() =>{
						
						LO_GameServer.DefaultServer.JoinHostRoom(item,(int state)=>{
							if (state == 0) {
								Debug.Log("加入房间" + item.gameName + "成功");
							}
						});
						
					}));
				}

				//重新刷新一下Grid控件的布局
				room_grid.enabled = true;
			}
		});
	}

	// Use this for initialization
	void Start () {

		//初始化游戏的MasterServer服务器
		LO_GameServer.DefaultServer.InitServer(RX_Define.RX_ServerIP,RX_Define.RX_ServerPort);

		//设定登陆游戏服务器的游戏角色
		sceneTitle.text = RX_UserManager.DefaultUser.user_name;
	}
}
