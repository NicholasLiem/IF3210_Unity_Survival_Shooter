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
    public int petPrice = 0;

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
        int currentGold = ScoreManager.gold;
        if (currentGold < petPrice)
        {
            errorTextTimeShown = errorTextShowTime;
            errorText.SetActive(true);
            return;
        }

        ScoreManager.gold = currentGold - petPrice;

        GameObject instance = Instantiate(attackPetPrefab, player.transform.position, Quaternion.identity);

        AttackerMovement moveScript = instance.GetComponent<AttackerMovement>();

        int petCount = GameManager.Instance.GetPetAmount("attack");

        GameManager.Instance.petData.Add(new Tuple<string, int>("attack", petCount + 1));
    }

    public void BuyHealPet()
    {
        int currentGold = ScoreManager.gold;
        if (currentGold < petPrice)
        {
            errorTextTimeShown = errorTextShowTime;
            errorText.SetActive(true);
            return;
        }

        ScoreManager.gold = currentGold - petPrice;

        GameObject instance = Instantiate(healPetPrefab, player.transform.position, Quaternion.identity);

        HealerMovement moveScript = instance.GetComponent<HealerMovement>();

        int petCount = GameManager.Instance.GetPetAmount("heal");

        GameManager.Instance.petData.Add(new Tuple<string, int>("heal", petCount + 1));
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
