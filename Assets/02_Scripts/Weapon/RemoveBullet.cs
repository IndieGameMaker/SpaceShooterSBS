using UnityEngine;

public class RemoveBullet : MonoBehaviour
{
    [SerializeField] private GameObject _sparkEffect;

    private void OnCollisionEnter(Collision coll)
    {
        // 충돌한 객체의 태그 비교
        if (coll.gameObject.CompareTag("BULLET"))
        {
            // 스파크 이펙트 발생
            // 1. 충돌정보 (Contact Point)
            ContactPoint cp = coll.GetContact(0);
            Vector3 point = cp.point;
            // 2. 법선 벡터 (Normal Vector)
            Vector3 normal = -cp.normal;
            // 벡터를 쿼터니언 타입으로 변환
            Quaternion rot = Quaternion.LookRotation(normal);
            
            // 3. Spart 생성
            var spark = Instantiate(_sparkEffect, point, rot);
            Destroy(spark, 0.4f);
            Destroy(coll.gameObject);
        }
    }
    
    
    /* 충돌 콜백 함수 (Collision Callback Function)
     * 1. IsTrigger 언체크 (강체)
     * OnCollisionEnter  -> 1 회 호출
     * OnCollisionStay   -> n 회 호출
     * OnCollisionExit   -> 1 회 호출
     *
     * 2. IsTrigger 체크 (관통)
     * OnTriggerEnter (Collider coll)
     * OnTriggerStay
     * OnTriggerExit
     */
}
