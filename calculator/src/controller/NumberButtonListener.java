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
		inputNumber = DataProcessing.getDataProcessing().deleteComma(inputNumberDTO.get());
		if (inputNumber.length() < Constant.MAX_LONG_LENGTH)
		{
			inputNumber += ((JButton)e.getSource()).getText();
			inputNumberDTO.set(DataProcessing.getDataProcessing().appendComma(Long.parseLong(inputNumber)));
		}
		answerLabel.setText(inputNumberDTO.get());
	}

}
