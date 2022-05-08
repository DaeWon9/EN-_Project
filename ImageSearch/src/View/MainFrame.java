package View;
import java.awt.Dimension;
import java.awt.Toolkit;
import javax.swing.JFrame;
import Utility.Constant;

public class MainFrame extends JFrame
{
	Dimension windowSize = Toolkit.getDefaultToolkit().getScreenSize();
	public LogPanel logPanel = new LogPanel();
	public SearchPanel searchPanel = new SearchPanel();
	public SearchResultPanel searchResultPanel = new SearchResultPanel();
	
	public void ShowFrame()
	{		
		setTitle(Constant.APPLICATION_TITLE);
		setSize(Constant.APPLICATION_WIDTH, Constant.APPLICATION_HEIGHT);
		setLocation((windowSize.width - Constant.APPLICATION_WIDTH) / 2,
                (windowSize.height - Constant.APPLICATION_HEIGHT) / 2); 
		setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);		
		add(searchPanel);	
		setVisible(true);	
	}	
	
	public void Change(String panelName)
	{
		if (panelName.equals("logPanel"))
		{
			getContentPane().removeAll();
			getContentPane().add(logPanel);
			revalidate();
			repaint();
		}
		else if (panelName.equals("searchPanel"))
		{
			getContentPane().removeAll();
			getContentPane().add(searchPanel);
			revalidate();
			repaint();
		}
		else if (panelName.equals("searchResultPanel"))
		{
			getContentPane().removeAll();
			getContentPane().add(searchResultPanel);
			revalidate();
			repaint();
		}
		
	}
}
