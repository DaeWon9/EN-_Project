package controller;


import java.awt.Dimension;
import java.util.Random;

import javax.swing.JButton;
import javax.swing.JPanel;

import Utility.Constant;


public class LogManagement
{
	public void addLog(JPanel logPanel, String formula, String answer)
	{
		Random rand = new Random();
		String logString = String.format(Constant.LOG_STRING_FORM,formula, answer);
		JButton logButton = new JButton(logString);
		logButton.setMaximumSize(new Dimension(350,40));
		logButton.setFocusPainted(false); 
		logButton.setContentAreaFilled(false);
		logPanel.add(logButton,1);
	}
}
