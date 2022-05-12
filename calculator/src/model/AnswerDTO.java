package model;

public class AnswerDTO 
{
	private String answer;
	
	public AnswerDTO(String answer)
	{
		this.answer = answer;
	}
	
	public String get()
	{
		return answer;
	}
	
	public void set(String answer)
	{
		this.answer = answer;
	}
}
