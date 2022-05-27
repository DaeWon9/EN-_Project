package controller.command;

import java.io.File;
import java.text.SimpleDateFormat;

import controller.CmdAction;
import model.UserPath;

public class Dir implements CmdAction
{
	private UserPath userPath;
	public Dir(UserPath userPath)
	{
		this.userPath = userPath;
	}
	
	@Override
	public void actionCommand(String inputCommand)
	{
		getFileList();
	}
	
	private void getDirCommandResult()
	{
		
	}
	
	
	private void getFileList()
	{
		int DirCount = 0,fileCount = 0;
        String DirString = "";
        File[] fileList;
        File file = new File(userPath.get());
        fileList = file.listFiles();
        for (File fileName : fileList) 
        {
        	if (fileName.isDirectory())
        	{
        		DirString = "<DIR>";
        		DirCount++;
        	}
        	if (fileName.isFile())
        	{
        		DirString = "";
        		fileCount++;
        	}
            System.out.println(getLastModified(file) + "\t" + DirString + "\t" + fileName.getName());
        }
        System.out.println("\t\t" + fileCount + "개 파일\t\t" + file.length() + "바이트");
		System.out.println("\t\t" + DirCount + "개 디렉터리 " + file.getUsableSpace() + "바이트 남음");
	}
	
	private String getLastModified(File file)
	{
        SimpleDateFormat simpleDateFormat = new SimpleDateFormat("yyyy-MM-dd a hh:mm");
        return simpleDateFormat.format(file.lastModified());
	}
	
	
}
