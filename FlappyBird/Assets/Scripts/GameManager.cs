using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.XR;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int highest_score;

    string file_name;
    string file_path;

    #region 플레이어 관련 변수
    public float jump_power;
    public float fall_power;
    #endregion

    #region 벽 관련 변수
    public float spawn_wall_delay;
    public float destroy_time;
    public float move_speed;
    #endregion

    void Awake()
    {
        GameObject[] temp = GameObject.FindGameObjectsWithTag("GameManager");

        if (temp.Length == 1)
        {
            DontDestroyOnLoad(temp[0]);
        }
        else
        {
            int i;
            for (i = 1; i < temp.Length; i++)
            {
                Destroy(temp[i].gameObject);
            }
        }

        instance = this;

        file_name = "file_p";
        file_path = Application.persistentDataPath + "/" + file_name + ".json";

        jump_power = 35f;
        fall_power = 2.5f;

        spawn_wall_delay = 2f;
        destroy_time = 5f;
        move_speed = 0.05f;

        if (File.Exists(file_path) == true)
        {
            Load();
        }
        else
        {
            ResetData();
            Save();
        }
    }

    void ResetData()
    {
        highest_score = 0;
    }

    public void Save()
    {
        Data save_data = new Data();

        save_data.highest_score = highest_score;

        string json = JsonUtility.ToJson(save_data);

        File.WriteAllText(file_path, json);
    }

    public void Load()
    {
        string json = File.ReadAllText(file_path);

        Data load_data = JsonUtility.FromJson<Data>(json);

        MoveDataToLocal(load_data);
    }

    void MoveDataToLocal(Data load_data)
    {
        highest_score = load_data.highest_score;
    }
}

public class Data
{
    public int highest_score;
}