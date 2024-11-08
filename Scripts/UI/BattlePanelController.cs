using UnityEngine;

public class BattlePanelController : MenuPanel
{
    
    public void OnClickStartBattle()
    {
        SceneLoadManager.Instance.LoadScene(SceneName.Play);
    }
}

