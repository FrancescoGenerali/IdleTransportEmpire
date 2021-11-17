
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectBuild : MonoBehaviour
{
    [Tooltip("Select from the scriptable Objects")]
    public Buildings thisBuildType;

    private float productionTime;

    private void Awake()
    {
        productionTime = thisBuildType.initialTime;
    }

    private void Start()
    {
        StartCoroutine(gainCurrency());
    }

    IEnumerator gainCurrency()
    {
        for(;;)
        {
            Data.currency += thisBuildType.initialProductivity;
            yield return new WaitForSeconds(productionTime);
        }
    }
}
