# 热血斗地主

@[测试视频](http://v.youku.com/v_show/id_XMTMxODc1NTU1Ng==.html#paction)


## 模块化思想设计

### 游戏纸牌逻辑模块 类列表
- 	RX_Card.cs
- 	RX_CardManager.cs
- 	RX_CardSet.cs
- 	RX_CardType.cs
- 	RX_PopCardSet.cs
- 	RX_PopCardSetManager.cs
- 	RX_SeatInfo.cs

**洗牌、发牌功能(RX_Manager.cs)**

```
	/// <summary>
	/// 洗牌函数
	/// </summary>
	public void Reshuffle()
	{
		//洗牌
		List<RX_Card> list = RX_CardManager.DefaultManager().Reshuffle ();

		//创建底下的座位对象
		bottom_seat = new RX_SeatInfo(RX_SEAT_POSITION.RX_SEAT_BOTTOM,this.bottom_pool);
		bottom_seat.CardList = list.GetRange(0,17);

		//创建左边的座位对象
		left_seat = new RX_SeatInfo(RX_SEAT_POSITION.RX_SEAT_LEFT,this.left_pool);
		left_seat.CardList = list.GetRange (17, 17);

		//创建右边的座位对象
		right_seat = new RX_SeatInfo(RX_SEAT_POSITION.RX_SEAT_RIGHT,this.right_pool);
		right_seat.CardList = list.GetRange (34, 17);

		//创建上边的座位对象
		top_seat = new RX_SeatInfo(RX_SEAT_POSITION.RX_SEAT_TOP,this.top_pool);
		top_seat.CardList = list.GetRange (51, 3);

		seat = bottom_seat;
	}
```
**打牌功能(RX_Manager.cs)**
```
	/// <summary>
	/// 弹出选中的牌型
	/// </summary>
	private RX_SeatInfo seat = null;
	public void PopSet()
	{

		RX_CardSet cardset = seat.PopCardSet ();

		if (cardset == null) {
			return;
		}

		bool is_successed = true;

		RX_PopCardSetManager.AddCardSet(cardset,out is_successed);


		if (is_successed) 
		{
			if (seat == bottom_seat) 
			{
				NGUITools.DestroyChildren(bottom_pop_pool.transform);
				seat.RemoveCardSet(cardset,bottom_pop_pool);
				bottom_pop_label.text = "";

				seat = this.right_seat;
				return;
			}
			if (seat == this.right_seat) 
			{
				NGUITools.DestroyChildren(right_pop_pool.transform);
				seat.RemoveCardSet(cardset,right_pop_pool);
				right_pop_label.text = "";

				seat = this.left_seat;

				return;
			}
			
			if (seat == this.left_seat) 
			{

				NGUITools.DestroyChildren(left_pop_pool.transform);
				seat.RemoveCardSet(cardset,left_pop_pool);
				left_pop_label.text = "";

				seat = this.bottom_seat;

			}

		}

		//当牌型不属于斗地主牌型时,cardset则为null
		//例如,选中的牌是3,4
		//例如,选中的牌是qq,kk,aa,22s
	}

	public void DontPop()
	{
		RX_CardSet cardset = new RX_CardSet();
		cardset.Typer = RX_CARD_SET.RX_TYPE_BUCHU;

		bool isSuccessed = true;
		RX_PopCardSetManager.AddCardSet(cardset,out isSuccessed);

		if (seat == bottom_seat) 
		{
			bottom_pop_label.text = "Pass";

			NGUITools.DestroyChildren(bottom_pop_pool.transform);

			seat = this.right_seat;

			return;
		}
		if (seat == this.right_seat) {

			right_pop_label.text = "Pass";
		
			NGUITools.DestroyChildren(right_pop_pool.transform);

			seat = this.left_seat;

			return;
		}

		if (seat == this.left_seat) {

			left_pop_label.text = "Pass";

			NGUITools.DestroyChildren(left_pop_pool.transform);

			seat = this.bottom_seat;
		}
	}
```
### 游戏网络逻辑模块 类列表
- 	LO_GameServer.cs
- 	RX_DataServer.cs
- 	RX_UserInfo.cs
- 	LO_XMLTool.cs
- 	RX_RootScript.cs
- 	RX_RoomScript.cs

**登陆、注册功能(RX_RootScript.cs)**
```
	/// <summary>
	/// 用来检测输入框的信息是否正确,位数大于6位
	/// </summary>
	private bool CheckUserInfo()
	{
		bool result = true;

		if (this.UserName.Length < 6 || this.UserPwd.Length < 6) 
		{
			Debug.Log("invalid user's infomation");
			result = false;
		}

		return result;
	}

	/// <summary>
	/// 注册按钮触发的消息
	/// </summary>
	public void OnClickRegister()
	{
		if (CheckUserInfo()) 
		{
			//通过RX_DataServer单例类进行用户信息注册
			RX_DataServer.DefaultServer.Register(this.UserName,this.UserPwd,(string xml)=>{

				//返回的注册信息
				error data = (error)LO_XMLTool.Deserialize(typeof(error),xml);

				Debug.Log(data);

			});
		}
	}

	/// <summary>
	/// 登陆、注册场景中点击登陆按钮触发的消息方法
	/// </summary>
	public void OnClickLogin()
	{
		if (CheckUserInfo()) 
		{
			//通过RX_DataServer单例类的登陆方法，对用户进行登陆
			RX_DataServer.DefaultServer.Login(this.UserName,this.UserPwd,(string xml)=>{

				//解析用户信息
				RX_UserInfo user = (RX_UserInfo)LO_XMLTool.Deserialize(typeof(RX_UserInfo),xml);

				//当解析用户失败时，即登陆失败
				if (user == null) {
					Debug.Log("failed");
				}
				else
				{
					Debug.Log("successed");

					//存储登陆成功后的用户信息
					RX_UserManager.DefaultUser = user;

					//加载房间列表场景
					Application.LoadLevel("RoomScene");
				}
			});		
		}
	}
```
**初始化游戏服务器功能(RX_RoomScript.cs)**
```
	// Use this for initialization
	void Start () {

		//初始化游戏的MasterServer服务器
		LO_GameServer.DefaultServer.InitServer(RX_Define.RX_ServerIP,RX_Define.RX_ServerPort);

		//设定登陆游戏服务器的游戏角色
		sceneTitle.text = RX_UserManager.DefaultUser.user_name;
	}
```

**创建房间功能(RX_RoomScript.cs)**
```
	/// <summary>
	/// 通过用户昵称来创建斗地主房间
	/// </summary>
	public void CreateRoom()
	{
		//创建游戏房间
		LO_GameServer.DefaultServer.CreateRoom(RX_UserManager.DefaultUser.user_name + "'s DZZ");
	}
```
**获取房间列表功能(RX_RoomScript.cs)**
```
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

```