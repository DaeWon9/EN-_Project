package controller;

import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;

import javax.swing.JComboBox;

import model.UserData;
import utility.DataProcessing;
import view.frame.MainFrame;

public class MainController 
{
	private UserData userData = new UserData();
	MainFrame mainFrame = new MainFrame();
	private controller.SignUp signUp = new SignUp(mainFrame.signUpPanel, userData);
	private controller.Login login = new Login(mainFrame.signUpPanel, userData);
	
	public void start()
	{
		mainFrame.ShowFrame();
		setButtonListener(mainFrame);
	}
	
	private void setButtonListener(MainFrame mainFrame)
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
				
				if(!userInputId.equals(""))
				{
					System.out.println("userId : " + userInputId);
					if (DataProcessing.get().isRegisteredId(userData.getUserIdList(), userInputId))
					{
						System.out.println("사용불가능한 아이디입니다.");
					}
					else
					{
						System.out.println("사용가능한 아이디입니다.");
					}
				}
			}
		});
		
		mainFrame.signUpPanel.signUpButton.addActionListener(new ActionListener() {
			
			@Override
			public void actionPerformed(ActionEvent e) 
			{
				signUp.action();
				// check all data input and validate
				// if not -> alert Message (please input "" data)
				// all data input -> confirm message (Would you like to sign up with this information?)
				// -> insert dataBase
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
					System.out.println("로그인완료");
				}
				else
				{
					System.out.println("로그인실패");
				}
			}
		});
		
	}

}
