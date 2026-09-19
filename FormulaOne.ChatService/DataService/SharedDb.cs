using System.Collections.Concurrent;
using FormulaOne.ChatService.Model;
namespace FormulaOne.ChatService.DataService;

public class SharedDb
{
    public readonly ConcurrentDictionary<string, UserConnection> _connections = new();

    public ConcurrentDictionary<string, UserConnection> Connections => _connections;
}
