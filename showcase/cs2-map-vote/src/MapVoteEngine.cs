namespace MapVote;

public sealed class MapVoteEngine
{
    private readonly List<string> _maps = new();
    private readonly Dictionary<string, int> _votes = new(StringComparer.OrdinalIgnoreCase); // steamid -> index
    public bool IsOpen { get; private set; }

    public IReadOnlyList<string> Maps => _maps;

    public void Open(IEnumerable<string> maps)
    {
        _maps.Clear();
        _maps.AddRange(maps.Where(m => !string.IsNullOrWhiteSpace(m)).Distinct(StringComparer.OrdinalIgnoreCase));
        _votes.Clear();
        IsOpen = _maps.Count > 0;
    }

    public bool Cast(string voterId, int index)
    {
        if (!IsOpen || index < 0 || index >= _maps.Count) return false;
        _votes[voterId] = index;
        return true;
    }

    public string? Close(Random? rng = null)
    {
        if (!IsOpen) return null;
        IsOpen = false;
        if (_votes.Count == 0) return _maps[(rng ?? Random.Shared).Next(_maps.Count)];

        var tallies = new int[_maps.Count];
        foreach (var idx in _votes.Values) tallies[idx]++;
        var best = tallies.Max();
        var tied = Enumerable.Range(0, _maps.Count).Where(i => tallies[i] == best).ToList();
        return _maps[tied[(rng ?? Random.Shared).Next(tied.Count)]];
    }

    public IReadOnlyDictionary<string, int> Tally()
    {
        var d = _maps.ToDictionary(m => m, _ => 0, StringComparer.OrdinalIgnoreCase);
        foreach (var idx in _votes.Values) d[_maps[idx]]++;
        return d;
    }
}
