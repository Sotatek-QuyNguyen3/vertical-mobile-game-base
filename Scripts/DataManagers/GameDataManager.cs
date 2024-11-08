using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataManager : Singleton<GameDataManager>
{
    [SerializeField] private GameDataOOP _gameData = new GameDataOOP();

    public GameDataOOP GameData
    {
        get { return _gameData; }
        set { _gameData = value; }
    }

    private void Start()
    {
        if (_gameData == null)
        {
            _gameData = new GameDataOOP();
        }
    }
}
