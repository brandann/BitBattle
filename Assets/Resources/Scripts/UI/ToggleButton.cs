using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ToggleButton : Button {

	protected List<string> mToggleList;
	private int _currentIndex = 0;
	
	public ToggleButton(GameObject button, List<string> items) : base(button)
	{
		mToggleList = items;
	}
	
	public string GetCurrentItem(){
		if(null != mToggleList && _currentIndex < mToggleList.Count)
			return mToggleList[_currentIndex];
		return "";
	}
	
	public override void OnClick()
	{
		base.OnClick();
		_currentIndex++;
		if(_currentIndex >= mToggleList.Count)
			_currentIndex = 0;
	}
}
