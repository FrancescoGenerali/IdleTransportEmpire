using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPS : MonoBehaviour
{
    private void Awake()
    {
        //QualitySettings.vSyncCount = 0; //VALUTARE POI
        Application.targetFrameRate = 60;   
    }
    void Update()
    {
        if (Application.targetFrameRate != 60)
            Application.targetFrameRate = 60;
    }
}
