package View;

import java.awt.Color;
import java.awt.GridLayout;
import java.awt.Image;
import java.io.IOException;
import java.net.URL;

import javax.imageio.ImageIO;
import javax.swing.ImageIcon;
import javax.swing.JButton;
import javax.swing.JPanel;
import javax.swing.border.EmptyBorder;

import org.json.simple.JSONArray;
import org.json.simple.JSONObject;

public class ImagePanel extends JPanel
{
	JButton backButton;
	public ImagePanel(JSONArray jsonArray, int display)
	{	
		setBounds(0,200,800,400);
		setBackground(Color.WHITE);
		JPanel iamgePanel = new JPanel(new GridLayout(2,5,10,10));
		try
		{
			for (int i=0; i<display; i++)
			{
				JSONObject jsonObject = (JSONObject)jsonArray.get(i);
				URL url = new URL (jsonObject.get("image_url").toString());
				Image image = ImageIO.read(url).getScaledInstance(100, 100, Image.SCALE_SMOOTH);
				JButton imageButton = new JButton(new ImageIcon(image));
				//imageButton.setSize(100,100);
				imageButton.setBorderPainted(false);
				iamgePanel.add(imageButton);
			}
		}
		catch (IOException e) {
        	e.printStackTrace();
        }
		add(iamgePanel);
	}
}
