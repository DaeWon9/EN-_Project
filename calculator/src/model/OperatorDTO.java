package model;

public class OperatorDTO 
{
	private String operator;
	private String lastOperator;
	
	public OperatorDTO(String operator, String lastOperator)
	{
		this.operator = operator;
		this.lastOperator = lastOperator;
	}
	
	
	public String get()
	{
		return operator;
	}
	
	public String getLast()
	{
		return lastOperator;
	}
	
	public void set(String operator)
	{
		this.operator = operator;
	}
	
	public void setLast(String operator)
	{
		this.lastOperator = operator;
	}
}
