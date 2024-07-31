using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float rotationSpeed = 3f;
    public float moveSpeed = 5f;
    public float zoomSpeed = 5f;
    public float minVerticalAngle = -89f;
    public float maxVerticalAngle = 89f;

    private Vector3 _lastMousePosition;
    private float _currentVerticalAngle;
    
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            _lastMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(1))
        {
            Vector3 delta = Input.mousePosition - _lastMousePosition;

            // 1. 마우스 우클릭 + 이동 = 카메라 회전
            if (!Input.GetKey(KeyCode.LeftControl) && !Input.GetKey(KeyCode.LeftShift))
            {
                // 수평 회전 (제한 없음)
                transform.Rotate(Vector3.up, delta.x * rotationSpeed * Time.deltaTime);

                // 수직 회전 (제한 있음)
                float verticalRotation = delta.y * rotationSpeed * Time.deltaTime;
                _currentVerticalAngle -= verticalRotation;
                _currentVerticalAngle = Mathf.Clamp(_currentVerticalAngle, minVerticalAngle, maxVerticalAngle);
                
                // X축 회전만 적용하고 Z축은 0으로 유지
                transform.rotation = Quaternion.Euler(_currentVerticalAngle, transform.eulerAngles.y, 0);
            }
            // 2. Ctrl + 마우스 우클릭 + 이동 = 카메라 상하 이동
            else if (Input.GetKey(KeyCode.LeftControl))
            {
                transform.Translate(Vector3.up * (delta.y * moveSpeed * Time.deltaTime), Space.World);
            }
            // 3. Shift + 마우스 우클릭 + 이동 = 카메라 전후 이동
            else if (Input.GetKey(KeyCode.LeftShift))
            {
                transform.Translate(Vector3.forward * (delta.y * zoomSpeed * Time.deltaTime));
            }

            _lastMousePosition = Input.mousePosition;
        }
    }
}
