package one;
import javax.swing.*;
import java.awt.*;
import java.awt.event.ActionEvent;

//September 19 2016, Created by Jimmy Patterson, with help from the Oracle Website and a bunch of swing tutorials
//So far, the point of this is to make an interface that is usable and reports everything to a String that will give the values.
//So technically, an apt Unity programmer could put everything in the game through this tool.
//However, I have no idea what HTML does or how it works, so it doesn't do any of the programming for anyone.
//My goal was to make an interface so other, more knowledgeable people can fine tune it and make it run for HTML.

/*
 * ..................................................................................................
 * ........__________...._________.....___.._____......___.._____...__.....__.......... _______.....
 * ......./____  ___/.../___  ___/..../  |./     |..../  |./     |..\  \../  /......../  ____  /.....
 * .........../ /........../ /......./   |/  ^   /.../   |/  ^   /...\  \/  /......../  /___/ /......
 * ........../ /........../ /......./  /|  /./  /.../  /|  /./  /.....\    /......../  ______/.......
 * ....__.../ /........../ /......./  /.|_/./  /.../  /.|_/./  /....../   /......../  /..............
 * ....\ \_/ /.......___/ /___..../  /...../  /.../  /...../  /....../   /......../  /.....__........
 * .....\___/......./________/.../__/...../__/.../__/...../__/....../___/......../__/...../_/........
 * ..................................................................................................
 * 
 */

public class One extends JFrame
{
	public String aggregateOutput;		//the string where all the info is collected (I don't think that's how it will be in the final version)
	public JButton createEnc; //see next comment
	public One()
	{
		//makes the final button (isn't actually necessary, was thinking about implementing events in multiple methods for this to be a thing but so far it's not)
		createEnc = new JButton("Create Encounter");
		createLayout(createEnc);
		createEnc.setVisible(true);
		initializeUserInterface();
		
	}
	
	public void initializeUserInterface() //This is a huge method, so I split it up.
	{
		initializeWindow();
		initializeTitles();
		initializeOptions();
		initializeButtons();
	}
	
	public void initializeWindow() //sets up the window, sizes it, puts title, etc
	{
		setTitle("Encounter Tool 1.0");
		setSize(3500, 2000);
	}
	
	public void initializeTitles()
	{
		//sets up button for submitting requirements for the encounter to occur.
		JButton defCon = new JButton("Under what conditions should the encounter occur?");
		defCon.setSize(500, 100);
		defCon.setLocation(200, 100);
		defCon.addActionListener((ActionEvent event) -> {
			String what = JOptionPane.showInputDialog(null, "What triggers this encounter?");
			aggregateOutput += "What triggers this encounter?\n\"" + what +"\"";
		});
		createLayout(defCon);
		defCon.setVisible(true);
		
		//sets up text box for filling in the text description for the encounter.
		JTextField txtEnc = new JTextField("Describe the encounter.");
		txtEnc.setSize(2500, 400);
		txtEnc.setLocation(200, 300);
		txtEnc.addActionListener((ActionEvent waitForClick) -> {
			aggregateOutput += "Describe the encounter.\n\"The encounter looks like this:\n" + txtEnc.getText()+"\"";
		});
		createLayout(txtEnc);
		txtEnc.setVisible(true);
	}
	
