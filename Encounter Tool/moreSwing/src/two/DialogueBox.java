package two;

import java.awt.*;
import java.awt.event.ActionEvent;
import java.awt.Color;
import java.awt.event.*;
import javax.swing.*;

public class DialogueBox //extends JPanel
{
	//THIS IS MY INTIAL WORK. THE REST IS ME EDITING ENCOUNTERPRACTICE.JAVA SO THAT IT IS IN THE RIGHT PACKAGE.
//	int index;
//	String[] flags;
//	String type;
//	int location;
//	
//	//THIS IS THE CONTAINER WHICH HOLDS ALL THE INFO WHICH IS GONNA BE ON THE FRAME
//	
//	public DialogueBox()
//	{
//		super();
//		location = 0;
//		type = "Dialogue";
//	}
//	
//	public DialogueBox(int location)
//	{
//		this();
//		this.location = location;
//	}
	JTextArea title, introText, option1, option2;
	String[] flagsb1, flagsb2, flagsc1, flagsc2;
	JComboBox flagb1, flagb2, flagc1, flagc2;
	JButton a1;
	Component[] stuff = {title, introText, option1, option2, flagb1, flagb2, flagc1, flagc2, a1};
	String boxName;
	String[] nullValues = {"null","null", "null"};
	String[] boxTexts= {"","","","","","","","","",""};
	//JFrame z; This setup works, but doesn't apply to the OG program.
	
	
    DialogueBox()
    {
//    	title=new JTextArea(300,300);
    	title=new JTextArea("Title (probably already done)");
//    	introText=new JTextArea(300,300);
    	introText=new JTextArea("Intro text");
//    	option1=new JTextArea(300,300);
//    	option2=new JTextArea(300,300);
    	option1=new JTextArea("Second option");
    	option2=new JTextArea("Third option");
    	//title.setBounds(20,10,600,50);
    	//introText.setBounds(20,70,600,250);
    	//option1.setBounds(20,420,300,50);
    	//option2.setBounds(20,490,300,50);
    	title.setLocation(20, 100);
    	title.setSize(100, 20);
    	introText.setLocation(20, 130);
    	introText.setSize(150, 50);
    	
    	a1 = new JButton("Walk Away");
    	a1.setLocation(20, 190);
    	a1.setSize(200, 50);
    	
    	option1.setLocation(20, 250);
    	option1.setSize(200, 50);
    	
    	option2.setLocation(20, 310);
    	option2.setSize(200, 50);
    	
    	//def.setBounds(20, 350, 600, 50);
    	
    	flagsb1 = nullValues;
    	flagsb2 = nullValues;
    	flagsc1 = nullValues;
    	flagsc2 = nullValues;
    	
    	flagb1=new JComboBox(flagsb1);
    	flagb2=new JComboBox(flagsb2);
    	flagc1=new JComboBox(flagsc1);
    	flagc2=new JComboBox(flagsc2);
    	
    	flagb1.setLocation(240, 250);
    	flagb2.setLocation(300, 250);
    	flagc1.setLocation(240, 310);
    	flagc2.setLocation(300, 310);
    	
    	flagb1.setSize(50, 50);
    	flagb2.setSize(50, 50);
    	flagc1.setSize(50, 50);
    	flagc2.setSize(50, 50);
    	
//    	flagb1.setBounds(400,420,100,50);
//    	flagb2.setBounds(520,420,100,50);
//    	flagc1.setBounds(400,490,100,50);
//    	flagc2.setBounds(520,490,100,50);   	
    	
    	
//    	z=new JFrame();
//    	
//        z.add(def);
//        z.add(flagb1);
//        z.add(flagb2);
//        z.add(flagc1);
//        z.add(flagc2);
//        z.add(introText);
//        z.add(title);
//        z.add(option1);
//        z.add(option2);
//        
//    	z.setSize(1200, 800);
//    	z.setLayout(null);
//    	z.setVisible(true);
    }
    
    DialogueBox(String[] flags, String[]flags2, String name)
    {
    	title=new JTextArea(300,300);
    	introText=new JTextArea(300,300);
    	option1=new JTextArea(300,300);
    	option2=new JTextArea(300,300);
    	title.setBounds(20,10,600,50);
    	introText.setBounds(20,70,600,250);
    	option1.setBounds(20,420,300,50);
    	option2.setBounds(20,490,300,50);
    	
    	boxName = name;
    	
    	flagsb1 = flags;
    	flagsb2 = flags2;
    	flagsc1 = flags;
    	flagsc2 = flags2;
    	
    	flagb1=new JComboBox(flagsb1);
    	flagb2=new JComboBox(flagsb2);
    	flagc1=new JComboBox(flagsc1);
    	flagc2=new JComboBox(flagsc2);
    	
    	flagb1.setBounds(400,420,100,50);
    	flagb2.setBounds(520,420,100,50);
    	flagc1.setBounds(400,490,100,50);
    	flagc2.setBounds(520,490,100,50);
    	
    	JButton def=new JButton("Walk Away");
    	def.setBounds(20, 350, 600, 50);
    }
//
//	public static void main(String args[])
//	{
//		new EncounterPractice();
//	}
}

