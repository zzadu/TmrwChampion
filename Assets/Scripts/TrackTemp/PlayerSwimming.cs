using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace UnityStandardAssets.Characters.FirstPerson
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(AudioSource))]
    public class PlayerSwimming : MonoBehaviour
    {
        /*[SerializeField] public float m_SwimSpeed;*/     // eidt
        public float m_SwimSpeed;
        [SerializeField] [Range(0f, 1f)] private float m_RunstepLenghten;
        [SerializeField] private float m_StickToGroundForce;
        [SerializeField] private float m_GravityMultiplier;
        [SerializeField] private MouseLook m_MouseLook;
        [SerializeField] private bool m_UseFovKick;
        [SerializeField] private FOVKick m_FovKick = new FOVKick();
        [SerializeField] private bool m_UseHeadBob;
        [SerializeField] private CurveControlledBob m_HeadBob = new CurveControlledBob();
        [SerializeField] private LerpControlledBob m_JumpBob = new LerpControlledBob();
        [SerializeField] private float m_StepInterval;
        [SerializeField] private AudioClip m_SwimSound;      // edit

        private Camera m_Camera;
        private float m_YRotation;
        private Vector2 m_Input;
        private Vector3 m_MoveDir = Vector3.zero;
        private CharacterController m_CharacterController;
        private CollisionFlags m_CollisionFlags;
        private bool m_PreviouslyGrounded;
        private Vector3 m_OriginalCameraPosition;
        private float m_StepCycle;
        private float m_NextStep;
        private AudioSource m_AudioSource;

        // swim 초기화 & 설정
        float waterHeight;
        GameObject FPStoSwim;
        GameObject Parent;

        // Use this for initialization
        private void Start()
        {
            m_CharacterController = GetComponent<CharacterController>();
            m_Camera = Camera.main;
            m_OriginalCameraPosition = m_Camera.transform.localPosition;
            m_FovKick.Setup(m_Camera);
            m_HeadBob.Setup(m_Camera, m_StepInterval);
            m_StepCycle = 0f;
            m_NextStep = m_StepCycle / 2f;
            m_AudioSource = GetComponent<AudioSource>();
            m_MouseLook.Init(transform, m_Camera.transform);

            // 수영 객체 관련
            FPStoSwim = GameObject.FindGameObjectWithTag("SwimHelper");
        }

        // Update is called once per frame
        private void Update()
        {
            RotateView();

            if (!m_PreviouslyGrounded && m_CharacterController.isGrounded)
            {
                StartCoroutine(m_JumpBob.DoBobCycle());
                m_MoveDir.y = 0f;
            }
            if (!m_CharacterController.isGrounded && m_PreviouslyGrounded)
            {
                m_MoveDir.y = 0f;
            }

            m_PreviouslyGrounded = m_CharacterController.isGrounded;
        }

        private void OnTriggerEnter(Collider other)
        {
            // 수영모드로 세팅
            if (other.CompareTag("Water"))
            {
                Debug.Log("Player in Water");

                waterHeight = other.GetComponent<Transform>().position.y;

                // 객체 파일 위치 (계층 구조 이용)
                Parent = FPStoSwim.transform.parent.gameObject;
                Debug.Log(Parent);
                transform.parent = Parent.transform;
                Debug.Log(gameObject);

                // 객체 좌표 설정
                Parent.transform.localPosition = new Vector3(transform.position.x, waterHeight - 1.3f, transform.position.z);
                FPStoSwim.transform.localPosition = Vector3.zero;
                gameObject.transform.localPosition = new Vector3(0, 1, 0);
            }
        }

        private void FixedUpdate()
        {
            float speed;
            GetInput(out speed);
            // always move along the camera forward as it is the direction that it being aimed at
            Vector3 desiredMove = transform.forward * m_Input.y + transform.right * m_Input.x;

            // get a normal for the surface that is being touched to move along it
            RaycastHit hitInfo;
            Physics.SphereCast(transform.position, m_CharacterController.radius, Vector3.down, out hitInfo,
                                m_CharacterController.height / 2f, Physics.AllLayers, QueryTriggerInteraction.Ignore);
            desiredMove = Vector3.ProjectOnPlane(desiredMove, hitInfo.normal).normalized;

            m_MoveDir.x = desiredMove.x * speed;
            m_MoveDir.z = desiredMove.z * speed;

            if (m_CharacterController.isGrounded)
            {
                m_MoveDir.y = -m_StickToGroundForce;
            }
            else
            {
                m_MoveDir += Physics.gravity * m_GravityMultiplier * Time.fixedDeltaTime;
            }
            m_CollisionFlags = m_CharacterController.Move(m_MoveDir * Time.fixedDeltaTime);

            m_MouseLook.UpdateCursorLock();

            if (Input.GetKey(KeyCode.A))
            {
                ProgressStepCycle(speed);
                // edit) fps roatate
            }
            else if (Input.GetKey(KeyCode.D))
            {
                ProgressStepCycle(speed);
                // edit) fps roatate
            }
            else if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
            {
                StopCycleAudio();
                // edit) fps roatate
            }

            UpdateCameraPosition(speed);
        }

        private void RotateView()
        {
            m_MouseLook.LookRotation(transform, m_Camera.transform);
        }

        private void GetInput(out float speed)
        {
            // Read input
            float horizontal = CrossPlatformInputManager.GetAxis("Horizontal");
            float vertical = CrossPlatformInputManager.GetAxis("Vertical");

            // set the desired speed to be walking or running
            speed = m_SwimSpeed;
            m_Input = new Vector2(m_Input.x, horizontal > 0 ? horizontal : -horizontal);

            // normalize input if it exceeds 1 in combined length:
            if (m_Input.sqrMagnitude > 1)       // edit를 하면, 2로 바꾸면, 더 느리나?
            {
                m_Input.Normalize();
            }

            // handle speed change to give an fov kick
            // only if the player is going to a run, is running and the fovkick is to be used
            if (m_UseFovKick && m_CharacterController.velocity.sqrMagnitude > 0)
            {
                StopAllCoroutines();
                StartCoroutine(m_FovKick.FOVKickUp());
            }
        }

        private void ProgressStepCycle(float speed)
        {
            if (m_CharacterController.velocity.sqrMagnitude > 0 && (m_Input.x != 0 || m_Input.y != 0))
            {
                m_StepCycle += (m_CharacterController.velocity.magnitude + (speed * 1f)) *
                                Time.fixedDeltaTime;
            }
            if (!(m_StepCycle > m_NextStep))
            {
                return;
            }
            m_NextStep = m_StepCycle + m_StepInterval;

            PlayCycleAudio();       // edit
        }

        private void UpdateCameraPosition(float speed)
        {
            Vector3 newCameraPosition;
            if (!m_UseHeadBob)
            {
                return;
            }
            if (m_CharacterController.velocity.magnitude > 0 && m_CharacterController.isGrounded)
            {
                m_Camera.transform.localPosition =
                    m_HeadBob.DoHeadBob(m_CharacterController.velocity.magnitude +
                                        (speed * 1f));
                newCameraPosition = m_Camera.transform.localPosition;
                newCameraPosition.y = m_Camera.transform.localPosition.y - m_JumpBob.Offset();      // cycle 버전: newCameraPosition.y = 2f;
            }
            else
            {
                newCameraPosition = m_Camera.transform.localPosition;
                newCameraPosition.y = m_Camera.transform.localPosition.y - m_JumpBob.Offset();      // cycle 버전: newCameraPosition.y = 2f;
            }
            m_Camera.transform.localPosition = newCameraPosition;
        }

        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            Rigidbody body = hit.collider.attachedRigidbody;
            //dont move the rigidbody if the character is on top of it
            if (m_CollisionFlags == CollisionFlags.Below)
            {
                return;
            }

            if (body == null || body.isKinematic)
            {
                return;
            }
            body.AddForceAtPosition(m_CharacterController.velocity * 0.1f, hit.point, ForceMode.Impulse);
        }

        private void PlayCycleAudio()
        {
            if (!m_AudioSource.isPlaying)
            {
                m_AudioSource.clip = m_SwimSound;
                m_AudioSource.Play();
            }
        }

        private void StopCycleAudio()
        {
            if (m_AudioSource.isPlaying)
            {
                m_AudioSource.Pause();
            }
        }
    }
}


