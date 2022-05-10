package View;

import java.awt.Color;
import java.awt.Font;
import java.awt.Image;

import javax.imageio.ImageIO;
import javax.swing.ImageIcon;
import javax.swing.JButton;
import javax.swing.JLabel;
import javax.swing.JPanel;
import javax.swing.JScrollPane;
import javax.swing.JTextArea;

public class LogPanel extends JPanel
{
	public JButton backButton, deleteButton;
	public JTextArea txtLog = new JTextArea();	
	public LogPanel()
	{
		setBackground(Color.WHITE);
		setLayout(null);
		ImageIcon imageIcon = new ImageIcon(SearchResultPanel.class.getResource("/Image/BackStage.png"));		
		backButton = new JButton(imageIcon);
		backButton.setBorderPainted(false);
		backButton.setContentAreaFilled(false);
		backButton.setSize(30,30);
		backButton.setLocation(740,10);
		add(backButton);
		
		ImageIcon imageIcon2 = new ImageIcon(SearchResultPanel.class.getResource("/Image/SearchHistory.png"));		
		Image image = imageIcon2.getImage().getScaledInstance(235, 50, Image.SCALE_SMOOTH);
		JLabel logLabel = new JLabel(imageIcon2);
		logLabel.setSize(235,50);
		logLabel.setLocation(282,20);
		add(logLabel);
		
		Font font = new Font("굴림", Font.BOLD, 15);
		txtLog.setFont(font);
		
		txtLog.setEditable(false);
		JScrollPane scrollPane = new JScrollPane(txtLog);
		scrollPane.setSize(500,400);
		scrollPane.setLocation(150,80);
		add(scrollPane);
		
		ImageIcon DeleteButtonImage = new ImageIcon(LogPanel.class.getResource("/Image/DeleteButton.png"));		
		deleteButton = new JButton(DeleteButtonImage);
		deleteButton.setBorderPainted(false);
		deleteButton.setSize(90,20);
		deleteButton.setLocation(560, 480);
		add(deleteButton);
	}
}
