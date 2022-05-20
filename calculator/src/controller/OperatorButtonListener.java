package controller;

import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.math.BigDecimal;

import javax.swing.JButton;
import javax.swing.JFrame;
import javax.swing.JLabel;
import javax.swing.JPanel;

import Utility.Constant;
import Utility.DataProcessing;
import model.AnswerDTO;
import model.FormulaDTO;
import model.OperatorDTO;
import view.ButtonPanel;
import view.LogPanel;
import view.MainFrame;
import model.InputNumberDTO;
import model.OperandDTO;

public class OperatorButtonListener implements ActionListener
{
	private MainFrame mainFrame;
	private LogPanel logPanel;
	private AnswerDTO answerDTO;
	private FormulaDTO formulaDTO;
	private InputNumberDTO inputNumberDTO;
	private OperatorDTO operatorDTO;
	private OperandDTO operandDTO;
	private Calculation calculation;
	private LogManagement logManagement;
	
	public OperatorButtonListener(MainFrame mainFrame, LogPanel logpanel, AnswerDTO answerDTO, InputNumberDTO inputNumberDTO, OperatorDTO operatorDTO, OperandDTO operandDTO, FormulaDTO formulaDTO)
	{
		this.mainFrame = mainFrame;
		this.logPanel = logpanel;
		this.answerDTO = answerDTO;
		this.formulaDTO = formulaDTO;
		this.inputNumberDTO = inputNumberDTO;
		this.operatorDTO = operatorDTO;
		this.operandDTO = operandDTO;
		this.calculation = new Calculation();
		this.logManagement = new LogManagement(logPanel, operandDTO, operatorDTO, answerDTO, inputNumberDTO, formulaDTO);
	}
	 
