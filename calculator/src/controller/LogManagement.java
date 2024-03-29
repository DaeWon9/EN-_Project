package controller;

import java.awt.Color;
import java.awt.Dimension;
import java.awt.Font;
import java.awt.event.MouseEvent;
import java.awt.event.MouseListener;
import javax.swing.JButton;
import javax.swing.SwingConstants;
import Utility.Constant;
import Utility.DataProcessing;
import model.AnswerDTO;
import model.FormulaDTO;
import model.OperandDTO;
import model.OperatorDTO;
import view.LogPanel;
import view.MainFrame;

public class LogManagement
{
	private Font buttonFont = new Font("맑은 고딕", 0, 20);
	private LogPanel logPanel;
	private OperandDTO operandDTO;
	private OperatorDTO operatorDTO;
	private AnswerDTO answerDTO;
	private FormulaDTO formulaDTO;
	
	public LogManagement(LogPanel logPanel, OperandDTO operandDTO, OperatorDTO operatorDTO, AnswerDTO answerDTO, FormulaDTO formulaDTO)
	{
		this.logPanel = logPanel;
		this.operandDTO = operandDTO;
		this.operatorDTO = operatorDTO;
		this.answerDTO = answerDTO;
		this.formulaDTO = formulaDTO;
	}
	
	public void addLog(MainFrame mainFrame)
	{ 
		logPanel.topLabel.setText(" ");
		logPanel.deleteButton.setVisible(true);
		String logString;
		String formula = formulaDTO.get();
		String answer = answerDTO.get();
		String operator = operatorDTO.getLast();
		String rightOperand = operandDTO.getRightOperand();
	
		if (!answerDTO.get().matches(Constant.EXCEPTION_TYPE_KOREAN))
		{
			logString = String.format(Constant.LOG_STRING_FORM, formula, answerDTO.get());
			JButton logButton = new JButton(logString);
			logButton.setHorizontalAlignment(SwingConstants.RIGHT);
			logButton.setFont(buttonFont);
			logButton.setPreferredSize(new Dimension(250,(formula.length()/30)*20 + 50)); 				
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
					mainFrame.textPanel.formula.setText(formula);
					mainFrame.textPanel.answer.setText(answer);
					operandDTO.setLeftOperand(answer);
					operandDTO.setRightOperand(rightOperand);
					operatorDTO.setLast(operator);			
					answerDTO.set(answer);	
					DataProcessing.getDataProcessing().resizeLabel(mainFrame);
					DataProcessing.getDataProcessing().setArrowButtonVisible(mainFrame);
				}
			});
			
			logPanel.logButtonPanel.add(logButton,1);
			if (logPanel.logButtonPanel.getComponentCount()>21) // 로그패널의 요소 개수가 21개가 넘어가면 (상단에 텍스트가 포함되어서 21)
				logPanel.logButtonPanel.remove(logPanel.logButtonPanel.getComponent(21)); // 가장 아래요소 제거
			
			logPanel.logButtonPanel.repaint();
			logPanel.logButtonPanel.revalidate();
		}
	}
}
