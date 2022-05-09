package Controller;

import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;

import javax.swing.JTextArea;

import Model.LogDAO;
import Utility.Constant;

public class LogManagement 
{
	public void LogInputToLogPanel(LogDAO logDAO, JTextArea logArea)
	{
		ResultSet logList = logDAO.GetLogList();
		logArea.selectAll();
		logArea.replaceSelection("");
		try 
		{
			while(logList.next())
			{
				logArea.append("\t" + logList.getString(Constant.LOG_FILED_SEARCH_TIME) + "\t" + logList.getString(Constant.LOG_FILED_SEARCH_WORD) + "\n");
			}
		}
		catch (SQLException e) 
		{
			e.printStackTrace();
		}
	}
}
