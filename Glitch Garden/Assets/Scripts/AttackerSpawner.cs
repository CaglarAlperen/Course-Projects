using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackerSpawner : MonoBehaviour
{

    bool spawn = true;
    [SerializeField] float minSpawnDelay = 1f;
    [SerializeField] float maxSpawnDelay = 5f;
    [SerializeField] Attacker[] attackerPrefabs;

    IEnumerator Start()
    {
        while (spawn)
        {
            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
            SpawnAttacker();
        }
    }

    public void StopSpawning()
    {
        spawn = false;
    }

    private void SpawnAttacker()
    {
        Attacker attacker = attackerPrefabs[Random.Range(0, attackerPrefabs.Length)];
        Spawn(attacker);
    }

    private void Spawn(Attacker attackerPrefab)
    {
        Attacker new_attacker = Instantiate(attackerPrefab, transform.position, transform.rotation) as Attacker;
        new_attacker.transform.parent = transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
