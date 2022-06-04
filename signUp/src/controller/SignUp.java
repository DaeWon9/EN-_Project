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
	
	public boolean isAction()
	{
		ImageIcon okRyanIcon = new ImageIcon(MainController.class.getResource("/image/okRyan.png"));	
		ImageIcon noApeachIcon = new ImageIcon(MainController.class.getResource("/image/noApeach.png"));
		ArrayList<String> userInputDataList = DataProcessing.get().getUserInputDataList(signUpPanel); 
		
		if (!signUpPanel.isIdCheck)
		{
			JOptionPane.showMessageDialog(null, "ID 체크를 진행해주세요", "EN# FRIENDS", JOptionPane.DEFAULT_OPTION, noApeachIcon);
		}
		else if (!signUpPanel.isPasswordCheck)
		{
			JOptionPane.showMessageDialog(null, "PW 체크가 일치하지 않습니다", "EN# FRIENDS", JOptionPane.DEFAULT_OPTION, noApeachIcon);
		}
		else if(DataProcessing.get().isAllInputDataValidate(userInputDataList))
		{
			userData.insertUserData(userInputDataList);
			JOptionPane.showMessageDialog(null, "회원가입 완료!!", "EN# FRIENDS", JOptionPane.DEFAULT_OPTION, okRyanIcon);
			return true;
		}
		else
		{
			JOptionPane.showMessageDialog(null, "모든데이터를 형식에 맞게 입력해주세요", "EN# FRIENDS", JOptionPane.DEFAULT_OPTION, noApeachIcon);
		}
		return false;
	}

}
