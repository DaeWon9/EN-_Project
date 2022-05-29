package view;

import java.io.BufferedReader;
import java.io.File;
import java.io.InputStreamReader;
import java.util.ArrayList;

import utility.DataProcessing;

public class CmdView extends Message
{	
	public void printDirCommandResult(File file, String filePath, ArrayList<File> fileArrayList)
	{
		String fileNameString = "", dirString = "", fileLengthString = "";
		Long dirCount = (long)0, fileCount = (long)0, fileLengthSum = (long)0;	
		if (file == null)
			return;
        printDirCommandLabel(filePath); // 상단부 라벨 출력
        for (File fileName : fileArrayList) 
        {
        	if (fileName.getPath().equals(filePath))
        		fileNameString = ".";
        	else if (fileName.getPath().equals(DataProcessing.get().moveUpPathStage(filePath, 1)))
        		fileNameString = "..";
        	else
        		fileNameString = fileName.getName();
        	
        	if (fileName.isDirectory())
        	{
        		dirString = "<DIR>";
        		fileLengthString = "";
        		dirCount++;
        	}
        	else if (fileName.isFile())
        	{
        		dirString = "";
        		fileLengthString = DataProcessing.get().appendComma(Long.toString(fileName.length()));
        		fileLengthSum = fileLengthSum + fileName.length();
        		fileCount++;
        	}
        	System.out.println(String.format("%s\t%s\t%8s %s", DataProcessing.get().getLastModified(fileName), dirString, fileLengthString, fileNameString));
        }
        System.out.println("\t\t" + fileCount + "개 파일\t\t" + DataProcessing.get().appendComma(Long.toString(fileLengthSum)) + " 바이트");
		System.out.println("\t\t" + dirCount + "개 디렉터리 " + DataProcessing.get().appendComma(Long.toString(file.getUsableSpace())) + " 바이트 남음");
	}
	
	
	private void printDirCommandLabel(String filePath)
	{
		printDiskVolumnId(filePath);
		System.out.println(String.format(" %s 디렉터리\n", filePath));
	}
	
	private void printDiskVolumnId(String filePath)
	{
		try
		{
		    Process process = Runtime.getRuntime().exec("cmd /c dir " + filePath);
		    BufferedReader reader = new BufferedReader(new InputStreamReader(process.getInputStream(),"MS949"));
		    System.out.println(reader.readLine());
		    System.out.println(reader.readLine() + "\n");
		    reader.close();
		    process.destroy();
		}
		catch(Exception e)
		{
			e.printStackTrace();
		}
	}
}