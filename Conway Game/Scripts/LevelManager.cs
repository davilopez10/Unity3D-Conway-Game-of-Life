﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    public void RestartLevel()
    {
        SceneManager.LoadScene(0);
    }
}
