package controller;

import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;

import javax.swing.JButton;
import javax.swing.JLabel;

import model.InputNumberDTO;

public class ExtraButtonListener implements ActionListener
{
	private InputNumberDTO inputNumberDTO;
	private JLabel answerLabel;
	
	public ExtraButtonListener(JLabel answerLabel, InputNumberDTO inputNumberDTO)
	{
		this.inputNumberDTO = inputNumberDTO;
		this.answerLabel = answerLabel;
	}
	@Override
	public void actionPerformed(ActionEvent e) 
	{
		if (((JButton)e.getSource()).getText().equals("C"))
		{
			System.out.println("C버튼");
		}
		
		else if (((JButton)e.getSource()).getText().equals("CE"))
		{
			System.out.println("CE버튼");
		}
		
		else if (((JButton)e.getSource()).getText().equals("<-"))
		{
			System.out.println("<-버튼");
		}
	}

}
