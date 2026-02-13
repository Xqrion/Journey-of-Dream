using UnityEngine;

public class ConsisentRotate : MonoBehaviour
{
    [SerializeField] private RotateAsis asis;
    [SerializeField] private float rotateSpeed = 10f;
    private void Update()
    {
        switch (asis)
        {
            case RotateAsis.X:
                //_vector3 = new Vector3(transform.rotation.x + rotateSpeed * Time.deltaTime, transform.rotation.y, transform.rotation.z);
                transform.Rotate(Vector3.right, rotateSpeed * Time.deltaTime);
                //_vector3.x %= 360f;
                break;
            case RotateAsis.Y:
                //_vector3 = new Vector3(transform.rotation.x, transform.rotation.y + rotateSpeed * Time.deltaTime, transform.rotation.z);
                //_vector3.y %= 360f;
                transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
                break;
            case RotateAsis.Z:
                //_vector3 = new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z + rotateSpeed * Time.deltaTime);
                transform.Rotate(Vector3.forward, rotateSpeed * Time.deltaTime);
                //_vector3.z %= 360f;
                break;
        }

        //transform.localEulerAngles = _vector3;
    }
}

public enum RotateAsis
{
    X ,Y ,Z
}
