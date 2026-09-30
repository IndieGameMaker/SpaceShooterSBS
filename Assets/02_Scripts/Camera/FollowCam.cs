using UnityEngine;

public class FollowCam : MonoBehaviour
{
    public Transform target;
    [Range(3f, 15f)]
    public float distance = 8f;
    [Range(-3f, 10f)]
    public float height = 3f;
    [Range(-3f, 5f)] 
    public float targetYOffset = 1.5f;
    [Range(-3f, 3f)]
    public float targetXOffset = 0f;

    private void LateUpdate()
    {
        Vector3 targetPos = target.position + (Vector3.up * targetYOffset) + (target.right * targetXOffset);
        transform.position = targetPos + (-target.forward * distance) + (Vector3.up * height); 
        transform.LookAt(targetPos);
    }
    
}

/* Collision Detection Option
 * Discrete : 이산 (값이 딱 떨어진다.) FixedUpdate 0.02sec
 * Continues : CCD 지속적으로 충돌여부를 검출 static (고정된 물체와의 충돌여부 검출)
 * Continues Dynamic : 가장 물리연산 부하가 큼
 * Continues Speculative : Continues Dynamic 개선 버전
 */
