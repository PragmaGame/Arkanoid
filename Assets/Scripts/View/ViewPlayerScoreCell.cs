using UnityEngine;
using UnityEngine.UI;

public class ViewPlayerScoreCell : MonoBehaviour
{
    [SerializeField] private Text _name;
    [SerializeField] private Text _score;

    public void SetPlayerScore(PlayerScore data)
    {
        _name.text = data.name;
        _score.text = data.score.ToString();
    }
}