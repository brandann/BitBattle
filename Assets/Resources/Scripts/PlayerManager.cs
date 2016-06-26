using UnityEngine;
using System.Collections;
using witchplease;

public class PlayerManager {
    private Player[] mPlayers;

    public void init()
    {
        mPlayers = new Player[4];
        for(int i = 0; i < mPlayers.Length; i++)
        {
            mPlayers[i] = new Player();
            mPlayers[i].playerID = i + 1;
        }
    }
}
