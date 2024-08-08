using System;
using UnityEngine;
using UnityEditor;
using System.IO;
using Constants;

public class MonsterCSVtoSO
{
    private static string monsterCSVPath = "/Data/CSV/SampleMonster.csv";
    [MenuItem("Utilities/Generate Monsters")]
    public static void GenerateMonsters()
    {
        string[] allLines = File.ReadAllLines(Application.dataPath + monsterCSVPath);

        for (int i = 1; i < allLines.Length; i++)
        {
            string[] splitData = allLines[i].Split(',');

            MonsterSO monster = ScriptableObject.CreateInstance<MonsterSO>();
            monster.Name = splitData[0];
            monster.Grade = (MonsterGrade)Enum.Parse(typeof(MonsterGrade), splitData[1]);
            monster.Speed = float.Parse(splitData[2]);
            monster.Health = float.Parse(splitData[3]);
            
            AssetDatabase.CreateAsset(monster,$"Assets/Data/SO/Monster/{monster.Name}.asset");
        }
        
        AssetDatabase.SaveAssets();
    }
}
