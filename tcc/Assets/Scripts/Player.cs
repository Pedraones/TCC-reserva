
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class Player : MonoBehaviour
{
    [Header("PLAYER")]
    public CharacterController player;
    public Animator anim;
    public float velocidade;
    public float velocidadeRun;
    private Vector3 Direcao;

    [Header("CAMERA")]
    public Transform camera;
    public float suavizarCamera;
    private float velocidadeRotacao;

    [Header("PULO")]
    public float alturaPulo;
    public float gravidade = -19.62f;
    private Vector3 forcaY;
    public Transform Detector;
    public LayerMask layerChao;
    public bool estaNoChao;

    // Correção do Pulo:
    public bool tempoPulo;

    // Método de detecção do chão
    private void FixedUpdate()
    {
        estaNoChao = Physics.CheckSphere(Detector.position, 0.3f, layerChao);
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Direcao = new Vector3(horizontal, 0, vertical);

        float visao = Mathf.Atan2(Direcao.x, Direcao.z) * Mathf.Rad2Deg + camera.eulerAngles.y;
        float anglo = Mathf.SmoothDampAngle(transform.eulerAngles.y, visao, ref velocidadeRotacao, suavizarCamera);

        player.Move(forcaY * Time.deltaTime);

        Movimento(visao, anglo);
        Correr(visao, anglo);
        Controle();
    }

    private void Movimento(float visao, float anglo)
    {
        if (Direcao.magnitude >= 0.1f)
        {
            transform.rotation = Quaternion.Euler(0, anglo, 0);
            Vector3 novaDirecao = Quaternion.Euler(0, visao, 0) * Vector3.forward;
            player.Move(novaDirecao * velocidade * Time.deltaTime);
            anim.SetBool("Andar", true);
        }
        else
        {
            anim.SetBool("Andar", false);
        }
    }

    private void Correr(float visao, float anglo)
    {
        if (Direcao.magnitude > 0.1 && Input.GetKey(KeyCode.LeftShift))
        {
            transform.rotation = Quaternion.Euler(0, anglo, 0);
            Vector3 novaDirecao = Quaternion.Euler(0, visao, 0) * Vector3.forward;
            player.Move(novaDirecao * velocidadeRun * Time.deltaTime);
            anim.SetBool("Correr", true);
        }
        else
        {
            anim.SetBool("Correr", false);
        }
    }

    public void TempoPulo(bool liberarPulo)
    {
        tempoPulo = liberarPulo;
    }

    private void Controle()
    {
        if (Input.GetKeyDown(KeyCode.Space) & estaNoChao == true & tempoPulo == false)
        {
            anim.SetBool("Pular", true);
            forcaY.y = Mathf.Sqrt(alturaPulo * -2 * gravidade);
        }
        else
        {
            forcaY.y += gravidade * Time.deltaTime;
            anim.SetBool("Pular", false);

            if (forcaY.y < 0 && estaNoChao)
            {
                forcaY.y = -6f;
            }
        }
    }
}
