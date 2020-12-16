using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARManager : MonoBehaviour {

    public static ARManager Instance;

    public GameObject stringAttachPoint;
    public GameObject arrowStartPoint;
    public GameObject stringStartPoint;

    public GameObject arrowPrefab;

    public SteamVR_TrackedObject trackedObj;

    private GameObject currentArrow;

    private bool isAttached = false;
    private bool playStringSound;


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    private void Start()
    {
        playStringSound = false;
    }

    void Update()
    {
        PullString();
    }

    private void PullString()
    {
        if (isAttached)
        {
            float dist = (stringStartPoint.transform.position - trackedObj.transform.position).magnitude;

            if (dist > 0 && !playStringSound)
            {
                AudioManager.instance.PlaySoundOnce("string");
                playStringSound = true;
            }

            stringAttachPoint.transform.localPosition = stringStartPoint.transform.localPosition + new Vector3(5f * dist, 0f, 0f);
        }
    }

    private void Fire()
    {
        playStringSound = false;
        AudioManager.instance.PlaySound("arrowshoot");

        currentArrow.transform.parent = null;
        currentArrow.GetComponent<Arrow>().Fired();

        Rigidbody r = currentArrow.GetComponent<Rigidbody>();


        Vector3 vel = r.velocity;
        float angle = Mathf.Atan2(vel.y, vel.x) * Mathf.Rad2Deg;

        currentArrow.transform.eulerAngles = new Vector3(0, 0, angle);

        stringAttachPoint.transform.position = stringStartPoint.transform.position;

        currentArrow = null;
        isAttached = false;
    }
}
