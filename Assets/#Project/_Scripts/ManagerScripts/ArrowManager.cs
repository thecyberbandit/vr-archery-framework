using UnityEngine;
using System.Collections;

public class ArrowManager : MonoBehaviour {

	public static ArrowManager Instance;

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
		AttachArrow ();
		PullString ();
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

			stringAttachPoint.transform.localPosition = stringStartPoint.transform.localPosition  + new Vector3 (5f* dist, 0f, 0f);

            var device = SteamVR_Controller.Input((int)trackedObj.index);

			if (device.GetTouchUp (SteamVR_Controller.ButtonMask.Trigger))
            {
                AudioManager.instance.StopSound("string");
				Fire ();
			}
		}
	}

	private void Fire()
    {
        playStringSound = false;
        AudioManager.instance.PlaySound("arrowshoot");

        float dist = (stringStartPoint.transform.position - trackedObj.transform.position).magnitude;

        currentArrow.transform.parent = null;
		currentArrow.GetComponent<Arrow> ().Fired ();

		Rigidbody r = currentArrow.GetComponent<Rigidbody> ();
		r.velocity = currentArrow.transform.forward * 25f * dist;
		r.useGravity = true;


        Vector3 vel = r.velocity;
        float angle = Mathf.Atan2(vel.y, vel.x) * Mathf.Rad2Deg;

        currentArrow.transform.eulerAngles = new Vector3(0, 0, angle);

        currentArrow.GetComponent<Collider> ().isTrigger = false;

		stringAttachPoint.transform.position = stringStartPoint.transform.position;

		currentArrow = null;
		isAttached = false;
	}

	private void AttachArrow()
    {
		if (currentArrow == null) {
			currentArrow = Instantiate (arrowPrefab);
			currentArrow.transform.parent = trackedObj.transform;
			currentArrow.transform.localPosition = new Vector3 (0f, 0f, .342f);
			currentArrow.transform.localRotation = Quaternion.identity;
		}
	}

	public void AttachBowToArrow()
    {
		currentArrow.transform.parent = stringAttachPoint.transform;
		currentArrow.transform.position = arrowStartPoint.transform.position;
		currentArrow.transform.rotation = arrowStartPoint.transform.rotation;

        AudioManager.instance.PlaySound("attach");

		isAttached = true;
	}

    public void PlayStringSound()
    {
        if (playStringSound)
        {
            AudioManager.instance.PlaySound("string");
            playStringSound = false;
        }
    }
}
