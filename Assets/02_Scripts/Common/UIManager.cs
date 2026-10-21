using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Button _startButton;

    private void OnEnable()
    {
        _startButton.onClick.AddListener(OnStartButtonClick);
    }

    private void OnStartButtonClick()
    {
        SceneManager.LoadScene("Level01");
        SceneManager.LoadScene("InGame", LoadSceneMode.Additive);
    }
}
