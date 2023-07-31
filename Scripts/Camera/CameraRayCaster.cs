using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraRayCaster : MonoBehaviour
{
    private Camera _camera;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (GetRaycastHit().collider.TryGetComponent<IRayGetter>(out IRayGetter _rayGetter))
            {
                _rayGetter.SendToReceiver();
            }
        }
    }

    private RaycastHit GetRaycastHit()
    {
        Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, Mathf.Infinity);
        return hit;
    }
}
