package two;

import java.awt.*;
import java.awt.event.ActionEvent;

import javax.swing.*;

public class FlagBox extends JInternalFrame
{
	String[] flags;
	int flagsIndex;
	JList<String> list = new JList<String>(flags);
	String type;
	
	//THIS WILL BE THE CONTAINER THAT HOLDS ALL THE FLAGS
	
	public FlagBox()
	{
		JScrollBar bar = new JScrollBar(0);
		this.type = "null";
		setSize(500, 500);
		setLocation(3000, 700);
	}
	
	public FlagBox(String type)
	{
		this();
		if (type.equals("Dialogue"))
		{
			this.type = "Dialogue";
		}
	}

	public String[] getFlags()
	{
		return flags;
	}
}
