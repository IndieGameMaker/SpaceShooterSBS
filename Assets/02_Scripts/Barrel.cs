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
        // rb.AddForce(Vector3.up * 30f, ForceMode.Impulse);

        Vector3 point = Random.insideUnitSphere; // (0, 0, 0) ~ (1, 1, 1)
        
        Vector2 point2D = Random.insideUnitCircle; // (0, 0) ~ (1, 1)

        rb.AddForceAtPosition(Vector3.up * 30f, transform.position + new Vector3(point2D.x, 0, point2D.y) , ForceMode.Impulse);
        
        // 3초후에 베럴을 사라지도록 ... 삭제 제거
        Destroy(gameObject, 3f);
    }
}
