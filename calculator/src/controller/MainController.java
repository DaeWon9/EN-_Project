package controller;

import javax.swing.JLabel;

import Utility.Constant;
import model.AnswerDTO;
import model.InputNumberDTO;
import model.OperandDTO;
import model.OperatorDTO;
import view.MainFrame;

public class MainController 
{
	private MainFrame mainFrame = new MainFrame();
	private AnswerDTO answerDTO = new AnswerDTO("0"); 
	private InputNumberDTO inputNumberDTO = new InputNumberDTO("0", "");
	private OperatorDTO operatorDTO = new OperatorDTO("", "");
	private OperandDTO operandDTO = new OperandDTO();
	
	private NumberButtonListener numberButtonListener = new NumberButtonListener(mainFrame.textPanel.answer, inputNumberDTO);
	private OperatorButtonListener operatorButtonListener = new OperatorButtonListener(mainFrame.textPanel.answer, mainFrame.textPanel.formula, answerDTO, inputNumberDTO, operatorDTO, operandDTO);
	private ExtraButtonListener extraButtonListener = new ExtraButtonListener(mainFrame.textPanel.answer, mainFrame.textPanel.formula, answerDTO, inputNumberDTO, operatorDTO, operandDTO);
	
	public void start()
	{	
		mainFrame.showFrame();
		SetNumberButtonListener();
		SetOperatorButtonListener();	
		SetExtraButtonListener();
	}
	
	private void SetNumberButtonListener() // 숫자 버튼 이벤트처리
	{
		mainFrame.buttonPanel.button[Constant.ButtonIndex.ZERO.getIndex()].addActionListener(numberButtonListener);
		mainFrame.buttonPanel.button[Constant.ButtonIndex.ONE.getIndex()].addActionListener(numberButtonListener);
		mainFrame.buttonPanel.button[Constant.ButtonIndex.TWO.getIndex()].addActionListener(numberButtonListener);
		mainFrame.buttonPanel.button[Constant.ButtonIndex.THREE.getIndex()].addActionListener(numberButtonListener);
		mainFrame.buttonPanel.button[Constant.ButtonIndex.FOUR.getIndex()].addActionListener(numberButtonListener);
		mainFrame.buttonPanel.button[Constant.ButtonIndex.FIVE.getIndex()].addActionListener(numberButtonListener);
		mainFrame.buttonPanel.button[Constant.ButtonIndex.SIX.getIndex()].addActionListener(numberButtonListener);
		mainFrame.buttonPanel.button[Constant.ButtonIndex.SEVEN.getIndex()].addActionListener(numberButtonListener);
		mainFrame.buttonPanel.button[Constant.ButtonIndex.EIGHT.getIndex()].addActionListener(numberButtonListener);
		mainFrame.buttonPanel.button[Constant.ButtonIndex.NINE.getIndex()].addActionListener(numberButtonListener);
		mainFrame.buttonPanel.button[Constant.ButtonIndex.POINT.getIndex()].addActionListener(numberButtonListener);
	}
	
	private void SetOperatorButtonListener() // 연산자 버튼 이벤트처리
	{
		mainFrame.buttonPanel.button[Constant.ButtonIndex.DIVISON.getIndex()].addActionListener(operatorButtonListener);
		mainFrame.buttonPanel.button[Constant.ButtonIndex.MULTIPLY.getIndex()].addActionListener(operatorButtonListener);
		mainFrame.buttonPanel.button[Constant.ButtonIndex.SUBTRACTION.getIndex()].addActionListener(operatorButtonListener);
		mainFrame.buttonPanel.button[Constant.ButtonIndex.PLUS.getIndex()].addActionListener(operatorButtonListener);
		mainFrame.buttonPanel.button[Constant.ButtonIndex.CALCULATION.getIndex()].addActionListener(operatorButtonListener);
	}
	
	private void SetExtraButtonListener() // 그 외의 버튼 이벤트처리
	{
		mainFrame.buttonPanel.button[Constant.ButtonIndex.CE.getIndex()].addActionListener(extraButtonListener);
		mainFrame.buttonPanel.button[Constant.ButtonIndex.C.getIndex()].addActionListener(extraButtonListener);
		mainFrame.buttonPanel.button[Constant.ButtonIndex.BACK_SPACE.getIndex()].addActionListener(extraButtonListener);
	}
	
}


