package view;

import java.awt.Dimension;
import java.awt.Toolkit;
import javax.swing.ImageIcon;
import javax.swing.JFrame;
import view.panel.Login;

public class MainFrame extends JFrame
{
	Dimension windowSize = Toolkit.getDefaultToolkit().getScreenSize();
	public Login loginPanel = new Login();

	public void ShowFrame()
	{		
		//ImageIcon iconImage = new ImageIcon(MainFrame.class.getResource("/Image/Ryan.png"));		
		//setIconImage(iconImage.getImage());
		setTitle("EN# FRIENDS");
		setSize(800, 500);
		setLocation((windowSize.width - 800) / 2,
                (windowSize.height - 500) / 2); 
		setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);	
		add(loginPanel);
		setResizable(false);
		setVisible(true);	
	}	
}
