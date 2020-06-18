using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooter.Managers
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private Text _scoreText;
        [SerializeField] private Image _livesImg;
        [SerializeField] private Sprite[] _liveSprites;
        [SerializeField] private Text _gameOverText;
        [SerializeField] private Text _restartLevelText;
        private GameManager _gameManager;

        private void Awake()
        {
            _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        }

        private void Start()
        {
            _scoreText.text = "Score: " + 0;
            _gameOverText.gameObject.SetActive(false);
        }

        public void UpdateScore(int playerScore)
        {
            _scoreText.text = "Score: " + playerScore;
        }

        public void UpdateLives(int currentLives)
        {
            _livesImg.sprite = _liveSprites[currentLives];
            if (currentLives == 0)
                GameOverSequence();
        }

        void GameOverSequence()
        {
            _gameManager.GameOver();
            _gameOverText.gameObject.SetActive(true);
            _restartLevelText.gameObject.SetActive(true);
            StartCoroutine(FlickerGameOver());
        }

        IEnumerator FlickerGameOver()
        {
            while (true)
            {
                _gameOverText.text = "GAME OVER";
                yield return new WaitForSeconds(0.75f);
                _gameOverText.text = "";
                yield return new WaitForSeconds(0.75f);
            }
        }
    }
}
