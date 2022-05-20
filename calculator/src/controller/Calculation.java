package controller;

import java.math.BigDecimal;
import java.math.MathContext;
import java.math.RoundingMode;

import javax.swing.JPanel;

import Utility.DataProcessing;
import model.OperandDTO;
import model.OperatorDTO;

public class Calculation
{
	@SuppressWarnings("deprecation")
	public String calculate(OperandDTO operandDTO, OperatorDTO operatorDTO) //계산하는 함수
	{
		BigDecimal leftOperand, rightOperand;
		BigDecimal calculateResult = new BigDecimal("0");
		if (operandDTO.getLeftOperand() == null)
			operandDTO.setLeftOperand(new BigDecimal("0"));
		
		operandDTO.setLeftOperand(new BigDecimal(DataProcessing.getDataProcessing().deleteComma(operandDTO.getLeftOperand().toString())));
		operandDTO.setRightOperand(new BigDecimal(DataProcessing.getDataProcessing().deleteComma(operandDTO.getRightOperand().toString()))); 
	
		leftOperand = operandDTO.getLeftOperand();
		rightOperand = operandDTO.getRightOperand();
		
		try
		{
			switch (operatorDTO.getLast())
			{
				case "÷":
					calculateResult = leftOperand.divide(rightOperand, MathContext.DECIMAL128); 
					break; 
				case "x":
					calculateResult = leftOperand.multiply(rightOperand, MathContext.DECIMAL128);//.setScale(15, RoundingMode.HALF_EVEN);
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
			if(e.getMessage().equals("Division by zero"))
				return "0으로 나눌 수 없습니다"; 
			else
				return "정의되지 않은 결과입니다.";
		}

		return calculateResult.toString();	
	}
} 
