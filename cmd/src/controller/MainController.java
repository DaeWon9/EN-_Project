package controller;

import model.UserPath;
import utility.DataProcessing;
import view.CmdView;
import view.Message;

public class MainController 
{
	private Message message = new Message();
	private CmdView cmdView = new CmdView();
	private UserPath userPath = new UserPath(System.getProperty("user.home"));
	
	public void start()
	{
		message.printVersion();
		message.printCurrentPath(userPath.get());
		//System.out.println(DataProcessing.get().getInputString());
		message.printHelpMessage();
	}
}
