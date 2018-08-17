using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelCSVParser : MonoBehaviour {

    public Tilemap tilemap;
    public TextAsset csvFile;
    public GameObject player;
    public List<LevelObject> levelObjects;
    private bool hasPopulated = false;

    public string[] _lines;


    private void Update()
    {
        if(!hasPopulated)
        {
            PopulateLevelFromFile();
        }
    }

    private void PopulateLevelFromFile()
    {
        if (csvFile != null)
        {
            
            int y = 0;
            _lines = Regex.Split(csvFile.text, "\n");
            foreach(string _line in _lines)
            {
                int x = 0;
                string[] _tiles = _line.Split(',');
                
                foreach (string _tile in _tiles)
                {
                    if (_tile != null && !_tile.Equals("")) { 
                        GameObject go = InitGameObjectFromKey(_tile);
                        go.transform.position = new Vector2(x, -y);
                        go.transform.SetParent(tilemap.gameObject.transform);
                        if(_tile.Contains("enter"))
                        {
                            GameObject _player = Instantiate(player);
                            _player.transform.position = new Vector2(x, -y);
                        }
                    }
                    x++;
                }
                y++;
            }
            hasPopulated = true;
        }
    }

    private GameObject InitGameObjectFromKey(string _key)
    {
        _key = _key.Trim();
        foreach(LevelObject _levelObject in levelObjects)
        {
            if(_key.Equals(_levelObject.key))
            {
                GameObject go = Instantiate(_levelObject.go);
                return go;
            } 
            if(_key.Equals("enter"))
            {
                
            }
        }
        if (_key.StartsWith("e"))
        {
            string[] _enemy = _key.Split('_');
            foreach (LevelObject _levelObject in levelObjects)
            {
                if (_enemy[0].Equals(_levelObject.key))
                {
                    GameObject go = Instantiate(_levelObject.go);
                    switch(_enemy[1])
                    {
                        case "L":
                            go.GetComponent<EnemyController>().SetFacing(Facing.Left);
                            break;
                        case "R":
                            go.GetComponent<EnemyController>().SetFacing(Facing.Right);
                            break;
                        case "U":
                            go.GetComponent<EnemyController>().SetFacing(Facing.Up);
                            break;
                        case "D":
                            go.GetComponent<EnemyController>().SetFacing(Facing.Down);
                            break;
                    }
                    return go;
                }
            }
        }
        return null;
    }

    [System.Serializable]
    public struct LevelObject
    {
        public string key;
        public GameObject go;
    }

}
