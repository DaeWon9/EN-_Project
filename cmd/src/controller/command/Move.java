package controller.command;

import java.io.File;
import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.StandardCopyOption;
import java.util.regex.Pattern;

import controller.CmdService;
import model.UserPath;
import utility.Constant;
import utility.DataProcessing;
import view.CmdView;

public class Move implements CmdService
{
	protected UserPath userPath;
	protected CmdView cmdView;
	public Move(UserPath userPath, CmdView cmdView)
	{
		this.userPath = userPath;
		this.cmdView = cmdView;
	}
	@Override
	public void actionCommand(String inputCommand) 
	{
		String beforePath;
		String afterPath;
		if (inputCommand.split(" ").length < 2)
		{
			cmdView.print("명령 구문이 올바르지 않습니다.\n");
			return;
		}
		beforePath = getBeforePath(inputCommand, getBeforeFileName(inputCommand)) + getBeforeFileName(inputCommand);
		afterPath = getAfterPath(inputCommand, getAfterFileName(inputCommand)) + getAfterFileName(inputCommand);
		if (DataProcessing.get().isValidPath(beforePath))
			moveFile(beforePath, afterPath);
		else
			cmdView.print("지정된 파일을 찾을 수 없습니다.\n");
	}
	
	private void moveFile(String beforePath, String afterPath)
	{
		try
		{
			Files.move(new File(beforePath).toPath(), new File(afterPath).toPath());
			cmdView.print("\t1개 파일을 이동했습니다.\n");
		} 
		catch (IOException e)
		{
			if (e.toString().contains("FileAlreadyExistsException"))
			{
				if (isReplaceIfExistFile(afterPath))
					moveFileOnReplaceOption(beforePath, afterPath);
				else
					cmdView.print("\t0개 파일을 이동했습니다.\n");
			}
		}
	}
	
	private void moveFileOnReplaceOption(String beforePath, String afterPath)
	{
		try
		{
			Files.move(new File(beforePath).toPath(), new File(afterPath).toPath(), StandardCopyOption.REPLACE_EXISTING);
			cmdView.print("\t1개 파일을 이동했습니다.\n");
		}
		catch (IOException e)
		{
			e.printStackTrace();
		}
	}
	
	protected String getBeforeFileName(String inputCommand)
	{
		String[] splitedPath = inputCommand.split(" ")[1].split("\\\\");
		return "\\" + splitedPath[splitedPath.length - 1];
	}

	protected String getAfterFileName(String inputCommand)
	{
		String[] splitedPath;
		if (inputCommand.split(" ").length > 2)
			splitedPath = inputCommand.split(" ")[2].split("\\\\");
		else
			splitedPath = inputCommand.split(" ")[1].split("\\\\");
		return "\\" + splitedPath[splitedPath.length - 1];
	}
	
	protected String getBeforePath(String inputCommand, String fileName)
	{
		inputCommand = inputCommand.split(" ")[1];
		if (!inputCommand.contains(":"))
			inputCommand = userPath.get();
		return inputCommand.replace(fileName, "");
	}
	
	protected String getAfterPath(String inputCommand, String fileName)
	{
		if (inputCommand.split(" ").length > 2)
		{
			inputCommand = inputCommand.split(" ")[2];
			if (!inputCommand.contains(":"))
				inputCommand = userPath.get();
			return inputCommand.replace(fileName, "");
		}
		return userPath.get();
	}
	
	protected Boolean isReplaceIfExistFile(String path)
	{
		String userAnswer;
		boolean isValidAnswer = false, isReplace = false;
		while(!isValidAnswer)
		{
			cmdView.printReplaceIfExist(path);
			userAnswer = DataProcessing.get().getInputString();
			if (Pattern.matches(Constant.REGEX_PATTERN_YES_OR_ALL, userAnswer))
			{
				isReplace = true;
				isValidAnswer =  true;
			}
			else if (Pattern.matches(Constant.REGEX_PATTERN_NO, userAnswer))
				isValidAnswer = true;
		}
		return isReplace;
	}
	
}
