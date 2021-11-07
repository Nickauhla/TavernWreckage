public interface IStabbable
{
    // Magnitude is passed in case we would want to attribute points depending on force.
    int Score(float magnitude, float distance, string touchedPart);
    float GetPenetrationThreshold();

    void PlaySound();
}
