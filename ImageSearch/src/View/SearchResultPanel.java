package View;

import java.awt.Color;
import java.awt.FlowLayout;

import javax.swing.BoxLayout;
import javax.swing.ImageIcon;
import javax.swing.JButton;
import javax.swing.JPanel;

public class SearchResultPanel extends JPanel
{
	public JButton backButton;
	
	public SearchResultPanel()
	{
		setBounds(0,0,800,200);
		setBackground(Color.WHITE);
		setLayout(null);
		ImageIcon imageIcon = new ImageIcon(SearchResultPanel.class.getResource("/Image/BackStage.png"));		
		backButton = new JButton(imageIcon);
		backButton.setBorderPainted(false);
		backButton.setSize(30,30);
		backButton.setLocation(740,10);
		add(backButton);
	}
}