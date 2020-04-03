
using UnityEngine;
[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed = 10f;
    [SerializeField]
    private float lookSens = 3;

    public Transform Spawnpoint;
    public GameObject Prefab;

    private PlayerMotor motor;

     void Start()
    {
        motor = GetComponent<PlayerMotor>();
    }

    void Update()
    {
        //Calculate movement velocity as a 3D vector
        float _xMov = Input.GetAxisRaw("Horizontal");
        float _zMov = Input.GetAxisRaw("Vertical");

        Vector3 _movHorizontal = transform.right * _xMov;
        Vector3 _movVertical = transform.forward * _zMov;
        //Final movement vector
        Vector3 _velocity = (_movHorizontal + _movVertical).normalized * speed;

        //Apply movement
        motor.Move(_velocity);

        //Calculate rotation as 3D vector: this allows player to turn around
        float _yRot = Input.GetAxisRaw("Mouse X");

        Vector3 _rotation = new Vector3(0f, _yRot, 0f) * lookSens;

        //apply rotation
        motor.Rotate(_rotation);

        //Calculate camera rotation as 3D vector: this allows player to turn around
        float _xRot = Input.GetAxisRaw("Mouse Y");

        Vector3 _cameraRotation = new Vector3(_xRot, 0f, 0f) * lookSens;

        //apply camera rotation
        motor.RotateCamera(_cameraRotation);
    }

    private void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            Instantiate(Prefab, Spawnpoint.position, Spawnpoint.rotation);

            other.gameObject.SetActive(false);
        }
    }


}
