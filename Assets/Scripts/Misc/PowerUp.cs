using UnityEngine;
using SpaceShooter.PlayerModules;

namespace SpaceShooter.Misc
{
    public class PowerUp : MonoBehaviour
    {
        [SerializeField] private float _powerUpSpeed = 3.0f;
        [SerializeField] private int _powerUpID; //0 = triple shot , 1 = speed, 2 = shield
        [SerializeField] private AudioClip _pwrUpClip;

        private void Update()
        {
            Vector3 powUpMovement = Vector3.down * _powerUpSpeed * Time.deltaTime;
            transform.Translate(powUpMovement);
            if (transform.position.y <= -7.5f)
            {
                Destroy(this.gameObject);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Player")
            {
                PlayerPowerUps playerPowerUps = other.transform.GetComponent<PlayerPowerUps>();
                AudioSource.PlayClipAtPoint(_pwrUpClip, transform.position);
                if (playerPowerUps != null)
                {
                    switch (_powerUpID)
                    {
                        case 0:
                            playerPowerUps.TripleShotActive();
                            break;
                        case 1:
                            playerPowerUps.SpeedPowerUpActive();
                            break;
                        case 2:
                            playerPowerUps.ShieldsActive();
                            break;
                    }
                }
                Destroy(this.gameObject);
            }
        }
    }
}
