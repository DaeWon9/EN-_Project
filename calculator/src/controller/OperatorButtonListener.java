package controller;

import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;

import javax.script.ScriptException;
import javax.swing.JButton;
import javax.swing.JLabel;

import Utility.Constant;
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
		String formulaString = ""; 
		double calculationResult;
		
		if ( ((JButton)e.getSource()).getText() == "=" ) //계산하기
		{
			if (inputNumberDTO.get() == "")
				operandDTO.setRightOperand(inputNumberDTO.getLast());
			else
				operandDTO.setRightOperand(inputNumberDTO.get());
			
			formulaString = operandDTO.getLeftOperand() + operatorDTO.get() + operandDTO.getRightOperand();
			formulaLabel.setText(formulaString + "=");
			calculationResult = calculation.calculate(formulaString);	
			answerDTO.set(Double.toString(calculationResult));
			operandDTO.setLeftOperand(answerDTO.get());
			answerLabel.setText(answerDTO.get());
			inputNumberDTO.setLast(operandDTO.getRightOperand());
			inputNumberDTO.set("");
		}
		else // 수식 셋팅
		{
			if (inputNumberDTO.get() == "")
				operandDTO.setLeftOperand(DataProcessing.getDataProcessing().deleteComma(answerDTO.get()));
			else
				operandDTO.setLeftOperand(inputNumberDTO.get());
			
			operatorDTO.set(((JButton)e.getSource()).getText());
			formulaString = operandDTO.getLeftOperand() + operatorDTO.get();
			formulaLabel.setText(formulaString);
			inputNumberDTO.setLast(inputNumberDTO.get());
			inputNumberDTO.set("");
		}

	}

}
