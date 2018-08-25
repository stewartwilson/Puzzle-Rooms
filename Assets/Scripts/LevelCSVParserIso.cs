using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelCSVParserIso : MonoBehaviour {

    public GameObject tiles;
    public TextAsset csvFile;
    public GameObject player;
    public GameObject baseTile;
    public GameObject hazards;
    public List<LevelObject> levelObjects;
    public bool hasPopulated = false;

    public string[] _lines;

    public void PopulateLevelFromFile()
    {
        if (!hasPopulated)
        {
            if (csvFile != null)
            {

                int y = 0;
                _lines = Regex.Split(csvFile.text, "\n");
                foreach (string _line in _lines)
                {
                    int x = 0;
                    string[] _tiles = _line.Split(',');

                    foreach (string _tile in _tiles)
                    {
                        if (_tile != null && !_tile.Equals(""))
                        {
                            GameObject go = InitGameObjectFromKey(_tile);
                            go.transform.position = IsometricHelper.gridToGamePostion(x,y);
                            go.GetComponent<SpriteRenderer>().sortingOrder = IsometricHelper.getTileSortingOrder(x, y);
                            if (go.tag.Equals("Enemy")) {
                                GameObject floor = Instantiate(baseTile, go.transform.position, Quaternion.identity);
                                floor.GetComponent<SpriteRenderer>().sortingOrder = IsometricHelper.getTileSortingOrder(x, y);
                                floor.transform.SetParent(tiles.transform);
                                string name = floor.name.Replace("(Clone)", "");
                                floor.name = name;
                                go.transform.SetParent(hazards.transform);

                            } else
                            {
                                go.transform.SetParent(tiles.transform);
                            }
                            if (_tile.Contains("enter"))
                            {
                                GameObject _player = Instantiate(player);
                                string name = _player.name.Replace("(Clone)", "");
                                _player.name = name;
                                _player.transform.position = IsometricHelper.gridToGamePostion(x, y);
                                _player.GetComponent<SpriteRenderer>().sortingOrder = IsometricHelper.getTileSortingOrder(x, y);
                            }
                        } else if(_tile.Equals(""))
                        {
                            GameObject floor = Instantiate(baseTile, IsometricHelper.gridToGamePostion(x, y), Quaternion.identity);
                            floor.GetComponent<SpriteRenderer>().sortingOrder = IsometricHelper.getTileSortingOrder(x, y);
                            floor.transform.SetParent(tiles.transform);
                            string name = floor.name.Replace("(Clone)", "");
                            floor.name = name;
                        }
                        x++;
                    }
                    y++;
                }
                hasPopulated = true;
            }
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
                string name = go.name.Replace("(Clone)", "");
                go.name = name;
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
                    string name = go.name.Replace("(Clone)", "");
                    go.name = name;
                    switch (_enemy[1])
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
