import java.awt.*;
import java.awt.event.ActionEvent;
import java.awt.event.KeyEvent;
import java.awt.event.KeyListener;

import javax.swing.*;

public class DialogSet 
{
	//This clump of declarations is for the pieces the DialogSet controls
	JTextField name, description;
	JTextField op1, op2, op3;
	JComboBox req1, req2, req3;
	JTextField rtxt1, rtxt2, rtxt3;
	JComboBox rdd1, rdd2, rdd3;
	
	//TXT means text, and DD means "drop down" or combo box. These arrays will be used to initialize everything
	JTextField[] initializeTXT;
	JComboBox[] initializeDD;
	
	//More misc variables
	String numbers;
	int numReqs;
	int[] numReactReqs;
	String[] reqs = {"What", "Is", "Going", "On"}; //THIS WILL BE ROTATED OUT WHEN WE GET ACTUAL REQS
	String[][] reactReqs; 
	String designation; //This is so DialogSets can be called by name in Three
	
	//The default constructor shouldn't be used except for helping other constructors (especially the name one)
	public DialogSet()
	{
		//utility initialization
		designation = "null";
		
		//part initialization
		name = new JTextField("Dialog name");
		description = new JTextField("Describe the Dialog");
			//option boxes
			op1 = new JTextField("Option 1");
			op2 = new JTextField("Option 2");
			op3 = new JTextField("Option 3");
			//combo boxes
			req1 = new JComboBox(reqs);
			req2 = new JComboBox(reqs);
			req3 = new JComboBox(reqs);
			
			
//			//reactive boxes -----do later? Like, not in a procrastinate-y way, but just have them be defined if the right reqs are picked? Literally later?
//				//reactive text
//				rtxt1 = new JTextField("How much...");
//				rtxt2 = new JTextField("How much...");
//				rtxt3 = new JTextField("How much...");
//				//reactive combo
//				rdd1 = new JComboBox(reactReqs[req1.getSelectedIndex()]);
//				rdd1 = new JComboBox(reactReqs[req1.getSelectedIndex()]);
//				rdd1 = new JComboBox(reactReqs[req1.getSelectedIndex()]);
		
		//array initialization
		initializeTXT = new JTextField[5];
		initializeDD = new JComboBox[3];
		initializeTXT[0] = name;
		initializeTXT[1] = description;
		initializeTXT[2] = op1;
		initializeTXT[3] = op2;
		initializeTXT[4] = op3;
		initializeDD[0] = req1;
		initializeDD[1] = req2;
		initializeDD[2] = req3;
	}
	
	public DialogSet(String designation)
	{
		this();
		this.designation = designation;
	}
}
