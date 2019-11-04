using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{

    [SerializeField]
    private float _powerUpSpeed = 3.0f;
    [SerializeField] //0 = triple shot , 1 = speed, 2 = shield
    private int _powerUpID;
    [SerializeField]
    private AudioClip _pwrUpClip;

    void Update()
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
            Player player = other.transform.GetComponent<Player>();
            AudioSource.PlayClipAtPoint(_pwrUpClip, transform.position);
            if (player != null)
            {
                switch (_powerUpID)
                {
                    case 0:
                        player.TripleShotActive();
                        break;
                    case 1:
                        player.SpeedPowerUpActive();
                        break;
                    case 2:
                        player.ShieldsActive();
                        break;
                }
            }
            Destroy(this.gameObject);
        }
    }
}
