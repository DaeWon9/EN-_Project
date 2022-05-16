package controller;

import java.math.BigDecimal;

import javax.swing.JPanel;

import Utility.DataProcessing;
import model.OperandDTO;
import model.OperatorDTO;

public class Calculation
{
	public String calculate(OperandDTO operandDTO, OperatorDTO operatorDTO) //계산하는 함수
	{
		BigDecimal leftOperand, rightOperand;
		BigDecimal calculateResult = null;
		String returnString;		
		
		operandDTO.setLeftOperand(new BigDecimal(DataProcessing.getDataProcessing().deleteComma(operandDTO.getLeftOperand().toString())));
		operandDTO.setRightOperand(new BigDecimal(DataProcessing.getDataProcessing().deleteComma(operandDTO.getRightOperand().toString()))); 
	
		leftOperand = operandDTO.getLeftOperand();
		rightOperand = operandDTO.getRightOperand();
		
		switch (operatorDTO.getLast())
		{
			case "÷":
				calculateResult = leftOperand.divide(rightOperand);
				break;
			case "x":
				calculateResult = leftOperand.multiply(rightOperand);
				break;
			case "-":
				calculateResult = leftOperand.subtract(rightOperand);
				break;
			case "+":
				calculateResult = leftOperand.add(rightOperand);
				break;
			default:
				break;			
		}
		
		//returnString = DataProcessing.getDataProcessing().deleteUnnecessaryDecimalPoint(calculateResult); //반환할때 불필요 소수점 제거
		if (calculateResult.toString().length() > 16)
			return String.format("%e", calculateResult);	
		return String.format("%s", calculateResult);	
	}
} 
