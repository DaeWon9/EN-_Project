package utility;

import java.util.Arrays;
import java.util.Scanner;

public class DataProcessing 
{
	private static final DataProcessing dataProcessing = new DataProcessing();
	
	public static final DataProcessing get()
	{
		return dataProcessing;
	}
	
	public String getInputString()
	{
		String inputString = "";
		Scanner scanner = new Scanner(System.in);
		inputString = scanner.nextLine();
		return inputString;
	}
	
	public int countChar(String targetString, char targetChar) // 문자열에서 특정문자의 개수를 반환하는 함수
	{
		int count = 0;
		for (int index = 0; index < targetString.length(); index++)
		{
			if (targetString.charAt(index) == targetChar)
				count++;
		}
		return count;
	}
	
	public String mergePath(String[] splitedPath, int lastIndex)
	{			
		StringBuilder mergedPath = new StringBuilder();
		mergedPath.append(splitedPath[0]);
		for (int index = 1; index < lastIndex; index++)
		{
			mergedPath.append("\\");
			mergedPath.append(splitedPath[index]);
		}
		return mergedPath.toString();
	}
}
