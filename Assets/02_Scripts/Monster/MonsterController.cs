using System.Collections;
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

    public bool IsDead = false;

    private WaitForSeconds _ws;

    private void Start()
    {
        _monsterTr = transform; // GetComponent<Transform>();
        _playerTr = GameObject.FindGameObjectWithTag("PLAYER")?.transform;
		_ws = new WaitForSeconds(0.3f);

        StartCoroutine(CheckMonsterState());
    }

    private IEnumerator CheckMonsterState()
    {
        while (!IsDead)
        {
            // 공격 사정거리 이내인 경우
            if ((_monsterTr.position - _playerTr.position).sqrMagnitude <= _attackDist * _attackDist)
            {
                _state = State.Attack;
            }
			else if ((_monsterTr.position - _playerTr.position).sqrMagnitude <= _traceDist * _traceDist)
            {
                _state = State.Trace;
            }
			else
			{
            	_state = State.Idle;
			}

            yield return _ws;
        }
    }

    private IEnumerator MonsterAction()
    {
        while (!IsDead)
        {
            // 몬스터의 상태에 따라서 행동 패턴 분기
            switch (_state)
            {
                case State.Idle:
                    Debug.Log("Idle 상태 진입");
                    break;
                case State.Trace:
                    Debug.Log("Trace");
                    break;
                case State.Attack:
                    Debug.Log("Attack");
                    break;
                case State.Die:
                    Debug.Log("Die");
                    break;
            }
            yield return _ws;
        }
    }
}
