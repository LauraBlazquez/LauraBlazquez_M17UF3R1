using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class SaveSystem
{
    private static string savePath => Path.Combine(Application.persistentDataPath, "save.json");

    public static void SavePlayer(Player player, WeaponManager weaponManager)
    {
        SaveData data = new SaveData
        {
            position = new float[] {
                player.transform.position.x,
                player.transform.position.y,
                player.transform.position.z
            },
            rotation = new float[] {
                player.transform.eulerAngles.x,
                player.transform.eulerAngles.y,
                player.transform.eulerAngles.z
            },
            unlockedWeapons = new List<string>()
        };

        foreach (var weapon in weaponManager.unlockedWeapons)
        {
            data.unlockedWeapons.Add(weapon.name);
        }

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(savePath, json);
        Debug.Log("Guardado en: " + savePath);
    }

    public static SaveData Load()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            return JsonUtility.FromJson<SaveData>(json);
        }
        else
        {
            Debug.LogWarning("No se encontró archivo de guardado.");
            return null;
        }
    }
}
