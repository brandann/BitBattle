using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SimpleGlobal : MonoBehaviour {

    // TEMP GLOBAL
    // DELETE FOR REAL PROGRAMMING

    // UI SCORE DISPLAY
    public Text mPlayer1ScoreText;
    public Text mPlayer2ScoreText;

    // DEATH COUNTS
    private int[] mScores = new int[2];

    public void Death(int PlayerID)
    {
        // INCREMENT THE DEATH COUNT
        mScores[PlayerID-1]++;

        // DISPLAY THE OTHER PLAYERS DEATH COUNT AS YOUR SCORE
        mPlayer1ScoreText.text = "" + mScores[1];
        mPlayer2ScoreText.text = "" + mScores[0];
    }
}
