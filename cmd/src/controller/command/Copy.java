package controller.command;

import java.io.File;
import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.StandardCopyOption;

import controller.CmdService;
import model.UserPath;
import utility.Constant;
import utility.DataProcessing;
import view.CmdView;

public class Copy extends Move implements CmdService
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
		File beforeFile = new File(beforePath);
		if (DataProcessing.get().isValidPath(beforePath) && beforeFile.isDirectory())
			copyDirectory(beforePath, afterPath);
		else if (DataProcessing.get().isValidPath(beforePath) && beforeFile.isFile())
			copyFile(beforePath, afterPath);
		else
			cmdView.print("지정된 파일을 찾을 수 없습니다.\n");
	}
	
	private void copyFile(String beforePath, String afterPath)
	{
		try 
		{
			Files.copy(new File(beforePath).toPath(), new File(afterPath).toPath());
			cmdView.print("\t1개 파일이 복사되었습니다.\n");
		} 
		catch (IOException e)
		{
			if (e.toString().contains("FileAlreadyExistsException"))
			{
				if (isReplaceIfExistFile(afterPath) == Constant.ReplaceOption.YES.getIndex() || isReplaceIfExistFile(afterPath) == Constant.ReplaceOption.ALL.getIndex())
				{
					copyFileOnReplaceOption(beforePath, afterPath);
					cmdView.print("\t1개 파일이 복사되었습니다.\n");
				}
				else
					cmdView.print("\t0개 파일이 복사되었습니다.\n");
			}
		}
	}
	
	private void copyDirectory(String beforePath, String afterPath)
	{
		boolean isALLOption = false;
		int movedFileCount = 0, replaceOption;
		File beforeFile = new File(beforePath);
		File[] fileList = beforeFile.listFiles(DataProcessing.get().fileFilter);
		for (File fileName : fileList)
		{
			if(beforePath.equals(afterPath))
			{
				afterPath = DataProcessing.get().moveUpPathStage(afterPath, 1);
				afterPath = afterPath + getBeforeFileName(fileName.getPath().replace("\\","\\\\"));
			}
			cmdView.print(fileName.toString().replace(userPath.get() + "\\", "") + "\n");
			try 
			{
				if (isALLOption)
					copyFileOnReplaceOption(fileName.getPath(), afterPath);
				else
					Files.copy(fileName.toPath(), new File(afterPath).toPath());
				movedFileCount++;
			}
			catch (IOException e)
			{
				if (e.toString().contains("FileAlreadyExistsException"))
				{
					replaceOption = isReplaceIfExistFile(afterPath);
					if (replaceOption == Constant.ReplaceOption.YES.getIndex())
					{
						copyFileOnReplaceOption(fileName.getPath(), afterPath);
						movedFileCount++;
					}
					else if (replaceOption == Constant.ReplaceOption.ALL.getIndex())
					{
						copyFileOnReplaceOption(fileName.getPath(), afterPath);
						movedFileCount++;
						isALLOption = true;
					}
				}
			}
		}
		cmdView.print("\t" + movedFileCount + "개 파일이 복사되었습니다.\n");
	}
	
	private void copyFileOnReplaceOption(String beforePath, String afterPath)
	{
		try
		{
			Files.copy(new File(beforePath).toPath(), new File(afterPath).toPath(), StandardCopyOption.REPLACE_EXISTING);
		}
		catch (IOException e)
		{
			e.printStackTrace();
		}
	}
}
