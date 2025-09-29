using System;
using System.Collections.Generic;

public static class EventBus
{
    private static readonly Dictionary<string, List<Delegate>> map = new();

    public static void Subscribe<T>(string id, Action<T> cb)
    {
        if (!map.TryGetValue(id, out var list)) { list = new(); map[id] = list; }
        list.Add(cb);
    }

    public static void Unsubscribe<T>(string id, Action<T> cb)
    {
        if (map.TryGetValue(id, out var list)) list.Remove(cb);
    }

    public static void Publish<T>(string id, T payload)
    {
        if (!map.TryGetValue(id, out var list)) return;
        var snap = list.ToArray();
        foreach (var d in snap) if (d is Action<T> cb) cb(payload);
    }
    public static void Publish(string id) => Publish<object>(id, null);

}
