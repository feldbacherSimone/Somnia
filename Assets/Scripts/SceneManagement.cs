using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class SceneManagement : MonoBehaviour
{
    Scene mainScene;
    [SerializeField] private string decorationSceneName;
    [SerializeField] private bool debug; 
    private void Start()
    {
        mainScene = SceneManager.GetActiveScene();
        if (decorationSceneName != null)
        {
            if (SceneManager.sceneCount < 2)
                SceneManager.LoadScene(decorationSceneName, LoadSceneMode.Additive);
            else
                PrintDebug("Scene was already loaded"); 
        }
        else
            PrintDebug("No decoration Scene Found, you forgot to add it, idiot"); 
    }

    void PrintDebug(string message)
    {
        if (debug)
        {
            Debug.Log(message);
        }
    }
}
