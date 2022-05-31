package controller.command;

import java.io.File;
import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.StandardCopyOption;
import controller.CmdService;
import model.UserPath;
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
		// path 및 fileName 설정 후 케이스별로 copy 진행 
		File beforeFile = new File(beforePath);
		if (DataProcessing.get().isValidPath(beforePath) && beforeFile.isDirectory())
			copyDirectory(beforePath, afterPath, beforeFileName, afterFileName);
		else if (DataProcessing.get().isValidPath(beforePath) && beforeFile.isFile())
			copyFile(beforePath, afterPath, beforeFileName, afterFileName);
		else
			cmdView.print("지정된 파일을 찾을 수 없습니다.\n");
	}
	
	private void copyFile(String beforePath, String afterPath, String beforeFileName, String afterFileName)
	{
		try 
		{
			if (new File(afterPath).isDirectory()) //copy 명령어는 파일만 copy 되기에 디렉터리면 파일명을 넣어줘야함
				afterPath = afterPath + beforeFileName;
			Files.copy(new File(beforePath).toPath(), new File(afterPath).toPath());
			cmdView.print("\t1개 파일이 복사되었습니다.\n");
		} 
		catch (IOException e)
		{
			if (e.toString().contains("FileAlreadyExistsException")) // 이미 존재하는 파일이면
			{
				ReplaceOption replaceOption = ReplaceOption.values()[getReplaceOption(afterPath)]; // 덮어쓰기 옵션 받기
				switch (replaceOption)
				{
				case ALL:
				case YES:	
					copyFileOnReplaceOption(beforePath, afterPath);
					cmdView.print("\t1개 파일이 복사되었습니다.\n");
					break;
				case NO:
					cmdView.print("\t0개 파일이 복사되었습니다.\n");
					break;
				default:
					break;
				}
			}
		}
	}
	private void copyDirectory(String beforePath, String afterPath, String beforeFileName, String afterFileName) // copy를 당하는 쪽이 디렉터리일때는 내부의 파일들을 copy
	{
		boolean isALLOption = false;
		int movedFileCount = 0;
		File beforeFile = new File(beforePath);
		File[] fileList = beforeFile.listFiles(DataProcessing.get().fileFilter);
		
		for (File fileName : fileList) // 해당 디렉터리내부의 파일들을 for문으로 반복
		{	
			afterPath = setAfterPathInDirecotryCopy(beforePath, afterPath, beforeFileName, afterFileName, fileName); // 옮겨지는 위치 조정
			cmdView.print(fileName.toString().replace(userPath.get() + "\\", "") + "\n");
			try 
			{
				if (isALLOption)
					copyFileOnReplaceOption(fileName.getPath(), afterPath); // all 옵션을 입력받았을경우 하위파일들은 모두 덮어쓰기
				else
					Files.copy(fileName.toPath(), new File(afterPath).toPath());
				movedFileCount++;
			}
			catch (IOException e)
			{
				if (e.toString().contains("FileAlreadyExistsException")) // 중복파일일경우
				{
					ReplaceOption replaceOption = ReplaceOption.values()[getReplaceOption(afterPath)]; // 덮어쓰기 옵션 받기
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
		if (fileList.length == 0) // copy를 당하는 디렉터리내부에 파일이 하나도 없을경우
		{
			cmdView.print(beforeFileName.replace("\\","") + "\\*\n");
			cmdView.print("지정된 파일을 찾을 수 없습니다.\n");
		}
		cmdView.print("\t" + movedFileCount + "개 파일이 복사되었습니다.\n");
	}
	
	private String setAfterPathInDirecotryCopy(String beforePath, String afterPath, String beforeFileName, String afterFileName, File fileName) 
	{
		if (beforeFileName.equals(afterFileName))
		{
			afterPath = DataProcessing.get().moveUpPathStage(afterPath, 1);
			afterPath = afterPath + getBeforeFileName(fileName.getPath().replace("\\","\\\\"));
		}
		else
			afterPath = afterPath + getBeforeFileName(fileName.getPath().replace("\\","\\\\"));
		return afterPath;
	}
	
	private void copyFileOnReplaceOption(String beforePath, String afterPath) // 덮어쓰기 옵션으로 copy 진행
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
