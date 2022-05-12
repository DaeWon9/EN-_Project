package model;

public class InputNumberDTO 
{
	private String inputNubmer;
	
	public InputNumberDTO(String str)
	{
		this.inputNubmer = str;
	}
	
	public String get()
	{
		return inputNubmer;
	}
	
	public void set(String inputNubmer)
	{
		this.inputNubmer = inputNubmer;
	}
}
