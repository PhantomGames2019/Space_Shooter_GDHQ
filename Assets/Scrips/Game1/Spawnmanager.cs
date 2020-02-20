using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnmanager : MonoBehaviour
{
    [SerializeField]
    private GameObject Enemy;
    [SerializeField]
    private float SpawnTime;
    [SerializeField]
    private GameObject _EnemyHolder;
    [SerializeField]
    private GameObject[] PowerUp;

    private bool _StopSpawning = false;

    public void StartSpawing()
    {
        StartCoroutine("SpawnEnemyRoutine");
        StartCoroutine("SpawnPowerUpRoutine");
    }
    

    IEnumerator SpawnEnemyRoutine()
    {
        

        while (_StopSpawning == false)
        {
            Vector3 PosToSpawn = new Vector3(Random.Range(-9, 9), 11, 0);
            GameObject _NewEnemy = Instantiate(Enemy, PosToSpawn, Quaternion.identity);
            _NewEnemy.transform.parent = _EnemyHolder.transform;
            yield return new WaitForSeconds(SpawnTime);

        }
    }

    IEnumerator SpawnPowerUpRoutine()
    {

        while (_StopSpawning == false)
        {
            yield return new WaitForSeconds(Random.Range(5, 10));
            Vector3 PowerUpPos = new Vector3 (Random.Range(-9, 9), transform.position.y, 0);
            Instantiate(PowerUp[Random.Range(0,3)], PowerUpPos, Quaternion.identity);
        }
    }
    
    public void OnPlayerdeath()
    {
        _StopSpawning = true;
    }

}
