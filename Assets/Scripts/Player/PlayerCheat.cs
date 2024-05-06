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

    string input;

    void Awake()
    {
        playerHealth = GetComponent<PlayerHealth>();
        playerMovement = GetComponent<PlayerMovement>();
        gun = GetComponent<Gun>(); 
        shotgun = GetComponent<Shotgun>(); 
        sword = GetComponent<Sword>();
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
        }
        else if (input == "twotimespeed")
        {
            Debug.Log("Two Time Speed");
            playerMovement.TwoTimeSpeed();
        }
        else if (input == "fullhppet")
        {
            Debug.Log("Full HP Pet");
        }
        else if (input == "killpet")
        {
            Debug.Log("Kill Pet");
        }
        else if (input == "orb")
        {
            Debug.Log("Orb");
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
