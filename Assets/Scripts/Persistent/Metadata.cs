using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Metadata
{
    [System.Serializable]
    public struct MetadataItem
    {
        public bool isUsed;
        public string SaveName;
        public string SaveTime;
    }

    public MetadataItem[] SaveSlot = new MetadataItem[3];

    public string ToJson()
    {
        return JsonUtility.ToJson(this);
    }

    public void LoadFromJson(string a_Json)
    {
        JsonUtility.FromJsonOverwrite(a_Json, this);
    }
}
