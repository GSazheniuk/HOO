using System.ServiceModel;
using HOO.Core;
using System.Collections.Generic;
using HOO.Core.Model.Universe;
using HOO.Core.Model;
using System.ServiceModel.Web;

namespace HOO.ComLib
{
    [ServiceContract(SessionMode = SessionMode.Allowed)]
    public interface IHOOService
    {
        [OperationContract]
        [WebInvoke(Method = "POST",
               BodyStyle = WebMessageBodyStyle.Wrapped,
               ResponseFormat = WebMessageFormat.Json,
               RequestFormat = WebMessageFormat.Json)]
        List<Universe> GetAllUniverses();

        [OperationContract]
        [WebInvoke(Method = "POST",
               BodyStyle = WebMessageBodyStyle.Wrapped,
               ResponseFormat = WebMessageFormat.Json,
               RequestFormat = WebMessageFormat.Json)]
        Universe GetUniverseById(long uId);

        [OperationContract]
        [WebInvoke(Method = "POST",
               BodyStyle = WebMessageBodyStyle.Wrapped,
               ResponseFormat = WebMessageFormat.Json,
               RequestFormat = WebMessageFormat.Json)]
        Galaxy AddNewGalaxy();

        [OperationContract]
        [WebInvoke(Method = "POST",
               BodyStyle = WebMessageBodyStyle.Wrapped,
               ResponseFormat = WebMessageFormat.Json,
               RequestFormat = WebMessageFormat.Json)]
        Star GenerateNewStar(long universeId, long galaxyId);

        [OperationContract]
        [WebInvoke(Method = "POST",
               BodyStyle = WebMessageBodyStyle.Wrapped,
               ResponseFormat = WebMessageFormat.Json,
               RequestFormat = WebMessageFormat.Json)]
        OnlinePlayer RegisterNewPlayer(Player p);

        [OperationContract]
        [WebInvoke(Method = "POST",
               BodyStyle = WebMessageBodyStyle.Wrapped,
               ResponseFormat = WebMessageFormat.Json,
               RequestFormat = WebMessageFormat.Json)]
        OnlinePlayer RegisterNewPlayerJS(string sessionId, string leaderName, string race, string motto, string color, string userName, string password, string eMail);

        [OperationContract]
        [WebInvoke(Method = "POST",
               BodyStyle = WebMessageBodyStyle.Wrapped,
               ResponseFormat = WebMessageFormat.Json,
               RequestFormat = WebMessageFormat.Json)]
        OnlinePlayer AuthPlayer(string sessionId, string userName, string password);

        [OperationContract]
        [WebInvoke(Method = "POST",
       BodyStyle = WebMessageBodyStyle.Wrapped,
       ResponseFormat = WebMessageFormat.Json,
       RequestFormat = WebMessageFormat.Json)]
        OnlinePlayer RefreshPlayer(string sessionId, string userName, string tokenId);

        [OperationContract]
        [WebInvoke(Method = "POST",
               BodyStyle = WebMessageBodyStyle.Wrapped,
               ResponseFormat = WebMessageFormat.Json,
               RequestFormat = WebMessageFormat.Json)]
        string HelloWorld(string name);

        [OperationContract]
        [WebInvoke(Method = "POST",
               BodyStyle = WebMessageBodyStyle.Wrapped,
               ResponseFormat = WebMessageFormat.Json,
               RequestFormat = WebMessageFormat.Json)]
        List<Product> GetAvailableProducts(string objectType, int OBID, string sessionId, string userName, string tokenId);

        [OperationContract]
        [WebInvoke(Method = "POST",
               BodyStyle = WebMessageBodyStyle.Wrapped,
               ResponseFormat = WebMessageFormat.Json,
               RequestFormat = WebMessageFormat.Json)]
        string GetWorldState();

        [OperationContract]
        [WebInvoke(Method = "POST",
               BodyStyle = WebMessageBodyStyle.Wrapped,
               ResponseFormat = WebMessageFormat.Json,
               RequestFormat = WebMessageFormat.Json)]
        OnlinePlayer SetPlayerInitAttrs(string sessionId, string userName, string tokenId, List<OAttribute> attributes);

        [OperationContract]
        [WebInvoke(Method = "POST",
               BodyStyle = WebMessageBodyStyle.Wrapped,
               ResponseFormat = WebMessageFormat.Json,
               RequestFormat = WebMessageFormat.Json)]
        Star GetRandomStarForHomeWorld(string sessionId, string userName, string tokenId);
    }
}
