using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Minigame_Manager : MonoBehaviour
{
    public GameObject popUp;
    private GameObject startCollider;

    private void Awake()
    {
        startCollider = GameObject.FindGameObjectWithTag("MG_Start"); //avoid scale passengers before start
    }

    private void Start()
    {
        startCollider.SetActive(false);
        popUp.SetActive(true);
        Time.timeScale = 0f;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.N))
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void closePopUp()
    {
        startCollider.SetActive(true);
        popUp.SetActive(false);
        Time.timeScale = 1f;
    }
}