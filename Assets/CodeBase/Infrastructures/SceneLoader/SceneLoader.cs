using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader
{
    private readonly ICoroutineRunner _coroutineRunner;

    public SceneLoader(ICoroutineRunner coroutineRunner)
    {
        _coroutineRunner = coroutineRunner;
    }

    public void Load(Scenes scene, Action onLoaded = null)
    {
        _coroutineRunner.StartCoroutine(LoadScene(scene, onLoaded));
    }

    private IEnumerator LoadScene(Scenes scene, Action onLoaded = null)
    {
        if (SceneManager.GetActiveScene().buildIndex == (int)scene)
        {
            onLoaded?.Invoke();
            yield break;
        }

        AsyncOperation waitNextScene = SceneManager.LoadSceneAsync((int)scene);

        while (!waitNextScene.isDone)
        {
            yield return null;
        }

        onLoaded?.Invoke();
    }


}

