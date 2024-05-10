using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaveManager
{
    private static Metadata metadata;

    static SaveManager()
    {
        metadata = ReadMetadata();
    }

    public static bool SaveGame(int num, string name, SaveData sd)
    {
        if (FileManager.WriteToFile(num + ".dat", sd.ToJson()))
        {
            metadata.SaveSlot[num - 1].isUsed = true;
            metadata.SaveSlot[num - 1].SaveName = name;
            metadata.SaveSlot[num - 1].SaveTime = GetCurentDateTime();

            if (!WriteMetadata())
            {
                return false;
            }
            return true;
        }

        return false;
    }

    public static bool LoadGame(int num, out SaveData sd)
    {
        sd = new();
        if (!metadata.SaveSlot[num - 1].isUsed)
        {
            return false;
        }

        if (FileManager.LoadFromFile(num + ".dat", out var json))
        {
            sd.LoadFromJson(json);
            return true;
        }

        return false;
    }

    private static string GetCurentDateTime()
    {
        return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
    }

    private static Metadata InitializeEmptyMetadata()
    {
        Metadata newMetadata = new();

        for (int i = 0; i < 3; i++)
        {
            newMetadata.SaveSlot[i] = new();
            newMetadata.SaveSlot[i].isUsed = false;
            newMetadata.SaveSlot[i].SaveName = "";
            newMetadata.SaveSlot[i].SaveTime = "";
        }

        return newMetadata;
    }

    private static Metadata ReadMetadata()
    {
        Metadata newMetadata = InitializeEmptyMetadata();

        if (FileManager.LoadFromFile("metadata.dat", out var json))
        {
            newMetadata.LoadFromJson(json);
        }

        return newMetadata;
    }

    private static bool WriteMetadata()
    {
        if (FileManager.WriteToFile("metadata.dat", metadata.ToJson()))
        {
            return true;
        }

        return false;
    }

    public static Tuple<bool, string>[] GetSaveSlotList()
    {
        Tuple<bool, string>[] saveSlots = new Tuple<bool, string>[3];

        for (int i = 0; i < 3; i++)
        {
            bool isUsed = metadata.SaveSlot[i].isUsed;
            string description = metadata.SaveSlot[i].SaveTime + " " + metadata.SaveSlot[i].SaveName;

            saveSlots[i] = Tuple.Create(isUsed, description);
        }

        return saveSlots;
    }
}
