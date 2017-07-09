var mGame = function () { };

mGame.prototype = {
    intervalIds: {},
    WorldTick: null,
    WorldTurn: null,
    WorldPeriod: null,

    constructor: function () {
        intervalIds = {};
        WorldTick = 0;
        WorldTurn = 0;
        WorldPeriod = 0;
    },

    //Callbacks
    refreshWorldTimeCallback: null,
    loginSuccessCallBack: null,
    setInitialAttrsCallback: null,
    getRandomStarCallback: null,

    setTimer: function (intervalId, delegate, interval, owner) {
        if (this.intervalIds[intervalId] != null) {
            window.clearInterval(this.intervalIds[intervalId]);
            this.intervalIds[intervalId] = null;
        }

        this.intervalIds[intervalId] = window.setInterval(delegate, interval, owner);
    },

    showLoginDialog: function () {
        getTemplate("/Templates/Login.html", function () { showPopup("popupNoClose", this.responseText); });
    },

    showRegisterDialog: function () {
        getTemplate("/Templates/Register.html", function () { showPopup("popupNoClose", this.responseText); });
    },

    registerSuccess: function () {
        if (p.tokenId != null) {
            MainGame.LoadMain();
        }
        else {
            console.log("Player not logged in.");
        }
    },

    loginSuccess: function () {
        if (p.tokenId != null) {
            closePopup("popupNoClose", MainGame.loginSuccessCallBack)
        }
        else {
            console.log("Player not logged in.");
        }
    },

    refreshWorldTime: function (w) {
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "http://localhost:8001/HOOService/GetWorldState",
            data: JSON.stringify({ }),
            dataType: "json",
            success: function (response) {
                console.log(response);

                if (response != null && response.GetWorldStateResult != null) {
                    var res = JSON.parse(response.GetWorldStateResult);
                    //console.log(response.GetWorldStateResult);
                    //console.log(res);

                    //console.log(w);

                    w.WorldPeriod = res.Period;
                    w.WorldTick = res.Tick;
                    w.WorldTurn = res.Turn;

                    if (typeof w.refreshWorldTimeCallback === "function")
                        w.refreshWorldTimeCallback();
                }
            },
            error: function (message) {
                console.error("error has occured");
                console.error(message);
            }
        })
    },

    getRandomStar: function(){
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "http://localhost:8001/HOOService/GetRandomStarForHomeWorld",
            data: JSON.stringify({ "sessionId": p.sessionId, "userName": p.userName, "tokenId": p.tokenId }),
            dataType: "json",
            success: function (response) {
                console.log(response);
                //console.log(pl);

                if (response != null && response.GetRandomStarForHomeWorldResult != null) {                    
                    if (typeof MainGame.getRandomStarCallback === "function")
                        MainGame.getRandomStarCallback(response.GetRandomStarForHomeWorldResult);
                }
            },
            error: function (message) {
                console.error("error has occured");
                console.error(message);
            }
        })
    },

    showInitAttrsDialog: function(){
        getTemplate("/Templates/InitialAttributes.html", function () { showPopup("popupNoClose", this.responseText); });
    },

    showRandomStarSystem: function () {
        getTemplate("/Templates/StarSystemView.html", function () { showPopup("popupNoClose", this.responseText); });
        MainGame.getRandomStar();
    },

    proceedWithInitialAttrs: function(){
        closePopup("popupNoClose", MainGame.setInitialAttrsCallback);
    },

    LoadMain: function () {
        $("#mainContainer").empty();
        $("#popupContainer1").empty();
        $("#popupContainer2").empty();

        getTemplate("/Templates/MainView.html", function () { $("#mainContainer").append(this.responseText); });
    }
}