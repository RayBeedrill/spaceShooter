using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemy;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject[] powerUpList;
    [SerializeField]
    private GameObject _powerUpContainer;
    private bool _isPlayerDead = false;

    // Start is called before the first frame update

    public void StartSpawning() 
    {
        StartCoroutine(SpawnRoutine());
        StartCoroutine(SpwanPowerUp());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator SpawnRoutine() 
    { 
        yield return new WaitForSeconds(3f);
        while(!_isPlayerDead)
        {
            Vector3 offset = new Vector3(Random.Range(9f, -9f), 10, 0);
            GameObject newEnemy = Instantiate(_enemy, transform.position + offset, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform; 
            yield return new WaitForSeconds(5);
        }
    
    }

    IEnumerator SpwanPowerUp()
    {
        yield return new WaitForSeconds(3f);
        while (!_isPlayerDead)
        {
            yield return new WaitForSeconds(5);
            Vector3 offset = new Vector3(Random.Range(9f, -9f), 10, 0);
            int randomIndex = Random.Range(0, powerUpList.Length);
            //int randomIndex = 2;
            GameObject newPowerUp = Instantiate(powerUpList[randomIndex], transform.position + offset, Quaternion.identity);
            newPowerUp.transform.parent = _powerUpContainer.transform;
        }
    }

    public void OnPlayerDeath()
    {
        _isPlayerDead = true;
    }
}
