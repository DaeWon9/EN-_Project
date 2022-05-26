package controller;

import controller.command.Dir;
import model.UserPath;
import utility.Constant;
import utility.DataProcessing;
import view.CmdView;
import view.Message;

public class MainController 
{
	private Message message = new Message();
	private CmdView cmdView = new CmdView();
	private UserPath userPath = new UserPath(System.getProperty("user.home"));
	private Dir dir = new Dir(userPath);
	
	public void start()
	{
		message.printVersion();
		commandListener();
		//System.out.println(classifyCommand("qweoqwekoq dir   ser"));
		//dir.actionCommand();
		
		//System.out.println(DataProcessing.get().getInputString());
		//message.print(Constant.CLS_COMMAND_STRING);

	}
	
	private void commandListener()
	{
		String inputCommand;
		boolean isExit = false;
		while(!isExit)
		{
			message.printCurrentPath(userPath.get());
			inputCommand = DataProcessing.get().getInputString();
			switch (classifyCommand(inputCommand))	
			{
			case 0:
				dir.actionCommand();
				break;
			case 1:
				break;
			case 2:
				break;
			case 3:
				break;
			case 4:
				message.printHelpMessage();
				break;
			case 5:
				message.print(Constant.CLS_COMMAND_STRING);
				break;
			default:
				break;
			}
		}
	}
	
	private int classifyCommand(String inputCommand)
	{
		inputCommand = inputCommand.toLowerCase();
		String[] commandKey = {"dir", "cd", "copy", "move", "help", "cls"};	
		for (int keyNumber = 0; keyNumber < commandKey.length; keyNumber++)
		{
			System.out.println(inputCommand);
			System.out.println(commandKey[keyNumber]);
			if (inputCommand.contains(commandKey[keyNumber]))
				return keyNumber;
		}
		return -1;
	}
}
