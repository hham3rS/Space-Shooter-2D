using UnityEngine;
using SpaceShooter.Managers;

namespace SpaceShooter.PlayerModules 
{
    public class Player : MonoBehaviour
    {
        private float _speed = 5.0f;
        [SerializeField] private GameObject _laserPrefab;
        [SerializeField] private GameObject _tripleShotPrefab;
        private float _fireRate = 0.2f;
        private float _canFire = 0.0f;
        private int _score;
        [SerializeField] private AudioClip _laserAudio;
        private AudioSource _laserSrc;
        private UIManager _uiManager;
        private AudioSource _powerUpSrc;
        PlayerPowerUps playerPowerUps;

        private void Awake()
        {
            _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
            _laserSrc = GetComponent<AudioSource>();
            playerPowerUps = GetComponent<PlayerPowerUps>();
        }

        private void Start()
        {
            transform.position = new Vector3(0, 0, 0);
            if (_laserSrc != null)
            {
                _laserSrc.clip = _laserAudio;
            }
        }

        private void Update()
        {
            ShipMovement();
            Boundaries();
            if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
                FireLaser();
        }

        private void Boundaries()
        {
            transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.4f, 0), 0);

            if (transform.position.x >= 11.25)
            {
                transform.position = new Vector3(-11.25f, transform.position.y, 0);
            }
            else if (transform.position.x <= -11.25)
            {
                transform.position = new Vector3(11.25f, transform.position.y, 0);
            }
        }

        private void ShipMovement()
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            Vector3 dirMovement = new Vector3(horizontalInput, verticalInput, 0);
            if (_isSpeedBoostActive)
            {
                _speed = 9.0f;
            }
            transform.Translate(dirMovement * _speed * Time.deltaTime);
        }

        private void FireLaser()
        {
            Vector3 offsetY = new Vector3(0, 1.05f, 0);
            _canFire = Time.time + _fireRate;

            if (_isTripleShotActive)
            {
                Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(_laserPrefab, transform.position + offsetY, Quaternion.identity);
            }
            _laserSrc.Play();
        }

        public void AddScore(int points)
        {
            _score += points;
            _uiManager.UpdateScore(_score);
        }

    }
}