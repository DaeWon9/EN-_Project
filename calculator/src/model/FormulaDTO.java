package model;

public class FormulaDTO 
{
	private String formula;
	
	public FormulaDTO(String formula)
	{
		this.formula = formula;
	}
	
	public String get()
	{
		return formula;
	}
	
	public void set(String formula)
	{
		this.formula = formula;
	}
}
