package View;
import java.awt.Dimension;
import java.awt.FlowLayout;
import java.awt.Toolkit;

import javax.swing.ImageIcon;
import javax.swing.JFrame;

import Controller.ImageSearcher;
import Utility.Constant;

public class MainFrame extends JFrame
{
	Dimension windowSize = Toolkit.getDefaultToolkit().getScreenSize();
	public LogPanel logPanel = new LogPanel();
	public SearchPanel searchPanel = new SearchPanel();
	public SearchResultPanel searchResultPanel = new SearchResultPanel();

	public void ShowFrame()
	{		
		ImageIcon iconImage = new ImageIcon(MainFrame.class.getResource("/Image/Ryan.png"));		
		setIconImage(iconImage.getImage());
		setTitle(Constant.APPLICATION_TITLE);
		setSize(Constant.APPLICATION_WIDTH, Constant.APPLICATION_HEIGHT);
		setLocation((windowSize.width - Constant.APPLICATION_WIDTH) / 2,
                (windowSize.height - Constant.APPLICATION_HEIGHT) / 2); 
		setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);	
		add(searchPanel);
		setResizable(false);
		setVisible(true);	
	}	
	
	public void BackStage(String panelName)
	{
		if (panelName.equals("searchPanel"))
		{
			searchPanel.inputTextFiled.setText(""); 
			getContentPane().removeAll();
			getContentPane().add(searchPanel);
			revalidate();
			repaint();
		}
		
	}
}
