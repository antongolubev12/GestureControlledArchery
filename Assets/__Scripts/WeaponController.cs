using UnityEngine;
using UnityEngine.UI;
using LockingPolicy = Thalmic.Myo.LockingPolicy;
using Pose = Thalmic.Myo.Pose;
using UnlockType = Thalmic.Myo.UnlockType;
using VibrationType = Thalmic.Myo.VibrationType;
using UnityEngine.SceneManagement;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private Bow bow;

    [SerializeField] private GameObject lightSaber;

    [SerializeField] private string enemyTag;

    [SerializeField] private float maxFirePower;

    [SerializeField] private float firePowerSpeed;

    private float firePower;

    private bool fire;

    private Quaternion _antiYaw = Quaternion.identity;

    private float _referenceRoll = 0.0f;

    private Pose _lastPose = Pose.Unknown;

    [SerializeField] private GameObject myo = null;

    private ThalmicMyo thalmicMyo;

    private Rigidbody bowRB;

    private Rigidbody saberRB;

    private Scene scene;

    enum Weapons
    {
        LightSaber,
        Bow
    }

    private Weapons currentWeapon;


    void Start()
    {   
        scene = SceneManager.GetActiveScene();
        currentWeapon = Weapons.Bow;
        lightSaber.SetActive(false);

        bow.SetEnemyTag(enemyTag);
        bow.Reload();

        bowRB = bow.GetComponent<Rigidbody>();
        saberRB = lightSaber.GetComponent<Rigidbody>();
    }

    void Update()
    {
        //if game is not paused or over
        if (Time.timeScale == 1f)
        {
            // Access the ThalmicMyo component attached to the Myo object.
            thalmicMyo = myo.GetComponent<ThalmicMyo>();

            if (currentWeapon == Weapons.Bow)
            {
                moveWeapon(bowRB);

                //if pose is fist then charge the bow
                if (thalmicMyo.pose == Pose.Fist)
                {
                    fire = true;
                }

                if (fire && firePower < maxFirePower)
                {
                    bow.Draw();
                    //fire power is pased on how long the bow was charged for
                    firePower += Time.deltaTime * firePowerSpeed;
                }

                //fire the bow when the fist is let go
                if (fire && thalmicMyo.pose != Pose.Fist)
                {
                    AudioManager.Instance.PlayFire();
                    bow.Fire(firePower);
                    firePower = 0;
                    fire = false;
                }

                //Wave out to change to lightsaber
                if (scene.name == "Game" && thalmicMyo.pose == Pose.WaveOut)
                {
                    AudioManager.Instance.LightSaberOn();
                    bow.gameObject.SetActive(false);
                    lightSaber.SetActive(true);
                    currentWeapon = Weapons.LightSaber;

                }
            }

            if (currentWeapon == Weapons.LightSaber)
            {
                moveWeapon(saberRB);

                if (thalmicMyo.pose == Pose.WaveIn)
                {
                    currentWeapon = Weapons.Bow;
                    lightSaber.SetActive(false);
                    bow.gameObject.SetActive(true);
                    AudioManager.Instance.LightSaberOff();
                }
            }
        }
    }

    void moveWeapon(Rigidbody rb)
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