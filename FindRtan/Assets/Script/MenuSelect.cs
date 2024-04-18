using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSelect : MonoBehaviour
{
    public RankRecord rankRecord;

    public GameObject StageMenu;
    public GameObject RankMenu;
    public GameObject StandardMenu;

    
    public GameObject BlockStage2;
    public GameObject BlockStage3;

    public void OpenStage()
    {
        StandardMenu.SetActive(false);
        StageMenu.SetActive(true);
        if(GameManager.usingStage >=2)
        {
            BlockStage2.SetActive(false);
        }
        if (GameManager.usingStage >= 3)
        {
            BlockStage3.SetActive(false);
        }
        else
        {
            BlockStage2.SetActive(true);
            BlockStage3.SetActive(true);
        }
    }

    public void OpenRank()
    {
        StandardMenu.SetActive(false);
        RankMenu.SetActive(true);
        rankRecord.RankWrite();
    }

    public void CloseMenu()
    {
        StandardMenu.SetActive(true);
        RankMenu.SetActive(false);
        StageMenu.SetActive(false);
    }
}
