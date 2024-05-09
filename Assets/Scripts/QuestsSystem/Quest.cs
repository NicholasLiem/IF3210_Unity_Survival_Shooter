using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Quest
{
    public QuestInfoSO info;
    public QuestState state;
    private int currentQuestStepIndex;

    public Quest(QuestInfoSO questInfo)
    {
        this.info = questInfo;
        this.state = QuestState.REQUIREMENTS_NOT_MET;
        this.currentQuestStepIndex = 0;
    }

    public void MoveToNextStep()
    {
        currentQuestStepIndex++;
        GameManager.Instance.questProgress++;
    }

    public bool CurrentStepExists()
    {
        return (currentQuestStepIndex < info.questStepPrefabs.Length);
    }

    public void InstantiateCurrentQuestStep(Transform parentTransform)
    {
        GameObject questStepPrefab = GetCurrentQuestStepPrefab();
        if (questStepPrefab != null)
        {
            GameObject instantiatedPrefab = Object.Instantiate(questStepPrefab, parentTransform);
            Debug.Log("Prefab instantiated successfully: " + instantiatedPrefab.name);

            QuestStep questStep = instantiatedPrefab.GetComponent<QuestStep>();
            if (questStep != null)
            {
                questStep.InitializeQuestStep(info.id);
            }
            else
            {
                Debug.LogError("QuestStep component not found on the instantiated prefab for quest ID: " + info.id);
            }
        }
        else
        {
            Debug.LogError("Failed to load quest step prefab for quest ID: " + info.id);
        }
    }


    private GameObject GetCurrentQuestStepPrefab()
    {
        GameObject questStepPrefab = null;
        if (CurrentStepExists())
        {
            questStepPrefab = info.questStepPrefabs[currentQuestStepIndex];
        }
        else 
        {
            Debug.LogWarning("Tried to get quest step prefab, but stepIndex was out of range indicating that "
                + "there's no current step: QuestId=" + info.id + ", stepIndex=" + currentQuestStepIndex);
        }
        return questStepPrefab;
    }
}
