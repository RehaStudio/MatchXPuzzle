using System;
using UnityEngine;

public class GameManager
{
    public event Action<int> OnGameRebuild;
    public void GameRebuild(int lineCount)
    {
        OnGameRebuild?.Invoke(lineCount);
    }
}
