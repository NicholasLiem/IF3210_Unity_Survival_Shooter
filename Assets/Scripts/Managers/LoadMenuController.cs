using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadMenuController : MonoBehaviour
{
    public Color disabledTextColor = Color.gray;

    public Button[] SaveButtons;
    public TMP_Text[] Texts;

    void OnEnable()
    {
        if (GameManager.Instance != null)
        {
            Tuple<bool, string>[] saveSlots = SaveManager.GetSaveSlotList();

            for (int i = 0; i < 3; i++)
            {
                if (SaveButtons[i] != null && saveSlots[i] != null)
                {
                    SaveButtons[i].interactable = saveSlots[i].Item1;
                    Texts[i].color = saveSlots[i].Item1 ? Texts[i].color : disabledTextColor;

                    SaveButtons[i].onClick.RemoveAllListeners();
                    if (saveSlots[i].Item1)
                    {
                        int num = i + 1;
                        Texts[i].text = saveSlots[i].Item2;
                        SaveButtons[i].onClick.AddListener(() => GameManager.Instance.LoadGame(num));
                    }
                }
            }
        }
    }
}
