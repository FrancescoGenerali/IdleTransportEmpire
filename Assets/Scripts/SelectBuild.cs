using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectBuild : MonoBehaviour
{
    [Tooltip("Select from the scriptable Objects")]
    public Buildings thisBuildType;

    [HideInInspector]
    public int owned, multiplier;

    private float productionTime;

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

    IEnumerator gainCurrency()
    {
        for(;;)
        {
            Data.currency += thisBuildType.initialProductivity * owned * multiplier;
            yield return new WaitForSeconds(productionTime);
        }
    }
}
