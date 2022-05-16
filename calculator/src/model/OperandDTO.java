package model;

import java.math.BigDecimal;

public class OperandDTO 
{
	private BigDecimal leftOperand;
	private BigDecimal rigthOperand;
	
	public BigDecimal getLeftOperand()
	{
		return leftOperand;
	}
	
	public BigDecimal getRightOperand()
	{
		return rigthOperand;
	}
	
	public void setLeftOperand(BigDecimal operand)
	{
		this.leftOperand = operand;
	}
	
	public void setRightOperand(BigDecimal operand)
	{
		this.rigthOperand = operand;
	}
}
