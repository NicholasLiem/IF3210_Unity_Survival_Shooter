using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAdvanceArea : MonoBehaviour
{
    public GameObject messageHUD;

    private bool isPlayerInTrigger = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = false;
        }
    }

    void Update()
    {
        ShowSaveMessage();

        if (isPlayerInTrigger)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                PerformAction();
            }
        }
    }

    void ShowSaveMessage()
    {
        if (isPlayerInTrigger) messageHUD.SetActive(true);
        else messageHUD.SetActive(false);
    }

    void PerformAction()
    {
        GameManager.Instance.AdvanceLevel();
    }
}
