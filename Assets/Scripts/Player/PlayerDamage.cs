using UnityEngine;
using SpaceShooter.Managers;

namespace SpaceShooter.PlayerModules 
{
    public class PlayerDamage : MonoBehaviour
    {
        //[SerializeField] private GameObject _shieldsVisuals;
        [SerializeField] private GameObject _rEngine;
        [SerializeField] private GameObject _lEngine;
        //private bool _isShieldActive = false;
        private int _lives = 3;
        private AudioSource _explSrc;
        private UIManager _uiManager;
        private SpawnManager _spawnManager;

        private void Awake()
        {
            _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
            _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
            _explSrc = GetComponent<AudioSource>();
        }

        public void Damage()
        {
            if (_isShieldActive)
            {
                _isShieldActive = false;
                _shieldsVisuals.SetActive(false);
                return;
            }

            _lives--;

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
                PlayerDeath();
            }
        }

        private void PlayerDeath()
        {
            _spawnManager.OnPlayerDeath();
            _explSrc.Play();
            Destroy(this.gameObject);
        }
    }
}