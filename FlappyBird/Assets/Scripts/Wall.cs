using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public float destroy_time;

    float temp;

    void Start()
    {
        StartCoroutine(DestroySelf());
    }

    IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(destroy_time);

        Destroy(gameObject);
    }

    void FixedUpdate()
    {
        temp = transform.position.x;
        Vector2 position = new Vector2(temp - 0.1f, 0);
        transform.position = position;
    }
}
