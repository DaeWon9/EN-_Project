package View;

import java.awt.Color;
import java.awt.Dimension;
import java.awt.FlowLayout;
import java.awt.GridLayout;
import java.awt.Toolkit;
import java.awt.Image;
import java.net.URL;

import javax.swing.ImageIcon;
import javax.swing.JButton;
import javax.swing.JFrame;
import javax.swing.JLabel;
import javax.swing.JPanel;
import javax.swing.border.EmptyBorder;
import javax.imageio.ImageIO;

import org.json.simple.JSONArray;
import org.json.simple.JSONObject;

import java.io.IOException;

import Utility.Constant;

public class ShowSearchedImage extends JFrame
{
	Dimension windowSize = Toolkit.getDefaultToolkit().getScreenSize();
	public ShowSearchedImage(JSONArray jsonArray, int display)
	{
		setTitle(Constant.APPLICATION_TITLE);
		setSize(Constant.APPLICATION_WIDTH, Constant.APPLICATION_HEIGHT);
		setLocation((windowSize.width - Constant.APPLICATION_WIDTH) / 2,
                (windowSize.height - Constant.APPLICATION_HEIGHT) / 2); 
		setDefaultCloseOperation(JFrame.DISPOSE_ON_CLOSE);		
		
		JPanel iamgePanel = new JPanel(new GridLayout(2,5));
		iamgePanel.setBorder(new EmptyBorder(10,10,10,10));
		
		try
		{
			for (int i=0; i<display; i++)
			{
				JSONObject jsonObject = (JSONObject)jsonArray.get(i);
				URL url = new URL (jsonObject.get("image_url").toString());
				Image image = ImageIO.read(url).getScaledInstance(100, 100, Image.SCALE_SMOOTH);
				JButton imageButton = new JButton(new ImageIcon(image));
				imageButton.setSize(50,50);
				iamgePanel.add(imageButton);
			}

		}
		catch (IOException e) {
        	e.printStackTrace();
        }
		add(iamgePanel);

		/*
		JButton imageButton = new JButton(new ImageIcon(image));
		imageButton.setSize(90,20);
		imageButton.setLocation(300, 250);
		add(imageButton);
		*/
		setVisible(true);	
	}
	
	public void CreateImagePanel(JSONArray jsonArray, int display)
	{
		/*
		if (jsonArray.size() > 0)
		{
			for (int i=0; i<display; i++)
			{
				JSONObject jsonObject = (JSONObject)jsonArray.get(i);
				System.out.println(jsonObject.get("image_url"));
			}
		}
		*/
		
		JPanel iamgePanel = new JPanel(new GridLayout(2,5));
		iamgePanel.setBorder(new EmptyBorder(10,10,10,10));
		
		if (jsonArray.size() > 0)
		{
			for (int i=0; i<display; i++)
			{
				JSONObject jsonObject = (JSONObject)jsonArray.get(i);
				ImageIcon imageIcon = new ImageIcon(jsonObject.get("image_url").toString());		
				JButton imageButton = new JButton(imageIcon);
				iamgePanel.add(imageButton);
			}
		}		
		
	}
}
