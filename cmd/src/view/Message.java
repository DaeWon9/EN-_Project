package view;

import java.io.BufferedReader;
import java.io.InputStreamReader;

import utility.Constant;

public class Message 
{
	public void print(String message)
	{
		System.out.print(message);
	}
	
	public void printError(String inputString)
	{
		System.out.print(String.format(Constant.ERROR_MESSAGE_FORM, inputString));
	}
	
	public void printWindowVersion()
	{
		try
		{
		    Process process = Runtime.getRuntime().exec("cmd");
		    BufferedReader reader = new BufferedReader(new InputStreamReader(process.getInputStream()));
		    System.out.println(reader.readLine());
		    System.out.println(reader.readLine() + "\n");
		    reader.close();
		    process.destroy();
		}
		catch(Exception e)
		{
			e.printStackTrace();
		}
	}
		
	public void printCurrentPath(String path)
	{
		System.out.print(path + ">");
	}
	
	public void printReplaceIfExist(String path)
	{
		System.out.print(path + "을(를) 덮어쓰시겠습니까? (Yes/No/All): ");
	}
}
