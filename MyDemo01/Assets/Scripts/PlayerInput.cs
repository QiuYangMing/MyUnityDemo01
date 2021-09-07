using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {
    public static PlayerInput Instance
    {
        get { return s_Instance; }
    }

    protected static PlayerInput s_Instance;

    public float Dup;
    public float Dright;
    public float Dmag;
    public float Jup;
    public float Jright;
    public Vector3 Dvec;
    private float h;
    private float v;
    private float velocityDup;
    private float velocityDright;
    private float Dup2;
    private float Dright2;
    public bool inputEnabled = true;
    public bool run;
    public bool lockon;
    public bool roll;
    public MyButton btnW = new MyButton();
    public MyButton btnLS = new MyButton();
    public MyButton btnAD = new MyButton();
	void Update () {

        btnW.Tick(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0);
        btnAD.Tick(Input.GetAxis("Horizontal") != 0);
        btnLS.Tick(Input.GetKey(KeyCode.LeftShift));
        lockon = btnLS.Ispressing;
        Jright = Input.GetAxis("Mouse X");
        Jup = Input.GetAxis("Mouse Y");
        if (inputEnabled == false)
        {
            Dup = 0;
            Dright = 0;
            Dmag = 0;
            Dvec = Vector3.zero;
            return;
        }
       
        roll = btnAD.Ispressing;
        run = (btnW.Ispressing && !btnW.IsDelaying && !lockon);
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        Dup = Mathf.SmoothDamp(Dup, v, ref velocityDup, 0.1f);
        Dright = Mathf.SmoothDamp(Dright, h, ref velocityDright, 0.1f);
        Vector2 tempDAxis = SquareToCircle(new Vector2(Dright,Dup));
        Dright2 = tempDAxis.x;
        Dup2 = tempDAxis.y;
        Dmag = Mathf.Sqrt((Dup2 * Dup2) + (Dright2 * Dright2));
        Dvec = Dright2 * transform.right + Dup2 * transform.forward;
    }

    private Vector2 SquareToCircle(Vector2 input)
    {
        Vector2 output = Vector2.zero;

        output.x = input.x * Mathf.Sqrt(1 - (input.y * input.y) / 2.0f);
        output.y = input.y * Mathf.Sqrt(1 - (input.x * input.x) / 2.0f);

        return output;

    }
}
