package view.panel;

import java.awt.Color;
import java.awt.Image;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.awt.event.KeyEvent;
import java.awt.event.KeyListener;
import java.util.ArrayList;
import javax.swing.ImageIcon;
import javax.swing.JButton;
import javax.swing.JComboBox;
import javax.swing.JLabel;
import javax.swing.JPanel;
import javax.swing.JPasswordField;
import javax.swing.JTextField;
import utility.Constant;
import view.ImageButton;
import view.LimitedJTextField;

public class Edit extends JPanel
{
	public JButton editButton, backStageButton;
	public JPasswordField passwordFiled, passwordCheckFiled;
	public JTextField nameFiled, idFiled, emailFiled, lastEmailFiled, addressFiled, middlePhoneNumberFiled, lastPhoneNumberFiled;
	public JComboBox<String> birthYear, birthMonth, birthDay, lastEmailComboBox, firstPhoneNumber;
	public JButton passwordInvisibleButton, passwordVisibleButton, passwordCheckInvisibleButton, passwordCheckVisibleButton;
	public boolean isPasswordCheck = false;
	
	public Edit()
	{
		setLayout(null);	
	
		// 이름 텍스트필드 설정
		nameFiled = new LimitedJTextField(Constant.REGEX_PATTERN_NAME, 10).get();
		nameFiled.setBounds(184, 28, 201, 30);
		add(nameFiled);
		
		// 아이디 텍스트필드 설정
		idFiled = new LimitedJTextField(Constant.REGEX_PATTERN_ID, 16).get();
		idFiled.setBounds(184, 84, 201, 30);
		idFiled.setEditable(false);
		add(idFiled);
		
		// 비밀번호 텍스트필드 설정
		passwordFiled = new JPasswordField();
		passwordFiled.setBounds(184, 141, 201, 30);
		passwordFiled.setEchoChar('\u25cf');
		passwordFiled.addKeyListener(new KeyListener() {
			@SuppressWarnings("deprecation")
			@Override
			public void keyTyped(KeyEvent e) {
				if(passwordFiled.getText().length() >= 16)
				{
					e.consume();
				}
			}
			
			@Override
			public void keyReleased(KeyEvent e) {
				if(passwordFiled.getText().matches(Constant.REGEX_PATTERN_PASSWORD))
				{
					passwordFiled.setForeground(Color.BLUE);
				}
				else
				{
					passwordFiled.setForeground(Color.RED);
				}
				
				// pw & pwCheck 가 같은지 확인
				if(passwordCheckFiled.getText().equals(passwordFiled.getText()))
				{
					passwordCheckFiled.setForeground(Color.BLUE);
					isPasswordCheck = true;
				}
				else
				{
					passwordCheckFiled.setForeground(Color.RED);
					isPasswordCheck = false;
				}
			}
			
			@Override
			public void keyPressed(KeyEvent e) {
				// TODO Auto-generated method stub
				
			}
		});
		add(passwordFiled);
		
		// 비밀번호 visible 버튼
		ImageIcon passwordVisibleButtonImage = new ImageIcon(SignUp.class.getResource("/image/visible.png"));		
		passwordVisibleButton = new ImageButton(passwordVisibleButtonImage, null, 394, 147).get();
		passwordVisibleButton.addActionListener(new ActionListener() 
		{
			@Override
			public void actionPerformed(ActionEvent e) 
			{
				passwordFiled.setEchoChar((char)0);
				passwordVisibleButton.setVisible(false);
				passwordInvisibleButton.setVisible(true);
			}
		});
		add(passwordVisibleButton);		
		
		ImageIcon passwordInvisibleButtonImage = new ImageIcon(SignUp.class.getResource("/image/invisible.png"));		
		passwordInvisibleButton = new ImageButton(passwordInvisibleButtonImage, null, 394, 147).get();
		passwordInvisibleButton.setVisible(false);
		passwordInvisibleButton.addActionListener(new ActionListener() 
		{
			@Override
			public void actionPerformed(ActionEvent e) 
			{
				passwordFiled.setEchoChar('\u25cf');
				passwordInvisibleButton.setVisible(false);
				passwordVisibleButton.setVisible(true);
			}
		});
		add(passwordInvisibleButton);		
		
		
		// 비밀번호 체크 텍스트필드 설정
		passwordCheckFiled = new JPasswordField();
		passwordCheckFiled.setBounds(184, 198, 201, 30);
		passwordCheckFiled.setEchoChar('\u25cf');
		passwordCheckFiled.addKeyListener(new KeyListener() {
			@SuppressWarnings("deprecation")
			@Override
			public void keyTyped(KeyEvent e) {
				if(passwordCheckFiled.getText().length() >= 16)
				{
					e.consume();
				}
			}
		
			@Override
			public void keyReleased(KeyEvent e) {
				if(passwordCheckFiled.getText().equals(passwordFiled.getText()))
				{
					passwordCheckFiled.setForeground(Color.BLUE);
					isPasswordCheck = true;
				}
				else
				{
					passwordCheckFiled.setForeground(Color.RED);
					isPasswordCheck = false;
				}
			}
			
			@Override
			public void keyPressed(KeyEvent e) {}
		});
		add(passwordCheckFiled);
		
		// 비밀번호 체크 visible 버튼	
		passwordCheckVisibleButton = new ImageButton(passwordVisibleButtonImage, null, 394, 204).get();
		passwordCheckVisibleButton.addActionListener(new ActionListener() 
		{
			@Override
			public void actionPerformed(ActionEvent e) 
			{
				passwordCheckFiled.setEchoChar((char)0);
				passwordCheckVisibleButton.setVisible(false);
				passwordCheckInvisibleButton.setVisible(true);
			}
		});
		add(passwordCheckVisibleButton);		
	
		passwordCheckInvisibleButton = new ImageButton(passwordInvisibleButtonImage, null, 394, 204).get();
		passwordCheckInvisibleButton.setVisible(false);
		passwordCheckInvisibleButton.addActionListener(new ActionListener() 
		{
			@Override
			public void actionPerformed(ActionEvent e) 
			{
				passwordCheckFiled.setEchoChar('\u25cf');
				passwordCheckInvisibleButton.setVisible(false);
				passwordCheckVisibleButton.setVisible(true);
			}
		});
		add(passwordCheckInvisibleButton);
		
		// 생년월일 필드 설정 -> 콤보박스로
		ArrayList<String> yearList = new ArrayList<String>();
		for (int year = 2022; year >= 1900; year--)
		{
			yearList.add(Integer.toString(year));
		}
		birthYear = new JComboBox<String>(yearList.toArray(new String[yearList .size()]));
		birthYear.setBounds(184, 257, 63, 30);
        add(birthYear);
        
        // month
		ArrayList<String> monthList = new ArrayList<String>();
		for (int month = 1; month <= 12; month++)
		{
			monthList.add(String.format("%02d",month));
		}
		birthMonth = new JComboBox<String>(monthList.toArray(new String[monthList .size()]));
		birthMonth.setBounds(253, 257, 63, 30);
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
			dayList.add(String.format("%02d",day));
		}
		birthDay = new JComboBox<String>(dayList.toArray(new String[dayList .size()]));
		birthDay.setBounds(322, 257, 63, 30);
        add(birthDay);
        
