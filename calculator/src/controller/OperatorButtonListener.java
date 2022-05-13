package controller;

import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import javax.swing.JButton;
import javax.swing.JFrame;
import javax.swing.JLabel;
import Utility.DataProcessing;
import model.AnswerDTO;
import model.OperatorDTO;
import model.InputNumberDTO;
import model.OperandDTO;

public class OperatorButtonListener implements ActionListener
{
	private JFrame mainFrame;
	private JLabel answerLabel;
	private JLabel formulaLabel;
	private AnswerDTO answerDTO;
	private InputNumberDTO inputNumberDTO;
	private OperatorDTO operatorDTO;
	private OperandDTO operandDTO;
	private Calculation calculation = new Calculation();
	
	public OperatorButtonListener(JFrame mainFrame, JLabel answerLabel, JLabel formulaLabel, AnswerDTO answerDTO, InputNumberDTO inputNumberDTO, OperatorDTO operatorDTO, OperandDTO operandDTO)
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
	public void actionPerformed(ActionEvent e)  // operator가 입력되면
	{
		String formulaString = "", calculationResult;		
		checkLastCharIsPoint(); // 숫자입력값 마지막이 . 이면 없애주기 
		operatorDTO.set(((JButton)e.getSource()).getText());
		if (!inputNumberDTO.get().equals(""))
		{
			inputNumberDTO.set(DataProcessing.getDataProcessing().deleteComma(inputNumberDTO.get()));
			inputNumberDTO.set(DataProcessing.getDataProcessing().deleteUnnecessaryDecimalPoint(Double.parseDouble(inputNumberDTO.get())));
			answerLabel.setText(inputNumberDTO.get());
		}	
		
		if (inputNumberDTO.get().equals("")) // 숫자가 입력되지 않고 오퍼레이터가 입력된경우
		{
			if (operatorDTO.get().equals("=") && operatorDTO.getLast().equals("")) // 라스트 오퍼레이터 없이 =만 입력된경우
			{
				formulaString = answerDTO.get() + operatorDTO.get();
				formulaLabel.setText(formulaString);
			}
			
			else if (operatorDTO.get().equals("=") && !operatorDTO.getLast().equals("")) // 라스트 오퍼레이터가 있고 = 가 입력된경우
			{
				operandDTO.setLeftOperand(answerDTO.get());
				if (operandDTO.getRightOperand() == null || operandDTO.getRightOperand().equals(""))
					operandDTO.setRightOperand(answerDTO.get());
				calculationResult = calculation.calculate(operandDTO, operatorDTO);	//계산
				if (!calculationResult.equals("Infinity"))
					calculationResult = DataProcessing.getDataProcessing().appendCommaInString(calculationResult); // 계산결과에 ,추가
				
				answerDTO.set(calculationResult);
				formulaString = operandDTO.getLeftOperand() + operatorDTO.getLast() + operandDTO.getRightOperand() + operatorDTO.get();
				formulaLabel.setText(formulaString);
				answerLabel.setText(answerDTO.get());
			}
			else // 다른오퍼레이터
			{ 
				formulaString = answerDTO.get() + operatorDTO.get();
				formulaLabel.setText(formulaString);
				operatorDTO.setLast(operatorDTO.get());
				operandDTO.setRightOperand(answerDTO.get());
			}
		}
		else // 숫자가 입력되고 오퍼레이터가 입력된 경우
		{
			if (operatorDTO.get().equals("=") && operatorDTO.getLast().equals("")) // 라스트오퍼레이터가 없는상태에서 = 입력받음
			{
				answerDTO.set(inputNumberDTO.get());
				inputNumberDTO.setLast(inputNumberDTO.get());
				formulaString = answerDTO.get() + operatorDTO.get();
				formulaLabel.setText(formulaString);
				answerLabel.setText(answerDTO.get());
			}
			
			else if (operatorDTO.get().equals("=") && !operatorDTO.getLast().equals("")) // 오퍼레이터가 = 리고 라스트 오퍼레이터가있음 -> 평범한 계산
			{
				if (formulaLabel.getText().contains("="))
					operandDTO.setLeftOperand(inputNumberDTO.get());
				else
					operandDTO.setRightOperand(inputNumberDTO.get());
				calculationResult = calculation.calculate(operandDTO, operatorDTO);	//계산
				if (!calculationResult.equals("Infinity"))
					calculationResult = DataProcessing.getDataProcessing().appendCommaInString(calculationResult); // 계산결과에 ,추가		
				answerDTO.set(calculationResult);
				inputNumberDTO.setLast("");
				formulaString = operandDTO.getLeftOperand() + operatorDTO.getLast() + operandDTO.getRightOperand() + operatorDTO.get();
				formulaLabel.setText(formulaString);
				answerLabel.setText(answerDTO.get());
				operandDTO.setLeftOperand(answerDTO.get());
			}
		
			else if (!operatorDTO.get().equals("=") && inputNumberDTO.getLast().equals("")) // 오퍼레이터가 =가 아니고, 라이스인풋값이 없으면 -> 즉 처음입력
			{
				operandDTO.setLeftOperand(inputNumberDTO.get());
				answerDTO.set(inputNumberDTO.get()); ///////////////////////////////////////////////////
				inputNumberDTO.setLast(inputNumberDTO.get());
				operatorDTO.setLast(operatorDTO.get());
				formulaString = operandDTO.getLeftOperand() + operatorDTO.get();
				formulaLabel.setText(formulaString);
			}
			
			else if (!operatorDTO.get().equals("=") && !inputNumberDTO.getLast().equals("")) // 오퍼레이터가 =가 아니고, 라스트 인풋값이 존재하면 -> 즉 두번째로 입력 -> 계산 
			{
				operandDTO.setRightOperand(inputNumberDTO.get());
				
				calculationResult = calculation.calculate(operandDTO, operatorDTO);	//계산
				if (!calculationResult.equals("Infinity"))
					calculationResult = DataProcessing.getDataProcessing().appendCommaInString(calculationResult); // 계산결과에 ,추가		
				answerDTO.set(calculationResult);
				operandDTO.setLeftOperand(answerDTO.get());
				operatorDTO.setLast(operatorDTO.get());
				inputNumberDTO.setLast(inputNumberDTO.get());
				formulaString = answerDTO.get() + operatorDTO.get();
				formulaLabel.setText(formulaString);
				answerLabel.setText(answerDTO.get());
			}
			
			else if (operatorDTO.get().equals("=") && !operatorDTO.getLast().equals("") && inputNumberDTO.getLast().equals("")) // 오퍼레이터가 = 이고 라스트오퍼레이터가 있으며, 라스트인풋이 없을경우
			{
				operandDTO.setLeftOperand(inputNumberDTO.get());
				calculationResult = calculation.calculate(operandDTO, operatorDTO);	//계산
				if (!calculationResult.equals("Infinity"))
					calculationResult = DataProcessing.getDataProcessing().appendCommaInString(calculationResult); // 계산결과에 ,추가		
				answerDTO.set(calculationResult);
				inputNumberDTO.setLast("");
				formulaString = operandDTO.getLeftOperand() + operatorDTO.getLast() + operandDTO.getRightOperand() + operatorDTO.get();
				formulaLabel.setText(formulaString);
				answerLabel.setText(answerDTO.get());
			}
		}	
		inputNumberDTO.set("");
		DataProcessing.getDataProcessing().resizeLabel(mainFrame, answerLabel);
		mainFrame.requestFocus();
	}

	private void checkLastCharIsPoint() // 마지막 문자가 .이면 마지막문자 제거해주기
	{
		char lastChar;
		if (inputNumberDTO.get().length() > 0)
		{
			lastChar = inputNumberDTO.get().charAt(inputNumberDTO.get().length() - 1);
			if (lastChar == '.') // 마지막이 .으로 끝나면
			{
				inputNumberDTO.set(inputNumberDTO.get().substring(0, inputNumberDTO.get().length()-1)); // 마지막 문자 지워주기
				answerLabel.setText(inputNumberDTO.get()); // 라벨새로지정
			}
		}
	}

}
