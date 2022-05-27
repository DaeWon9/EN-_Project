package controller.command;

import controller.CmdAction;

public class Move implements CmdAction
{
	@Override
	public void actionCommand(String inputCommand) 
	{
		//Files.move(new File("C:\\projects\\test").toPath(), new File("C:\\projects\\dirTest").toPath(), StandardCopyOption.REPLACE_EXISTING);
		// TODO Auto-generated method stub
	}
	
	/*
	private boolean move(File sourceFile, File destFile)
	{
	    if (sourceFile.isDirectory())
	    {
	        for (File file : sourceFile.listFiles())
	        {
	            move(file, new File(file.getPath().substring("temp".length()+1)));
	        }
	    }
	    else
	    {
	        try {
	            Files.move(Paths.get(sourceFile.getPath()), Paths.get(destFile.getPath()), StandardCopyOption.REPLACE_EXISTING);
	            return true;
	        } catch (IOException e) {
	            return false;
	        }
	    }
	    return false;
	}
	*/
}
