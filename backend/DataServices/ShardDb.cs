using System.Collections.Concurrent;
using SignlR_Web_ApI.Models;

namespace SignlR_Web_ApI.DataServices;

public class ShardDb
{
    private readonly ConcurrentDictionary<string, UserConnectionTemp> _connections = new ConcurrentDictionary<string, UserConnectionTemp>();
    public ConcurrentDictionary<string, UserConnectionTemp> connections => _connections;
}