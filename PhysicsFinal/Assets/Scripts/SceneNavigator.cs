using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneNavigator : MonoBehaviour
{
    [Serializable]
    public class SceneRef
    {
        public string name;
        public int index;

        public SceneRef(string name, int index)
        {
            this.name = name;
            this.index = index;
        }
    }

    public SceneRef[] sceneList;

    public void GoToSceneAtIndex(int index)
    {
        SceneManager.LoadScene(index);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GoToSceneAtIndex(0);
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            GoToSceneAtIndex(1);
        }
    }

    public void EndItAll()
    {
        Application.Quit();
    }
}
