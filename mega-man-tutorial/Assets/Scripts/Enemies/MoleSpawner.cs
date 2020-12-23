using System.Collections;
using UnityEngine;

public class MoleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private Vector2 interval = new Vector2(0.25f, 0.75f);

    public void StartSpawn()
    {
        if (!prefab || spawnPoints.Length == 0) return;

        StartCoroutine(Spawning());
    }

    public void FinishSpawn()
    {
        StopAllCoroutines();
    }

    private IEnumerator Spawning()
    {
        while(true)
        {
            int index = Random.Range(0, spawnPoints.Length);
            GameObject mole = Instantiate(prefab, spawnPoints[index].position, spawnPoints[index].rotation);
            Destroy(mole, 15f);
            yield return new WaitForSeconds(Random.Range(interval.x, interval.y));
        }
    }
}
