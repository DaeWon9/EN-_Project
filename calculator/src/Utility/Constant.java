package Utility;

public class Constant 
{

	public enum ButtonIndex
	{ 
		CE(0), C(1), BACK_SPACE(2), DIVISON(3), 
		SEVEN(4), EIGHT(5), NINE(6), MULTIPLY(7), 
		FOUR(8), FIVE(9), SIX(10), SUBTRACTION(11), 
		ONE(12), TWO(13), THREE(14), PLUS(15), 
		NEGATE(16), ZERO(17), POINT(18), CALCULATION(19) ;		
	    private final int index;
	    ButtonIndex(int index) {this.index = index;}  
	    public int getIndex() {return index;}
	}
	
	/*
	public enum ButtonIndex
	{ 
		CE(0), C(1), BACK_SPACE(2), DIVISON(3), 
		SEVEN(4), EIGHT(5), NINE(6), MULTIPLY(7), 
		FOUR(8), FIVE(9), SIX(10), SUBTRACTION(11), 
		ONE(12), TWO(13), THREE(14), PLUS(15), 
		NEGATE(16), ZERO(17), POINT(18), CALCULATION(19) ;		
	}
	*/
	
	/*
	public enum ButtonIndex
	{ 
		CE, C, BACK_SPACE, DIVISON, 
		SEVEN, EIGHT, NINE, MULTIPLY, 
		FOUR, FIVE, SIX, SUBTRACTION, 
		ONE, TWO, THREE, PLUS, 
		NEGATE, ZERO, POINT, CALCULATION ;		
	}
	*/
	

	public static final int MAX_LONG_LENGTH = 16;
	
}