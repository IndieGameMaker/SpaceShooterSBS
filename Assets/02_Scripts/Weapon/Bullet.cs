using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _force = 800.0f;

    private Rigidbody _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        
        _rb.AddRelativeForce(Vector3.forward * _force); // 800 뉴튼
    }
}
