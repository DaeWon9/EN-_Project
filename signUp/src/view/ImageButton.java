package view;

import javax.swing.ImageIcon;
import javax.swing.JButton;

public class ImageButton extends JButton
{
	private JButton button;
	
	public ImageButton(ImageIcon image, int posX, int posY)
	{
		this.button = new JButton(image);
		button.setSize(image.getIconWidth(), image.getIconHeight());
		button.setLocation(posX, posY);
		button.setBorderPainted(false);
		button.setBorderPainted(false);
		button.setFocusPainted(false); 
		button.setContentAreaFilled(false);	
	}
	
	public JButton get()
	{
		return this.button;
	}
}
