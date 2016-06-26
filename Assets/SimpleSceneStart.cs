using UnityEngine;
using System.Collections;

public class SimpleSceneStart : MonoBehaviour {	
	public void OnPlayButtonClick()
	{
		print ("OnPlayButtonClick");
		GlobalVars.CurrentGameScene = GlobalVars.GAME_SCENES.game;
	}
}
