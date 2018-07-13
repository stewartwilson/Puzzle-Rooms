using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public float transitionSpeed;
    public Facing facing;

    public int xPosition;
    public int yPosition;

    public bool isMoving;

    public Vector2 movementTarget;

    public List<Vector2> path;

    public bool repeatPath;

    private LevelData levelData;

    // Use this for initialization
    void Start()
    {
        levelData = GameObject.Find("LevelData").GetComponent<LevelData>();
        movementTarget = transform.position;
        isMoving = false;
    }

    // Update is called once per frame
    void Update()
    {
        float step = transitionSpeed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, movementTarget, step);
    }

    IEnumerator TakeTurn(Vector3 _movementTarget)
    {
        isMoving = true;
        movementTarget = _movementTarget;
        yield return new WaitForSeconds(1f);
        isMoving = false;
    }

    public void TakeTurn()
    {
        Vector2 _movementTarget = Vector2.zero;
        if (!isMoving)
        {
            switch (facing)
            {
                case Facing.Down:
                    _movementTarget = transform.position + new Vector3(0, -1, 0);
                    break;
                case Facing.Up:
                    _movementTarget = transform.position + new Vector3(0, 1, 0);
                    break;
                case Facing.Left:
                    _movementTarget = transform.position + new Vector3(-0, 0, 0);
                    break;
                case Facing.Right:
                    _movementTarget = transform.position + new Vector3(1, 0, 0);
                    break;
            }
            if (levelData.isCellPositionValid(_movementTarget))
            {
                movementTarget = _movementTarget;
            }
        }
    }
}
