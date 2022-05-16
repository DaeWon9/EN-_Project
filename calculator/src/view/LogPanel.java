package view;

import java.awt.BorderLayout;
import java.awt.Color;
import java.awt.Dimension;
import java.awt.Font;

import javax.swing.BoxLayout;
import javax.swing.ImageIcon;
import javax.swing.JButton;
import javax.swing.JLabel;
import javax.swing.JPanel;
import javax.swing.JScrollPane;

public class LogPanel extends JPanel
{
	public JButton deleteButton;
	public JLabel titleLabel, topLabel;
	public JPanel logButtonPanel;
	public LogPanel() 
	{
		
		setLayout(new BorderLayout());
		setBackground(new Color(241, 243, 249));
		setPreferredSize(new Dimension(250,140));

		titleLabel = new JLabel(" [ 기록 ]");
		titleLabel.setFont(new Font("맑은 고딕", 0, 20));
		titleLabel.setAlignmentX(LEFT_ALIGNMENT);
		add(titleLabel, BorderLayout.NORTH);

		
		logButtonPanel = new JPanel();
		logButtonPanel.setBackground(new Color(241, 243, 249));
		logButtonPanel.setLayout(new BoxLayout(logButtonPanel, BoxLayout.Y_AXIS));
		
		topLabel = new JLabel("아직 기록이 없음");
		topLabel.setFont(new Font("맑은 고딕", 0, 15));
		topLabel.setAlignmentX(LEFT_ALIGNMENT);
		logButtonPanel.add(topLabel);
		
		add(logButtonPanel, BorderLayout.CENTER);
		
		
		ImageIcon deleteButtonIcon = new ImageIcon(LogPanel.class.getResource("/Image/deleteIcon.png"));	
		deleteButton = new JButton(deleteButtonIcon);	
		deleteButton.setSize(deleteButtonIcon.getIconWidth(), deleteButtonIcon.getIconHeight());
		deleteButton.setAlignmentX(RIGHT_ALIGNMENT);
		deleteButton.setBorderPainted(false);
		deleteButton.setContentAreaFilled(false);
		add(deleteButton, BorderLayout.SOUTH);
	}
}
