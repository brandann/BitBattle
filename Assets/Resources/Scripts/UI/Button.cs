using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Button {
	
	private GameObject _button;
	
	public Button(GameObject button)
	{
		_button = button;
		Name = _button.GetComponentInChildren<Text>().text;
	}
	
	public virtual void OnClick()
	{
		
	}
	
	public GameObject GetButton()
	{
		return _button;
	}
	
	public string Name
	{
		get;
		protected set;
	}
	
	public void Hover()
	{
	
	}
}
