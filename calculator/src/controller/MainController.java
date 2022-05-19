package controller;

import java.awt.BorderLayout;
import java.awt.Color;
import java.awt.Dimension;
import java.awt.Font;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.awt.event.ComponentEvent;
import java.awt.event.ComponentListener;
import java.awt.event.KeyEvent;
import java.awt.event.KeyListener;
import java.awt.event.MouseEvent;
import java.awt.event.MouseListener;
import java.math.BigDecimal;
import java.util.Random;

import javax.swing.BoxLayout;
import javax.swing.JButton;
import javax.swing.JFrame;
import javax.swing.JPanel;
import javax.swing.JScrollPane;

import Utility.Constant;
import Utility.DataProcessing;
import model.AnswerDTO;
import model.InputNumberDTO;
import model.OperandDTO;
import model.OperatorDTO;
import view.LogPanel;
import view.MainFrame;

public class MainController implements KeyListener, ComponentListener
{
	private MainFrame mainFrame = new MainFrame();
	private LogPanel logPanel = new LogPanel();	
	private JScrollPane scrollPane = new JScrollPane(logPanel.logButtonPanel);
	private AnswerDTO answerDTO = new AnswerDTO("0");
	private InputNumberDTO inputNumberDTO = new InputNumberDTO("", "");
	private OperatorDTO operatorDTO = new OperatorDTO("", "");
	private OperandDTO operandDTO = new OperandDTO();
	
	public void start()
	{
		NumberButtonListener numberButtonListener = new NumberButtonListener(mainFrame, inputNumberDTO);
		OperatorButtonListener operatorButtonListener = new OperatorButtonListener(mainFrame, logPanel, answerDTO, inputNumberDTO, operatorDTO, operandDTO);
		ExtraButtonListener extraButtonListener = new ExtraButtonListener(mainFrame, answerDTO, inputNumberDTO, operatorDTO, operandDTO);
		
		mainFrame.showFrame();
		setNumberButtonListener(numberButtonListener);
		setOperatorButtonListener(operatorButtonListener);	
		setExtraButtonListener(extraButtonListener);
		setTextPanelEventListner();
		mainFrame.addKeyListener(this);
		mainFrame.addComponentListener(this);
	}
	
	private void setTextPanelEventListner()
	{
		mainFrame.textPanel.addMouseListener(new MouseListener() {
			
			@Override
			public void mouseReleased(MouseEvent e) 
			{
				if (mainFrame.textPanel.getBackground().toString().contains("[r=169,g=171,b=175]"))
					showMainPanels();
			}
			@Override
			public void mousePressed(MouseEvent e) {}
			@Override
			public void mouseExited(MouseEvent e) {}	
			@Override
			public void mouseEntered(MouseEvent e) {}	
			@Override
			public void mouseClicked(MouseEvent e) {}
		});
		
		mainFrame.textPanel.logButton.addActionListener(new ActionListener() 
		{	
			@Override
			public void actionPerformed(ActionEvent e) 
			{
				if (mainFrame.textPanel.getBackground().toString().contains("[r=169,g=171,b=175]"))
				{
					showMainPanels();
				}
				else
				{
					logPanel.titleLabel.setText("");
					mainFrame.getContentPane().removeAll();
					mainFrame.textPanel.setBackground(new Color(169, 171, 175));
					mainFrame.textPanel.formulaScroll.getViewport().setBackground(new Color(169, 171, 175));
					mainFrame.getContentPane().add(mainFrame.textPanel, BorderLayout.NORTH);
					mainFrame.getContentPane().add(logPanel, BorderLayout.CENTER);
					
					scrollPane.setAlignmentX(logPanel.logButtonPanel.RIGHT_ALIGNMENT);		
					scrollPane.getVerticalScrollBar().setPreferredSize(new Dimension(10,100));
					scrollPane.getVerticalScrollBar().setUnitIncrement(20);

					logPanel.add(scrollPane);

					mainFrame.revalidate();
					mainFrame.repaint();
				}
			}
		});
	}
	
