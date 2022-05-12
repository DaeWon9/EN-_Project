package controller;

import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import javax.swing.JButton;
import javax.swing.JLabel;

import Utility.DataProcessing;
import model.AnswerDTO;
import model.OperatorDTO;
import model.InputNumberDTO;
import model.OperandDTO;

public class OperatorButtonListener implements ActionListener
{
	private AnswerDTO answerDTO;
	private InputNumberDTO inputNumberDTO;
	private JLabel answerLabel;
	private JLabel formulaLabel;
	
	private Calculation calculation = new Calculation();
	private OperatorDTO operatorDTO = new OperatorDTO("");
	private OperandDTO operandDTO = new OperandDTO();
	
	
	public OperatorButtonListener(JLabel answerLabel, JLabel formulaLabel, AnswerDTO answerDTO, InputNumberDTO inputNumberDTO)
	{
		this.answerDTO = answerDTO;
		this.inputNumberDTO = inputNumberDTO;
		this.answerLabel = answerLabel;
		this.formulaLabel = formulaLabel;

	}
	
	@Override
	public void actionPerformed(ActionEvent e)  // operator가 입력되면
	{
		String formulaString = "", calculationResult;
		
		checkLastCharIsPoint();
		if ( ((JButton)e.getSource()).getText() == "=" ) // operator중에서 "="이 입력되면 계산
		{
			setRigthOperand();
			formulaString = operandDTO.getLeftOperand() + operatorDTO.get() + operandDTO.getRightOperand();
			formulaLabel.setText(formulaString + "=");
			
			calculationResult = calculation.calculate(operandDTO, operatorDTO);	//계산
			calculationResult = DataProcessing.getDataProcessing().appendCommaInString(calculationResult); // 계산결과에 ,추가
			// 계산결과값으로 새로 set
			answerDTO.set(calculationResult);
			operandDTO.setLeftOperand(DataProcessing.getDataProcessing().deleteComma(answerDTO.get()));
			answerLabel.setText(answerDTO.get());
			inputNumberDTO.setLast(operandDTO.getRightOperand());
			inputNumberDTO.set("");
		}
		
		else // 수식 셋팅
		{
			setLeftOperand();
			// 추가된 값으로 새로 set
			operatorDTO.set(((JButton)e.getSource()).getText());
			formulaString = operandDTO.getLeftOperand() + operatorDTO.get();
			formulaLabel.setText(formulaString);
			inputNumberDTO.setLast(inputNumberDTO.get());
			inputNumberDTO.set("");
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
		if (inputNumberDTO.get() == "")
			operandDTO.setRightOperand(DataProcessing.getDataProcessing().deleteComma(inputNumberDTO.getLast()));
		else
			operandDTO.setRightOperand(DataProcessing.getDataProcessing().deleteComma(inputNumberDTO.get()));
	}
	
	private void setLeftOperand()
	{
		if (inputNumberDTO.get() == "")
			operandDTO.setLeftOperand(DataProcessing.getDataProcessing().deleteComma(answerDTO.get()));
		else
			operandDTO.setLeftOperand(DataProcessing.getDataProcessing().deleteComma(inputNumberDTO.get()));
	}
}
