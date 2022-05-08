package View;

import java.awt.Color;

import javax.swing.ImageIcon;
import javax.swing.JButton;
import javax.swing.JPanel;

public class SearchResultPanel extends JPanel
{
	public JButton backButton;
	private MainFrame mainFrame;
	
	public SearchResultPanel(MainFrame mainFrame)
	{
		this.mainFrame = mainFrame;
		setLayout(null);
		setBackground(Color.WHITE);
		ImageIcon imageIcon = new ImageIcon(SearchResultPanel.class.getResource("/Image/BackStage.png"));		
		backButton = new JButton(imageIcon);
		backButton.setBorderPainted(false);
		backButton.setSize(30,30);
		backButton.setLocation(740,10);
		add(backButton);
	}
}
