package Utility;

import java.awt.Font;
import java.math.BigDecimal;
import java.text.DecimalFormat;
import java.text.NumberFormat;

import javax.swing.JFrame;
import javax.swing.JLabel;

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

	
	public String numberFormat(String numberString)
	{
		if (numberString.equals("0으로 나눌 수 없습니다"))
			return "0으로 나눌 수 없습니다";
		else if (numberString.equals("정의되지 않은 결과입니다."))
			return "정의되지 않은 결과입니다.";
		else if (numberString.equals("오버플로"))
			return "오버플로";
		String formatResult= "";
		BigDecimal bigDeciaml = new BigDecimal(numberString);
		DecimalFormat decimalFormat = new DecimalFormat(",###.################");
		DecimalFormat exponentialFormat = new DecimalFormat("0.###############E0");

		if (bigDeciaml.compareTo(new BigDecimal("9.999999999999999e+9999")) > 0 || bigDeciaml.compareTo(new BigDecimal("-9.999999999999999e+9999")) < 0 || (bigDeciaml.compareTo(new BigDecimal("1e-9999")) < 0 && bigDeciaml.compareTo(BigDecimal.ZERO) > 0) || (bigDeciaml.compareTo(new BigDecimal("-1e-9999")) > 0 && bigDeciaml.compareTo(BigDecimal.ZERO) < 0))
			formatResult = "오버플로";
		else if (bigDeciaml.compareTo(new BigDecimal("10000000000000000")) > 0 || bigDeciaml.compareTo(new BigDecimal("-10000000000000000")) < 0)
			formatResult = (exponentialFormat.format(bigDeciaml)).replace("E", "e+");
		else if ((bigDeciaml.compareTo(new BigDecimal("0.0000000000000001")) < 0 && bigDeciaml.compareTo(BigDecimal.ZERO) > 0 ) || (bigDeciaml.compareTo(new BigDecimal("-0.0000000000000001")) > 0 && bigDeciaml.compareTo(BigDecimal.ZERO) < 0 ))
			formatResult = (exponentialFormat.format(bigDeciaml)).replace("E-", "e-");
		else
			formatResult = decimalFormat.format(bigDeciaml).replace("E", "e");
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
			{
				resultString = numberFormat(inputNumberString);
			}
		}
		return resultString;
	}
	

	public String deleteComma(String str) // 문자열 숫자에 ,제거하는 함수
	{
		return str.replace(",", "");
	}

	public void resizeLabel(JFrame frame, JLabel label)
	{
		int minimumSize;
		minimumSize = setMininumTextSize(frame);
		label.setFont(new Font("맑은 고딕", 0, 45));
				
		if (label.getText().matches(Constant.EXCEPTION_TYPE_KOREAN))
		{
			label.setFont(new Font("맑은 고딕", 0, 25));
			return;
		}
		while (label.getFont().getSize()/2 * label.getText().length() > minimumSize)
		{
			label.setFont(new Font("맑은 고딕", 0, label.getFont().getSize() - 1));
			if (label.getFont().getSize() < 1)
				break;
		}
	}
	
	private int setMininumTextSize(JFrame frame)
	{
		int minimumSize;
		if (frame.getWidth() > 580)
			minimumSize = frame.getWidth() - 300 - 70; 
		else
			minimumSize = frame.getWidth() - 70;
		
		return minimumSize;
	}
	
	
	private boolean isScrollbarVisible(MainFrame mainFrame)
	{
		return mainFrame.textPanel.formulaScroll.getHorizontalScrollBar().isVisible();
	}
	
	public void setArrowButtonVisible(MainFrame mainFrame)
	{
		if (isScrollbarVisible(mainFrame))
		{
			mainFrame.textPanel.leftArrowButton.setVisible(true);
			mainFrame.textPanel.rightArrowButton.setVisible(true);
		}

		else
		{
			mainFrame.textPanel.leftArrowButton.setVisible(false);
			mainFrame.textPanel.rightArrowButton.setVisible(false);
		}
		mainFrame.revalidate();
		mainFrame.repaint();
	}
	
	
	
	
	
	
}
