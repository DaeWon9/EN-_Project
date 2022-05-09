package View;

import java.awt.Color;
import java.awt.FlowLayout;

import javax.swing.BoxLayout;
import javax.swing.ImageIcon;
import javax.swing.JButton;
import javax.swing.JComboBox;
import javax.swing.JPanel;
import javax.swing.JTextField;

public class SearchResultPanel extends JPanel
{
	public JButton backButton, searchButton, showLogButton;
	public JTextField inputTextFiled;
	public JComboBox displayBox;
	
	public SearchResultPanel()
	{
		setBounds(0,0,800,170);
		setBackground(Color.WHITE);
		setLayout(null);
		ImageIcon imageIcon = new ImageIcon(SearchResultPanel.class.getResource("/Image/BackStage.png"));		
		backButton = new JButton(imageIcon);
		backButton.setBorderPainted(false);
		backButton.setContentAreaFilled(false);
		backButton.setSize(30,30);
		backButton.setLocation(740,10);
		add(backButton);
		
		inputTextFiled = new JTextField();
		inputTextFiled.setSize(500,30);
		inputTextFiled.setLocation(100,100);
		add(inputTextFiled);
		
	
		ImageIcon searchButtonImage = new ImageIcon(SearchResultPanel.class.getResource("/Image/SearchButton.png"));		
		searchButton = new JButton(searchButtonImage);
		searchButton.setBorderPainted(false);
		searchButton.setSize(90,20);
		searchButton.setLocation(620, 100);
		add(searchButton);
		
		String[] display ={"10", "20", "30"};
		displayBox = new JComboBox(display);
		displayBox.setSize(90,20);
		displayBox.setLocation(620, 125);
        add(displayBox);

		
	}
}
