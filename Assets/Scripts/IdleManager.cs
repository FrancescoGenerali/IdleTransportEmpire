using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IdleManager : MonoBehaviour
{
    private void Awake()
    {
        if (Data.prestige == 0)
        {
            Data.prestige = 1;
            Data.bonusPrestige = 1;
        }
    }

    void Start()
    {
        StartCoroutine(autosave());
    }

    //SOLO TESTING CODE
    public Text money;
    public Text prestige;
    
    void Update()
    {
        money.text = CashConverter.doubleToString(Data.currency);
        prestige.text = CashConverter.doubleToString(Data.prestige);
        Debug.Log(Data.bonusPrestige);
        //CHEAT AUMENTA SOLDI
        if (Input.GetKeyDown(KeyCode.P)){
            Data.currency += 1 + Data.currency * 2;
        }
    }

    public void nextScene()
    {
        Data.currency = 0;
        Data.prestige++;
        Data.updateBonusPrestige();
        Data.actualScene++;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void resetSave()
    {
        SceneManager.LoadScene(0);
        SaveAndLoad.resetSave();
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