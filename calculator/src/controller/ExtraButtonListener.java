package controller;

import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.math.BigDecimal;

import javax.swing.JButton;
import javax.swing.JFrame;
import javax.swing.JLabel;

import Utility.DataProcessing;
import model.AnswerDTO;
import model.InputNumberDTO;
import model.OperandDTO;
import model.OperatorDTO;
import view.MainFrame;

public class ExtraButtonListener implements ActionListener
{
	private MainFrame mainFrame;
	private AnswerDTO answerDTO;
	private InputNumberDTO inputNumberDTO;
	private OperatorDTO operatorDTO;
	private OperandDTO operandDTO;

	public ExtraButtonListener(MainFrame mainFrame, AnswerDTO answerDTO, InputNumberDTO inputNumberDTO, OperatorDTO operatorDTO, OperandDTO operandDTO)
	{
		this.mainFrame = mainFrame;
		this.answerDTO = answerDTO;
		this.inputNumberDTO = inputNumberDTO;
		this.operatorDTO = operatorDTO;
		this.operandDTO = operandDTO;
	}
	@Override
	public void actionPerformed(ActionEvent e) 
	{
		switch (((JButton)e.getSource()).getText())
		{
		case "C":
			actionCButton();
			break;
		case "CE":
			actionCEButton();
			break;
		case "⌫":
			actionDeleteButton();
			break;
		case "+/-":
			actionNegateButton();
			break;
		default:
			break;
		}
		DataProcessing.getDataProcessing().resizeLabel(mainFrame);
		DataProcessing.getDataProcessing().setArrowButtonVisible(mainFrame);
		mainFrame.requestFocus();
	}
	
	private void actionCButton()
	{
		answerDTO.set("0");
		inputNumberDTO.set("");
		inputNumberDTO.setLast("");
		operatorDTO.set("");
		operatorDTO.setLast("");
		operandDTO.setLeftOperand(null);
		operandDTO.setRightOperand(null);
		mainFrame.textPanel.answer.setText("0");
		mainFrame.textPanel.formula.setText(" ");
	}
	
	private void actionCEButton()
	{
		if (mainFrame.textPanel.formula.getText().contains("=")) // 좌측 오퍼랜드 초기화, 상단에 수식 초기화, 인풋넘버 초기화
		{
			operandDTO.setLeftOperand(null);
			mainFrame.textPanel.formula.setText(" ");
		}
		answerDTO.set("0");
		inputNumberDTO.set("");
		mainFrame.textPanel.answer.setText("0");
	}
	
	private void actionDeleteButton()
	{
		inputNumberDTO.set(DataProcessing.getDataProcessing().deleteComma(inputNumberDTO.get()));
		if (inputNumberDTO.get().length() > 0 && !answerDTO.get().equals(""))
		{
			inputNumberDTO.set(inputNumberDTO.get().substring(0, inputNumberDTO.get().length()-1));
			if(inputNumberDTO.get().equals("") || inputNumberDTO.get().equals("-")) // 공백까지 지워지면 0으로 셋팅
				mainFrame.textPanel.answer.setText("0");
			else
				mainFrame.textPanel.answer.setText(DataProcessing.getDataProcessing().inputNumberFormat(inputNumberDTO.get()));
			inputNumberDTO.set(mainFrame.textPanel.answer.getText());
		}
		else
		{
			mainFrame.textPanel.formula.setText(" ");
			inputNumberDTO.set("");
		}
	}
	
	private void actionNegateButton()
	{
		switch (inputNumberDTO.get())
		{
		case "0":
			mainFrame.textPanel.answer.setText("0");
		case "": // 기능완성하고 함수로 빼기
			if (answerDTO.get().equals("0"))
				mainFrame.textPanel.answer.setText("0");	
			else if (answerDTO.get().charAt(0) == '-') // 첫글자가 -면 삭제
				answerDTO.set(answerDTO.get().substring(1));
			else // -가 아니면 -붙여주기
				answerDTO.set("-" + answerDTO.get());
			mainFrame.textPanel.answer.setText(DataProcessing.getDataProcessing().numberFormat(DataProcessing.getDataProcessing().deleteComma(answerDTO.get())));	
			if (mainFrame.textPanel.formula.getText().contains("negate"))
				mainFrame.textPanel.formula.setText("negate(" + mainFrame.textPanel.formula.getText().replace("-","") + ")");
			else
				mainFrame.textPanel.formula.setText("negate(" + answerDTO.get().replace("-","") + ")");
			break;
		default: // 기능완성하고 함수로 빼기 
			if (inputNumberDTO.get().charAt(0) == '-') // 첫글자가 -면 삭제
				inputNumberDTO.set(inputNumberDTO.get().substring(1));
			else // -가 아니면 -붙여주기
				inputNumberDTO.set("-" + inputNumberDTO.get());
			mainFrame.textPanel.answer.setText(DataProcessing.getDataProcessing().numberFormat(DataProcessing.getDataProcessing().deleteComma(inputNumberDTO.get())));				
			DataProcessing.getDataProcessing().resizeLabel(mainFrame);
			break;
			
		}
	}

}
