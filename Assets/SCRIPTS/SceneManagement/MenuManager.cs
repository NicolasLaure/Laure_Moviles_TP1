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
            Loader.ChangeScene(2);
        }

        public void PlayMultiPlayer()
        {
            config.isSinglePlayer = false;
            Loader.ChangeScene(2);
        }
    }
}