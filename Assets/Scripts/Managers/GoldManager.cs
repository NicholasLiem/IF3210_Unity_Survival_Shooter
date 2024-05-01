using Nightmare;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldManager : MonoBehaviour
{
    Text goldText;

    void Awake()
    {
        goldText = GetComponent<Text>();
    }


    void Update()
    {
        goldText.text = "Gold: " + ScoreManager.gold;
    }
}
