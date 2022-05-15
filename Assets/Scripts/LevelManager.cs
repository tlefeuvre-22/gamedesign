using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    int index;
    private void Start()
    {
        index = SceneManager.GetActiveScene().buildIndex;
    }
    void Update()
    {
        if (Board.Instance.Enemys.Count == 0 && index + 1 < SceneManager.sceneCountInBuildSettings)
        {
            //SceneManager.UnloadSceneAsync(index);
            SceneManager.LoadScene(index + 1, LoadSceneMode.Single);
        }
        if (Board.Instance.pieces.Count == 0)
        {
            //SceneManager.UnloadSceneAsync(index);
            SceneManager.LoadScene(0, LoadSceneMode.Single);
            
        }
        
    }
}
