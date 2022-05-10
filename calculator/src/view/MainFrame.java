package view;

import java.awt.BorderLayout;
import java.awt.Color;
import java.awt.Container;

import javax.swing.BoxLayout;
import javax.swing.JFrame;
import javax.swing.JPanel;
import javax.swing.JTextField;
import javax.swing.border.Border;

import Utility.Constant;

public class MainFrame extends JFrame
{
	public MainFrame()
	{
		TextPanel textPanel = new TextPanel();
		ButtonPanel buttonPanel = new ButtonPanel();
		setTitle("계산기");
		setDefaultCloseOperation(EXIT_ON_CLOSE);
		
		add(textPanel, BorderLayout.NORTH);
		add(buttonPanel, BorderLayout.CENTER);		
		setSize(422,534);
		setVisible(true);
	
	}
}