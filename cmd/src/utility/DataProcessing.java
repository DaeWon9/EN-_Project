package utility;

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
		scanner.close();
		return inputString;
	}
}
