using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelect : MonoBehaviour
{
    

    public int type;

    public void SelectStage()
    {
        if(GameManager.usingStage>=type)
        {
            SceneManager.LoadScene(type);
        }
        else
        {
            Debug.Log("X");
        }
    }
}
