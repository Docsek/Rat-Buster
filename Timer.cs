public class Timer
{
    public float elapsedTime { get; private set; }
    public float duration { get; private set; }
    public bool isLooping { get; private set; }
    public bool isRunning { get; private set; }
    public int? iterations { get; private set; }
    public int elapsedIterations { get; private set; }
    public Action? Callback { private get; set; }

    public Timer(float duration, bool isLooping, int? iterations, Action? Callback = null)
    {
        this.duration = duration;
        this.iterations = iterations;
        this.isLooping = isLooping;
        this.Callback = Callback;
        this.elapsedTime = 0f;
        this.elapsedIterations = 0;
        this.isRunning = true;
    }
    public void Update(float dt)
    {
        if (isRunning)
        {
            elapsedTime += dt;
            if (elapsedTime >= duration)
            {
                Callback?.Invoke();
                if (isLooping) elapsedTime = 0f;
                else
                {
                    elapsedIterations++;
                    if (iterations != null && elapsedIterations < iterations) elapsedTime = 0f;
                    else isRunning = false;
                }
            }
        }
        else return;
    }
    public void Reset()
    {
        elapsedTime = 0f;
        elapsedIterations = 0;
    }
    public void ResetCurrentIteration()
    {
        elapsedTime = 0f;
    }
    public void Start()
    {
        Reset();
        isRunning = true;
    }

    public void Pause()
    {
        isRunning = false;
    }
    public void SetDuration(float newDuration)
    {
        duration = newDuration;
    }
    public void AddIteration()
    {
        iterations++;
    }
    public bool IsFinished()
    {
        if (elapsedTime >= duration && (elapsedIterations >= iterations || iterations == null)) return true;
        else return false;
    }
    public float TimeLeft()
    {
        return duration - elapsedTime;
    }
    public int RoundedTimeLeft()
    {
        return (int)Math.Round(TimeLeft());
    }

    // ToString override to print Timers remaining time
    public override string ToString()
    {
        return $"{RoundedTimeLeft()}";
    }
}