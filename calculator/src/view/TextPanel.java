package view;

import java.awt.Color;
import java.awt.Dimension;
import java.awt.Font;
import javax.swing.BoxLayout;
import javax.swing.ImageIcon;
import javax.swing.JButton;
import javax.swing.JLabel;
import javax.swing.JPanel;

public class TextPanel extends JPanel
{
	public JButton logButton;
	public JLabel formula, answer;
	public TextPanel()
	{
		setLayout(new BoxLayout(this, BoxLayout.Y_AXIS));
		setBackground(new Color(241, 243, 249));
	
		ImageIcon logButtonIcon = new ImageIcon(TextPanel.class.getResource("/Image/clockIcon.png"));	
		logButton = new JButton(logButtonIcon);	
		logButton.setSize(logButtonIcon.getIconWidth(), logButtonIcon.getIconHeight());
		logButton.setAlignmentX(RIGHT_ALIGNMENT);
		logButton.setBorderPainted(false);
		logButton.setContentAreaFilled(false);
		add(logButton);
		
		formula = new JLabel(" ");
		formula.setFont(new Font("맑은 고딕", 0, 15));
		formula.setAlignmentX(RIGHT_ALIGNMENT);
		add(formula);
		
		answer = new JLabel("0");
		answer.setFont(new Font("맑은 고딕", 0, 45));
		answer.setAlignmentX(RIGHT_ALIGNMENT);
		add(answer);
		
		setPreferredSize(new Dimension(322, 140));
		
	}
}
