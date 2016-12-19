import java.awt.*;
import java.awt.event.ActionEvent;
import java.awt.event.KeyEvent;
import java.awt.event.KeyListener;

import javax.swing.*;
import javax.swing.event.ListSelectionEvent;


import two.Two;

class Three extends JFrame
{
	//Alright this is version three. It is basically two but better and it works with arrays and events.
	
	//VERSION HISTORY:
	//	10/13/16	Version Three	3.0
	//	10/22/16	Version 3.1		3.1
	//	10/25/16	Version 3.2		3.2
	//	10/26/16	Version 3.3		3.3
	//  11/02/16	Version 3.4		3.4
	//  11/04/16	Version 3.5		3.5 ***THIS IS THE PRE-CRUNCH VERSION***
	//	11/04/16	Version 3.6		3.6 ***THIS IS THE POST-CRUNCH VERSION***
	//  12/16/16	Version 3.7		3.7
	//	12/18/16	Version 3.8		3.8 ***Christmas Update***
		//	Version 3.8 includes implementation of XML coding, and a working navigation list
	
	//I've done some math (counting) and we need 40 frames.
	//That means (at my calculations) we need like 683 separate parts of the String file.
	//We only need 603 pieces for the XML because I'm more efficient in version 3.8
	//Even so, it still works.
	
	/* ...........___________..._________.....__...___............__...___......__......___.............________......
	 * ........../____  ____/../___  ___/..../  \./   \........../  \./   \.....\  \.../  /............/  ____  \.....
	 * ............../ /........../ /......./ /\ V /\  |......../ /\ V /\  |.....\  \./  /............/  /..../  |....
	 * ............./ /........../ /......./ /..\ /..| |......./ /..\ /..| |......\  V  /............/  /____/  /.....
	 * ............/ /........../ /......./ /....V...| /....../ /....V...| /.......|  ,/............/  ________/......
	 * .....__..../ /........../ /......./ /......../ /....../ /......../ /......./  /............./  /...............
	 * .....\ \__/ /.......___/ /___..../ /......../ /....../ /......../ /......./  /............./  /....__..........
	 * ......\____/......./________/.../_/......../_/....../_/......../_/......./__/............./__/..../_/..........
	 */
	
	
	// I have three more hours on this flight, and I've already finished the preliminary xml integration, so here's some more ASCii art
	/*                            _____     _______      ______                  *            *       *                                          *
	 *       * |                 /     \   /       \    /   *  \           *                               *                                    / \
	 *       _\|/_       *       |      | /   *     |  |        |             *           *        *                                           /_ -O
	 *        /|\                |      | |         | /        /                   *             *                                            /__-J_\
	 *         |                /      / /         / /        /      *                       *       *                                       O_~-   __\
	 *              *                 |/          |/    *     |                        *                   *                                /-J     __\
	 *                         *     /           /           /                               _            *                                /~-     O __\
	 *      *                       /      *    /   *       /    _       o____     o____    / \    |        *                            _/__ --      __O_
	 *                  *          |           |           |    / \      /   /     /   /   /   |  /     *                               /__     J-      __\
	 *         *                   /          /           /    /  /     /   /     /   /   /   /  /| *                                   _/__ O    ~--  __\_
	 *                            /  *       /    *      /     \_/     /   /     /   /   /   /  / |       *                            /__       O   -J  __\
	 *                  *        |          |           |   ___/\__/__/   (__/__/   (__//    \_/  /          *                           O____       __-__\
	 *       *                                       *                              *          __/___/                                        | J  J|
	 *                          *                *                           *                /  |                                            |__|__|
	 *            *                      *                        *              *           |   /                              (lol the only JTree in this program)
	 *                     *                         *                                *       \_/
	 *                 __________  *         ___                           *                *
	 *                /          \          /   \                    *           *                          *
	 *               /            |        |     |    *                  *                 |       *
	 *              /            /         /     |          *                     *       /                           *
	 *             |      *               /      |              *        *                |                                *
	 *            /                       \    _/                                         /     _       *
	 *           /                        |  _/         *                            _____|-----               *
	 *          /           *             |_/___                                  _--     /                                    *
	 *         /                        _///    \                                        /                               *
	 *        |    *                 __/ /       |                  0                    |         __  __   __        ___
	 *        |                         /       /        o____               |\          |        /  |/  | /  |      /   \         |\
	 *        |              /\        |       |         /   /      |        | \         |       /  //   //   /     |     |        | \
	 *        |               /        /       /        /   /      /|       /   \       /|      /   |    |    |    /      /       /   \
	 *         \             /        /       /        /   /     _/ /      /    /     _/ /     /   /     /    /    |      |      /    /
	 *          \___________/        |        \____/__/   (__/__/   \__/__/\___/__/__/   \___//    |     |    \__/_/\____/\__/__/\___/__/
	 *   
	 */
		
	//Output stuff
	OutputFrame frame = new OutputFrame();
	
	//Font stuff
	Font myFont = new Font("Helvetica", 1, 12);
	
	
	//Sizing stuff
	final int HEIGHT = 1920;
	final int WIDTH = 1080;
	
