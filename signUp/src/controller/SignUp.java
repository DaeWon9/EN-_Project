package controller;

import java.util.ArrayList;

import model.UserData;
import utility.DataProcessing;

public class SignUp 
{
	private view.panel.SignUp signUpPanel;
	private UserData userData;
	
	public SignUp(view.panel.SignUp signUpPanel, UserData userData)
	{
		this.signUpPanel = signUpPanel; 
		this.userData = userData;
	}
	
	public void action()
	{
		ArrayList<String> userInputDataList = DataProcessing.get().getUserInputDataList(signUpPanel); 
		if(isAllDataValidate(userInputDataList));
		{
			userData.insertUserData(userInputDataList);
			System.out.println("회원가입 완료");
		}
	}
	
	private boolean isAllDataValidate(ArrayList<String> userInputDataList)
	{
		ArrayList<String> regexFormList = DataProcessing.get().getRegexList();
		
		for (int index = 0;  index < userInputDataList.size(); index++)
		{
			if (!DataProcessing.get().isValidateInputData(userInputDataList.get(index), regexFormList.get(index)))
			{
				return false;
			}
		}
		return true;
	}
}
