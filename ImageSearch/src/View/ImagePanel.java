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
import javax.swing.JOptionPane;
import javax.swing.JPanel;
import javax.swing.border.EmptyBorder;

import org.json.simple.JSONArray;
import org.json.simple.JSONObject;

import Utility.Constant;


public class ImagePanel extends JPanel
{
	public JPanel CreateImagePanel(JSONArray jsonArray, int display)
	{	
		JPanel panel = new JPanel();
		panel.setBounds(0,170,800,400);
		panel.setBackground(Color.WHITE);
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
		iamgePanel.setBackground(Color.WHITE);
		for (int i=0; i<display; i++)
		{
			try
			{
				JSONObject jsonObject = (JSONObject)jsonArray.get(i);
				System.out.println(jsonObject.get("image_url").toString());
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
				imageButton.setContentAreaFilled(false);
				iamgePanel.add(imageButton);
			}
			catch (MalformedURLException e) 
			{
				e.printStackTrace();
			} 
			catch (IOException e) 
			{
				display++;
			} 
			catch (NullPointerException e)
			{
				display++;
			}
			catch (IndexOutOfBoundsException e)
			{
				JOptionPane.showMessageDialog(null, "해당 개수만큼 출력하지 못했습니다.");
				break;
			}
		}
		panel.add(iamgePanel);
		
		return panel;
	}
	
	public void ShowOriginalImage(Image image)
	{	
		Dimension windowSize = Toolkit.getDefaultToolkit().getScreenSize();
		JFrame imageFrame = new JFrame();
		ImageIcon iconImage = new ImageIcon(MainFrame.class.getResource("/Image/Ryan.png"));		
		imageFrame.setIconImage(iconImage.getImage());
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
