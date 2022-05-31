package controller.command;

import java.io.File;
import java.util.ArrayList;
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
		ArrayList<File> fileArrayList = getFileArrayList(getFile(inputCommand), getFilePath(inputCommand));
		cmdView.printDirCommandResult(getFile(inputCommand), getFilePath(inputCommand), fileArrayList);
	}
		
	private String getFilePath(String inputCommand)
	{
		String filePath;
		if (inputCommand.equals("dir"))
			filePath =  userPath.get();
		else
			filePath = (userPath.get() + "\\" + inputCommand.split("dir")[1]).replace("\\\\", "\\");
		return filePath.replace(" ", "");
	}
	
	private File getFile(String inputCommand)
	{
		File file = null;
        String pathString = getFilePath(inputCommand);     
        pathString = DataProcessing.get().getCanonicalPath(pathString);
        if (!DataProcessing.get().isValidPath(pathString))
        	cmdView.print("파일을 찾을 수 없습니다.\n\n");
        else
	        file = new File(pathString);
        return file;
	}

	private ArrayList<File> getFileArrayList(File file, String filePath)
	{
		if (file == null)
			return null;
        File[] fileList = file.listFiles(DataProcessing.get().fileFilter);
        ArrayList<File> fileArrayList = new ArrayList<File>(); 
        for (File fileName : fileList) 
        	fileArrayList.add(fileName);
        
        // dir 명령어 수행결과 최상단에 ., .. 처리부분
        if (filePath.equals(filePath.substring(0, 3))) // root 경로일경우는 바로 return
        	return fileArrayList;
        if (DataProcessing.get().moveUpPathStage(filePath, 1).equals(filePath.substring(0, 3)))
        	fileArrayList.add(0, new File(filePath));
        else
        {
        	fileArrayList.add(0, new File(DataProcessing.get().moveUpPathStage(filePath, 1)));
            fileArrayList.add(0, new File(filePath));
        }
        return fileArrayList;
	}
}
