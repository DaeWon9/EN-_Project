package Controller;

import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;

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
				mainFrame.Change("searchResultPanel");	
				ShowSearchedImage showSearchedImage = new ShowSearchedImage(imageSearcher.GetImageList("python"), 10);
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
