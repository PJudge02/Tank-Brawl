using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(AudioSource))]
public class TurretScript : MonoBehaviour
{
    Rigidbody2D _rbody;
    public float _ROTATION;
    public MSMScript _managerScript;
    public char _TANKPLAYER;
    public GameObject _tankBody;

    // Quaternion _centerDir;
    bool _centerTurretA;
    bool _centerTurretB;

    // for audio
    //public AudioClip _turretRotation;
    //public AudioClip _tankFiring; 
    //AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        _rbody = GetComponent<Rigidbody2D>();
        _centerTurretB = false;

    }

    // Update is called once per frame
    void Update()
    {
        RotateTurret();
        FireTurret();
    }

    void RotateTurret()
    {
        if ((Input.GetKey(KeyCode.Q) && _TANKPLAYER == 'B' && !_centerTurretB))
        {
            Quaternion direction = transform.rotation * Quaternion.Euler(0, 0, _ROTATION);
            _rbody.MoveRotation(direction);
        }
        else if ((Input.GetKey(KeyCode.W) && _TANKPLAYER == 'B' && !_centerTurretB))
        {
            Quaternion direction = transform.rotation * Quaternion.Euler(0, 0, -1 * _ROTATION);
            _rbody.MoveRotation(direction);
        }
        else if (!_centerTurretA && _TANKPLAYER == 'A')
        {            
            Vector3 turretPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 turretDirection = new Vector2(turretPos.x - transform.position.x,
                turretPos.y - transform.position.y);
            transform.up = turretDirection;
        }


        /*if ((Input.GetKey(KeyCode.Comma) && _TANKPLAYER == 'A' && !_centerTurretA) ||
            (Input.GetKey(KeyCode.X) && _TANKPLAYER == 'B' && !_centerTurretB))
        {
            //_audioSource.PlayOneShot(_turretRotation);
            Quaternion direction = transform.rotation * Quaternion.Euler(0, 0, _ROTATION);
            _rbody.MoveRotation(direction);
        }
        else if ((Input.GetKey(KeyCode.Period) && _TANKPLAYER == 'A' && !_centerTurretA) ||
            (Input.GetKey(KeyCode.C) && _TANKPLAYER == 'B' && !_centerTurretB))
        {
            //_audioSource.PlayOneShot(_turretRotation);
            Quaternion direction = transform.rotation * Quaternion.Euler(0, 0, -1 * _ROTATION);
            _rbody.MoveRotation(direction);
        }*/
    }
    void FireTurret()
    {
        if ((Input.GetMouseButtonDown(0) && _TANKPLAYER == 'A') ||
            (Input.GetKeyDown(KeyCode.LeftShift) && _TANKPLAYER == 'B'))
        {
            _managerScript.shoot(_TANKPLAYER);
        }

    }
}