	private void setNumberButtonListener(NumberButtonListener numberButtonListener) // 숫자 버튼 이벤트처리
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
	
	private void setOperatorButtonListener(OperatorButtonListener operatorButtonListener) // 연산자 버튼 이벤트처리
	{
		mainFrame.buttonPanel.button[Constant.ButtonIndex.DIVISON.getIndex()].addActionListener(operatorButtonListener);
		mainFrame.buttonPanel.button[Constant.ButtonIndex.MULTIPLY.getIndex()].addActionListener(operatorButtonListener);
		mainFrame.buttonPanel.button[Constant.ButtonIndex.SUBTRACTION.getIndex()].addActionListener(operatorButtonListener);
		mainFrame.buttonPanel.button[Constant.ButtonIndex.PLUS.getIndex()].addActionListener(operatorButtonListener);
		mainFrame.buttonPanel.button[Constant.ButtonIndex.CALCULATION.getIndex()].addActionListener(operatorButtonListener);
	}
	
	private void setExtraButtonListener(ExtraButtonListener extraButtonListener) // 그 외의 버튼 이벤트처리
	{
		mainFrame.buttonPanel.button[Constant.ButtonIndex.CE.getIndex()].addActionListener(extraButtonListener);
		mainFrame.buttonPanel.button[Constant.ButtonIndex.C.getIndex()].addActionListener(extraButtonListener);
		mainFrame.buttonPanel.button[Constant.ButtonIndex.BACK_SPACE.getIndex()].addActionListener(extraButtonListener);
		mainFrame.buttonPanel.button[Constant.ButtonIndex.NEGATE.getIndex()].addActionListener(extraButtonListener);
	}
	
	private void showMainPanels()
	{
		mainFrame.textPanel.emptyLabel.setText("");
		mainFrame.textPanel.logButton.setVisible(true);
		mainFrame.getContentPane().removeAll();
		mainFrame.textPanel.setBackground(new Color(241, 243, 249));
		mainFrame.textPanel.formulaScroll.getViewport().setBackground(new Color(241, 243, 249));
		mainFrame.getContentPane().add(mainFrame.textPanel, BorderLayout.NORTH);
		mainFrame.getContentPane().add(mainFrame.buttonPanel, BorderLayout.CENTER);
		mainFrame.revalidate();
		mainFrame.repaint();
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

	@Override
	public void componentResized(ComponentEvent e) {
		DataProcessing.getDataProcessing().resizeLabel(mainFrame, mainFrame.textPanel.answer);
		showMainPanels();
		if (mainFrame.getSize().width > 580)
		{
			mainFrame.getContentPane().removeAll();
			mainFrame.textPanel.setBackground(new Color(241, 243, 249));
			mainFrame.textPanel.logButton.setVisible(false);
			logPanel.titleLabel.setText(" [ 기록 ]");
			mainFrame.textPanel.emptyLabel.setText(" ");
				
			JPanel leftPanel = new JPanel();
			leftPanel.setLayout(new BorderLayout());
			leftPanel.add(mainFrame.textPanel, BorderLayout.NORTH);
			leftPanel.add(mainFrame.buttonPanel, BorderLayout.CENTER);
		
			scrollPane.setAlignmentX(logPanel.logButtonPanel.RIGHT_ALIGNMENT);
			scrollPane.getVerticalScrollBar().setPreferredSize(new Dimension(10,100));
			scrollPane.getVerticalScrollBar().setUnitIncrement(20);
			logPanel.add(scrollPane);
			
			mainFrame.getContentPane().add(leftPanel, BorderLayout.CENTER);
			mainFrame.getContentPane().add(logPanel, BorderLayout.EAST);
			mainFrame.revalidate();
			mainFrame.repaint();
		}
	}

	@Override
	public void componentMoved(ComponentEvent e) {
		// TODO Auto-generated method stub
		
	}

	@Override
	public void componentShown(ComponentEvent e) {
		// TODO Auto-generated method stub
		
	}

	@Override
	public void componentHidden(ComponentEvent e) {
		// TODO Auto-generated method stub
		
	}

	
}


