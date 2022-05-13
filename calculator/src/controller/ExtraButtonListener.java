package controller;

import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;

import javax.swing.JButton;
import javax.swing.JLabel;

import model.AnswerDTO;
import model.InputNumberDTO;
import model.OperandDTO;
import model.OperatorDTO;

public class ExtraButtonListener implements ActionListener
{
	private JLabel answerLabel;
	private JLabel formulaLabel;
	private AnswerDTO answerDTO;
	private InputNumberDTO inputNumberDTO;
	private OperatorDTO operatorDTO;
	private OperandDTO operandDTO;

	public ExtraButtonListener(JLabel answerLabel, JLabel formulaLabel, AnswerDTO answerDTO, InputNumberDTO inputNumberDTO, OperatorDTO operatorDTO, OperandDTO operandDTO)
	{
		this.answerLabel = answerLabel;
		this.formulaLabel = formulaLabel;
		this.answerDTO = answerDTO;
		this.inputNumberDTO = inputNumberDTO;
		this.operatorDTO = operatorDTO;
		this.operandDTO = operandDTO;
	}
	@Override
	public void actionPerformed(ActionEvent e) 
	{
		if (((JButton)e.getSource()).getText().equals("C")) // C버튼은 전부 초기화
		{
			answerDTO.set("0");
			inputNumberDTO.set("0");
			inputNumberDTO.setLast("");
			operatorDTO.set("");
			operatorDTO.setLast("");
			operandDTO.setLeftOperand("");
			operandDTO.setRightOperand("");
			answerLabel.setText("0");
			formulaLabel.setText(" ");
		}
		
		else if (((JButton)e.getSource()).getText().equals("CE")) // CE버튼은 최근 입력삭제....?
		{
			answerDTO.set("0");
			inputNumberDTO.set("0");
			answerLabel.setText("0");
		}
		
		else if (((JButton)e.getSource()).getText().equals("<-"))
		{
			/*
			input
			integerPart = inputNumber.substring(0, inputNumber.length()-1);
			answerLabel.setText(inputNumberDTO.get());
			*/
		}
	}

}
