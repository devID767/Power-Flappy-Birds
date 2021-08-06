using UnityEngine;

public enum TypeOfDump
{
    Tree,
    TreeBreak,
    Ice,
    IceBreak,
    Metal,
    MetalBreak,
    Default,
    DefaultBreak
}

public class Dump : MonoBehaviour
{
    public TypeOfDump material;
    public bool IsDestroying
    {
        get
        {
            if (material == TypeOfDump.TreeBreak || material == TypeOfDump.IceBreak || material == TypeOfDump.MetalBreak || material == TypeOfDump.DefaultBreak) return true;
            else return false;
        }
    }

    public GameObject DestroyObject;

    public Dump_Generate ParentDump;

    public void DestroyAllDump()
    {
        ParentDump.Destroy();
    }
    public void DestroyThisPart()
    {
        var destroydump = Instantiate(DestroyObject, transform.position, transform.rotation);
        destroydump.transform.localScale = transform.localScale;
    }
}
