using System.Collections;
using UnityEngine;

namespace SpaceShooter.Managers
{
    public class SpawnManager : MonoBehaviour
    {
        [SerializeField] private GameObject _enemyPrefab;
        [SerializeField] private GameObject _enemyContainer;
        [SerializeField] private GameObject _asteroid;
        [SerializeField] private GameObject[] _powerUps;
        private bool _stopSpawning = false;

        public void StartSpawning()
        {
            StartCoroutine(SpawnEnemyRoutine());
            StartCoroutine(SpawnPowerUpRoutine());
        }

        IEnumerator SpawnEnemyRoutine()
        {
            yield return new WaitForSeconds(3.0f);
            Vector3 posToSpawn = new Vector3(Random.Range(-8.0f, 8.0f), 7.5f, 0);
            while (!_stopSpawning)
            {
                GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);
                newEnemy.transform.parent = _enemyContainer.transform;
                yield return new WaitForSeconds(5.0f);
            }

        }

        IEnumerator SpawnPowerUpRoutine()
        {
            yield return new WaitForSeconds(3.0f);
            while (!_stopSpawning)
            {
                Vector3 posToSpawnPowerUp = new Vector3(Random.Range(-8.0f, 8.0f), 7.5f, 0);
                int randomPowerUp = Random.Range(0, 3);
                GameObject powerUp = Instantiate(_powerUps[randomPowerUp], posToSpawnPowerUp, Quaternion.identity);
                yield return new WaitForSeconds(Random.Range(7.0f, 10.0f));
            }

        }

        public void OnPlayerDeath()
        {
            _stopSpawning = true;
        }
    }
}
