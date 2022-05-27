package controller.command;

import controller.CmdAction;
import model.UserPath;
import view.CmdView;

public class Copy extends Move implements CmdAction
{
	public Copy(UserPath userPath, CmdView cmdView) 
	{
		super(userPath, cmdView);
	}

	@Override
	public void actionCommand(String inputCommand) 
	{
		// TODO Auto-generated method stub
	}
}
