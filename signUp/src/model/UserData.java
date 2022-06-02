package model;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;
import java.util.ArrayList;
import java.util.Date;

import utility.Constant;


public class UserData 
{
	private Connection connection;
	private Connection getConnection() throws ClassNotFoundException, SQLException
	{
		Connection connection = DriverManager.getConnection(Constant.DATABASE_CONNECTION_INFORMATION, Constant.DATABASE_USER_NAME, Constant.DATABASE_PASSWORD);
		return connection;
	}

	public ArrayList<String> GetUserIdList()
	{
		ArrayList<String> userIdList = new ArrayList<String>();
		String sqlCommand; 
		try 
		{
			connection = getConnection();
			sqlCommand = String.format(Constant.SELECT_QUERY, Constant.USER_FILED_ID);
			Statement statement = connection.createStatement();
			ResultSet resultSet = statement.executeQuery(sqlCommand);
			while(resultSet.next())
			{
				userIdList.add(resultSet.getString(Constant.USER_FILED_ID));
			}
			resultSet.close();
		} 
		catch (ClassNotFoundException | SQLException e) 
		{
			e.printStackTrace();
			System.out.println("서버오류 관리자에게 문의하세요");
		}
		return userIdList;
	}
}
