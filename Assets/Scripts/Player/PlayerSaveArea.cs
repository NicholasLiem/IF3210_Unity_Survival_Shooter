using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSaveArea : MonoBehaviour
{
    public GameObject messageHUD;
    public GameObject saveMenu;

    public Button[] saveButtons;

    private bool isPlayerInTrigger = false;
    private bool isPerformingAction = false;

    private void Start()
    {
        if (GameManager.Instance != null)
        {
            for (int i = 0; i < 3; i++)
            {
                if (saveButtons[i] != null)
                {
                    int saveSlot = i + 1;
                    saveButtons[i].onClick.RemoveAllListeners();
                    saveButtons[i].onClick.AddListener(() => GameManager.Instance.SaveGame(saveSlot));
                }
            }
        }
    }

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
            if (Input.GetKeyDown(KeyCode.Space) && !isPerformingAction)
            {
                PerformAction();
            }
            else if (Input.GetKeyDown(KeyCode.Escape) && isPerformingAction)
            {
                QuitPerformAction();
            }
        }
        else
        {
            QuitPerformAction();
        }
    }

    void ShowSaveMessage()
    {
        if (isPlayerInTrigger && !isPerformingAction) messageHUD.SetActive(true);
        else messageHUD.SetActive(false);
    }

    void PerformAction()
    {
        isPerformingAction = true;
        saveMenu.SetActive(true);
    }

    public void QuitPerformAction()
    {
        saveMenu.SetActive(false);
        isPerformingAction = false;
    }
}
