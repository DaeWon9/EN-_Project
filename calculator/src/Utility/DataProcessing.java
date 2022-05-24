package Utility;

import java.awt.Font;
import java.math.BigDecimal;
import java.text.DecimalFormat;
import java.text.NumberFormat;
import java.util.ArrayList;

import javax.swing.JFrame;
import javax.swing.JLabel;
import javax.swing.JScrollBar;

import view.MainFrame;


public class DataProcessing 
{
	private static DataProcessing dataProcessing = new DataProcessing();
	
	public static DataProcessing getDataProcessing()
	{
		return dataProcessing;
	}
	
	public int countChar(String targetString, char targetChar) // 문자열에 특정문자의 개수 반환하는 함수
	{
		int count = 0;
		for (int index = 0; index < targetString.length(); index++)
		{
			if (targetString.charAt(index) == targetChar)
				count++;
		}
		return count;
	}
	
	public String appendCommaInLong(Long number) // 숫자에 ,추가하는 함수
	{
		String formatResult= "";
		NumberFormat numberFormat = NumberFormat.getNumberInstance();
		formatResult = numberFormat.format(number);
		return formatResult;
	}

	public String formatFormulaOperand(String operandString)
	{
		String formatString = "";
		if (operandString.contains("negate"))
			formatString = deleteComma(operandString);
		else
			formatString = numberFormat(operandString);
		return formatString;
	}
	
	public String numberFormat(String numberString)
	{
		String decimalFormatFormString;
		if (numberString.equals("0으로 나눌 수 없습니다"))
			return "0으로 나눌 수 없습니다";
		else if (numberString.equals("정의되지 않은 결과입니다."))
			return "정의되지 않은 결과입니다.";
		else if (numberString.equals("오버플로"))
			return "오버플로";
		String formatResult= "";
		BigDecimal bigDeciaml = new BigDecimal(deleteComma(numberString));
		DecimalFormat decimalFormat = new DecimalFormat(",###.################");
		DecimalFormat exponentialFormat = new DecimalFormat("0.###############E0");

		if (bigDeciaml.compareTo(new BigDecimal("9.999999999999999e+9999")) > 0 || bigDeciaml.compareTo(new BigDecimal("-9.999999999999999e+9999")) < 0 || (bigDeciaml.compareTo(new BigDecimal("1e-9999")) < 0 && bigDeciaml.compareTo(BigDecimal.ZERO) > 0) || (bigDeciaml.compareTo(new BigDecimal("-1e-9999")) > 0 && bigDeciaml.compareTo(BigDecimal.ZERO) < 0))
			formatResult = "오버플로";
		else if (bigDeciaml.compareTo(new BigDecimal("10000000000000000")) >= 0 || bigDeciaml.compareTo(new BigDecimal("-10000000000000000")) <= 0)
			formatResult = (exponentialFormat.format(bigDeciaml)).replace("E", "e+");
		else if (((bigDeciaml.compareTo(new BigDecimal("0.001")) < 0 && bigDeciaml.compareTo(BigDecimal.ZERO) > 0 ) || (bigDeciaml.compareTo(new BigDecimal("-0.001")) > 0) && bigDeciaml.compareTo(BigDecimal.ZERO) < 0 ) && bigDeciaml.toPlainString().replace("-","").length() > 18)
			formatResult = (exponentialFormat.format(bigDeciaml)).replace("E-", "e-");
		else
		{
			if(bigDeciaml.toPlainString().length() > 18)
			{
				decimalFormatFormString = ",###.";
				for (int repeat = 0; repeat < 18 - 1 - bigDeciaml.toPlainString().split("\\.")[0].length(); repeat++)
				{
					decimalFormatFormString += "#";
				}
				decimalFormat = new DecimalFormat(decimalFormatFormString);
			}
			formatResult = decimalFormat.format(bigDeciaml);
		}
		
		if (formatResult.contains("e") && !formatResult.contains("."))
			formatResult = formatResult.replace("e", ".e");
		return formatResult;
	}
	
	public String inputNumberFormat(String inputNumberString)
	{
		String integerPart;
		String decimalPointPart;
		String resultString = "";
		if (inputNumberString.split("\\.").length == 2) // point로 쪼갰을 때 정수부와 실수부로 쪼개진다면, 나눠서 ,포멧 처리후 합쳐주기 (정수부가 0이면 소수부는 16자리까지, 그외에는 합쳐서 16자리)
		{
			integerPart = inputNumberString.split("\\.")[0];
			decimalPointPart = inputNumberString.split("\\.")[1];
			if ((integerPart.equals("0") && inputNumberString.length() <= Constant.MAX_LONG_LENGTH + 2) || (!integerPart.equals("0") && inputNumberString.length() <= Constant.MAX_LONG_LENGTH + 1))
			{
				integerPart = DataProcessing.getDataProcessing().appendCommaInLong(Long.parseLong(integerPart));
				resultString = integerPart + "." + decimalPointPart;
			}
		}
		else if (inputNumberString.contains(".") && inputNumberString.length() <= Constant.MAX_LONG_LENGTH + 1)
		{
			integerPart = inputNumberString.substring(0, inputNumberString.length()-1);
			integerPart = DataProcessing.getDataProcessing().appendCommaInLong(Long.parseLong(integerPart)); 
			resultString = integerPart + ".";
		}
		else
		{
			if (inputNumberString.length() <= Constant.MAX_LONG_LENGTH)
				resultString = numberFormat(inputNumberString);
		}
		return resultString;
	}
	
	public String deleteComma(String str) // 문자열 숫자에 ,제거하는 함수
	{
		return str.replace(",", "");
	}
	
	public String deleteNegateMark(String str)
	{
		return str.replace("negate(", "").replace(")", "").replace(",", "").replace(" ", "");
	}

	public void resizeLabel(MainFrame mainFrame)
	{
		int minimumSize;
		minimumSize = setMininumTextSize(mainFrame);
		mainFrame.textPanel.answer.setFont(new Font("맑은 고딕", 0, 45));
				
		while (mainFrame.textPanel.answer.getPreferredSize().width > minimumSize)
		{
			mainFrame.textPanel.answer.setFont(new Font("맑은 고딕", 0, mainFrame.textPanel.answer.getFont().getSize() - 1));
			if (mainFrame.textPanel.answer.getFont().getSize() < 1)
				break;
		}
	}
	
	private int setMininumTextSize(MainFrame mainFame)
	{
		int minimumSize;
		if (mainFame.getWidth() > 580)
			minimumSize = mainFame.getSize().width - 280 - 20; 
		else
			minimumSize = mainFame.getSize().width - 20;
		
		return minimumSize;
	}
	
	public void setArrowButtonVisible(MainFrame mainFrame)
	{
		JScrollBar formulaScroll;
		int formulaStringWidth = mainFrame.textPanel.formula.getPreferredSize().width;
		int textPanelWidth = mainFrame.textPanel.getSize().width;

		if (formulaStringWidth > textPanelWidth)
		{
			mainFrame.textPanel.leftArrowButton.setVisible(true);
			mainFrame.textPanel.rightArrowButton.setVisible(true);
			mainFrame.revalidate();
			formulaScroll = mainFrame.textPanel.formulaScroll.getHorizontalScrollBar();
			formulaScroll.setValue(formulaScroll.getMaximum());
		}
		else
		{
			mainFrame.textPanel.leftArrowButton.setVisible(false);
			mainFrame.textPanel.rightArrowButton.setVisible(false);
		}
	}
}
