using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.5f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _destroyAnimation;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private float _fireRate = 10f;
    [SerializeField]
    private int _lifes = 3;
    private float _nextFire = -1f;
    private SpawnManager _spawnManager;
    [SerializeField]
    private float _tripleshotTime = 5f;
    [SerializeField]
    private float _speedUpTime = 5f;
    [SerializeField]
    private bool _isTripleShotActive = false;
    [SerializeField]
    private bool _isShielded = false;
    [SerializeField]
    private int _playerScore = 0;
    [SerializeField]
    private UIManager _UImanager;
    [SerializeField]
    private int _damagedDrive;
    private AudioSource _audioSource;
    [SerializeField]
    private AudioClip _laserAudio;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0,0,0);
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        _audioSource = gameObject.GetComponent<AudioSource>();
        if (_spawnManager == null)
        {
            Debug.LogError("spawnManager is null");
        }
        if(_audioSource == null)
        {
            Debug.LogError("audio source null");
        }
        else
        {
            _audioSource.clip = _laserAudio;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        PlayerBounds();
        Fire();
    }

    void Fire() 
    {
        if(Input.GetKeyDown(KeyCode.Space) && Time.time > _nextFire) 
        {
            _nextFire = Time.time + _fireRate;
            Vector3 offset = new Vector3(0, 0.8f, 0);
            if (_isTripleShotActive)
            {
                Instantiate(_tripleShotPrefab, transform.position + offset, Quaternion.identity);
                return;
            }
            Instantiate(_laserPrefab, transform.position + offset, Quaternion.identity);
            _audioSource.Play();
        }
    }

    void Move() 
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput, verticalInput);
        transform.Translate(direction * _speed * Time.deltaTime);
    }

    void PlayerBounds() 
    {
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y,-3.8f, 0), 0);
        
        if(transform.position.x >= 11) {
            transform.position = new Vector3(-11, transform.position.y, 0);
        } else if(transform.position.x <= -11) {
            transform.position = new Vector3(11, transform.position.y, 0);
        }
    }

    public void Damage()
    { 
        if(_isShielded)
        {
            _isShielded = false;
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
            return;
        }
        
        _lifes -= 1;
        _UImanager.WatchLives();
        ShowDamage();

        if (_lifes <= 0)
        {
            _spawnManager.OnPlayerDeath();
            _UImanager.OnShowGameoverText();
            GameObject anim = Instantiate(_destroyAnimation, transform.position, Quaternion.identity);
            Destroy(gameObject);
            Destroy(anim, 2.5f);
        }
    }

    public void ActivateTripleShot()
    {
        this._isTripleShotActive = true;
        StartCoroutine(TripleShotRoutine());
    }

    private void ShowDamage()
    {
        int randomDrive = isDriverDamagedCheck();
        
        gameObject.transform.GetChild(randomDrive).gameObject.SetActive(true);
    }

    private int isDriverDamagedCheck()
    {
        if(_damagedDrive == 0)
        {
            _damagedDrive = UnityEngine.Random.Range(2, 4);
            return _damagedDrive;
        }
        if(_damagedDrive == 3)
        {
            return 2;
        }
        return 3;
    }

    IEnumerator TripleShotRoutine()
    {
        yield return new WaitForSeconds(_tripleshotTime);
        this._isTripleShotActive = false;
    }
    public void ActivateSpeedUp()
    {
        StartCoroutine(SpeedUpRoutine());
    }

    public void ActivateShield()
    {
        _isShielded = true;
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
    }


    IEnumerator SpeedUpRoutine()
    {
        _speed = _speed * 2;
        yield return new WaitForSeconds(_speedUpTime);
        _speed = _speed / 2;
    }

    public int GetPlayerScore()
    {
        return _playerScore;
    }

    public void IncresePlayerScore()
    {
        _playerScore += 10;
    }

    public int GetLifes()
    {
        return _lifes;
    }
} 
