using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameManager : MonoBehaviour
{
    float spawn_wall_delay;

    GameObject gamemanager;
    GameObject player;
    GameObject walls_prefab;
    GameObject up_down_walls_prefab;

    void Awake()
    {
        player = GameObject.Find("Player");
        walls_prefab = Resources.Load("Prefabs/Walls") as GameObject;
        up_down_walls_prefab = Resources.Load("Prefabs/UpDownWalls") as GameObject;
    }

    void Start()
    {
        spawn_wall_delay = 2f;

        StartCoroutine(SpawnWalls());
    }

    IEnumerator SpawnWalls()
    {
        float random_y_value;

        while(true)
        {
            yield return new WaitForSeconds(spawn_wall_delay);

            random_y_value = Random.Range(-2f, 2f);

            Vector2 walls = new Vector2(player.transform.position.x + 2.8f, random_y_value); //2.8
            Vector2 up_down_walls = new Vector2(player.transform.position.x + 2.8f, 0);

            Instantiate(walls_prefab, walls, Quaternion.identity);
            Instantiate(up_down_walls_prefab, up_down_walls, Quaternion.identity);
        }
    }
}
