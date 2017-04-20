using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class PlaneControls : MonoBehaviour
    {
        public float MaxSpeed;
        public float Acceleration;
        public float Durability = 1;
        public float Maneuverability;
        [Tooltip("Setting this doesn't affect anything, it's just used to show current speed in editor")]
        public float CurrentSpeed;
        [Tooltip("Amount of force to raise or lower the nose. Values in range -1 .. 1"), Range(-1.0f, 1.0f)]
        public float PitchTorque;

        public Slider ThrottleSlider;

        private Rigidbody2D _rb;
        private float _speed;
        private float throttle;
        private float lastInputThrottleValue;
        private float lastInputPitchValue;

        public float Throttle
        {
            get { return throttle; }
            set { throttle = value; }
        }
        public float Pitch
        {
            get { return PitchTorque; }
            set { PitchTorque = value; }
        }
        void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
        }
        void Update()
        {
            float throttle = Input.GetAxis("Horizontal");
            float pitch = Input.GetAxis("Vertical");

            if (throttle > 0.0f && throttle > lastInputThrottleValue)
            {
                this.throttle += throttle;
                if (this.throttle > 1)
                {
                    this.throttle = 1;
                }
                ThrottleSlider.value = this.throttle;
            } else if (throttle < 0.0f && throttle < lastInputThrottleValue)
            {
                this.throttle -= throttle * -1;
                if (this.throttle < 0) this.throttle = 0;
                ThrottleSlider.value = this.throttle;
            }
            lastInputThrottleValue = throttle;
            if (pitch > 0.0f && pitch > lastInputPitchValue)
            {
                this.Pitch += pitch;
                if (this.Pitch > 1)
                {
                    this.Pitch = 1;
                }
            } else if (pitch < 0.0f && pitch < lastInputPitchValue)
            {
                this.Pitch -= pitch*-1 ;
                if (this.Pitch < -1) this.Pitch = -1;
            }
            lastInputPitchValue = pitch;
            if (Throttle < 0.25f) // going too slow, plane starts to drop
            {
                _rb.gravityScale = 1 * (0.25f - Throttle);
            }
            //todo if over 0.9, plane goes up
            else
            {
                _rb.gravityScale = 0;
            }

            /*else
            {
                if (_rb.angularVelocity > 0.01f)
                {
                    _rb.angularVelocity /= 2;
                }
                else
                {
                    _rb.angularVelocity = 0;
                }

            }*/

            _rb.AddTorque(PitchTorque * Maneuverability, ForceMode2D.Force);
        }

        private void FixedUpdate()
        {
            CurrentSpeed = MaxSpeed * Throttle;
            _rb.AddRelativeForce(Vector2.up * MaxSpeed * Throttle, ForceMode2D.Force);
        }

        public float GetSpeed()
        {
            return Throttle;
        }
    }

}