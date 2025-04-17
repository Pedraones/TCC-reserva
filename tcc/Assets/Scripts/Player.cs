using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("PLAYER")]
    public CharacterController player;
    public Animator anim;
    public float vel;
    public float velRun;
    private Vector3 Direcao;

    [Header("CAMERA")]
    public Transform camera;
    private float velocidadeRotacao;
    public float suavizarCamera;



    void Update()
    {
        // Magnitude: mede o tamanho de um vetor.
        // Move: é um método do CharacterController para mover um objeto
        // Time.DeltaTime: garante que o movimento ou a ação seja constante e independente do FPS.


        float horizontal = Input.GetAxisRaw("Horizontal"); // Obtem o eixo horizontal(A, D)
        float vertical = Input.GetAxisRaw("Vertical"); // Obtem o eixo vertical(W, S)
        Direcao = new Vector3(horizontal, 0, vertical); // Obtem um Vector3 que x e z são controlados pelo jogador(WASD)

        float visao = Mathf.Atan2(Direcao.x, Direcao.z) * Mathf.Rad2Deg + camera.eulerAngles.y; // Calcula o ângulo de direção:
        float anglo = Mathf.SmoothDampAngle(transform.eulerAngles.y, visao, ref velocidadeRotacao, suavizarCamera); // Suaviza a rotação para transição suave


        Movimento(visao, anglo);
        Correr(visao, anglo);
    }



    // Método responsável pelo Movimento básico do personagem:
    private void Movimento(float visao, float anglo)
    {
        if (Direcao.magnitude >= 0.1)
        {
            // Aplica a rotação suavizada ao personagem:
            transform.rotation = Quaternion.Euler(0, anglo, 0);

            // Calcula nova direção com rotação aplicada.
            Vector3 novaDirecao = Quaternion.Euler(0, visao, 0) * Vector3.forward;


            /*          novaDirecao: É a direção utilizando o algorítimo de rotação acima.                 */
            player.Move(novaDirecao * vel * Time.deltaTime); // Move o jogador com base na velocidade
            anim.SetBool("Andar", true); // Determina se a animação(1°p - nome_Parametro) esta ativada ou não(2°p - bool).
        }


        else
        {
            anim.SetBool("Andar", false);
        }
    }


    private void Correr(float visao, float anglo)
    {
        if (Input.GetKey(KeyCode.LeftShift) && Direcao.magnitude >= 0.1f)
        {
            transform.rotation = Quaternion.Euler(0, anglo, 0);

            Vector3 novaDirecao = Quaternion.Euler(0, visao, 0) * Vector3.forward;

            player.Move(novaDirecao * velRun * Time.deltaTime);
            anim.SetBool("Correr", true);
        }

        else
        {
            anim.SetBool("Correr", false);
        }
    }
}
