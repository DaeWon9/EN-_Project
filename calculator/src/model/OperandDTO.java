package model;

import java.math.BigDecimal;

public class OperandDTO 
{
	private String leftOperand;
	private String rigthOperand;
	
	public OperandDTO(String leftOperand, String rigthOperand)
	{
		this.leftOperand = leftOperand;
		this.rigthOperand = rigthOperand;
	}
	
	public String getLeftOperand()
	{
		return leftOperand;
	}
	
	public String getRightOperand()
	{
		return rigthOperand;
	}
	
	public void setLeftOperand(String operand)
	{
		this.leftOperand = operand;
	}
	
	public void setRightOperand(String operand)
	{
		this.rigthOperand = operand;
	}
}
