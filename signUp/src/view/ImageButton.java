package view;

import java.awt.Image;
import java.awt.event.MouseEvent;
import java.awt.event.MouseListener;

import javax.swing.ImageIcon;
import javax.swing.JButton;
import javax.swing.JLabel;

import view.frame.MainFrame;
import view.panel.Login;

public class ImageButton extends JButton
{
	private JButton button;	
	
	public ImageButton(ImageIcon image, ImageIcon secondImage, int posX, int posY)
	{
		this.button = new JButton(image);
		button.setSize(image.getIconWidth(), image.getIconHeight());
		button.setLocation(posX, posY);
		button.setBorderPainted(false);
		button.setBorderPainted(false);
		button.setFocusPainted(false); 
		button.setContentAreaFilled(false);	
		
		button.addMouseListener(new MouseListener() {
			
			@Override
			public void mouseReleased(MouseEvent e) {
				if (secondImage != null) {
					button.setIcon(image);
				}
			}
			
			@Override
			public void mousePressed(MouseEvent e) {
				if (secondImage != null) {
					button.setIcon(secondImage);
				}
			}
			
			@Override
			public void mouseExited(MouseEvent e) {
				if (secondImage != null) {
					button.setIcon(image);
				}
			}
			
			@Override
			public void mouseEntered(MouseEvent e) {
				if (secondImage != null) {
					button.setIcon(secondImage);
				}
			}
			
			@Override
			public void mouseClicked(MouseEvent e) {
				if (secondImage != null) {
					button.setIcon(image);
				}
			}
		});
	}
	
	
	public JButton get()
	{
		return this.button;
	}
}
