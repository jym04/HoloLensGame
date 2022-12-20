using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RobotController : MonoBehaviour
{

    public float smooth = 1;

    [Range(0.0f, 360.0f)]
    public float Axis1;

    [Range(-20.0f, 70.0f)]
    public float Axis2;
    [Range(-50.0f, 50.0f)]
    public float Axis3;
    [Range(0.0f, 360.0f)]
    public float Axis4;
    [Range(300.0f, 360.0f)]
    public float Axis5;

    private float m_Axis1 = 0;
    private float m_Axis2 = 0;
    private float m_Axis3 = 0;
    private float m_Axis4 = 0;
    private float m_Axis5 = 0 ;


    // Start is called before the first frame update
    void Start()
    {
        setRotation();
    }

    // Update is called once per frame
    void Update()
    {
     

        if (Axis1 != m_Axis1)
        {
            gameObject.GetComponentInChildren<Axis1Y>().RotateAxis1Y(Axis1, smooth); //
            m_Axis1 = Axis1;
        }
          

        if ( Axis2 != m_Axis2)
        {
            gameObject.GetComponentInChildren<Axis2Z>().RotateAxis2Z(Axis2, smooth); //
            m_Axis2 = Axis2;
        }

        if (Axis3 != m_Axis3)
        {
            gameObject.GetComponentInChildren<Axis3Z>().RotateAxis3Z(Axis3, smooth); //
            m_Axis3 = Axis3;
        }

        if (Axis4 != m_Axis4)
        {
            gameObject.GetComponentInChildren<Axis4X>().RotateAxis4X(Axis4, smooth); //
            m_Axis4 = Axis4;
        }

        if (Axis5 != m_Axis5)
        {
            gameObject.GetComponentInChildren<Axis5Z>().RotateAxis5Z(Axis5, smooth); //
            m_Axis5 = Axis5;
        }



    }

    private void configRotation()
    {

        setRotation();
    }

    private void setRotation()
    {
        gameObject.GetComponentInChildren<Axis1Y>().RotateAxis1Y(Axis1, smooth);
        gameObject.GetComponentInChildren<Axis2Z>().RotateAxis2Z(Axis2, smooth);
        gameObject.GetComponentInChildren<Axis3Z>().RotateAxis3Z(Axis3, smooth);
        gameObject.GetComponentInChildren<Axis4X>().RotateAxis4X(Axis4, smooth);
        gameObject.GetComponentInChildren<Axis5Z>().RotateAxis5Z(Axis5, smooth);
    }

    
}
