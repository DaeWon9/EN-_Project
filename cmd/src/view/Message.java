package view;

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
	
	public void printVersion()
	{
		System.out.println("Microsoft Windows [Version 10.0.22000.675]");
		System.out.println("(c) Microsoft Corporation. All rights reserved.\n");
	}
	
	public void printCurrentPath(String path)
	{
		System.out.print(path + ">");
	}
}
