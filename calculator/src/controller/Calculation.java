package controller;

import javax.script.ScriptEngineManager;
import javax.script.ScriptException;
import javax.script.ScriptEngine;

public class Calculation 
{
	public double calculate(String formulaString)
	{
		String[] operatorArray = {"÷", "x", "\\-", "\\+"};
		String[] splitArray;
		double calculateResult = 0.0;
		
		for (String operator : operatorArray) 
		{
			splitArray = formulaString.split(operator);
			if (splitArray.length == 2)
			{
				if (operator == "÷")
					calculateResult = Double.parseDouble(splitArray[0]) / Double.parseDouble(splitArray[1]);
				else if (operator == "x")
					calculateResult = Double.parseDouble(splitArray[0]) * Double.parseDouble(splitArray[1]);
				else if (operator == "\\-")
					calculateResult = Double.parseDouble(splitArray[0]) - Double.parseDouble(splitArray[1]);
				else if (operator == "\\+")
					calculateResult = Double.parseDouble(splitArray[0]) + Double.parseDouble(splitArray[1]);
			}
		}
		return calculateResult;	
	}
}
