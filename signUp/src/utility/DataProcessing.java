package utility;

import java.util.ArrayList;

public class DataProcessing 
{
	private static final DataProcessing dataProcessing = new DataProcessing();
	
	public static final DataProcessing get()
	{
		return dataProcessing;
	}
	
	public boolean isRegisteredId(ArrayList<String> userIdList, String userId) // 사용자가 입력한 아이디가 이미 등록되어있는지 체크
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
	
	public boolean isValidateInputData(String inputData, String regexForm) //정규식으로 입력된 값들이 유효한지 검사
	{
		if (inputData == null)
			return false;
		if (inputData.matches(regexForm))
			return true;
		return false;
	}
	
	public ArrayList<String> getUserInputDataList(view.panel.SignUp signUpPanel) // 회원가입시 유저가 입력한 정보들의 리스트 반환
	{
		ArrayList<String> userInputDataList = new ArrayList<String>();
		
		String userId = signUpPanel.idFiled.getText();
		String userName = signUpPanel.nameFiled.getText();
		@SuppressWarnings("deprecation")
		String userPassword = signUpPanel.passwordFiled.getText();
		String userBirth = String.format("%s%02d%02d", signUpPanel.birthYear.getSelectedItem().toString() 
													 , Integer.parseInt(signUpPanel.birthMonth.getSelectedItem().toString())
													 , Integer.parseInt(signUpPanel.birthDay.getSelectedItem().toString()));
		String userEmail = signUpPanel.emailFiled.getText() + "@" + signUpPanel.lastEmailFiled.getText();
		String userPhoneNumber = signUpPanel.firstPhoneNumber.getSelectedItem().toString()
								 + signUpPanel.middlePhoneNumberFiled.getText()
								 + signUpPanel.lastPhoneNumberFiled.getText();
		String userAddress = signUpPanel.addressFiled.getText();
		
		userInputDataList.add(userId);
		userInputDataList.add(userName);
		userInputDataList.add(userPassword);
		userInputDataList.add(userBirth);
		userInputDataList.add(userEmail);
		userInputDataList.add(userPhoneNumber);
		userInputDataList.add(userAddress);
		
		return userInputDataList;
	}
	
	
	public ArrayList<String> getUserInputDataList(view.panel.Edit editPanel) // 수정창에 입력되어있는 유저의 데이터 반환
	{
		ArrayList<String> userInputDataList = new ArrayList<String>();
		
		String userId = editPanel.idFiled.getText();
		String userName = editPanel.nameFiled.getText();
		@SuppressWarnings("deprecation")
		String userPassword = editPanel.passwordFiled.getText();
		String userBirth = String.format("%s%02d%02d", editPanel.birthYear.getSelectedItem().toString() 
													 , Integer.parseInt(editPanel.birthMonth.getSelectedItem().toString())
													 , Integer.parseInt(editPanel.birthDay.getSelectedItem().toString()));
		String userEmail = editPanel.emailFiled.getText() + "@" + editPanel.lastEmailFiled.getText();
		String userPhoneNumber = editPanel.firstPhoneNumber.getSelectedItem().toString()
								 + editPanel.middlePhoneNumberFiled.getText()
								 + editPanel.lastPhoneNumberFiled.getText();
		String userAddress = editPanel.addressFiled.getText();
		
		userInputDataList.add(userId);
		userInputDataList.add(userName);
		userInputDataList.add(userPassword);
		userInputDataList.add(userBirth);
		userInputDataList.add(userEmail);
		userInputDataList.add(userPhoneNumber);
		userInputDataList.add(userAddress);
		
		return userInputDataList;
	}
	
	public ArrayList<String> getRegexList() // id, name, pw, birth, email, phoneNumber, address 순서대로 해당 정규식을 리스트에 추가하여 반환
	{
		ArrayList<String> regexList = new ArrayList<String>();
		
		regexList.add(Constant.REGEX_PATTERN_ID);
		regexList.add(Constant.REGEX_PATTERN_NAME);
		regexList.add(Constant.REGEX_PATTERN_PASSWORD);
		regexList.add(Constant.REGEX_PATTERN_ANY);
		regexList.add(Constant.REGEX_PATTERN_EMAIL);
		regexList.add(Constant.REGEX_PATTERN_PHONE_NUMBER); 
		regexList.add(Constant.REGEX_PATTERN_ADDRESS);
		
		return regexList;
	}
	
	public boolean isAllInputDataValidate(ArrayList<String> userInputDataList) // 입력되어있는 모든 데이터들이 올바른 형식으로 입력되었는지 체크
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
