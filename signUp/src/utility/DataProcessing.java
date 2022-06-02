package utility;

import java.util.ArrayList;
import java.util.Scanner;

public class DataProcessing 
{
	private static final DataProcessing dataProcessing = new DataProcessing();
	
	public static final DataProcessing get()
	{
		return dataProcessing;
	}
	
	public boolean isRegisteredId(ArrayList<String> userIdList, String userId)
	{	
		for (int repeat = 0; repeat < userIdList.size(); repeat++)
		{
			if (userIdList.get(repeat).equals(userId))
			{
				return true;
			}
		}
		return false;
	}
}
