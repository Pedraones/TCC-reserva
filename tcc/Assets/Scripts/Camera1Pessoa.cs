using UnityEngine;

public class Camera1Pessoa : MonoBehaviour
{
    [Header("Sensibilidade")]
    [Range(0f, 300f)]
    public float mouseSens;

    [Header("Player")]
    public Transform player;

    [Header("Rotação X e Y")]
    private float xRotation, yRotation;
    private void Update()
    {
        RotacaoCamera();
    }

    private void RotacaoCamera()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;

        xRotation -= mouseY;
        yRotation += mouseX;

        xRotation = Mathf.Clamp(xRotation, -80f, 80f);
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0f);
        player.localRotation = Quaternion.Euler(0f, yRotation, 0f);
    }
}
