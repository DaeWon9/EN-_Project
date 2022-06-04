package controller;

import javax.swing.ImageIcon;
import javax.swing.JOptionPane;
import model.UserData;
import utility.DataProcessing;

public class Login 
{
	private UserData userData;
	public String userId;
	
	public Login(UserData userData)
	{
		this.userData = userData;
	}
	
	public boolean isLoginSucess(String id, String password)
	{	
		userId = id;
		ImageIcon noApeachIcon = new ImageIcon(MainController.class.getResource("/image/noApeach.png"));
		
		if (userId.equals(""))
		{
			JOptionPane.showMessageDialog(null, "아이디릅 입력해주세요", "EN# FRIENDS", JOptionPane.DEFAULT_OPTION, noApeachIcon);
			return false;
		}
		
		if (DataProcessing.get().isRegisteredId(userData.getUserIdList(), userId)) // 등록되어있는 아이디
		{
			if (userData.getUserPassword(userData, userId).equals(password))
			{
				return true;
			}
			JOptionPane.showMessageDialog(null, "비밀번호가 일치하지 않습니다.", "EN# FRIENDS", JOptionPane.DEFAULT_OPTION, noApeachIcon);
			return false;
		}
		JOptionPane.showMessageDialog(null, "등록되지 않은 아이디입니다.", "EN# FRIENDS", JOptionPane.DEFAULT_OPTION, noApeachIcon);
		return false;
	}
}
