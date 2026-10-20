using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private List<Transform> _spawnPoints;
    [SerializeField] private GameObject _monsterPrefab;
    [Header("생성 옵션")]
    [SerializeField] private float _createRate = 3.0f;

    // Object Pool
    [SerializeField] private List<Transform> _monsterPool;
    [SerializeField] private int _maxPool = 10;

    // UI
    [Header("UI")]
    [SerializeField] private Image _hpBar;
    [SerializeField] private TextMeshProUGUI _scoreText;

    private bool _isGameOver = false;
    
    // 스코어 저장용 변수
    private int _totScore;
    private const string TOT_SCORE = "TOT_SCORE";

    public void ChangeHpBar(float currHp, float maxHp)
    {
        _hpBar.fillAmount = currHp / maxHp;
    }

    public void DisplayScore(int score)
    {
        _totScore += score;
        _scoreText.text = $"SCORE: <color=#ff0000>{_totScore:00000}</color>";
        
        // 점수 저장 기능
        PlayerPrefs.SetInt(TOT_SCORE, _totScore);
    }

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
        // 저장된 최고 점수 불러오기
        if (PlayerPrefs.HasKey(TOT_SCORE))
        {
            _totScore = PlayerPrefs.GetInt(TOT_SCORE);
            DisplayScore(0);
        }

        // _totScore = PlayerPrefs.GetInt(TOT_SCORE, 0);
        // DisplayScore(0);
        
        // Invoke(nameof(CreateMonster), _createRate); // 1회 호출
        InvokeRepeating(nameof(CreateMonster), 2.0f, _createRate);

        // Monster Pool 에 몬스터 생성
        for (int i = 0; i < _maxPool; i++)
        {
            var monster = Instantiate(_monsterPrefab);
            monster.name = $"Monster_{i:00}";
            monster.SetActive(false);
            _monsterPool.Add(monster.transform);
        }
    }

    private void CreateMonster()
    {
        // 생성할 위치
        var index = Random.Range(1, _spawnPoints.Count);
        // Instantiate(_monsterPrefab, _spawnPoints[index].position, _spawnPoints[index].rotation);
        foreach (var monster in _monsterPool)
        {
            if (monster.gameObject.activeSelf == false)
            {
                monster.position = _spawnPoints[index].position;
                var dir = Vector3.zero - _spawnPoints[index].position;
                monster.rotation = Quaternion.LookRotation(dir);
                monster.gameObject.SetActive(true);
                break;
            }
        }
    }
}