	//This is every piece of the JFrame, including the DialogSets
	JTextField name = new JTextField("Name the encounter.");  //Encounter naming text field
	JLabel flagLabel = new JLabel("List the zone(s) in which the encounter can occur. Please separate multiples with a comma.");
	JTextField flags = new JTextField();
	 //Tree for navigation is down in the initializeNav method (does not contribute to final string)
	String[] designations = new String[40];
	JList list; //List of flags for the entire encounter (contributes a list as part no. 3 of the xml **Not yet implemented**) and it turns out it's not necessary for 3.8
	JButton crEnc = new JButton("Create Encounter"); //Button to "create the encounter" that actually just puts all the things together in a string and displays it
	JButton quit = new JButton("Quit"); //Button to leave
	
	//These are buttons to be used for navigation until the tree is done, as a proof that the multiple frames work and in case the tree is awkward or the last thing we do.
	JButton buttonUp = new JButton("Increment Frame"); //Button to go from frame n to frame n+1
	JButton buttonDown = new JButton("Decrement Frame"); //Button to go from frame n to frame n-1
	int activeFrame; //Int to keep track of which frame is active
	
	//Max DialogSets is 40 by my math, and they can be changed with buttons and should soon be able to be changed with the tree.
	DialogSet[] frames;
	
	//aggregateOutput is what should be the final output- put together with all the pieces from the parts array
	//xmlOutput is what te XML file should read- put together from all the pieces from the parts array, and then translated
	String aggregateOutput;
	String xmlOutput;
	//This array should have an index for which method provides info.
	String parts[];
	String xmlparts[];
	
	
	//The Constructor should be split up
	Three()
	{
		//Initializing variables
		aggregateOutput = "";
		parts = new String[683]; // I've done the math. We need (at most) 683 separate parts of the final xml, assuming one encounter flag
		for (int i = 0; i < parts.length; i++)
		{
			parts[i] = "";
		}
		xmlparts = new String[603];
		for (int i = 0; i < xmlparts.length; i++)
		{
			xmlparts[i] = "";
		}
		activeFrame = 0;
		frames = new DialogSet[40]; //I've done the math. We need (at most) 40 frames to incorporate everything
		for (int i = 0; i < frames.length; i ++)
		{
			frames[i] = new DialogSet(i);
		}		
		
		//Initializing Window
		setSize(HEIGHT, WIDTH); //This size doesn't work all the time but it works for me and its size is what everything's locations are based off of.
		setTitle("Encounter Tool"); //Makes sense.
		//Initializing easy stuff
		initializeStuff(); //"Stuff" means the text box for encounter name, the buttons quit, create encounter, increment frame, and decrement frame
		//Initializing hard stuff
		initializeNav(); //This is the tree
		initializeFlags(); //This is the list for flags
		//Initializing impossible stuff
		initializeFrames(); //These are the frames, with all their parts (20 each)
	}
	
