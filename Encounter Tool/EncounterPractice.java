package encounterTool;
import javax.swing.*;

import java.awt.Color;
import java.awt.event.*;
public class EncounterPractice 
{
	JTextArea area;
	JFrame z;
	JButton p;
    EncounterPractice()
    {
    	String arrs[]={"Pick Me","No Pick ME","|||||||||||","What?"};
    	JComboBox com=new JComboBox(arrs);
    	com.setBounds(1000,600,200,40);
    	area=new JTextArea(400,400);
    	area.setBounds(10,30,300,300);
    	area.setBackground(Color.black);
    	area.setForeground(Color.green);
    	p=new JButton("This is my Encounter Practice, Its not functional!!!");
    	p.setBounds(400, 400, 400, 400);
    	z=new JFrame();
    	z.add(p);
    	z.add(area);
    	z.add(com);
    	z.setSize(200, 200);
    	z.setLayout(null);
    	z.setVisible(true);
    }

	public static void main(String args[])
	{
		new EncounterPractice();
	}
}
