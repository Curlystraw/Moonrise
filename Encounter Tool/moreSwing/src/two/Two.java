package two;
import java.awt.*;
import java.awt.event.ActionEvent;
import java.awt.event.KeyEvent;
import java.awt.event.KeyListener;

import javax.swing.*;

//CURRENT FILE STATE: All implementations of the available frame components are in the frame except the first option walk away thing. Dunno why that is.
//Second, none of the action events are there yet, but that will take awhile anyway.
//Third, there is definitely a better way to do this but I had a lot of trouble getting anything to work the way I did it until this really rudimentary solution.
//That should be looked in to.

public class Two extends JFrame
{
	static int numDialogues;
	String aggregateOutput, title;
	String[] flagList, attributeList;
	DialogueBox[] frames = new DialogueBox[10];
	
	FlagBox[] flags;
	String[] exampleArray = new String[4];
	public Two()
	{
		exampleArray[0] = "what";  exampleArray[1] = "is";	exampleArray[2] = "going";	exampleArray[3] = "on";
		numDialogues = 1;
		aggregateOutput = ""; title = "";
		initializeUserInterface();
		for (int i = 0; i < frames.length; i++)
		{
			frames[i] = new DialogueBox(exampleArray, exampleArray, "Name");
		}
	}

	public void initializeUserInterface()
	{
//		for (int i = 0; i < numDialogues; i ++)
//		{
//			frames[i] = new DialogueBox(); //that's not how this works.
//		}
//		for (int i = 0; i < numDialogues; i ++)
//		{
//			flags[i] = new FlagBox(frames[i].type); //that's not how any of this works.
//		}
		//Actually, that is. Weird. But it works in the constructor instead cause that's better.
		initializeWindow();
		initializeButtons();
		initializeNav();
		initializeFrame();
	}
	
	public void initializeWindow()
	{
		setSize(3600, 2100); //3600, 2100 OR 1920, 1080
		setTitle("Encounter Tool");
	}
	
	public void initializeButtons()
	{
		boolean titleDone = false;
		JLabel txtDesc = new JLabel("Name the encounter. Press ENTER after finishing.");
		JTextField txtTitle = new JTextField();
		txtDesc.setLocation(20, 25);
		txtTitle.setLocation(20, 40);
		txtDesc.setSize(300, 10);
		txtTitle.setSize(200, 20);
		txtTitle.addActionListener((ActionEvent event) -> {
//			JOptionPane.showMessageDialog(null, "Event Triggered."); <--- test to see when event happens. (It happens on pressing ENTER)
			title = txtTitle.getText();
		});
		add(txtDesc);
		add(txtTitle);
		
		JButton btnCreate = new JButton("Create Encounter");
		JButton btnExit = new JButton("Exit");
		btnCreate.setLocation(40, 1900);
		btnCreate.setSize(175, 50);
		btnExit.setLocation(300, 1900);
		btnExit.setSize(100, 50);
		btnCreate.addActionListener((ActionEvent event) -> {
			aggregateOutput = "";
			setUpString();
			JOptionPane.showMessageDialog(null, aggregateOutput);
		});
		btnExit.addActionListener((ActionEvent event) -> {
			System.exit(0);
		});
		add(btnCreate);
		add(btnExit);
				
		JLabel lblFiller = new JLabel("This shouldn't be in the top corner."); //It is. Wtf.
		lblFiller.setSize(400, 50);
		lblFiller.setLocation(500, 500);
		add(lblFiller);
		lblFiller.setVisible(false);
	}
	
	public void setUpString() //this method should put all the data in the String in the correct order. The plan is that every new piece will be labeled.
	{
		String nothing = "";
		if (!title.equals(nothing))
		{
			aggregateOutput += "<title>" + title + "</title>";
		}
		
		for (int i = 0; i < frames.length; i++)
		{
			aggregateOutput += frames[i].boxTexts[6];
			aggregateOutput += frames[i].boxTexts[7];
			aggregateOutput += frames[i].boxTexts[8];
			aggregateOutput += frames[i].boxTexts[9];
		}
	}
	
	public void initializeNav() //As of now, the tree doesn't work.
	{
		Tree tree = new Tree();
		tree.setLocation(3000, 1300);
		tree.setSize(500, 500);
		add(tree);
		
		JLabel x = new JLabel();
		add(x);
	}
	
	public void initializeFrame() //As of now there's a problem here that I don't know.
	{
		//DialogueBox one = new DialogueBox(exampleArray, exampleArray, "Box 1.0");
		setUpFrame(frames[0]); //specifically with this method
		//one.setLocation(20, 100);
		//add((JPanel)one);
		
		JLabel lblFiller = new JLabel("This shouldn't be in the top corner."); //It is. Wtf.
		lblFiller.setSize(400, 50);
		lblFiller.setLocation(500, 500);
		add(lblFiller);
		lblFiller.setVisible(false);
	}
	
