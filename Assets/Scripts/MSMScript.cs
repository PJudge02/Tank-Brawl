using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class MSMScript : MonoBehaviour
{
    public GameObject _barrelA;
    public GameObject _barrelB;
    public GameObject _bulletPrefab;
    public BulletScript _bulletScript;
    public int _NUMSHOTS;

    // keept track of health
    public Text _tankAHeath;
    public Text _tankBHeath;
    int tankAHealth;
    int tankBHealth;

    // text ammo tracker
    public Text _tankAAmmo;
    public Text _tankBAmmo;
    // keeps track of how many shots fired
    int tankAShotsFired;
    int tankBShotsFired;

    // handles reloading
    public float _reloadingTime;
    float tankAStartTimeReload;
    bool tankAReloading;
    float tankBStartTimeReload;
    bool tankBReloading;

    // audio
    public AudioClip _bulletHit;
    public AudioClip _tankFiring;
    public AudioClip _gameMusic;
    AudioSource _audioSource;

    public int health;

    // Start is called before the first frame update
    void Start()
    {
        tankAHealth = health;
        tankAShotsFired = 0;
        tankBHealth = health;
        tankBShotsFired = 0;
        _tankAHeath.text = "P1 Health: " + tankAHealth;
        _tankBHeath.text = "P2 Health: " + tankBHealth;
        _tankAAmmo.text = "P1 Ammo: " + (_NUMSHOTS - tankAShotsFired);
        _tankBAmmo.text = "P2 Ammo: " + (_NUMSHOTS - tankBShotsFired);
        _audioSource = GetComponent<AudioSource>();

        _audioSource.clip = _gameMusic;
        _audioSource.loop = true;
        _audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        // text for health left for the two players
        int APlayerRed = (255 / health) * tankAHealth;
        int BPlayerRed = (255 / health) * tankBHealth;
        Color shadeA = new Color(1, (float)APlayerRed / 255, (float)APlayerRed / 255);
        Color shadeB = new Color(1, (float)BPlayerRed / 255, (float)BPlayerRed / 255);
        _tankAHeath.color = shadeA;
        _tankBHeath.color = shadeB;
        _tankAHeath.text = "P1 Health: " + tankAHealth;
        _tankBHeath.text = "P2 Health: " + tankBHealth;

        // text for ammo left for the two players
        if (tankAShotsFired == _NUMSHOTS)
        {
            _tankAAmmo.color = Color.red;
        }else { _tankAAmmo.color = Color.white;  }
        if (tankBShotsFired == _NUMSHOTS)
        {
            _tankBAmmo.color = Color.red;
        }
        else { _tankBAmmo.color = Color.white; }

        _tankAAmmo.text = "P1 Ammo: " + (_NUMSHOTS - tankAShotsFired);
        _tankBAmmo.text = "P2 Ammo: " + (_NUMSHOTS - tankBShotsFired);

        // reloading
        if (tankAReloading && Time.time - tankAStartTimeReload >= _reloadingTime)
        {
            tankAReloading = false;
            tankReload('A');
        }
        if (tankBReloading && Time.time - tankBStartTimeReload >= _reloadingTime)
        {
            tankBReloading = false;
            tankReload('B');
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void tankHit(char player)
    {
        _audioSource.PlayOneShot(_bulletHit);
        if (player == 'A')
        {
            tankAHealth--;
        }
        if (player == 'B')
        {
            tankBHealth--;
        }
        if (player == '!')
        {
        }
        if (tankAHealth <= 0 || tankBHealth <= 0)
        {
            SceneManager.LoadScene("StartScreen");
        }
    }

    public void shoot(char player)
    {
        if (tankAShotsFired < _NUMSHOTS && player == 'A')
        {
            _audioSource.PlayOneShot(_tankFiring);
            Quaternion turretDirection = _barrelA.transform.rotation;

            GameObject bullet = Instantiate(_bulletPrefab,
                new Vector2(_barrelA.transform.position.x, _barrelA.transform.position.y), turretDirection);
            bullet.GetComponent<BulletScript>().setPlayer(player);
            tankAShotsFired++;
            // starts process for reloading
            tankReload('A');
        }
        else if (tankBShotsFired < _NUMSHOTS && player == 'B')
        {
            _audioSource.PlayOneShot(_tankFiring);
            Quaternion turretDirection = _barrelB.transform.rotation;

            GameObject bullet = Instantiate(_bulletPrefab,
                new Vector2(_barrelB.transform.position.x, _barrelB.transform.position.y), turretDirection);
            bullet.GetComponent<BulletScript>().setPlayer(player);
            tankBShotsFired++;
            // starts process for reloading
            tankReload('B');
        }
    }
    // decreases the number of shots a tank fired
    public void tankReload(char player)
    {
        if(tankAShotsFired == 1 && !tankAReloading) {
            tankAReloading = true;
            tankAStartTimeReload = Time.time;
        }
        if (tankBShotsFired == 1 && !tankBReloading)
        {
            tankBReloading = true;
            tankBStartTimeReload = Time.time;
        }
        if (player == 'A' && !tankAReloading)
        {
            tankAShotsFired = 0;
        }
        if (player == 'B' && !tankBReloading)
        {
            tankBShotsFired = 0;
        }
    }
}
