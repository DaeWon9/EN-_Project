package view;

import java.io.File;
import java.io.FileFilter;

import utility.DataProcessing;

public class CmdView extends Message
{
	public void printDirCommandResult(File file, String filePath)
	{
		String dirString = "";
		int dirCount = 0, fileCount = 0;
		FileFilter fileFilter = new FileFilter()
        {
            @Override
            public boolean accept(File entry)
            {
                if (entry.isHidden()) return false;
                return true;
            }
        };
		if (file != null)
		{
			printDirCommandLabel(filePath);
	        File[] fileList = file.listFiles(fileFilter);
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
	
	private void printDirCommandLabel(String filePath)
	{
		System.out.println(" C 드라이브의 볼륨에는 이름이 없습니다."); // C드라이브 말고 다른경우 처리하기
		System.out.println(String.format(" 볼륨 일련 번호: %s\n", "D666D-E41B")); //볼륨번호 따로 구해서 넣기
		System.out.println(String.format(" %s 디렉터리\n", filePath));
	}
}