using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PowerManager : MonoBehaviour
{
    public static PowerManager instance;

    [Header("Buttons of powers")]
    public GameObject ShieldButton;
    public GameObject RocketButton;
    public GameObject DealButton;
    public GameObject BombButton;

    [Header("Scale of powers")]
    public GameObject ShieldScale;
    public GameObject RocketScale;
    public GameObject DealScale;
    public GameObject BombScale;

    //lox

    private Image Image;

    [Header("Time of powers")]
    public float[] ShieldTimeOfLevels;
    public float[] RocketTimeOfLevels;
    public float[] DealTimeOfLevels;
    public float[] BombTimeOfLevels;

    public float ShieldTime
    {
        get
        {
            return ShieldTimeOfLevels[AbilitiesLevel.instance.Abilities[(int)EAbility.Shield].Level];
        }
    }
    public float RocketTime
    {
        get
        {
            return RocketTimeOfLevels[AbilitiesLevel.instance.Abilities[(int)EAbility.Rocket].Level];
        }
    }
    public float DealTime
    {
        get
        {
            return DealTimeOfLevels[AbilitiesLevel.instance.Abilities[(int)EAbility.Deal].Level];
        }
    }
    public float BombTime
    {
        get
        {
            return BombTimeOfLevels[AbilitiesLevel.instance.Abilities[(int)EAbility.Bomb].Level];
        }
    }

    [HideInInspector] public Bird Target;
    [HideInInspector] public float CoolDown;
    private float _duration;
    [HideInInspector] public bool CanUse;

    private bool Using;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (this != instance)
        {
            Destroy(this);
        }

        Target = FindObjectOfType<Bird>();
        CoolDown = Target.CoolDownPower();

        ChoosePowerScale();

        BirdsChanger.Changed += BirdChanged;
    }
    private void Update()
    {
        if (Using)
        {
            UsingPower();
        }
        else
        {
            FillPowerImage();
        }
    }
    private void UsingPower()
    {
        Image.fillAmount -= 1 / _duration * Time.deltaTime;

        if (Image.fillAmount <= 0)
        {
            Using = false;
        }
    }

    private void FillPowerImage()
    {
        if (Image.fillAmount < 1)
        {
            Image.fillAmount += 1 / CoolDown * Time.deltaTime;
            CanUse = false;
        }
        else
        {
            Image.fillAmount = 1;
            CanUse = true;
            ChoosePowerButton();
        }
    }

    private void BirdChanged(Bird bird)
    {
        Target = bird;
        CoolDown = bird.CoolDownPower();

        ChoosePowerScale();
        DeactivateAllButton();

        Image.fillAmount = 0;
    }

    private void ChoosePowerScale()
    {
        switch (Target.typeOfBird)
        {
            case TypeOfBird.Red:
                ScaleOn(ShieldScale);
                break;
            case TypeOfBird.Blue:
                ScaleOn(DealScale);
                break;
            case TypeOfBird.Yellow:
                ScaleOn(RocketScale);
                break;
            case TypeOfBird.Gray:
                ScaleOn(BombScale);
                break;
        }
    }

    private void ScaleOn(GameObject Scale)
    {
        DeactivateAllScale();
        Scale.SetActive(true);
        Image = Scale.GetComponent<BoardOfPowerScale>().PowerScale;
    }

    private void DeactivateAllScale()
    {
        ShieldScale.SetActive(false);
        DealScale.SetActive(false);
        RocketScale.SetActive(false);
        BombScale.SetActive(false);
    }

    private void ChoosePowerButton()
    {
        switch (Target.typeOfBird)
        {
            case TypeOfBird.Red:
                ButtonOn(ShieldButton);
                break;
            case TypeOfBird.Blue:
                ButtonOn(DealButton);
                break;
            case TypeOfBird.Yellow:
                ButtonOn(RocketButton);
                break;
            case TypeOfBird.Gray:
                ButtonOn(BombButton);
                break;
        }
    }

    private void ButtonOn(GameObject Button)
    {
        DeactivateAllButton();
        Button.SetActive(true);
    }

    private void DeactivateAllButton()
    {
        ShieldButton.SetActive(false);
        DealButton.SetActive(false);
        RocketButton.SetActive(false);
        BombButton.SetActive(false);
    }

    private void Use(float duration)
    {
        CanUse = false;

        Using = true;
        _duration = duration;
        Image.fillAmount = 1;

        DeactivateAllButton();
    }

    public void Shield(Bird bird)
    {
        ScaleOn(ShieldScale);

        Use(ShieldTime);

        bird.CurrentPower = PowerEnum.Shield;
        bird.Shield.SetActive(true);

        StartCoroutine(ShieldStop(bird));
    }
    public void ShieldDestroy(Bird bird)
    {
        bird.Shield.SetActive(false);
        bird.CurrentPower = PowerEnum.None;
        Image.fillAmount = 0;
    }
    IEnumerator ShieldStop(Bird bird)
    {
        yield return new WaitForSeconds(ShieldTime);
        if (bird != null)
        {
            bird.Shield.SetActive(false);
            bird.CurrentPower = PowerEnum.None;
        }
    }

    public void Rocket(Bird bird)
    {
        ScaleOn(RocketScale);

        Use(RocketTime);

        bird.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        bird.GetComponent<Rigidbody2D>().isKinematic = true;
        bird.GetComponent<Animator>().SetBool("falling", false);
        bird.GetComponent<Animator>().Play("idle");

        bird.CurrentPower = PowerEnum.Rocket;

        StartCoroutine(RocketStop(bird));
    }
    IEnumerator RocketStop(Bird bird)
    {
        yield return new WaitForSeconds(RocketTime);
        if (bird != null)
        {
            bird.CurrentPower = PowerEnum.None;
            bird.GetComponent<Rigidbody2D>().isKinematic = false;
        }
    }

    public void Deal(Bird bird)
    {
        ScaleOn(DealScale);

        Use(DealTime);

        BirdsCount.AddBird();
        var vectorNewBird = new Vector2(bird.gameObject.transform.position.x, bird.gameObject.transform.position.y - 1.5f);
        GameObject newBird = Instantiate(bird.gameObject, vectorNewBird, bird.gameObject.transform.rotation);

        bird.gameObject.transform.position = new Vector2(bird.gameObject.transform.position.x, bird.gameObject.transform.position.y + 1.5f);

        newBird.GetComponent<Rigidbody2D>().velocity = bird.gameObject.GetComponent<Rigidbody2D>().velocity;


        bird.CurrentPower = PowerEnum.Deal;
        newBird.GetComponent<Bird>().CurrentPower = PowerEnum.Deal;

        StartCoroutine(DealStop(bird, newBird.GetComponent<Bird>()));
    }

    IEnumerator DealStop(params Bird[] birds)
    {
        yield return new WaitForSeconds(DealTime);
        foreach (var bird in birds)
        {
            bird.CurrentPower = PowerEnum.None;
        }
    }

    public void Bomb(Bird bird)
    {
        ScaleOn(BombScale);

        Use(BombTime);

        var dumps = FindObjectsOfType<Dump_Generate>();

        foreach (var dump in dumps)
        {
            dump.Destroy();
        }

        StartCoroutine(BombStop(bird));
    }

    IEnumerator BombStop(Bird bird)
    {
        yield return new WaitForSeconds(BombTime);
        bird.CurrentPower = PowerEnum.None;
    }

    private void OnDisable()
    {
        BirdsChanger.Changed -= BirdChanged;
    }
}
