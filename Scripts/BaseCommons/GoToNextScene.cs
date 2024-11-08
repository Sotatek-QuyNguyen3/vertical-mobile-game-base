using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToNextScene : MonoBehaviour
{
    [SerializeField] private SceneName nextScene;
    // Start is called before the first frame update
    void Start()
    {
        SceneManager.LoadScene(nextScene.ToString());
    }
}