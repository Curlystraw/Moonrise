package swingtest;
import java.awt.*;

import java.awt.event.ActionEvent;
import javax.swing.*;
public class Swing extends JFrame {
	
	//A CONSTRUCTOR IN THE MAIN METHOD? WHAAT?
	public Swing()
	{
		//Making a button
		JButton qButt = new JButton("Quit");
		
		//This defines what the quit button does.
		qButt.addActionListener((ActionEvent event) -> {
			System.exit(0);
		});
		createLayout(qButt);
		
		//Setting title and size of frame
		setTitle("test 1");
		setSize(3000, 2000);
		
	}
	
	//This thing helps make any component
	public void createLayout(JComponent... arg) {
		Container pane = getContentPane();
		GroupLayout g1 = new GroupLayout(pane);
		pane.setLayout(g1);
		//makes gaps so it doesnt go over
		g1.setAutoCreateContainerGaps(true);
		//let the button exist in the vertical and horizontal axes
		g1.setHorizontalGroup(g1.createSequentialGroup().addComponent(arg[0]));
		g1.setVerticalGroup(g1.createSequentialGroup().addComponent(arg[0]));
	}
	
	
	public static void main(String[] args) 
	{
		//THIS IS THE THING THAT DOES THE STUFF. THIS METHOD. DO ALL THE STUFF IN THE BRACKETS.
		EventQueue.invokeLater(() -> {
			Swing a1 = new Swing();
			a1.setVisible(true);
		});
		
		

	}

}
