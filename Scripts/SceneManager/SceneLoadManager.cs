using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UIElements.Experimental;

public class SceneLoadManager : Singleton<SceneLoadManager>
{
    [SerializeField] private AnimationCurve _loadOutSceneAnimationCurve;
    [SerializeField] private AnimationCurve _loadInSceneAnimationCurve;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private float _loadSceneDuration = 0.4f;

    public void LoadScene(SceneName sceneName)
    {
        StartLoadSceneAnimation(sceneName);
    }

    private void StartLoadSceneAnimation(SceneName sceneName){
        LeanTween.value(0, 1, _loadSceneDuration)
        .setOnStart(() => {
            _canvasGroup.gameObject.SetActive(true);
        })
        .setOnUpdate((float value) => {
            _canvasGroup.alpha = _loadOutSceneAnimationCurve.Evaluate(value);
        })
        .setOnComplete(() => {
            _canvasGroup.alpha = 1;
            StartCoroutine(LoadSceneAsync(sceneName.ToString()));
        });
    }

    private void StartLoadInSceneAnimation(){
        LeanTween.value(0, 1, _loadSceneDuration)
        .setOnUpdate((float value) => {
            _canvasGroup.alpha = _loadInSceneAnimationCurve.Evaluate(value);
        })
        .setOnComplete(() => {
            _canvasGroup.alpha = 0;
            _canvasGroup.gameObject.SetActive(false);
        });
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        while (!asyncLoad.isDone)
        {
            Debug.Log($"Loading progress: {asyncLoad.progress * 100}%");

            yield return null; 
        }
        StartLoadInSceneAnimation();
    }
}
