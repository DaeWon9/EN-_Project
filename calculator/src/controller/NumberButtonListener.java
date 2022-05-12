package controller;

import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import javax.swing.JButton;
import javax.swing.JLabel;
import Utility.Constant;
import Utility.DataProcessing;
import model.InputNumberDTO;

public class NumberButtonListener implements ActionListener
{
	private InputNumberDTO inputNumberDTO;
	private JLabel answerLabel;
	
	public NumberButtonListener(JLabel answerLabel, InputNumberDTO inputNumberDTO)
	{
		this.inputNumberDTO = inputNumberDTO;
		this.answerLabel = answerLabel;
	}
	
	@Override
	public void actionPerformed(ActionEvent e) 
	{
		String inputNumber;
		String integerPart;
		String decimalPointPart;
				
		inputNumber = DataProcessing.getDataProcessing().deleteComma(inputNumberDTO.get());
		inputNumber += ((JButton)e.getSource()).getText();
		if (DataProcessing.getDataProcessing().countChar(inputNumber, '.') > 1) // point는 하나만 가능
			return;
			
		if (inputNumber.split("\\.").length == 2)
		{
			integerPart = inputNumber.split("\\.")[0];
			decimalPointPart = inputNumber.split("\\.")[1];
			if ((integerPart.equals("0") && inputNumber.length() <= Constant.MAX_LONG_LENGTH + 2) || (!integerPart.equals("0") && inputNumber.length() <= Constant.MAX_LONG_LENGTH + 1))
			{
				integerPart = DataProcessing.getDataProcessing().appendComma(Long.parseLong(integerPart));
				inputNumberDTO.set(integerPart + "." + decimalPointPart);
			}
		}
		else if (inputNumber.contains(".") && inputNumber.length() <= Constant.MAX_LONG_LENGTH + 1)
		{
			integerPart = inputNumber.substring(0, inputNumber.length()-1);
			integerPart = DataProcessing.getDataProcessing().appendComma(Long.parseLong(integerPart));
			inputNumberDTO.set(integerPart + ".");
		}
		else
		{
			if (inputNumber.length() <= Constant.MAX_LONG_LENGTH)
			{
				inputNumberDTO.set(DataProcessing.getDataProcessing().appendComma(Long.parseLong(inputNumber)));
			}
		}
		answerLabel.setText(inputNumberDTO.get());
	}

}
