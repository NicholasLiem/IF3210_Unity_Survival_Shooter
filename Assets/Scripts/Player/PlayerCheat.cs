using Nightmare;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class PlayerCheat : MonoBehaviour
{

    PlayerHealth playerHealth;
    PlayerMovement playerMovement;
    GameObject weaponGun;
    GameObject weaponShotgun;
    GameObject weaponSword;
    Gun gun;
    Shotgun[] shotgun  = new Shotgun[5];
    Sword sword;
    GameObject[] pet;
    GameObject[] enemyPet;

    string input;
    float powerToAdd = 0.10f;

    void Awake()
    {
        playerHealth = GetComponent<PlayerHealth>();
        playerMovement = GetComponent<PlayerMovement>();
        weaponGun = transform.Find("Gun").gameObject;
        weaponShotgun = transform.Find("Shotgun").gameObject;
        weaponSword = transform.Find("Sword").gameObject;
        weaponGun = weaponGun.transform.Find("GunBarrelEnd").gameObject;
        gun = weaponGun.GetComponent<Gun>();
        for (int i = 0; i < 5; i++)
        {
            string barrelName = i == 0 ? "ShotgunBarrelEnd" : $"ShotgunBarrelEnd ({i})";
            GameObject shotgunBarrelEnd = weaponShotgun.transform.Find(barrelName).gameObject;
            shotgun[i] = shotgunBarrelEnd.GetComponent<Shotgun>();
        }
        weaponSword = weaponSword.transform.Find("KatanaEnd").gameObject;
        sword = weaponSword.GetComponent<Sword>();
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
        Debug.Log(gun);
        gun.OneShotKillCheat();
        for (int i = 0; i < shotgun.Length; i++)
        {
            shotgun[i].OneShotKillCheat();
        }
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
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else if (input == "deactivecheat")
        {
            Debug.Log("Deactive Cheat");
        }
    }
}
