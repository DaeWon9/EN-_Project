package controller;

import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.math.BigDecimal;

import javax.swing.JButton;
import javax.swing.JFrame;
import javax.swing.JLabel;

import Utility.DataProcessing;
import model.AnswerDTO;
import model.InputNumberDTO;
import model.OperandDTO;
import model.OperatorDTO;

public class ExtraButtonListener implements ActionListener
{
	private JFrame mainFrame;
	private JLabel answerLabel;
	private JLabel formulaLabel;
	private AnswerDTO answerDTO;
	private InputNumberDTO inputNumberDTO;
	private OperatorDTO operatorDTO;
	private OperandDTO operandDTO;

	public ExtraButtonListener(JFrame mainFrame, JLabel answerLabel, JLabel formulaLabel, AnswerDTO answerDTO, InputNumberDTO inputNumberDTO, OperatorDTO operatorDTO, OperandDTO operandDTO)
	{
		this.mainFrame = mainFrame;
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
			inputNumberDTO.set("");
			inputNumberDTO.setLast("");
			operatorDTO.set("");
			operatorDTO.setLast("");

			operandDTO.setLeftOperand(null);
			operandDTO.setRightOperand(null);

			answerLabel.setText("0");
			formulaLabel.setText(" ");
		}
		
		else if (((JButton)e.getSource()).getText().equals("CE"))
		{
			if (formulaLabel.getText().contains("=")) // 좌측 오퍼랜드 초기화, 상단에 수식 초기화, 인풋넘버 초기화
			{
				operandDTO.setLeftOperand(null);
				formulaLabel.setText(" ");
			}
			answerDTO.set("0");
			inputNumberDTO.set("");
			answerLabel.setText("0");
		}
		
		else if (((JButton)e.getSource()).getText().equals("⌫"))
		{

			inputNumberDTO.set(DataProcessing.getDataProcessing().deleteComma(inputNumberDTO.get()));
			if (inputNumberDTO.get().length() > 0 && !formulaLabel.getText().equals("  "))//여기 수정
			{
				inputNumberDTO.set(inputNumberDTO.get().substring(0, inputNumberDTO.get().length()-1));
				if(inputNumberDTO.get().equals("") || inputNumberDTO.get().equals("-")) // 공백까지 지워지면 0으로 셋팅
					answerLabel.setText("0");
				else
					answerLabel.setText(DataProcessing.getDataProcessing().numberFormat(inputNumberDTO.get()));
				inputNumberDTO.set(answerLabel.getText());
			}
			else
			{
				formulaLabel.setText("  "); //여기 수정
				inputNumberDTO.set("");
			}
			DataProcessing.getDataProcessing().resizeLabel(mainFrame, answerLabel);
		}
		
		else if (((JButton)e.getSource()).getText().equals("+/-")) // +/-버튼은 숫자 부호 바꾸기 -> 상단에 수식에 negate() 뜨는거 추가해야함.. 
		{	
			if (inputNumberDTO.get().equals("0"))
				answerLabel.setText("0");	
			else if (inputNumberDTO.get().equals("")) // 입력값이 없으면 -> answer에 있는값 부호 바꿔주기
			{
				if (answerDTO.get().equals("0"))
					answerLabel.setText("0");	
				else if (answerDTO.get().charAt(0) == '-') // 첫글자가 -면 삭제
					answerDTO.set(answerDTO.get().substring(1));
				else // -가 아니면 -붙여주기
					answerDTO.set("-" + answerDTO.get());
				answerLabel.setText(DataProcessing.getDataProcessing().numberFormat(DataProcessing.getDataProcessing().deleteComma(answerDTO.get())));	
				if (formulaLabel.getText().contains("negate"))
					formulaLabel.setText("negate(" + formulaLabel.getText().replace("-","") + ")");
				else
					formulaLabel.setText("negate(" + answerDTO.get().replace("-","") + ")");
			}
			else
			{
				if (inputNumberDTO.get().charAt(0) == '-') // 첫글자가 -면 삭제
					inputNumberDTO.set(inputNumberDTO.get().substring(1));
				else // -가 아니면 -붙여주기
					inputNumberDTO.set("-" + inputNumberDTO.get());
				answerLabel.setText(DataProcessing.getDataProcessing().numberFormat(DataProcessing.getDataProcessing().deleteComma(inputNumberDTO.get())));				
				DataProcessing.getDataProcessing().resizeLabel(mainFrame, answerLabel);
			}
		}
		mainFrame.requestFocus();
	}

}
