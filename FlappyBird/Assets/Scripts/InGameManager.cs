using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameManager : MonoBehaviour
{
    float spawn_wall_delay;

    GameObject walls_prefab;
    GameObject up_down_walls_prefab;
    GameObject Canvas;
    TextMeshProUGUI score;

    void Start()
    {
        SetDefaultValue();

        StartCoroutine(SpawnWalls());
    }

    void SetDefaultValue()
    {
        spawn_wall_delay = GameManager.instance.spawn_wall_delay;

        walls_prefab = Resources.Load("Prefabs/Walls") as GameObject;
        up_down_walls_prefab = Resources.Load("Prefabs/UpDownWalls") as GameObject;
        Canvas = GameObject.Find("Canvas");
        score = GameObject.Find("Canvas/Score").GetComponent<TextMeshProUGUI>();
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
        score.text = GameManager.instance.score.ToString();
    }

    public void OnDeath()
    {
        Time.timeScale = 0;
        Canvas.transform.Find("OnDeath").gameObject.SetActive(true);
        Canvas.transform.Find("OnDeath").transform.Find("BestScore").GetComponent<TextMeshProUGUI>().text = "Best Score: "; // 나중에 추가
        Canvas.transform.Find("OnDeath").transform.Find("CurrentScore").GetComponent<TextMeshProUGUI>().text = "Current Score: " + GameManager.instance.score;
    }

    public void RestartGameButtonClicked()
    {
        SceneManager.LoadScene("InGame");
    }

    public void GoToMainScreenButtonClicked()
    {
        SceneManager.LoadScene("MainScreen");
    }
}
