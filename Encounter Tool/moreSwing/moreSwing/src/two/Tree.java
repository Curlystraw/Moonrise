package two;

import java.awt.*;
import java.awt.event.*;

import javax.swing.*;
import javax.swing.tree.*;

public class Tree extends JTree
{
	public JTree tree;
	
	public void createNodes(DefaultMutableTreeNode node)
	{
		DefaultMutableTreeNode category;
		category = new DefaultMutableTreeNode("Option 1");
		if (node.toString().equals("Option 0"))
		{
			node.add(category);
		}
		
		
	}
	
	public Tree()
	{
		
		DefaultMutableTreeNode begin = new DefaultMutableTreeNode("Option 0");
		createNodes(begin);
		
		tree = new JTree(begin);
	}
	
	public String toString()
	{
		return "";
	}

}
