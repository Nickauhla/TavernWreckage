using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR.Interaction.Toolkit;
using Random = System.Random;

public class ThrowingBehaviour : XRGrabInteractable
{
    [SerializeField] private Rigidbody m_rigidbody;
    [SerializeField] private Transform m_tip;
    [SerializeField] private Collider m_inAirCollider;
    [SerializeField] private NotifyScore ScoreNotifPrefab;
    [SerializeField] private AudioSource m_audioSource;
    [SerializeField] private List<AudioClip> m_clips;
    private bool m_inAir;
    private Vector3 m_lastPosition;
    private Vector3 m_positionOfLaunch;

    private int m_ignoreLayer;
    private int m_defaultLayer;
    public bool HasBeenUsed { get; private set; } = false;

    protected override void Awake()
    {
        base.Awake();
        m_defaultLayer = this.gameObject.layer;
        m_ignoreLayer = LayerMask.NameToLayer("Ignore");
        m_rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (m_inAir)
        {
            CheckForCollision();
            m_lastPosition = m_tip.position;
        }
    }

    private void CheckForCollision()
    {
        if (!Physics.Linecast(m_lastPosition, m_tip.position, out RaycastHit hitInfo)) return;
        Collider hitInfoCollider = hitInfo.collider;
        // Avoid thrown object to collide with himself in air or head collider for food
        if (hitInfoCollider == m_inAirCollider || hitInfoCollider.tag == "Food") return;
        GameObject objectTouched = hitInfoCollider.gameObject;
        Rigidbody hitRigibody = hitInfo.rigidbody;
        IStabbable stabbable = null;
        stabbable = hitRigibody ? hitRigibody.gameObject.GetComponent<IStabbable>() : objectTouched.GetComponent<IStabbable>();
        this.transform.parent = hitInfo.collider.transform;
        if (stabbable != null)
        {
            HandleHitOnStabbable(stabbable, objectTouched);
        }
        else
        {
            if (m_rigidbody.velocity.magnitude >= 1)
            {
                Stop();
                m_audioSource.PlayOneShot(RandomizeAudioClip());
            }
        }
    }

    private AudioClip RandomizeAudioClip()
    {
        Random rand = new Random();
        int next = rand.Next(m_clips.Count);
        return m_clips[next];
    }

    private void InstantiateScoreNotif(Vector3 hitInfoPoint, int score)
    {
        NotifyScore notifyScore = Instantiate(ScoreNotifPrefab, hitInfoPoint+Vector3.up*0.2f, Quaternion.identity);
        notifyScore.Fill(score);
    }

    private void HandleHitOnStabbable(IStabbable stabbable, GameObject objectTouched)
    {
        float magnitude = m_rigidbody.velocity.magnitude;
        if (magnitude >= stabbable.GetPenetrationThreshold())
        {
            Stop();
            int score = stabbable.Score(magnitude, Vector3.Distance(m_positionOfLaunch, objectTouched.transform.position), objectTouched.name);
            InstantiateScoreNotif(transform.position, score);
            stabbable.PlaySound();
        }
    }

    private void Stop()
    {
        m_inAir = false;
        SetPhysics(false);
        m_rigidbody.constraints = RigidbodyConstraints.None;
    }

    private void SetPhysics(bool usePhysics)
    {
        m_rigidbody.isKinematic = !usePhysics;
        m_rigidbody.useGravity = usePhysics;
        colliders[0].enabled = !usePhysics;
        m_inAirCollider.enabled = usePhysics;
        this.gameObject.layer = usePhysics ?  m_defaultLayer : m_ignoreLayer;
    }

    private void Release()
    {
        m_inAir = true;
        SetPhysics(true);
        m_lastPosition = m_tip.position;
    }

    protected override void OnSelectEntered(XRBaseInteractor interactor)
    {
        base.OnSelectEntered(interactor);
        HasBeenUsed = true;
    }

    protected override void OnSelectExited(XRBaseInteractor interactor)
    {
        base.OnSelectExited(interactor);
        m_positionOfLaunch = interactor.transform.position;
        Release();
    }
}