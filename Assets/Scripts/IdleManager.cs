using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IdleManager : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(autosave());
    }

    //SOLO TESTING CODE
    public GameObject bike;
    public GameObject bus;
    public Text textBike;
    public Text textBus;
    public Text busOwn;
    public Text bikOwn;
    public Text money;
    
    void Update()
    {
        textBike.text = "bikeMult " + bike.GetComponent<SelectBuild>().multiplier.ToString();
        textBus.text = "busMult " + bus.GetComponent<SelectBuild>().multiplier.ToString();
        busOwn.text = "busOwn " + bus.GetComponent<SelectBuild>().owned.ToString();
        bikOwn.text = "bikeOwn " + bike.GetComponent<SelectBuild>().owned.ToString();
        money.text = Data.currency.ToString();
    }

    public void resetSave()
    {
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