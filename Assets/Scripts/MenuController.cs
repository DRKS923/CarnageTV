using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject mainScreen, aboutScreen;
    
    public void OnButtonPlayGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void OnAboutGame()
    {
        mainScreen.SetActive(false);
        aboutScreen.SetActive(true);
    }

    public void OnButtonBack()
    {
        mainScreen.SetActive(true);
        aboutScreen.SetActive(false);
    }

    public void OnButtonQuit()
    {
        Application.Quit();
    }

}
