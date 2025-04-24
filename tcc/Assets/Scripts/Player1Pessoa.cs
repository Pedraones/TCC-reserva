using UnityEngine;

public class Player1Pessoa : MonoBehaviour
{
    [Header("Player")]
    public CharacterController controller;
    public Animator animator;
    public Transform detector;
    public LayerMask cenario;

    public float gravidade, alturaPulo, verticalVel;
    private bool estaNoChao;

    [Range(0f, 50f)]
    public float velAndar;

    [Range(0f, 50f)]
    public float velCorrer, velPulando;

    private float velAtual;

    [Header("Camera")]
    public Transform cameraHolder;

    [Header("Outros")]
    private Vector3 playerInput;

    private void Update()
    {
        Movimentacao();
        Pulo();

        Vector3 movimentoFinal = playerInput * velAtual;
        movimentoFinal.y = verticalVel;

        controller.Move(movimentoFinal * Time.deltaTime);
    }

    private void Movimentacao()
    {
        //input e movimentação do player
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        playerInput = new Vector3(horizontal, 0f, vertical);
        playerInput = transform.TransformDirection(playerInput);

        bool correndo = Input.GetKey(KeyCode.LeftShift);
        velAtual = correndo ? velCorrer : velAndar;

        ChecarAnimacoes(correndo, playerInput);
    }

    private void Pulo()
    {
        //checar se esta no chão
        estaNoChao = Physics.CheckSphere(detector.position, 0.3f, cenario);
        velAtual = estaNoChao ? velAtual : velPulando;

        //aplica pulo de acordo com a altura do pulo
        if (Input.GetKeyDown(KeyCode.Space) && estaNoChao)
        {
            verticalVel = Mathf.Sqrt(-2f * alturaPulo * gravidade);
            animator.SetTrigger("Pular");
        }

        if (!estaNoChao && verticalVel < -4)
        {
            animator.CrossFade("Falling To Landing", 0.1f);
        }

        if (estaNoChao && verticalVel < 0)
        {
            verticalVel = -1f;
        }

        //aplica gravidade constantemente
        verticalVel += gravidade * Time.deltaTime;

        animator.SetBool("noChao", true);
    }

    private void ChecarAnimacoes(bool correndo, Vector3 playerInput)
    {
        //verificação das condições das animações
        if (playerInput.magnitude > 0.1f)
        {
            animator.SetBool("Andar", true);
            animator.SetBool("Correr", correndo);
        }

        else
        {
            animator.SetBool("Andar", false);
            animator.SetBool("Correr", false);
        }
    }
}

