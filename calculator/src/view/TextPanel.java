package view;

import java.awt.BorderLayout;
import java.awt.Color;
import java.awt.Font;
import java.awt.GridLayout;

import javax.swing.BoxLayout;
import javax.swing.JButton;
import javax.swing.JLabel;
import javax.swing.JPanel;
import javax.swing.SwingConstants;

public class TextPanel extends JPanel
{
	public TextPanel()
	{
		JPanel totalPanel = new JPanel();
		totalPanel.setLayout(new BoxLayout(totalPanel,BoxLayout.Y_AXIS));
		totalPanel.setBackground(Color.GRAY);
		
		JPanel topPanel = new JPanel();
		topPanel.setLayout(new BorderLayout());
		JButton backButton = new JButton("<-");
		topPanel.add(backButton, BorderLayout.EAST);
		
		JPanel centerPanel = new JPanel();
		centerPanel.setLayout(new BorderLayout());
		JLabel calculationProcess = new JLabel("=");
		calculationProcess.setFont(new Font("맑은 고딕", 0, 40));
		calculationProcess.setForeground(Color.DARK_GRAY);
		centerPanel.add(calculationProcess, BorderLayout.CENTER);
		
		JPanel buttomPanel = new JPanel();
		buttomPanel.setLayout(new BorderLayout());	
		JLabel calculationResult = new JLabel("0");
		calculationResult.setFont(new Font("맑은 고딕", Font.BOLD , 55));
		calculationResult.setForeground(Color.DARK_GRAY);	
		buttomPanel.add(calculationResult, BorderLayout.CENTER);
		
		totalPanel.add(backButton);
		totalPanel.add(calculationProcess);
		totalPanel.add(calculationResult);
		add(totalPanel);
	}
}
