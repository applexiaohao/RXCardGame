using System;
using System.Collections.Generic;

namespace AssemblyCSharp
{
	public class RX_PopInfo
	{
		private string error_info;
		public string ErrorInfo{
			set{ 
				error_info = value;
			}
			get{ 
				return error_info;
			}
		}

		private RX_PopCardSet pop_cardset;
		public RX_PopCardSet PopCardSet{
			set{ 
				this.pop_cardset = value;
			}
			get{ 
				return this.pop_cardset;
			}
		}

		public RX_PopInfo()
		{
		}
	}
	public class RX_PopCardSet
	{
		public RX_PopCardSet ()
		{
		}

		private List<RX_CardSet> list_cardset;
		public List<RX_CardSet> ListCardSet{
			get{ 
				if (list_cardset == null) {
					list_cardset = new List<RX_CardSet> ();
				}
				return list_cardset;
			}
		}

		private int CardSetCount()
		{
			
			return this.ListCardSet.Count;

		}

		public RX_CardSet Last()
		{
			return this.ListCardSet[this.CardSetCount() - 1];
		}

		public RX_CardSet Last2()
		{
			return this.ListCardSet[this.CardSetCount() - 2];
		}
		public void AddCardSet(RX_CardSet sender)  
		{
			this.ListCardSet.Add(sender);
		}
	}
}

