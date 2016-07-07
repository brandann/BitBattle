using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainMenu : MonoBehaviour {
	
	List<Button> menu;
	public GameObject[] UIButtons;
	private int _currentButtonHover;
	private const int ERROR = -1;
	
	void Start()
	{
		menu = new List<Button>();
		//menu.Add(new PlayButton(UIButtons[0]));
		//menu.Add (new OptionsButton(UIButtons[1]));
		//menu.Add (new QuitButton(UIButtons[2]));
	}
	
	void Update()
	{
		if(Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
		{
			//HANDLE MOVING THE HOVER DOWN
			_currentButtonHover -= 1;
			UpdateHoverButton();
		}
		else if(Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
		{
			//HANDLE MOVING THE HOVER UP
			_currentButtonHover += 1;
			UpdateHoverButton();
		}
		else if(Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.KeypadEnter))
		{
			//HANDLE MAKING A SELECTION
			SelectButton();
		}
	}
	
	private void ChangeHover(int delta)
	{
		_currentButtonHover += delta;
		if(_currentButtonHover < 0)
			_currentButtonHover = menu.Count - 1;
		else if(_currentButtonHover >= menu.Count)
			_currentButtonHover = 0;
	}
	
	public void HandleClick(string s)
	{
		int index = contains(s);
		if(index != ERROR)
		{
			_currentButtonHover = index;
			UpdateHoverButton();
			SelectButton();
		}
	}
	
	private void UpdateHoverButton()
	{
		HoverButton(_currentButtonHover);
	}
	
	private void HoverButton(int index)
	{
		_currentButtonHover = index;
		if(_currentButtonHover < 0)
			_currentButtonHover = menu.Count - 1;
		else if(_currentButtonHover >= menu.Count)
			_currentButtonHover = 0;
		menu[_currentButtonHover].Hover();
	}
	
	private void SelectButton()
	{
		SelectButton(_currentButtonHover);
	}
	
	private void SelectButton(int index)
	{
		if(index < menu.Count && index >= 0)
		{
			print (menu[index].Name + "Click");
			menu[index].OnClick();
			_currentButtonHover = index;
		}
	}
	
	private int contains(string s)
	{
		for(int i = 0; i < menu.Count; i++)
		{
			if(menu[i].Name == s)
				return i;
		}
		return ERROR;
	}
	
	private int contains(GameObject go)
	{
		for(int i = 0; i < menu.Count; i++)
		{
			if(menu[i].GetButton() == go)
				return i;
		}
		return ERROR;
	}
}
