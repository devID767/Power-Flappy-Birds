public static class BirdsCount
{
    private static int _count = 1;
    public static int Count
    {
        get
        {
            return _count;
        }
        private set
        {

        }
    }

    public static void AddBird()
    {
        _count++;
    }

    public static void RemoveBird()
    {
        _count--;
        if (_count <= 0)
        {
            _count = 1;
            GameOver.instance.Activate();
        }
    }
}
