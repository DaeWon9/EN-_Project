package controller.command;

import controller.CmdService;
import model.UserPath;
import utility.DataProcessing;
import view.CmdView;
import utility.Constant;
import utility.Constant.CdCommandType;

public class Cd implements CmdService
{
	private UserPath userPath;
	private CmdView cmdView;
	public Cd(UserPath userPath, CmdView cmdView)
	{
		this.userPath = userPath;
		this.cmdView = cmdView;
	}
	
	@Override
	public void actionCommand(String inputCommand) 
	{
		int CommandType = classifyCdCommand(inputCommand);
		CdCommandType cdCommandType = CdCommandType.values()[CommandType];
		switch (cdCommandType)
		{
		case SHIFT:
			shiftPath(inputCommand, cdCommandType);
			break;
		case CD:
			cmdView.print(userPath.get() + "\n");
			break;
		case MOVE_START_PATH:
			shiftToStartPath();
			break;
		case UP_STAGE:
			userPath.set(DataProcessing.get().moveUpPathStage(userPath.get(), 1));
			break;
		case DOUBLE_UP_STAGE:
			userPath.set(DataProcessing.get().moveUpPathStage(userPath.get(), 2));
			break;
		case MOVE_INPUT_PATH:
			shiftPath(inputCommand, cdCommandType);
			break;
		default:
			break;
		}
	}
	
	private void shiftPath(String inputCommand, CdCommandType cdCommandType)
	{
		String targetPath = getShiftPath(inputCommand, cdCommandType);
		targetPath = targetPath.replace(" ", "");
		if (DataProcessing.get().isValidPath(targetPath))
			userPath.set(targetPath);
		else
			cmdView.print("지정된 경로를 찾을 수 없습니다.\n");
	}
	
	private String getShiftPath(String inputCommand, CdCommandType cdCommandType)
	{
		String ShiftedPath = "";
		switch (cdCommandType)
		{
		case SHIFT: // cd 명령어 뒤에 입력된 위치로 이동
			ShiftedPath = (userPath.get() + "\\" + inputCommand.split("cd")[1]).replace("\\\\", "\\");
			break;
		case MOVE_INPUT_PATH: // 입력된 절대경로로 이동
			ShiftedPath = inputCommand.split("cd")[1];
			break;
		default:
			break;
		}
		return ShiftedPath;
	}
	
	private void shiftToStartPath()
	{
		String currentPath = userPath.get();
		userPath.set(currentPath.substring(0, 3)); //시작 경로 set
	}
	
	private int classifyCdCommand(String inputCommand)
	{
		String[] cdCommandKey = {"cd", "cd\\", "cd..", "cd..\\.."};	
		if (isCommandContainPath(inputCommand)) // 절대경로를 포함하고 있을경우
			return cdCommandKey.length + 1;
		else
		{
			for (int keyNumber = 0; keyNumber < cdCommandKey.length; keyNumber++)
			{
				if (inputCommand.equals(cdCommandKey[keyNumber]))
					return keyNumber + 1;
			}
			return Constant.CdCommandType.SHIFT.getIndex();
		}
	}
	
	private boolean isCommandContainPath(String inputCommand)
	{
		if (inputCommand.contains(":"))
			return true;
		return false;
	}
}
