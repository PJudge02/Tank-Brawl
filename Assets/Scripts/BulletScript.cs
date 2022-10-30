using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class BulletScript : MonoBehaviour
{
    Rigidbody2D _rbody;
    public float _SPEED;
    public float _pelletTimeLife;
    float startTime;
    public MSMScript _managerScript;
    public AudioClip _bulletHittingWall;
    AudioSource _audioSource;
    char _player;
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        _managerScript = FindObjectOfType<MSMScript>();
        _rbody = GetComponent<Rigidbody2D>();
        _audioSource = gameObject.AddComponent<AudioSource>();

        _rbody.velocity = transform.up * _SPEED;
    }
    public void setPlayer(char player)
    {
        _player = player;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - startTime >= _pelletTimeLife)
        {
            _managerScript.tankReload(_player);
            Destroy(gameObject);
        }
        

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals(TagList.wall))
        {
            _audioSource.PlayOneShot(_bulletHittingWall);
        }
        else if (collision.gameObject.tag.Equals(TagList.playerTagA))
        {
            _managerScript.tankReload(_player);
            _managerScript.tankHit('A');
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag.Equals(TagList.playerTagB))
        {
            _managerScript.tankReload(_player);
            _managerScript.tankHit('B');
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag.Equals(TagList.bullet))
        {
            _managerScript.tankReload(_player);
            _managerScript.tankHit('!');
            Destroy(gameObject);
        }
        
    }
}
