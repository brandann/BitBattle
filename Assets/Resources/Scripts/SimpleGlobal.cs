using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SimpleGlobal : MonoBehaviour {

    // TEMP GLOBAL
    // DELETE FOR REAL PROGRAMMING

    // UI SCORE DISPLAY
    public Text mPlayer1ScoreText;
    public Text mPlayer2ScoreText;
    public Text mPlayer3ScoreText;
    public Text mPlayer4ScoreText;

    // DEATH COUNTS
    private int[] mScores = new int[4];

    public void Death(int PlayerID)
    {
        // INCREMENT THE DEATH COUNT
        mScores[PlayerID-1]++;

        // DISPLAY THE OTHER PLAYERS DEATH COUNT AS YOUR SCORE
        mPlayer1ScoreText.text = "" + mScores[0] * -1 + " P" + 1;
        mPlayer2ScoreText.text = "" + mScores[1] * -1 + " P" + 2;
        mPlayer3ScoreText.text = "" + mScores[2] * -1 + " P" + 3;
        mPlayer4ScoreText.text = "" + mScores[3] * -1 + " P" + 4;
    }
}
