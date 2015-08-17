using UnityEngine;
using System.Collections;
using AssemblyCSharp;
using System.Collections.Generic;

public class RX_RootScript : MonoBehaviour {


	/// <summary>
	/// 登陆、注册场景的账号输入框
	/// </summary>
	public UIInput	nameField;

	/// <summary>
	/// 登陆、注册常见的密码输入框
	/// </summary>
	public UIInput	pwdField;

	//属性
	private string UserName{
		get{
			return nameField.value.Trim();
		}
	}
	private string UserPwd{
		get{
			return pwdField.value.Trim();
		}
	}


	/// <summary>
	/// 用来检测输入框的信息是否正确,位数大于6位
	/// </summary>
	/// <returns><c>true</c>, if user info was checked, <c>false</c> otherwise.</returns>
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

	public UILabel testLabel;
	/// <summary>
	/// 登陆、注册场景中点击登陆按钮触发的消息方法
	/// </summary>
	public void OnClickLogin()
	{
		this.nameField.value = Random.Range (0, 1000).ToString ();
//		testLabel.text = 
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
}
