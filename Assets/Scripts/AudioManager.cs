using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource _audioSource;
    [SerializeField]
    private AudioClip _powerUpAudio;
    // Start is called before the first frame update
    void Start()
    {
        _audioSource = gameObject.GetComponent<AudioSource>();
        if(_audioSource == null)
        {
            Debug.LogError("audio source null");
        }
    }

    // Update is called once per frame

    public void PlayPowerUp()
    {
        _audioSource.clip = _powerUpAudio;
        _audioSource.Play();
    }
}
