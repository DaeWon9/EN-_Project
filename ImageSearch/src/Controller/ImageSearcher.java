package Controller;
 
import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.net.HttpURLConnection;
import java.net.MalformedURLException;
import java.net.URL;
import java.net.URLEncoder;

import javax.swing.JOptionPane;

import org.json.simple.JSONArray;
import org.json.simple.JSONObject;
import org.json.simple.parser.JSONParser;
import org.json.simple.parser.ParseException;
import Utility.Constant;

public class ImageSearcher 
{
	private JSONObject SearchImage(String query)
	{
		JSONObject responseJson = new JSONObject();
		try
		{
			URL url = new URL(Constant.KAKAO_API_SEARCH_QUERY + URLEncoder.encode(query, "UTF-8"));
			HttpURLConnection connection = (HttpURLConnection) url.openConnection();
			connection.setRequestMethod("GET");
			connection.setRequestProperty("Authorization", "KakaoAK 0a2ee0db0f8fc875f74abcdb7e816265");
			
			int responseCode = connection.getResponseCode();
			if (responseCode == 401)
				System.out.println("401:: Header����");
			else if (responseCode == 400)
				JOptionPane.showMessageDialog(null, "�˻�� �Է����ּ���");
			else if (responseCode == 500)
				JOptionPane.showMessageDialog(null, "Ư�����ڸ� �Է��ϼ̰ų�, ���������Դϴ�");
			else
			{
				BufferedReader reader = new BufferedReader(new InputStreamReader(connection.getInputStream()));
				StringBuilder builder = new StringBuilder();
				String line = "";
				while ((line = reader.readLine()) != null) 
				{
					builder.append(line);
				}
				
				JSONParser jsonParser = new JSONParser();				
				Object object = jsonParser.parse(builder.toString());							
				responseJson = (JSONObject) object;
			}
		}
		catch (MalformedURLException e) 
		{
			e.printStackTrace();
		} 
		catch (IOException e) 
		{
			e.printStackTrace();
		} 
		catch (ParseException e) 
		{
			e.printStackTrace();
		} 
		return responseJson;
	}

	public JSONArray GetImageList(String query)
	{
		JSONObject searchResult = SearchImage(query);
		JSONArray jsonArray = (JSONArray)searchResult.get("documents");
		return jsonArray;
	}
}
