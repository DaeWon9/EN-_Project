package Utility;

public final class Constant 
{
	// 처리안해도됌
	public static final String APPLICATION_TITLE = "EN# ImageSearch";
	public static final int APPLICATION_WIDTH = 800;
	public static final int APPLICATION_HEIGHT = 600;
	
	public static final String DATABASE_CONNECTION_INFORMATION = "jdbc:mysql://localhost:3307/daewonImageSearch";
	public static final String DATABASE_USER_NAME = "root";
	public static final String DATABASE_PASSWORD = "0000";
	
	public static final String KAKAO_API_SEARCH_QUERY = "https://dapi.kakao.com/v2/search/image?query=";
	
	public static final String INSERT_LOG_QUERY = "INSERT INTO log VALUES ('%s', '%s')";
	public static final String SELECT_LOG_QUERY = "SELECT %s FROM LOG ORDER BY searchTime DESC";
	public static final String DELETE_LOG_QUERY = "DELETE FROM log";
	public static final String UPDATE_LOG_QUERY = "UPDATE log SET searchTime = '%s' WHERE searchWord = '%s'";

	//
	public static final String LOG_FILED_ALL = "*";
	public static final String LOG_FILED_SEARCH_WORD = "searchWord";
	public static final String LOG_FILED_SEARCH_TIME = "searchTime";


}
