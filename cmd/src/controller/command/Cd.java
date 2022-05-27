package controller.command;

import java.util.Arrays;

import controller.CmdAction;
import model.UserPath;
import utility.Constant;
import utility.DataProcessing;
import utility.Constant.CdCommandType;
import utility.Constant.CommandKey;

public class Cd implements CmdAction
{
	private UserPath userPath;
	public Cd(UserPath userPath)
	{
		this.userPath = userPath;
	}
	
	@Override
	public void actionCommand(String inputCommand) 
	{
		int CommandType = classifyCdCommand(inputCommand);
		CdCommandType cdCommandType = CdCommandType.values()[CommandType];
		switch (cdCommandType)
		{
		case ERROR:
			System.out.println("ERROR");
			break;
		case CD:
			System.out.println(userPath.get() + "\n");
			break;
		case MOVE_START_PATH:
			System.out.println("cd\\");
			break;
		case UP_STAGE:
			moveUpPathStage(1);
			break;
		case DOUBLE_UP_STAGE:
			moveUpPathStage(2);
			break;
		case MOVE_INPUT_PATH:
			movePath(inputCommand);
			break;
		default:
			break;
		}
	}
	
	private void movePath(String inputCommand)
	{
		String targetPath = inputCommand.split("cd")[1];
		userPath.set(targetPath);
	}
	
	private void moveUpPathStage(int stage)
	{
		String currentPath = userPath.get();
		String[] splitedPath = currentPath.split("\\\\");
		int pathLenght = DataProcessing.get().countChar(currentPath, '\\');
		userPath.set(DataProcessing.get().mergePath(splitedPath, pathLenght + 1 - stage));
	}
	
	private int classifyCdCommand(String inputCommand)
	{
		String[] cdCommandKey = {"cd", "cd\\", "cd..", "cd..\\.."};	
		if (isCommandContainPath(inputCommand))
			return cdCommandKey.length + 1;
		else
		{
			for (int keyNumber = 0; keyNumber < cdCommandKey.length; keyNumber++)
			{
				if (inputCommand.equals(cdCommandKey[keyNumber]))
					return keyNumber + 1;
			}
			return Constant.CommandKey.ERROR.getIndex();
		}
	}
	
	private boolean isCommandContainPath(String inputCommand)
	{
		if (inputCommand.contains(":"))
			return true;
		return false;
	}
	
}
