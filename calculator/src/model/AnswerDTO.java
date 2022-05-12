package model;

public class AnswerDTO 
{
	private String answer;
	
	public AnswerDTO(String str)
	{
		this.answer = str;
	}
	
	public String Get()
	{
		return answer;
	}
	
	public void Set(String answer)
	{
		this.answer = answer;
	}
}
