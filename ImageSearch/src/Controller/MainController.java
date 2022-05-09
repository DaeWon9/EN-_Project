package Controller;

import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;

import javax.swing.JOptionPane;

import org.json.simple.JSONArray;
import Model.LogDAO;
import Model.SearchWordDTO;
import View.ImagePanel;
import View.MainFrame;

public class MainController
{	
	public void Start()
	{
		MainFrame mainFrame = new MainFrame();
		ImagePanel imagePanel = new ImagePanel();
		ImageSearcher imageSearcher = new ImageSearcher();
		SearchWordDTO searchWordDTO = new SearchWordDTO();
		LogDAO logDAO = new LogDAO();
		LogManagement logManagement = new LogManagement();
		
		mainFrame.ShowFrame();
		
		mainFrame.searchResultPanel.backButton.addActionListener(new ActionListener() {	
			@Override
			public void actionPerformed(ActionEvent e) {
				mainFrame.BackStage("searchPanel");		
			}
		});
		
		mainFrame.searchResultPanel.searchButton.addActionListener(new ActionListener() {		
			@Override
			public void actionPerformed(ActionEvent e) {
				String data = mainFrame.searchResultPanel.inputTextFiled.getText(); 
				int display;
				JSONArray imageList = imageSearcher.GetImageList(data);
				if (imageList != null)
				{
					searchWordDTO.Set(data);
					logDAO.AddLog(data);
					display = Integer.parseInt(mainFrame.searchResultPanel.displayBox.getSelectedItem().toString());
					mainFrame.getContentPane().removeAll();
					mainFrame.getContentPane().setLayout(null);
					mainFrame.getContentPane().add(mainFrame.searchResultPanel);
					mainFrame.getContentPane().add(imagePanel.CreateImagePanel(imageList, display));
					mainFrame.revalidate();
					mainFrame.repaint();
				}	
			}
		});

		mainFrame.searchResultPanel.displayBox.addActionListener(new ActionListener() {	
			@Override
			public void actionPerformed(ActionEvent e) 
			{
				String data = searchWordDTO.Get(); 
				int display;
				mainFrame.searchResultPanel.displayBox.getSelectedItem().toString();
				JSONArray imageList = imageSearcher.GetImageList(data);
				if (imageList != null)
				{
					searchWordDTO.Set(data);
					logDAO.AddLog(data);
					display = Integer.parseInt(mainFrame.searchResultPanel.displayBox.getSelectedItem().toString());
					mainFrame.getContentPane().removeAll();
					mainFrame.getContentPane().setLayout(null);
					mainFrame.getContentPane().add(mainFrame.searchResultPanel);
					mainFrame.getContentPane().add(imagePanel.CreateImagePanel(imageList, display));
					mainFrame.revalidate();
					mainFrame.repaint();
				}
			}
		});
		
		mainFrame.searchPanel.searchButton.addActionListener(new ActionListener() {		
			@Override
			public void actionPerformed(ActionEvent e) {
				String data = mainFrame.searchPanel.inputTextFiled.getText(); 
				if (data.length() > 100)
				{
					JOptionPane.showMessageDialog(null, "100글자 이내로 입력해주세요");
					return;
				}
				JSONArray imageList = imageSearcher.GetImageList(data);
				if (imageList != null)
				{
					searchWordDTO.Set(data);
					logDAO.AddLog(data);
					mainFrame.searchResultPanel.inputTextFiled.setText(""); 
					mainFrame.getContentPane().removeAll();
					mainFrame.getContentPane().setLayout(null);
					mainFrame.getContentPane().add(mainFrame.searchResultPanel);
					mainFrame.searchResultPanel.displayBox.setSelectedIndex(0);
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
				logManagement.LogInputToLogPanel(logDAO, mainFrame.logPanel.txtLog);
				mainFrame.revalidate();
				mainFrame.repaint();
			}
		});
		
		mainFrame.logPanel.backButton.addActionListener(new ActionListener() {
			@Override
			public void actionPerformed(ActionEvent e) {
				mainFrame.BackStage("searchPanel");	
			}
		});

		mainFrame.logPanel.deleteButton.addActionListener(new ActionListener() {
			@Override
			public void actionPerformed(ActionEvent e) {
				logDAO.DeleteAllLog();
				mainFrame.getContentPane().removeAll();
				logManagement.LogInputToLogPanel(logDAO, mainFrame.logPanel.txtLog);
				mainFrame.getContentPane().add(mainFrame.logPanel);
				mainFrame.revalidate();
				mainFrame.repaint();
			}
		});
	}

}
