package utility;

public class Constant 
{
	//DB Info
	public static final String DATABASE_CONNECTION_INFORMATION = "jdbc:mysql://localhost:3307/daewonSignUp";
	public static final String DATABASE_USER_NAME = "root";
	public static final String DATABASE_PASSWORD = "0000";
	
	public static final String INSERT_QUERY = "INSERT INTO user VALUES ('%s', '%s', '%s', '%s', '%s', '%s', '%s', '%s')";
	public static final String SELECT_QUERY = "SELECT %s FROM user";
	public static final String DELETE_QUERY = "DELETE FROM user WHERE id = '%s'";
	public static final String UPDATE_QUERY = "UPDATE user SET %s = '%s' WHERE id = '%s'";
	
	// user Table field
	public static final String USER_FIELD_ALL = "*";
	public static final String USER_FIELD_NAME = "name";
	public static final String USER_FIELD_ID = "id";
	public static final String USER_FIELD_PASSWORD = "pw";
	public static final String USER_FIELD_BIRTH = "birth";
	public static final String USER_FIELD_EMAIL = "email";
	public static final String USER_FIELD_PHONE_NUMBER = "phonenumber";
	public static final String USER_FIELD_ADDRESS = "address";
	
	// 정규식
	public static final String REGEX_PATTERN_SPECIAL_CHAR = "^[!@#$%^&*()-=_+]$";
	public static final String REGEX_PATTERN_ENGLISTH_NUMBER = "^[0-9a-zA-Z]*$";
	public static final String REGEX_PATTERN_ANY = "^[a-zA-Z0-9ㄱ-ㅎ가-힣\\s!@#$%^&*()-=_+]*$";
	public static final String REGEX_PATTERN_NAME = "^([가-힣]{2,5}|[a-zA-Z]{2,10})$";
	public static final String REGEX_PATTERN_ID = "^[0-9a-zA-Z]{6,16}$";
	public static final String REGEX_PATTERN_PASSWORD = "^(?=.*[A-Za-z])(?=.*\\d)(?=.*[~!@#$%^&*()+|=])[A-Za-z\\d~!@#$%^&*()+|=]{8,16}$";
	public static final String REGEX_PATTERN_PHONE_NUMBER = "^(01[0|1|6|7|8|9]|02|03[1-3]|04[1-4]|05[1-5]|06[1-4])([0-9]{3,4})([0-9]{4})$";
	public static final String REGEX_PATTERN_MIDDLE_PHONE_NUMBER = "^[0-9]{3,4}$";
	public static final String REGEX_PATTERN_LAST_PHONE_NUMBER = "^[0-9]{4}$";
	public static final String REGEX_PATTERN_EMAIL = "^[0-9a-zA-Z]{6,16}@[a-zA-Z]*\\.[a-z]{2,3}$";
	public static final String REGEX_PATTERN_LAST_EMAIL = "^[a-zA-Z]*\\.[a-z]{2,3}$";
	public static final String REGEX_PATTERN_ADDRESS = "^([가-힣]*(시)\\s[가-힣]*(군|구)\\s|[가-힣]*(도)\\s[가-힣]*(시)\\s[가-힣]*(구)\\s)([가-힣0-9]*(읍|면|동)|[가-힣0-9]*(로|길))[가-힣-0-9\\s(),-]*$";
}
