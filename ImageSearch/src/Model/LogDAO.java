package Model;

import java.sql.*;
import java.text.SimpleDateFormat;
import java.time.LocalDate;
import java.time.LocalDateTime;
import java.util.ArrayList;
import java.util.Date;

import Utility.Constant;

public class LogDAO 
{
	private SimpleDateFormat timeFormat = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss");
	private Connection connection;
	private String sql;
	
	private Connection getConnection() throws ClassNotFoundException, SQLException
	{
		Connection connection = DriverManager.getConnection(Constant.DATABASE_CONNECTION_INFORMATION, Constant.DATABASE_USER_NAME, Constant.DATABASE_PASSWORD);
		return connection;
	}
	
	public void AddLog(String searchWord)
	{
		try 
		{
			connection = getConnection();
			Date today = new Date();
			
			if (IsAlreaySearchedWord(searchWord))
				sql = String.format(Constant.UPDATE_LOG_QUERY,(timeFormat.format(today)), searchWord);
			else
				sql = String.format(Constant.INSERT_LOG_QUERY, searchWord, (timeFormat.format(today)));
			Statement statement = connection.createStatement();
			statement.execute(sql);
			connection.close();
		} 
		catch (ClassNotFoundException | SQLException e) 
		{
			e.printStackTrace();
		}
	}
	
	public void DeleteAllLog() 
	{
		try 
		{
			connection = getConnection();
			sql = Constant.DELETE_LOG_QUERY;
			Statement statement = connection.createStatement();
			statement.execute(sql);
			connection.close();
		} 
		catch (ClassNotFoundException | SQLException e) 
		{
			e.printStackTrace();
		}
	}
	
	public ResultSet GetLogList()
	{
		ResultSet resultSet = null;
		try 
		{
			connection = getConnection();
			sql = String.format(Constant.SELECT_LOG_QUERY, Constant.LOG_FILED_ALL);
			Statement statement = connection.createStatement();
			resultSet = statement.executeQuery(sql);
		} 
		catch (ClassNotFoundException | SQLException e) 
		{
			e.printStackTrace();
		}
		
		return resultSet;
	}
	
	private ArrayList<String> GetSearchedWordList()
	{
		ArrayList<String> searchedWordList = new ArrayList<String>();
		try 
		{
			connection = getConnection();
			sql = String.format(Constant.SELECT_LOG_QUERY, Constant.LOG_FILED_SEARCH_WORD);
			Statement statement = connection.createStatement();
			ResultSet resultSet = statement.executeQuery(sql);
			while(resultSet.next())
			{
				searchedWordList.add(resultSet.getString(Constant.LOG_FILED_SEARCH_WORD));
			}
			resultSet.close();
		} 
		catch (ClassNotFoundException | SQLException e) 
		{
			e.printStackTrace();
		}
		return searchedWordList;
	}
	
	private boolean IsAlreaySearchedWord(String searchWord)
	{
		ArrayList<String> searchedWordList = GetSearchedWordList();
		
		for (int repeat = 0; repeat < searchedWordList.size(); repeat++)
		{
			if (searchedWordList.get(repeat).equals(searchWord))
				return true;
		}
		return false;
	}
}
