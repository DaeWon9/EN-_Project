package view;

import java.awt.BorderLayout;
import java.awt.Dimension;
import java.awt.Toolkit;

import javax.swing.BoxLayout;
import javax.swing.ImageIcon;
import javax.swing.JFrame;
import javax.swing.JPanel;


public class MainFrame extends JFrame
{
	public TextPanel textPanel = new TextPanel();
	public ButtonPanel buttonPanel = new ButtonPanel();
	Dimension windowSize = Toolkit.getDefaultToolkit().getScreenSize();
	public void showFrame()
	{
		setTitle("계산기");
		setMinimumSize(new Dimension(335, 502));
		ImageIcon imageIcon = new ImageIcon(MainFrame.class.getResource("/Image/calculatorIcon.png"));		
		setIconImage(imageIcon.getImage());
		setDefaultCloseOperation(EXIT_ON_CLOSE);
		setLocation((windowSize.width - 335) / 2, (windowSize.height - 502) / 2); 	
		
		add(textPanel, BorderLayout.NORTH);
		add(buttonPanel, BorderLayout.CENTER);
		setSize(335,510);
		setVisible(true);
		
		setFocusable(true);
		requestFocus();
	}
}