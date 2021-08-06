using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum TypeOfBird
{
    Red,
    Blue,
    Yellow,
    Gray
}

public class Bird : MonoBehaviour
{
    [Header("Characters")]
    public float ForceToJump;
    public TypeOfBird typeOfBird;

    [Header("Power")]
    public float[] CoolDownPowerOfLevels;
    [HideInInspector] public PowerEnum CurrentPower;

    [Header("Power effects")]
    public GameObject Shield;

    [Header("Particles")]
    public GameObject[] FeatherParticles;
    public GameObject FlapDownParticle;
    private List<GameObject> _flapDownParticles = new List<GameObject>();
    public Transform FlapDownParticleTransform;

    private Rigidbody2D rigidbody2D;
    private Animator anim;

    private bool IsDead = false;

    private void OnEnable()
    {
        SwipeDetector.OnSwipe += SwipeDetector_OnSwipe;
    }

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        if (GameStatus.Current != GameStatusEnum.IsPlaying)
        {
            rigidbody2D.bodyType = RigidbodyType2D.Static;
        }

        if (typeOfBird == TypeOfBird.Gray)
        {
            transform.rotation = Quaternion.identity;
        }
    }
    void Update()
    {
        if (!IsDead && GameStatus.Current == GameStatusEnum.IsPlaying)
        {
            KeyBoardControl();

            if (CurrentPower == PowerEnum.Rocket)
            {
                if (Input.touchCount > 0)
                {
                    if (Input.touches[0].phase == TouchPhase.Moved)
                    {
                        if (!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                        {
                            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(Input.touches[0].position);
                            rigidbody2D.position = new Vector2(rigidbody2D.position.x, touchPosition.y);
                            rigidbody2D.velocity = Vector2.zero;
                        }
                    }
                }
            }
            else if (rigidbody2D.velocity.y < 0)
            {
                anim.SetBool("falling", true);
            }
        }
    }
    public void UseEnergy() //jump down
    {
        int index = Icons.energy.Count - 1;
        while (index >= 0)
        {
            if (Icons.energy.Do(x => x.GetComponent<Energy>().Use(), index))
            {
                InstantiateFeather();
                InstantiateFlapDownEffect();
                if (!anim.GetBool("flap_down"))
                {
                    rigidbody2D.velocity = Vector2.zero;
                    rigidbody2D.AddForce(new Vector2(0, -ForceToJump));
                }
                else
                    rigidbody2D.AddForce(new Vector2(0, -ForceToJump / 2));
                anim.SetBool("flap_down", true);
                break;
            }
            else
                index--;
        }
    }

    public void PowerActivate(Bird bird)
    {
        if (Power.instance.CanUse)
        {
            switch (bird.typeOfBird)
            {
                case TypeOfBird.Red:
                    Power.instance.Shield(bird);
                    break;
                case TypeOfBird.Blue:
                    Power.instance.Deal(bird);
                    break;
                case TypeOfBird.Yellow:
                    Power.instance.Rocket(bird);
                    break;
                case TypeOfBird.Gray:
                    Power.instance.Bomb(bird);
                    break;
            }
        }
    }
    public void Dead()
    {
        DestroyFlapDownEffects();
        InstantiateFeather();

        rigidbody2D.freezeRotation = false;
        IsDead = true;
        anim.SetBool("Dead", true);

        BirdsCount.RemoveBird();

        Destroy(this);
    }

    public void Change()
    {
        Destroy(gameObject);
    }

    public void GameStarted()
    {
        rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
        Flip();
    }

    private void SwipeDetector_OnSwipe(SwipeData obj)
    {
        if (!IsDead && CurrentPower != PowerEnum.Rocket)
        {
            if (obj.Direction == SwipeDirection.None)
            {
                Flip();
            }
            else if (obj.Direction == SwipeDirection.Down)
            {
                UseEnergy();
            }
            else if (obj.Direction == SwipeDirection.Right)
            {
                PowerActivate(this);
            }
        }
    }

    private void KeyBoardControl()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            Flip();
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            UseEnergy();
        }
    }

    private void Flip()
    {
        try
        {
            DestroyFlapDownEffects();
            anim.SetBool("flap_down", false);
            anim.SetBool("falling", false);
            anim.SetTrigger("flap");
            rigidbody2D.velocity = Vector2.zero;
            rigidbody2D.AddForce(new Vector2(0, ForceToJump));
        }
        catch
        {

        }
    }

    private void InstantiateFeather()
    {
        Instantiate(FeatherParticles[Random.Range(0, FeatherParticles.Length)], transform.position, Quaternion.identity);

    }
    private void InstantiateFlapDownEffect()
    {
        _flapDownParticles.Add(Instantiate(FlapDownParticle, FlapDownParticleTransform, false));
        _flapDownParticles[_flapDownParticles.Count - 1].transform.localScale = FlapDownParticleTransform.localScale;
    }

    private void DestroyFlapDownEffects()
    {
        foreach (var item in _flapDownParticles)
        {
            Destroy(item);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Coin coin))
        {
            coin.Add();
        }
        else if (collision.TryGetComponent(out Bird_Resource bird))
        {
            bird.Add();
        }
        else if (TryGetComponentInParent(collision, out Dump dump))
        {
            if (CheckTypeOfBirdAndDump(dump.material))
            {
                InstantiateFeather();
                dump.DestroyAllDump();
            }
            else if (CurrentPower == PowerEnum.Shield)
            {
                Power.instance.ShieldDestroy(this);
                dump.DestroyAllDump();
            }
            else
            {
                Dead();
            }
        }
        else if(collision.CompareTag("Enemy"))
        {
            Dead();
        }
    }

    private bool TryGetComponentInParent<T>(Collider2D collision, out T dump)
    {
        dump = collision.GetComponentInParent<T>();

        if (dump == null)
        {
            return false;
        }
        return true;
    }

    private bool CheckTypeOfBirdAndDump(TypeOfDump typeOfDump)
    {
        if (typeOfDump == TypeOfDump.TreeBreak && typeOfBird == TypeOfBird.Yellow)
            return true;
        else if (typeOfDump == TypeOfDump.Tree && typeOfBird == TypeOfBird.Yellow && CurrentPower == PowerEnum.Rocket)
            return true;
        else if (typeOfDump == TypeOfDump.IceBreak && typeOfBird == TypeOfBird.Blue)
            return true;
        else if (typeOfDump == TypeOfDump.Ice && typeOfBird == TypeOfBird.Blue && CurrentPower == PowerEnum.Deal)
            return true;
        else if (typeOfDump == TypeOfDump.MetalBreak && typeOfBird == TypeOfBird.Gray)
            return true;
        else if (typeOfDump == TypeOfDump.Metal && typeOfBird == TypeOfBird.Gray && CurrentPower == PowerEnum.Bomb)
            return true;
        else if (typeOfDump == TypeOfDump.DefaultBreak)
            return true;
        else
            return false;
    }

    public float CoolDownPower()
    {
        return CoolDownPowerOfLevels[LevelsOfBirds.dict[typeOfBird]];
    }

    private void OnDisable()
    {
        SwipeDetector.OnSwipe -= SwipeDetector_OnSwipe;
    }
}
