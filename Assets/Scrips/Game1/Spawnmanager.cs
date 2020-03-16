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

    [SerializeField]
    private GameObject _MissilePowerUp;
    
    private bool _StopSpawning = false;

    private bool _MissileCanSpawn = true;


    public void StartSpawing()
    {
        StartCoroutine("SpawnEnemyRoutine");
        StartCoroutine("SpawnPowerUpRoutine");
        StartCoroutine("NukeSpawner");
    }

    private void Update()
    {
        
        

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
            Instantiate(PowerUp[Random.Range(0,PowerUp.Length)], PowerUpPos, Quaternion.identity);
        }
    }
   
    IEnumerator NukeSpawner()
    {
        if (_MissileCanSpawn == true)
        {
            yield return new WaitForSeconds(Random.Range(15,30));
            Vector3 PowerUpPos = new Vector3(Random.Range(-9, 9), transform.position.y, 0);
            Instantiate(_MissilePowerUp, PowerUpPos, Quaternion.identity);
            _MissileCanSpawn = false;
            StopCoroutine("NukeSpawner");
        }
    }

    public void OnPlayerdeath()
    {
        _StopSpawning = true;
    }

   public void StartMissileSpawn()
   {
        StartCoroutine("NukeSpawner");
        _MissileCanSpawn = true;
   }
   
    
   


}
