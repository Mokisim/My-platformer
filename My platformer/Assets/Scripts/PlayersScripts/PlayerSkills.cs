using UnityEngine;

[RequireComponent(typeof(InputReader))]
public abstract class PlayerSkills : MonoBehaviour
{
    public InputReader InputReader {  get; private set; }
    
    private void Awake()
    {
        InputReader = GetComponent<InputReader>();
    }

    private void Update()
    {
        if (SetInput())
        {
            UseSkill();
        }
    }

    public abstract bool SetInput();

    public abstract void UseSkill();
}
