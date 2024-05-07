using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nightmare
{
    public class PlayerShooting : MonoBehaviour
    {

        GameObject gun;
        GameObject shotgun;
        GameObject sword;
        public GameObject currentWeapon;

        void Awake()
        {
            gun = transform.Find("Gun").gameObject;
            shotgun = transform.Find("Shotgun").gameObject;
            sword = transform.Find("Sword").gameObject;
            // Gun gunScript = gun.GetComponentInChildren<Gun>();
        }

        // Start is called before the first frame update
        void Start()
        {
            SwitchWeapon(gun);
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Debug.Log("This is gun");
                SwitchWeapon(gun);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                Shotgun shotgunScript = shotgun.GetComponentInChildren<Shotgun>();
                shotgunScript.heldByPlayer = true;
                Debug.Log("This is shotgun");
                SwitchWeapon(shotgun);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                Sword swordScript = sword.GetComponentInChildren<Sword>();
                swordScript.heldByPlayer = true;
                Debug.Log("This is sword");
                SwitchWeapon(sword);
            }
        }

        void SwitchWeapon(GameObject newWeapon)
        {

            if (newWeapon == null)
            {
                Debug.LogError("newWeapon is null");
                return;
            }

            // Disable the current weapon
            if (currentWeapon != null)
            {
                currentWeapon.SetActive(false);
            }

            // Enable the new weapon
            newWeapon.SetActive(true);

            // Update the current weapon
            currentWeapon = newWeapon;
        }

        public void disable()
        {
            gun.SetActive(false);
            shotgun.SetActive(false);
            sword.SetActive(false);
        }

        public void setMultiplier(float multiplier)
        {
            Gun gunScript = gun.GetComponentInChildren<Gun>();
            Shotgun shotgunScript = shotgun.GetComponentInChildren<Shotgun>();
            Sword swordScript = sword.GetComponentInChildren<Sword>();
            if (gunScript != null)
            {
                gunScript.multiplier = multiplier;
            }
            else
            {
                Debug.Log("Gun script is null");

            }
            if (shotgunScript != null)
            {
                shotgunScript.multiplier = multiplier;
            }
            else
            {
                Debug.Log("Shotgun script is null");
            }
            if (swordScript != null)
            {
                swordScript.multiplier = multiplier;
            }
            else
            {
                Debug.Log("Sword script is null");
            }

        }
    }

}
