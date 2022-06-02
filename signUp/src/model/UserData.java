package model;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.SQLException;

import utility.Constant;


public class UserData 
{
	private Connection connection;
	private Connection getConnection() throws ClassNotFoundException, SQLException
	{
		Connection connection = DriverManager.getConnection(Constant.DATABASE_CONNECTION_INFORMATION, Constant.DATABASE_USER_NAME, Constant.DATABASE_PASSWORD);
		return connection;
	}
}
