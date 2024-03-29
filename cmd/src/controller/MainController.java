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

public class MainController 
{
	private CmdView cmdView = new CmdView();
	private UserPath userPath = new UserPath(System.getProperty("user.home"));
	private Dir dir = new Dir(userPath, cmdView);
	private Cd cd = new Cd(userPath, cmdView);
	private Move move = new Move(userPath, cmdView);
	private Copy copy = new Copy(userPath, cmdView);
	
	public void start()
	{
		cmdView.printWindowVersion();
		commandListener();
	}
	
	private void commandListener()
	{
		String inputCommand, lowerCommand;
		boolean isExit = false;
		while(!isExit)
		{
			cmdView.printCurrentPath(userPath.get());
			inputCommand = DataProcessing.get().getInputString();
			lowerCommand = inputCommand.toLowerCase();
			CommandKey commandKey = CommandKey.values()[classifyCommand(lowerCommand)];
			switch (commandKey)	
			{
			case DIR:
				dir.actionCommand(lowerCommand);
				break;
			case CD:
				cd.actionCommand(lowerCommand);
				break;
			case COPY:
				copy.actionCommand(lowerCommand);
				break;
			case MOVE:
				move.actionCommand(lowerCommand);
				break;
			case HELP:
				cmdView.print(Constant.HELP_COMMAND_STRING);
				break;
			case CLS:
				cmdView.print(Constant.CLS_COMMAND_STRING);
				break;
			case ERROR:
				cmdView.printError(lowerCommand);
			default:
				break;
			}
		}
	}
	
	private int classifyCommand(String inputCommand)
	{
		if (inputCommand.equals(""))
			return Constant.CommandKey.NONE.getIndex() + 1;
		inputCommand = inputCommand.split(" ")[0];
		String[] commandKey = {"dir", "cd", "copy", "move", "help", "cls"};	
		for (int keyNumber = 0; keyNumber < commandKey.length; keyNumber++)
		{
			if (inputCommand.contains(commandKey[keyNumber]))
				return keyNumber + 1;
		}
		return Constant.CommandKey.ERROR.getIndex();
	}
}
