using UnityEngine;

public class Dump_Generate : MonoBehaviour
{
    public bool OnlyDestroyingDumps = false;
    public Dump[] dumps_head;
    public Dump[] dumps_body;

    public int ActiveDump { get; private set; }

    private void Start()
    {
        for (int i = 0; i < dumps_body.Length; i++)
        {
            dumps_body[i].gameObject.SetActive(false);
            dumps_head[i].gameObject.SetActive(false);
        }

        ActiveDump = Random.Range(0, dumps_body.Length);
        if (OnlyDestroyingDumps)
        {
            while (!dumps_body[ActiveDump].IsDestroying)
            {
                ActiveDump = Random.Range(0, dumps_body.Length);
            }
        }
        dumps_head[ActiveDump].gameObject.SetActive(true);
        dumps_body[ActiveDump].gameObject.SetActive(true);
    }

    public void Destroy()
    {
        dumps_head[ActiveDump].DestroyThisPart();
        dumps_body[ActiveDump].DestroyThisPart();
        Destroy(gameObject);
    }
}
