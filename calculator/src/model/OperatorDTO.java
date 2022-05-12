package model;

public class OperatorDTO 
{
	private String operator;
	
	public OperatorDTO(String str)
	{
		this.operator = str;
	}
	
	public String get()
	{
		return operator;
	}
	
	public void set(String operator)
	{
		this.operator = operator;
	}
}
