package Controller;

import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;

import View.ImagePanel;
import View.MainFrame;
import View.ShowSearchedImage;

public class MainController
{	
	public void Start()
	{
		MainFrame mainFrame = new MainFrame();
		ImageSearcher imageSearcher = new ImageSearcher();
		
		mainFrame.ShowFrame();
		
		mainFrame.logPanel.backButton.addActionListener(new ActionListener() {
			@Override
			public void actionPerformed(ActionEvent e) {
				mainFrame.Change("searchPanel");	
			}
		});
		
		mainFrame.searchResultPanel.backButton.addActionListener(new ActionListener() {
			
			@Override
			public void actionPerformed(ActionEvent e) {
				mainFrame.Change("searchPanel");	
				
			}
		});
		
		mainFrame.searchPanel.searchButton.addActionListener(new ActionListener() {
			
			@Override
			public void actionPerformed(ActionEvent e) {
				String data = mainFrame.searchPanel.inputTextFiled.getText(); 
				System.out.println(data);
				ImagePanel imagePanel = new ImagePanel(imageSearcher.GetImageList(data), 10);
				
				mainFrame.getContentPane().removeAll();
				mainFrame.getContentPane().setLayout(null);
				mainFrame.getContentPane().add(mainFrame.searchResultPanel);
				mainFrame.getContentPane().add(imagePanel);
				mainFrame.revalidate();
				mainFrame.repaint();
			}
		});
				
		mainFrame.searchPanel.showLogButton.addActionListener(new ActionListener() {
			
			@Override
			public void actionPerformed(ActionEvent e) {
				mainFrame.Change("logPanel");					
			}
		});
	}

}
