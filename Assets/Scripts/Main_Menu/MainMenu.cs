using UnityEngine;
using UnityEngine.SceneManagement;

namespace SpaceShooter.Menu
{
    public class MainMenu : MonoBehaviour
    {
        public void LoadGame()
        {
            SceneManager.LoadScene(1);
        }
    }
}