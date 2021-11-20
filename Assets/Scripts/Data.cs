using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Data
{
    public static double currency;
    public static double totalProduction;
    public static int prestige;
    public static float bonusPrestige;
    public static int actualScene;

    public static float updateBonusPrestige()
    {
        bonusPrestige = 1;
        int tempPrestige = prestige;

        while(tempPrestige > 1)
        {
            bonusPrestige += 0.2f;
            tempPrestige--;
        }
        return bonusPrestige;
    }

    public static double calculateTotalProduction(GameObject[] buildingInScene)
    {
        foreach (GameObject building in buildingInScene)
        {
            totalProduction += building.GetComponent<BuildingManager>().calculateProductivity();
        }
        return totalProduction;
    }
}
