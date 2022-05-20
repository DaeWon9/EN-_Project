package controller;

import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import javax.swing.JButton;
import javax.swing.JFrame;
import javax.swing.JLabel;
import Utility.Constant;
import Utility.DataProcessing;
import model.InputNumberDTO;
import view.MainFrame;

public class NumberButtonListener implements ActionListener
{
	private InputNumberDTO inputNumberDTO;
	private MainFrame mainFrame;
	
	public NumberButtonListener(MainFrame mainFrame, InputNumberDTO inputNumberDTO)
	{
		this.inputNumberDTO = inputNumberDTO;
		this.mainFrame = mainFrame;
	}
		
	@Override
	public void actionPerformed(ActionEvent e)
	{
		String inputNumber;
		String integerPart;
		String decimalPointPart;
		
		inputNumber = DataProcessing.getDataProcessing().deleteComma(inputNumberDTO.get()); // 입력된값에서 ,지우고 받아오기
		inputNumber += ((JButton)e.getSource()).getText(); // 입력된 값 추가
		if (DataProcessing.getDataProcessing().countChar(inputNumber, '.') > 1) // point는 하나만 가능 
			return;
		
		if (inputNumber.equals("."))
			inputNumber = "0.";
		
		inputNumberDTO.set(DataProcessing.getDataProcessing().inputNumberFormat(inputNumber));

		mainFrame.textPanel.answer.setText(inputNumberDTO.get());
		DataProcessing.getDataProcessing().resizeLabel(mainFrame, mainFrame.textPanel.answer);
		mainFrame.requestFocus();
	}	
}
