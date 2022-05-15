package controller;

import javax.swing.JPanel;

import Utility.DataProcessing;
import model.OperandDTO;
import model.OperatorDTO;

public class Calculation
{
	public String calculate(OperandDTO operandDTO, OperatorDTO operatorDTO) //계산하는 함수
	{
		double calculateResult = 0.0;
		String returnString;		
		
		operandDTO.setLeftOperand(DataProcessing.getDataProcessing().deleteComma(operandDTO.getLeftOperand()));
		operandDTO.setRightOperand(DataProcessing.getDataProcessing().deleteComma(operandDTO.getRightOperand())); 
	
		if (operatorDTO.getLast() == "÷")
			calculateResult = Double.parseDouble(operandDTO.getLeftOperand()) / Double.parseDouble(operandDTO.getRightOperand());
		else if (operatorDTO.getLast() == "x")
			calculateResult = Double.parseDouble(operandDTO.getLeftOperand()) * Double.parseDouble(operandDTO.getRightOperand());
		else if (operatorDTO.getLast() == "-")
			calculateResult = Double.parseDouble(operandDTO.getLeftOperand()) - Double.parseDouble(operandDTO.getRightOperand());
		else if (operatorDTO.getLast() == "+")
			calculateResult = Double.parseDouble(operandDTO.getLeftOperand()) + Double.parseDouble(operandDTO.getRightOperand());

		returnString = DataProcessing.getDataProcessing().deleteUnnecessaryDecimalPoint(calculateResult); //반환할때 불필요 소수점 제거
		return returnString;	
	}
} 
