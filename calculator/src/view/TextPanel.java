package view;

import java.awt.BorderLayout;
import java.awt.Color;
import java.awt.Dimension;
import java.awt.Font;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import javax.swing.BoxLayout;
import javax.swing.ImageIcon;
import javax.swing.JButton;
import javax.swing.JLabel;
import javax.swing.JPanel;
import javax.swing.JScrollBar;
import javax.swing.JScrollPane;
import javax.swing.SwingConstants;

public class TextPanel extends JPanel
{
	public JButton logButton, leftArrowButton, rightArrowButton;
	public JLabel emptyLabel, formula, answer;
	public JScrollPane formulaScroll;
	public TextPanel()
	{
		setLayout(new BoxLayout(this, BoxLayout.Y_AXIS));
		setBackground(new Color(241, 243, 249));
	
		emptyLabel = new JLabel("");
		emptyLabel.setFont(new Font("맑은 고딕", 0, 26));
		emptyLabel.setAlignmentX(RIGHT_ALIGNMENT);
		add(emptyLabel);
		
		ImageIcon logButtonIcon = new ImageIcon(TextPanel.class.getResource("/Image/clockIcon.png"));	
		logButton = new JButton(logButtonIcon);	
		logButton.setSize(logButtonIcon.getIconWidth(), logButtonIcon.getIconHeight());
		logButton.setAlignmentX(RIGHT_ALIGNMENT);
		logButton.setBorderPainted(false);
		logButton.setContentAreaFilled(false);
		add(logButton);
		
		
		JPanel formulaPanel = new JPanel();
		formulaPanel.setLayout(new BorderLayout());
		formulaPanel.setBackground(new Color(241, 243, 249));
		formulaPanel.setPreferredSize(new Dimension(10000,21));
		formulaPanel.setMaximumSize(new Dimension(10000,21));
		formulaPanel.setAlignmentX(RIGHT_ALIGNMENT);
		
		formula = new JLabel(" ");
		formula.setFont(new Font("맑은 고딕", 0, 15));
		formula.setHorizontalAlignment(SwingConstants.RIGHT);
		
		formulaScroll = new JScrollPane(formula);
		formulaScroll.setMaximumSize(new Dimension(10000,20));
		formulaScroll.setMinimumSize(new Dimension(215,20));
		JScrollBar jScrollBar = formulaScroll.getHorizontalScrollBar();
		jScrollBar.setPreferredSize(new Dimension(400,0));
		jScrollBar.setUnitIncrement(20);
		formulaScroll.getViewport().setBackground(new Color(241, 243, 249));
		formulaScroll.setBorder(null);	

		
		ImageIcon leftArrowIcon = new ImageIcon(TextPanel.class.getResource("/Image/leftArrow.png"));	
		leftArrowButton = new JButton(leftArrowIcon);	
		leftArrowButton.setAlignmentX(LEFT_ALIGNMENT);
		leftArrowButton.setPreferredSize(new Dimension(20,20));
		leftArrowButton.setMaximumSize(new Dimension(20,20));
		leftArrowButton.setBorderPainted(false);
		leftArrowButton.setContentAreaFilled(false);
		leftArrowButton.setVisible(false);	
		leftArrowButton.addActionListener(new ActionListener() {
			@Override
			public void actionPerformed(ActionEvent e) {
				jScrollBar.getModel().setValue(jScrollBar.getModel().getValue() - 200);
			}
		});
		
		ImageIcon rightArrowIcon = new ImageIcon(TextPanel.class.getResource("/Image/rightArrow.png"));		
		rightArrowButton = new JButton(rightArrowIcon);	
		rightArrowButton.setAlignmentX(RIGHT_ALIGNMENT);
		rightArrowButton.setPreferredSize(new Dimension(20,20));
		rightArrowButton.setMaximumSize(new Dimension(20,20));
		rightArrowButton.setBorderPainted(false);
		rightArrowButton.setContentAreaFilled(false);
		rightArrowButton.setVisible(false);
		rightArrowButton.addActionListener(new ActionListener() {
			@Override
			public void actionPerformed(ActionEvent e) {
				jScrollBar.getModel().setValue(jScrollBar.getModel().getValue() + 200);
			}
		});
		
		
		formulaPanel.add(leftArrowButton, BorderLayout.WEST);
		formulaPanel.add(formulaScroll, BorderLayout.CENTER);
		formulaPanel.add(rightArrowButton, BorderLayout.EAST);
		add(formulaPanel);
	
		
		answer = new JLabel("0");
		answer.setFont(new Font("맑은 고딕", 0, 45));
		answer.setAlignmentX(RIGHT_ALIGNMENT);
		answer.setHorizontalAlignment(SwingConstants.RIGHT);
		add(answer);
		
		setPreferredSize(new Dimension(322, 140));
		
	}
}
