using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class tankScript : MonoBehaviour
{
    Rigidbody2D _rbody;
    public float _SPEED; //10f
    public float _ROTATION; //5f
    public string _Axis;
    public GameObject _TURRET;
    public TurretScript _turretScript;
    public char _TANKPLAYER;
    Rigidbody2D _trbody;
    // Start is called before the first frame update
    void Start()
    {
        _trbody = _TURRET.GetComponent<Rigidbody2D>();
        _rbody = GetComponent<Rigidbody2D>();    
    }

    private void Update()
    {
        handleMovement();
        
    }

    void handleMovement()
    {
        
        float y = _SPEED * Input.GetAxis(_Axis);
        _rbody.velocity = new Vector2(transform.up.x * y, y * transform.up.y);
        Vector2 newPos = new Vector2(_rbody.position.x, _rbody.position.y);
        _trbody.transform.position = newPos;
        

        if ((_TANKPLAYER == 'A' && Input.GetKey(KeyCode.LeftArrow)) ||
            (_TANKPLAYER == 'B' && Input.GetKey(KeyCode.F)))
        {
            Quaternion directionBody = transform.rotation * Quaternion.Euler(0, 0, _ROTATION);
            Quaternion directionTurret = _TURRET.transform.rotation * Quaternion.Euler(0, 0, _ROTATION);
            _rbody.MoveRotation(directionBody);
        }else if ((_TANKPLAYER == 'A' && Input.GetKey(KeyCode.RightArrow)) ||
            (_TANKPLAYER == 'B' && Input.GetKey(KeyCode.H)))
        {
            Quaternion directionBody = transform.rotation * Quaternion.Euler(0, 0, -1* _ROTATION);
            Quaternion directionTurret = _TURRET.transform.rotation * Quaternion.Euler(0, 0, -1*_ROTATION);
            _rbody.MoveRotation(directionBody);
        }
        else { _rbody.MoveRotation(transform.rotation); }


    }
}