	//Something tells me the JFrame.add(Component) method works just as well....
	public void createLayout(JComponent... arg) 	//I got this method off the Internet, it basically puts the components on the Frame. I think.
	{ 
		Container pane = getContentPane();
		GroupLayout g1 = new GroupLayout(pane);
		pane.setLayout(g1);
		g1.setAutoCreateContainerGaps(true);
		g1.setHorizontalGroup(g1.createSequentialGroup().addComponent(arg[0]));
		g1.setVerticalGroup(g1.createSequentialGroup().addComponent(arg[0]));
	}
	
	public void takeDownFrame(DialogueBox previous) //As of yet, this is not implemented because multiple frames aren't implemented yet.
	{
		for (Component i : previous.stuff)
		{
			i.setVisible(false);
		}
	}
	
	public void setUpFrame(DialogueBox box) //for some reason the JComponent casted versions of box.stuff are not recognized.
	{
		//This was my first solution. For some reason it won't work.
//		for (Component i : box.stuff)
//		{
//			add(i);
//		}
		//Initial working code
		setUpComponents(box);
		add(box.title);
		add(box.introText);
		//add(box.a1);
		add(box.option1);
		add(box.option2);
		add(box.flagb1);
		add(box.flagb2);
		add(box.flagc1);
		add(box.flagc2);
		//New test with old for loops
//		setUpComponents(box);
//		for (int i = 0; i < box.stuff.length; i++)
//		{
//			add(box.stuff[i]);
//		}
	}
	
	public void setUpComponents(DialogueBox box)
	{
		box.title.setLocation(20, 100);
    	box.title.setSize(100, 20);
    	
    	//stackoverflow solution that I don't understand
//    	box.title.addKeyListener(new KeyListener(){
//    	public void keyPressed(KeyEvent e)
//    	{
//    		if (e.getKeyCode() == KeyEvent.VK_ENTER)
//    		{
//    			
//    		}
//    	}
//    	
//    	public void keyTyped(KeyEvent e){}
//    	public void keyReleased(KeyEvent e){}
//    	});
    	
    	box.introText.setLocation(20, 130);
    	box.introText.setSize(150, 50);
    	
    	//box.a1.setLocation(20, 190);
    	//box.a1.setSize(200, 50);
    	
    	box.option1.setLocation(20, 250);
    	box.option1.setSize(200, 50);
    	
    	box.option2.setLocation(20, 310);
    	box.option2.setSize(200, 50);
    	
    	//def.setBounds(20, 350, 600, 50);
    	
    	box.flagsb1 = exampleArray;
    	box.flagsb2 = exampleArray;
    	box.flagsc1 = exampleArray;
    	box.flagsc2 = exampleArray;
    	
    	box.flagb1=new JComboBox(box.flagsb1);
    	box.flagb2=new JComboBox(box.flagsb2);
    	box.flagc1=new JComboBox(box.flagsc1);
    	box.flagc2=new JComboBox(box.flagsc2);
    	
    	box.flagb1.setLocation(240, 250);
    	box.flagb2.setLocation(300, 250);
    	box.flagc1.setLocation(240, 310);
    	box.flagc2.setLocation(300, 310);
    	
    	box.flagb1.setSize(50, 50);
    	box.flagb2.setSize(50, 50);
    	box.flagc1.setSize(50, 50);
    	box.flagc2.setSize(50, 50);
    	
    	box.flagb1.addActionListener((ActionEvent event)->{
    		box.boxTexts[6] = "<option2requirement1>" + box.flagb1.getToolTipText() + "</option2requirement1>";
    	});
    	box.flagb2.addActionListener((ActionEvent event)->{
    		box.boxTexts[7] = "<option2requirement2>" + box.flagb2.getToolTipText() + "</option2requirement2>";
    	});
    	box.flagc1.addActionListener((ActionEvent event)->{
    		box.boxTexts[8] = "<option3requirement1>" + box.flagc1.getToolTipText() + "</option3requirement1>";
    	});
    	box.flagc2.addActionListener((ActionEvent event)->{
    		box.boxTexts[9] = "<option3requirement2>" + box.flagc2.getToolTipText() + "</option3requirement2>";
    	});
    	
//    	flagb1.setBounds(400,420,100,50);
//    	flagb2.setBounds(520,420,100,50);
//    	flagc1.setBounds(400,490,100,50);
//    	flagc2.setBounds(520,490,100,50); 
	}

	public static void main(String[] args) {
		EventQueue.invokeLater(() -> {
			Two go = new Two();
			go.setVisible(true);
		});

	}
	
}
