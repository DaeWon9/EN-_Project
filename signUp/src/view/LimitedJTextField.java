package view;

import java.awt.Color;
import java.awt.event.KeyEvent;
import java.awt.event.KeyListener;

import javax.swing.JTextField;

import utility.Constant;

public class LimitedJTextField extends JTextField
{
	private JTextField jTextField;
	
	public LimitedJTextField(String regexForm, int limit)
	{
		this.jTextField = new JTextField();
		jTextField.addKeyListener(new KeyListener() {
			
			@Override
			public void keyTyped(KeyEvent e) {
				if(jTextField.getText().length() >= limit)
				{
					e.consume();
				}
			}
			
			@Override
			public void keyReleased(KeyEvent e) {
				if(jTextField.getText().matches(regexForm))
				{
					jTextField.setForeground(Color.BLUE);
				}
				else
				{
					jTextField.setForeground(Color.RED);
				}
				
			}
			
			@Override
			public void keyPressed(KeyEvent e) {
				// TODO Auto-generated method stub
				
			}
		});
	}
	
	public JTextField get()
	{
		return this.jTextField;
	}
}
