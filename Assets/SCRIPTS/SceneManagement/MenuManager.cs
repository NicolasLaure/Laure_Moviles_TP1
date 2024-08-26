using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneManagement
{
    public class MenuManager : MonoBehaviour
    {
        [SerializeField] private GameConfig config;
        public void PlaySinglePlayer()
        {
            config.isSinglePlayer = true;
            SceneManager.LoadScene(1);
        }
        
        public void PlayMultiPlayer()
        {
            config.isSinglePlayer = false;
            SceneManager.LoadScene(1);
        }
    }
}
