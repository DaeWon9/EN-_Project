package utility;

public class Constant 
{
	//DB Info
	public static final String DATABASE_CONNECTION_INFORMATION = "jdbc:mysql://localhost:3307/daewonSignUp";
	public static final String DATABASE_USER_NAME = "root";
	public static final String DATABASE_PASSWORD = "0000";
	
	// 정규식
	public static final String REGEX_PATTERN_NAME = "^([가-힣]{2,5}|[a-zA-Z]{2,10})$";
	public static final String REGEX_PATTERN_ID = "^[0-9a-zA-Z]{6,16}$";
	public static final String REGEX_PATTERN_PASSWORD = "^(?=.*[A-Za-z])(?=.*\\d)(?=.*[~!@#$%^&*()+|=])[A-Za-z\\d~!@#$%^&*()+|=]{8,16}$";
	public static final String REGEX_PATTERN_PHONE_NUMBER = "^[0-9]{3,4}*$";
}
