using UnityEngine;

namespace BeatEmUpTutorial
{
    public class CameraSeguidora : MonoBehaviour
    {
        /* Distancia em X que o player pode andar antes da camera seguir */
        [SerializeField] private float xMargin = 0f;

        /* O quanto suave sera a movimentacao da camera caso o jogador ande no eixo X / Y*/
        [SerializeField] private float xSmoothness = 8f;
        [SerializeField] private float ySmoothness = 8f;

        /* Os valores minimos / maximos de X e Y que a câmera pode ter */
        [SerializeField] private Vector2 maximoXY;
        [SerializeField] private Vector2 minimoXY;

        private Transform transformPlayer;

        public Vector2 MaximoXY { get => maximoXY; set => maximoXY = value; }
        public Vector2 MinimoXY { get => minimoXY; set => minimoXY = value; }

        private void Start()
        {
            transformPlayer = GameObject.FindGameObjectWithTag("Player").transform;
        }

        private void Update()
        {
            FollowPlayer();
        }

        private bool CheckXMargin()
        {
            /* Desenha posicao da camera para o play */
            Debug.DrawRay(transform.position, transformPlayer.position, Color.red);
            return (transform.position.x - transformPlayer.position.x) < xMargin;
        }

        private void FollowPlayer()
        {
            /* Por padrao, os eixos X e Y do alvo da camera sao dela mesma */
            float targetX = transform.position.x;

            /* Se o jogador se mover depois da margem em X */
            if (CheckXMargin())
            {
                /* o alvo em X deve interpolar entre o position.x da câmera e position.x do jogador */
                targetX = Mathf.Lerp(transform.position.x, transformPlayer.position.x, xSmoothness * Time.deltaTime);
            }

            /* Os eixos X e Y do alvo nao devem ser maiores que o limite "maximoXY" e menor que "minimoXY" */
            targetX = Mathf.Clamp(targetX, MinimoXY.x, MaximoXY.x);

            /* Atualiza a posição da camera */
            transform.position = new Vector3(targetX, transform.position.y, transform.position.z);
        }
    }
}