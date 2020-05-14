using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarDriving : MonoBehaviour
{
    //public float motorForce, steerForce, brakeForce;
    //public WheelCollider FL_Wheel, FR_Wheel, RL_Wheel, RR_Wheel;
    //Rigidbody rigid;
    //private void Start()
    //{
    //    rigid = GetComponent<Rigidbody>();
    //}

    //private void Update()
    //{
    //    float vertical = Input.GetAxis("Vertical") * motorForce;
    //    float horizontal = Input.GetAxis("Horizontal") * steerForce;
    //    RR_Wheel.motorTorque = vertical;
    //    RL_Wheel.motorTorque = vertical;
    //    FL_Wheel.motorTorque = horizontal;
    //    FR_Wheel.motorTorque = horizontal;

    //    if (Input.GetKey(KeyCode.Space))
    //    {
    //        RL_Wheel.brakeTorque = brakeForce;
    //        RR_Wheel.brakeTorque = brakeForce;
    //    }
    //    if (Input.GetKeyUp(KeyCode.Space))
    //    {
    //        RL_Wheel.brakeTorque = 0;
    //        RR_Wheel.brakeTorque = 0;
    //    }
    //    if (Input.GetAxis("Vertical") == 0)
    //    {
    //        RL_Wheel.brakeTorque = brakeForce;
    //        RR_Wheel.brakeTorque = brakeForce;
    //    }
    //    else
    //    {
    //        RL_Wheel.brakeTorque = 0;
    //        RR_Wheel.brakeTorque = 0;
    //    }
    //}
    public List<AxleInfo> axleInfos; // the information about each individual axle
    public float maxMotorTorque; // maximum torque the motor can apply to wheel
    public float maxSteeringAngle; // maximum steer angle the wheel can have

    public void FixedUpdate()
    {
        float motor = maxMotorTorque * Input.GetAxis("Vertical");
        float steering = maxSteeringAngle * Input.GetAxis("Horizontal");

        foreach (AxleInfo axleInfo in axleInfos)
        {
            if (axleInfo.steering)
            {
                axleInfo.leftWheel.steerAngle = steering;
                axleInfo.rightWheel.steerAngle = steering;
            }
            if (axleInfo.motor)
            {
                axleInfo.leftWheel.motorTorque = motor;
                axleInfo.rightWheel.motorTorque = motor;
            }
        }
    }
}

[System.Serializable]
public class AxleInfo
{
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;
    public bool motor; // is this wheel attached to motor?
    public bool steering; // does this wheel apply steer angle?

}

