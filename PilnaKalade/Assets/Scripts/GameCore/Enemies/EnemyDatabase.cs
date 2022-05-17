using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyDatabase
{
    private static List<EnemyData> enemies;
    private const string ENEMY_DATA_FILE = "Enemies.json";

    //TODO: FIX ENCAPSULATION
    public static EnemyData GetEnemyData(int index)
    {
        return enemies[index];
    }

    public static EnemyData getRandomEnemyData()
    {
        return enemies[UnityEngine.Random.Range(0, enemies.Count - 1)];
    }

    public static int GetCount()
    {
        return enemies.Count;
    }

    public static void LoadEnemies()
    {
        string jsonName = ENEMY_DATA_FILE.Split('.')[0];
        TextAsset jsonFile = Resources.Load<TextAsset>(jsonName);
        // Making json object out of array
        var json = jsonFile.text;
        json = "{\"enemies\":" + json + "}";
        Root jsonObject = JsonUtility.FromJson<Root>(json);
        enemies = jsonObject.enemies;
    }

    [Serializable]
    private class Root
    {
        public List<EnemyData> enemies;
    }
}
