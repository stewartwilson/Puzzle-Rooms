using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    private List<Vector2> validPositions;
    private List<GameObject> hazards;
    public string currentLevelName = "Level1";

    private void Start()
    {
        hazards = new List<GameObject>();
        foreach (Transform child in GameObject.Find("Hazards").transform)
        {
            hazards.Add(child.gameObject);
        }
    }

    public void TakeTurn()
    {
        foreach(GameObject go in hazards)
        {
            EnemyController ec = go.GetComponent<EnemyController>();
            if(ec != null)
            {
                ec.TakeTurn();
            }
        }
    }

    public void loadLevel(string newlevelName)
    {
        SceneManager.LoadScene(newlevelName, LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync(currentLevelName);
        currentLevelName = newlevelName;

    }

    public void reloadLevel()
    {
        SceneManager.UnloadSceneAsync(currentLevelName);
        SceneManager.LoadScene(currentLevelName, LoadSceneMode.Additive);

    }

}
