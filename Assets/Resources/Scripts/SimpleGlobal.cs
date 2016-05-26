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
        mScores[PlayerID-1]--;
    }

    void Start()
    {
    }

    void Update()
    {
        mPlayer1ScoreText.text = " P1: " + mScores[0];
        mPlayer2ScoreText.text = " P2: " + mScores[1];
        mPlayer3ScoreText.text = " P3: " + mScores[2];
        mPlayer4ScoreText.text = " P4: " + mScores[3];
    }
}
