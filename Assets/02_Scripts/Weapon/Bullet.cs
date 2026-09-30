using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _force = 800.0f;

    private Rigidbody _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        // 로컬 좌표계 기준으로 Force를 적용
        _rb.AddRelativeForce(Vector3.forward * _force); // 800 뉴튼
    }
}
