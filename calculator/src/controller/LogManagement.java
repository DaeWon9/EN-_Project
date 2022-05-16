package controller;


import java.awt.Dimension;
import java.awt.Font;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.math.BigDecimal;
import java.util.Random;

import javax.swing.JButton;
import javax.swing.JFrame;
import javax.swing.JLabel;
import javax.swing.JPanel;
import javax.swing.SwingConstants;

import Utility.Constant;
import model.AnswerDTO;
import model.OperandDTO;
import model.OperatorDTO;


public class LogManagement
{
	private Font buttonFont = new Font("맑은 고딕", 0, 20);
	private JPanel logPanel;
	private OperandDTO operandDTO;
	private OperatorDTO operatorDTO;
	private AnswerDTO answerDTO;
	
	public LogManagement(JPanel logPanel, OperandDTO operandDTO, OperatorDTO operatorDTO, AnswerDTO answerDTO)
	{
		this.logPanel = logPanel;
		this.operandDTO = operandDTO;
		this.operatorDTO = operatorDTO;
		this.answerDTO = answerDTO;
	}
	
	public void addLog(JLabel formulaLabel, JLabel answerLabel, BigDecimal leftOperand, String operator, BigDecimal RightOperand, String answer)
	{ 
		String formula = leftOperand + operator + RightOperand;
		String logString = String.format(Constant.LOG_STRING_FORM, formula, answer);
		JButton logButton = new JButton(logString);
		logButton.setHorizontalAlignment(SwingConstants.RIGHT);
		logButton.setFont(buttonFont);
		logButton.setMinimumSize(new Dimension(350,60));
		logButton.setFocusPainted(false); 
		logButton.setContentAreaFilled(false);
		logButton.addActionListener(new ActionListener() {
			
			@Override
			public void actionPerformed(ActionEvent e) {
				formulaLabel.setText(leftOperand + operator + RightOperand + "=");
				answerLabel.setText(answer);
				operandDTO.setLeftOperand(leftOperand);
				operandDTO.setRightOperand(RightOperand);
				operatorDTO.setLast(operator);			
				answerDTO.set(answer);			
			}
		});
		
		
		logPanel.add(logButton,1);
	}
}
