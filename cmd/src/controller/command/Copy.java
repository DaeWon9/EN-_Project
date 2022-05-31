package controller.command;

import java.io.File;
import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.StandardCopyOption;

import controller.CmdService;
import model.UserPath;
import utility.Constant;
import utility.DataProcessing;
import utility.Constant.ReplaceOption;
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
		String beforeFileName, afterFileName;
		String beforePath, afterPath;
		if (inputCommand.split(" ").length < 2)
		{
			cmdView.print("명령 구문이 올바르지 않습니다.\n");
			return;
		}
		beforeFileName = getBeforeFileName(inputCommand);
		afterFileName = getAfterFileName(inputCommand);
		beforePath = getBeforePath(inputCommand, beforeFileName) + beforeFileName;
		afterPath = getAfterPath(inputCommand, afterFileName) + afterFileName;		
		
		File beforeFile = new File(beforePath);
		if (DataProcessing.get().isValidPath(beforePath) && beforeFile.isDirectory())
			copyDirectory(beforePath, afterPath, beforeFileName, afterFileName);
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
				if (getReplaceOption(afterPath) == Constant.ReplaceOption.YES.getIndex() || getReplaceOption(afterPath) == Constant.ReplaceOption.ALL.getIndex())
				{
					copyFileOnReplaceOption(beforePath, afterPath);
					cmdView.print("\t1개 파일이 복사되었습니다.\n");
				}
				else
					cmdView.print("\t0개 파일이 복사되었습니다.\n");
			}
		}
	}
	private void copyDirectory(String beforePath, String afterPath, String beforeFileName, String afterFileName)
	{
		boolean isALLOption = false;
		int movedFileCount = 0;
		File beforeFile = new File(beforePath);
		File[] fileList = beforeFile.listFiles(DataProcessing.get().fileFilter);
		
		if (fileList.length == 0)
		{
			cmdView.print(beforeFileName.replace("\\","") + "\\*\n");
			cmdView.print("지정된 파일을 찾을 수 없습니다.\n");
		}
		for (File fileName : fileList)
		{	
			afterPath = setAfterPathIfIsDirectory(beforePath, afterPath, beforeFileName, afterFileName, fileName);
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
					ReplaceOption replaceOption = ReplaceOption.values()[getReplaceOption(afterPath)];
					switch (replaceOption)
					{
					case ALL:
						isALLOption = true;
					case YES:	
						copyFileOnReplaceOption(fileName.getPath(), afterPath);
						movedFileCount++;
						break;
					case NO:
					default:
						break;
					}
				}
			}
		}
		cmdView.print("\t" + movedFileCount + "개 파일이 복사되었습니다.\n");
	}
	
	private String setAfterPathIfIsDirectory(String beforePath, String afterPath, String beforeFileName, String afterFileName, File fileName) 
	{
		if (beforeFileName.equals(afterFileName))
		{
			afterPath = DataProcessing.get().moveUpPathStage(afterPath, 1);
			afterPath = afterPath + getBeforeFileName(fileName.getPath().replace("\\","\\\\"));
		}
		return afterPath;
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
