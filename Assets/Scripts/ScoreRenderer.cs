using TMPro;
using UnityEngine;

public class ScoreRenderer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tmp;
    // Update is called once per frame
    void Update()
    {
        tmp.text = $"Score : {GameManager.Score}";
    }
}
