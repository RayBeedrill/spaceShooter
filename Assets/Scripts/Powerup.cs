using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5f;
    [SerializeField]
    private int _powerupId;
    [SerializeField]
    private AudioManager _audioManager;
    // Start is called before the first frame update
    void Start()
    {
        _audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
       transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if(transform.position.y == -10f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "player")
        {
            _audioManager.PlayPowerUp();
            switch (_powerupId)
            {

                case 0:
                    collision.GetComponent<Player>().ActivateTripleShot();
                    break;
                case 1:
                    collision.GetComponent<Player>().ActivateSpeedUp();
                    break;
                case 2:
                    collision.GetComponent<Player>().ActivateShield();
                    break;

            }
            
            Destroy(gameObject);
        }
    }
}
