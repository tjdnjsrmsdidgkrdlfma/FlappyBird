using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Wall : MonoBehaviour
{
    float destroy_time;
    float move_speed;

    void Start()
    {
        SetDefaultValue();

        StartCoroutine(DestroySelf());
    }

    void SetDefaultValue()
    {
        destroy_time = GameManager.instance.destroy_time;
        move_speed = GameManager.instance.move_speed;
    }

    IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(destroy_time);

        Destroy(gameObject);
    }

    void FixedUpdate()
    {
        MoveWall();
    }

    void MoveWall()
    {
        float temp;

        temp = transform.position.x;
        Vector2 position = new Vector2(temp - move_speed, this.transform.position.y);
        transform.position = position;
    }
}
