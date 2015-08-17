using System;
using Mono.Data.Sqlite;
using System.Data.Sql;
using UnityEngine;

namespace AssemblyCSharp
{
	public class LO_SQLITETool
	{
		private SqliteConnection connection = null;
		private LO_SQLITETool ()
		{
		}
		private LO_SQLITETool (string dbname)
		{
			string conn_string = "Data Source=";
			conn_string += Application.persistentDataPath + "/";
			conn_string += dbname + ".sqlite";

			connection = new SqliteConnection (conn_string);
			connection.Open ();
		}

		/*
		 * 			Data Source=C:\SQLITEDATABASES\SQLITEDB1.sqlite;Version=3;
			SqliteConnection conn = new SqliteConnection ("");
			*/

		private static LO_SQLITETool s_LO_SQLITETool = null;
		public static LO_SQLITETool DefaultTool(string dbname)
		{
			
				if (s_LO_SQLITETool == null) {
					s_LO_SQLITETool = new LO_SQLITETool (dbname);
				}
				return s_LO_SQLITETool;

		}
	}
}

