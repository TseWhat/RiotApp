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
        format: "dd/mm/yyyy"
    });

    $("#submit").on("click", function () {
        $("#loading").removeClass("hidden");
        $("#statsSection").fadeOut(300);
        $("#submit").text("Requesting...").attr("disabled", "true");
        $("#championTable_wrapper").fadeOut(300);
        $("#championTable tbody").empty();

        $.ajax(homeUrl + "/GetMatchInformation", {
            data: {
                hours: $("#hours").val(), minutes: $("#minutes").val(), date: $("#date").val(), region: $("#region").val()
            },
            success: function (data) {
                $("#championTable").removeClass("hidden");
                var teemoDeaths = 0;
                    
                for (var i = 0; i < data.ChampionScoreCards.length; i++) {
                    var dataRows = "<tr>";
                    dataRows += "<td>" + data.ChampionScoreCards[i].ChampionName + "</td>";
                    dataRows += "<td>" + data.ChampionScoreCards[i].TimesPlayed + "</td>";
                    dataRows += "<td>" + data.ChampionScoreCards[i].Kills + "</td>";
                    dataRows += "<td>" + data.ChampionScoreCards[i].Deaths + "</td>";
                    dataRows += "<td>" + data.ChampionScoreCards[i].Assists + "</td>";
                    dataRows += "<td>" + (data.ChampionScoreCards[i].Kills / data.ChampionScoreCards[i].Deaths).toFixed(2) + "</td>";
                    dataRows += "<td>" + ((data.ChampionScoreCards[i].Kills + data.ChampionScoreCards[i].Assists) / data.ChampionScoreCards[i].Deaths).toFixed(2) + "</td>";
                    dataRows += "</tr>";

                    if (data.ChampionScoreCards[i].ChampionName == "Teemo")
                        teemoDeaths += data.ChampionScoreCards[i].Deaths * 1;

                    $("#championTable tbody").append(dataRows);
                }

                $("#totalGames").text(data.NumberOfGames);
                $("#totalKills").text(data.AverageStats.TotalKills);
                $("#totalDeaths").text(data.AverageStats.TotalDeaths);
                $("#doubleKills").text(data.AverageStats.DoubleKills);
                $("#tripleKills").text(data.AverageStats.TripleKills);
                $("#quadraKills").text(data.AverageStats.QuadraKills);
                $("#pentaKills").text(data.AverageStats.PentaKills);
                $("#biggestCrit").text(data.AverageStats.BiggestCrit);
                $("#teemoDeaths").text(teemoDeaths);

                $("#championTable").DataTable({
                    paging: false
                });
                $("#championTable_wrapper").fadeIn(300);
                $("#statsSection").removeClass("hidden");
            }
        })
        .always(function () {
            $("#submit").text("Submit").removeAttr("disabled");
            $("#loading").fadeOut(300);
        });



        
    });


})