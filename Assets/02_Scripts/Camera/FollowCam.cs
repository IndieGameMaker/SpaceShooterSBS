using UnityEngine;

public class FollowCam : MonoBehaviour
{
    public Transform target;
    public float distance = 8f;
    public float height = 3f;

    private void LateUpdate()
    {
        transform.position = target.position + (-target.forward * distance) + (Vector3.up * height); 
    }
    
}
