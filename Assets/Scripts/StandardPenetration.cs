using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class StandardPenetration : MonoBehaviour, IStabbable
{
    [SerializeField] private float m_penetrationThreshold;
    [SerializeField] private int m_points;
    
    public int Score(float magnitude, float distance, string touchedPart)
    {
        float distanceForMaxPoints = GameManager.DistanceForMaxPoints;
        float ratio = Mathf.Clamp(distance / distanceForMaxPoints, 0, 1);
        int score = (int) (m_points * ratio);
        GameManager.Score += score;
        return score;
    }

    public float GetPenetrationThreshold()
    {
        return m_penetrationThreshold;
    }

    public void PlaySound()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.Play();
    }
}
