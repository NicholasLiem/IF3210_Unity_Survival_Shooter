using TMPro;
using UnityEngine;

public class SettingsMenuController : MonoBehaviour
{
    public TMP_InputField usernameInputField;
    public TMP_Dropdown gameDifficultyDropdown;

    void OnEnable()
    {
        if (GameManager.Instance != null)
        {
            usernameInputField.text = GameManager.Instance.Username;
            gameDifficultyDropdown.value = (int) GameManager.Instance.gameDifficulty;
        }
    }

    public void UpdateUsername()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.Username = usernameInputField.text;
        }
    }

    public void UpdateGameDifficulty()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.gameDifficulty = (GameDifficulty) gameDifficultyDropdown.value;
        }
    }
}