	void initializeStuff()
	{
		//drop method is defined waaay below
		drop(HEIGHT/40, WIDTH/40, 6*HEIGHT/40, WIDTH/40, true, name); //Name box placement/creation
		drop(HEIGHT/40, 8*WIDTH/10, HEIGHT/20, WIDTH/40, true, quit); //Quit button placement/creation
		drop(HEIGHT/10, 8*WIDTH/10, 3*HEIGHT/20, WIDTH/40, true, crEnc); //create encounter button placement/creation
		drop(HEIGHT/40, 13*WIDTH/40, WIDTH, HEIGHT/4, false, frame.text);
		drop(3*HEIGHT/10, 17*WIDTH/20, HEIGHT/20, WIDTH/40, false, frame.back);
		name.addActionListener((ActionEvent waitForClick) -> {
			parts[0] = "";
			parts[0] = "Encounter name: " + name.getText();
			}); //changes the first string to whatever's in the name box
		quit.addActionListener((ActionEvent event)-> {System.exit(0);}); //quits
		//runs the create-a-string algorithm
		crEnc.addActionListener((ActionEvent event)->{
			setUpXML(); //this is the method for that
//			JOptionPane.showMessageDialog(null, aggregateOutput); //THIS WHOLE THING DOESN'T WORK
//			//this last line will be unnecessary when the string is translated to xml, but we could still use it so writers can check their work
			frame.setWords(aggregateOutput);
			frame.setVisible(true);
			System.out.print(xmlOutput);
		});
		
		//In the final version, this functionality can exist but shouldn't be the only way to navigate
		drop(3*HEIGHT/10, 8*WIDTH/10, 3*HEIGHT/20, WIDTH/40, true, buttonUp); //next frame button placement/creation
		buttonUp.addActionListener((ActionEvent event)->{
			if (activeFrame < 39) //check if current frame is last frame
			{
				changeSets(frames[activeFrame], frames[activeFrame + 1]); //make it go to next frame
				activeFrame = activeFrame + 1; //change index
			}
			else
			{
				changeSets(frames[activeFrame], frames[0]); //make it go to frame 0
				activeFrame = 0; //change index
			}
		});
		
		drop(HEIGHT/2, 8*WIDTH/10, 3*HEIGHT/20, WIDTH/40, true, buttonDown); //previous frame button placement/creation
		buttonDown.addActionListener((ActionEvent event)->{
			if (activeFrame > 0) //check if current frame is first frame
			{
				changeSets(frames[activeFrame], frames[activeFrame - 1]); //make it go to previous frame
				activeFrame = activeFrame - 1; //change index
			}
			else
			{
				changeSets(frames[activeFrame], frames[39]); //make it go to last frame
				activeFrame = 39; //change index
			}
		});
		
		
		
		//for some reason adding a useless label really helps it all come together.
		//seriously if anyone knows how to fix this do it.
		JLabel x = new JLabel();
		x.setLocation(3000, 3000);
		x.setSize(10, 10);
		add(x);
	}
		//Gonna indent methods that have to do with initializeStuff()
		void setUpString()
		{
			String nothing = ""; //makes a string to compare to	
			parts[0] = "Encounter name: " + name.getText(); // no if statement here because a writer should always name their encounter. Geez.
			xmlparts[0] = "<encounterName> " + name.getText() + "</encounterName>";
			parts[1] = "Encounter flags: " + flags.getText(); // same as line before-- every writer should have at least one flag.		
			xmlparts[1] = "<flags> \n <areas> " + flags.getText() + "</areas> \n </flags>";
			for (DialogSet set : frames)
			{
				if (set.name.getText().equals("Dialog name") == false)
				{
					parts[3 + (set.IDNum * 17) + 1] = "Frame " + set.designation + " Dialogue title: " + set.initializeTXT[0].getText();
				}
				if (set.initializeTXT[1].getText().equals("Describe the Dialog") == false)
				{
					parts[3 + (set.IDNum * 17) + 2] = "Frame " + set.designation + " Dialogue Description: " + set.initializeTXT[1].getText();
				}
				if (set.initializeTXT[2].getText().equals("Option 1") == false)
				{
					parts[3 + (set.IDNum * 17) + 3] = "Frame " + set.designation + " Option 1: " + set.initializeTXT[2].getText();
				}
				if (set.initializeTXT[3].getText().equals("Option 2") == false)
				{
					parts[3 + (set.IDNum * 17) + 8] = "Frame " + set.designation + " Option 2: " + set.initializeTXT[3].getText();
				}
				if (set.initializeTXT[4].getText().equals("Option 3") == false)
				{
					parts[3 + (set.IDNum * 17) + 13] = "Frame " + set.designation + " Option 3: " + set.initializeTXT[4].getText();
				}
				if (set.initializeReactTXT[0].getText().equals("How much...") == false)
				{
					parts[3 + (set.IDNum * 17) + 5] = "Frame " + set.designation + " Option 1 requirement amount: " + set.initializeReactTXT[0].getText();
				}
				if (set.initializeReactTXT[1].getText().equals("How much...") == false)
				{
					parts[3 + (set.IDNum * 17) + 10] = "Frame " + set.designation + " Option 2 requirement amount: " + set.initializeReactTXT[1].getText();
				}
				if (set.initializeReactTXT[2].getText().equals("How much...") == false)
				{
					parts[3 + (set.IDNum * 17) + 15] = "Frame " + set.designation + " Option 3 requirement amount: " + set.initializeReactTXT[2].getText();
				}
				if (set.initializeReactTXT[3].getText().equals("Reward") == false)
				{
					parts[3 + (set.IDNum * 17) + 7] = "Frame " + set.designation + " Option 1 ends dialog. Reward: " + set.initializeReactTXT[3].getText();
				}
				if (set.initializeReactTXT[4].getText().equals("Reward") == false)
				{
					parts[3 + (set.IDNum * 17) + 12] = "Frame " + set.designation + " Option 2 ends dialog. Reward: " + set.initializeReactTXT[4].getText();
				}
				if (set.initializeReactTXT[5].getText().equals("Reward") == false)
				{
					parts[3 + (set.IDNum * 17) + 15] = "Frame " + set.designation + " Option 3 ends dialog. Reward: " + set.initializeReactTXT[5].getText();
				}
			}
			
			for (int i = 0; i < parts.length; i++) //go through all the parts of the "parts" array
			{
				if (parts[i].equals(nothing) == false) //make sure each part isn't nothing
				{
					aggregateOutput += parts[i] + "\n"; //then add it to the aggregate output
				}
			}
			//this method assumes parts is ordered correctly, which I think it is, but, you know, you never know.
		}
		
		
		void setUpXML()
		{
			setUpString();
			String nothing = ""; //makes a string to compare to	
			xmlparts[0] = "<encounterName> " + name.getText() + "</encounterName>"; // for the xml code to work, people need to have at least an encounter name
			if (flags.getText().equals(nothing) == false)
			{
				xmlparts[1] = "<flags> \n <areas> " + flags.getText() + "</areas> \n </flags>";
			}
			for (DialogSet set : frames)
			{
				String introStr = "<dialog> \n <dialogpath>" + set.IDNum * 4 + "</dialogpath>";
				if (set.name.getText().equals("Dialog name") == false)
				{
					xmlparts[3 + (set.IDNum *15) + 2] = introStr + " <dialogname>" + set.initializeTXT[0].getText() + "</dialogname>";
				}
				if (set.initializeTXT[1].getText().equals("Describe the Dialog") == false)
				{
					xmlparts[3 + (set.IDNum *15) + 3] = "<text>" + set.initializeTXT[1].getText() + "</text>";
				}
				if (set.initializeTXT[2].getText().equals("Option 1") == false)
				{
					xmlparts[3 + (set.IDNum *15) + 4] = "<option> \n <optionNum>1</optionNum> \n <text>" + set.initializeTXT[2].getText() + "</text>";
				}
				if (set.initializeTXT[3].getText().equals("Option 2") == false)
				{
					xmlparts[3 + (set.IDNum *15) + 7] = "<option> \n <optionNum>2</optionNum> \n <text>" + set.initializeTXT[2].getText() + "</text>";
				}
				if (set.initializeTXT[4].getText().equals("Option 3") == false)
				{
					xmlparts[3 + (set.IDNum *15) + 10] = "<option> \n <optionNum>3</optionNum> \n <text>" + set.initializeTXT[2].getText() + "</text>";
				}
				if (set.initializeReactTXT[0].getText().equals("How much...") == false)
				{
					xmlparts[3 + (set.IDNum *15) + 6] = "<amount>" + set.initializeReactTXT[0].getText() + "</amount> \n </requirement> \n </option>";
				}
				if (set.initializeReactDD[0].getSelectedIndex() != 0)
				{
					xmlparts[3 + (set.IDNum *15) + 6] = "<amount>" + set.reactReqs[set.initializeReactDD[0].getSelectedIndex()] + "</amount> \n </requirement> \n </option>";
				}
				if (set.initializeReactTXT[1].getText().equals("How much...") == false)
				{
					xmlparts[3 + (set.IDNum *15) + 9] = "<amount>" + set.initializeReactTXT[1].getText() + "</amount> \n </requirement> \n </option>";
				}
				if (set.initializeReactDD[1].getSelectedIndex() != 0)
				{
					xmlparts[3 + (set.IDNum *15) + 9] = "<amount>" + set.reactReqs[set.initializeReactDD[1].getSelectedIndex()] + "</amount> \n </requirement> \n </option>";
				}
				if (set.initializeReactTXT[2].getText().equals("How much...") == false)
				{
					xmlparts[3 + (set.IDNum *15) + 12] = "<amount>" + set.initializeReactTXT[2].getText() + "</amount> \n </requirement> \n </option>";
				}
				if (set.initializeReactDD[2].getSelectedIndex() != 0)
				{
					xmlparts[3 + (set.IDNum *15) + 12] = "<amount>" + set.reactReqs[set.initializeReactDD[2].getSelectedIndex()] + "</amount> \n </requirement> \n </option>";
				}
				if (set.reqs[set.initializeDD[0].getSelectedIndex()].equals("unselected") == false)
				{
					xmlparts[3 + (set.IDNum * 15) + 5] = "<requirement> \n <name>" + set.reqs[set.initializeDD[0].getSelectedIndex()] + "</name>";
				}
				if (set.reqs[set.initializeDD[1].getSelectedIndex()].equals("unselected") == false)
				{
					xmlparts[3 + (set.IDNum * 15) + 8] = "<requirement> \n <name>" + set.reqs[set.initializeDD[1].getSelectedIndex()] + "</name>";
				}
				if (set.reqs[set.initializeDD[2].getSelectedIndex()].equals("unselected") == false)
				{
					xmlparts[3 + (set.IDNum * 15) + 11] = "<requirement> \n <name>" + set.reqs[set.initializeDD[2].getSelectedIndex()] + "</name>";
				}
				
				if (set.initializeReactTXT[3].getText().equals("Reward") == false)
				{
					xmlparts[3 + (set.IDNum * 15) + 13] = "<dialog> \n <dialogpath>" + (set.IDNum * 4 + 1) + "</dialogpath> \n <text> " + set.initializeReactTXT[3] + "</text> \n </dialog>";
				}
				if (set.initializeReactTXT[4].getText().equals("Reward") == false)
				{
					xmlparts[3 + (set.IDNum * 15) + 14] = "<dialog> \n <dialogpath>" + (set.IDNum * 4 + 2) + "</dialogpath> \n <text> " + set.initializeReactTXT[4] + "</text> \n </dialog>";
				}
				if (set.initializeReactTXT[5].getText().equals("Reward") == false)
				{
					xmlparts[3 + (set.IDNum * 15) + 15] = "<dialog> \n <dialogpath>" + (set.IDNum * 4 + 3) + "</dialogpath> \n <text> " + set.initializeReactTXT[5] + "</text> \n </dialog>";
				}
			}
			
			for (int i = 0; i < xmlparts.length; i++)
			{
				if (xmlparts[i].equals(nothing) == false)
				{
					xmlOutput += xmlparts[i] + "\n";
				}
			}
			
		}
	
