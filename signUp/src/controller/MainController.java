package controller;

import java.awt.Color;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.util.ArrayList;

import javax.swing.ImageIcon;
import javax.swing.JComboBox;
import javax.swing.JOptionPane;

import model.UserData;
import utility.Constant;
import utility.DataProcessing;
import view.frame.MainFrame;

public class MainController 
{
	private ImageIcon okRyanIcon = new ImageIcon(MainController.class.getResource("/image/okRyan.png"));	
	private ImageIcon noApeachIcon = new ImageIcon(MainController.class.getResource("/image/noApeach.png"));
	private UserData userData = new UserData();
	private MainFrame mainFrame = new MainFrame();
	private controller.SignUp signUp = new SignUp(mainFrame.signUpPanel, userData);
	private controller.Login login = new Login(mainFrame.signUpPanel, userData);
	
	public void start()
	{
		mainFrame.ShowFrame();
		setLoginPanelButtonListener();
		setSignUpPanelButtonListener();
		setMainPanelButtonListener();
		setEditPanelButtonListener();
	}
	
	private void setLoginPanelButtonListener()
	{
		mainFrame.loginPanel.signUpButton.addActionListener(new ActionListener() // 로그인패널에서 회원가입버튼 클릭시 이벤트 처리
		{
			@Override
			public void actionPerformed(ActionEvent e) {
				
				// 회원가입 패널 들어가기전 field값들 초기화
				mainFrame.signUpPanel.nameFiled.setText(""); // 이름 필드 셋팅
				mainFrame.signUpPanel.idFiled.setText(""); // 아이디 필드 셋팅
				mainFrame.signUpPanel.passwordFiled.setText(""); // 비밀번호 필드 셋팅
				mainFrame.signUpPanel.passwordCheckFiled.setText(""); // 비밀번호 체크 필드 셋팅
				
				mainFrame.signUpPanel.birthYear.setSelectedIndex(0); // 생년월일 중 년도 필드 셋팅
				mainFrame.signUpPanel.birthMonth.setSelectedIndex(0);  // 생년월일 중 월 필드 셋팅
				mainFrame.signUpPanel.birthDay.setSelectedIndex(0);  // 생년월일 중 일 필드 셋팅
				
				mainFrame.signUpPanel.emailFiled.setText(""); // 앞쪽 이메일 필드 셋팅
				mainFrame.signUpPanel.lastEmailFiled.setText(""); // 뒷쪽 이메일 필드 셋팅
				
				mainFrame.signUpPanel.firstPhoneNumber.setSelectedIndex(0); // 핸드폰번호 필드 셋팅
				mainFrame.signUpPanel.middlePhoneNumberFiled.setText(""); // 중간
				mainFrame.signUpPanel.lastPhoneNumberFiled.setText(""); // 마지막
			
				mainFrame.signUpPanel.addressFiled.setText(""); // 주소 필드 셋팅
				
				mainFrame.getContentPane().removeAll();
				mainFrame.getContentPane().add(mainFrame.signUpPanel);
				mainFrame.revalidate();
				mainFrame.repaint();
			}
		});
		
		mainFrame.loginPanel.loginButton.addActionListener(new ActionListener() {
			
			@Override
			public void actionPerformed(ActionEvent e) 
			{
				String userId = mainFrame.loginPanel.idTextFiled.getText();
				@SuppressWarnings("deprecation")
				String userPassword = mainFrame.loginPanel.pwTextFiled.getText();
				System.out.println(userId);
				System.out.println(userPassword);
				if (login.isLoginSucess(userId, userPassword))
				{
					mainFrame.getContentPane().removeAll();
					mainFrame.getContentPane().add(mainFrame.mainPanel);
					mainFrame.revalidate();
					mainFrame.repaint();
				}
			}
		});
	}
	
	private void setSignUpPanelButtonListener()
	{
		
		mainFrame.signUpPanel.backStageButton.addActionListener(new ActionListener() // 회원가입 패널에서 뒤로가기 버튼 클릭시 이벤트 처리
		{
			@Override
			public void actionPerformed(ActionEvent e) {
				mainFrame.getContentPane().removeAll();
				mainFrame.getContentPane().add(mainFrame.loginPanel);
				mainFrame.revalidate();
				mainFrame.repaint();
			}
		});	
		
		mainFrame.signUpPanel.idCheckButton.addActionListener(new ActionListener() {
			
			@Override
			public void actionPerformed(ActionEvent e) {
				String userInputId = mainFrame.signUpPanel.idFiled.getText();

				mainFrame.signUpPanel.isIdCheck = false;
				if (userInputId.equals(""))
				{
					JOptionPane.showMessageDialog(null, "아이디를 입력해주세요.","ID CHECK",JOptionPane.DEFAULT_OPTION, noApeachIcon);
				}
				
				else if (!mainFrame.signUpPanel.idFiled.getText().matches(Constant.REGEX_PATTERN_ID))
				{
					JOptionPane.showMessageDialog(null, "형식에 맞는 아이디를 입력해주세요.","ID CHECK",JOptionPane.DEFAULT_OPTION, noApeachIcon);
				}
				
				else if (DataProcessing.get().isRegisteredId(userData.getUserIdList(), userInputId))
				{
					JOptionPane.showMessageDialog(null, "이미 사용중인 아이디입니다.","ID CHECK",JOptionPane.DEFAULT_OPTION, noApeachIcon);
					mainFrame.signUpPanel.idFiled.setForeground(Color.RED);
				}
				else
				{
					JOptionPane.showMessageDialog(null, "사용가능한 아이디입니다.","ID CHECK",JOptionPane.DEFAULT_OPTION, okRyanIcon);
					mainFrame.signUpPanel.isIdCheck = true;
				}
			}
		});
		
		mainFrame.signUpPanel.signUpButton.addActionListener(new ActionListener() {
			
			@Override
			public void actionPerformed(ActionEvent e) 
			{
				signUp.action();
			}
		});
	}

