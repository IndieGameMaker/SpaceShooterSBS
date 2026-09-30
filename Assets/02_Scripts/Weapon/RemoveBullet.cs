using UnityEngine;

public class RemoveBullet : MonoBehaviour
{
    [SerializeField] private GameObject _sparkEffect;

    private void OnCollisionEnter(Collision coll)
    {
        // 충돌한 객체의 태그 비교
        if (coll.gameObject.CompareTag("BULLET"))
        {
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
