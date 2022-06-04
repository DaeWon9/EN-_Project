package controller;

import java.util.ArrayList;
import model.UserData;
import utility.Constant;
import utility.DataProcessing;

public class Editer 
{
	private view.panel.Edit editPanel;
	private UserData userData;

	private int editCount;
	
	public Editer(view.panel.Edit editPanel, UserData userData)
	{
		this.editPanel = editPanel; 
		this.userData = userData;
	}
	
	public void action(String loginedUserId)
	{
		ArrayList<String> userInputDataList = DataProcessing.get().getUserInputDataList(editPanel); 
		
		if (!DataProcessing.get().isAllInputDataValidate(userInputDataList))
		{
			return;
		}
		editCount = 0;
		editName(loginedUserId);
		editPassword(loginedUserId);
		editBirth(loginedUserId);
		editEmail(loginedUserId);
		editPhoneNumber(loginedUserId);
		editAddress(loginedUserId);
	}
	
	private void editName(String loginedUserId)
	{
		String loginedUserName = userData.getLoginedUserDataList(loginedUserId).get(0); // 로그인되어있는 유저 아이디 받아오기
		if (!loginedUserName.equals(editPanel.nameFiled.getText())) // 정보의 변동사항이 있다면
		{
			userData.updateUserData(editPanel.nameFiled.getText(), loginedUserId, Constant.USER_FIELD_NAME); // 업데이트 해주기
			editCount++;
		}
	}
	
	@SuppressWarnings("deprecation")
	private void editPassword(String loginedUserId)
	{
		String loginedUserPassword = userData.getLoginedUserDataList(loginedUserId).get(2); // 로그인되어있는 유저 비밀번호 받아오기
		if (!loginedUserPassword.equals(editPanel.passwordFiled.getText())) // 정보의 변동사항이 있다면
		{
			if (editPanel.passwordFiled.getText().equals(editPanel.passwordCheckFiled.getText())) // 비밀번호와, 비밀번호 체크가 같을경우
			{		
				userData.updateUserData(editPanel.passwordFiled.getText(), loginedUserId, Constant.USER_FIELD_PASSWORD); // 업데이트 해주기
				editCount++;
			}
		}
	}
	
	private void editBirth(String loginedUserId)
	{
		String loginedUserBirth = userData.getLoginedUserDataList(loginedUserId).get(4); // 로그인되어있는 유저 생년월일 받아오기
		String editedUserBirth = String.format("%s%02d%02d", editPanel.birthYear.getSelectedItem().toString()
														   , Integer.parseInt(editPanel.birthMonth.getSelectedItem().toString())
														   , Integer.parseInt(editPanel.birthDay.getSelectedItem().toString())); // 수정된 생년월일 정보 
		if (!loginedUserBirth.equals(editedUserBirth)) // 정보의 변동사항이 있다면
		{
			userData.updateUserData(editedUserBirth, loginedUserId, Constant.USER_FIELD_BIRTH); // 업데이트 해주기
			editCount++;
		}
	}
	
	private void editEmail(String loginedUserId)
	{
		String loginedUserEmail = userData.getLoginedUserDataList(loginedUserId).get(5); // 로그인되어있는 유저 이메일 받아오기
		String editedUserEmail = editPanel.emailFiled.getText() + "@" + editPanel.lastEmailFiled.getText();
		if (!loginedUserEmail.equals(editedUserEmail)) // 정보의 변동사항이 있다면
		{
			userData.updateUserData(editedUserEmail, loginedUserId, Constant.USER_FIELD_EMAIL); // 업데이트 해주기
			editCount++;
		}
	}
	
	private void editPhoneNumber(String loginedUserId)
	{
		String loginedUserPhoneNumber = userData.getLoginedUserDataList(loginedUserId).get(6); // 로그인되어있는 유저 핸드폰 번호 받아오기
		String editedUserPhoneNumber = editPanel.firstPhoneNumber.getSelectedItem().toString()
				 						+ editPanel.middlePhoneNumberFiled.getText()
				 						+ editPanel.lastPhoneNumberFiled.getText(); // 수정된 핸드폰 번호
		if (!loginedUserPhoneNumber.equals(editedUserPhoneNumber)) // 정보의 변동사항이 있다면
		{
			userData.updateUserData(editedUserPhoneNumber, loginedUserId, Constant.USER_FIELD_PHONE_NUMBER); // 업데이트 해주기
			editCount++;
		}
	}
	
	private void editAddress(String loginedUserId)
	{
		String loginedUserAddress = userData.getLoginedUserDataList(loginedUserId).get(7); // 로그인되어있는 유저 주소 받아오기
		if (!loginedUserAddress.equals(editPanel.addressFiled.getText())) // 정보의 변동사항이 있다면
		{
			userData.updateUserData(editPanel.addressFiled.getText(), loginedUserId, Constant.USER_FIELD_ADDRESS); // 업데이트 해주기
			editCount++;
		}
	}
	
	public boolean isEditExecuted()
	{
		if (editCount > 0)
			return true;
		return false;
	}
}
