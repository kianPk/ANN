namespace KnifeRound;

public enum KnifePhase { Idle, Live, Picking, Done }

public sealed class KnifeRoundConfig
{
    public bool IdleOnPractice { get; set; } = true;
    public int PickTimeoutSeconds { get; set; } = 30;
}

public sealed class KnifeRoundController
{
    private readonly KnifeRoundConfig _cfg;
    public KnifePhase Phase { get; private set; } = KnifePhase.Idle;
    public string? WinnerTeam { get; private set; }
    public string? PickedSide { get; private set; }
    public bool IsActive { get; private set; } = true;

    public KnifeRoundController(KnifeRoundConfig? cfg = null) => _cfg = cfg ?? new();

    public void OnServerType(string type)
    {
        IsActive = !(_cfg.IdleOnPractice && type.Equals("Practice", StringComparison.OrdinalIgnoreCase));
        if (!IsActive) Reset();
    }

    public void Start()
    {
        if (!IsActive) return;
        Phase = KnifePhase.Live;
        WinnerTeam = null;
        PickedSide = null;
    }

    public void OnRoundEnd(string winnerTeam)
    {
        if (!IsActive || Phase != KnifePhase.Live) return;
        WinnerTeam = winnerTeam;
        Phase = KnifePhase.Picking;
    }

    public bool TryPick(string team, string side)
    {
        if (Phase != KnifePhase.Picking) return false;
        if (!string.Equals(team, WinnerTeam, StringComparison.OrdinalIgnoreCase)) return false;
        side = side.ToUpperInvariant();
        if (side is not ("CT" or "T")) return false;
        PickedSide = side;
        Phase = KnifePhase.Done;
        return true;
    }

    public void Reset() { Phase = KnifePhase.Idle; WinnerTeam = null; PickedSide = null; }
}
