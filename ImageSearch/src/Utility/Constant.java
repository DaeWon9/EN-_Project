package Utility;

public final class Constant 
{
	public static final String APPLICATION_TITLE = "EN# ImageSearch";
	public static final int APPLICATION_WIDTH = 800;
	public static final int APPLICATION_HEIGHT = 600;
	
	public static final String DATABASE_CONNECTION_INFORMATION = "jdbc:mysql://localhost:3307/daewonImageSearch";
	public static final String DATABASE_USER_NAME = "root";
	public static final String DATABASE_PASSWORD = "0000";
	
	public static final String KAKAO_API_SEARCH_QUERY = "https://dapi.kakao.com/v2/search/image?query=";
	public static final String INSERT_LOG_QUERY = "INSERT INTO log VALUES ('%s', '%s')";
}
