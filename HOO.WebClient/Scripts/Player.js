var Player = function () { };

Player.prototype = {
    tokenId: null,
    sessionId: null,
    userName: null,
    leaderName: null,
    race: null,
    motto : null,
    color: null,
    attributes: null,

    constructor: function () {
        tokenId = "";
        sessionId = "";
        leaderName = "";
        race = "";
        motto = "";
        color = "";
    },

    //Callbacks
    refreshPlayerCallback: null,

    authPlayer: function (userName, password) {

        this.sessionId = generateUUID();
        this.userName = userName;
        var pl = this;

        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "http://localhost:8001/HOOService/AuthPlayer",
            data: JSON.stringify({ "sessionId": this.sessionId, "userName": userName, "password": password }),
            dataType: "json",
            success: function (response) {
                console.log(response);
                //console.log(pl);

                if (response != null && response.AuthPlayerResult != null) {
                    var res = JSON.stringify(response.AuthPlayerResult);
                    //console.log(res);
                    pl.loadFromJSON(response.AuthPlayerResult);
                    console.log('Player : ' + pl.leaderName + ' logged in successfully.');
                    MainGame.loginSuccess();
                }
            },
            error: function (message) {
                console.error("error has occured");
                console.error(message);
            }
        })
    },

    registerNewPlayer: function (leaderName, race, motto, color, userName, password, eMail) {
        this.leaderName = leaderName;
        this.race = race;
        this.motto = motto;
        this.color = color;
        this.userName = userName;
        this.sessionId = generateUUID();

        var p = this;

        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "http://localhost:8001/HOOService/RegisterNewPlayerJS",
            data: JSON.stringify({
                "sessionId": this.sessionId, "leaderName": leaderName, "race": race, "motto": motto, "color": color
                , "userName": userName, "password": password, "eMail": eMail
            }),
            dataType: "json",
            success: function (response) {
                console.log(response);

                if (response != null && response.RegisterNewPlayerJSResult != null) {
                    var res = JSON.stringify(response.RegisterNewPlayerJSResult);
                    console.log(res);
                    p.loadFromJSON(response.RegisterNewPlayerJSResult);
                    console.log('Player : ' + p.leaderName + ' logged in successfully.');
                    MainGame.registerSuccess();
                }
            },
            error: function (message) {
                console.error("error has occured");
                console.error(message);
            }
        })
    },

    loadFromJSON: function (player, pl) {
        var plr = null;

        if (pl != null) plr = pl;
        else plr = this;

        plr.color = player.Color;
        plr.leaderName = player.LeaderName;
        plr.motto = player.Motto;
        plr.tokenId = player.TokenID;

        plr.attributes = player.Attributes;
    },

    refreshPlayer: function (owner) {
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "http://localhost:8001/HOOService/RefreshPlayer",
            data: JSON.stringify({ "sessionId": owner.sessionId, "userName": owner.userName, "tokenId": owner.tokenId }),
            dataType: "json",
            success: function (response) {
                console.log(response);
//                console.log(owner);

                if (response != null && response.RefreshPlayerResult != null) {
                    owner.loadFromJSON(response.RefreshPlayerResult, owner);
                    console.log('Player : ' + owner.leaderName + ' logged in successfully.');
                }

                if (typeof owner.refreshPlayerCallback === "function")
                    owner.refreshPlayerCallback();
            },
            error: function (message) {
                console.error("error has occured");
                console.error(message);
            }
        })
    },

    setInitialAttrs: function(){
        var owner = this;
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "http://localhost:8001/HOOService/SetPlayerInitAttrs",
            data: JSON.stringify({
                "sessionId": owner.sessionId, "userName": owner.userName, "tokenId": owner.tokenId,
                "attributes": owner.attributes,
            }),
            dataType: "json",
            success: function (response) {
                console.log(response);
                //                console.log(owner);

                if (response != null && response.SetPlayerInitAttrsResult != null) {
                    owner.loadFromJSON(response.SetPlayerInitAttrsResult, owner);
                    console.log('Player : ' + owner.leaderName + ' logged in successfully.');
                }
                MainGame.proceedWithInitialAttrs();
            },
            error: function (message) {
                console.error("error has occured");
                console.error(message);
            }
        })
    },

    getAttributeValue: function (a, b) {
        var attr = this.attributes.find(x=> x.AttributeType === a && x.Attribute === b);
        if (attr != null)
            return attr.Value;
        else
            return null;
    },
}