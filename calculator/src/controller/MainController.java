package controller;

import java.awt.event.KeyEvent;
import java.awt.event.KeyListener;
import Utility.Constant;
import model.AnswerDTO;
import model.InputNumberDTO;
import model.OperandDTO;
import model.OperatorDTO;
import view.MainFrame;

public class MainController implements KeyListener
{
	private MainFrame mainFrame = new MainFrame();
	private AnswerDTO answerDTO = new AnswerDTO("0"); 
	private InputNumberDTO inputNumberDTO = new InputNumberDTO("", "");
	private OperatorDTO operatorDTO = new OperatorDTO("", "");
	private OperandDTO operandDTO = new OperandDTO();
	
	private NumberButtonListener numberButtonListener = new NumberButtonListener(mainFrame, mainFrame.textPanel.answer, inputNumberDTO);
	private OperatorButtonListener operatorButtonListener = new OperatorButtonListener(mainFrame, mainFrame.textPanel.answer, mainFrame.textPanel.formula, answerDTO, inputNumberDTO, operatorDTO, operandDTO);
	private ExtraButtonListener extraButtonListener = new ExtraButtonListener(mainFrame, mainFrame.textPanel.answer, mainFrame.textPanel.formula, answerDTO, inputNumberDTO, operatorDTO, operandDTO);
	
	public void start()
	{	
		mainFrame.showFrame();
		setNumberButtonListener();
		setOperatorButtonListener();	
		setExtraButtonListener();
		mainFrame.addKeyListener(this);
	}
	
	private void setNumberButtonListener() // 숫자 버튼 이벤트처리
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
	
	private void setOperatorButtonListener() // 연산자 버튼 이벤트처리
	{
		mainFrame.buttonPanel.button[Constant.ButtonIndex.DIVISON.getIndex()].addActionListener(operatorButtonListener);
		mainFrame.buttonPanel.button[Constant.ButtonIndex.MULTIPLY.getIndex()].addActionListener(operatorButtonListener);
		mainFrame.buttonPanel.button[Constant.ButtonIndex.SUBTRACTION.getIndex()].addActionListener(operatorButtonListener);
		mainFrame.buttonPanel.button[Constant.ButtonIndex.PLUS.getIndex()].addActionListener(operatorButtonListener);
		mainFrame.buttonPanel.button[Constant.ButtonIndex.CALCULATION.getIndex()].addActionListener(operatorButtonListener);
	}
	
	private void setExtraButtonListener() // 그 외의 버튼 이벤트처리
	{
		mainFrame.buttonPanel.button[Constant.ButtonIndex.CE.getIndex()].addActionListener(extraButtonListener);
		mainFrame.buttonPanel.button[Constant.ButtonIndex.C.getIndex()].addActionListener(extraButtonListener);
		mainFrame.buttonPanel.button[Constant.ButtonIndex.BACK_SPACE.getIndex()].addActionListener(extraButtonListener);
		mainFrame.buttonPanel.button[Constant.ButtonIndex.NEGATE.getIndex()].addActionListener(extraButtonListener);
	}
	
	@Override
	public void keyTyped(KeyEvent e) {}
	@Override
	public void keyReleased(KeyEvent e) {}
	@Override
	public void keyPressed(KeyEvent e) 
	{
	    switch (e.getKeyCode()) 
	    {
	    case KeyEvent.VK_0: 
	    case KeyEvent.VK_NUMPAD0:
	    	mainFrame.buttonPanel.button[Constant.ButtonIndex.ZERO.getIndex()].doClick();   
	        break;
	    case KeyEvent.VK_NUMPAD1:
	    case KeyEvent.VK_1:
	    	mainFrame.buttonPanel.button[Constant.ButtonIndex.ONE.getIndex()].doClick();   
	        break;
	    case KeyEvent.VK_2:
	    case KeyEvent.VK_NUMPAD2:
	    	mainFrame.buttonPanel.button[Constant.ButtonIndex.TWO.getIndex()].doClick();   
	        break;
	    case KeyEvent.VK_3:
	    case KeyEvent.VK_NUMPAD3:
	    	mainFrame.buttonPanel.button[Constant.ButtonIndex.THREE.getIndex()].doClick();   
	        break;
	    case KeyEvent.VK_4:
	    case KeyEvent.VK_NUMPAD4:
	    	mainFrame.buttonPanel.button[Constant.ButtonIndex.FOUR.getIndex()].doClick();   
	        break;
	    case KeyEvent.VK_5:
	    case KeyEvent.VK_NUMPAD5:
	    	mainFrame.buttonPanel.button[Constant.ButtonIndex.FIVE.getIndex()].doClick();   
	        break;
	    case KeyEvent.VK_6:
	    case KeyEvent.VK_NUMPAD6:
	    	mainFrame.buttonPanel.button[Constant.ButtonIndex.SIX.getIndex()].doClick();   
	        break;
	    case KeyEvent.VK_7:
	    case KeyEvent.VK_NUMPAD7:
	    	mainFrame.buttonPanel.button[Constant.ButtonIndex.SEVEN.getIndex()].doClick();   
	        break;
	    case KeyEvent.VK_8:
	    case KeyEvent.VK_NUMPAD8:
	    	mainFrame.buttonPanel.button[Constant.ButtonIndex.EIGHT.getIndex()].doClick();   
	        break;
	    case KeyEvent.VK_9:
	    case KeyEvent.VK_NUMPAD9:
	    	mainFrame.buttonPanel.button[Constant.ButtonIndex.NINE.getIndex()].doClick();   
	        break;
	    case KeyEvent.VK_PERIOD :
	    case KeyEvent.VK_DECIMAL : 
	    	mainFrame.buttonPanel.button[Constant.ButtonIndex.POINT.getIndex()].doClick();   
	        break;
	    case KeyEvent.VK_ENTER : 
	    	mainFrame.buttonPanel.button[Constant.ButtonIndex.CALCULATION.getIndex()].doClick();   
	    	break;
	    case KeyEvent.VK_MULTIPLY :
	    	mainFrame.buttonPanel.button[Constant.ButtonIndex.MULTIPLY.getIndex()].doClick();   
	    	break;
	    case KeyEvent.VK_MINUS :
	    case KeyEvent.VK_SUBTRACT : 
	    	mainFrame.buttonPanel.button[Constant.ButtonIndex.SUBTRACTION.getIndex()].doClick();   
	    	break;
	    case KeyEvent.VK_ADD : 
	    	mainFrame.buttonPanel.button[Constant.ButtonIndex.PLUS.getIndex()].doClick();   
	        break;
	    case KeyEvent.VK_DIVIDE :
	    case KeyEvent.VK_SLASH : 
	    	mainFrame.buttonPanel.button[Constant.ButtonIndex.DIVISON.getIndex()].doClick();   
	    	break;
	    case KeyEvent.VK_BACK_SPACE : 
	    	mainFrame.buttonPanel.button[Constant.ButtonIndex.BACK_SPACE.getIndex()].doClick();   
	    	break;
	    case KeyEvent.VK_ESCAPE : 
	    	mainFrame.buttonPanel.button[Constant.ButtonIndex.C.getIndex()].doClick();   
	    	break;
	    case KeyEvent.VK_DELETE : 
	    	mainFrame.buttonPanel.button[Constant.ButtonIndex.CE.getIndex()].doClick();   
	    	break;
	    case KeyEvent.VK_F9 : 
	    	mainFrame.buttonPanel.button[Constant.ButtonIndex.NEGATE.getIndex()].doClick();   
	    	break;
	    }

	}

}


