using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneController : MonoBehaviour
{
    [SerializeField] GameObject StartScreen;
    [SerializeField] GameObject Credits;

    public void LoadGame()
    {
        SceneManager.LoadScene(1);
    }
    public void GoToCredits()
    {
        StartScreen.SetActive(false);
        Credits.SetActive(true);
    }
    public void GetBack()
    {
        Credits.SetActive(false);
        StartScreen.SetActive(true);
    }
}
