using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float fall_power;
    public float jump_power;

    bool jump = false;
    bool on_jump_delay = false;
    bool falling = false;

    GameObject gamemanager;
    Rigidbody2D rigidbody2d;

    void Awake()
    {
        //gamemanager = GameObject.Find("GameManager");
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    void Start()
    {

    }

    void Update()
    {
        GetInput();
    }

    void GetInput()
    {
        if (Input.GetMouseButtonDown(0) == true)
            //jump = !jump;
            jump = true;
        else
            jump = false;
    }

    void FixedUpdate()
    {
        if (jump == true && on_jump_delay == false)
        {
            StartCoroutine(Jump());
        }
        else if (jump == false && on_jump_delay == false && falling == false)
        {
            StartCoroutine(Fall());
        }
    }

    IEnumerator Jump()
    {
        on_jump_delay = true;
        falling = false;

        int i;
        float temp = jump_power;

        for (i = 0; i < 10; i++)
        {
            temp = Mathf.Lerp(temp, 0, 0.5f);

            rigidbody2d.velocity = new Vector2(0, temp);

            yield return new WaitForSeconds(0.01f);
        }

        rigidbody2d.velocity = Vector2.zero;

        on_jump_delay = false;
    }

    IEnumerator Fall()
    {
        falling = true;

        int i;
        float temp = 0;

        for (i = 0; i < 10; i++)
        {
            temp = Mathf.Lerp(temp, fall_power, 0.5f);

            rigidbody2d.velocity = new Vector2(0, temp) * -1;

            yield return new WaitForSeconds(0.01f);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Wall") == true)
        {
            Debug.Log("Game Over");
        }
    }
}
