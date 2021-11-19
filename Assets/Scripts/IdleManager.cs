using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IdleManager : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(autosave());
    }

    //SOLO TESTING CODE
    public Text money;
    
    void Update()
    {
        money.text = Data.currency.ToString();

        //CHEAT AUMENTA SOLDI
        if (Input.GetKeyDown(KeyCode.P)){
            Data.currency += 1 + Data.currency * 2;
        }
    }

    public void resetSave()
    {
        SaveAndLoad.resetSave();
    }

    public void nextScene()
    {
        Data.currency = 0;
        Data.actualScene++;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    IEnumerator autosave()
    {
        for(;;)
        {
            SaveAndLoad.SaveToJson();
            yield return new WaitForSeconds(1);
        }
    }
}