	private void setMainPanelButtonListener()
	{
		mainFrame.mainPanel.backStageButton.addActionListener(new ActionListener() {
			
			@Override
			public void actionPerformed(ActionEvent e) {
				mainFrame.getContentPane().removeAll();
				mainFrame.getContentPane().add(mainFrame.loginPanel);
				mainFrame.revalidate();
				mainFrame.repaint();
			}
		});
		
		mainFrame.mainPanel.editButton.addActionListener(new ActionListener() {
			
			@Override
			public void actionPerformed(ActionEvent e) {
				ArrayList<String> userDataList = userData.getLoginedUserDataList(login.userId);
				
				// 정보수정 패널 들어가기전 field값들 설정
				mainFrame.editPanel.nameFiled.setText(userDataList.get(0)); // 이름 필드 셋팅
				mainFrame.editPanel.idFiled.setText(userDataList.get(1)); // 아이디 필드 셋팅
				mainFrame.editPanel.passwordFiled.setText(userDataList.get(2)); // 비밀번호 필드 셋팅
				mainFrame.editPanel.passwordCheckFiled.setText(userDataList.get(3)); // 비밀번호 체크 필드 셋팅
				
				mainFrame.editPanel.birthYear.setSelectedItem(userDataList.get(4).substring(0, 4)); // 생년월일 중 년도 필드 셋팅
				mainFrame.editPanel.birthMonth.setSelectedItem(userDataList.get(4).substring(4, 6)); // 생년월일 중 월 필드 셋팅
				mainFrame.editPanel.birthDay.setSelectedItem(userDataList.get(4).substring(6, 8)); // 생년월일 중 일 필드 셋팅
				
				mainFrame.editPanel.emailFiled.setText(userDataList.get(5).split("@")[0]); // 앞쪽 이메일 필드 셋팅
				mainFrame.editPanel.lastEmailFiled.setText(userDataList.get(5).split("@")[1]); // 뒷쪽 이메일 필드 셋팅
				
				mainFrame.editPanel.firstPhoneNumber.setSelectedItem(userDataList.get(6).substring(0, 3)); // 핸드폰번호 필드 셋팅
				mainFrame.editPanel.middlePhoneNumberFiled.setText(userDataList.get(6).substring(3, userDataList.get(6).length() - 4));
				mainFrame.editPanel.lastPhoneNumberFiled.setText(userDataList.get(6).substring(userDataList.get(6).length() - 4
																								, userDataList.get(6).length())); // 마지막
				
				mainFrame.editPanel.addressFiled.setText(userDataList.get(7)); // 주소 필드 셋팅
				
				mainFrame.getContentPane().removeAll();
				mainFrame.getContentPane().add(mainFrame.editPanel);
				mainFrame.revalidate();
				mainFrame.repaint();
			}
		});
		
		mainFrame.mainPanel.withdrawalButton.addActionListener(new ActionListener() {
			
			@Override
			public void actionPerformed(ActionEvent e) {
				JOptionPane.showMessageDialog(null, "준비중입니다...","EN# FRIENDS",JOptionPane.DEFAULT_OPTION, noApeachIcon);
				
			}
		});
	}
	
	private void setEditPanelButtonListener()
	{
		mainFrame.editPanel.backStageButton.addActionListener(new ActionListener() {
			
			@Override
			public void actionPerformed(ActionEvent e) {
				mainFrame.getContentPane().removeAll();
				mainFrame.getContentPane().add(mainFrame.mainPanel);
				mainFrame.revalidate();
				mainFrame.repaint();
			}
		});
		
		mainFrame.editPanel.editButton.addActionListener(new ActionListener() {
			
			@Override
			public void actionPerformed(ActionEvent e) {
				JOptionPane.showMessageDialog(null, "준비중입니다...","EN# FRIENDS",JOptionPane.DEFAULT_OPTION, noApeachIcon);
				
			}
		});
	}
}
