using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainScreenManager : MonoBehaviour
{
    bool is_starting = false;

    GameObject quit_or_not;

    void Awake()
    {
        quit_or_not = GameObject.Find("Canvas").transform.Find("QuitOrNot").gameObject;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
            //quit_or_not.SetActive(true);
        }
    }

    public void OnStartButtonClick()
    {
        if (is_starting == false)
            SceneManager.LoadScene("InGame");
    }

    public void OnQuitButtonClick()
    {
        GameManager.instance.Save();
        Application.Quit();
    }

    public void OnNotButtonClick()
    {
        quit_or_not.SetActive(false);
    }
}