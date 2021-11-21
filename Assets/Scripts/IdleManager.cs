using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class IdleManager : MonoBehaviour
{
    private double totalProduction;
    
    private void Awake()
    {
        if (SaveAndLoad.haveToLoad)
            SaveAndLoad.LoadFromJson();
        else if (Data.prestige == 0)
        {
            Data.prestige = 1;
            Data.bonusPrestige = 1;
        }
    }

    void Start()
    {
        StartCoroutine(autosave());
    }
    
    void Update()
    {
        //------CHEAT------//

        // DOUBLE MONEY
        if (Input.GetKeyDown(KeyCode.M)){
            Data.currency += 1 + Data.currency * 2;
        }

        //ONE HOUR AHEAD
        if (Input.GetKeyDown(KeyCode.H))
        {
            GameObject[] Buildings = GameObject.FindGameObjectsWithTag("Building");
            Data.currency += (DateTime.Now.AddHours(1) - DateTime.Now).TotalSeconds * Data.calculateTotalProduction(Buildings);
        }

        //-----------------//
    }

    public void nextScene()
    {
        Data.currency = 0;
        Data.prestige++;
        Data.updateBonusPrestige();
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