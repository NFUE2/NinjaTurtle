using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankRecord : MonoBehaviour
{
    public Text HighScore1;
    public Text HighScore2;
    public Text HighScore3;

    int bestScore = 0;
    public string key;
    public void Record(int num,int totalScore)
    {
        string key = "bestScore" + num;
        if(PlayerPrefs .HasKey(key))
        {
            int best = PlayerPrefs .GetInt(key);
            if(best < totalScore)
            {
                PlayerPrefs .SetInt(key, totalScore);
                bestScore = totalScore;
            }
        }
        else
        {
            PlayerPrefs.SetInt(key, totalScore);
            bestScore = totalScore;
        }
    }
    public void RankWrite()
    {
        if (PlayerPrefs.HasKey("bestScore1"))
        {
            int bestScore = PlayerPrefs.GetInt("bestScore1");
            HighScore1.text = bestScore.ToString();
        }
        if (PlayerPrefs.HasKey("bestScore2"))
        {
            int bestScore = PlayerPrefs.GetInt("bestScore2");
            HighScore2.text = bestScore.ToString();
        }
        if (PlayerPrefs.HasKey("bestScore3"))
        {
            int bestScore = PlayerPrefs.GetInt("bestScore3");
            HighScore3.text = bestScore.ToString();
        }
    }

}
