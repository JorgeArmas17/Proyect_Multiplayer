using UnityEngine;
using UnityEngine.UI;

using System.Collections;
using TMPro;
using Com.ArmasJorge.BattleKnights;

namespace Com.ArmasJorge.BattleKnights
{
    public class Puntos : MonoBehaviour
    {
        #region Private Fields

        [Tooltip("UI Text to display Player's Name")]
        [SerializeField]
        public static int puntos = 0;
        public Text impPuntos;

        private PlayerManager target;
        [Tooltip("Pixel offset from the player target")]
        [SerializeField]
        private Vector3 screenOffset = new Vector3(0f, 30f, 0f);
        float characterControllerHeight = 0f;
        Transform targetTransform;
        Renderer targetRenderer;
        CanvasGroup _canvasGroup;
        Vector3 targetPosition;
        #endregion

        #region MonoBehaviour Callbacks
        void Update()
        {
            if (target == null)
            {
                Destroy(this.gameObject);
                return;
            }
            if (puntos != null)
            {
                impPuntos.text = target.puntos.ToString();
            }

        }
        void Awake()
        {
            this.transform.SetParent(GameObject.Find("Canvas").GetComponent<Transform>(), false);
            _canvasGroup = this.GetComponent<CanvasGroup>();
        }
        void LateUpdate()
        {
            if (targetRenderer != null)
            {
                this._canvasGroup.alpha = targetRenderer.isVisible ? 1f : 0f;
            }

            if (targetTransform != null)
            {
                targetPosition = targetTransform.position;
                targetPosition.y += characterControllerHeight;
                this.transform.position = Camera.main.WorldToScreenPoint(targetPosition) + screenOffset;
            }
        }
        #endregion

        #region Public Methods
        public void SetTarget(PlayerManager _target)
        {
            if (_target == null)
            {
                Debug.LogError("<Color=Red><a>Missing</a></Color> PlayMakerManager target for PlayerUI.SetTarget.", this);
                return;
            }
            target = _target;
            targetTransform = this.target.GetComponent<Transform>();
            targetRenderer = this.target.GetComponent<Renderer>();
            CharacterController characterController = _target.GetComponent<CharacterController>();
            if (characterController != null)
            {
                characterControllerHeight = characterController.height;
            }
            
        }
        #endregion

    }
}