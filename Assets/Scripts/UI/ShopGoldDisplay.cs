using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopGoldDisplay : MonoBehaviour
{
    Text goldTextObject;
    void Awake()
    {
        goldTextObject = GetComponent<Text>();
    }

    private void Update()
    {
        goldTextObject.text = "Gold: " + GameManager.Instance.PlayerStats.GoldCollected;
    }
}
