package controller;

import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;

import model.UserData;
import utility.DataProcessing;
import view.frame.MainFrame;

public class MainController 
{
	private UserData userData = new UserData();
	
	public void start()
	{
		MainFrame mainFrame = new MainFrame();
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
				System.out.println(DataProcessing.get().isRegisteredId(userData.GetUserIdList(), userInputId));
			}
		});
	}

}
