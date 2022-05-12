package Utility;

import java.text.NumberFormat;

public class DataProcessing 
{
	public String formatNumber(long number) 
	{
		String formatResult= "";
		NumberFormat numberFormat = NumberFormat.getNumberInstance();
		formatResult = numberFormat.format(number);
		return formatResult;
	}
}
