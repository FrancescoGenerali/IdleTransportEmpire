using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    private void Awake()
    {
        Screen.SetResolution(Mathf.FloorToInt(Screen.height*0.5625f), Screen.height, true);
    }

    void Start()
    {
        if (File.Exists(Application.dataPath + "/Progress.json"))
            SaveAndLoad.LoadSceneFromJson();
        else
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