	void initializeNav() 
	{
		
//		add(tree);
//		tree.setLocation(3000, 1000);
//		tree.setSize(400, 800);	
//		tree.setVisible(true);
		
		JList tree = new JList(designations); //so it's not really a tree, but rather a list
		tree.setSelectionMode(ListSelectionModel.SINGLE_SELECTION);
		tree.setLayoutOrientation(JList.VERTICAL);
		tree.setVisibleRowCount(-1);
		JScrollPane scroller = new JScrollPane(tree);
		tree.addListSelectionListener((ListSelectionEvent e) -> {
			if (e.getValueIsAdjusting() == false)
			{
				changeSets(frames[activeFrame], frames[tree.getSelectedIndex()]);
				updateTree(activeFrame);
				activeFrame = tree.getSelectedIndex();
				
			}
		});
		
		//for some reason adding a useless label really helps it all come together.
		JLabel x = new JLabel();
		x.setLocation(3000, 3000);
		x.setSize(10, 10);
		add(x);
		
		drop(7*HEIGHT/10, 9*WIDTH/40, HEIGHT/5, 27*WIDTH/40, true, tree);
		//make a tree and put it in here (I think this one works now but at first I hadn't fixed the method so it wouldn't work)
	}
		
		void updateTree(int index)
		{
			if (frames[index].initializeTXT[0].getText().equals("Dialog name") == false)
			{
				designations[index] = frames[index].designation + " " + frames[index].initializeTXT[0].getText();
			}
		}
	
