using Nightmare;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public Canvas shopCanvas;
    public GameObject panel;
    public GameObject attackPetPrefab;
    public GameObject healPetPrefab;
    public GameObject errorText;
    public float errorTextShowTime = 2f;
    public int petPrice = 50;

    public List<Tuple<string, int>> petData = new List<Tuple<string, int>>();

    float errorTextTimeShown = 0f;
    GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void CloseShopUI()
    {
        panel.SetActive(false);
    }

    public void BuyAttackPet()
    {
        int currentGold = GameManager.Instance.gold;
        if (currentGold < petPrice && !ScoreManager.motherlodeCheat)
        {
            errorTextTimeShown = errorTextShowTime;
            errorText.SetActive(true);
            return;
        }

        if (!ScoreManager.motherlodeCheat)
        {
            GameManager.Instance.gold = currentGold - petPrice;
        }

        Instantiate(attackPetPrefab, player.transform.position, Quaternion.identity);

        GameManager.Instance.AddOrUpdatePet("attack");
    }

    public void BuyHealPet()
    {
        int currentGold = GameManager.Instance.gold;
        if (currentGold < petPrice && !ScoreManager.motherlodeCheat)
        {
            errorTextTimeShown = errorTextShowTime;
            errorText.SetActive(true);
            return;
        }

        if (!ScoreManager.motherlodeCheat)
        {
            GameManager.Instance.gold = currentGold - petPrice;
        }

        Instantiate(healPetPrefab, player.transform.position, Quaternion.identity);

        GameManager.Instance.AddOrUpdatePet("heal");
    }

    private void Update()
    {
        if (errorText != null)
        {
            if (errorTextTimeShown > 0)
            {
                errorTextTimeShown -= Time.deltaTime;
            }
            else
            {
                errorText.SetActive(false);
            }
        }   
    }
}
