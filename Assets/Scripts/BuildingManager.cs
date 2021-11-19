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
    private Image nextMultBar;

    [HideInInspector]
    public int owned, multiplier, nextMult;
    [HideInInspector]
    public float productionTime;
    [HideInInspector]
    public double cost;
    [HideInInspector]
    public bool reachMax;

    //Need for fillbar
    private int barCount, prevNextMult;

    private void Awake()
    {
        if (productionTime == 0)
        {
            initialReset();
        }
    }

    private void Start()
    {
        StartCoroutine(gainCurrency());
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
        else
            costUI.text = CashConverter.doubleToString(cost);

        ownedUI.text = owned.ToString();
        nextMultBar.fillAmount = (float)barCount/(nextMult-prevNextMult);
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
            multiplier++;
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
        barCount = 0;
        prevNextMult = 0;
        nextMult = thisBuildType.nextMult[0];
        productionTime = thisBuildType.initialTime;
        cost = calculateCost();
        updateUI();
    }
}
