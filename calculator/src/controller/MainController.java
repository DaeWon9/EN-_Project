package controller;

import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;

import Utility.Constant;
import Utility.DataProcessing;
import model.AnswerDTO;
import view.MainFrame;

public class MainController 
{
	private String inputString = "";	
	
	public void start()
	{
		MainFrame mainFrame = new MainFrame();
		DataProcessing dataProcessing = new DataProcessing();
		AnswerDTO answerDTO = new AnswerDTO("");
		
		mainFrame.showFrame();
		
		
		
		mainFrame.buttonPanel.button[Constant.ButtonIndex.ONE.getIndex()].addActionListener(new ActionListener() {	
			@Override
			public void actionPerformed(ActionEvent e) 
			{
				inputString += "1";
				answerDTO.Set(dataProcessing.formatNumber(Long.parseLong(inputString)));
				mainFrame.textPanel.answer.setText(answerDTO.Get());
			}
		});
		
	}
}
