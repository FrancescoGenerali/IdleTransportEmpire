using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Buildings : ScriptableObject
{
    public string buildType;
    [Tooltip("Cost of the first investment.")]
    public int initialCost;
    [Tooltip("Multiplier for each additional investment in this building.")]
    public float coefficient;
    [Tooltip("Amount of seconds to produce something. Without bonus.")]
    public float initialTime;
    [Tooltip("Amount of product at the end of the cycle of production.")]
    public float initialRevenue;
    [Tooltip("Amount of currency the business makes per second. Without bonus")]
    public float initialProductivity;
}
