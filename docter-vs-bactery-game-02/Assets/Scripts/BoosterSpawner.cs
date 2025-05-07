using UnityEngine;

public class BoosterSpawner : MonoBehaviour
{
    public GameObject boosterPrefab;
    public float interval = 10f;

    [Header("Area Player 1")]
    public float BatasKiriP1;
    public float BatasKananP1;
    public float BatasAtasP1;
    public float BatasBawahP1;

    [Header("Area Player 2")]
    public float BatasKiriP2;
    public float BatasKananP2;
    public float BatasAtasP2;
    public float BatasBawahP2;

    void Start()
    {
        InvokeRepeating("SpawnBoosters", 5f, interval);
    }

    void SpawnBoosters()
    {
        if (boosterPrefab != null)
        {
            // Spawn untuk Player 1
            Vector2 pos1 = new Vector2(Random.Range(BatasKiriP1, BatasKananP1), Random.Range(BatasBawahP1, BatasAtasP1));
            Instantiate(boosterPrefab, pos1, Quaternion.identity);

            // Spawn untuk Player 2
            Vector2 pos2 = new Vector2(Random.Range(BatasKiriP2, BatasKananP2), Random.Range(BatasBawahP2, BatasAtasP2));
            Instantiate(boosterPrefab, pos2, Quaternion.identity);
        }
    }


}

