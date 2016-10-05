package encounterTool;
import javax.swing.*;

import java.awt.Color;
import java.awt.event.*;
public class EncounterPractice 
{
	JFrame z;
    EncounterPractice()
    {
    	JTextArea title=new JTextArea(300,300);
    	JTextArea introText=new JTextArea(300,300);
    	JTextArea option1=new JTextArea(300,300);
    	JTextArea option2=new JTextArea(300,300);
    	title.setBounds(20,10,600,50);
    	introText.setBounds(20,70,600,250);
    	option1.setBounds(20,420,300,50);
    	option2.setBounds(20,490,300,50);
    	
    	String flagb1data[]={"Flag A","Flag B","Flag C"};
    	String flagb2data[]={"Flag A","Flag B","Flag C"};
    	String flagc1data[]={"Flag A","Flag B","Flag C"};
    	String flagc2data[]={"Flag A","Flag B","Flag C"};
    	JComboBox flagb1=new JComboBox(flagb1data);
    	JComboBox flagb2=new JComboBox(flagb2data);
    	JComboBox flagc1=new JComboBox(flagc1data);
    	JComboBox flagc2=new JComboBox(flagc2data);
    	flagb1.setBounds(400,420,100,50);
    	flagb2.setBounds(520,420,100,50);
    	flagc1.setBounds(400,490,100,50);
    	flagc2.setBounds(520,490,100,50);
    	
    	JButton def=new JButton("Walk Away");
    	def.setBounds(20, 350, 600, 50);
    	
    	z=new JFrame();
    	
        z.add(def);
        z.add(flagb1);
        z.add(flagb2);
        z.add(flagc1);
        z.add(flagc2);
        z.add(introText);
        z.add(title);
        z.add(option1);
        z.add(option2);
        
    	z.setSize(1200, 800);
    	z.setLayout(null);
    	z.setVisible(true);
    }

	public static void main(String args[])
	{
		new EncounterPractice();
	}
}
