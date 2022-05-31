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
import utility.Constant.ReplaceOption;
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
		String beforePath, afterPath;
		String beforeFileName, afterFileName;
		if (inputCommand.split(" ").length < 2)
		{
			cmdView.print("명령 구문이 올바르지 않습니다.\n");
			return;
		}
		beforeFileName = getBeforeFileName(inputCommand);
		afterFileName = getAfterFileName(inputCommand);
		beforePath = getBeforePath(inputCommand, beforeFileName) + beforeFileName;
		afterPath = getAfterPath(inputCommand, afterFileName) + afterFileName;
		if (new File(afterPath).isDirectory())
			afterPath = getAfterPath(inputCommand, beforeFileName) + beforeFileName;
		// path 및 fileName설정 후 move 명령어 수행
		if (DataProcessing.get().isValidPath(beforePath))
			moveFile(beforePath, afterPath);
		else
			cmdView.print("지정된 파일을 찾을 수 없습니다.\n");
	}
	
	private void moveFile(String beforePath, String afterPath)
	{
		boolean isDirectory = new File(beforePath).isDirectory();
		try
		{
			Files.move(new File(beforePath).toPath(), new File(afterPath).toPath());
			cmdView.printMoveSuccessMessage(beforePath, isDirectory, 1);
		} 
		catch (IOException e)
		{
			if (e.toString().contains("FileAlreadyExistsException")) // 중복파일이면
			{
				ReplaceOption replaceOption = ReplaceOption.values()[getReplaceOption(afterPath)]; // 중복옵션 입력받기
				switch (replaceOption) // 옵션에따라 처리
				{
				case ALL:
				case YES:	
					moveFileOnReplaceOption(beforePath, afterPath);
					break;
				case NO:
					cmdView.printMoveSuccessMessage(beforePath, isDirectory, 0);
					break;
				default:
					break;
				}
			}
		}
	}
	
	private void moveFileOnReplaceOption(String beforePath, String afterPath)
	{
		if (new File(afterPath).isDirectory()) // move 명령어 수행 시 이후경로가 디렉터리일경우 엑세스 거부 출력
		{
			cmdView.print("액세스가 거부되었습니다.\n");
			return;
		}
		
		boolean isDirectory = new File(beforePath).isDirectory();
		try
		{
			Files.move(new File(beforePath).toPath(), new File(afterPath).toPath(), StandardCopyOption.REPLACE_EXISTING);
			cmdView.printMoveSuccessMessage(beforePath, isDirectory, 1);
		}
		catch (IOException e)
		{
			e.printStackTrace();
		}
	}
	
	protected String getBeforeFileName(String inputCommand)
	{
		String[] splitedPath;
		if (inputCommand.contains(" "))
			splitedPath = inputCommand.split(" ")[1].split("\\\\");
		else
			splitedPath = inputCommand.split("\\\\");
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
	
	protected String getPath(String inputCommand, String fileName)
	{
		if (!inputCommand.contains("\\")) // 파일명만 입력된 경우
			return userPath.get();
		else
		{
			if (inputCommand.contains(":")) //절대경로
				inputCommand = inputCommand.replace(fileName, "");
			else // 상대경로
				inputCommand = userPath.get() + inputCommand.replace(fileName, "");
		}
		return inputCommand;
	}
	
	protected String getBeforePath(String inputCommand, String fileName)
	{
		inputCommand = inputCommand.split(" ")[1];
		return getPath(inputCommand,fileName);
	}
	
	protected String getAfterPath(String inputCommand, String fileName)
	{
		if (inputCommand.split(" ").length > 2)
		{
			inputCommand = inputCommand.split(" ")[2];
			return getPath(inputCommand,fileName);
		}
		return userPath.get();
	}
	
	protected int getReplaceOption(String path) // 덮어쓰기 옵션을 user로 부터 받아옴
	{
		String userAnswer;
		boolean isValidAnswer = false;
		int isReplace = 0;
		while(!isValidAnswer)
		{
			cmdView.printReplaceIfExist(path);
			userAnswer = DataProcessing.get().getInputString();
			if (Pattern.matches(Constant.REGEX_PATTERN_YES, userAnswer))
			{
				isReplace = Constant.ReplaceOption.YES.getIndex();
				isValidAnswer =  true;
			}
			else if (Pattern.matches(Constant.REGEX_PATTERN_NO, userAnswer))
			{
				isReplace = Constant.ReplaceOption.NO.getIndex();
				isValidAnswer =  true;
			}
			else if (Pattern.matches(Constant.REGEX_PATTERN_ALL, userAnswer))
			{
				isReplace = Constant.ReplaceOption.ALL.getIndex();
				isValidAnswer = true;
			}
		}
		return isReplace;
	}
	
}
