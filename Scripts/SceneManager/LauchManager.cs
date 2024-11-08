using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LauchManager : Singleton<LauchManager>
{


    private void Start() {
        SceneLoadManager.Instance.LoadScene(SceneName.Menu);
    }
}
