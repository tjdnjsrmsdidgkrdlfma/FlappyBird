using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainScreenManager : MonoBehaviour
{
    bool is_starting = false;

    TextMeshProUGUI press_start;

    void Awake()
    {
        press_start = GameObject.Find("Canvas/PressStart").GetComponent<TextMeshProUGUI>();
    }

    public void OnStartButtonClick()
    {
        if (is_starting == false)
            StartCoroutine(StartGame());
    }

    IEnumerator StartGame()
    {
        is_starting = true;

        int i;

        for (i = 3; i != 0; i--)
        {
            press_start.text = i.ToString();

            yield return new WaitForSeconds(1.0f);
        }

        press_start.text = "Start!";

        yield return new WaitForSeconds(1.0f);

        SceneManager.LoadScene("InGame");
    }
}