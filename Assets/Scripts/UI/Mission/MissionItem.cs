using UnityEngine;
using UnityEngine.UI;

public class MissionItem : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] Text text;
    [SerializeField] Sprite[] potionSprites;
    [SerializeField] Sprite[] obstacleSprites;

    private int currentNumber;

    public void SetupMissionItem(int number, EPotion? potionType = null, EObstacle? obstacleType = null)
    {
        currentNumber = number;
        text.text = $"X{number}";
        if (potionType != null)
            image.sprite = potionSprites[(int)potionType];
        else
            image.sprite = obstacleSprites[(int)obstacleType];
    }

    public void UpdateNumber(int newNumber)
    {
        this.currentNumber = Mathf.Max(this.currentNumber - newNumber, 0);
        text.text = $"X{currentNumber}";
    }
}
