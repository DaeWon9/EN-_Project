package controller;


import java.awt.Dimension;
import java.awt.Font;
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
		Random rand = new Random();
		String logString = String.format(Constant.LOG_STRING_FORM,formula, answer);
		JButton logButton = new JButton(logString);
		logButton.setHorizontalAlignment(SwingConstants.RIGHT);
		logButton.setFont(buttonFont);
		logButton.setMinimumSize(new Dimension(350,60));
		logButton.setFocusPainted(false); 
		logButton.setContentAreaFilled(false);
		logPanel.add(logButton,1);
	}
}
