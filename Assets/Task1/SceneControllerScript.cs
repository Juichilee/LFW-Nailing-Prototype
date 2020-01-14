using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControllerScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("LoadSceneA");
    }

    public void LoadScene(string sceneName)
    {
        Debug.Log("Loaded Scene: " + sceneName);
        SceneManager.LoadScene(sceneName);
    }
}
