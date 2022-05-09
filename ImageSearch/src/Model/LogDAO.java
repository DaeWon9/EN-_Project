package Model;

import java.sql.*;
import java.text.SimpleDateFormat;
import java.time.LocalDate;
import java.time.LocalDateTime;
import java.util.Date;

import Utility.Constant;

public class LogDAO 
{
	SimpleDateFormat timeFormat = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss");
	
	private Connection getConnection() throws ClassNotFoundException, SQLException
	{
		Connection connection = DriverManager.getConnection(Constant.DATABASE_CONNECTION_INFORMATION, Constant.DATABASE_USER_NAME, Constant.DATABASE_PASSWORD);
		return connection;
	}
	
	public void AddLog(String searchWord)
	{
		Connection connection;
		try 
		{
			connection = getConnection();
			Date today = new Date();
			String sql = String.format(Constant.INSERT_LOG_QUERY, searchWord, (timeFormat.format(today)));
			Statement statement = connection.createStatement();
			statement.execute(sql);
		} 
		catch (ClassNotFoundException | SQLException e) 
		{
			e.printStackTrace();
		}
	}
}
