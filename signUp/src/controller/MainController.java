package controller;

import java.awt.Color;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;

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
		
	}
	
	private void setLoginPanelButtonListener()
	{
		mainFrame.loginPanel.signUpButton.addActionListener(new ActionListener() // 로그인패널에서 회원가입버튼 클릭시 이벤트 처리
		{
			@Override
			public void actionPerformed(ActionEvent e) {
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
				JOptionPane.showMessageDialog(null, "준비중입니다...","EN# FRIENDS",JOptionPane.DEFAULT_OPTION, noApeachIcon);
				
			}
		});
		
		mainFrame.mainPanel.withdrawalButton.addActionListener(new ActionListener() {
			
			@Override
			public void actionPerformed(ActionEvent e) {
				JOptionPane.showMessageDialog(null, "준비중입니다...","EN# FRIENDS",JOptionPane.DEFAULT_OPTION, noApeachIcon);
				
			}
		});
	}
}
