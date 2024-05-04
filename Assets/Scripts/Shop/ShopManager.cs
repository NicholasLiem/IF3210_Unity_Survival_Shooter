using Nightmare;
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

        moveScript.masterObject = player;
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

        moveScript.masterObject = player;
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
