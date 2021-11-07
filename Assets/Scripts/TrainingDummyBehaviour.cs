using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class TrainingDummyBehaviour : MonoBehaviour, IStabbable
{
    [SerializeField] private Animator m_animator;
    [SerializeField] private GameObject m_colliders;

    public int Score(float magnitude, float distance, string touchedPart)
    {
        float distanceForMaxPoints = GameManager.DistanceForMaxPoints;
        float ratio = Mathf.Clamp(distance / distanceForMaxPoints, 0, 1);
        if (touchedPart == "Head")
        {
            m_animator.SetBool("HeadHit", true);
            m_colliders.SetActive(false);
            int score = (int) (50*ratio);
            GameManager.Score += score;
            return score;
        }
        if (touchedPart == "Shield")
        {
            m_animator.SetBool("ShieldHit", true);
            int score = (int) (25*ratio);
            GameManager.Score += score;
            return score;
        }

        return 0;
    }

    public float GetPenetrationThreshold()
    {
        return 0;
    }

    public void PlaySound()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.Play();
    }
}
