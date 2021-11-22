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
    [SerializeField]
    private GameObject nextMultBar;

    [HideInInspector]
    public int owned, nextMult, prevNextMult, barCount;
    [HideInInspector]
    public float productionTime;
    [HideInInspector]
    public double cost;
    [HideInInspector]
    public bool reachMax;

    private void Awake()
    {
        if (Data.currency == 0)
        {
            initialReset();
        }
    }

    private void Start()
    {
        StartCoroutine(gainCurrency());

        if (thisBuildType.shouldStartFree && owned == 0)
            cost = 0;
        else
            cost = calculateCost();

        updateUI();
    }

    public void buy()
    {
        if (Data.currency >= cost && !reachMax)
        {
            raiseOwned();
            Data.currency -= cost;
            cost = calculateCost();
            updateUI();
        }
    }

    public void updateUI()
    {
        if (reachMax)
            costUI.text = "MAX";
        else if (cost == 0)
            costUI.text = "FREE";
        else
            costUI.text = CashConverter.doubleToString(cost);

        ownedUI.text = owned.ToString();
        nextMultBar.GetComponent<Slider>().value = (float)barCount/(nextMult-prevNextMult);
        productivityUI.text = CashConverter.doubleToString(calculateProductivity());
    }

    public void raiseOwned()
    {
        owned++;
        barCount++;
        if (owned == nextMult)
        {
            reachNextMult();
            productionTime /= 2;
        }
    }

    public void reachNextMult()
    {
        if (!(nextMult == thisBuildType.nextMult[thisBuildType.nextMult.Length - 1]))
        {
            for (int i = 0; i < thisBuildType.nextMult.Length - 1; i++)
            {
                if (nextMult == thisBuildType.nextMult[i])
                {
                    prevNextMult = nextMult;
                    nextMult = thisBuildType.nextMult[i + 1];
                    barCount = 0;
                    return;
                }
            }
        }
        else
        {
            reachMax = true;
        }
    }

    public double calculateProductivity()
    {
        return (thisBuildType.initialRevenue / productionTime) * owned *Data.bonusPrestige;
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
        barCount = 0;
        prevNextMult = 0;
        nextMult = thisBuildType.nextMult[0];
        productionTime = thisBuildType.initialTime;
        cost = calculateCost();
        updateUI();
    }
}
