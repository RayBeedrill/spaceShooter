using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private float _speed = 8f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();    
    }

    void Move() 
    {
        Vector3 direction = new Vector3(0, 1, 0);
        transform.Translate(direction * _speed * Time.deltaTime);
        if(transform.position.y >= 20) {
            if(transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            } 
            Destroy(gameObject);
        }
    }
}


