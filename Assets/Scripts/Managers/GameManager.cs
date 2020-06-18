using UnityEngine;
using UnityEngine.SceneManagement;

namespace SpaceShooter.Managers
{
    public class GameManager : MonoBehaviour
    {
        private bool _isGameOver;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.R) && _isGameOver == true)
                SceneManager.LoadScene(1); //Current Game Scene

        }

        public void GameOver()
        {
            _isGameOver = true;
        }
    }
}
