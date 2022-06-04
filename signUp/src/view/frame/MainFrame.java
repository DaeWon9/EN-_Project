package view.frame;

import java.awt.Dimension;
import java.awt.Toolkit;
import javax.swing.ImageIcon;
import javax.swing.JFrame;

import view.panel.Edit;
import view.panel.Login;
import view.panel.MainPanel;
import view.panel.SignUp;

public class MainFrame extends JFrame
{
	Dimension windowSize = Toolkit.getDefaultToolkit().getScreenSize();
	public Login loginPanel = new Login();
	public SignUp signUpPanel = new SignUp(); 
	public Edit editPanel = new Edit();
	public MainPanel mainPanel = new MainPanel();

	public void ShowFrame()
	{		
		ImageIcon iconImage = new ImageIcon(MainFrame.class.getResource("/Image/Ryan.png"));		
		setIconImage(iconImage.getImage());
		setTitle("EN# FRIENDS");
		setSize(800, 539);
		setLocation((windowSize.width - 800) / 2,
                (windowSize.height - 539) / 2); 
		setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);	
		add(loginPanel);
		setResizable(false);
		setVisible(true);	
	}	
}
