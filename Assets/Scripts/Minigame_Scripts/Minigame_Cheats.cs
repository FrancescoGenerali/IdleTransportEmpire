using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Minigame_Cheats : MonoBehaviour
{
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.N))
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}