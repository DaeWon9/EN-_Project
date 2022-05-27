package controller.command;

import java.util.Arrays;

import controller.CmdAction;
import model.UserPath;
import utility.Constant;
import utility.DataProcessing;

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
		//System.out.println(inputCommand);
		int cdCommandType = classifyCdCommand(inputCommand);
		//System.out.println(System.getProperty("user.home"));
		switch (cdCommandType)
		{
		case 0:
			System.out.println("ERROR");
			break;
		case 1: //cd
			System.out.println(userPath.get() + "\n");
			break;
		case 2: //cd..
			moveUpPathStage(1);
			break;
		case 3: //cd\
			System.out.println("cd\\");
			break;
		case 4: // cd..\..
			moveUpPathStage(2);
			break;
		case 5: // cd c:~~
			System.out.println("cd c:");
			break;
		default:
			break;
		}
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
		String[] cdCommandKey = {"cd", "cd..", "cd\\", "cd..\\.."};	
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
		if (inputCommand.contains("c:"))
			return true;
		return false;
	}
	
}
