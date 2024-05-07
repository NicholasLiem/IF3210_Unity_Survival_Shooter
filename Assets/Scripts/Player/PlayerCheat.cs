using Nightmare;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheat : MonoBehaviour
{

    PlayerHealth playerHealth;
    PlayerMovement playerMovement;
    Gun gun;
    Shotgun shotgun;
    Sword sword;
    GameObject[] pet;
    GameObject[] enemyPet;

    string input;
    float powerToAdd = 0.10f;

    void Awake()
    {
        playerHealth = GetComponent<PlayerHealth>();
        playerMovement = GetComponent<PlayerMovement>();
        gun = GetComponent<Gun>(); 
        shotgun = GetComponent<Shotgun>(); 
        sword = GetComponent<Sword>();
        pet = GameObject.FindGameObjectsWithTag("Pet");
        enemyPet = GameObject.FindGameObjectsWithTag("EnemyPet");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReadInputString(string s)
    {
        input = s;
        Cheat();
        input = "";
    }

    void OneHitKill()
    {
        gun.OneShotKillCheat();
        shotgun.OneShotKillCheat();
        sword.OneHitKillCheat();
    }

    void FullHpPet()
    {
        for (int i=0; i < pet.Length; i++) { 
            PetHealth petHealth = pet[i].GetComponent<PetHealth>();
            petHealth.enableGodMode();
        }
    }

    void KillPet()
    {
        for (int i = 0; i < enemyPet.Length; i++)
        {
            PetHealth enemyPetHealth = enemyPet[i].GetComponent<PetHealth>();
            enemyPetHealth.instantKillPet();
        }
    }

    void Cheat()
    {
        if (input == "nodamage")
        {
            Debug.Log("No Damage");
            playerHealth.NoDamageCheat();
        }
        else if (input == "onehitkill")
        {
            Debug.Log("One Hit Kill");
            OneHitKill();
            
        }
        else if (input == "motherlode")
        {
            Debug.Log("Motherlode");
            ScoreManager.MotherlodeCheat();
        }
        else if (input == "twotimespeed")
        {
            Debug.Log("Two Time Speed");
            playerMovement.TwoTimeSpeed();
        }
        else if (input == "fullhppet")
        {
            Debug.Log("Full HP Pet");
            FullHpPet();
        }
        else if (input == "killpet")
        {
            Debug.Log("Kill Pet");
            KillPet();
        }
        else if (input == "orb")
        {
            Debug.Log("Orb");
            playerHealth.PowerUp(powerToAdd);
        }
        else if (input == "skip")
        {
            Debug.Log("Skip");
        }
        else if (input == "deactivecheat")
        {
            Debug.Log("Deactive Cheat");
        }
    }
}
