using UnityEngine;

public class Camera1Pessoa : MonoBehaviour
{
    [Header("Sensibilidade/Frequência e Amplitude")]
    [Range(0f, 300f)]
    public float mouseSens;

    [Range(0f, 10f)]
    public float bobFrequencyY, bobAmplitudeY, bobFrequencyX, bobAmplitudeX;

    [Header("Player")]
    public Transform player;
    public Transform cameraHolder;
    public CharacterController controller;

    [Header("Rotação X e Y")]
    private float xRotation, yRotation;

    [Header("Outros")]
    private Vector3 posicaInicial;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        posicaInicial = cameraHolder.localPosition;
    }
    private void Update()
    {
        RotacaoCamera();
        BalancarCabeca();
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

    private void BalancarCabeca()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 playerInput = new Vector3(horizontal, 0f, vertical);

        Vector3 pos = Vector3.zero;

        if (playerInput.magnitude > 0.1f && controller.isGrounded)
        {
            pos.y += Mathf.Sin(Time.time * bobFrequencyY) * bobAmplitudeY;
            pos.x += Mathf.Cos(Time.time * bobFrequencyX / 2) * bobAmplitudeX * 2;

            Vector3 bobOffset = new Vector3(pos.x, pos.y, 0f);
            cameraHolder.localPosition = Vector3.Lerp(cameraHolder.localPosition, posicaInicial + bobOffset, Time.deltaTime * 10);
        }
        else
        {
            ResetarPosicao();
        }
    }

    private void ResetarPosicao()
    {
        if (cameraHolder.localPosition == posicaInicial) return;
        cameraHolder.localPosition = Vector3.Lerp(cameraHolder.localPosition, posicaInicial, Time.deltaTime * 5);
    }
}
