import java.awt.*;
import java.awt.event.ActionEvent;
import java.awt.event.KeyEvent;
import java.awt.event.KeyListener;

import javax.swing.*;

class DialogSet 
{
	//This clump of declarations is for the pieces the DialogSet controls
	JTextField name, description;
	JTextField op1, op2, op3;
	JComboBox req1, req2, req3;
	JTextField rtxt1, rtxt2, rtxt3;
	JComboBox rdd1, rdd2, rdd3;
	JLabel design;
	JRadioButton ends1, ends2, ends3;
	JTextField rwd1, rwd2, rwd3;
	JToggleButton y1, y2, y3;
	
	//TXT means text, and DD means "drop down" or combo box. These arrays will be used to initialize everything
	//Also, this is an easy way to group things together for easier initialization
	JTextField[] initializeTXT;
	JComboBox[] initializeDD;
	JTextField[] initializeReactTXT;
	JComboBox[] initializeReactDD;
	JToggleButton[] initializeToggle;
	
	//More misc variables
	int IDNum;
	String numbers;
	int numReqs;
	int[] numReactReqs;
	String[] reqs = {"HasItem", "Currency", "Health", "Time", "Speech", "Growl", "Werewolf-ness", "dayTime", "none"}; //THIS IS AN ATTEMPT TO MAKE ACTUAL REQS
	
		//So far the next two arrays don't really work with the method used in the constructor below
	String[] listofDDReqs = {"Speech", "Growl", "Werewolf-ness", "dayTime"}; //This helps differentiate which Combo Box choices make any combo boxes
	String[] listofBinaries = {"Werewolf-ness", "dayTime"}; //This helps differentiate which Combo Box choices make small combo boxes
		//int reqDivide;
	String[][] reactReqs; 
	String designation; //This is so DialogSets can be called by name in Three
		//int[] reqDD = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10};
	
	//The default constructor shouldn't be used except for helping other constructors (especially the name one)
	DialogSet()
	{
		//utility initialization
		designation = "null";
			//reqDivide = 1;
		//part initialization
		name = new JTextField("Dialog name");
		description = new JTextField("Describe the Dialog");
		design = new JLabel(designation);
			//option boxes
			op1 = new JTextField("Option 1");
			op2 = new JTextField("Option 2");
			op3 = new JTextField("Option 3");
			//combo boxes
			req1 = new JComboBox(reqs);
			req2 = new JComboBox(reqs);
			req3 = new JComboBox(reqs);
		
		//initializing reactReqs
		reactReqs = new String[reqs.length][4];
//		for (int i = 0; i < reqs.length; i++)
//		{
//			for (int j = 0; j < listofDDReqs.length; j++)
//			{
//				if (reqs[i].equals(listofDDReqs[j]))
//				{
//					j = listofDDReqs.length;
//					for (int k = 0; k < listofBinaries.length; k++)
//					{
//						if(reqs[i].equals(listofBinaries[k]))
//						{
//							reactReqs[i][0] = "True";
//							reactReqs[i][1] = "False";
//							k  = listofBinaries.length;
//						}
//						else
//						{
//							reactReqs[i][0] = "1";
//							reactReqs[i][1] = "2";
//							reactReqs[i][2] = "3";
//							reactReqs[i][3] = "4";
//							k = listofBinaries.length;
//						}
//					}
//				}
//			}
//		}
		reactReqs[0][0] = "null";
		reactReqs[1][0] = "null";
		reactReqs[2][0] = "null";
		reactReqs[3][0] = "null";
		
		reactReqs[4][0] = "Speech 1";
		reactReqs[4][1] = "Speech 2";
		reactReqs[4][2] = "Speech 3";
		reactReqs[4][3] = "Speech 4";
		reactReqs[5][0] = "Growl 1";
		reactReqs[5][1] = "Growl 2";
		reactReqs[5][2] = "Growl 3";
		reactReqs[5][3] = "Growl 4";
		reactReqs[6][0] = "Human";
		reactReqs[6][1] = "Werewolf";
		reactReqs[7][0] = "Day";
		reactReqs[7][1] = "Night";
			
			//reactive boxes -----do later? Like, not in a procrastinate-y way, but just have them be defined if the right reqs are picked? Literally later?
				//reactive text
				rtxt1 = new JTextField("How much...");
				rtxt2 = new JTextField("How much...");
				rtxt3 = new JTextField("How much...");
				//reactive combo
				rdd1 = new JComboBox(reactReqs[req1.getSelectedIndex()]);
				rdd2 = new JComboBox(reactReqs[req2.getSelectedIndex()]);
				rdd3 = new JComboBox(reactReqs[req3.getSelectedIndex()]);
		
			//reward boxes
				//bool ends dialogue (buttons?)
				y1 = new JToggleButton("Ends dialog?");
				y2 = new JToggleButton("Ends dialog?");
				y3 = new JToggleButton("Ends dialog?");
				//reactive reward
				rwd1 = new JTextField("Reward");
				rwd2 = new JTextField("Reward");
				rwd3 = new JTextField("Reward");
				
		//array initialization
		initializeTXT = new JTextField[5];
		initializeDD = new JComboBox[3];
		initializeReactTXT = new JTextField[6];
		initializeReactDD = new JComboBox[3];
		initializeToggle = new JToggleButton[3];
		initializeTXT[0] = name;
		initializeTXT[1] = description;
		initializeTXT[2] = op1;
		initializeTXT[3] = op2;
		initializeTXT[4] = op3;
		initializeDD[0] = req1;
		initializeDD[1] = req2;
		initializeDD[2] = req3;
		initializeReactDD[0] = rdd1;
		initializeReactDD[1] = rdd2;
		initializeReactDD[2] = rdd3;
		initializeReactTXT[0] = rtxt1;
		initializeReactTXT[1] = rtxt2;
		initializeReactTXT[2] = rtxt3;
		initializeReactTXT[3] = rwd1;
		initializeReactTXT[4] = rwd2;
		initializeReactTXT[5] = rwd3;
		initializeToggle[0] = y1;
		initializeToggle[1] = y2;
		initializeToggle[2] = y3;
	}
	
	DialogSet(String designation) //Isn't used in Three.java
	{
		this();
		this.designation = designation;
		design = new JLabel(designation);
	}
	
	DialogSet(int id)
	{
		this();
		this.IDNum = id;
	}
	
	DialogSet(int id, String designation)
	{
		this(id);
		this.designation = designation;
		design = new JLabel(designation);
	}
}
