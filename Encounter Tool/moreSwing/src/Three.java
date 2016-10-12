import java.awt.*;
import java.awt.event.ActionEvent;
import java.awt.event.KeyEvent;
import java.awt.event.KeyListener;

import javax.swing.*;

import two.Two;

public class Three extends JFrame{
	
	//This is every piece of the JFrame, including the DialogSets
	JTextField name = new JTextField("Name the encounter.");
	JTree tree = new JTree();
	JList list = new JList();
	JButton crEnc = new JButton("Create Encounter");
	JButton quit = new JButton("Quit");
	
	//Max DialogSets is 24 I think, but I haven't looked into dynamically changing that
	DialogSet[] frames;
	
	//aggregateOutput is what should be the final output- put together with all the pieces from the parts array
	String aggregateOutput;
	//This array should have an index for which method provides info.
	String parts[];
	
	
	//The Constructor should be split up
	public Three()
	{
		//Initializing variables
		aggregateOutput = "";
		parts = new String[24];
		for (int i = 0; i < parts.length; i++)
		{
			parts[i] = "";
		}
		frames = new DialogSet[24];
		for (int i = 0; i < frames.length; i ++)
		{
			frames[i] = new DialogSet();
		}
		//Initializing Window
		setSize(3600, 2100);
		setTitle("Encounter Tool");
		//Initializing easy stuff
		initializeStuff();
		//Initializing hard stuff
		initializeNav();
		initializeFlags();
		//Initializing impossible stuff
		initializeFrames();
	}
	
	void initializeStuff()
	{
		drop(100, 100, 200, 50, true, name);
		drop(100, 1500, 100, 50, true, quit);
		drop(300, 1500, 200, 50, true, crEnc);
		name.addActionListener((ActionEvent waitForClick) -> {parts[0] = "Encounter name: " + name.getText();});
		quit.addActionListener((ActionEvent event)-> {System.exit(0);});
		crEnc.addActionListener((ActionEvent event)->{
			setUpString();
			JOptionPane.showMessageDialog(null, aggregateOutput);
		});
		
		//for some reason adding a useless label really helps it all come together.
				JLabel x = new JLabel();
				x.setLocation(3000, 3000);
				x.setSize(10, 10);
				add(x);
	}
		//Gonna indent methods that have to do with initializeStuff()
		void setUpString()
		{
			//TODO: set up how all the strings come together. The method should make aggregateOutput a collection (in order) of everything. Should be long but simple.
			for (int i = 0; i < parts.length; i++)
			{
				String nothing = "";
				if (parts[i].equals(nothing) == false)
				{
					aggregateOutput += parts[i] + "\n";
				}
			}
		}
	
	void initializeNav() //Both of these are far from done
	{
		add(tree);
		tree.setLocation(3000, 1000);
		tree.setSize(400, 800);
		tree.setVisible(true);
		
		//for some reason adding a useless label really helps it all come together.
		JLabel x = new JLabel();
		x.setLocation(3000, 3000);
		x.setSize(10, 10);
		add(x);
		
		//drop(1000, 500, 400, 300, true, tree);
		//make a tree and put it in here
	}
	
	void initializeFlags() //Both of these are far from done
	{
		add(list);
		list.setLocation(3000, 50);
		list.setSize(400, 800);
		list.setVisible(true);
		
		//for some reason adding a useless label really helps it all come together.
		JLabel x = new JLabel();
		x.setLocation(3000, 3000);
		x.setSize(10, 10);
		add(x);
		
		//drop(1000, 50, 400, 300, true, list);
		//make a list and put it in here
	}
	
	void initializeFrames()
	{
		// So far, I've only put in the first frame because easy and because the other frames should be dynamically created in the program
		frames[0] = new DialogSet("1.0");
		dropFrame(frames[0], true);
		addActions(frames[0]);
		
		//for some reason adding a useless label really helps it all come together.
		JLabel x = new JLabel();
		x.setLocation(3000, 3000);
		x.setSize(10, 10);
		add(x);
	}
	
		//This method should put all the things in the frame
		public void dropFrame(DialogSet set, boolean bool)
		{
			drop(200, 200, 200, 50, bool, set.initializeTXT[0]);
			drop(200, 300, 500, 100, bool, set.initializeTXT[1]);
			drop(250, 450, 200, 50, bool, set.initializeTXT[2]);
			drop(250, 550, 200, 50, bool, set.initializeTXT[3]);
			drop(250, 650, 200, 50, bool, set.initializeTXT[4]);
			drop(500, 450, 200, 50, bool, set.initializeDD[0]);
			drop(500, 550, 200, 50, bool, set.initializeDD[1]);
			drop(500, 650, 200, 50, bool, set.initializeDD[2]);
		}
	
	
	//utlilty methods that work ish
	public void drop(int x, int y, int w, int h, boolean in, JComponent comp)
	{
		comp.setLocation(x, y);
		comp.setSize(w, h);
		comp.setVisible(in);
		add(comp);
	}
	
	public void addActions(DialogSet set)
	{			//Weird order is because the final method should incorporate the caveats to the dialog options
		set.initializeTXT[0].addActionListener((ActionEvent event) ->{
			parts[1] = "Dialogue title: " + set.initializeTXT[0].getText();
		});
		set.initializeTXT[1].addActionListener((ActionEvent event) ->{
			parts[2] = "Dialogue description:\n" + set.initializeTXT[1].getText();
		});
		set.initializeTXT[2].addActionListener((ActionEvent event) ->{
			parts[3] = "Option 1: " + set.initializeTXT[2].getText();
		});
		set.initializeTXT[3].addActionListener((ActionEvent event) ->{
			parts[6] = "Option 2: " + set.initializeTXT[3].getText();
		});
		set.initializeTXT[4].addActionListener((ActionEvent event) ->{
			parts[9] = "Option 3: " + set.initializeTXT[4].getText();
		});
		set.initializeDD[0].addActionListener((ActionEvent event) ->{
			parts[4] = "Option 1 requirement: " + set.initializeDD[0].getToolTipText();
		});
		set.initializeDD[1].addActionListener((ActionEvent event) ->{
			parts[7] = "Option 2 requirement: " + set.initializeDD[2].getToolTipText();
		});
		set.initializeDD[2].addActionListener((ActionEvent event) ->{
			parts[10] = "Option 3 requirement: " + set.initializeDD[2].getToolTipText();
		});
	}
	
	//For organization, I'd like to have this stay at the bottom
	public static void main(String[] args)
	{
		EventQueue.invokeLater(() -> {
			Three go = new Three();
			go.setVisible(true);
		});

	}
}
