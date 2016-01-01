using System.ServiceModel;
using HOO.Core;
using System.Collections.Generic;
using HOO.Core.Model.Universe;
using HOO.Core.Model;

namespace HOO.ComLib
{
    [ServiceContract(SessionMode = SessionMode.Allowed)]
    public interface IHOOService
    {
        [OperationContract]
        List<Universe> GetAllUniverses();

        [OperationContract]
        Universe GetUniverseById(long uId);

        [OperationContract]
        Star GenerateNewStar(long universeId, long galaxyId);

        [OperationContract]
        OnlinePlayer RegisterNewPlayer(Player p);

        [OperationContract]
        OnlinePlayer AuthPlayer(string sessionId, string userName, string password);
    }
}
