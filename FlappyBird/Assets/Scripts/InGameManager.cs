using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameManager : MonoBehaviour
{
    public int score;

    float spawn_wall_delay;
    bool on_ready = false;

    GameObject walls_prefab;
    GameObject up_down_walls_prefab;
    GameObject Canvas;
    TextMeshProUGUI score_text;

    void Start()
    {
        SetDefaultValue();

        StartCoroutine(PrepareTime());

        StartCoroutine(SpawnWalls());
    }

    void SetDefaultValue()
    {
        spawn_wall_delay = GameManager.instance.spawn_wall_delay;

        walls_prefab = Resources.Load("Prefabs/Walls") as GameObject;
        up_down_walls_prefab = Resources.Load("Prefabs/UpDownWalls") as GameObject;
        Canvas = GameObject.Find("Canvas");
        score_text = GameObject.Find("Canvas/Score").GetComponent<TextMeshProUGUI>();
    }

    IEnumerator PrepareTime()
    {
        on_ready = true;
        Time.timeScale = 0;

        TextMeshProUGUI temp = GameObject.Find("Canvas/Score").GetComponent<TextMeshProUGUI>();
        int i;

        for (i = 3; i != 0; i--)
        {
            temp.text = i.ToString(); Debug.Log(i);

            yield return new WaitForSecondsRealtime(1.0f);
        }

        temp.text = "Start!";

        yield return new WaitForSecondsRealtime(1.0f);

        on_ready = false;
        Time.timeScale = 1;
    }

    IEnumerator SpawnWalls()
    {
        float random_y_value;

        while (true)
        {
            random_y_value = Random.Range(-2.9f, 2.9f);

            Vector2 walls = new Vector2(4f, random_y_value);
            Vector2 up_down_walls = new Vector2(4f, 0);

            Instantiate(walls_prefab, walls, Quaternion.identity);
            Instantiate(up_down_walls_prefab, up_down_walls, Quaternion.identity);

            yield return new WaitForSeconds(spawn_wall_delay);
        }
    }

    void Update()
    {
        if(on_ready == false)
            score_text.text = score.ToString();
    }

    public void OnDeath()
    {
        Time.timeScale = 0;

        Canvas.transform.Find("OnDeath").gameObject.SetActive(true);

        if (score > GameManager.instance.highest_score)
        {
            GameManager.instance.highest_score = score;
            GameManager.instance.GetComponent<GameManager>().Save();
            Canvas.transform.Find("OnDeath").transform.Find("CurrentScore").GetComponent<TextMeshProUGUI>().text = "New Record";
        }
        else
        {
            Canvas.transform.Find("OnDeath").transform.Find("CurrentScore").GetComponent<TextMeshProUGUI>().text = "Current Score: " + score;
        }

        Canvas.transform.Find("OnDeath").transform.Find("BestScore").GetComponent<TextMeshProUGUI>().text = "Best Score: " + GameManager.instance.highest_score;
    }

    public void RestartGameButtonClicked()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("InGame");
    }

    public void GoToMainScreenButtonClicked()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainScreen");
    }
}
