package controller.command;

import java.io.File;
import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.StandardCopyOption;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

import controller.CmdAction;
import model.UserPath;
import view.CmdView;

public class Move implements CmdAction
{
	private UserPath userPath;
	private CmdView cmdView;
	public Move(UserPath userPath, CmdView cmdView)
	{
		this.userPath = userPath;
		this.cmdView = cmdView;
	}
	@Override
	public void actionCommand(String inputCommand) 
	{
		if (inputCommand.split(" ").length < 2)
		{
			cmdView.print("명령 구문이 올바르지 않습니다.\n");
			return;
		}
		System.out.println(getBeforePath(inputCommand, getBeforeFileName(inputCommand)) + getBeforeFileName(inputCommand));
		System.out.println("to");
		System.out.println(getAfterPath(inputCommand, getAfterFileName(inputCommand)) + getAfterFileName(inputCommand));
		
		//moveFile(getBeforePath(inputCommand) + getBeforeFileName(inputCommand), getAfterPath(inputCommand) + getAfterFileName(inputCommand));
	}
	
	private void moveFile(String beforePath, String afterPath)
	{
		try 
		{
			Files.move(new File(beforePath).toPath(), new File(afterPath).toPath(), StandardCopyOption.REPLACE_EXISTING);
		} 
		catch (IOException e)
		{
			System.out.println(e);
			e.printStackTrace();
		}
	}
	
	private String getBeforeFileName(String inputCommand)
	{
		String[] splitedPath = inputCommand.split(" ")[1].split("\\\\");
		return "\\" + splitedPath[splitedPath.length - 1];
	}

	private String getAfterFileName(String inputCommand)
	{
		String[] splitedPath;
		if (inputCommand.split(" ").length > 2)
			splitedPath = inputCommand.split(" ")[2].split("\\\\");
		else
			splitedPath = inputCommand.split(" ")[1].split("\\\\");
		return "\\" + splitedPath[splitedPath.length - 1];
	}
	
	private String getBeforePath(String inputCommand, String fileName)
	{
		inputCommand = inputCommand.split(" ")[1];
		if (!inputCommand.contains(":"))
			inputCommand = userPath.get();
		return inputCommand.replace(fileName, "");
	}
	
	private String getAfterPath(String inputCommand, String fileName)
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
	
}
