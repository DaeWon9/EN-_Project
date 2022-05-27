package controller.command;

import java.io.File;
import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.StandardCopyOption;

import controller.CmdAction;
import model.UserPath;
import utility.DataProcessing;
import view.CmdView;

public class Copy extends Move implements CmdAction
{
	public Copy(UserPath userPath, CmdView cmdView) 
	{
		super(userPath, cmdView);
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
			copyFile(beforePath, afterPath);
		else
			cmdView.print("지정된 파일을 찾을 수 없습니다.\n");
	}
	
	private void copyFile(String beforePath, String afterPath)
	{
		try 
		{
			Files.copy(new File(beforePath).toPath(), new File(afterPath).toPath(), StandardCopyOption.REPLACE_EXISTING);
			cmdView.print("\t1개 파일이 복사되었습니다.\n");
		} 
		catch (IOException e)
		{
			System.out.println(e);
			e.printStackTrace();
		}
	}
}
