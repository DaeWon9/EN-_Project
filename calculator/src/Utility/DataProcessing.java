package Utility;

import java.text.NumberFormat;

public class DataProcessing 
{
	private static DataProcessing dataProcessing = new DataProcessing();
	
	public static DataProcessing getDataProcessing()
	{
		return dataProcessing;
	}
	
	public int countChar(String targetString, char targetChar)
	{
		int count = 0;
		for (int index = 0; index < targetString.length(); index++)
		{
			if (targetString.charAt(index) == targetChar)
				count++;
		}
		return count;
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
	
	public static String deleteUnnecessaryDecimalPoint(double doubleValue)
	{
	    if(doubleValue == (long) doubleValue)
	        return String.format("%d",(long)doubleValue);
	    else
	        return String.format("%s",doubleValue);
	}
}
