﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelData : MonoBehaviour {

    Tilemap tilemap;

    private void Start()
    {
        tilemap = GetComponentInChildren<Tilemap>();
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
