using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    float jump_power;

    bool jump = false;
    bool on_jump_delay = false;

    GameObject in_game_manager;
    Rigidbody2D rigidbody2d;

    void Start()
    {
        SetDefaultValue();
    }

    void SetDefaultValue()
    {
        jump_power = GameManager.instance.jump_power;

        in_game_manager = GameObject.Find("InGameManager");
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        GetInput();
    }

    void GetInput()
    {
        if (Input.GetMouseButtonDown(0) == true)
            jump = true;
        else
            jump = false;
    }

    void FixedUpdate()
    {
        //if (jump == true && on_jump_delay == false)
        //{
            //StartCoroutine(Jump());
            if(jump==true)
            Jump_();
        //}
    }

    IEnumerator Jump()
    {
        on_jump_delay = true;

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

    void Jump_()
    {
        Debug.Log("A");
        rigidbody2d.AddForce(Vector2.up);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Wall") == true)
        {
            in_game_manager.GetComponent<InGameManager>().OnDeath();
        }
        else if (other.gameObject.CompareTag("CheckPassWall") == true)
        {
            in_game_manager.GetComponent<InGameManager>().score++;
        }
    }
}
