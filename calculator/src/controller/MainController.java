package controller;

import Utility.Constant;
import model.AnswerDTO;
import model.InputNumberDTO;
import view.MainFrame;

public class MainController 
{
	private MainFrame mainFrame = new MainFrame();
	private AnswerDTO answerDTO = new AnswerDTO("0"); 
	private InputNumberDTO inputNumberDTO = new InputNumberDTO("", "");
	private NumberButtonListener numberButtonListener = new NumberButtonListener(mainFrame.textPanel.answer, inputNumberDTO);
	private OperatorButtonListener operatorButtonListener = new OperatorButtonListener(mainFrame.textPanel.answer, mainFrame.textPanel.formula, answerDTO, inputNumberDTO);
	
	
	public void start()
	{	
		mainFrame.showFrame();
		SetNumberButtonListener();
		SetOperatorButtonListener();
		
	}
	
	
	
	
	
	private void SetOperatorButtonListener()
	{
		mainFrame.buttonPanel.button[Constant.ButtonIndex.DIVISON.getIndex()].addActionListener(operatorButtonListener);
		mainFrame.buttonPanel.button[Constant.ButtonIndex.MULTIPLY.getIndex()].addActionListener(operatorButtonListener);
		mainFrame.buttonPanel.button[Constant.ButtonIndex.SUBTRACTION.getIndex()].addActionListener(operatorButtonListener);
		mainFrame.buttonPanel.button[Constant.ButtonIndex.PLUS.getIndex()].addActionListener(operatorButtonListener);
		mainFrame.buttonPanel.button[Constant.ButtonIndex.CALCULATION.getIndex()].addActionListener(operatorButtonListener);
	}
	
	
	private void SetNumberButtonListener()
	{
		mainFrame.buttonPanel.button[Constant.ButtonIndex.ONE.getIndex()].addActionListener(numberButtonListener);
		mainFrame.buttonPanel.button[Constant.ButtonIndex.TWO.getIndex()].addActionListener(numberButtonListener);
		mainFrame.buttonPanel.button[Constant.ButtonIndex.THREE.getIndex()].addActionListener(numberButtonListener);
		mainFrame.buttonPanel.button[Constant.ButtonIndex.FOUR.getIndex()].addActionListener(numberButtonListener);
		mainFrame.buttonPanel.button[Constant.ButtonIndex.FIVE.getIndex()].addActionListener(numberButtonListener);
		mainFrame.buttonPanel.button[Constant.ButtonIndex.SIX.getIndex()].addActionListener(numberButtonListener);
		mainFrame.buttonPanel.button[Constant.ButtonIndex.SEVEN.getIndex()].addActionListener(numberButtonListener);
		mainFrame.buttonPanel.button[Constant.ButtonIndex.EIGHT.getIndex()].addActionListener(numberButtonListener);
		mainFrame.buttonPanel.button[Constant.ButtonIndex.NINE.getIndex()].addActionListener(numberButtonListener);
	}
	
}


