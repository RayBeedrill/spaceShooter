using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    private float _speed = 4f;
    private Player _player;
    private Animator _anim;
    private AudioSource _audioSource;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _audioSource = gameObject.GetComponent<AudioSource>();
        _anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y <= -10)
        {
            GameObject player = GameObject.Find("Player");
            if (player == null)
            {
                Destroy(gameObject);
            }
            RepawnEnemy();
        }
    }
    
    void RepawnEnemy()
    {
        transform.position = new Vector3(Random.Range(9f, -9f), 10, 0);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.transform.tag == "laser")
        {
            _speed = 0f;
            _anim.SetTrigger("OnEnemyDeath");
            Destroy(other.gameObject);
            _player.IncresePlayerScore();
            _audioSource.Play();
            Destroy(gameObject, 2.5f);
        }
        if (other.transform.tag == "player")
        {
            other.GetComponent<Player>().Damage();
            _anim.SetTrigger("OnEnemyDeath");
            _audioSource.Play();
            Destroy(gameObject, 2f);
        }
        
    }
}
