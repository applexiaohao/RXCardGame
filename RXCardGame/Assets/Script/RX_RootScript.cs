using UnityEngine;
using System.Collections;
using AssemblyCSharp;
using System.Collections.Generic;

public class RX_RootScript : MonoBehaviour {


	/// <summary>
	/// The name field.
	/// </summary>
	public UIInput	nameField;
	/// <summary>
	/// The pwd field.
	/// </summary>
	public UIInput	pwdField;
	
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

	public void OnClickRegister()
	{
		if (CheckUserInfo()) 
		{
			RX_DataServer.DefaultServer.Register(this.UserName,this.UserPwd,(string xml)=>{

				error data = (error)LO_XMLTool.Deserialize(typeof(error),xml);

				Debug.Log(data);

			});
		}
	}
	public void OnClickLogin()
	{
		if (CheckUserInfo()) 
		{
			RX_DataServer.DefaultServer.Login(this.UserName,this.UserPwd,(string xml)=>{
				RX_UserInfo user = (RX_UserInfo)LO_XMLTool.Deserialize(typeof(RX_UserInfo),xml);

				if (user == null) {
					Debug.Log("failed");
				}
				else
				{
					Debug.Log("successed");

					RX_UserManager.DefaultUser = user;

					Application.LoadLevel("RoomScene");
				}
			});		
		}
	}
}
