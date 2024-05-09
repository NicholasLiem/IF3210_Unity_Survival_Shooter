using UnityEngine;
using UnityEngine.UI;

public class QuestDisplay : MonoBehaviour
{
    public Text questText;

    private QuestManager questManager;

    void Start()
    {
        questManager = FindObjectOfType<QuestManager>();
        questManager.OnQuestUpdated += UpdateUI;
    }

    private void OnDestroy()
    {
        if (questManager != null)
            questManager.OnQuestUpdated -= UpdateUI;
    }

    void UpdateUI(Quest quest)
    {
        if (quest != null && quest.state == QuestState.IN_PROGRESS)
        {
            questText.text = $"Quest: {quest.info.displayName}\n" +
                             $"Description: {quest.info.description}\n" +
                             $"Gold Reward: {quest.info.goldReward}";
        }
        else
        {
            questText.text = "No active quest";
        }
    }
}
