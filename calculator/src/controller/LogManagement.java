package controller;


import java.awt.Color;
import java.awt.Dimension;
import java.awt.Font;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.awt.event.MouseEvent;
import java.awt.event.MouseListener;
import java.math.BigDecimal;
import java.util.Random;

import javax.swing.JButton;
import javax.swing.JFrame;
import javax.swing.JLabel;
import javax.swing.JPanel;
import javax.swing.SwingConstants;

import Utility.Constant;
import Utility.DataProcessing;
import model.AnswerDTO;
import model.InputNumberDTO;
import model.OperandDTO;
import model.OperatorDTO;
import view.LogPanel;


public class LogManagement
{
	private Font buttonFont = new Font("맑은 고딕", 0, 20);
	private LogPanel logPanel;
	private OperandDTO operandDTO;
	private OperatorDTO operatorDTO;
	private AnswerDTO answerDTO;
	private InputNumberDTO inputNumberDTO;
	
	public LogManagement(LogPanel logPanel, OperandDTO operandDTO, OperatorDTO operatorDTO, AnswerDTO answerDTO, InputNumberDTO inputNumberDTO)
	{
		this.logPanel = logPanel;
		this.operandDTO = operandDTO;
		this.operatorDTO = operatorDTO;
		this.answerDTO = answerDTO;
		this.inputNumberDTO = inputNumberDTO;
	}
	
	public void addLog(JLabel formulaLabel, JLabel answerLabel, BigDecimal leftOperand, String operator, BigDecimal RightOperand, String answer)
	{ 
		logPanel.topLabel.setText(" ");
		logPanel.deleteButton.setVisible(true);
		String log_string_form;
		String logString;
		String formula = DataProcessing.getDataProcessing().numberFormat(leftOperand.toString()) + " " + operator + " " + DataProcessing.getDataProcessing().numberFormat(RightOperand.toString());
		formula = DataProcessing.getDataProcessing().deleteComma(formula);
		if (formula.length() > 25)
		{
			log_string_form = "<HTML><body><p style='font-size:11px;text-align:right;'>%s</p><p style='font-size:11px;text-align:right;'> %s = </p><p style='font-size:13px;text-align:right;'><strong>%s</strong></p></body></HTML>";
			logString = String.format(log_string_form, DataProcessing.getDataProcessing().numberFormat(leftOperand.toString()) + " " + operator, DataProcessing.getDataProcessing().numberFormat(RightOperand.toString()), DataProcessing.getDataProcessing().numberFormat(answer));
		}
		else
		{
			log_string_form = "<HTML><body><p style='font-size:11px;text-align:right;'> %s = </p><p style='font-size:13px;text-align:right;'><strong>%s</strong></p></body></HTML>";
			logString = String.format(log_string_form, formula, DataProcessing.getDataProcessing().numberFormat(answer));
		}

		JButton logButton = new JButton(logString);
		logButton.setHorizontalAlignment(SwingConstants.RIGHT);
		logButton.setFont(buttonFont);
		logButton.setMinimumSize(new Dimension(350,60));
		logButton.setBackground(new Color(232,234,240));
		logButton.setFocusPainted(false); 
		logButton.setContentAreaFilled(false);
		logButton.setBorderPainted(false);
		logButton.addMouseListener(new MouseListener() {
			
			@Override
			public void mouseReleased(MouseEvent e) {
			}
			@Override
			public void mousePressed(MouseEvent e) {
			}	
			@Override
			public void mouseExited(MouseEvent e) {
				logButton.setContentAreaFilled(false);
			}
			@Override
			public void mouseEntered(MouseEvent e) {
				logButton.setContentAreaFilled(true);
			}	
			@Override
			public void mouseClicked(MouseEvent e) {
				String formula = DataProcessing.getDataProcessing().numberFormat(leftOperand.toString()) + operator + DataProcessing.getDataProcessing().numberFormat(RightOperand.toString());
				formula = DataProcessing.getDataProcessing().deleteComma(formula);
				formulaLabel.setText(formula + "=");
				answerLabel.setText(DataProcessing.getDataProcessing().numberFormat(answer));
				operandDTO.setLeftOperand(new BigDecimal(answer));
				operandDTO.setRightOperand(RightOperand);
				operatorDTO.setLast(operator);			
				answerDTO.set(answer);	
				//inputNumberDTO.set(logString);
			}
		});
		
		logPanel.logButtonPanel.add(logButton,1);
		if (logPanel.logButtonPanel.getComponentCount()>21) // 로그패널의 요소 개수가 21개가 넘어가면 (상단에 텍스트가 포함되어서 21)
			logPanel.logButtonPanel.remove(logPanel.logButtonPanel.getComponent(21)); // 가장 아래요소 제거
		
		logPanel.logButtonPanel.repaint();
		logPanel.logButtonPanel.revalidate();
		
	}
}
