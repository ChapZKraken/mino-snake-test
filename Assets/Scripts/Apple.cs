using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{
    private Grid grid;

    public void Initialize(Grid grid)
    {
        this.grid = grid;

        Spawn();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Snake snake = collision.GetComponent<Snake>();
        snake.Grow();

        Spawn();
    }

    private void Spawn()
    {
        Vector2 emptyTile = grid.GetEmptyTile();

        transform.position = emptyTile;
    }
}
