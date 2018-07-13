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

    // Use this for initialization
    void Start()
    {
        movementTarget = transform.position;
        isMoving = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 movement = Vector2.zero;
        if (!isMoving)
        {
            
        }

    }

    IEnumerator TakeTurn(Vector3 movement)
    {
        isMoving = true;
        movementTarget = transform.position + movement;
        yield return new WaitForSeconds(1f);
        isMoving = false;
    }
}
