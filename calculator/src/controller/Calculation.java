package controller;

import java.math.BigDecimal;
import java.math.MathContext;

import javax.swing.JPanel;

import Utility.DataProcessing;
import model.OperandDTO;
import model.OperatorDTO;

public class Calculation
{
	public String calculate(OperandDTO operandDTO, OperatorDTO operatorDTO) //계산하는 함수
	{
		BigDecimal leftOperand, rightOperand;
		BigDecimal calculateResult = new BigDecimal("0");
		String returnString;		
		
		operandDTO.setLeftOperand(new BigDecimal(DataProcessing.getDataProcessing().deleteComma(operandDTO.getLeftOperand().toString())));
		operandDTO.setRightOperand(new BigDecimal(DataProcessing.getDataProcessing().deleteComma(operandDTO.getRightOperand().toString()))); 
	
		leftOperand = operandDTO.getLeftOperand();
		rightOperand = operandDTO.getRightOperand();
		
		try
		{
			switch (operatorDTO.getLast())
			{
				case "÷":
					calculateResult = leftOperand.divide(rightOperand, MathContext.DECIMAL64);
					break;
				case "x":
					calculateResult = leftOperand.multiply(rightOperand, MathContext.DECIMAL64);
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
		}
		catch(ArithmeticException e)
		{
			return "0으로 나눌 수 없습니다"; 
		}
		
		//returnString = DataProcessing.getDataProcessing().deleteUnnecessaryDecimalPoint(calculateResult.toString()); //반환할때 불필요 소수점 제거

		/*
		BigDecimal compareNumber = new BigDecimal("1000000000000000");
		if (calculateResult.compareTo(compareNumber) > 0)
			return String.format("%e", calculateResult);	//calculateResult.stripTrailingZeros().toPlainString()
		*/
		return calculateResult.toString();	
		//return calculateResult.toString();
	}
} 
