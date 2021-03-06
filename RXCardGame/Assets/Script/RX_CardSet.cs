﻿using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace AssemblyCSharp
{
	/// <summary>
	/// 11种牌型
	/// </summary>
	public enum RX_CARD_SET
	{
		RX_TYPE_DAN 		= 0,
		RX_TYPE_DUI			= 1,
		RX_TYPE_FEI_BUDAI	= 2,
		RX_TYPE_FEI_DAI		= 3,
		RX_TYPE_SHUN		= 4,
		RX_TYPE_LIANDUI		= 5,
		RX_TYPE_BOOM		= 6,
		RX_TYPE_WANGZHA		= 7,
		RX_TYPE_SIDAIER		= 8,
		RX_TYPE_SAN_BUDAI	= 9,
		RX_TYPE_SAN_DAI		= 10,
		RX_TYPE_BUCHU		= 11,
	}

	[XmlRoot("cardset")]
	public class RX_CardSet
	{
		#region 属性设置
		//牌列表
		private List<RX_Card> 	card_list;
		//牌类型
		private RX_CARD_SET		card_type;
		//牌大小
		private RX_CARD_LEVEL	card_level;


		/// <summary>
		/// Gets or sets the list.
		/// </summary>
		/// <value>The list.</value>
		[XmlArray("crads"),XmlArrayItem("card")]
		public List<RX_Card> Lister{
			set{ 
				card_list = value;
			}
			get{ 
				return card_list;
			}
		}
		[XmlElement("type")]
		public RX_CARD_SET Typer{
			set{ 
				card_type = value;
			}
			get{ 
				return card_type;
			}
		}
		[XmlElement("level")]
		public RX_CARD_LEVEL Level{
			set{ 
				card_level = value;
			}
			get{ 
				return card_level;
			}
		}
		#endregion


		/// <summary>
		/// Initializes a new instance of the <see cref="AssemblyCSharp.RX_CardSet"/> class.
		/// </summary>
		public RX_CardSet ()
		{
		}

		public override string ToString ()
		{
			return LO_XMLTool.Serializer (typeof(RX_CardSet), this);
		}
	}
}

