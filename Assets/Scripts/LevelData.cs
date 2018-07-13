using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelData : MonoBehaviour {

    Tilemap tilemap;

    private void Start()
    {
        tilemap = GetComponentInChildren<Tilemap>();
        PopulateHazardsList();
    }

    private void PopulateHazardsList()
    {
        List<GameObject> _hazards = new List<GameObject>();
        foreach (Transform child in GameObject.Find("Hazards").transform)
        {
            _hazards.Add(child.gameObject);
        }
        GameObject.Find("GameController").GetComponent<GameController>().SetHazards(_hazards);
    }

    public bool isCellPositionValid(Vector3 _position)
    {
        Vector3Int _cellPosition =  tilemap.LocalToCell(_position);
        Debug.Log(_cellPosition);
        TileBase _tile = tilemap.GetTile(_cellPosition);
        if (_tile != null)
        {
            Debug.Log("tile found" + _tile.name);
            if (_tile.name.Contains("wall"))
            {
                return false;
            } else
            {
                return true;
            }
        } else
        {
            return false;
        }

    }
}