	void initializeFlags() //Both of these are far from done
	{
//		add(list);
//		list.setLocation(3000, 50);
//		list.setSize(400, 800);
//		list.setVisible(true);
	
		drop(6*HEIGHT/10, WIDTH/40, 3*HEIGHT/10, WIDTH/40, true, flagLabel);
		drop(6*HEIGHT/10, 3*WIDTH/40, 3*HEIGHT/10, WIDTH/40, true, flags);
		flags.addActionListener((ActionEvent event)->{
			parts[3] = "";
			parts[3] = "Flags for the encounter: " + flags.getText();
		});
		
		//for some reason adding a useless label really helps it all come together.
		JLabel x = new JLabel();
		x.setLocation(3000, 3000);
		x.setSize(10, 10);
		add(x);
		
		//drop(1000, 50, 400, 300, true, list);
		//make a list and put it in here (I think this one works now but at first I hadn't fixed the method so it wouldn't work)
	}
	
	
	void initializeFrames()
	{
		// So far, I've only put in the first frame because easy and because the other frames should be dynamically created in the program
		// Ehhh, nevermind. Maybe we will change that in the final version.
		for (int i = 0; i < frames.length; i++) //goes through all the frames
		{			
			//String designation = (i) + ".0"; //gives the designation a number (there was an intent here but it's kinda stupid)
			//This is a super long method that gives the actual designations a writer can understand. And it's long because I was lazy, 
			//but it's also long because I'm dumb and it's complicated.
			String designation = "";
			if (i == 0)
			{
				 designation = "0.0";
			}
			else if (i > 0 && i <= 13)
			{
				 designation = "1.";
				if (i == 1)
				{
					designation += "0";
				}
				else if (i > 1 && i <= 5)
				{
					designation += "1.";
					if (i == 2)
					{
						designation += "0";
					}
					else if (i == 3)
					{
						designation += "1";
					}
					else if (i == 4)
					{
						designation += "2";
					}
					else if (i == 5)
					{
						designation += "3";
					}
				}
				else if (i > 5 && i <= 9)
				{
					designation += "2.";
					if (i == 6)
					{
						designation += "0";
					}
					else if (i == 7)
					{
						designation += "1";
					}
					else if (i == 8)
					{
						designation += "2";
					}
					else if (i == 9)
					{
						designation += "3";
					}
				}
				else if (i > 9 && i <= 13)
				{
					designation += "3.";
					if (i == 10)
					{
						designation += "0";
					}
					else if (i == 11)
					{
						designation += "1";
					}
					else if (i == 12)
					{
						designation += "2";
					}
					else if (i == 13)
					{
						designation += "3";
					}
				}
			}
			else if (i > 13 && i <= 26)
			{
				 designation = "2.";
				if (i == 14)
				{
					designation += "0";
				}
				else if (i > 14 && i <= 18)
				{
					designation += "1.";
					if (i == 15)
					{
						designation += "0";
					}
					else if (i == 16)
					{
						designation += "1";
					}
					else if (i == 17)
					{
						designation += "2";
					}
					else if (i == 18)
					{
						designation += "3";
					}
				}
				else if (i > 18 && i <= 22)
				{
					designation += "2.";
					if (i == 19)
					{
						designation += "0";
					}
					else if (i == 20)
					{
						designation += "1";
					}
					else if (i == 21)
					{
						designation += "2";
					}
					else if (i == 22)
					{
						designation += "3";
					}
				}
				else if (i > 22 && i <= 26)
				{
					designation += "3.";
					if (i == 23)
					{
						designation += "0";
					}
					else if (i == 24)
					{
						designation += "1";
					}
					else if (i == 25)
					{
						designation += "2";
					}
					else if (i == 26)
					{
						designation += "3";
					}
				}
			}
			else if (i > 26 && i <= 39)
			{
				 designation = "3.";
				if (i == 27)
				{
					designation += "0";
				}
				else if (i > 27 && i <= 31)
				{
					designation += "1.";
					if (i == 28)
					{
						designation += "0";
					}
					else if (i == 29)
					{
						designation += "1";
					}
					else if (i == 30)
					{
						designation += "2";
					}
					else if (i == 31)
					{
						designation += "3";
					}
				}
				else if (i > 31 && i <= 35)
				{
					designation += "2.";
					if (i == 32)
					{
						designation += "0";
					}
					else if (i == 33)
					{
						designation += "1";
					}
					else if (i == 34)
					{
						designation += "2";
					}
					else if (i == 35)
					{
						designation += "3";
					}
				}
				else if (i > 35 && i <= 39)
				{
					designation += "3.";
					if (i == 36)
					{
						designation += "0";
					}
					else if (i == 37)
					{
						designation += "1";
					}
					else if (i == 38)
					{
						designation += "2";
					}
					else if (i == 39)
					{
						designation += "3";
					}
				}
			}
			designations[i] = designation;
			
			frames[i] = new DialogSet(i, designation); //initializes the variable (FINALLY)
			dropFrame(frames[i], false); //drops it in because now this method works
			addActions(frames[i]); //these two methods are kickass and make it so easy to write this program
		}
		setFrameVisible(frames[0], true); //then once all the frames are made set the first frame to visible
		
		//for some reason adding a useless label really helps it all come together.
		JLabel x = new JLabel();
		x.setLocation(3000, 3000);
		x.setSize(10, 10);
		add(x);
	}
	
