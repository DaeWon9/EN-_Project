package controller;

import Utility.DataProcessing;
import model.OperandDTO;
import model.OperatorDTO;
public class Calculation 
{
	public String calculate(OperandDTO operandDTO, OperatorDTO operatorDTO) //계산하는 함수
	{
		double calculateResult = 0.0;
		String returnString;		
	
		if (operatorDTO.get() == "÷")
			calculateResult = Double.parseDouble(operandDTO.getLeftOperand()) / Double.parseDouble(operandDTO.getRightOperand());
		else if (operatorDTO.get() == "x")
			calculateResult = Double.parseDouble(operandDTO.getLeftOperand()) * Double.parseDouble(operandDTO.getRightOperand());
		else if (operatorDTO.get() == "-")
			calculateResult = Double.parseDouble(operandDTO.getLeftOperand()) - Double.parseDouble(operandDTO.getRightOperand());
		else if (operatorDTO.get() == "+")
			calculateResult = Double.parseDouble(operandDTO.getLeftOperand()) + Double.parseDouble(operandDTO.getRightOperand());


		returnString = DataProcessing.getDataProcessing().deleteUnnecessaryDecimalPoint(calculateResult); //반환할때 불필요 소수점 제거
		return returnString;	
	}
}
