package controller.command;

import java.io.File;
import controller.CmdService;
import model.UserPath;
import utility.DataProcessing;
import view.CmdView;

public class Dir implements CmdService
{
	private UserPath userPath;
	private CmdView cmdView;
	public Dir(UserPath userPath, CmdView cmdView)
	{
		this.userPath = userPath;
		this.cmdView = cmdView;
	}
	
	@Override
	public void actionCommand(String inputCommand)
	{
		cmdView.printDirCommandResult(getFileList(inputCommand), getFilePath(inputCommand));
	}
		
	private String getFilePath(String inputCommand)
	{
		String filePath;
		if (inputCommand.equals("dir"))
			filePath =  userPath.get();
		else
			filePath = inputCommand.split("dir")[1];
		return filePath;
	}
	
	private File getFileList(String inputCommand)
	{
		File file = null;
        String pathString = getFilePath(inputCommand);
        if (!DataProcessing.get().isValidPath(pathString))
        	cmdView.print("파일을 찾을 수 없습니다.\n\n");
        else
	        file = new File(pathString);
        return file;
	}

}
