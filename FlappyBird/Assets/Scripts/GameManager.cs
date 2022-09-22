using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int score;
    public int highest_score;

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
        instance = this;

        score = 0;

        jump_power = 50f;
        fall_power = 2.5f;

        spawn_wall_delay = 2f;
        destroy_time = 5f;
        move_speed = 0.1f;
    }
}
