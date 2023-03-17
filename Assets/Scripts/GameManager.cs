using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    private int currentFrame;
    private int gameUpdate = 10;

    [SerializeField] private Grid grid;

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    private void Update()
    {
        currentFrame++;

        if (currentFrame < gameUpdate)
            return;

        currentFrame = 0;

        foreach (Snake player in grid.players)
        {
            player.UpdatePosition();
        }
    }

    private void OnPlayerJoined(PlayerInput input)
    {
        grid.players.Add(input.GetComponent<Snake>());
    }

    private void OnPlayerLeft(PlayerInput input)
    {
        grid.players.Remove(input.GetComponent<Snake>());
    }
}
