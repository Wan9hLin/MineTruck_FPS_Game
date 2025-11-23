using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MiningObjectiveUI : MonoBehaviour
{
    public TextMeshProUGUI oreNumText;

    private void Start()
    {
        oreNumText = GetComponentInChildren<TextMeshProUGUI>();
    }
    public void SetObjective(int oreNum, int maxOreNum)
    {
        oreNumText.text = oreNum.ToString() + " / " + maxOreNum.ToString();
    }
}
