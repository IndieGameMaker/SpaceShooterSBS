using UnityEngine;

public class Barrel : MonoBehaviour
{
    [SerializeField] private int _hitCount = 0;
    [SerializeField] private GameObject _expEffect;
    [SerializeField] private AudioClip _expSFX;
    
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
        
        // 폭발효과 생성한 후 5초에 폭발 프리팹을 소멸
        var obj = Instantiate(_expEffect, transform.position, Quaternion.identity);
        Destroy(obj, 5f);
        
        // 폭발음 재생
        GetComponent<AudioSource>().PlayOneShot(_expSFX, 0.2f);
    }
}
