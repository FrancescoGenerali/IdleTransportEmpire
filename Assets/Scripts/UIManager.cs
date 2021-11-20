using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI money;
    public TextMeshProUGUI prestige;

    void Update()
    {
        money.text = CashConverter.doubleToString(Data.currency);
        prestige.text = CashConverter.doubleToString(Data.prestige);
    }
}
