package controller;

import model.UserData;
import utility.DataProcessing;

public class Login 
{
	private view.panel.SignUp signUpPanel;
	private UserData userData;
	
	public Login(view.panel.SignUp signUpPanel, UserData userData)
	{
		this.signUpPanel = signUpPanel; 
		this.userData = userData;
	}
	
	public boolean isLoginSucess(String userId, String userPassword)
	{
		if (DataProcessing.get().isRegisteredId(userData.getUserIdList(), userId)) // 등록되어있는 아이디
		{
			if (userData.getUserPassword(userData, userId).equals(userPassword))
				return true;
		}
		return false;
	}
}
