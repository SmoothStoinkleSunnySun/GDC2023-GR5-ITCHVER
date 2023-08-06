using UnityEngine;
using UnityEngine.Serialization;
namespace Scenes.Level1.Scripts
{
    public class PlayerMove : MonoBehaviour
    {
        //some code from https://www.youtube.com/watch?v=f473C43s8nE
        [TextArea] [SerializeField] private string notes;

        [Header("Public")] public float groundDrag;
        public float playerHeight;
        [FormerlySerializedAs("Ground")] public LayerMask ground;
        public Rigidbody rb;
        public bool AllowMovement { get; set; } = true; //this variable decides whether or not the player can move
        public Animator anim;

        [Header("Private")]
        [SerializeField] private float moveSpeed;

        //not visible in inspector

        private Vector3 _moveD; //moveDirection
        private float _moveX;
        private float _moveY;
        private bool _grounded;
        private static readonly int AnimMoveX = Animator.StringToHash("AnimMoveX");
        private static readonly int AnimMoveY = Animator.StringToHash("AnimMoveY");
        private static readonly int BreathSpeed = Animator.StringToHash("BreathSpeed");
        private float _breathingSpeed;

        private void Start()
        {
            anim.SetFloat(AnimMoveX, 0);
            anim.SetFloat(AnimMoveY, 1);
        }

        private void Update()
        {
            //check if on ground
            _grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, ground);

            if (AllowMovement)
            {
                ProcessInputs();
                SpeedControl();
            }

            // Hello dear inexperienced c# programmer,
            // you can put in methods like this that are only called by 1 other method to make your
            // code cleaner to read. No, these aren't called every Update(), only if the if statement allows it
            void ProcessInputs()
            {
                _moveX = Input.GetAxisRaw("Horizontal");
                _moveY = Input.GetAxisRaw("Vertical");
            }
            void SpeedControl()
            {
                var velocity = rb.velocity;
                Vector3 flatVel = new(velocity.x, 0, velocity.z);

                if (!(flatVel.magnitude > moveSpeed)) return;
                var limitedVel = flatVel.normalized * moveSpeed;
                rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
            }
            
            //change drag
            if (_grounded)
                rb.drag = groundDrag;
            else
                rb.drag = 0;

            if (AllowMovement && (_moveX != 0 || _moveY != 0)) Animate();
            //This was previously in ProcessInputs, then in FixedUpdate, now here
            // Originally did not include extra movement direction calculation
            void Animate()
            {
                _moveD = new Vector3(_moveX, 0, _moveY).normalized;
                anim.Play("Walk_new", 3);
                anim.SetFloat(AnimMoveX, _moveD.x);
                anim.SetFloat(AnimMoveY, _moveD.z);
            }
        }

        private void FixedUpdate()
        {
            //If AllowMovement is true/enabled
            if (AllowMovement) Move();
            
            void Move()
            {
                _moveD = new Vector3(_moveX, 0, _moveY).normalized;
                rb.AddForce(10f * moveSpeed * _moveD, ForceMode.Force);
            }
            
            //breathing speed control
            if (AllowMovement && (_moveX != 0 || _moveY != 0))
            {
                if (_breathingSpeed < 4.5) _breathingSpeed += 0.015f;
            }
            else
            {
                anim.SetFloat(BreathSpeed, _breathingSpeed);
                if (_breathingSpeed > 1 && _breathingSpeed != 1) _breathingSpeed -= 0.005f;
                else _breathingSpeed = 1; 
            }
        }
    }
}