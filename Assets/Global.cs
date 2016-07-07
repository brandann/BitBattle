using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace witchplease
{
    public enum ePlayerDeathEvents { Projectile, Lava }
    public enum ePlayerID { Player1 = 1, Player2 = 2, Player3 = 3, Player4 = 4 }
    public enum eScenes {game, Menu}

    public class Global : MonoBehaviour
    {

        // UI SCORE DISPLAY
        public Text mPlayer1ScoreText;
        public Text mPlayer2ScoreText;
        public Text mPlayer3ScoreText;
        public Text mPlayer4ScoreText;

        // DEATH COUNTS
        private static int[] mScores = new int[4];
        private static float[] mHealth = new float[4];

        public static void Death(ePlayerID id)
        {
            // INCREMENT THE DEATH COUNT
            mScores[(int)id - 1]--;

            // TEMP TODO HANDLE DEATH AS A REGEN AFTERWARDS WITH 100% HEALTH
            mHealth[(int)id - 1] = 1;
        }

        public static void Health(ePlayerID id, float delta, bool set = false)
        {
            // DELTA THE HEALTH
            if (set)
                mHealth[(int)id -1] = delta;
            else
                mHealth[(int)id - 1] += delta;
            mHealth[(int)id - 1] = Mathf.Clamp(mHealth[(int)id - 1], 0, 1);
        }

        public static float Health(ePlayerID id)
        {
            return mHealth[(int) id - 1];
        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            mPlayer1ScoreText.text = " P1: " + mScores[0] + "\n(" + (int)(100 * mHealth[0]) + "%)";
            mPlayer2ScoreText.text = " P2: " + mScores[1] + "\n(" + (int)(100 * mHealth[1]) + "%)";
            mPlayer3ScoreText.text = " P3: " + mScores[2] + "\n(" + (int)(100 * mHealth[2]) + "%)";
            mPlayer4ScoreText.text = " P4: " + mScores[3] + "\n(" + (int)(100 * mHealth[3]) + "%)";
        }
    }
}

