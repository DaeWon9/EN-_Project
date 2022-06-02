package view.panel;

import java.awt.Image;
import javax.swing.ImageIcon;
import javax.swing.JButton;
import javax.swing.JLabel;
import javax.swing.JPanel;
import javax.swing.JTextField;

public class Login extends JPanel
{
	public JButton loginButton, signUpButton, findIdButton, findPwButton;
	public JTextField idTextFiled, pwTextFiled;
	public Login()
	{
		setLayout(null);	
		idTextFiled = new JTextField();
		idTextFiled.setSize(185,30);
		idTextFiled.setLocation(437,327);
		add(idTextFiled);
		
		pwTextFiled = new JTextField();
		pwTextFiled.setSize(185,30);
		pwTextFiled.setLocation(437,363);
		add(pwTextFiled);

		ImageIcon loginButtonImage = new ImageIcon(Login.class.getResource("/image/loginButton.png"));		
		loginButton = new JButton(loginButtonImage);
		loginButton.setBorderPainted(false);
		loginButton.setSize(loginButtonImage.getIconWidth(), loginButtonImage.getIconHeight());
		loginButton.setLocation(628, 324);
		loginButton.setBorderPainted(false);
		loginButton.setFocusPainted(false); 
		loginButton.setContentAreaFilled(false);
		add(loginButton);

		ImageIcon findIdButtonImage = new ImageIcon(Login.class.getResource("/image/findIdButton.png"));		
		findIdButton = new JButton(findIdButtonImage);
		findIdButton.setBorderPainted(false);
		findIdButton.setSize(findIdButtonImage.getIconWidth(), findIdButtonImage.getIconHeight());
		findIdButton.setLocation(434, 412);
		findIdButton.setBorderPainted(false);
		findIdButton.setFocusPainted(false); 
		findIdButton.setContentAreaFilled(false);
		add(findIdButton);
		
		ImageIcon findPwButtonImage = new ImageIcon(Login.class.getResource("/image/findPwButton.png"));		
		findPwButton = new JButton(findPwButtonImage);
		findPwButton.setBorderPainted(false);
		findPwButton.setSize(findPwButtonImage.getIconWidth(), findPwButtonImage.getIconHeight());
		findPwButton.setLocation(531, 412);
		findPwButton.setBorderPainted(false);
		findPwButton.setFocusPainted(false); 
		findPwButton.setContentAreaFilled(false);
		add(findPwButton);
		
		ImageIcon signUpButtonImage = new ImageIcon(Login.class.getResource("/image/signUpButton.png"));		
		signUpButton = new JButton(signUpButtonImage);
		signUpButton.setBorderPainted(false);
		signUpButton.setSize(signUpButtonImage.getIconWidth(), signUpButtonImage.getIconHeight());
		signUpButton.setLocation(628, 412);
		signUpButton.setBorderPainted(false);
		signUpButton.setFocusPainted(false); 
		signUpButton.setContentAreaFilled(false);
		add(signUpButton);
		
		ImageIcon imageIcon = new ImageIcon(Login.class.getResource("/image/loginScreen.jpg"));
		Image image = imageIcon.getImage().getScaledInstance(800, 500, Image.SCALE_SMOOTH);
		JLabel backgroundImage = new JLabel(new ImageIcon(image));
		backgroundImage.setBounds(0,0,800,500);
		add(backgroundImage);	
	}
}
