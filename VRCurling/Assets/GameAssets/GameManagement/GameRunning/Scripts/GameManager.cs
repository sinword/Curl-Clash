using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Assertions;
using Cysharp.Threading.Tasks;

public class GameManager : SingletonWithoutDestroy<GameManager>
{
    LevelRunner levelRunner;
    public async void EnterLevelScene(int levelIndex){
        string levelName = "Level" + levelIndex;
        await SceneManager.LoadSceneAsync(levelName);
        levelRunner = LevelRunner.Instance;
        Assert.IsNotNull(levelRunner);
        levelRunner.OnRequestExit.AddListener(requestExitHandler);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    private void requestExitHandler()
    {
        if (levelRunner != null)
        {
            levelRunner.OnRequestExit.RemoveListener(requestExitHandler);
        }
        levelRunner = null;
        SceneManager.LoadScene("StartScene");
    }
}
