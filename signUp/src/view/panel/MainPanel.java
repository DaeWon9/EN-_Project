package view.panel;

import java.awt.Image;

import javax.swing.ImageIcon;
import javax.swing.JButton;
import javax.swing.JLabel;
import javax.swing.JPanel;

import view.ImageButton;

public class MainPanel extends JPanel
{
	public JButton editButton, withdrawalButton, backStageButton;
	
	public MainPanel()
	{
		setLayout(null);
		
		ImageIcon editButtonImage = new ImageIcon(MainPanel.class.getResource("/image/editButton.png"));		
		ImageIcon editButtonImage2 = new ImageIcon(MainPanel.class.getResource("/image/s_editButton.png"));	
		editButton = new ImageButton(editButtonImage, editButtonImage2, 272, 93).get();
		add(editButton);
	
		ImageIcon withdrawalButtonImage = new ImageIcon(MainPanel.class.getResource("/image/withdrawalButton.png"));
		ImageIcon withdrawalButtonImage2 = new ImageIcon(MainPanel.class.getResource("/image/s_withdrawalButton.png"));
		withdrawalButton = new ImageButton(withdrawalButtonImage, withdrawalButtonImage2, 272, 159).get();
		add(withdrawalButton);
		
		// 뒤로가기 버튼 설정
		ImageIcon backStageButtonImage = new ImageIcon(MainPanel.class.getResource("/image/backStage.png"));		
		ImageIcon backStageButtonImage2 = new ImageIcon(MainPanel.class.getResource("/image/s_backStage.png"));	
		backStageButton = new ImageButton(backStageButtonImage, backStageButtonImage2, 750, 4).get();
		add(backStageButton);
		
		ImageIcon imageIcon = new ImageIcon(MainPanel.class.getResource("/image/mainScreen.jpg"));
		Image image = imageIcon.getImage().getScaledInstance(800, 500, Image.SCALE_SMOOTH);
		JLabel backgroundImage = new JLabel(new ImageIcon(image));
		backgroundImage.setBounds(0,0,800,500);
		add(backgroundImage);	
	}
}
