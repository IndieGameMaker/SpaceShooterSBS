using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private List<Transform> _spawnPoints;

    protected override void Awake()
    {
        base.Awake();
        
        // _STAGES 게임오브젝트를 검색
        var pointGroup = GameObject.Find("_STAGES/SpawnPointGroup").transform;
        pointGroup.GetComponentsInChildren<Transform>(_spawnPoints);
        
        //var points = stages.Find("SpawnPointGroup").transform;
    }
}
