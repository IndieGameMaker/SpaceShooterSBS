using UnityEngine;

public class Barrel : MonoBehaviour
{
    [SerializeField] private int _hitCount = 0;

    private void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.CompareTag("BULLET"))
        {
            //_hitCount++;
            if (++_hitCount == 3)
            {
                ExplosionBarrel();
            }
        }
    }

    private void ExplosionBarrel()
    {
        // 물리엔진 런타임 적용
        var rb = this.gameObject.AddComponent<Rigidbody>();
        rb.AddForce(Vector3.up * 1000f, ForceMode.Impulse);
    }
}
