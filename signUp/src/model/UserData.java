package model;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Date;

import utility.Constant;
import view.frame.MainFrame;
import view.panel.SignUp;


public class UserData 
{
	private Connection connection;
	private String sqlCommand;
	
	private Connection getConnection() throws ClassNotFoundException, SQLException
	{
		Connection connection = DriverManager.getConnection(Constant.DATABASE_CONNECTION_INFORMATION, Constant.DATABASE_USER_NAME, Constant.DATABASE_PASSWORD);
		return connection;
	}
	
	public String SelectPassword(String userId)
	{
		String userPassword = "";
		try 
		{
			connection = getConnection();
			sqlCommand = String.format(Constant.SELECT_QUERY, Constant.USER_FIELD_PASSWORD);
			sqlCommand = sqlCommand + " WHERE id = '" + userId + "'";
			Statement statement = connection.createStatement();
			ResultSet resultSet = statement.executeQuery(sqlCommand);
			while(resultSet.next())
			{
				userPassword =  resultSet.getString(Constant.USER_FIELD_PASSWORD);
			}
			resultSet.close();
		} 
		catch (ClassNotFoundException | SQLException e) 
		{
			System.out.println("서버오류 관리자에게 문의하세요");
		}
		return userPassword;
	}
	
	public ArrayList<String> getLoginedUserDataList(String userId)
	{
		ArrayList<String> userDataList = new ArrayList<String>();
		
		try 
		{
			connection = getConnection();
			sqlCommand = String.format(Constant.SELECT_QUERY, Constant.USER_FIELD_ALL);
			sqlCommand = sqlCommand + " WHERE id = '" + userId + "'";
			Statement statement = connection.createStatement();
			ResultSet resultSet = statement.executeQuery(sqlCommand);
			while(resultSet.next())
			{
				userDataList.add(resultSet.getString(Constant.USER_FIELD_NAME));
				userDataList.add(resultSet.getString(Constant.USER_FIELD_ID));
				userDataList.add(resultSet.getString(Constant.USER_FIELD_PASSWORD));
				userDataList.add(resultSet.getString(Constant.USER_FIELD_PASSWORD));
				userDataList.add(resultSet.getString(Constant.USER_FIELD_BIRTH));
				userDataList.add(resultSet.getString(Constant.USER_FIELD_EMAIL));
				userDataList.add(resultSet.getString(Constant.USER_FIELD_PHONE_NUMBER));
				userDataList.add(resultSet.getString(Constant.USER_FIELD_ADDRESS));
			}
			resultSet.close();
		} 
		catch (ClassNotFoundException | SQLException e) 
		{
			System.out.println("서버오류 관리자에게 문의하세요");
		}
		return userDataList;
	}
	
	public void insertUserData(ArrayList<String> userInputDataList)
	{
		SimpleDateFormat timeFormat = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss");
		Date today = new Date();
			
		String userId = userInputDataList.get(0);
		String userName = userInputDataList.get(1);
		String userPassword = userInputDataList.get(2);
		String userBirth = userInputDataList.get(3);
		String userEmail = userInputDataList.get(4);
		String userPhoneNumber = userInputDataList.get(5);
		String userAddress = userInputDataList.get(6);
		String signUpDate = timeFormat.format(today);
		
		try 
		{
			connection = getConnection();
			sqlCommand = String.format(Constant.INSERT_QUERY, userName, userId, userPassword, userBirth, userEmail
															, userPhoneNumber, userAddress, signUpDate);

			Statement statement = connection.createStatement();
			System.out.println(sqlCommand);
			statement.execute(sqlCommand);
			connection.close();
		} 
		catch (ClassNotFoundException | SQLException e) 
		{
			//e.printStackTrace();
			System.out.println("서버오류 관리자에게 문의하세요");
		}
	}
	
	public void updateUserData(String targetUserData, String userId, String field)
	{
		try 
		{
			connection = getConnection();
			sqlCommand = String.format(Constant.UPDATE_QUERY, field, targetUserData, userId);

			Statement statement = connection.createStatement();
			System.out.println(sqlCommand);
			statement.execute(sqlCommand);
			connection.close();
		} 
		catch (ClassNotFoundException | SQLException e) 
		{
			//e.printStackTrace();
			System.out.println("서버오류 관리자에게 문의하세요");
		}
	}
	//UPDATE `daewonsignup`.`user` SET `pw` = 'asd123!2' WHERE (`id` = 'dudghks12');

	public ArrayList<String> getUserIdList()
	{
		ArrayList<String> userIdList = new ArrayList<String>();

		try 
		{
			connection = getConnection();
			sqlCommand = String.format(Constant.SELECT_QUERY, Constant.USER_FIELD_ID);
			Statement statement = connection.createStatement();
			ResultSet resultSet = statement.executeQuery(sqlCommand);
			while(resultSet.next())
			{
				userIdList.add(resultSet.getString(Constant.USER_FIELD_ID));
			}
			resultSet.close();
		} 
		catch (ClassNotFoundException | SQLException e) 
		{
			System.out.println("서버오류 관리자에게 문의하세요");
		}
		return userIdList;
	}
	
	public String getUserPassword(UserData userData, String userId)
	{
		String userPassword = SelectPassword(userId);
		return userPassword;
	}
}
