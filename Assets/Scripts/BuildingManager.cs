using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuildingManager : MonoBehaviour
{
    [Tooltip("Select from the scriptable Objects")]
    public Buildings thisBuildType;

    [SerializeField]
    private TextMeshProUGUI costUI, ownedUI, productivityUI;

    [HideInInspector]
    public int owned, multiplier;
    [HideInInspector]
    public float productionTime;
    [HideInInspector]
    public double cost;

    private void Awake()
    {
        if (productionTime == 0)
            productionTime = thisBuildType.initialTime;
        
        if (multiplier == 0)
            multiplier = 1;
    }

    private void Start()
    {
        StartCoroutine(gainCurrency());
        cost = calculateCost();
        updateUI();
    }

    public void buy()
    {
        if (Data.currency >= cost)
        {
            raiseOwned();
            Data.currency -= cost;
        }
        cost = calculateCost();
        updateUI();
    }

    public void updateUI()
    {
        costUI.text = cost.ToString();
        ownedUI.text = owned.ToString();
        productivityUI.text = calculateProductivity().ToString();
    }

    public void raiseOwned()
    {
        owned++;
        if (owned == 25 || owned == 50 || owned == 100 || owned == 200 || owned == 300 || owned == 400)
        {
            productionTime /= 2;
            multiplier++;
        }
    }

    public double calculateProductivity()
    {
        return (thisBuildType.initialRevenue / productionTime) * owned * multiplier;
    }

    public double calculateCost()
    {
        return thisBuildType.initialCost * Mathf.Pow(thisBuildType.coefficient, owned);
    }

    IEnumerator gainCurrency()
    {
        for(;;)
        {
            Data.currency += calculateProductivity();
            yield return new WaitForSeconds(productionTime);
        }
    }

    public void initialReset()
    {
        owned = 0;
        multiplier = 1;
        productionTime = thisBuildType.initialTime;
        cost = calculateCost();
        updateUI();
    }
}
