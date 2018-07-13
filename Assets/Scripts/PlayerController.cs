using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float transitionSpeed;
    public Facing facing;

    public int xPosition;
    public int yPosition;

    public float inputDelay;

    private float inputWaitTime;

    public bool isMoving;

    public Vector2 movementTarget;

    private GameController gameController;

    private LevelData levelData;

	// Use this for initialization
	void Start () {
        movementTarget = transform.position;
        isMoving = false;
        if(gameController == null)
        {
            gameController = GameObject.Find("GameController").GetComponent<GameController>();
        }
        if (levelData == null)
        {
            levelData = GameObject.Find("LevelData").GetComponent<LevelData>();
        }
    }
	
	// Update is called once per frame
	void Update () {
        float _horizontal = Input.GetAxis("Horizontal");
        float _vertical = Input.GetAxis("Vertical");
        Vector2 movement = Vector2.zero;
        if (!isMoving)
        {
            bool _attemptMove = false;
            Vector3 _movementTarget = Vector3.zero;
            if (_horizontal > 0)
            {
                facing = Facing.Right;
                _movementTarget = transform.position + new Vector3(1, 0, 0);
                _attemptMove = true;
            }
            else if (_horizontal < 0)
            {
                facing = Facing.Left;
                _movementTarget = transform.position + new Vector3(-1, 0, 0);
                _attemptMove = true;
            }
            else if (_vertical > 0)
            {
                facing = Facing.Up;
                _movementTarget = transform.position + new Vector3(0, 1, 0);
                _attemptMove = true;
            }
            else if (_vertical < 0)
            {
                facing = Facing.Down;
                _movementTarget = transform.position + new Vector3(0, -1, 0);
                _attemptMove = true;
            }

            if(_attemptMove)
            {
                if (levelData.isCellPositionValid(_movementTarget))
                {
                    StartCoroutine(TakeTurn(_movementTarget));
                }
            }
        }


            float step = transitionSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position,movementTarget, step);
        

        

    }

    IEnumerator TakeTurn(Vector3 _movementTarget)
    {
        isMoving = true;
        movementTarget = _movementTarget;
        yield return new WaitForSeconds(1f);
        isMoving = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision " + collision.tag);
        if(collision.tag.Equals("LevelExit"))
        {
            string nextLevelName = collision.gameObject.GetComponent<LevelExit>().nextLevelName;
            gameController.loadLevel(nextLevelName);
        }
        if(collision.tag.Equals("Enemy"))
        {
            gameController.reloadLevel();
        }
    }
}
