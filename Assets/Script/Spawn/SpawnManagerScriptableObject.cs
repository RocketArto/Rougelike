using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SpawnManagerScriptableObject", order = 1)]
public class SpawnManagerScriptableObject : ScriptableObject
{
    
    public int spawnIndex;
    public GameObject[] spawnList;
}

[System.Serializable]
public struct EnemyData
{
    public GameObject enemyPrefab;
}
