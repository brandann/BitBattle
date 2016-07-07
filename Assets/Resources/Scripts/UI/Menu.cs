using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Menu {

	protected List<Button> MenuButtons;
	protected int mCurrentSelectedItem;
	
	public Menu()
	{
		MenuButtons = new List<Button>();
	}
	
	public void Add(Button button)
	{
		MenuButtons.Add (button);
	}
}
