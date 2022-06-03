package controller;

import java.util.ArrayList;

import javax.swing.ImageIcon;
import javax.swing.JOptionPane;

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
		ImageIcon okRyanIcon = new ImageIcon(MainController.class.getResource("/image/okRyan.png"));	
		ImageIcon noApeachIcon = new ImageIcon(MainController.class.getResource("/image/noApeach.png"));
		ArrayList<String> userInputDataList = DataProcessing.get().getUserInputDataList(signUpPanel); 
		
		if (!signUpPanel.isIdCheck)
		{
			JOptionPane.showMessageDialog(null, "ID 체크를 진행해주세요","회원가입",JOptionPane.DEFAULT_OPTION, noApeachIcon);
		}
		else if (!signUpPanel.isPasswordCheck)
		{
			JOptionPane.showMessageDialog(null, "PW 체크가 일치하지 않습니다","회원가입",JOptionPane.DEFAULT_OPTION, noApeachIcon);
		}
		else if(isAllDataValidate(userInputDataList))
		{
			userData.insertUserData(userInputDataList);
			JOptionPane.showMessageDialog(null, "회원가입 완료!!","회원가입",JOptionPane.DEFAULT_OPTION, okRyanIcon);
		}
		else
		{
			JOptionPane.showMessageDialog(null, "모든데이터를 형식에 맞게 입력해주세요","회원가입",JOptionPane.DEFAULT_OPTION, noApeachIcon);
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
