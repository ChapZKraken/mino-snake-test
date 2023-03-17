using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Snake : MonoBehaviour
{
    [SerializeField] private CircleCollider2D bodyPrefab;
    public List<CircleCollider2D> bodyParts;

    private Vector2 currentDirection = new Vector2(1, 0);
    private Vector2 newDirection = new Vector2(1, 0);

    private List<Vector2> previousPositions = new List<Vector2>();

    public void UpdatePosition()
    {
        SavePreviousPosition();

        currentDirection = newDirection;
        transform.position += new Vector3(currentDirection.x, currentDirection.y);

        for (int i = 0; i < bodyParts.Count; i++)
        {
            CircleCollider2D body = bodyParts[i];
            body.transform.position = previousPositions[i];
            body.enabled = true;
        }
    }

    private void SavePreviousPosition()
    {
        previousPositions.Insert(0, transform.position);

        if (previousPositions.Count > bodyParts.Count)
            previousPositions.RemoveAt(previousPositions.Count - 1);
    }

    public void OnMove(InputValue value)
    {
        ChangeDirection(value.Get<Vector2>());
    }

    private void ChangeDirection(Vector2 inputDirection)
    {
        if (inputDirection.sqrMagnitude != 1) //Prevents diagonal input
            return;

        if (inputDirection == -currentDirection) //Prevents going in the opposing direction
            return;

        newDirection = inputDirection;
    }

    public void Grow()
    {
        Vector2 spawnPosition = previousPositions.Count == 0 ? transform.position : previousPositions[previousPositions.Count - 1];

        CircleCollider2D body = Instantiate(bodyPrefab, spawnPosition, Quaternion.identity);
        bodyParts.Add(body);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(gameObject.tag))
        {
            Dead();
        }
    }

    public void Dead()
    {
        foreach(var body in bodyParts)
        {
            Destroy(body.gameObject);
        }

        bodyParts.Clear();

        Destroy(gameObject);
    }
}
