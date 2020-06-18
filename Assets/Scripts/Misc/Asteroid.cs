using UnityEngine;
using SpaceShooter.Managers;

namespace SpaceShooter.Misc
{
    public class Asteroid : MonoBehaviour
    {
        [SerializeField] private float _rotateSpeed = 19.0f;
        [SerializeField] private GameObject _explosionPrefab;
        private SpawnManager _spawnManager;

        private void Awake()
        {
            _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        }

        private void Update()
        {
            transform.Rotate(Vector3.forward * _rotateSpeed * Time.deltaTime);
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Laser")
            {
                DestroyAsteroid(other);
            }
        }

        private void DestroyAsteroid(Collider2D other)
        {
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            _spawnManager.StartSpawning();
            Destroy(this.gameObject, 0.25f);
        }
    }
}
