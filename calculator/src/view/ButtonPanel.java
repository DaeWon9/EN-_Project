package view;

import java.awt.Color;
import java.awt.Dimension;
import java.awt.Font;
import java.awt.GridLayout;

import javax.swing.JButton;
import javax.swing.JLabel;
import javax.swing.JPanel;
import javax.swing.SwingConstants;

import Utility.Constant;

public class ButtonPanel extends JPanel
{
	public JButton[] button;
	
	public ButtonPanel()
	{
		Font buttonFont = new Font("맑은 고딕", 0, 20);
		button = new JButton[20]; //버튼 20개임
		setLayout(new GridLayout(5,4,2,2)); // 5행4열로 배치하고 간격 2로 설정
		setBackground(Color.GRAY);
		
		button[Constant.ButtonIndex.CE.getIndex()] = new JButton("CE");
		button[Constant.ButtonIndex.C.getIndex()] = new JButton("C");
		button[Constant.ButtonIndex.BACK_SPACE.getIndex()] = new JButton("<-");
		button[Constant.ButtonIndex.DIVISON.getIndex()] = new JButton("÷");
		button[Constant.ButtonIndex.SEVEN.getIndex()] = new JButton("7");
		button[Constant.ButtonIndex.EIGHT.getIndex()] = new JButton("8");
		button[Constant.ButtonIndex.NINE.getIndex()] = new JButton("9");
		button[Constant.ButtonIndex.MULTIPLY.getIndex()] = new JButton("x");
		button[Constant.ButtonIndex.FOUR.getIndex()] = new JButton("4");
		button[Constant.ButtonIndex.FIVE.getIndex()] = new JButton("5");
		button[Constant.ButtonIndex.SIX.getIndex()] = new JButton("6");
		button[Constant.ButtonIndex.SUBTRACTION.getIndex()] = new JButton("-");
		button[Constant.ButtonIndex.ONE.getIndex()] = new JButton("1");
		button[Constant.ButtonIndex.TWO.getIndex()] = new JButton("2");
		button[Constant.ButtonIndex.THREE.getIndex()] = new JButton("3");
		button[Constant.ButtonIndex.PLUS.getIndex()] = new JButton("+");
		button[Constant.ButtonIndex.NEGATE.getIndex()] = new JButton("+/-");
		button[Constant.ButtonIndex.ZERO.getIndex()] = new JButton("0");
		button[Constant.ButtonIndex.POINT.getIndex()] = new JButton(".");
		button[Constant.ButtonIndex.CALCULATION.getIndex()] = new JButton("=");
		
		for (int repeat = 0; repeat <20; repeat++)
		{
			button[repeat].setFont(buttonFont);
			button[repeat].setForeground(Color.white);
			button[repeat].setBackground(Color.DARK_GRAY);
			add(button[repeat]);
		}
		
	}
}
