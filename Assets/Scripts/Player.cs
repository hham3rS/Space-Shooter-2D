using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float _speed;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _shieldsVisuals;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    private float _fireRate = 0.2f;
    private float _canFire = 0.0f;
    private int _lives = 3;
    private SpawnManager _spawnManager;
    private bool _isTripleShotActive = false;
    private bool _isSpeedBoostActive = false;
    private bool _isShieldActive = false;
    private int _score;
    [SerializeField]
    private GameObject _rEngine;
    [SerializeField]
    private GameObject _lEngine;
    [SerializeField]
    private AudioClip _laserAudio;
    private AudioSource _laserSrc;
    private AudioSource _explSrc;
    private AudioSource _powerUpSrc;
    private UIManager _uiManager;
    

    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _laserSrc = GetComponent<AudioSource>();
        _explSrc = GetComponent<AudioSource>();
        if (_uiManager == null)
        {
            Debug.LogError("UI Manager is null!");
        }
        if (_spawnManager == null)
        {
            Debug.LogError("Spawn Manager is null!");
        }
        if (_laserSrc == null)
        {
            Debug.LogError("Audio Source on player is null!");
        }
        else
        {
            _laserSrc.clip = _laserAudio;
        }
        
    }

    void Update()
    {
        ShipMovement();
        Boundaries();
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }

    }

    void Boundaries()
    {
        if (transform.position.y >= 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if (transform.position.y <= -3.4f)
        {
            transform.position = new Vector3(transform.position.x, -3.4f, 0);
        }
        //optimized way of the code above
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.4f, 0), 0);
        //cannot be clamped we need to move left and right to wrap
        if (transform.position.x >= 11.25)
        {
            transform.position = new Vector3(-11.25f, transform.position.y, 0);
        }
        else if (transform.position.x <= -11.25)
        {
            transform.position = new Vector3(11.25f, transform.position.y, 0);
        }
    }


    void ShipMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 dirMovement = new Vector3(horizontalInput, verticalInput, 0);
        //if speedboost enabled move at 8.5f speed duration 5s
        _speed = 5.0f;
        if (_isSpeedBoostActive == true)
        {
            _speed = 9.0f;
        }
        transform.Translate(dirMovement * _speed * Time.deltaTime);


    }


    void FireLaser()
    {
        Vector3 offsetY = new Vector3(0, 1.05f, 0);
        
            //canfire = 1.0f + 0.2f is time.time 1.1f > _canFire no so you cannot shoot
            _canFire = Time.time + _fireRate;

            if (_isTripleShotActive == true)
            {
                Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(_laserPrefab, transform.position + offsetY, Quaternion.identity);
            }

        _laserSrc.Play();
        }

    public void Damage()
    {
        Debug.Log("Damage Taken");

        if (_isShieldActive == true)
        {
            _isShieldActive = false;
            _shieldsVisuals.SetActive(false);   //disable the visualizer
            return;
        }

        _lives --;

        _uiManager.UpdateLives(_lives);

        if (_lives == 2)
        {
            _rEngine.SetActive(true);
        }
        else if (_lives == 1)
        {
            _lEngine.SetActive(true);
        } 
        else if (_lives < 1)
        {
            _spawnManager.OnPlayerDeath();
            _explSrc.Play();
            Destroy(this.gameObject);
        }
    }

    public void TripleShotActive()
    {
        _isTripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());

    }

    IEnumerator TripleShotPowerDownRoutine()
    {
            yield return new WaitForSeconds(5.0f);
            _isTripleShotActive = false;
    }

    public void SpeedPowerUpActive()
    {
        _isSpeedBoostActive = true;
        StartCoroutine(SpeedBoostDownRoutine());

    }

    IEnumerator SpeedBoostDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isSpeedBoostActive = false;
    }

    public void ShieldsActive()
    {
        _isShieldActive = true;
        _shieldsVisuals.SetActive(true);   //enable visualizer
    }

    public void AddScore(int points)
    {
        _score += points;
        _uiManager.UpdateScore(_score);
    }

}
