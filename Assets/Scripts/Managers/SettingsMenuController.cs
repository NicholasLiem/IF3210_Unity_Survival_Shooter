using TMPro;
using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    public TMP_InputField usernameInputField;
    public TMP_Dropdown gameDifficultyDropdown;

    void Start()
    {
        if (GameManagement.Instance != null)
        {
            usernameInputField.text = GameManagement.Instance.Username;
        }
    }

    public void UpdateUsername()
    {
        if (GameManagement.Instance != null)
        {
            GameManagement.Instance.Username = usernameInputField.text;
        }
    }

    public void UpdateGameDifficulty()
    {
        if (GameManagement.Instance != null)
        {
            GameManagement.Instance.gameDifficulty = (GameDifficulty) gameDifficultyDropdown.value;
        }
    }
}
