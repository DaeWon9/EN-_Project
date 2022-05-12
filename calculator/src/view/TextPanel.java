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
	public JLabel calculationProcess, calculationResult;
	public TextPanel()
	{
		setLayout(new BoxLayout(this, BoxLayout.Y_AXIS));
		setBackground(Color.GRAY);
		
	
		logButton = new JButton("시계");
		logButton.setAlignmentX(RIGHT_ALIGNMENT);
		add(logButton);
		
		calculationProcess = new JLabel("=");
		calculationProcess.setFont(new Font("맑은 고딕", 0, 30));
		calculationProcess.setForeground(Color.WHITE);
		calculationProcess.setAlignmentX(RIGHT_ALIGNMENT);
		add(calculationProcess);
		

		calculationResult = new JLabel("0");
		calculationResult.setFont(new Font("맑은 고딕", Font.BOLD , 55));
		calculationResult.setForeground(Color.WHITE);	
		calculationResult.setAlignmentX(SwingConstants.RIGHT);
		add(calculationResult);
		

	}
}
