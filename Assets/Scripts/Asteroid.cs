using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float _speed = 25f;
    [SerializeField]
    private GameObject _destroyAnimation;
    private SpawnManager _spawnManager;
    private void Start()
    {
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    }
    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Rotate(Vector3.forward * _speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D obj)
    {
        if(obj.transform.tag == "laser")
        {
            Destroy(obj.gameObject);
            GameObject anim = Instantiate(_destroyAnimation, transform.position, Quaternion.identity);
            Destroy(gameObject, 0.3f);
            Destroy(anim, 2.5f);
            _spawnManager.StartSpawning();
            
        }
    } 
}
