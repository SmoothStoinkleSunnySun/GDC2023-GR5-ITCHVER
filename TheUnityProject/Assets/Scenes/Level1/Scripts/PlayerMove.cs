using UnityEngine;
using UnityEngine.Serialization;

namespace Scenes.Level1.Scripts
{
    public class PlayerMove : MonoBehaviour
    {
        //some code from https://www.youtube.com/watch?v=f473C43s8nE
        [TextArea][SerializeField] string notes;

        [Header("Public")]
        public float groundDrag;
        public float playerHeight;
        [FormerlySerializedAs("Ground")] public LayerMask ground;
        public Rigidbody rb;
        public bool AllowMovement { get; set; } = true; //this variable decides whether or not the player can move

        [Header("Private")]
        [SerializeField] float moveSpeed;
        [SerializeField] Animator anim;

        //not visible in inspector

        Vector3 _moveD; //moveDirection
        float _moveX;
        float _moveY;
        bool _grounded;
        private static readonly int AnimMoveX = Animator.StringToHash("AnimMoveX");
        private static readonly int AnimMoveY = Animator.StringToHash("AnimMoveY");

        // Update is called once per frame
        void Update()
        {
            //check if on ground
            _grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, ground);

            if (AllowMovement)
            {
                ProcessInputs();
                SpeedControl();
            }

            //change drag
            if (_grounded)
            {
                rb.drag = groundDrag;
            }
            else
            {
                rb.drag = 0;
            }

        }

        private void FixedUpdate()
        {
            //If allowmovement is true/enabled
            if (AllowMovement)
            {
                Move();
            }
        }
        void Move()
        {
            _moveD = new Vector3(_moveX, 0, _moveY).normalized;
            rb.AddForce(10f * moveSpeed * _moveD, ForceMode.Force);
        }
        private void ProcessInputs()
        {
            _moveX = Input.GetAxisRaw("Horizontal");
            _moveY = Input.GetAxisRaw("Vertical");

            //this if statement prevents the character from going back to whatever animation is the default one when standing still
            // || and && operators explanation: https://kodify.net/csharp/if-else/if-logical-operators/
            if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
            {
                Animate();
            }
        }
        void Animate()
        {
            //from https://www.youtube.com/watch?v=nlBwNx-CKLg
            anim.SetFloat(AnimMoveX, _moveD.x);
            anim.SetFloat(AnimMoveY, _moveD.z);
        }
        private void SpeedControl()
        {
            var velocity = rb.velocity;
            Vector3 flatVel = new(velocity.x, 0, velocity.z);

            if (!(flatVel.magnitude > moveSpeed)) return;
            var limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }
}
