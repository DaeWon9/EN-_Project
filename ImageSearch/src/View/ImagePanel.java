package View;

import java.awt.Color;
import java.awt.Dimension;
import java.awt.GridLayout;
import java.awt.Image;
import java.awt.Toolkit;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.awt.event.MouseEvent;
import java.awt.event.MouseListener;
import java.io.IOException;
import java.net.MalformedURLException;
import java.net.URL;

import javax.imageio.ImageIO;
import javax.swing.ImageIcon;
import javax.swing.JButton;
import javax.swing.JFrame;
import javax.swing.JLabel;
import javax.swing.JPanel;
import javax.swing.border.EmptyBorder;

import org.json.simple.JSONArray;
import org.json.simple.JSONObject;

import Utility.Constant;


public class ImagePanel extends JPanel
{
	JButton backButton;
	public ImagePanel(JSONArray jsonArray, int display)
	{	
		setBounds(0,200,800,400);
		setBackground(Color.WHITE);
		int coulum = 0;
		int row = 0;
		int interval = 0;
		int imageSize = 0;
		switch (display)
		{
			case 10:
				coulum = 5; 
				row = 2;
				interval = 10;
				imageSize = 80;
				break;
			case 20:
				coulum = 5; 
				row = 4;
				interval = 10;	
				imageSize = 60;
				break;
			case 30:
				coulum = 6; 
				row = 5;
				interval = 10;	
				imageSize = 40;
				break;
		}
		
		JPanel iamgePanel = new JPanel(new GridLayout(row,coulum,interval,interval));
		if (jsonArray != null)
		{
			try
			{
				if (jsonArray.size() >= display)
				{
					for (int i=0; i<display; i++)
					{
						JSONObject jsonObject = (JSONObject)jsonArray.get(i);
						URL url = new URL (jsonObject.get("image_url").toString());
						Image orignalImage = ImageIO.read(url);
						Image image = orignalImage.getScaledInstance(imageSize, imageSize, Image.SCALE_SMOOTH);
						JButton imageButton = new JButton(new ImageIcon(image));
						imageButton.addMouseListener(new MouseListener() {		
							@Override
							public void mouseReleased(MouseEvent e) {		
							}				
							@Override
							public void mousePressed(MouseEvent e) {
							}					
							@Override
							public void mouseExited(MouseEvent e) {
							}
							@Override
							public void mouseEntered(MouseEvent e) {
							}
							@Override
							public void mouseClicked(MouseEvent e) {
								if (e.getClickCount() > 1)
								{
									ShowOriginalImage(orignalImage);
								}	
							}
						});
						imageButton.setBorderPainted(false);
						iamgePanel.add(imageButton);
					}
				}
				else
				{
					for (int i=0; i<jsonArray.size(); i++)
					{
						JSONObject jsonObject = (JSONObject)jsonArray.get(i);
						URL url = new URL (jsonObject.get("image_url").toString());
						Image orignalImage = ImageIO.read(url);
						Image image = orignalImage.getScaledInstance(imageSize, imageSize, Image.SCALE_SMOOTH);
						JButton imageButton = new JButton(new ImageIcon(image));
						imageButton.addMouseListener(new MouseListener() {		
							@Override
							public void mouseReleased(MouseEvent e) {		
							}				
							@Override
							public void mousePressed(MouseEvent e) {
							}					
							@Override
							public void mouseExited(MouseEvent e) {
							}
							@Override
							public void mouseEntered(MouseEvent e) {
							}
							@Override
							public void mouseClicked(MouseEvent e) {
								if (e.getClickCount() > 1)
								{
									ShowOriginalImage(orignalImage);
								}	
							}
						});
						imageButton.setBorderPainted(false);
						iamgePanel.add(imageButton);
					}
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
		}
		add(iamgePanel);
	}
	
	public void ShowOriginalImage(Image image)
	{	
		Dimension windowSize = Toolkit.getDefaultToolkit().getScreenSize();
		JFrame imageFrame = new JFrame();
		imageFrame.setTitle(Constant.APPLICATION_TITLE);
		imageFrame.setSize(image.getWidth(null), image.getHeight(null) );
		imageFrame.setLocation((windowSize.width - Constant.APPLICATION_WIDTH) / 2,
                (windowSize.height - Constant.APPLICATION_HEIGHT) / 2); 
		imageFrame.setDefaultCloseOperation(JFrame.DISPOSE_ON_CLOSE);	
		
		JLabel originalImage = new JLabel(new ImageIcon(image));
		originalImage.setBounds(0,0,800,600);
		imageFrame.add(originalImage);	
		
		imageFrame.setResizable(false);
		imageFrame.setVisible(true);	
	}
}
