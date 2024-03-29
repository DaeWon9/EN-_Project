package View;

import java.awt.Color;
import java.awt.Image;

import javax.swing.ImageIcon;
import javax.swing.JButton;
import javax.swing.JLabel;
import javax.swing.JPanel;
import javax.swing.JTextField;

public class SearchPanel extends JPanel
{
	public JButton searchButton, showLogButton;
	public JTextField inputTextFiled;

	public SearchPanel()
	{
		setLayout(null);
		setBackground(Color.WHITE);
		inputTextFiled = new JTextField();
		inputTextFiled.setSize(300,30);
		inputTextFiled.setLocation(250,200);
		add(inputTextFiled);
		
		
		ImageIcon searchButtonImage = new ImageIcon(SearchPanel.class.getResource("/Image/SearchButton.png"));		
		searchButton = new JButton(searchButtonImage);
		searchButton.setBorderPainted(false);
		searchButton.setSize(90,20);
		searchButton.setLocation(300, 250);
		add(searchButton);
		
		ImageIcon showLogButtonImage = new ImageIcon(SearchPanel.class.getResource("/Image/ShowLogButton.png"));		
		showLogButton = new JButton(showLogButtonImage);
		showLogButton.setBorderPainted(false);
		showLogButton.setSize(90,20);
		showLogButton.setLocation(400, 250);
		add(showLogButton);
			
		ImageIcon imageIcon = new ImageIcon(MainFrame.class.getResource("/Image/MainBackground.jpg"));
		Image image = imageIcon.getImage().getScaledInstance(800, 600, Image.SCALE_SMOOTH);
		JLabel backgroundImage = new JLabel(new ImageIcon(image));
		backgroundImage.setBounds(0,0,800,600);
		add(backgroundImage);	
	}
}

