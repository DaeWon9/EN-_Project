package utility;

import java.io.File;
import java.math.BigDecimal;
import java.text.DecimalFormat;
import java.text.NumberFormat;
import java.text.SimpleDateFormat;
import java.util.Scanner;
import java.util.regex.Pattern;

import model.UserPath;

public class DataProcessing 
{
	private static final DataProcessing dataProcessing = new DataProcessing();
	
	public static final DataProcessing get()
	{
		return dataProcessing;
	}
	
	public String getInputString()
	{
		String inputString = "";
		Scanner scanner = new Scanner(System.in);
		inputString = scanner.nextLine();
		return inputString;
	}
	
	public String appendComma(String numberString)
	{
		if (Pattern.matches(Constant.REGEX_PATTERN_NUMBER, numberString))
		{
			BigDecimal bigDeciaml = new BigDecimal(numberString);
			DecimalFormat decimalFormat = new DecimalFormat(",###");
			return decimalFormat.format(bigDeciaml);
		}
		return numberString;
	}
	
	public int countChar(String targetString, char targetChar)
	{
		int count = 0;
		for (int index = 0; index < targetString.length(); index++)
		{
			if (targetString.charAt(index) == targetChar)
				count++;
		}
		return count;
	}
	
	public String mergePath(String[] splitedPath, int lastIndex)
	{			
		StringBuilder mergedPath = new StringBuilder();
		mergedPath.append(splitedPath[0]);
		for (int index = 1; index < lastIndex; index++)
		{
			mergedPath.append("\\");
			mergedPath.append(splitedPath[index]);
		}
		return mergedPath.toString();
	}
	
	public boolean isValidPath(String inputPath)
	{
		File file = new File(inputPath);
		return file.exists();
	}
	
	public String getLastModified(File file)
	{
        SimpleDateFormat simpleDateFormat = new SimpleDateFormat("yyyy-MM-dd  a hh:mm");
        return simpleDateFormat.format(file.lastModified());
	}
	
	public String moveUpPathStage(String path, int stage)
	{
		String mergedPath;
		String[] splitedPath = path.split("\\\\");
		int pathLenght = DataProcessing.get().countChar(path, '\\');
		mergedPath = DataProcessing.get().mergePath(splitedPath, pathLenght + 1 - stage);
		if (mergedPath.length() < 3)
			mergedPath  = mergedPath + "\\";
		return mergedPath;
	}	
}
