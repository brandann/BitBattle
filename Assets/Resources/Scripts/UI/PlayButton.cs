using UnityEngine;
using System.Collections;
using witchplease;

public class PlayButton : MonoBehaviour {
	
	public void OnClick()
	{
		Application.LoadLevel(eScenes.game.ToString());
	}
}
