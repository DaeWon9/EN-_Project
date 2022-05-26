package view;

import utility.Constant;

public class Message 
{
	public void printVersion()
	{
		System.out.println(System.getProperty("os.arch"));
		System.out.println("Microsoft Windows [Version 10.0.22000.675]");
		System.out.println("(c) Microsoft Corporation. All rights reserved.\n");
	}
	
	public void printCurrentPath(String path)
	{
		System.out.print(path + ">");
	}
	
	public void printHelpMessage()
	{
		System.out.print(Constant.HELP_COMMAND_STRING);
	}
}
