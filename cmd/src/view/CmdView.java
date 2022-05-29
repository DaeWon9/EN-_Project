package view;

import java.io.File;
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
        	System.out.println(String.format("%s\t%s\t%6s %s", DataProcessing.get().getLastModified(fileName), dirString, fileLengthString, fileNameString));
        }
        System.out.println("\t\t" + fileCount + "개 파일\t\t" + DataProcessing.get().appendComma(Long.toString(fileLengthSum)) + " 바이트");
		System.out.println("\t\t" + dirCount + "개 디렉터리 " + DataProcessing.get().appendComma(Long.toString(file.getUsableSpace())) + " 바이트 남음");
		
	}
	
	private void printDirCommandLabel(String filePath)
	{
		System.out.println(" C 드라이브의 볼륨에는 이름이 없습니다."); // C드라이브 말고 다른경우 처리하기
		System.out.println(String.format(" 볼륨 일련 번호: %s\n", "D666D-E41B")); //볼륨번호 따로 구해서 넣기
		System.out.println(String.format(" %s 디렉터리\n", filePath));
	}
}