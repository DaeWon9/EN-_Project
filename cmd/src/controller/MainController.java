package controller;

import controller.command.Cd;
import controller.command.Copy;
import controller.command.Dir;
import controller.command.Move;
import model.UserPath;
import utility.Constant;
import utility.Constant.CommandKey;
import utility.DataProcessing;
import view.CmdView;
import view.Message;

public class MainController 
{
	private Message message = new Message();
	private CmdView cmdView = new CmdView();
	private UserPath userPath = new UserPath(System.getProperty("user.home"));
	private Dir dir = new Dir(userPath);
	private Cd cd = new Cd(userPath);
	private Copy copy = new Copy();
	private Move move = new Move();
	
	public void start()
	{
		message.printVersion();
		commandListener();
	}
	
	private void commandListener()
	{
		String inputCommand;
		boolean isExit = false;
		while(!isExit)
		{
			message.printCurrentPath(userPath.get());
			inputCommand = DataProcessing.get().getInputString(); 
			inputCommand = inputCommand.toLowerCase();
			inputCommand = inputCommand.replace(" ", "");
			CommandKey commandKey = CommandKey.values()[classifyCommand(inputCommand)];
			switch (commandKey)	
			{
			case DIR:
				dir.actionCommand(inputCommand);
				break;
			case CD:
				cd.actionCommand(inputCommand);
				break;
			case COPY:
				copy.actionCommand(inputCommand);
				break;
			case MOVE:
				move.actionCommand(inputCommand);
				break;
			case HELP:
				message.print(Constant.HELP_COMMAND_STRING);
				break;
			case CLS:
				message.print(Constant.CLS_COMMAND_STRING);
				break;
			case ERROR:
				message.printError(inputCommand);
			default:
				break;
			}
		}
	}
	
	private int classifyCommand(String inputCommand)
	{
		String[] commandKey = {"dir", "cd", "copy", "move", "help", "cls"};	
		for (int keyNumber = 0; keyNumber < commandKey.length; keyNumber++)
		{
			if (inputCommand.contains(commandKey[keyNumber]))
				return keyNumber + 1;
		}
		return Constant.CommandKey.ERROR.getIndex();
	}
}
