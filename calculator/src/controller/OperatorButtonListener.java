package controller;

import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import javax.swing.JButton;
import javax.swing.JFrame;
import javax.swing.JLabel;

import Utility.DataProcessing;
import model.AnswerDTO;
import model.OperatorDTO;
import model.InputNumberDTO;
import model.OperandDTO;

public class OperatorButtonListener implements ActionListener
{
	private JFrame mainFrame;
	private JLabel answerLabel;
	private JLabel formulaLabel;
	private AnswerDTO answerDTO;
	private InputNumberDTO inputNumberDTO;
	private OperatorDTO operatorDTO;
	private OperandDTO operandDTO;
	
	private Calculation calculation = new Calculation();
	
	public OperatorButtonListener(JFrame mainFrame, JLabel answerLabel, JLabel formulaLabel, AnswerDTO answerDTO, InputNumberDTO inputNumberDTO, OperatorDTO operatorDTO, OperandDTO operandDTO)
	{
		this.mainFrame = mainFrame;
		this.answerLabel = answerLabel;
		this.formulaLabel = formulaLabel;
		this.answerDTO = answerDTO;
		this.inputNumberDTO = inputNumberDTO;
		this.operatorDTO = operatorDTO;
		this.operandDTO = operandDTO;
	}
	
	@Override
	public void actionPerformed(ActionEvent e)  // operator가 입력되면
	{
		String formulaString = "", calculationResult;
		
		checkLastCharIsPoint();
		if ( ((JButton)e.getSource()).getText() == "=" ) // operator중에서 "="이 입력되면 계산
		{
			setRigthOperand(); // 경우의 수 더 생각하기
			formulaString = operandDTO.getLeftOperand() + operatorDTO.get() + operandDTO.getRightOperand();
			formulaLabel.setText(formulaString + "=");
			
			calculationResult = calculation.calculate(operandDTO, operatorDTO);	//계산
			if (!calculationResult.equals("Infinity"))
				calculationResult = DataProcessing.getDataProcessing().appendCommaInString(calculationResult); // 계산결과에 ,추가
			// 계산결과값으로 새로 set
			answerDTO.set(calculationResult);
			operandDTO.setLeftOperand(DataProcessing.getDataProcessing().deleteComma(answerDTO.get()));
			answerLabel.setText(answerDTO.get());
			inputNumberDTO.setLast(operandDTO.getRightOperand());
			inputNumberDTO.set("0");
			operatorDTO.setLast(operatorDTO.get());
			DataProcessing.getDataProcessing().resizeLabel(mainFrame, answerLabel);
		}
		
		else // 수식 셋팅
		{
			setLeftOperand(); // 경우의 수 더 생각하기
			// 추가된 값으로 새로 set
			operatorDTO.set(((JButton)e.getSource()).getText());
			formulaString = operandDTO.getLeftOperand() + operatorDTO.get();
			formulaLabel.setText(formulaString);
			inputNumberDTO.setLast(inputNumberDTO.get());
			inputNumberDTO.set("0");
			operatorDTO.setLast(operatorDTO.get());
		}

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
				answerLabel.setText(inputNumberDTO.get()); // 라벨새로지정
			}
		}
	}
	
	private void setRigthOperand()
	{
		if (inputNumberDTO.get().equals("0"))
			operandDTO.setRightOperand(DataProcessing.getDataProcessing().deleteComma(inputNumberDTO.getLast()));
		else
			operandDTO.setRightOperand(DataProcessing.getDataProcessing().deleteComma(inputNumberDTO.get()));
	}
	
	private void setLeftOperand()
	{
		if (inputNumberDTO.get().equals("0"))
			operandDTO.setLeftOperand(DataProcessing.getDataProcessing().deleteComma(answerDTO.get()));
		else
			operandDTO.setLeftOperand(DataProcessing.getDataProcessing().deleteComma(inputNumberDTO.get()));
	}
}
