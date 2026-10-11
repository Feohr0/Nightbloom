using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "NewSkill", menuName = "SkillTree/Skill")]
public class Skills : ScriptableObject
{
    public string skillName;
    public Image skillIcon;
    public int unluckCost;
    public float coolDown;
    public GameObject skillVFX;
    public AudioClip skillSFX;
    public SkillType skillType;
    public enum SkillType
    {
        Passive,
        Attack,
        Utility
    }
}