	public void initializeOptions()
	{
		//initializes defines, places, adds events to all option requirement combo boxes.
		String[] requirements = {"1", "2", "3", "4", "5"};
		JComboBox opReq1 = new JComboBox(requirements);
		opReq1.setSelectedIndex(0);
		opReq1.addActionListener((ActionEvent event) -> {
			aggregateOutput += "For Option 1, the requirement is " + (String)opReq1.getSelectedItem();
		});
		opReq1.setLocation(200, 800);
		opReq1.setSize(300, 100);
		JComboBox opReq2 = new JComboBox(requirements);
		opReq2.setSelectedIndex(0);
		opReq2.addActionListener((ActionEvent event) -> {
			aggregateOutput += "For Option 2, the requirement is " + (String)opReq2.getSelectedItem();
		});
		opReq2.setLocation(200, 1000);
		opReq2.setSize(300, 100);
		JComboBox opReq3 = new JComboBox(requirements);
		opReq3.setSelectedIndex(0);
		opReq3.addActionListener((ActionEvent event) -> {
			aggregateOutput += "For Option 3, the requirement is " + (String)opReq3.getSelectedItem();
		});
		opReq3.setLocation(200, 1200);
		opReq3.setSize(300, 100);
		JComboBox opReq4 = new JComboBox(requirements);
		opReq4.setSelectedIndex(0);
		opReq4.addActionListener((ActionEvent event) -> {
			aggregateOutput += "For Option 4, the requirement is " + (String)opReq4.getSelectedItem();
		});
		opReq4.setLocation(200, 1400);
		opReq4.setSize(300, 100);
		JComboBox opReq5 = new JComboBox(requirements);	
		opReq5.setSelectedIndex(0);
		opReq5.addActionListener((ActionEvent event) -> {
			aggregateOutput += "For Option 5, the requirement is " + (String)opReq5.getSelectedItem();
		});
		opReq5.setLocation(200, 1600);
		opReq5.setSize(300, 100);
		
		
		//initializes, defines, places, adds events to all option text boxes.
		JTextField opTxt1 = new JTextField("Option 1");
		opTxt1.setLocation(700, 800);
		opTxt1.setSize(700, 100);
		opTxt1.addActionListener((ActionEvent waitForClick) -> {
			aggregateOutput += "What is the first option's text?\n" + opTxt1.getText()+"\"";
		});
		JTextField opTxt2 = new JTextField("Option 2");	
		opTxt2.setLocation(700, 1000);
		opTxt2.setSize(700, 100);
		opTxt2.addActionListener((ActionEvent waitForClick) -> {
			aggregateOutput += "What is the second option's text?\n" + opTxt2.getText()+"\"";
		});
		JTextField opTxt3 = new JTextField("Option 3");
		opTxt3.setLocation(700, 1200);
		opTxt3.setSize(700, 100);
		opTxt3.addActionListener((ActionEvent waitForClick) -> {
			aggregateOutput += "What is the third option's text?\n" + opTxt3.getText()+"\"";
		});
		JTextField opTxt4 = new JTextField("Option 4");
		opTxt4.setLocation(700, 1400);
		opTxt4.setSize(700, 100);
		opTxt4.addActionListener((ActionEvent waitForClick) -> {
			aggregateOutput += "What is the fourth option's text?\n" + opTxt4.getText()+"\"";
		});
		JTextField opTxt5 = new JTextField("Option 5");
		opTxt5.setLocation(700, 1600);
		opTxt5.setSize(700, 100);
		opTxt5.addActionListener((ActionEvent waitForClick) -> {
			aggregateOutput += "What is the fifth option's text?\n" + opTxt5.getText()+"\"";
		});
		JComponent[] components = {opReq1, opReq2, opReq3, opReq4, opReq5, opTxt1, opTxt2, opTxt3, opTxt4, opTxt5};
		for(int i = 0; i < components.length; i++)
		{
			createLayout(components[i]);
		}
	}		
	
	
	public void initializeButtons() //sets up create encounter and exit buttons
	{
		createEnc.setLocation(1500, 1600);
		createEnc.setSize(150, 50);
		createEnc.addActionListener((ActionEvent click) -> {
			JOptionPane.showMessageDialog(null, aggregateOutput);
		});
		
		//There is something wrong with this.
		//If I take out the btnQuit definition, the btnExit goes to the top left corner.
		//If I take out the btnExit definition, the btnQuit stays in the top left corner.
		//So I made both, and made the Quit one invisible.
		JButton btnExit = new JButton("Exit");
		btnExit.setSize(150, 50);
		btnExit.setLocation(1700, 1600);
		btnExit.addActionListener((ActionEvent event)-> {
			System.exit(0);
		});
		createLayout(btnExit);
			
		//I think the weirdness is here somewhere- it may also be in createLayout
		JButton btnQuit = new JButton("Quit");
		btnQuit.setSize(150, 50);
		btnQuit.setLocation(2000, 1600);
		btnQuit.addActionListener((ActionEvent event)->{
			System.exit(0);
		});
		createLayout(btnQuit);
		btnQuit.setVisible(false);
	}
	
	public void createLayout(JComponent... arg) { //I got this method off the Internet, it basically puts the components on the Frame. I think.
		Container pane = getContentPane();
		GroupLayout g1 = new GroupLayout(pane);
		pane.setLayout(g1);
		g1.setAutoCreateContainerGaps(true);
		g1.setHorizontalGroup(g1.createSequentialGroup().addComponent(arg[0]));
		g1.setVerticalGroup(g1.createSequentialGroup().addComponent(arg[0]));
	}
	
	public static void main(String[] args) //main method yay
	{
		EventQueue.invokeLater(() -> {
			One go = new One();
			go.setVisible(true);
		});

	}
	
	

}
