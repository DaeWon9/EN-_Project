package view;

import java.awt.Color;
import java.awt.event.KeyEvent;
import java.awt.event.KeyListener;

import javax.swing.JPasswordField;
import javax.swing.JTextField;

public class LimitedJPasswordTextField 
{
	private JPasswordField jPasswordField;
	
	public LimitedJPasswordTextField(String regexForm, int limit)
	{
		this.jPasswordField = new JPasswordField();
		jPasswordField.addKeyListener(new KeyListener() {
			
			@SuppressWarnings("deprecation")
			@Override
			public void keyTyped(KeyEvent e) {
				if(jPasswordField.getText().length() >= limit)
				{
					e.consume();
				}
			}
			
			@Override
			public void keyReleased(KeyEvent e) {
				if(jPasswordField.getText().matches(regexForm))
				{
					jPasswordField.setForeground(Color.BLUE);
				}
				else
				{
					jPasswordField.setForeground(Color.RED);
				}
				
			}
			
			@Override
			public void keyPressed(KeyEvent e) {
				// TODO Auto-generated method stub
				
			}
		});
	}
	
	public JPasswordField get()
	{
		return this.jPasswordField;
	}
}
