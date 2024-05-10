using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SaveDisplay : MonoBehaviour
{
    public Button[] saveButtons;
    public TMP_Text[] Texts;
    public TMP_InputField SaveNameInput;

    public int currentSlot = -1;

    private void OnEnable()
    {
        if (GameManager.Instance != null)
        {
            UpdateUI();
        }
    }

    private void UpdateUI()
    {
        Tuple<bool, string>[] saveSlots = SaveManager.GetSaveSlotList();

        for (int i = 0; i < 3; i++)
        {
            if (saveButtons[i] != null && saveSlots[i] != null)
            {
                int saveSlot = i + 1;
                saveButtons[i].onClick.RemoveAllListeners();
                saveButtons[i].onClick.AddListener(() => currentSlot = saveSlot); ;

                if (saveSlots[i].Item1)
                {
                    Texts[i].text = saveSlots[i].Item2;
                }
            }
        }
    }

    public void Save()
    {
        GameManager.Instance.SaveGame(currentSlot, SaveNameInput.text);
        UpdateUI();
        SaveNameInput.text = "";
    }
}
