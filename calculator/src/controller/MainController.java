package controller;

import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;

import javax.swing.JButton;

import Utility.Constant;
import Utility.DataProcessing;
import model.AnswerDTO;
import model.InputNumberDTO;
import view.MainFrame;

public class MainController 
{
	private MainFrame mainFrame = new MainFrame();
	private DataProcessing dataProcessing = new DataProcessing();
	private AnswerDTO answerDTO = new AnswerDTO("");
	private InputNumberDTO inputNumberDTO = new InputNumberDTO("");
	private String inputString = "";	
	private NumberButtonListener numberButtonListener = new NumberButtonListener(mainFrame.textPanel.answer, inputNumberDTO);
	public void start()
	{	
		mainFrame.showFrame();
		
	
		for (int buttonIndex = 0; buttonIndex <=20; buttonIndex++)
		{
			switch (buttonIndex)
			{
				case 12:
					mainFrame.buttonPanel.button[Constant.ButtonIndex.ONE.getIndex()].addActionListener(numberButtonListener);
				/*
				case Constant.ButtonIndex.ONE.getIndex():
				case Constant.ButtonIndex.TWO.getIndex():
				case Constant.ButtonIndex.THREE.getIndex():
				case Constant.ButtonIndex.FOUR.getIndex():
				case Constant.ButtonIndex.FIVE.getIndex():
				case Constant.ButtonIndex.SIX.getIndex():
				case Constant.ButtonIndex.SEVEN.getIndex():
				case Constant.ButtonIndex.EIGHT.getIndex():
				case Constant.ButtonIndex.NINE.getIndex():
				*/
					break;
				default:
					break;
			}
		}
		
		
		
		/*
		mainFrame.buttonPanel.button[Constant.ButtonIndex.ONE.getIndex()].addActionListener(new ActionListener() {	
			@Override
			public void actionPerformed(ActionEvent e) 
			{
				if (inputString.length() < Constant.MAX_LONG_LENGTH)
				{
					inputString += "1";
					answerDTO.set(dataProcessing.formatNumber(Long.parseLong(inputString)));
					mainFrame.textPanel.answer.setText(answerDTO.get());
				}
			}
		});
		*/
	}
	
}


