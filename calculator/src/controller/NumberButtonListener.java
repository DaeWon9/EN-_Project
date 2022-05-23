package controller;

import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import javax.swing.JButton;
import javax.swing.JFrame;
import javax.swing.JLabel;
import Utility.Constant;
import Utility.DataProcessing;
import model.FormulaDTO;
import model.InputNumberDTO;
import view.MainFrame;

public class NumberButtonListener implements ActionListener
{
	private MainFrame mainFrame;
	private LogManagement logManagement;
	private FormulaDTO formulaDTO;
	private InputNumberDTO inputNumberDTO;
	
	public NumberButtonListener(MainFrame mainFrame, LogManagement logManagement, FormulaDTO formulaDTO, InputNumberDTO inputNumberDTO)
	{
		this.mainFrame = mainFrame;
		this.logManagement = logManagement;
		this.formulaDTO = formulaDTO;
		this.inputNumberDTO = inputNumberDTO;
	}
		
	@Override
	public void actionPerformed(ActionEvent e)
	{
		String inputNumber;
		String integerPart;
		String decimalPointPart;
		
		if (inputNumberDTO.get().contains("negate"))
		{
			inputNumberDTO.set("");
			formulaDTO.set(" ");
			mainFrame.textPanel.formula.setText(" ");
			DataProcessing.getDataProcessing().setArrowButtonVisible(mainFrame);
		}
		
		inputNumber = DataProcessing.getDataProcessing().deleteComma(inputNumberDTO.get()); // 입력된값에서 ,지우고 받아오기
		inputNumber += ((JButton)e.getSource()).getText(); // 입력된 값 추가
		if (DataProcessing.getDataProcessing().countChar(inputNumber, '.') > 1) // point는 하나만 가능 
			return;
		
		if (inputNumber.equals("."))
			inputNumber = "0.";
			
		if (inputNumber.split("\\.").length == 2) // point로 쪼갰을 때 정수부와 실수부로 쪼개진다면, 나눠서 ,포멧 처리후 합쳐주기 (정수부가 0이면 소수부는 16자리까지, 그외에는 합쳐서 16자리)
		{
			integerPart = inputNumber.split("\\.")[0];
			decimalPointPart = inputNumber.split("\\.")[1];
			if ((integerPart.equals("0") && inputNumber.length() <= Constant.MAX_LONG_LENGTH + 2) || (!integerPart.equals("0") && inputNumber.length() <= Constant.MAX_LONG_LENGTH + 1))
			{
				integerPart = DataProcessing.getDataProcessing().appendCommaInLong(Long.parseLong(integerPart));
				inputNumberDTO.set(integerPart + "." + decimalPointPart);
			}
		}
		else if (inputNumber.contains(".") && inputNumber.length() <= Constant.MAX_LONG_LENGTH + 1)
		{
			integerPart = inputNumber.substring(0, inputNumber.length()-1);
			integerPart = DataProcessing.getDataProcessing().appendCommaInLong(Long.parseLong(integerPart)); 
			inputNumberDTO.set(integerPart + ".");
		}
		else
		{
			if (inputNumber.length() <= Constant.MAX_LONG_LENGTH)
			{
				inputNumberDTO.set(DataProcessing.getDataProcessing().numberFormat(inputNumber));
			}
		}
		mainFrame.textPanel.answer.setText(inputNumberDTO.get());
		DataProcessing.getDataProcessing().resizeLabel(mainFrame);
		mainFrame.requestFocus();
	}	
}
