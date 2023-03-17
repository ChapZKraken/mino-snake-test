using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounds : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Snake snake = collision.GetComponent<Snake>();
        snake.Dead();
    }
}
