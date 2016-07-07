using UnityEngine;
using System.Collections;
using witchplease;

public class ButtonClicks : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void OnPanelChange(GameObject panel)
	{
		panel.SetActive(true);
		
	}
	
	public void OnPlayButtonClick()
	{
		print ("OnPlayButtonClick");
		GlobalVars.CurrentGameScene = GlobalVars.GAME_SCENES.game;
	}
}
