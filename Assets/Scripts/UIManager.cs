using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI money;
    public TextMeshProUGUI prestige;
    public TextMeshProUGUI prestigeMultiplier;

    void Update()
    {
        money.text = CashConverter.doubleToString(Data.currency);
        prestige.text = Data.prestige.ToString();
        prestigeMultiplier.text = "x" + Data.bonusPrestige;
    }
}
