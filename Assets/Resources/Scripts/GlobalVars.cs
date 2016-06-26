using UnityEngine;
using System.Collections;

public class GlobalVars : MonoBehaviour {

	#region ENUMS
	public enum GAME_SCENES {Menu = 0, game = 1};
	#endregion
	
	private static GAME_SCENES _currentGameScene;
	public static GAME_SCENES CurrentGameScene
	{
		get{
			return _currentGameScene;
		}
		set
		{
			_currentGameScene = value;
			Application.LoadLevel(value.ToString());
		}
	}
}
