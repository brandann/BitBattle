using UnityEngine;
using System.Collections;
using witchplease;

public class OptionsButton : MonoBehaviour {

	public GameObject ShowPanel;
	public GameObject HidePanel;
	
	public void OnClick()
	{
		ShowPanel.SetActive(true);
		HidePanel.SetActive(false);
	}
}
