package controller.command;

import java.io.File;
import controller.CmdAction;
import model.UserPath;
import utility.Constant;
import utility.DataProcessing;
import view.Message;
import utility.Constant.CdCommandType;


public class Cd implements CmdAction
{
	private UserPath userPath;
	private Message message;
	public Cd(UserPath userPath, Message message)
	{
		this.userPath = userPath;
		this.message = message;
	}
	
	@Override
	public void actionCommand(String inputCommand) 
	{
		int CommandType = classifyCdCommand(inputCommand);
		CdCommandType cdCommandType = CdCommandType.values()[CommandType];
		switch (cdCommandType)
		{
		case SHIFT:
			shiftPath(inputCommand);
			break;
		case CD:
			message.print(userPath.get() + "\n");
			break;
		case MOVE_START_PATH:
			moveStartPath();
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
	
	private boolean isValidPath(String movePath)
	{
		File file = new File(movePath);
		return file.exists();
	}
	
	private void shiftPath(String inputCommand)
	{
		String targetPath = userPath.get() + "\\" + inputCommand.split("cd")[1];
		if (isValidPath(targetPath))
			userPath.set(targetPath);
		else
			message.print("지정된 경로를 찾을 수 없습니다.\n");
	}
	
	private void moveStartPath()
	{
		String currentPath = userPath.get();
		userPath.set(currentPath.substring(0, 3));
	}
	
	private void movePath(String inputCommand)
	{
		String targetPath = inputCommand.split("cd")[1];
		if (isValidPath(targetPath))
			userPath.set(targetPath);
		else
			message.print("지정된 경로를 찾을 수 없습니다.\n");
	}
	
	private void moveUpPathStage(int stage)
	{
		String mergedPath;
		String[] splitedPath = userPath.get().split("\\\\");
		int pathLenght = DataProcessing.get().countChar(userPath.get(), '\\');
		mergedPath = DataProcessing.get().mergePath(splitedPath, pathLenght + 1 - stage);
		if (mergedPath.length() < 3)
			mergedPath  = mergedPath + "\\";
		userPath.set(mergedPath);
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
