using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    public TMP_Text gameOverText;
    static public SceneController sceneControllerObj;

    [SerializeField] GameObject gameHUD;
    [SerializeField] GameObject gameOverScreen;

    [SerializeField] HealthSystem healthScript;

    [SerializeField] Image[] healthContainer;
    [SerializeField] Sprite fullHeart;
    [SerializeField] Sprite emptyHeart;

    [SerializeField] Spawner spawner;
    private void Awake()
    {
        sceneControllerObj = this;
    }
    void Start()
    {
        if (gameHUD.activeSelf == false)
        {
            gameHUD.SetActive(true);
        }

        if (gameOverScreen.activeSelf == true)
        {
            gameOverScreen.SetActive(false);
        }

        StartCoroutine(WinScreen());
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GameOverScreen()
    {
        SoundController.soundController.GameOverSound();
        spawner.StopAllCoroutines();
        gameHUD.SetActive(false);

        gameOverScreen.SetActive(true);
        gameOverText.text = RandomText();
    }
    
    string RandomText()
    {
        List<string> textList = new()
        {
            "What do you mean: I don't pay my bills?",
            "I said don't call the ambulance. They did call.",
            "Sometimes I think if a pigeon's life would be easier...",
            "I should quit the 3$ coffee.",
            "If you require rest, now is not the time.",
            "F",
            "First time?",
            "I think, so I quit.",
            "An hour ago I started to question my sanity."
        };

        int randomChoice = Random.Range(0, textList.Count);
        return textList[randomChoice];
    }

    public void UpdateHeart()
    {
        for (int i = 0; i < healthContainer.Length; i++)
        {
            if (i < healthScript.CurrentHealth)
            {
                healthContainer[i].sprite = fullHeart;
            }
            else
            {
                healthContainer[i].sprite = emptyHeart;
            }
        }
    }

    IEnumerator WinScreen()
    {
        yield return new WaitForSeconds(200f);

        if (gameOverScreen.activeSelf == false)
        {
            SceneManager.LoadScene(2);
        }
    }

}
