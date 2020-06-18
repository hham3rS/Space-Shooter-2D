using System.Collections;
using UnityEngine;

namespace SpaceShooter.PlayerModules 
{
    public class PlayerPowerUps : MonoBehaviour
    {
        [SerializeField] private GameObject _shieldsVisuals;
        private bool _isShieldActive = false;
        private bool _isTripleShotActive = false;
        private bool _isSpeedBoostActive = false;

        public void TripleShotActive()
        {
            _isTripleShotActive = true;
            StartCoroutine(TripleShotPowerDownRoutine());
        }

        IEnumerator TripleShotPowerDownRoutine()
        {
            yield return new WaitForSeconds(5.0f);
            _isTripleShotActive = !_isTripleShotActive;
        }

        public void SpeedPowerUpActive()
        {
            _isSpeedBoostActive = true;
            StartCoroutine(SpeedBoostDownRoutine());
        }

        IEnumerator SpeedBoostDownRoutine()
        {
            yield return new WaitForSeconds(5.0f);
            _isSpeedBoostActive = !_isSpeedBoostActive;
        }

        public void ShieldsActive()
        {
            _isShieldActive = true;
            _shieldsVisuals.SetActive(true);
        }
    }
}