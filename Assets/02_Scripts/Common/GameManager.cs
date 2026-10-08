using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private List<Transform> _spawnPoints;
    [SerializeField] private GameObject _monsterPrefab;
    [Header("생성 옵션")] 
    [SerializeField] private float _createRate = 3.0f;

    private bool _isGameOver = false;
    
    public bool IsGameOver
    {
        get { return _isGameOver; }
        set
        {
            _isGameOver = value;
            if (_isGameOver)
            {
                CancelInvoke(nameof(CreateMonster));
            }
        }
    }
    
    protected override void Awake()
    {
        base.Awake();
        
        // _STAGES 게임오브젝트를 검색
        var pointGroup = GameObject.Find("_STAGES/SpawnPointGroup").transform;
        pointGroup.GetComponentsInChildren<Transform>(_spawnPoints);
    }
    
    private void Start()
    {
        // Invoke(nameof(CreateMonster), _createRate); // 1회 호출
        InvokeRepeating(nameof(CreateMonster), 2.0f, _createRate);
    }

    private void CreateMonster()
    {
        // 생성할 위치
        var index = Random.Range(1, _spawnPoints.Count);
        Instantiate(_monsterPrefab, _spawnPoints[index].position, _spawnPoints[index].rotation);
    }
}
