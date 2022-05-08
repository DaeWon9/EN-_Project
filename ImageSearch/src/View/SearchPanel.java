package View;

import java.awt.Color;
import javax.swing.ImageIcon;
import javax.swing.JButton;
import javax.swing.JLabel;
import javax.swing.JPanel;
import javax.swing.JTextField;

public class SearchPanel extends JPanel
{
	public JButton searchButton, showLogButton;

	public SearchPanel()
	{
		setLayout(null);
		setBackground(Color.WHITE);
		JTextField inputTextFiled = new JTextField();
		inputTextFiled.setSize(300,30);
		inputTextFiled.setLocation(250,200);
		add(inputTextFiled);
		
		searchButton = new JButton("검색하기");
		searchButton.setSize(90,20);
		searchButton.setLocation(300, 250);
		add(searchButton);
		
		showLogButton = new JButton("기록보기");
		showLogButton.setSize(90,20);
		showLogButton.setLocation(400, 250);
		add(showLogButton);
		
		ImageIcon imageIcon = new ImageIcon(MainFrame.class.getResource("/Image/MainBackground.jpg"));
		JLabel backgroundImage = new JLabel(imageIcon);
		backgroundImage.setBounds(0,0,800,600);
		add(backgroundImage);	
	}
}
