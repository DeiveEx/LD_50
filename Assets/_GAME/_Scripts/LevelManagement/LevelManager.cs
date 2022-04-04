using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _GAME._Scripts.LevelManagement
{
    [CreateAssetMenu(fileName = "LevelManager", 
        menuName = "Level Manager")]
    public class LevelManager : ScriptableObject
    {
        public GameScene[] gameLevels;
        public GameScene gameOverScene;

        private int _currentGameLevelIndex;

        private UniTask LoadScene(string sceneName) =>
            SceneManager.LoadSceneAsync(sceneName).ToUniTask();

        private UniTask LoadLevelAtAsync(int index)
        {
            var sceneName = gameLevels[index].name;
            return LoadScene(sceneName);
        }

        public async void LoadLevelAt(int index) => 
            await LoadLevelAtAsync(index);

        public async void LoadNextLevel()
        {
            ++_currentGameLevelIndex;

            await LoadLevelAtAsync(_currentGameLevelIndex);
        }

        public async void LoadGameOverLevel()
        {
            _currentGameLevelIndex = 0;
            
            await LoadScene(gameOverScene.name);
        }
    }
}