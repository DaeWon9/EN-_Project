package view;

import java.awt.Color;
import java.awt.Font;
import javax.swing.BoxLayout;
import javax.swing.JButton;
import javax.swing.JLabel;
import javax.swing.JPanel;
import javax.swing.SwingConstants;

public class TextPanel extends JPanel
{
	public JButton logButton;
	public JLabel formula, answer;
	public TextPanel()
	{
		setLayout(new BoxLayout(this, BoxLayout.Y_AXIS));
		setBackground(Color.GRAY);
		
	
		logButton = new JButton("시계");
		logButton.setAlignmentX(RIGHT_ALIGNMENT);
		add(logButton);
		
		formula = new JLabel(" ");
		formula.setFont(new Font("맑은 고딕", 0, 20));
		formula.setForeground(Color.WHITE);
		formula.setAlignmentX(RIGHT_ALIGNMENT);
		add(formula);
		

		answer = new JLabel("0");
		answer.setFont(new Font("맑은 고딕", Font.BOLD , 38));
		answer.setForeground(Color.WHITE);	
		answer.setAlignmentX(SwingConstants.RIGHT);
		add(answer);
		

	}
}
