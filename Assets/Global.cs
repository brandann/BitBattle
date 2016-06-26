using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace witchplease
{
    public enum ePlayerDeathEvents { Projectile, Lava }
    public enum ePlayerID { Player1 = 1, Player2 = 2, Player3 = 3, Player4 = 4 }

    public class Global : MonoBehaviour
    {

        // UI SCORE DISPLAY
        public Text mPlayer1ScoreText;
        public Text mPlayer2ScoreText;
        public Text mPlayer3ScoreText;
        public Text mPlayer4ScoreText;

        // DEATH COUNTS
        private static int[] mScores = new int[4];

        public static void Death(int PlayerID)
        {
            // INCREMENT THE DEATH COUNT
            mScores[PlayerID - 1]--;
        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            mPlayer1ScoreText.text = " P1: " + mScores[0];
            mPlayer2ScoreText.text = " P2: " + mScores[1];
            mPlayer3ScoreText.text = " P3: " + mScores[2];
            mPlayer4ScoreText.text = " P4: " + mScores[3];
        }
    }
}

