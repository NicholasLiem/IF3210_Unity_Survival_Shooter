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
                Debug.Log("This is shotgun");
                SwitchWeapon(shotgun);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
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
    }

}
