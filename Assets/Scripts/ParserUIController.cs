using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParserUIController : MonoBehaviour {

    public GameObject levelParser;

	public void GenerateLevel()
    {
        if(levelParser == null)
        {
            levelParser = GameObject.Find("LevelParser");
        }

        levelParser.GetComponent<LevelCSVParser>().PopulateLevelFromFile();
    }
}
