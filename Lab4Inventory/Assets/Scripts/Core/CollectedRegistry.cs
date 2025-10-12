using System.Collections.Generic;

public static class CollectedRegistry
{
    private static readonly HashSet<string> _collected = new HashSet<string>();

    public static void MarkCollected(string id)
    {
        if (!string.IsNullOrEmpty(id)) _collected.Add(id);
    }

    public static bool IsCollected(string id)
    {
        return !string.IsNullOrEmpty(id) && _collected.Contains(id);
    }

    public static void Clear() => _collected.Clear();

    public static List<string> ExportList() => new List<string>(_collected);

    public static void ImportList(List<string> ids)
    {
        _collected.Clear();
        if (ids == null) return;
        foreach (var id in ids) if (!string.IsNullOrEmpty(id)) _collected.Add(id);
    }
}
