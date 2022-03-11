using System.Collections.Generic;
using UnityEngine;

public class LevelLayoutGenerator : MonoBehaviour
{
    [SerializeField] private LevelChunkData levelChunkData;
                     
    [SerializeField] private Vector3 spawnOrigin;
    [SerializeField] private int chunksToSpawn = 10;

    private Vector3 spawnPosition;
    private LevelChunkData previousChunk;

    private void OnEnable()
    {
        TriggerExit.OnChunkExited += PickAndSpawnChunk;
    }

    private void OnDisable()
    {
        TriggerExit.OnChunkExited -= PickAndSpawnChunk;
    }

    private void Start()
    {
        previousChunk = levelChunkData;

        for (int i = 0; i < chunksToSpawn; i++)
        {
            PickAndSpawnChunk();
        }
    }

    private void PickAndSpawnChunk()
    {
        LevelChunkData chunkToSpawn = PickNextChunk();

        GameObject objectFromChunk = chunkToSpawn.levelChunks[Random.Range(0, chunkToSpawn.levelChunks.Length)];
        previousChunk = chunkToSpawn;
        Instantiate(objectFromChunk, spawnPosition + spawnOrigin, Quaternion.identity);

    }

    private LevelChunkData PickNextChunk()
    {
        spawnPosition += new Vector3(0f, 0, previousChunk.chunkSize.y);
        return levelChunkData;
    }

    public void UpdateSpawnOrigin(Vector3 originDelta)
    {
        spawnOrigin = spawnOrigin + originDelta;
    }
}
