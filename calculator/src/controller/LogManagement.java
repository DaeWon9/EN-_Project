package controller;


import java.awt.Dimension;
import java.awt.Font;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.util.Random;

import javax.swing.JButton;
import javax.swing.JPanel;
import javax.swing.SwingConstants;

import Utility.Constant;


public class LogManagement
{
	private Font buttonFont = new Font("맑은 고딕", 0, 20);
	
	public void addLog(JPanel logPanel, String formula, String answer)
	{ 
		String logString = String.format(Constant.LOG_STRING_FORM,formula, answer);
		JButton logButton = new JButton(logString);
		logButton.setHorizontalAlignment(SwingConstants.RIGHT);
		logButton.setFont(buttonFont);
		logButton.setMinimumSize(new Dimension(350,60));
		logButton.setFocusPainted(false); 
		logButton.setContentAreaFilled(false);
		logButton.addActionListener(new ActionListener() {
			
			@Override
			public void actionPerformed(ActionEvent e) {
				System.out.println("식 : " + formula);
				System.out.println("답 : " + answer);
				
			}
		});
		
		
		logPanel.add(logButton,1);
	}
}
