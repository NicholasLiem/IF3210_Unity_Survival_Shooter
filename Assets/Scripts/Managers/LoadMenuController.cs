using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadMenuController : MonoBehaviour
{
    public Color disabledTextColor = Color.gray;

    public Button save01;
    public TMP_Text buttonText01;

    public Button save02;
    public TMP_Text buttonText02;

    public Button save03;
    public TMP_Text buttonText03;

    void OnEnable()
    {
        if (GameManager.Instance != null)
        {
            save01.interactable = FileManager.IsFileExist("1.dat");
            buttonText01.color = save01.interactable ? buttonText01.color : disabledTextColor;

            save02.interactable = FileManager.IsFileExist("2.dat");
            buttonText02.color = save02.interactable ? buttonText02.color : disabledTextColor;

            save03.interactable = FileManager.IsFileExist("3.dat");
            buttonText03.color = save03.interactable ? buttonText03.color : disabledTextColor;
        }
    }
}
