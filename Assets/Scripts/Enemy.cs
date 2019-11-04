using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _enemySpeed = 4.0f;
    private Player _player;
    private Animator _animator;
    private AudioSource _explSrc;
    private bool _isHit = false;


    void Start()
    {
        _explSrc = GetComponent<AudioSource>();
        transform.position = new Vector3(Random.Range(-8.0f, 8.0f), 7.5f, 0);
        if (_player == null)
        {
            Debug.LogError("Player is null");
        }
        _player = GameObject.Find("Player").GetComponent<Player>();
        if (_animator == null)
        {
            Debug.LogError("Animator is null");
        }
        _animator = GetComponent<Animator>();
    }
    void Update()
    {
        Vector3 enemyMovement = Vector3.down * _enemySpeed * Time.deltaTime;
        transform.Translate(enemyMovement);
        Vector3 respawnTop = new Vector3(Random.Range(-8.0f, 8.0f), 7.5f, 0);
        if (transform.position.y <= -5.5f)
        {
            transform.position = respawnTop;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_isHit == false)
        {
            _isHit = true;

            Player player = other.transform.GetComponent<Player>();
            if (other.tag == "Player")
            {
                if (player != null)
                {
                    player.Damage();
                }

                _animator.SetTrigger("OnEnemyDeath"); //trigger anim 
                _enemySpeed = 0;
                _explSrc.Play();
                Destroy(this.gameObject, 2.5f);
            }

            if (other.tag == "Laser")
            {
                Destroy(other.gameObject);
                if (_player != null)
                {
                    _player.AddScore(Random.Range(5, 25));
                }

                _animator.SetTrigger("OnEnemyDeath"); //trigger anim
                _enemySpeed = 0;
                _explSrc.Play();
                Destroy(this.gameObject, 2.5f);
            }
        }

    }
}
