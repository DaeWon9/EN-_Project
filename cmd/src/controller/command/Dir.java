package controller.command;

import java.io.File;
import java.text.SimpleDateFormat;

import controller.CmdAction;
import model.UserPath;
import utility.DataProcessing;
import view.Message;

public class Dir implements CmdAction
{
	private UserPath userPath;
	private Message message;
	public Dir(UserPath userPath, Message message)
	{
		this.userPath = userPath;
		this.message = message;
	}
	
	@Override
	public void actionCommand(String inputCommand)
	{
		getFileList(inputCommand);
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
	
	private void getFileList(String inputCommand)
	{
		int dirCount = 0,fileCount = 0;
        String dirString = "";
        String pathString = getFilePath(inputCommand);
        if (!DataProcessing.get().isValidPath(pathString))
        {
        	message.print("파일을 찾을 수 없습니다.\n\n");
        	return;
        }
        File file = new File(pathString);
        File[] fileList = file.listFiles();
        for (File fileName : fileList) 
        {
        	if (fileName.isDirectory())
        	{
        		dirString = "<DIR>";
        		dirCount++;
        	}
        	if (fileName.isFile())
        	{
        		dirString = "";
        		fileCount++;
        	}
            System.out.println(getLastModified(fileName) + "\t" + dirString + "\t" + fileName.getName());
        }
        System.out.println("\t\t" + fileCount + "개 파일\t\t" + file.length() + " 바이트");
		System.out.println("\t\t" + dirCount + "개 디렉터리 " + file.getUsableSpace() + " 바이트 남음");
	}
	
	private String getLastModified(File file)
	{
        SimpleDateFormat simpleDateFormat = new SimpleDateFormat("yyyy-MM-dd a hh:mm");
        return simpleDateFormat.format(file.lastModified());
	}
	
	
}