		//This method should put all the things in the frame
		void dropFrame(DialogSet set, boolean bool) //holy shit this method is really cool- i didn't think it was gonna work but it did
		{
			drop(HEIGHT/40, 3*WIDTH/40, HEIGHT/10, WIDTH/40, bool, set.initializeTXT[0]); //this
			drop(HEIGHT/40, 5*WIDTH/40, 11*HEIGHT/20, WIDTH/40, bool, set.initializeTXT[1]); //literally
			drop(HEIGHT/40, 7*WIDTH/40, HEIGHT/10, WIDTH/40, bool, set.initializeTXT[2]); //puts
			drop(HEIGHT/40, 9*WIDTH/40, HEIGHT/10, WIDTH/40, bool, set.initializeTXT[3]); //in
			drop(HEIGHT/40, 11*WIDTH/40, HEIGHT/10, WIDTH/40, bool, set.initializeTXT[4]); //the 
			drop(3*HEIGHT/20, 7*WIDTH/40, HEIGHT/10, WIDTH/40, bool, set.initializeDD[0]); //Jframe
			drop(3*HEIGHT/20, 9*WIDTH/40, HEIGHT/10, WIDTH/40, bool, set.initializeDD[1]); //every
			drop(3*HEIGHT/20, 11*WIDTH/40, HEIGHT/10, WIDTH/40, bool, set.initializeDD[2]); //single
			drop(11*HEIGHT/40, 7*WIDTH/40, HEIGHT/10, WIDTH/40, bool, set.initializeReactDD[0]); //teensy
			drop(11*HEIGHT/40, 9*WIDTH/40, HEIGHT/10, WIDTH/40, bool, set.initializeReactDD[1]); //weensy
			drop(11*HEIGHT/40, 11*WIDTH/40, HEIGHT/10, WIDTH/40, bool, set.initializeReactDD[2]); //teeny
			drop(11*HEIGHT/40, 7*WIDTH/40, HEIGHT/10, WIDTH/40, bool, set.initializeReactTXT[0]); //itsy
			drop(11*HEIGHT/40, 9*WIDTH/40, HEIGHT/10, WIDTH/40, bool, set.initializeReactTXT[1]); //bitsy
			drop(11*HEIGHT/40, 11*WIDTH/40, HEIGHT/10, WIDTH/40, bool, set.initializeReactTXT[2]); //specific
			drop(2*HEIGHT/5, 7*WIDTH/40, HEIGHT/10, WIDTH/40, bool, set.initializeToggle[0]); //tiny
			drop(2*HEIGHT/5, 9*WIDTH/40, HEIGHT/10, WIDTH/40, bool, set.initializeToggle[1]); //precise
			drop(2*HEIGHT/5, 11*WIDTH/40, HEIGHT/10, WIDTH/40, bool, set.initializeToggle[2]); //piece
			drop(21*HEIGHT/40, 7*WIDTH/40, HEIGHT/10, WIDTH/40, bool, set.initializeReactTXT[3]); //of
			drop(21*HEIGHT/40, 9*WIDTH/40, HEIGHT/10, WIDTH/40, bool, set.initializeReactTXT[4]); //the
			drop(21*HEIGHT/40, 11*WIDTH/40, HEIGHT/10, WIDTH/40, bool, set.initializeReactTXT[5]); //Dialog
			drop(3*HEIGHT/10, 3*WIDTH/40, HEIGHT/10, WIDTH/40, bool, set.design); //Set
		}
		
