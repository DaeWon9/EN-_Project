package view;

import java.io.File;

import utility.DataProcessing;

public class CmdView extends Message
{
	public void printDirCommandResult(File file)
	{
		String dirString = "";
		int dirCount = 0, fileCount = 0;
		if (file != null)
		{
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
	            System.out.println(DataProcessing.get().getLastModified(fileName) + "\t" + dirString + "\t" + fileName.getName());
	        }
	        System.out.println("\t\t" + fileCount + "개 파일\t\t" + file.length() + " 바이트");
			System.out.println("\t\t" + dirCount + "개 디렉터리 " + file.getUsableSpace() + " 바이트 남음");
		}
	}
}