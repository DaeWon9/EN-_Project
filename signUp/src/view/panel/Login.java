package view.panel;

import java.awt.Image;
import javax.swing.ImageIcon;
import javax.swing.JButton;
import javax.swing.JLabel;
import javax.swing.JPanel;
import javax.swing.JTextField;

import view.ImageButton;

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
		loginButton = new ImageButton(loginButtonImage, 628, 324).get();
		add(loginButton);

		ImageIcon findIdButtonImage = new ImageIcon(Login.class.getResource("/image/findIdButton.png"));
		findIdButton = new ImageButton(findIdButtonImage, 434, 412).get();
		add(findIdButton);
		
		ImageIcon findPwButtonImage = new ImageIcon(Login.class.getResource("/image/findPwButton.png"));	
		findPwButton = new ImageButton(findPwButtonImage, 531, 412).get();
		add(findPwButton);
		
		ImageIcon signUpButtonImage = new ImageIcon(Login.class.getResource("/image/signUpButton.png"));
		signUpButton = new ImageButton(signUpButtonImage, 628, 412).get();
		add(signUpButton);
		
		ImageIcon imageIcon = new ImageIcon(Login.class.getResource("/image/loginScreen.jpg"));
		Image image = imageIcon.getImage().getScaledInstance(800, 500, Image.SCALE_SMOOTH);
		JLabel backgroundImage = new JLabel(new ImageIcon(image));
		backgroundImage.setBounds(0,0,800,500);
		add(backgroundImage);	
	}
}
