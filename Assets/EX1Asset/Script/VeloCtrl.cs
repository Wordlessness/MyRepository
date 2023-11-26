using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VeloCtrl : MonoBehaviour
{

    //position Initialization!
    public Camera mainCamera;
    public float xScale, yScale;
    public Button rerun;
    public Button reset;
    private Vector2 screenBounds;

    private int mod;
    private Rigidbody2D rb;
    private Vector2 centerPosition;
    private float radius;

    private Vector2 initialVelocity;
    private float totalE;
    private Vector2 initialPosition;

    private Vector2 fieldForce;
    private Vector2 fieldForceDirection;

    public TMP_Text calculateText;
    public TMP_Text parameterText;

    private float userInputEX;
    private float userInputEY;
    private float userInputQ;
    private float userInputM;
    private float userInputV0;
    private Vector2 velocityDirection;
    // Start is called before the first frame update
    void Start()
    {
        rerun.onClick.AddListener(onReRunClick);
        reset.onClick.AddListener(onReSetClick);
        rb = GetComponent<Rigidbody2D>();
        centerPosition = new Vector2(0f, 0f);
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width * xScale, Screen.height * yScale, mainCamera.transform.position.z));
        rb.position = new Vector3(screenBounds.x, screenBounds.y, mainCamera.transform.position.z);

        userInputEX = DataManager.instance.Ex;
        userInputEY = DataManager.instance.Ey;
        userInputQ = DataManager.instance.Q;
        userInputM = DataManager.instance.M;
        userInputV0 = DataManager.instance.V0;

        fieldForce = new Vector2(userInputEX * userInputQ, userInputEY * userInputQ - 9.8f * userInputM);
        fieldForceDirection = fieldForce.normalized;

        velocityDirection = Vector2.Perpendicular(fieldForceDirection).normalized;
        initialVelocity = velocityDirection * userInputV0;
        totalE = 0.5f * rb.mass * Mathf.Pow(initialVelocity.magnitude, 2);
        rb.velocity = initialVelocity;

        initialPosition = rb.position;
        radius = (rb.position - centerPosition).magnitude;
        DataManager.instance.R = radius;

        mod = 1;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 deltaVelocity;
        Vector2 deltaComFF;
        Vector2 totalForce;
        Vector2 centrifugalForce;
        Vector2 tension;
        Vector2 tensionDirection;

        float nowEV;
        float nowEP;
        float nowVelocityMagnitude;

        userInputEX = DataManager.instance.Ex;
        userInputEY = DataManager.instance.Ey;
        userInputQ = DataManager.instance.Q;
        userInputV0 = DataManager.instance.V0;
        userInputM = DataManager.instance.M;

        if (mod == 1)//Active and Constraint
        {
            tensionDirection = (centerPosition - rb.position).normalized;
            if ((centerPosition - rb.position).magnitude != radius)
                rb.position = -1 * tensionDirection * radius;

            centrifugalForce = -1 * rb.mass * Mathf.Pow(rb.velocity.magnitude, 2) / radius * tensionDirection;//mv^2/r
            deltaComFF = tensionDirection * Vector2.Dot(fieldForce, tensionDirection);
            if (Vector2.Dot(deltaComFF + centrifugalForce, tensionDirection) > 0)
            {
                mod = 2;
                tension = new Vector2(0f, 0f);
            }
            else
            {
                tension = -1 * (deltaComFF + centrifugalForce);
            }
            totalForce = tension + fieldForce;
            deltaVelocity = totalForce / rb.mass * Time.fixedDeltaTime;
            rb.velocity = rb.velocity + deltaVelocity;
            nowEP = -1 * Vector2.Dot(((Vector2)transform.position - initialPosition), fieldForce);
            nowEV = totalE - nowEP;
            if (0.5f * rb.mass * Mathf.Pow(rb.velocity.magnitude, 2) < nowEV)
            {
                nowVelocityMagnitude = Mathf.Pow(nowEV * 2f / rb.mass, 0.5f);
                rb.velocity = rb.velocity.normalized * nowVelocityMagnitude;
            }
        }
        else if (mod == 2)
        {
            Vector2 lossVelocity;
            deltaVelocity = fieldForce / rb.mass * Time.fixedDeltaTime;//只受场力
            rb.velocity = rb.velocity + deltaVelocity;
            if ((centerPosition - (Vector2)transform.position).magnitude > radius)
            {
                tensionDirection = (centerPosition - (Vector2)transform.position).normalized;
                lossVelocity = Vector2.Dot(tensionDirection, rb.velocity) * tensionDirection * -1;
                rb.velocity = rb.velocity + lossVelocity;
                nowEP = -1 * Vector2.Dot(((Vector2)transform.position - initialPosition), fieldForce);
                totalE = 0.5f * rb.mass * Mathf.Pow(rb.velocity.magnitude, 2) + nowEP;
                mod = 1;
            }
        }
        else
        {
            if ((rb.position - centerPosition).magnitude >= radius + 0.05) {
                rb.velocity = new Vector2(0, 0);
                rb.position = (initialPosition - centerPosition).normalized * radius;
            }
                
        }
        float velocityX = rb.velocity.x;
        float velocityY = rb.velocity.y;
        float positionX = ((Vector2)transform.position).x;
        float positionY = ((Vector2)transform.position).y;

        calculateText.text = "Velocity_x: " + velocityX.ToString("F2") + "\n" +
            "Velocity_y: " + velocityY.ToString("F2") + "\n" +
            "position_x: " + positionX.ToString("F2") + "\n" +
            "position_y: " + positionY.ToString("F2") + "\n";
        parameterText.text = "mass:" + userInputM.ToString("F2") + "\n" +
            "E_X:" + userInputEX.ToString("F2") + "\n" +
            "E_Y:" + userInputEY.ToString("F2") + "\n" +
            "Q:" + userInputQ.ToString("F2") + "\n" +
            "V0:" + userInputV0.ToString("F2") + "\n" +
            "g:" + "9.8" + "\n";
    }
    void onReRunClick()
    {
        rb.position = initialPosition;
        rb.velocity = initialVelocity;
        mod = 1;
    }
    void onReSetClick()
    {
        //读取新的数据
        fieldForce = new Vector2(userInputEX * userInputQ, userInputEY * userInputQ - 9.8f * userInputM);
        fieldForceDirection = fieldForce.normalized;
        velocityDirection = Vector2.Perpendicular(fieldForceDirection).normalized;
        initialVelocity = velocityDirection * userInputV0;
        totalE = 0.5f * rb.mass * Mathf.Pow(initialVelocity.magnitude, 2);
        rb.velocity = initialVelocity;
        initialPosition = fieldForceDirection * radius;
        rb.velocity = (initialPosition - rb.position).normalized * 10f;
        mod = 3;
    }
    
}

