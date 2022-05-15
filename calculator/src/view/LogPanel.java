package view;

import java.awt.Color;
import java.awt.Dimension;
import java.awt.Font;

import javax.swing.BoxLayout;
import javax.swing.ImageIcon;
import javax.swing.JButton;
import javax.swing.JLabel;
import javax.swing.JPanel;

public class LogPanel extends JPanel
{
	public JButton button;
	public LogPanel()
	{
		setLayout(new BoxLayout(this, BoxLayout.Y_AXIS));
		setBackground(new Color(241, 243, 249));
		setPreferredSize(new Dimension(250, 140));

		
		button = new JButton("TEST");
		button.setAlignmentX(RIGHT_ALIGNMENT);
		button.setBorderPainted(false);
		button.setContentAreaFilled(false);
		add(button);
	
		
	}
}
