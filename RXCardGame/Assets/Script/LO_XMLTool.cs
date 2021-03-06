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
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace AssemblyCSharp
{
	public class LO_XMLTool
	{

		#region 反序列化
		/// <summary>
		/// 反序列化
		/// </summary>
		/// <param name="type">类型</param>
		/// <param name="xml">XML字符串</param>
		/// <returns></returns>
		public static object Deserialize(Type type, string xml)
		{
			try
			{
				using (StringReader sr = new StringReader(xml))
				{
					XmlSerializer xmldes = new XmlSerializer(type);
					return xmldes.Deserialize(sr);
				}
			}
			catch (Exception e)
			{
				NGUIDebug.Log (e);
				return null;
			}
		}
		/// <summary>
		/// 反序列化
		/// </summary>
		/// <param name="type"></param>
		/// <param name="xml"></param>
		/// <returns></returns>
		public static object Deserialize(Type type, Stream stream)
		{
			XmlSerializer xmldes = new XmlSerializer(type);
			return xmldes.Deserialize(stream);
		}
		#endregion
		
		#region 序列化
		/// <summary>
		/// 序列化
		/// </summary>
		/// <param name="type">类型</param>
		/// <param name="obj">对象</param>
		/// <returns></returns>
		public static string Serializer(Type type, object obj)
		{
			MemoryStream Stream = new MemoryStream();
			XmlSerializer xml = new XmlSerializer(type);
			try
			{
				//序列化对象
				xml.Serialize(Stream, obj);
			}
			catch (InvalidOperationException)
			{
				throw;
			}
			Stream.Position = 0;
			StreamReader sr = new StreamReader(Stream);
			string str = sr.ReadToEnd();
			
			sr.Dispose();
			Stream.Dispose();
			
			return str;
		}
		
		#endregion
	}
}

