using UnityEngine;
using System.Collections;

public class BackButton : MonoBehaviour {

	public GameObject ShowPanel;
	public GameObject HidePanel;
	
	public void OnClick()
	{
		ShowPanel.SetActive(true);
		HidePanel.SetActive(false);
	}
}
