using UnityEngine;

public enum State
{
    Idle,
    Trace,
    Attack,
    Die
}

public class MonsterController : MonoBehaviour
{
    [SerializeField] private State _state = State.Idle;

    [SerializeField, Range(5f, 10f)] private float _traceDist = 8f;
    [SerializeField] private float _attackDist = 2f;

    private Transform _monsterTr;
    private Transform _playerTr;

    private void Start()
    {
        _monsterTr = transform; // GetComponent<Transform>();
        _playerTr = GameObject.FindGameObjectWithTag("PLAYER")?.transform;
    }
}
