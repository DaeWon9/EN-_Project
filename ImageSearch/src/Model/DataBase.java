package Model;

import java.sql.*;

import Utility.Constant;

public class DataBase 
{
	public void AddLog()
	{
		try
		{
			Connection connection = DriverManager.getConnection(Constant.DATABASE_CONNECTION_INFORMATION, Constant.DATABASE_USER_NAME, Constant.DATABASE_PASSWORD);
			if (connection != null)
			{
				System.out.println("연결성공");
			}
		}
		catch (SQLException e) 
		{
			System.out.println("SQL Exception : " + e.getMessage()); 
			e.printStackTrace(); 
		}

	}

}
