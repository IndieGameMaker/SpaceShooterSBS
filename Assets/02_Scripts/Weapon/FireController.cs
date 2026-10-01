using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class FireController : MonoBehaviour
{
    [SerializeField] private Transform _firePos;
    [SerializeField] private GameObject _bulletPrefab;

    [SerializeField] private InputActionReference _fireAction;

    private MeshRenderer _muzzleFlash;

    private void Start()
    {
        _muzzleFlash = _firePos.GetComponentInChildren<MeshRenderer>();
        _muzzleFlash.enabled = false;
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
    }

    private IEnumerator ShowMuzzleFlash()
    {
        _muzzleFlash.enabled = true;
        yield return new WaitForSeconds(0.2f);
        _muzzleFlash.enabled = false;
    }

    // private void Update()
    // {
    //     if (Input.GetMouseButtonDown(0))
    //     {
    //         Debug.Log("Fire");
    //     }
    // }
}