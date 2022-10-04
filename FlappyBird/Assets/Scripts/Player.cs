using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    float jump_power;

    bool jump = false;

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
        if (jump == true)
            Jump();
    }

    void Jump()
    {
        rigidbody2d.velocity = Vector2.zero;
        rigidbody2d.AddForce(Vector2.up * jump_power, ForceMode2D.Impulse);
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
