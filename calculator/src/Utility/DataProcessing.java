package Utility;

import java.text.NumberFormat;

public class DataProcessing 
{
	public String formatNumber(int number) 
	{
		NumberFormat numberFormat = NumberFormat.getNumberInstance();
		return numberFormat.format(number);
	}
}
