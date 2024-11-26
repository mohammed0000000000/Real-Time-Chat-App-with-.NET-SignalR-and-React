using System.Collections.Concurrent;
using SignlR_Web_ApI.Models;

namespace SignlR_Web_ApI.DataServices;

public class ShardDb
{
    private readonly ConcurrentDictionary<string, UserConnection> _connections = new ConcurrentDictionary<string, UserConnection>();
    public ConcurrentDictionary<string, UserConnection> connections => _connections;
}