		//This method should change the visibility of an entire frame
		void setFrameVisible(DialogSet set, boolean bool) //this is cool- i basically just wanted to use for:each loops
		{
			for (JTextField n : set.initializeTXT) //every little part of the always visible TXT boxes
			{
				n.setVisible(bool);
			}
			for (JComboBox n : set.initializeDD) //every little part of the always visible DD boxes
			{
				n.setVisible(bool);
			}
			for (JToggleButton n : set.initializeToggle) //the three toggles
			{
				n.setVisible(bool);
			}
			for (JComboBox n : set.initializeReactDD) //these are false because when a new function comes up any and all reactDDs should be invisible
			{
				n.setVisible(false);
			}
			for (JTextField n : set.initializeReactTXT) //these are false because when a new function comes up any and all reactTXTs should be invisible
			{
				n.setVisible(false);
			}
			set.design.setVisible(bool); //the label of the frame
		}
		
		//This method should change the sets
		void changeSets(DialogSet before, DialogSet after)
		{
			setFrameVisible(before, false); //set the first one false
			setFrameVisible(after, true); //set the next one true
		}
	
	
	//utility methods that work ish
	void drop(int x, int y, int w, int h, boolean in, JComponent comp) //gotta love drop. it condenses five lines every time
	{
		comp.setLocation(x, y); //hated having to do this all the time
		comp.setSize(w, h); //same
		comp.setVisible(in); //lol me too
		comp.setFont(myFont); //let's hope this works (it does)
		add(comp); //this method sucks a bunch but for some reason it works so well
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
		//All of the parts of a DialogSet are indexed, so for example any set.initializeTXT[1] is the description of the dialogue frame for any DialogSet set
		//So, if someone chose a certain dialog option, the next frame's dialog description would sorta be the response.
		set.initializeTXT[0].addActionListener((ActionEvent event) ->{
			parts[3 + (set.IDNum * 17) + 1]/* this math was super hard to figure out */ = "";
			parts[3 + (set.IDNum * 17) + 1]/* this math was super hard to figure out */ = " Frame " + set.designation + " Dialogue title: " + set.initializeTXT[0].getText(); 
		});
		set.initializeTXT[1].addActionListener((ActionEvent event) ->{
			parts[3 + (set.IDNum * 17) + 2] = "";
			parts[3 + (set.IDNum * 17) + 2] = "Frame " + set.designation + " Dialogue description: " + set.initializeTXT[1].getText(); 
		});
		set.initializeTXT[2].addActionListener((ActionEvent event) ->{
			parts[3 + (set.IDNum * 17) + 3] = "";
			parts[3 + (set.IDNum * 17) + 3] = "Frame " + set.designation + " Option 1 text: " + set.initializeTXT[2].getText(); 
		});
			set.initializeDD[0].addActionListener((ActionEvent event) ->{
				parts[3 + (set.IDNum * 17) + 4] = "";
				parts[3 + (set.IDNum * 17) + 4] = "Frame " + set.designation + " Option 1 requirement: " + set.reqs[set.initializeDD[0].getSelectedIndex()];
				boolean notDone = true;
				for (int i = 0; i < set.listofDDReqs.length; i++)
				{
					if (set.reqs[set.initializeDD[0].getSelectedIndex()].equals(set.listofDDReqs[i]))
					{
						//had to look this up
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
				if (set.reqs[set.initializeDD[0].getSelectedIndex()].equals("none"))
				{
					set.initializeReactTXT[0].setVisible(false);
				}
			});
				set.initializeReactTXT[0].addActionListener((ActionEvent event) ->{
					parts[3 + (set.IDNum * 17) + 5] = "";
					parts[3 + (set.IDNum * 17) + 5] = "Frame " + set.designation + " Option 1 requirement qualifier: " + set.initializeReactTXT[0].getText();
				});
				set.initializeReactDD[0].addActionListener((ActionEvent event) ->{
					parts[3 + (set.IDNum * 17) + 6] = "";
					parts[3 + (set.IDNum * 17) + 6] = "Frame " + set.designation + " Option 1 requirement amount: " + set.reactReqs[set.initializeDD[0].getSelectedIndex()][set.initializeReactDD[0].getSelectedIndex()];
				});
					set.initializeToggle[0].addActionListener((ActionEvent event) -> {
						if (set.initializeReactTXT[3].isVisible())
						{
							set.initializeReactTXT[3].setVisible(false);
							parts[3 + (set.IDNum * 17) + 7] = "";
							set.initializeReactTXT[3].setText("Reward");
						}
						else
						{
							set.initializeReactTXT[3].setVisible(true);
						}
					});
					set.initializeReactTXT[3].addActionListener((ActionEvent event) ->{
						parts[3 + (set.IDNum * 17) + 7] = "";
						parts[3 + (set.IDNum * 17) + 7] = "Frame " + set.designation + " Option 1 ends dialog. Reward: " + set.initializeReactTXT[3].getText();
					});			
		set.initializeTXT[3].addActionListener((ActionEvent event) ->{
			parts[3 + (set.IDNum * 17) + 8] = "";
			parts[3 + (set.IDNum * 17) + 8] = "Frame " + set.designation + " Option 2 text: " + set.initializeTXT[3].getText();
		});
			set.initializeDD[1].addActionListener((ActionEvent event) ->{
				parts[3 + (set.IDNum * 17) + 9] = "";
				parts[3 + (set.IDNum * 17) + 9] = "Frame " + set.designation + " Option 2 requirement: " + set.reqs[set.initializeDD[1].getSelectedIndex()];
				boolean notDone = true;
				for (int i = 0; i < set.listofDDReqs.length; i++)
				{
					if (set.reqs[set.initializeDD[1].getSelectedIndex()].equals(set.listofDDReqs[i]))
					{
						//had to look this up
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
				if (set.reqs[set.initializeDD[1].getSelectedIndex()].equals("none"))
				{
					set.initializeReactTXT[1].setVisible(false);
				}
			});
				set.initializeReactTXT[1].addActionListener((ActionEvent event) ->{
					parts[3 + (set.IDNum * 17) + 10] = "";
					parts[3 + (set.IDNum * 17) + 10] = "Frame " + set.designation + " Option 2 requirement qualifier: " + set.initializeReactTXT[1].getText();
				});
				set.initializeReactDD[1].addActionListener((ActionEvent event) ->{
					parts[3 + (set.IDNum * 17) + 11] = "";
					parts[3 + (set.IDNum * 17) + 11] = "Frame " + set.designation + " Option 2 requirement amount: " + set.reactReqs[set.initializeDD[1].getSelectedIndex()][set.initializeReactDD[1].getSelectedIndex()];
				});
					set.initializeToggle[1].addActionListener((ActionEvent event) -> {
						if (set.initializeReactTXT[4].isVisible())
						{
							set.initializeReactTXT[4].setVisible(false);
							parts[3 + (set.IDNum * 17) + 12] = "";
							set.initializeReactTXT[4].setText("Reward");
						}
						else
						{
							set.initializeReactTXT[4].setVisible(true);
						}
					});
					set.initializeReactTXT[4].addActionListener((ActionEvent event) ->{
						parts[3 + (set.IDNum * 17) + 12] = "";
						parts[3 + (set.IDNum * 17) + 12] = "Frame " + set.designation + " Option 2 ends dialog. Reward: " + set.initializeReactTXT[4].getText();
					});		
		set.initializeTXT[4].addActionListener((ActionEvent event) ->{
			parts[3 + (set.IDNum * 17) + 13] = "";
			parts[3 + (set.IDNum * 17) + 13] = "Frame " + set.designation + " Option 3 text: " + set.initializeTXT[4].getText();
		});
			set.initializeDD[2].addActionListener((ActionEvent event) ->{
				parts[3 + (set.IDNum * 17) + 14] = "";
				parts[3 + (set.IDNum * 17) + 14] = "Frame " + set.designation + " Option 3 requirement: " + set.reqs[set.initializeDD[2].getSelectedIndex()];
				boolean notDone = true;
				for (int i = 0; i < set.listofDDReqs.length; i++)
				{
					if (set.reqs[set.initializeDD[2].getSelectedIndex()].equals(set.listofDDReqs[i]))
					{
						//had to look this up
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
				if (set.reqs[set.initializeDD[2].getSelectedIndex()].equals("none"))
				{
					set.initializeReactTXT[2].setVisible(false);
				}
			});
				set.initializeReactTXT[2].addActionListener((ActionEvent event) ->{
					parts[3 + (set.IDNum * 17) + 15] = "";
					parts[3 + (set.IDNum * 17) + 15] = "Frame " + set.designation + " Option 3 requirement qualifier: " + set.initializeReactTXT[2].getText();
				});
				set.initializeReactDD[2].addActionListener((ActionEvent event) ->{
					parts[3 + (set.IDNum * 17) + 16] = "";
					parts[3 + (set.IDNum * 17) + 16] = "Frame " + set.designation + " Option 3 requirement amount: " + set.reactReqs[set.initializeDD[2].getSelectedIndex()][set.initializeReactDD[2].getSelectedIndex()];
				});
					set.initializeToggle[2].addActionListener((ActionEvent event) -> {
						if (set.initializeReactTXT[5].isVisible())
						{
							set.initializeReactTXT[5].setVisible(false);
							parts[3 + (set.IDNum * 17) + 17] = "";
							set.initializeReactTXT[5].setText("Reward");
						}
						else
						{
							set.initializeReactTXT[5].setVisible(true);
						}
					});
					set.initializeReactTXT[5].addActionListener((ActionEvent event) ->{
						parts[3 + (set.IDNum * 17) + 17] = "";
						parts[3 + (set.IDNum * 17) + 17] = "Frame " + set.designation + " Option 3 ends dialog. Reward: " + set.initializeReactTXT[5].getText();
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
