package utility;

public class Constant 
{
	//DB Info
	public static final String DATABASE_CONNECTION_INFORMATION = "jdbc:mysql://localhost:3307/daewonSignUp";
	public static final String DATABASE_USER_NAME = "root";
	public static final String DATABASE_PASSWORD = "0000";
	
	public static final String INSERT_QUERY = "INSERT INTO user VALUES ('%s', '%s')";
	public static final String SELECT_QUERY = "SELECT %s FROM user";
	public static final String DELETE_QUERY = "DELETE FROM user WHERE id = '%s'";
	public static final String UPDATE_QUERY = "UPDATE user SET %s = '%s' WHERE id = '%s'";
	
	//
	public static final String USER_FILED_ALL = "*";
	public static final String USER_FILED_NAME = "name";
	public static final String USER_FILED_ID = "id";
	public static final String USER_FILED_PASSWORD = "pw";
	public static final String USER_FILED_BIRTH = "birth";
	public static final String USER_FILED_EMAIL = "email";
	public static final String USER_FILED_PHONE_NUMBER = "phonenumber";
	public static final String USER_FILED_ADDRESS = "address";
	
	// 정규식
	public static final String REGEX_PATTERN_NAME = "^([가-힣]{2,5}|[a-zA-Z]{2,10})$";
	public static final String REGEX_PATTERN_ID = "^[0-9a-zA-Z]{6,16}$";
	public static final String REGEX_PATTERN_PASSWORD = "^(?=.*[A-Za-z])(?=.*\\d)(?=.*[~!@#$%^&*()+|=])[A-Za-z\\d~!@#$%^&*()+|=]{8,16}$";
	public static final String REGEX_PATTERN_PHONE_NUMBER = "^[0-9]{3,4}*$";
}
