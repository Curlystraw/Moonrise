import javax.swing.*;
import java.awt.*;
import java.awt.event.ActionEvent;
public class OutputFrame 
{
	//JFrame home;
	JTextArea text = new JTextArea();
	JButton back = new JButton("Close");
	JScrollBar scroll;
	
	public OutputFrame()
	{
		text.setText("");
		text.setLocation(50, 50);
		text.setSize(900, 700);
		text.setOpaque(true);
		text.setWrapStyleWord(true);
		text.setBackground(Color.white);
		back.setLocation(400, 800);
		back.setSize(200, 100);
		back.addActionListener((ActionEvent event)->{
			setWords("");
			setVisible(false);
		});
		scroll = new JScrollBar(JScrollBar.VERTICAL, 30, 40, 0, 300);
		
	}
	
	public void setWords(String text)
	{
		this.text.setText(text);
	}
	
	public void setVisible(boolean bool)
	{
		text.setVisible(bool);
		back.setVisible(bool);
	}
}
