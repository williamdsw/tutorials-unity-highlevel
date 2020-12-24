using UnityEngine;
using UnityEngine.UI;

public class StageSelect : MonoBehaviour
{
    [SerializeField] private Image selection;
    [SerializeField] private StageSelect up;
    [SerializeField] private StageSelect down;
    [SerializeField] private StageSelect left;
    [SerializeField] private StageSelect right;
    [SerializeField] private string scene;
    [SerializeField] private int boss;

    public StageSelect() { }

    public Image Selection { get => selection; set => selection = value; }
    public StageSelect Up { get => up; set => up = value; }
    public StageSelect Down { get => down; set => down = value; }
    public StageSelect Left { get => left; set => left = value; }
    public StageSelect Right { get => right; set => right = value; }
    public string Scene { get => scene; set => scene = value; }
    public int Boss { get => boss; set => boss = value; }
}
