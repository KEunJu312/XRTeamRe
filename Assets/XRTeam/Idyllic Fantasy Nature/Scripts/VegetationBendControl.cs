using UnityEngine;
using Unity.XR.CoreUtils; // XR Origin 탐색용 네임스페이스

namespace IdyllicFantasyNature
{
    [ExecuteInEditMode]
    public class VegetationBendControl : MonoBehaviour
    {
        [SerializeField] private bool _enableBendFeature = false;

        [Tooltip("The origin where the impact on the object starts")]
        [SerializeField] private Transform _bendOrigin;

        [Range(0.3f, 1)]
        [Tooltip("object starts to bend when the player is at a certain distance")]
        [SerializeField] private float _startBendRange = 0.5f;

        [Range(0, 1)]
        [SerializeField] private float _bendStrength = 0.5f;

        [Tooltip("material of the vegetation objects")]
        [SerializeField] private Material[] _material;

        // current world space position of the bending object 
        private Vector3 _currentBendPosition;

        private void Start()
        {
            // _bendOrigin이 비어있으면 XR 카메라 자동 연결
            if (_bendOrigin == null)
            {
#if UNITY_2023_1_OR_NEWER
                var xrOrigin = Object.FindFirstObjectByType<XROrigin>();
#else
                var xrOrigin = Object.FindObjectOfType<XROrigin>();
#endif
                if (xrOrigin != null && xrOrigin.Camera != null)
                {
                    _bendOrigin = xrOrigin.Camera.transform;
                }
                else
                {
                    Debug.LogWarning("VegetationBendControl: XROrigin 또는 카메라를 찾을 수 없습니다.");
                }
            }
        }

        private void Update()
        {
            if (_enableBendFeature && _bendOrigin != null)
            {
                MoveOnVegetation();
            }
        }

        private void OnValidate()
        {
            BendSettings();
        }

        /// <summary>
        /// The material gets the object position to know when to bend.
        /// Only updates when player moves.
        /// </summary>
        void MoveOnVegetation()
        {
            if (_currentBendPosition != _bendOrigin.position)
            {
                foreach (var mat in _material)
                {
                    if (mat != null)
                        mat.SetVector("_Player_Position", _bendOrigin.position);
                }
                _currentBendPosition = _bendOrigin.position;
            }
        }

        /// <summary>
        /// The material gets the bend settings.
        /// </summary>
        void BendSettings()
        {
            foreach (var mat in _material)
            {
                if (mat != null)
                {
                    mat.SetFloat("_Bend_Strength", _bendStrength);
                    mat.SetFloat("_Start_Bend_Range", _startBendRange);
                }
            }
        }
    }
}


