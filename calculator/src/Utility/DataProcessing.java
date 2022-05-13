package Utility;

import java.awt.Font;
import java.text.NumberFormat;

import javax.swing.JFrame;
import javax.swing.JLabel;

import view.MainFrame;

public class DataProcessing 
{
	private static DataProcessing dataProcessing = new DataProcessing();
	
	public static DataProcessing getDataProcessing()
	{
		return dataProcessing;
	}
	
	public int countChar(String targetString, char targetChar) // 문자열에 특정문자의 개수 반환하는 함수
	{
		int count = 0;
		for (int index = 0; index < targetString.length(); index++)
		{
			if (targetString.charAt(index) == targetChar)
				count++;
		}
		return count;
	}
	
	public String appendCommaInLong(Long number) // 숫자에 ,추가하는 함수
	{
		String formatResult= "";
		NumberFormat numberFormat = NumberFormat.getNumberInstance();
		formatResult = numberFormat.format(number);
		return formatResult;
	}

	public String appendCommaInString(String numberString) // 문자열 숫자에 ,추가하는 함수
	{
		String formatResult= "";
		String integerPart;
		String decimalPointPart;
		
		NumberFormat numberFormat = NumberFormat.getNumberInstance();
		
		if (numberString.split("\\.").length == 2) // point로 쪼갰을 때 정수부와 실수부로 쪼개진다면
		{
			integerPart = numberString.split("\\.")[0];
			decimalPointPart = numberString.split("\\.")[1];
			integerPart = numberFormat.format(Long.parseLong(integerPart));
			formatResult = integerPart + "." + decimalPointPart;
		}
		else if (numberString.contains(".")) // 마지막만 point면 
		{
			integerPart = numberString.substring(0, numberString.length()-1);
			formatResult = numberFormat.format(Long.parseLong(integerPart));
		}
		else // 정수면
		{
			formatResult = numberFormat.format(Long.parseLong(numberString));
		}
		return formatResult;
	}

	public String deleteComma(String str) // 문자열 숫자에 ,제거하는 함수
	{
		return str.replace(",", "");
	}
	
	public String deleteUnnecessaryDecimalPoint(double doubleValue) // 불필요 소수점 제거하는 함수
	{
	    if(doubleValue == (long) doubleValue)
	        return String.format("%d",(long)doubleValue);
	    else
	        return String.format("%s",doubleValue);
	}

	public void resizeLabel(JFrame frame, JLabel label)
	{
		label.setFont(new Font("맑은 고딕", 0, 45));
		while (label.getFont().getSize()/2 * label.getText().length() > frame.getWidth() - 50)
		{
			label.setFont(new Font("맑은 고딕", 0, label.getFont().getSize() - 1));
			if (label.getFont().getSize() < 0)
				break;
		}
	}
}
