package Utility;

import java.text.NumberFormat;

public class DataProcessing 
{
	private static DataProcessing dataProcessing = new DataProcessing();
	
	public static DataProcessing getDataProcessing()
	{
		return dataProcessing;
	}
	
	public String appendComma(long number) 
	{
		String formatResult= "";
		NumberFormat numberFormat = NumberFormat.getNumberInstance();
		formatResult = numberFormat.format(number);
		return formatResult;
	}
	
	public String deleteComma(String str)
	{
		return str.replace(",", "");
	}
}
