using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class FireController : MonoBehaviour
{
    [SerializeField] private Transform _firePos;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private AudioClip _fireSFX;
    
    [SerializeField] private InputActionReference _fireAction;
    
    private MeshRenderer _muzzleFlash;
    private Light _fireLight;
    private AudioSource _audio;

    private void Start()
    {
        _muzzleFlash = _firePos.GetComponentInChildren<MeshRenderer>();
        _muzzleFlash.enabled = false;
        
        _fireLight = _firePos.GetComponentInChildren<Light>();
        _fireLight.intensity = 0.0f;
        
        _audio = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        _fireAction.action.performed += Fire;
        _fireAction.action.Enable();
    }

    private void OnDisable()
    {
        _fireAction.action.performed -= Fire;
        _fireAction.action.Disable();
    }

    private void Fire(InputAction.CallbackContext ctx)
    {
        // Instantiate (생성할객체, 좌표, 각도, [부모객체])
        Instantiate(_bulletPrefab, _firePos.position, _firePos.rotation);

        StartCoroutine(ShowMuzzleFlash());
        
        _audio.PlayOneShot(_fireSFX, 0.2f);
    }

    private IEnumerator ShowMuzzleFlash()
    {
        // Texture Offset 변경
        // Random.Range(int, int)       Random.Range(0, 10) => 0, 1, 2,..., 9
        // Random.Range(float, float)   Random.Range(0.0f, 10.0f) => 0.0f, ... , 10.0f
        // var aa = Random.Range(0, 2) * 0.5f; // 0, 0.5f
        
        // (0, 0) , (0, 0.5) , (0.5, 0) , (0.5, 0.5)
        Vector2 offset = new Vector2(Random.Range(0,2), Random.Range(0, 2)) * 0.5f;
        _muzzleFlash.material.mainTextureOffset = offset;
        
        // Scale 변경
        float scale = Random.Range(0.8f, 2.0f);
        _muzzleFlash.transform.localScale = Vector3.one * scale; //new Vector3(scale, scale, scale); 
        
        // 회전 처리
        float angle = Random.Range(0, 360);
        _muzzleFlash.transform.localRotation = Quaternion.Euler(0, 0, angle);

        _fireLight.intensity = scale;
        
        _muzzleFlash.enabled = true;
        yield return new WaitForSeconds(0.2f);
        _muzzleFlash.enabled = false;
        
        _fireLight.intensity = 0.0f;
    }
}

/* Quaternion 쿼터니언 (복소수 사차원 벡터) 사 원수 (x, y, z, w)
 *
 * - 오일러 회전 (Euler Rotation)
 * - 짐벌락 (김벌락 : Gimbal Lock)
 *
 * - Quaternion.LookRotation(벡터)
 * - Quaternion.Euler(x, y, z)
 */