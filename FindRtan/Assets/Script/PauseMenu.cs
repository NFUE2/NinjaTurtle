using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject Menu;
    // public Camera Camera;

    public void Pause()
    {
        Menu.SetActive(true);
        Time.timeScale = 0.0f;
        // Camera.GetComponent<PostProcessVolume>().isGlobal = true;
    }

    public void Continu()
    {
        Time.timeScale = 1.0f;
        Menu.SetActive(false);
        // Camera.GetComponent<PostProcessVolume>().isGlobal = false;
    }
    public void ReSetGame()
    {
        SceneManager.LoadScene(0);
        Menu.SetActive(false);
        // Camera.GetComponent<PostProcessVolume>().isGlobal = false;
    }

    public void ReStart()
    {
        SceneManager.LoadScene(GameManager.Instance.level);
        Menu.SetActive(false);
        // Camera.GetComponent<PostProcessVolume>().isGlobal = false;
    }
}
