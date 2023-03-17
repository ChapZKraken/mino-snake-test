using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] private Vector2 gridSize = new Vector2(20, 20);

    private Camera mainCamera;

    public List<Snake> players;
    [SerializeField] private Apple apple;

    private void Awake()
    {
        transform.localScale = new Vector3(gridSize.x, gridSize.y, 1);
        transform.localPosition = gridSize / 2;
        transform.localPosition -= Vector3.one * 0.5f;

        mainCamera = GetComponentInChildren<Camera>();
        mainCamera.orthographicSize = gridSize.y / 2;

        apple.Initialize(this);
    }

    public Vector2 GetEmptyTile()
    {
        Vector2 tile = Vector2.zero;

        bool emptyTile = false;
        while(!emptyTile)
        {
            emptyTile = true;

            tile = new Vector2(Random.Range(0, (int)gridSize.x - 1), Random.Range(0, (int)gridSize.y - 1));

            foreach (Snake snake in players)
            {
                if ((Vector2)snake.transform.position == tile)
                {
                    emptyTile = false;
                    break;
                }

                foreach (CircleCollider2D body in snake.bodyParts)
                {
                    if ((Vector2)body.transform.position == tile)
                    {
                        emptyTile = false;
                        break;
                    }
                }
            }

            if ((Vector2)apple.transform.position == tile)
            {
                emptyTile = false;
            }
        }
        

        return tile;
    }
}
