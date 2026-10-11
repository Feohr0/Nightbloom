using UnityEngine;
using UnityEngine.UI;

public class SkillSlot : MonoBehaviour
{
    public Skills skill;
    public Sprite skillIcon;
    Button skillButton;
    private void Awake()
    {
        skillIcon = GetComponent<Sprite>();
        skillButton = GetComponent<Button>();
        skillButton.image.sprite = skillIcon;
    }
}
