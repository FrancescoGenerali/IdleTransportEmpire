using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.SceneManagement;

public static class SaveAndLoad
{
    public static bool haveToLoad;

    public static void SaveToJson()
    {
        JsonData dataJ = new JsonData();
        dataJ.currency = Data.currency;
        dataJ.prestige = Data.prestige;
        dataJ.bonusPrestige = Data.bonusPrestige;
        dataJ.actualSceneNumber = Data.actualScene;

        var buildingInScene = GameObject.FindGameObjectsWithTag("Building");
        dataJ.listBuild = new BuildJ[buildingInScene.Length];
        
        for (int i = 0; i < buildingInScene.Length; i++)
        {
            dataJ.listBuild[i] = new BuildJ();
            dataJ.listBuild[i].owned = buildingInScene[i].GetComponent<BuildingManager>().owned;
            dataJ.listBuild[i].nextMult = buildingInScene[i].GetComponent<BuildingManager>().nextMult;
            dataJ.listBuild[i].prevNextMult = buildingInScene[i].GetComponent<BuildingManager>().prevNextMult;
            dataJ.listBuild[i].barCount = buildingInScene[i].GetComponent<BuildingManager>().barCount;
            dataJ.listBuild[i].productionTime = buildingInScene[i].GetComponent<BuildingManager>().productionTime;
        }

        dataJ.jLastLog = DateTime.Now.ToFileTimeUtc(); //DateTime need conversion 'cause isn't serializable

        string json = JsonUtility.ToJson(dataJ, true);
        File.WriteAllText(Application.dataPath+"/Progress.json", json);
    }
    
    public static void LoadSceneFromJson()
    {
        string json = File.ReadAllText(Application.dataPath + "/Progress.json");
        JsonData dataJ = JsonUtility.FromJson<JsonData>(json);

        Data.currency = dataJ.currency + (((DateTime.Now - DateTime.FromFileTimeUtc(dataJ.jLastLog)).TotalSeconds - 3600) * Data.totalProduction); //3600 fixes one hour late during conversion
        
        if (double.IsNaN(Data.currency))
            Data.currency = dataJ.currency; //this bug happened just one time and I wasn't able to replicate it
        
        Data.prestige = dataJ.prestige;
        Data.bonusPrestige = dataJ.bonusPrestige;
        Data.actualScene = dataJ.actualSceneNumber;

        haveToLoad = true;

        SceneManager.LoadScene("Idle_" + (Data.actualScene + 1).ToString());
    }

    public static void LoadFromJson()
    {
        string json = File.ReadAllText(Application.dataPath + "/Progress.json");
        JsonData dataJ = JsonUtility.FromJson<JsonData>(json);

        var buildingInScene = GameObject.FindGameObjectsWithTag("Building");
        Data.totalProduction = 0;

        for (int i = 0; i < dataJ.listBuild.Length; i++)
        {
            buildingInScene[i].GetComponent<BuildingManager>().owned = dataJ.listBuild[i].owned;
            buildingInScene[i].GetComponent<BuildingManager>().nextMult = dataJ.listBuild[i].nextMult;
            buildingInScene[i].GetComponent<BuildingManager>().prevNextMult = dataJ.listBuild[i].prevNextMult;
            buildingInScene[i].GetComponent<BuildingManager>().barCount = dataJ.listBuild[i].barCount;
            buildingInScene[i].GetComponent<BuildingManager>().productionTime = dataJ.listBuild[i].productionTime;
            Data.totalProduction += buildingInScene[i].GetComponent<BuildingManager>().calculateProductivity();
        }

        haveToLoad = false;
    }
}

[Serializable]
public class JsonData
{
    public double currency;
    public int prestige;
    public float bonusPrestige;
    public int actualSceneNumber;
    public long jLastLog;
    public BuildJ[] listBuild;
}

[Serializable]
public class BuildJ
{
    public int owned, nextMult, prevNextMult, barCount;
    public float productionTime;
}
