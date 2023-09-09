using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RemotePlayer : MonoBehaviour
{
    public int serverTickRate = 10;
    [SerializeField] private int money;
    [SerializeField] private int populationGrowth;
    public int[] TilesOwned;

    private void Update()
    {
    }

    public void SetRole(string role)
    {
        nameText.role = role;
    }
    public void SetServerTickRate(int tickRate)
    {
        serverTickRate = tickRate;
    }
}