	@Override
	public void actionPerformed(ActionEvent e)  // operator가 입력되면
	{	
		if (answerDTO.get().matches(Constant.EXCEPTION_TYPE_KOREAN))
			return;
		
		String formulaString = "", calculationResult;		
		checkLastCharIsPoint(); // 숫자입력값 마지막이 . 이면 없애주기 
			
		operatorDTO.set(((JButton)e.getSource()).getText());
		if (!inputNumberDTO.get().equals(""))
		{
			inputNumberDTO.set(DataProcessing.getDataProcessing().deleteComma(inputNumberDTO.get()));
			mainFrame.textPanel.answer.setText(DataProcessing.getDataProcessing().numberFormat(inputNumberDTO.get()));
		}	
		
		if (inputNumberDTO.get().equals("")) // 숫자가 입력되지 않고 오퍼레이터가 입력된경우
		{
			if (operatorDTO.get().equals("=") && operatorDTO.getLast().equals("")) // 라스트 오퍼레이터 없이 =만 입력된경우
			{
				formulaString = DataProcessing.getDataProcessing().numberFormat(answerDTO.get()) + operatorDTO.get();
				formulaDTO.set(DataProcessing.getDataProcessing().deleteComma(formulaString));
				mainFrame.textPanel.formula.setText(formulaDTO.get());
			}
			
			else if (operatorDTO.get().equals("=") && !operatorDTO.getLast().equals("")) // 라스트 오퍼레이터가 있고 = 가 입력된경우
			{
				if (!answerDTO.get().matches(Constant.EXCEPTION_TYPE_KOREAN))
					operandDTO.setLeftOperand(new BigDecimal(DataProcessing.getDataProcessing().deleteComma(answerDTO.get())));
				if (operandDTO.getRightOperand() == null)
					operandDTO.setRightOperand(new BigDecimal(DataProcessing.getDataProcessing().deleteComma(answerDTO.get())));
				calculationResult = calculation.calculate(operandDTO, operatorDTO);	//계산			
				answerDTO.set(DataProcessing.getDataProcessing().numberFormat(calculationResult));
				formulaString = DataProcessing.getDataProcessing().numberFormat(operandDTO.getLeftOperand().toString()) + operatorDTO.getLast() + DataProcessing.getDataProcessing().numberFormat(operandDTO.getRightOperand().toString()) + operatorDTO.get();
				formulaDTO.set(DataProcessing.getDataProcessing().deleteComma(formulaString));
				if (!answerDTO.get().matches(Constant.EXCEPTION_TYPE_KOREAN))
					logManagement.addLog(mainFrame);
				mainFrame.textPanel.formula.setText(formulaDTO.get());
				mainFrame.textPanel.answer.setText(answerDTO.get());
			}
			else // 다른오퍼레이터
			{ 
				formulaString = DataProcessing.getDataProcessing().numberFormat(DataProcessing.getDataProcessing().deleteComma(answerDTO.get())) + operatorDTO.get();
				formulaDTO.set(DataProcessing.getDataProcessing().deleteComma(formulaString));
				mainFrame.textPanel.formula.setText(formulaDTO.get());
				operatorDTO.setLast(operatorDTO.get());
				if (!answerDTO.get().matches(Constant.EXCEPTION_TYPE_KOREAN))
					operandDTO.setRightOperand(new BigDecimal(DataProcessing.getDataProcessing().deleteComma(answerDTO.get())));
			}
		}
		else // 숫자가 입력되고 오퍼레이터가 입력된 경우
		{
			if (operatorDTO.get().equals("=") && operatorDTO.getLast().equals("")) // 라스트오퍼레이터가 없는상태에서 = 입력받음
			{
				answerDTO.set(inputNumberDTO.get()); 
				inputNumberDTO.setLast(inputNumberDTO.get());
				operandDTO.setLeftOperand(new BigDecimal(inputNumberDTO.get()));
				formulaString = DataProcessing.getDataProcessing().numberFormat(operandDTO.getLeftOperand().toString()) + operatorDTO.get();
				formulaDTO.set(DataProcessing.getDataProcessing().deleteComma(formulaString));
				mainFrame.textPanel.formula.setText(formulaDTO.get());	
				mainFrame.textPanel.answer.setText(DataProcessing.getDataProcessing().numberFormat(answerDTO.get()));
			}
			
			else if (operatorDTO.get().equals("=") && !operatorDTO.getLast().equals("")) // 오퍼레이터가 = 리고 라스트 오퍼레이터가있음 -> 평범한 계산
			{
				if (mainFrame.textPanel.formula.getText().contains("="))
					operandDTO.setLeftOperand(new BigDecimal(inputNumberDTO.get()));
				else
					operandDTO.setRightOperand(new BigDecimal(inputNumberDTO.get()));
				calculationResult = calculation.calculate(operandDTO, operatorDTO);	//계산
				answerDTO.set(DataProcessing.getDataProcessing().numberFormat(calculationResult));
				inputNumberDTO.setLast("");
				formulaString = DataProcessing.getDataProcessing().numberFormat(operandDTO.getLeftOperand().toString()) + operatorDTO.getLast() + DataProcessing.getDataProcessing().numberFormat(operandDTO.getRightOperand().toString()) + operatorDTO.get();
				formulaDTO.set(DataProcessing.getDataProcessing().deleteComma(formulaString));
				if (!answerDTO.get().matches(Constant.EXCEPTION_TYPE_KOREAN))
					logManagement.addLog(mainFrame);
				mainFrame.textPanel.formula.setText(formulaDTO.get());
				mainFrame.textPanel.answer.setText(answerDTO.get()); 
				if (!answerDTO.get().matches(Constant.EXCEPTION_TYPE_KOREAN))
					operandDTO.setLeftOperand(new BigDecimal(DataProcessing.getDataProcessing().deleteComma(answerDTO.get())));
			}
		
			else if (!operatorDTO.get().equals("=") && inputNumberDTO.getLast().equals("")) // 오퍼레이터가 =가 아니고, 라이스인풋값이 없으면 -> 즉 처음입력
			{
				operandDTO.setLeftOperand(new BigDecimal(inputNumberDTO.get()));
				operandDTO.setLeftOperand(new BigDecimal(inputNumberDTO.get()));
				answerDTO.set(operandDTO.getLeftOperand().toString()); 
				inputNumberDTO.setLast(inputNumberDTO.get());
				operatorDTO.setLast(operatorDTO.get());
				formulaString = DataProcessing.getDataProcessing().numberFormat(operandDTO.getLeftOperand().toString()) + operatorDTO.get();
				formulaDTO.set(DataProcessing.getDataProcessing().deleteComma(formulaString));
				mainFrame.textPanel.formula.setText(formulaDTO.get());
			}
			
			else if (!operatorDTO.get().equals("=") && !inputNumberDTO.getLast().equals("")) // 오퍼레이터가 =가 아니고, 라스트 인풋값이 존재하면 -> 즉 두번째로 입력 -> 계산 
			{
				operandDTO.setRightOperand(new BigDecimal(inputNumberDTO.get()));
				
				calculationResult = calculation.calculate(operandDTO, operatorDTO);	//계산
				answerDTO.set(DataProcessing.getDataProcessing().numberFormat(calculationResult));
				inputNumberDTO.setLast("");
				if (!answerDTO.get().matches(Constant.EXCEPTION_TYPE_KOREAN)) 
				{
					logManagement.addLog(mainFrame);
					operandDTO.setLeftOperand(new BigDecimal(DataProcessing.getDataProcessing().deleteComma(answerDTO.get())));

					operandDTO.setRightOperand(null);
					operatorDTO.setLast(operatorDTO.get());
					inputNumberDTO.setLast(inputNumberDTO.get());
					formulaString = DataProcessing.getDataProcessing().numberFormat(answerDTO.get()) + operatorDTO.get();
					formulaDTO.set(DataProcessing.getDataProcessing().deleteComma(formulaString));
					mainFrame.textPanel.formula.setText(formulaDTO.get());
				}	
				mainFrame.textPanel.answer.setText(answerDTO.get());

			}
			
			else if (operatorDTO.get().equals("=") && !operatorDTO.getLast().equals("") && inputNumberDTO.getLast().equals("")) // 오퍼레이터가 = 이고 라스트오퍼레이터가 있으며, 라스트인풋이 없을경우
			{
				operandDTO.setLeftOperand(new BigDecimal(inputNumberDTO.get()));
				calculationResult = calculation.calculate(operandDTO, operatorDTO);	//계산
				answerDTO.set(DataProcessing.getDataProcessing().numberFormat(calculationResult));
				inputNumberDTO.setLast("");
				formulaString = DataProcessing.getDataProcessing().numberFormat(operandDTO.getLeftOperand().toString()) + operatorDTO.getLast() + DataProcessing.getDataProcessing().numberFormat(operandDTO.getRightOperand().toString()) + operatorDTO.get();
				formulaDTO.set(DataProcessing.getDataProcessing().deleteComma(formulaString));
				if (!answerDTO.get().matches(Constant.EXCEPTION_TYPE_KOREAN))
					logManagement.addLog(mainFrame);
				mainFrame.textPanel.formula.setText(formulaDTO.get());
				mainFrame.textPanel.answer.setText(answerDTO.get());
			}
		}	
		inputNumberDTO.set("");
		DataProcessing.getDataProcessing().resizeLabel(mainFrame);
		DataProcessing.getDataProcessing().setArrowButtonVisible(mainFrame);
		mainFrame.requestFocus();
	}

	private void checkLastCharIsPoint() // 마지막 문자가 .이면 마지막문자 제거해주기
	{
		char lastChar;
		if (inputNumberDTO.get().length() > 0)
		{
			lastChar = inputNumberDTO.get().charAt(inputNumberDTO.get().length() - 1);
			if (lastChar == '.') // 마지막이 .으로 끝나면
			{
				inputNumberDTO.set(inputNumberDTO.get().substring(0, inputNumberDTO.get().length()-1)); // 마지막 문자 지워주기
				mainFrame.textPanel.answer.setText(inputNumberDTO.get()); // 라벨새로지정
			}
		}
	}
}
