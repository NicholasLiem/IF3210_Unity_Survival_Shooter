using TMPro;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    public TMP_InputField usernameInputField;
    public AudioMixer audioMixer;
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

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("masterVol", volume);
    }

    public void UpdateGameDifficulty()
    {
        if (GameManagement.Instance != null)
        {
            GameManagement.Instance.gameDifficulty = (GameDifficulty) gameDifficultyDropdown.value;
        }
    }
}
