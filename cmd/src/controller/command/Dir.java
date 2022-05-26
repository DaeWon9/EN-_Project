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
	public void actionCommand()
	{
		getFileList();
	}
	
	private void getDirCommandResult()
	{
		
	}
	
	
	private void getFileList()
	{
		File file = new File(userPath.get());
		/*
        String[] fileList;
        File file = new File(userPath.get());
        fileList = file.list();
        for (String fileName : fileList) {
            System.out.println(fileName);
        }
        */

		
		//System.out.println(file.getFreeSpace());
		//System.out.println(file.getUsableSpace());
		//System.out.println(file.lastModified());
		System.out.println(file.isDirectory());
	}
	
	private void getLastModified(File file)
	{
        SimpleDateFormat simpleDateFormat = new SimpleDateFormat("yyyy-MM-dd a hh:mm");
        System.out.println(simpleDateFormat.format(file.lastModified()));
	}
	
	
}