        // 이메일 필드 설정
        emailFiled = new LimitedJTextField(Constant.REGEX_PATTERN_ID, 30).get();
        emailFiled.setBounds(184, 301, 132, 30);
		add(emailFiled);
		
        // @ 뒤에 이메일 필드 설정
		lastEmailFiled = new LimitedJTextField(Constant.REGEX_PATTERN_LAST_EMAIL, 15).get();
		lastEmailFiled.setBounds(344, 301, 75, 30);
		add(lastEmailFiled);
		
		// @ 뒤에 이메일 필드 설정 (콤보박스)
		String[] emailList ={"", "naver.com", "daum.net", "gmail.com", "hanmail.net", "nate.com"};
		lastEmailComboBox = new JComboBox<String>(emailList);
		lastEmailComboBox.setBounds(419, 301, 95, 30);
		lastEmailComboBox.setFocusable(false);
		lastEmailComboBox.addActionListener(new ActionListener() {
			@Override
			public void actionPerformed(ActionEvent e) {
				lastEmailFiled.setText(lastEmailComboBox.getSelectedItem().toString());
			}
		});
        add(lastEmailComboBox);
		
		// 핸드폰번호 필드 설정 (첫번째는 콤보박스로)
		String[] firstPhoneNumberList ={"010", "011", "016"};
		firstPhoneNumber = new JComboBox<String>(firstPhoneNumberList);
		firstPhoneNumber.setBounds(184, 345, 63, 30);
        add(firstPhoneNumber);
        
        // 두번째 핸드폰번호 필드 설정
		middlePhoneNumberFiled = new LimitedJTextField(Constant.REGEX_PATTERN_MIDDLE_PHONE_NUMBER, 4).get();
		middlePhoneNumberFiled.setBounds(253, 345, 63, 30);
		add(middlePhoneNumberFiled);
		
        // 세번째 핸드폰번호 필드 설정
		lastPhoneNumberFiled = new LimitedJTextField(Constant.REGEX_PATTERN_LAST_PHONE_NUMBER, 4).get();
		lastPhoneNumberFiled.setBounds(322, 345, 63, 30);
		add(lastPhoneNumberFiled);
		
        // 주소 필드 설정
		addressFiled = new LimitedJTextField(Constant.REGEX_PATTERN_ADDRESS, 100).get();
		addressFiled.setBounds(184, 388, 235, 30);
		add(addressFiled);
		
		// 뒤로가기 버튼 설정
		ImageIcon backStageButtonImage = new ImageIcon(SignUp.class.getResource("/image/backStage.png"));		
		ImageIcon backStageButtonImage2 = new ImageIcon(SignUp.class.getResource("/image/s_backStage.png"));	
		backStageButton = new ImageButton(backStageButtonImage, backStageButtonImage2, 750, 4).get();
		add(backStageButton);
		
		// 정보수정 버튼 설정
		ImageIcon editButtonImage = new ImageIcon(SignUp.class.getResource("/image/editButton2.png"));		
		ImageIcon editButtonImage2 = new ImageIcon(SignUp.class.getResource("/image/s_editButton2.png"));		
		editButton = new ImageButton(editButtonImage, editButtonImage2, 389, 453).get();
		add(editButton);
		
		
		// 회원가입 배경이미지 설정
		ImageIcon imageIcon = new ImageIcon(Login.class.getResource("/image/editScreen.jpg"));
		Image image = imageIcon.getImage().getScaledInstance(800, 500, Image.SCALE_SMOOTH);
		JLabel backgroundImage = new JLabel(new ImageIcon(image));
		backgroundImage.setBounds(0,0,800,500);
		add(backgroundImage);	
	}
}
