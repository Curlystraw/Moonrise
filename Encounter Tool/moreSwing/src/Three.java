import java.awt.*;
import java.awt.event.ActionEvent;
import java.awt.event.KeyEvent;
import java.awt.event.KeyListener;

import javax.swing.*;

import two.Two;

class Three extends JFrame
{
	//Alright this is version three. It is basically two but better and it works with arrays and events.
	
	//I've done some math (counting) and we need 40 frames.
	//That means (at 
	
	
	//This is every piece of the JFrame, including the DialogSets
	JTextField name = new JTextField("Name the encounter.");
	JTree tree = new JTree();
	JList list = new JList();
	JButton crEnc = new JButton("Create Encounter");
	JButton quit = new JButton("Quit");
	
	//This is a button to be used for weird navigation until the tree is done, as a proof that the multiple frames work.
	JButton lever = new JButton("Increment Frame");
	
	//Max DialogSets is 24 I think, but I haven't looked into dynamically changing that
	DialogSet[] frames;
	
	//aggregateOutput is what should be the final output- put together with all the pieces from the parts array
	String aggregateOutput;
	//This array should have an index for which method provides info.
	String parts[];
	
	
	//The Constructor should be split up
	Three()
	{
		//Initializing variables
		aggregateOutput = "";
		parts = new String[683]; // I've done the math
		for (int i = 0; i < parts.length; i++)
		{
			parts[i] = "";
		}
		frames = new DialogSet[40]; //I've done the math
		for (int i = 0; i < frames.length; i ++)
		{
			frames[i] = new DialogSet(i);
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
		
		//In the final version, this functionality shouldn't exist
		drop(600, 1500, 200, 50, true, lever);
		lever.addActionListener((ActionEvent event)->{
			for (int i = frames.length - 1; i >= 0; i--)
			{
				if (frames[i].design.isVisible())
				{
					if (i == 23)
					{
						changeSets(frames[i], frames[0]);
					}
					else
					{
						changeSets(frames[i], frames[i+1]);
					}
				}
			}
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
			//Actually I think it's done, as long as we do the formatting for xml in the actions themselves, which should be easier to effectively customize.
			//That's not entirely true.
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
		// Ehhh, nevermind. Maybe we will change that in the final version.
		for (int i = 0; i < frames.length; i++)
		{			
			String designation = (i) + ".0";
			frames[i] = new DialogSet(designation);
			dropFrame(frames[i], false);
			addActions(frames[i]);
		}
		setFrameVisible(frames[0], true);
		
		//for some reason adding a useless label really helps it all come together.
		JLabel x = new JLabel();
		x.setLocation(3000, 3000);
		x.setSize(10, 10);
		add(x);
	}
	
		//This method should put all the things in the frame
		void dropFrame(DialogSet set, boolean bool)
		{
			drop(200, 200, 200, 50, bool, set.initializeTXT[0]);
			drop(200, 300, 500, 100, bool, set.initializeTXT[1]);
			drop(250, 450, 200, 50, bool, set.initializeTXT[2]);
			drop(250, 550, 200, 50, bool, set.initializeTXT[3]);
			drop(250, 650, 200, 50, bool, set.initializeTXT[4]);
			drop(500, 450, 200, 50, bool, set.initializeDD[0]);
			drop(500, 550, 200, 50, bool, set.initializeDD[1]);
			drop(500, 650, 200, 50, bool, set.initializeDD[2]);
			drop(750, 450, 200, 50, bool, set.initializeReactDD[0]);
			drop(750, 550, 200, 50, bool, set.initializeReactDD[1]);
			drop(750, 650, 200, 50, bool, set.initializeReactDD[2]);
			drop(750, 450, 200, 50, bool, set.initializeReactTXT[0]);
			drop(750, 550, 200, 50, bool, set.initializeReactTXT[1]);
			drop(750, 650, 200, 50, bool, set.initializeReactTXT[2]);
			drop(1000, 450, 200, 50, bool, set.initializeToggle[0]);
			drop(1000, 550, 200, 50, bool, set.initializeToggle[1]);
			drop(1000, 650, 200, 50, bool, set.initializeToggle[2]);
			drop(1250, 450, 200, 50, bool, set.initializeReactTXT[3]);
			drop(1250, 550, 200, 50, bool, set.initializeReactTXT[4]);
			drop(1250, 650, 200, 50, bool, set.initializeReactTXT[5]);
			drop(500, 200, 200, 50, bool, set.design);
		}
		
		//This method should change the visibility of an entire frame
		void setFrameVisible(DialogSet set, boolean bool)
		{
			for (JTextField n : set.initializeTXT)
			{
				n.setVisible(bool);
			}
			for (JComboBox n : set.initializeDD)
			{
				n.setVisible(bool);
			}
			for (JToggleButton n : set.initializeToggle)
			{
				n.setVisible(bool);
			}
			set.design.setVisible(bool);
		}
		
		//This method should change the sets
		void changeSets(DialogSet before, DialogSet after)
		{
			setFrameVisible(before, false);
			setFrameVisible(after, true);
		}
	
	
	//utility methods that work ish
	void drop(int x, int y, int w, int h, boolean in, JComponent comp)
	{
		comp.setLocation(x, y);
		comp.setSize(w, h);
		comp.setVisible(in);
		add(comp);
	}
	
	//This is the first draft of the addActions method
//	void addActions(DialogSet set)
//	{			//Weird order is because the final method should incorporate the caveats to the dialog options
//		set.initializeTXT[0].addActionListener((ActionEvent event) ->{
//			parts[1] = "Frame " + set.designation + "Dialogue title: " + set.initializeTXT[0].getText();
//		});
//		set.initializeTXT[1].addActionListener((ActionEvent event) ->{
//			parts[2] = "Frame " + set.designation + " Dialogue description:\n" + set.initializeTXT[1].getText();
//		});
//		set.initializeTXT[2].addActionListener((ActionEvent event) ->{
//			parts[3] = "Frame " + set.designation + "Option 1: " + set.initializeTXT[2].getText();
//		});
//		set.initializeTXT[3].addActionListener((ActionEvent event) ->{
//			parts[6] = "Frame " + set.designation + "Option 2: " + set.initializeTXT[3].getText();
//		});
//		set.initializeTXT[4].addActionListener((ActionEvent event) ->{
//			parts[9] = "Frame " + set.designation + "Option 3: " + set.initializeTXT[4].getText();
//		});
//		set.initializeDD[0].addActionListener((ActionEvent event) ->{
//			parts[4] = "Frame " + set.designation + "Option 1 requirement: " + set.reqs[set.initializeDD[0].getSelectedIndex()];
//		});
//		set.initializeDD[1].addActionListener((ActionEvent event) ->{
//			parts[7] = "Frame " + set.designation + "Option 2 requirement: " + set.reqs[set.initializeDD[1].getSelectedIndex()];
//		});
//		set.initializeDD[2].addActionListener((ActionEvent event) ->{
//			parts[10] = "Frame " + set.designation + "Option 3 requirement: " + set.reqs[set.initializeDD[2].getSelectedIndex()];
//		});
//	}
	
	//This is the second (and hopefully final) draft of the addActions method. The parts are indented for viewing convenience. It doesn't have all the right methods but the thought is there.
	//Also there's nothing there that accounts for the writers messing up or changing their minds.
	void addActions(DialogSet set)
	{
		set.initializeTXT[0].addActionListener((ActionEvent event) ->{
			parts[3 + (set.IDNum * 17) + 1] = "Frame " + set.designation + "Dialogue title: " + set.initializeTXT[0].getText();
		});
		set.initializeTXT[1].addActionListener((ActionEvent event) ->{
			parts[3 + (set.IDNum * 17) + 2] = "Frame " + set.designation + "Dialogue description: " + set.initializeTXT[1].getText();
		});
		set.initializeTXT[2].addActionListener((ActionEvent event) ->{
			parts[3 + (set.IDNum * 17) + 3] = "Frame " + set.designation + "Option 1 text: " + set.initializeTXT[2].getText();
		});
			set.initializeDD[0].addActionListener((ActionEvent event) ->{
				parts[3 + (set.IDNum * 17) + 4] = "Frame " + set.designation + "Option 1 requirement: " + set.reqs[set.initializeDD[0].getSelectedIndex()];
				boolean notDone = true;
				for (int i = 0; i < set.listofDDReqs.length; i++)
				{
					if (set.reqs[set.initializeDD[0].getSelectedIndex()].equals(set.listofDDReqs[i]))
					{
						set.initializeReactDD[0].setVisible(true);
						DefaultComboBoxModel model = new DefaultComboBoxModel(set.reactReqs[set.initializeDD[0].getSelectedIndex()]);
						set.initializeReactDD[0].setModel(model);
						set.initializeReactTXT[0].setVisible(false);
						notDone = false;
					}	
				}
				if (notDone)
				{
					set.initializeReactTXT[0].setVisible(true);
					set.initializeReactDD[0].setVisible(false);
				}
			});
				set.initializeReactTXT[0].addActionListener((ActionEvent event) ->{
					parts[3 + (set.IDNum * 17) + 5] = "Frame " + set.designation + "Option 1 requirement qualifier: " + set.initializeReactTXT[0].getText();
				});
				set.initializeReactDD[0].addActionListener((ActionEvent event) ->{
					parts[3 + (set.IDNum * 17) + 6] = "Frame " + set.designation + "Option 1 requirement amount: " + set.reqs[set.initializeReactDD[0].getSelectedIndex()];
				});
					set.initializeToggle[0].addActionListener((ActionEvent event) -> {
						if (set.initializeReactTXT[3].isVisible())
						{
							set.initializeReactTXT[3].setVisible(false);
						}
						else
						{
							set.initializeReactTXT[3].setVisible(true);
						}
					});
					set.initializeReactTXT[3].addActionListener((ActionEvent event) ->{
						parts[3 + (set.IDNum * 17) + 7] = "Frame " + set.designation + "Option 1 ends dialog. Reward: " + set.initializeReactTXT[3].getText();
					});			
		set.initializeTXT[3].addActionListener((ActionEvent event) ->{
			parts[3 + (set.IDNum * 17) + 8] = "Frame " + set.designation + "Option 2 text: " + set.initializeTXT[3].getText();
		});
			set.initializeDD[1].addActionListener((ActionEvent event) ->{
				parts[3 + (set.IDNum * 17) + 9] = "Frame " + set.designation + "Option 2 requirement: " + set.reqs[set.initializeDD[1].getSelectedIndex()];
				boolean notDone = true;
				for (int i = 0; i < set.listofDDReqs.length; i++)
				{
					if (set.reqs[set.initializeDD[1].getSelectedIndex()].equals(set.listofDDReqs[i]))
					{
						set.initializeReactDD[1].setVisible(true);
						DefaultComboBoxModel model = new DefaultComboBoxModel(set.reactReqs[set.initializeDD[1].getSelectedIndex()]);
						set.initializeReactDD[1].setModel(model);
						set.initializeReactTXT[1].setVisible(false);
						notDone = false;
					}	
				}
				if (notDone)
				{
					set.initializeReactTXT[1].setVisible(true);
					set.initializeReactDD[1].setVisible(false);
				}
			});
				set.initializeReactTXT[1].addActionListener((ActionEvent event) ->{
					parts[3 + (set.IDNum * 17) + 10] = "Frame " + set.designation + "Option 2 requirement qualifier: " + set.initializeReactTXT[1].getText();
				});
				set.initializeReactDD[1].addActionListener((ActionEvent event) ->{
					parts[3 + (set.IDNum * 17) + 11] = "Frame " + set.designation + "Option 2 requirement amount: " + set.reqs[set.initializeReactDD[1].getSelectedIndex()];
				});
					set.initializeToggle[1].addActionListener((ActionEvent event) -> {
						if (set.initializeReactTXT[4].isVisible())
						{
							set.initializeReactTXT[4].setVisible(false);
						}
						else
						{
							set.initializeReactTXT[4].setVisible(true);
						}
					});
					set.initializeReactTXT[4].addActionListener((ActionEvent event) ->{
						parts[3 + (set.IDNum * 17) + 12] = "Frame " + set.designation + "Option 2 ends dialog. Reward: " + set.initializeReactTXT[4].getText();
					});		
		set.initializeTXT[4].addActionListener((ActionEvent event) ->{
			parts[3 + (set.IDNum * 17) + 13] = "Frame " + set.designation + "Option 3 text: " + set.initializeTXT[4].getText();
		});
			set.initializeDD[2].addActionListener((ActionEvent event) ->{
				parts[3 + (set.IDNum * 17) + 14] = "Frame " + set.designation + "Option 3 requirement: " + set.reqs[set.initializeDD[2].getSelectedIndex()];
				boolean notDone = true;
				for (int i = 0; i < set.listofDDReqs.length; i++)
				{
					if (set.reqs[set.initializeDD[2].getSelectedIndex()].equals(set.listofDDReqs[i]))
					{
						set.initializeReactDD[2].setVisible(true);
						DefaultComboBoxModel model = new DefaultComboBoxModel(set.reactReqs[set.initializeDD[2].getSelectedIndex()]);
						set.initializeReactDD[2].setModel(model);
						set.initializeReactTXT[2].setVisible(false);
						notDone = false;
					}	
				}
				if (notDone)
				{
					set.initializeReactTXT[2].setVisible(true);
					set.initializeReactDD[2].setVisible(false);
				}
			});
				set.initializeReactTXT[2].addActionListener((ActionEvent event) ->{
					parts[3 + (set.IDNum * 17) + 15] = "Frame " + set.designation + "Option 3 requirement qualifier: " + set.initializeReactTXT[2].getText();
				});
				set.initializeReactDD[2].addActionListener((ActionEvent event) ->{
					parts[3 + (set.IDNum * 17) + 16] = "Frame " + set.designation + "Option 3 requirement amount: " + set.reqs[set.initializeReactDD[2].getSelectedIndex()];
				});
					set.initializeToggle[2].addActionListener((ActionEvent event) -> {
						if (set.initializeReactTXT[5].isVisible())
						{
							set.initializeReactTXT[5].setVisible(false);
						}
						else
						{
							set.initializeReactTXT[5].setVisible(true);
						}
					});
					set.initializeReactTXT[5].addActionListener((ActionEvent event) ->{
							parts[3 + (set.IDNum * 17) + 17] = "Frame " + set.designation + "Option 3 ends dialog. Reward: " + set.initializeReactTXT[5].getText();
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
