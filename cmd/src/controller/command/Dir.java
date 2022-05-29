package controller.command;

import java.io.File;
import java.io.FileFilter;
import java.nio.file.Files;
import java.nio.file.Paths;
import java.util.ArrayList;

import controller.CmdService;
import model.UserPath;
import utility.DataProcessing;
import view.CmdView;

public class Dir implements CmdService
{
	private UserPath userPath;
	private CmdView cmdView;
	private FileFilter fileFilter = new FileFilter() 
    {
		@Override
		public boolean accept(File pathname) 
		{
			if (pathname.isHidden() || (!Files.isReadable(Paths.get(pathname.getPath())) && pathname.isDirectory()))
				return false;
			return true;
		}
	};
	
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
			filePath = inputCommand.split("dir")[1].replace(" ", "");
		return filePath;
	}
	
	private File getFile(String inputCommand)
	{
		File file = null;
        String pathString = getFilePath(inputCommand);
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
        File[] fileList = file.listFiles(fileFilter);
        ArrayList<File> fileArrayList = new ArrayList<File>(); 
        for (File fileName : fileList) 
        	fileArrayList.add(fileName);
        
        if (filePath.equals(filePath.substring(0, 3)))
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
