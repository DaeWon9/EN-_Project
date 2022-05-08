package View;

import javax.swing.JButton;
import javax.swing.JFrame;
import javax.swing.JTextField;

import Utility.Constant;

public class MainFrame extends JFrame 
{
	public MainFrame()
	{
		setTitle(Constant.WINDOW_TITLE);
		setSize(Constant.WINDOW_WIDTH, Constant.WINDOW_HEIGHT);
		setLayout(null);
		
		JTextField inputTextFiled = new JTextField();
		inputTextFiled.setSize(500,30);
		inputTextFiled.setLocation(150,200);
		add(inputTextFiled);
		
		JButton searchButton = new JButton("검색하기");
		searchButton.setSize(90,20);
		searchButton.setLocation(300, 250);
		add(searchButton);
		
		JButton showLogButton = new JButton("기록보기");
		showLogButton.setSize(90,20);
		showLogButton.setLocation(400, 250);
		add(showLogButton);
	}
	
	public void Show()
	{
		setVisible(true);	
	}
}
