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

namespace AssemblyCSharp
{
	public class RX_Resources
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="AssemblyCSharp.RX_Resources"/> class.
		/// </summary>
		private RX_Resources ()
		{
			card_atlas = (UIAtlas)Resources.Load("Puke",typeof(UIAtlas));
		}

		private static RX_Resources s_RX_Resources = null;

		public static RX_Resources DefaultResources{
			get{
				if (s_RX_Resources == null) {
					s_RX_Resources = new RX_Resources();
				}

				return s_RX_Resources;
			}
		}


		private UIAtlas card_atlas;
		public UIAtlas CardAtlas{
			get{
				return card_atlas;
			}
		}
	}
}
