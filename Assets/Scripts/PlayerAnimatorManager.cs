using UnityEngine;
using System.Collections;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Com.ArmasJorge.BattleKnights
{
    public class PlayerAnimatorManager : MonoBehaviourPun
    {

        #region Private Fields
        [SerializeField]
        public Rigidbody rigidbody;
        public float velocidadMovimiento = 5.0f;
        public float velocidadRotacion = 200.0f;
        private bool salto = false;
        private bool ataque = false;
        public Text impPuntos;       
        private static int puntos = 0;

        public float fuerzaSalto;
        public Animator animator;
        public float x, y;
        #endregion
        #region MonoBehaviour Callbacks

        void Start()
        {
            animator = GetComponent<Animator>();
            if (!animator)
            {
                Debug.LogError("PlayerAnimatorManager is Missing Animator Component", this);
            }
        }

        void Update()
        {
            if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
            {
                return;
            }
            if (!animator)
            {
                return;
            }
            x = Input.GetAxis("Horizontal");
            y = Input.GetAxis("Vertical");

            transform.Rotate(0, x * Time.deltaTime * velocidadRotacion, 0);
            transform.Translate(0, 0, y * Time.deltaTime * velocidadMovimiento);

            animator.SetFloat("VelX", x);
            animator.SetFloat("VelY", y);

            animator.SetBool("estaAtacando", false);
            ataque = false; 


            if (Input.GetKeyDown(KeyCode.Space) && salto == false)
            {
                animator.SetBool("estaSaltando", true);
                rigidbody.AddForce(new Vector3(0, fuerzaSalto, 0));
                salto = true;
            }

            if (Input.GetKeyDown(KeyCode.X) && ataque == false)
            {
                animator.SetBool("estaAtacando", true);
                ataque = true;
            }
            

        }
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "plataforma")
            {
                animator.SetBool("estaSaltando", false);
                salto = false;
            }
           
            if (collision.gameObject.tag == "espada1")
            {
                puntos += 10;
                impPuntos.text = puntos.ToString();
            }
            if (collision.gameObject.tag == "espadaEspecial")
            {
                //SceneManager.LoadScene(4);
            }
        }
            #endregion

        }
}