using UnityEngine;
using SpaceShooter.PlayerModules;

namespace SpaceShooter.Misc
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private float _enemySpeed = 4.0f;
        private PlayerDamage _playerDamage;
        private Player _player;
        private Animator _animator;
        private AudioSource _explSrc;
        private bool _isHit = false;

        private void Awake()
        {
            _explSrc = GetComponent<AudioSource>();
            _playerDamage = GameObject.Find("Player").GetComponent<PlayerDamage>();
            _animator = GetComponent<Animator>();
            _player = GetComponent<Player>();
        }

        void Start()
        {
            transform.position = new Vector3(Random.Range(-8.0f, 8.0f), 7.5f, 0);
        }

        void Update()
        {
            EnemyMovement();
        }

        private void EnemyMovement()
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
            if (!_isHit)
            {
                _isHit = true;
                //_player = other.transform.GetComponent<Player>();
                if (other.tag == "Player")
                {
                    if (_playerDamage != null)
                    {
                        _playerDamage.Damage();
                    }

                    CollisionAnimSound();
                }
                
                if (other.tag == "Laser")
                {
                    Destroy(other.gameObject);
                    if (_player != null)
                    {
                        _player.AddScore(Random.Range(5, 25));
                    }
                    CollisionAnimSound();
                }
            }

        }

        private void CollisionAnimSound()
        {
            _animator.SetTrigger("OnEnemyDeath"); //trigger anim 
            _enemySpeed = 0;
            _explSrc.Play();
            Destroy(this.gameObject, 2.5f);
        }
    }
}  
