using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance { get; private set; }
    private List<GameObject> instantiatedPrefabs = new List<GameObject>();
    private Dictionary<string, Quest> questMap;
    private int currentPlayerLevel = 0;

    public delegate void QuestUpdateHandler(Quest quest);
    public event QuestUpdateHandler OnQuestUpdated;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        InitializeQuests();
    }

    public void InitializeQuests()
    {
        questMap = CreateQuestMap();
        foreach (Quest quest in questMap.Values)
        {
            NotifyQuestUpdate(quest);
        }
    }

    public void RestartQuest(int restartLevel)
    {
        ClearInstantiatedPrefabs();
        InitializeQuests();
        currentPlayerLevel = restartLevel;
    }

    private void ClearInstantiatedPrefabs()
    {
        foreach (GameObject prefab in instantiatedPrefabs)
        {
            Destroy(prefab);
        }
        instantiatedPrefabs.Clear();
    }

    public void RegisterPrefabInstance(GameObject prefabInstance)
    {
        instantiatedPrefabs.Add(prefabInstance);
    }

    private void OnEnable()
    {
        GameEventsManager.Instance.questEvents.onStartQuest += StartQuest;
        GameEventsManager.Instance.questEvents.onAdvanceQuest += AdvanceQuest;
        GameEventsManager.Instance.questEvents.onFinishQuest += FinishQuest;
        GameEventsManager.Instance.miscEvents.OnLevelAdvance += PlayerLevelChange;
        GameEventsManager.Instance.questEvents.onQuestStepStateChange += QuestStepStateChange;
    }

    private void OnDisable()
    {
        GameEventsManager.Instance.questEvents.onStartQuest -= StartQuest;
        GameEventsManager.Instance.questEvents.onAdvanceQuest -= AdvanceQuest;
        GameEventsManager.Instance.questEvents.onFinishQuest -= FinishQuest;
        GameEventsManager.Instance.miscEvents.OnLevelAdvance -= PlayerLevelChange;
        GameEventsManager.Instance.questEvents.onQuestStepStateChange -= QuestStepStateChange;
    }

    private void QuestStepStateChange(string id, int stepIndex, QuestStepState questStepState)
    {
        Quest quest = GetQuestById(id);
        quest.StoreQuestStepState(questStepState, stepIndex);
        ChangeQuestState(id, quest.state);
    }

    private void Start()
    {
        foreach (Quest quest in questMap.Values)
        {
            if (quest.state == QuestState.IN_PROGRESS)
            {
                quest.InstantiateCurrentQuestStep(this.transform);
            }
            GameEventsManager.Instance.questEvents.QuestStateChange(quest);
        }
    }

    private void PlayerLevelChange(int level)
    {
        currentPlayerLevel = level;
    }

    private bool CheckRequirementsMet(Quest quest)
    {
        bool meetsRequirements = true;
        if (currentPlayerLevel < quest.info.levelRequirement)
        {
            meetsRequirements = false;
        }

        foreach (QuestInfoSO prerequisiteQuestInfo in quest.info.questPrerequisites)
        {
            if (GetQuestById(prerequisiteQuestInfo.id).state != QuestState.FINISHED)
            {
                meetsRequirements = false;
            }
        }

        return meetsRequirements;        
    }

    private void Update()
    {
        foreach (Quest quest in questMap.Values)
        {
            if (quest.state == QuestState.REQUIREMENTS_NOT_MET && CheckRequirementsMet(quest))
            {
                ChangeQuestState(quest.info.id, QuestState.CAN_START);
            }

            if (quest.state == QuestState.CAN_START)
            {
                StartQuest(quest.info.id);
            }

            if (quest.state == QuestState.IN_PROGRESS)
            {
                NotifyQuestUpdate(quest);
            }
        }
    }

    private void ChangeQuestState(string id, QuestState state)
    {
        Quest quest = GetQuestById(id);
        quest.state = state;
        GameEventsManager.Instance.questEvents.QuestStateChange(quest);
        NotifyQuestUpdate(quest);
    }

    private void NotifyQuestUpdate(Quest quest)
    {
        OnQuestUpdated?.Invoke(quest);
    }

    private void StartQuest(string id)
    {
        Debug.Log("Start quest: " + id);
        Quest quest = GetQuestById(id);
        quest.InstantiateCurrentQuestStep(this.transform);
        ChangeQuestState(quest.info.id,  QuestState.IN_PROGRESS);
        NotifyQuestUpdate(quest);
    }

    private void AdvanceQuest(string id)
    {
        Debug.Log("Advance quest: " + id);
        Quest quest = GetQuestById(id);
        quest.MoveToNextStep();

        if (quest.CurrentStepExists())
        {
            quest.InstantiateCurrentQuestStep(this.transform);
        }
        else
        {
            ChangeQuestState(quest.info.id, QuestState.CAN_FINISH);
            FinishQuest(quest.info.id);
        }
    }

    private void FinishQuest(string id)
    {
        Debug.Log("Finish quest: " + id);
        Quest quest = GetQuestById(id);
        ClaimRewards(quest);
        ChangeQuestState(quest.info.id, QuestState.FINISHED);
    }

    private void ClaimRewards(Quest quest)
    {
        GameEventsManager.Instance.miscEvents.TriggerGoldCollected(quest.info.goldReward);
    }

    private Dictionary<string, Quest> CreateQuestMap()
    {
        QuestInfoSO[] allQuests = Resources.LoadAll<QuestInfoSO>("Quests");
        Dictionary<string, Quest> idToQuestMap = new Dictionary<string, Quest>();
        foreach (QuestInfoSO questInfo in allQuests)
        {
            if (idToQuestMap.ContainsKey(questInfo.id))
            {
                Debug.LogWarning("Duplicate ID found when creating quest map: " + questInfo.id);
            }
            idToQuestMap.Add(questInfo.id, LoadQuest(questInfo));
        }
        return idToQuestMap;
    }

    private Quest GetQuestById(string id)
    {
        Quest quest = questMap[id];
        if (quest == null)
        {
            Debug.LogError("ID not found in the Quest Map: " + id);
        }
        return quest;
    }

    // Pindahin ke JSON builder?
    private void OnApplicationQuit()
    {
        foreach (Quest quest in questMap.Values)
        {
            QuestData questData = quest.GetQuestData();
            Debug.Log(quest.info.id);
            Debug.Log("State: " + questData.state);
            foreach (QuestStepState stepState in questData.questStepStates)
            {
                Debug.Log("Step state: " + stepState.state);
            }
        }
    }

    private void SaveQuest(Quest quest)
    {
        try
        {
            QuestData questData = quest.GetQuestData();
            string serializedData = ""; // set To JSOn or something
        }
        catch (System.Exception e)
        {
            Debug.LogError("Fail to save quest");
        }
    }

    private Quest LoadQuest(QuestInfoSO questInfo)
    {
        Quest quest = null;
        try
        {
            // serializedData = ""; //Ambil dari file
            // QuestData questData = JsonUtility.FromJSON<questData>(serializedData);
            // quest = new Quest(questInfo, questData.state, questData.questStepIndex, questData.questStepStates);
            quest = new Quest(questInfo);
        }
        catch (System.Exception e)
        {
            Debug.LogError("Fail to load quest");
        }
        return quest;
    }

}
