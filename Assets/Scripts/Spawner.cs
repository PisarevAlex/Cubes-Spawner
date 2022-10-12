using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject spawningPrefab;

    [field: Header("Parameters")]
    [field: SerializeField] public ParameterField Distance { get; private set; }
    [field: SerializeField] public ParameterField Speed { get; private set; }

    [SerializeField] private ParameterField spawnInterval;

    private float spawnIntervalCounter;

    private void OnEnable()
    {
        StartCoroutine(SpawnIntervalRoutine());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    public void SpawnObject()
    {
        GameObject instance = Instantiate(spawningPrefab);
        instance.GetComponent<SpawnBody>().OnInstantiate(this);
    }

    IEnumerator SpawnIntervalRoutine()
    {
        while (true)
        {
            while (spawnIntervalCounter < spawnInterval.Value)
            {
                spawnIntervalCounter += Time.deltaTime;
                yield return 0;
            }

            SpawnObject();
            spawnIntervalCounter = 0f;
            yield return 0;
        }
    }
}
