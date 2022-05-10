package Controller;

import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;

import javax.swing.JOptionPane;
import javax.swing.JScrollPane;
import javax.swing.JTextArea;

import Model.LogDAO;
import Utility.Constant;

public class LogManagement 
{
	public void LogInputToLogPanel(LogDAO logDAO, JTextArea logArea) // db에 있는 로그정보들을 로그패널의 txtArea에 추가해주는 함수
	{
		ResultSet logList = logDAO.GetLogList();
		logArea.selectAll();
		logArea.replaceSelection("");
		try 
		{
			while(logList.next())
			{
				logArea.append("          " + logList.getString(Constant.LOG_FILED_SEARCH_TIME) + "\t" + logList.getString(Constant.LOG_FILED_SEARCH_WORD) + "\n");
			}
			logList.close();
		}
		catch (SQLException e) 
		{
			JOptionPane.showMessageDialog(null, "로그출력에 문제가 생겼습니다. 관리자에게 문의하세요");
		}
	}
}
