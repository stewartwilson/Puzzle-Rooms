using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public List<Vector2> path;
    public Facing facing;
    public bool repeatPath;

    private float transitionSpeed;
    private Vector2 movementTarget;
    private GameController gameController;
    private LevelData levelData;

    // Use this for initialization
    void Start()
    {
        if (gameController == null)
        {
            gameController = GameObject.Find("GameController").GetComponent<GameController>();
            transitionSpeed = gameController.movementSpeed;
        }
        levelData = GameObject.Find("LevelData").GetComponent<LevelData>();
        movementTarget = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float step = transitionSpeed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, movementTarget, step);
    }

    public void TakeTurn()
    {
        MakeMove();
    }

    private void MakeMove()
    {
        Vector2 _movementTarget = Vector2.zero;
        switch (facing)
        {
            case Facing.Down:
                _movementTarget = transform.position + new Vector3(0, -1, 0);
                break;
            case Facing.Up:
                _movementTarget = transform.position + new Vector3(0, 1, 0);
                break;
            case Facing.Left:
                _movementTarget = transform.position + new Vector3(-1, 0, 0);
                break;
            case Facing.Right:
                _movementTarget = transform.position + new Vector3(1, 0, 0);
                break;
        }
        Debug.Log("Enemy Move: " + _movementTarget + "Facing " + facing);
        if (levelData.isMoveValid(_movementTarget))
        {
            movementTarget = _movementTarget;
        } else
        {
            FlipAndMove();
        }
    }

    private void FlipAndMove()
    {
        switch (facing)
        {
            case Facing.Down:
                facing = Facing.Up;
                break;
            case Facing.Up:
                facing = Facing.Down;
                break;
            case Facing.Left:
                facing = Facing.Right;
                break;
            case Facing.Right:
                facing = Facing.Left;
                break;
        }
        MakeMove();
    }
    private void TurnAndMove()
    {
        switch (facing)
        {
            case Facing.Down:
                facing = Facing.Left;
                break;
            case Facing.Up:
                facing = Facing.Right;
                break;
            case Facing.Left:
                facing = Facing.Up;
                break;
            case Facing.Right:
                facing = Facing.Down;
                break;
        }
        MakeMove();
    }

    public void SetFacing(Facing _facing)
    {
        facing = _facing;
    }

}
