package model;

public class OperandDTO 
{
	private String leftOperand;
	private String rigthOperand;
	
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
