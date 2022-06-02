package view.panel;

import java.awt.Image;
import java.util.ArrayList;

import javax.swing.ImageIcon;
import javax.swing.JButton;
import javax.swing.JComboBox;
import javax.swing.JLabel;
import javax.swing.JPanel;
import javax.swing.JTextField;

public class SignUp extends JPanel
{
	public JButton idCheckButton, signUpButton, backStageButton;
	public JTextField nameFiled, idFiled, passwordFiled, passwordCheckFiled, emailFiled, lastEmailFiled, middlePhoneNumberFiled, lastPhoneNumberFiled, addressFiled;
	public JComboBox<String> birthYear, birthMonth, birthDay, firstPhoneNumber;
	public SignUp()
	{
		setLayout(null);	
		// 이름 텍스트필드 설정
		nameFiled = new JTextField();
		nameFiled.setSize(201,30);
		nameFiled.setLocation(184,28);
		add(nameFiled);
		
		// 아이디 텍스트필드 설정
		idFiled = new JTextField();
		idFiled.setSize(201,30);
		idFiled.setLocation(184,84);
		add(idFiled);
		
		// 비밀번호 텍스트필드 설정
		passwordFiled = new JTextField();
		passwordFiled.setSize(201,30);
		passwordFiled.setLocation(184,141);
		add(passwordFiled);
		
		// 비밀번호 체크 텍스트필드 설정
		passwordCheckFiled = new JTextField();
		passwordCheckFiled.setSize(201,30);
		passwordCheckFiled.setLocation(184,198);
		add(passwordCheckFiled);
		
		// 생년월일 필드 설정 -> 콤보박스로
		ArrayList<String> yearList = new ArrayList<String>();
		for (int year = 2022; year >= 1900; year--)
		{
			yearList.add(Integer.toString(year));
		}
		birthYear = new JComboBox<String>(yearList.toArray(new String[yearList .size()]));
		birthYear.setSize(63,30);
		birthYear.setLocation(184, 257);
        add(birthYear);
        
        // month
		ArrayList<String> monthList = new ArrayList<String>();
		for (int month = 1; month <= 12; month++)
		{
			monthList.add(Integer.toString(month));
		}
		birthMonth = new JComboBox<String>(monthList.toArray(new String[monthList .size()]));
		birthMonth.setSize(63,30);
		birthMonth.setLocation(253, 257);
        add(birthMonth);
        
		// day
		ArrayList<String> dayList = new ArrayList<String>();
		/*
		int selectedMonth = Integer.parseInt(birthMonth.getSelectedItem().toString());
		int maxDay;
		switch (selectedMonth)
		{
			case 2:
				maxDay = 28;
				break;
			case 4:
			case 6:
			case 9:
			case 11:
				maxDay = 30;
				break;
			default:
				maxDay = 31;
				break;
		}
		 */
		for (int day = 1; day <= 31; day++)
		{
			dayList.add(Integer.toString(day));
		}
		birthDay = new JComboBox<String>(dayList.toArray(new String[dayList .size()]));
		birthDay.setSize(63,30);
		birthDay.setLocation(322, 257);
        add(birthDay);
        
        // 이메일 필드 설정
        emailFiled = new JTextField();
        emailFiled.setSize(201,30);
        emailFiled.setLocation(184,301);
		add(emailFiled);
		
        // @ 뒤에 이메일 필드 설정
		lastEmailFiled = new JTextField();
		lastEmailFiled.setSize(102,30);
		lastEmailFiled.setLocation(410,301);
		add(lastEmailFiled);
		
		// 핸드폰번호 필드 설정 (첫번째는 콤보박스로)
		String[] firstPhoneNumberList ={"010", "011", "016"};
		firstPhoneNumber = new JComboBox<String>(firstPhoneNumberList);
		firstPhoneNumber.setSize(63,30);
		firstPhoneNumber.setLocation(184, 345);
        add(firstPhoneNumber);
        
        // 두번째 핸드폰번호 필드 설정
		middlePhoneNumberFiled = new JTextField();
		middlePhoneNumberFiled.setSize(63,30);
		middlePhoneNumberFiled.setLocation(253,345);
		add(middlePhoneNumberFiled);
		
        // 세번째 핸드폰번호 필드 설정
		lastPhoneNumberFiled = new JTextField();
		lastPhoneNumberFiled.setSize(63,30);
		lastPhoneNumberFiled.setLocation(322,345);
		add(lastPhoneNumberFiled);
		
        // 주소 필드 설정
		addressFiled = new JTextField();
		addressFiled.setSize(327,30);
		addressFiled.setLocation(184,391);
		add(addressFiled);
		
		// 뒤로가기 버튼 설정
		ImageIcon backStageButtonImage = new ImageIcon(SignUp.class.getResource("/image/backStage.png"));		
		backStageButton = new JButton(backStageButtonImage);
		backStageButton.setBorderPainted(false);
		backStageButton.setSize(backStageButtonImage.getIconWidth(), backStageButtonImage.getIconHeight());
		backStageButton.setLocation(750, 4);
		backStageButton.setBorderPainted(false);
		backStageButton.setFocusPainted(false); 
		backStageButton.setContentAreaFilled(false);
		add(backStageButton);
		// 아이디 체크 버튼 설정
		ImageIcon idCheckButtonImage = new ImageIcon(SignUp.class.getResource("/image/idCheckButton.png"));		
		idCheckButton = new JButton(idCheckButtonImage);
		idCheckButton.setBorderPainted(false);
		idCheckButton.setSize(idCheckButtonImage.getIconWidth(), idCheckButtonImage.getIconHeight());
		idCheckButton.setLocation(396, 84);
		idCheckButton.setBorderPainted(false);
		idCheckButton.setFocusPainted(false); 
		idCheckButton.setContentAreaFilled(false);
		add(idCheckButton);
		// 회원가입 버튼 설정
		ImageIcon signUpButtonImage = new ImageIcon(SignUp.class.getResource("/image/signUpButton_2.png"));		
		signUpButton = new JButton(signUpButtonImage);
		signUpButton.setBorderPainted(false);
		signUpButton.setSize(signUpButtonImage.getIconWidth(), signUpButtonImage.getIconHeight());
		signUpButton.setLocation(389, 453);
		signUpButton.setBorderPainted(false);
		signUpButton.setFocusPainted(false); 
		signUpButton.setContentAreaFilled(false);
		add(signUpButton);
		// 회원가입 배경이미지 설정
		ImageIcon imageIcon = new ImageIcon(Login.class.getResource("/image/signUpScreen.jpg"));
		Image image = imageIcon.getImage().getScaledInstance(800, 500, Image.SCALE_SMOOTH);
		JLabel backgroundImage = new JLabel(new ImageIcon(image));
		backgroundImage.setBounds(0,0,800,500);
		add(backgroundImage);	
	}
}
