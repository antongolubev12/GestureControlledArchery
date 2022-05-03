using UnityEngine;
using UnityEngine.UI;
using LockingPolicy = Thalmic.Myo.LockingPolicy;
using Pose = Thalmic.Myo.Pose;
using UnlockType = Thalmic.Myo.UnlockType;
using VibrationType = Thalmic.Myo.VibrationType;
public class WeaponController : MonoBehaviour
{
    [SerializeField]
    private Text firePowerText;

    [SerializeField]
    private Bow bow;

    [SerializeField]
    private string enemyTag;

    [SerializeField] private GameObject ArrowPrefab;

    [SerializeField]
    private float maxFirePower;

    [SerializeField]
    private float firePowerSpeed;

    private float firePower;

    [SerializeField]
    private float rotateSpeed;

    [SerializeField]
    private float minRotation;

    [SerializeField]
    private float maxRotation;

    private float mouseY;

    private bool fire;

    private Quaternion _antiYaw = Quaternion.identity;

    private float _referenceRoll = 0.0f;

    private Pose _lastPose = Pose.Unknown;

    [SerializeField]
    private GameObject myo = null;
    private ThalmicMyo thalmicMyo;

    private Rigidbody rb;


    void Start()
    {
        bow.SetEnemyTag(enemyTag);
        bow.Reload();

        rb = bow.GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Access the ThalmicMyo component attached to the Myo object.
        thalmicMyo = myo.GetComponent<ThalmicMyo>();

        moveBow();
        if (thalmicMyo.pose == Pose.Fist)
        {
            fire = true;
        }

        if (fire && firePower < maxFirePower)
        {
            bow.Draw();
            firePower += Time.deltaTime * firePowerSpeed;
        }
        
        if (fire && thalmicMyo.pose != Pose.Fist)
        {
            bow.Fire(firePower);
            firePower = 0;
            fire = false;
        }
    }

    void moveBow()
    {
        // Update references when the pose becomes fingers spread or the q key is pressed.
        bool updateReference = false;
        if (thalmicMyo.pose != _lastPose)
        {
            _lastPose = thalmicMyo.pose;

            if (thalmicMyo.pose == Pose.FingersSpread)
            {
                updateReference = true;

                ExtendUnlockAndNotifyUserAction(thalmicMyo);
            }
        }
        if (Input.GetKeyDown("r"))
        {
            updateReference = true;
        }

        if (updateReference)
        {
            _antiYaw = Quaternion.FromToRotation(
                new Vector3(myo.transform.forward.x, 0, myo.transform.forward.z),
                new Vector3(0, 0, 1)
            );

            Vector3 referenceZeroRoll = computeZeroRollVector(myo.transform.forward);
            _referenceRoll = rollFromZero(referenceZeroRoll, myo.transform.forward, myo.transform.up);
        }
        Vector3 zeroRoll = computeZeroRollVector(myo.transform.forward);

        float roll = rollFromZero(zeroRoll, myo.transform.forward, myo.transform.up);

        float relativeRoll = normalizeAngle(roll - _referenceRoll);

        Quaternion antiRoll = Quaternion.AngleAxis(relativeRoll, myo.transform.forward);

        rb.rotation = _antiYaw * antiRoll * Quaternion.LookRotation(myo.transform.forward);
    }
    float normalizeAngle(float angle)
    {
        if (angle > 180.0f)
        {
            return angle - 360.0f;
        }
        if (angle < -180.0f)
        {
            return angle + 360.0f;
        }
        return angle;
    }

    float rollFromZero(Vector3 zeroRoll, Vector3 forward, Vector3 up)
    {
        float cosine = Vector3.Dot(up, zeroRoll);

        Vector3 cp = Vector3.Cross(up, zeroRoll);
        float directionCosine = Vector3.Dot(forward, cp);
        float sign = directionCosine < 0.0f ? 1.0f : -1.0f;

        // Return the angle of roll (in degrees) from the cosine and the sign.
        return sign * Mathf.Rad2Deg * Mathf.Acos(cosine);
    }

    Vector3 computeZeroRollVector(Vector3 forward)
    {
        Vector3 antigravity = Vector3.up;
        Vector3 m = Vector3.Cross(myo.transform.forward, antigravity);
        Vector3 roll = Vector3.Cross(m, myo.transform.forward);

        return roll.normalized;
    }

    void ExtendUnlockAndNotifyUserAction(ThalmicMyo myo)
    {
        ThalmicHub hub = ThalmicHub.instance;

        if (hub.lockingPolicy == LockingPolicy.Standard)
        {
            myo.Unlock(UnlockType.Timed);
        }

        myo.NotifyUserAction();
    }
}