package model;

public class InputNumberDTO 
{
	private String inputNubmer;
	private String lastInputNumber;
		
	public InputNumberDTO(String inputNumber, String lasInputNumber)
	{
		this.inputNubmer = inputNumber;
		this.lastInputNumber = lasInputNumber;
	}
	
	public String get()
	{
		return inputNubmer;
	}
	
	public String getLast()
	{
		return lastInputNumber;
	}
	
	public void set(String inputNubmer)
	{
		this.inputNubmer = inputNubmer;
	} 
	
	public void setLast(String inputNubmer)
	{
		this.lastInputNumber = inputNubmer;
	}
}
