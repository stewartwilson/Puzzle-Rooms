using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelData : MonoBehaviour {

    public List<Vector3> validMoves = new List<Vector3>();
    public GameObject tiles;
    public GameObject hazards;

    public void PopulateLevelData()
    {
        PopulateHazardsList();
        PopulateValidMovesList();
    }

    public void PopulateHazardsList()
    {
        List<GameObject> _hazards = new List<GameObject>();
        foreach (Transform child in hazards.transform)
        {
            _hazards.Add(child.gameObject);
        }
        GameObject.Find("GameController").GetComponent<GameController>().SetHazards(_hazards);
    }

    public void PopulateValidMovesList()
    {
        List<GameObject> _tiles = new List<GameObject>();
        foreach (Transform child in tiles.transform)
        {
            if(child.gameObject.tag.Equals("Walkable"))
            {
                validMoves.Add(child.position);
            }
        }
    }

    public bool isMoveValid(Vector3 _position)
    {
        if(validMoves.Contains(_position))
        {
            return true;
        } else
        {
            return false;
        }

    }
}
