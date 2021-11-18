using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class SaveAndLoad : MonoBehaviour
{
    public void SaveToJson()
    {
        JsonData dataJ = new JsonData();
        dataJ.currency = Data.currency;
        dataJ.prestige = Data.prestige;
        dataJ.actualScene = Data.prestige;

        var buildingInScene = GameObject.FindGameObjectsWithTag("Building");
        dataJ.listBuild = new BuildJ[buildingInScene.Length];
        
        for (int i = 0; i < buildingInScene.Length; i++)
        {
            dataJ.listBuild[i] = new BuildJ();
            dataJ.listBuild[i].owned = buildingInScene[i].GetComponent<SelectBuild>().owned;
            dataJ.listBuild[i].multiplier = buildingInScene[i].GetComponent<SelectBuild>().multiplier;
        }

        dataJ.jLastLog = DateTime.Now.ToFileTimeUtc(); //DateTime need conversion 'cause isn't serializable

        string json = JsonUtility.ToJson(dataJ, true);
        File.WriteAllText(Application.dataPath+"/Progress.json", json);
    }
    
    public void LoadFromJson()
    {
        string json = File.ReadAllText(Application.dataPath + "/Progress.json");
        JsonData dataJ = JsonUtility.FromJson<JsonData>(json);

        var buildingInScene = GameObject.FindGameObjectsWithTag("Building");
        Data.actualProduction = 0;
        
        for (int i = 0; i < dataJ.listBuild.Length; i++)
        {
            buildingInScene[i].GetComponent<SelectBuild>().owned = dataJ.listBuild[i].owned;
            buildingInScene[i].GetComponent<SelectBuild>().multiplier = dataJ.listBuild[i].multiplier;
            Data.actualProduction += buildingInScene[i].GetComponent<SelectBuild>().calculateProductivity();
        }

        Data.currency = dataJ.currency + (((DateTime.Now - DateTime.FromFileTimeUtc(dataJ.jLastLog)).TotalSeconds -3600) * Data.actualProduction); //3600 fixes one hour late during conversion
        Data.prestige = dataJ.prestige;
        Data.actualScene = dataJ.actualScene;
    }
}

[Serializable]
public class JsonData
{
    public double currency;
    public int prestige;
    public int actualScene;
    public long jLastLog;
    public BuildJ[] listBuild;
}

[Serializable]
public class BuildJ
{
    public int owned;
    public int multiplier;
}
