using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public enum State
{
    Idle,
    Trace,
    Attack,
    Die
}

public class MonsterController : MonoBehaviour, IDamageable
{
    [SerializeField] private State _state = State.Idle;

    [SerializeField, Range(5f, 10f)] private float _traceDist = 8f;
    [SerializeField] private float _attackDist = 2f;
    [SerializeField] private float _hp = 100f;
    
    private Transform _monsterTr;
    private Transform _playerTr;
    private NavMeshAgent _agent;
    private Animator _animator;
    private CapsuleCollider _collider;

    public bool IsDead = false;

    private WaitForSeconds _ws;
    
    // Animator Parameter 해시값 추출
    private static readonly int hashIsTrace = Animator.StringToHash("IsTrace");
    private static readonly int hashIsAttack = Animator.StringToHash("IsAttack");
    private static readonly int hashHit = Animator.StringToHash("Hit");
    private static readonly int hashDie = Animator.StringToHash("Die");

    private void Start()
    {
        _monsterTr = transform; // GetComponent<Transform>();
        _playerTr = GameObject.FindGameObjectWithTag("PLAYER")?.transform;
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _collider = GetComponent<CapsuleCollider>();
        
		_ws = new WaitForSeconds(0.3f);

        StartCoroutine(CheckMonsterState());
        StartCoroutine(MonsterAction());
    }

    private IEnumerator CheckMonsterState()
    {
        while (!IsDead)
        {
            if (_state == State.Die) yield break;
            
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
                    _agent.isStopped = true;
                    _animator.SetBool(hashIsTrace, false);
                    break;
                
                case State.Trace:
                    _agent.SetDestination(_playerTr.position);
                    _agent.isStopped = false; // _agent.Resume();
                    _animator.SetBool(hashIsAttack, false);
                    _animator.SetBool(hashIsTrace, true);
                    break;
                
                case State.Attack:
                    _agent.isStopped = true;
                    _animator.SetBool(hashIsAttack, true);
                    break;
                
                case State.Die:
                    IsDead = true;
                    _agent.isStopped = true;
                    _animator.SetTrigger(hashDie);
                    _collider.enabled = false;
                    break;
            }
            yield return _ws;
        }
    }

    public void TakeDamage(float damage)
    {
        _hp -= damage;

        if (_hp <= 0)
        {
            _state = State.Die;
        }
    }

    private void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.CompareTag("BULLET"))
        {
            TakeDamage(25f);
            _animator.SetTrigger(hashHit);
            
            Destroy(coll.gameObject);
        }
    }
}
