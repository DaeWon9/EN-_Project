package Controller;

import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;

import javax.swing.JOptionPane;

import org.json.simple.JSONArray;

import Model.DataBase;
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
				mainFrame.BackStage("searchPanel");	
			}
		});
		
		mainFrame.searchResultPanel.backButton.addActionListener(new ActionListener() {
			
			@Override
			public void actionPerformed(ActionEvent e) {
				mainFrame.BackStage("searchPanel");	
				
			}
		});
		
		mainFrame.searchPanel.searchButton.addActionListener(new ActionListener() {
			
			@Override
			public void actionPerformed(ActionEvent e) {
				String data = mainFrame.searchPanel.inputTextFiled.getText(); 
				
				JSONArray imageList = imageSearcher.GetImageList(data);
				if (imageList != null)
				{
					ImagePanel imagePanel = new ImagePanel(imageList, 10);
					mainFrame.getContentPane().removeAll();
					mainFrame.getContentPane().setLayout(null);
					mainFrame.getContentPane().add(mainFrame.searchResultPanel);
					mainFrame.getContentPane().add(imagePanel);
					mainFrame.revalidate();
					mainFrame.repaint();
				}
			}
		});
				
		mainFrame.searchPanel.showLogButton.addActionListener(new ActionListener() {
			
			@Override
			public void actionPerformed(ActionEvent e) {
				mainFrame.getContentPane().removeAll();
				mainFrame.getContentPane().add(mainFrame.logPanel);
				mainFrame.revalidate();
				mainFrame.repaint();
				DataBase dataBase = new DataBase();
				dataBase.AddLog();
			}
		});
	}

}
