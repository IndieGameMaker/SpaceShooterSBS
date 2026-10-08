using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private List<Transform> _spawnPoints;
    [SerializeField] private GameObject _monsterPrefab;
    [Header("생성 옵션")] 
    [SerializeField] private float _createRate = 3.0f;
    
    
    protected override void Awake()
    {
        base.Awake();
        
        // _STAGES 게임오브젝트를 검색
        var pointGroup = GameObject.Find("_STAGES/SpawnPointGroup").transform;
        pointGroup.GetComponentsInChildren<Transform>(_spawnPoints);
        
    }
}
