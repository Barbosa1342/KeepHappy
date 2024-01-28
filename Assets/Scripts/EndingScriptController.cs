using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class EndingScriptController : MonoBehaviour
{
    [SerializeField] GameObject[] textContainer;
    [SerializeField] GameObject prizeButton;
    [SerializeField] GameObject quitButton;

    private void Start()
    {
        StartCoroutine(Prize());
    }

    IEnumerator Prize()
    {
        yield return new WaitForSeconds(3f);
        for (int i = 0; i < textContainer.Length; i++)
        {
            textContainer[i].SetActive(true);

            yield return new WaitForSeconds(4f);

            if (i != textContainer.Length - 1)
            {
                for (float color = 1f; color >= 0f; color -= 0.01f)
                {
                    textContainer[i].GetComponent<TextMeshProUGUI>().color = new Color(1f, 1f, 1f, color);
                    yield return null;
                }
                textContainer[i].SetActive(false);
            }
        }
        yield return new WaitForSeconds(5f);
        prizeButton.SetActive(true);

        yield return new WaitForSeconds(15f);
        quitButton.SetActive(true);
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }
}
