$(function () {
    var today = new Date();
    var dd = today.getDate();
    var mm = today.getMonth()+1; //January is 0!
    var yyyy = today.getFullYear();

    if(dd<10) {
        dd='0'+dd
    } 

    if(mm<10) {
        mm='0'+mm
    } 

    today = dd+'/'+mm+'/'+yyyy;

    $("#date").datepicker({
        format: "dd/mm/yyyy", 
        endDate: new Date(today)
    });

    $("#submit").on("click", function () {
      //  $("#submit").text("Requesting...").attr("disabled", "true");        
        var championNames;
        $.getJSON("http://ddragon.leagueoflegends.com/cdn/5.7.2/data/en_US/champion.json?callback=?", function (data) {
            
            championNames = $.map(data.data, function (el) { return {"id": el.key, "name" : el.name} });

            $.ajax(homeUrl + "/GetMatchInformation", {
                data: { hours: $("#hours").val(), minutes: $("#minutes").val(), date: $("#date").val() },
                success: function (data) {
                    $("#championTable").removeClass("hidden");

                    for (var i = 0; i < data.ChampionScoreCards.length; i++) {
                        var dataRows = "<tr>";
                        dataRows += "<td>" + championNames[i].name + "</td>";
                        dataRows += "<td>" + data.ChampionScoreCards[i].Kills + "</td>";
                        dataRows += "<td>" + data.ChampionScoreCards[i].Deaths + "</td>";
                        dataRows += "<td>" + data.ChampionScoreCards[i].Assists + "</td>";
                        dataRows += "<td>" + data.ChampionScoreCards[i].DoubleKills + "</td>";
                        dataRows += "<td>" + data.ChampionScoreCards[i].TripleKills + "</td>";
                        dataRows += "<td>" + data.ChampionScoreCards[i].QuadraKills + "</td>";
                        dataRows += "<td>" + data.ChampionScoreCards[i].PentaKills + "</td>";
                        dataRows += "</tr>";     
                        $("#championTable tbody").append(dataRows);
                    }
                }
            });
        });
   //     $("#submit").text("Submit").removeAttr("disabled");



        
    });


})