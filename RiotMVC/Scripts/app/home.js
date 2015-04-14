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
        $("#submit").text("Requesting...").attr("disabled", "true");        
        var championNames;
        $.getJSON("http://ddragon.leagueoflegends.com/cdn/5.7.2/data/en_US/champion.json", function (data) {
            
            championNames = $.map(data.data, function (el) { return {"id": el.key, "name" : el.name} });

            $.ajax(homeUrl + "/GetMatchInformation", {
                data: { hours: $("#hours").val(), minutes: $("#minutes").val(), date: $("#date").val() },
                success: function (data) {
                    $("#championTable").removeClass("hidden");
                    $("#championTable tbody").empty();

                    var doubleKills = 0;
                    var tripleKills = 0;
                    var quadraKills = 0;
                    var pentaKills = 0;
                    var teemoDeaths = 0;
                    
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

                        if (championNames[i].name == "Teemo")
                            teemoDeaths += data.ChampionScoreCards[i].Deaths * 1;

                        doubleKills += data.ChampionScoreCards[i].DoubleKills * 1;
                        tripleKills += data.ChampionScoreCards[i].TripleKills * 1;
                        quadraKills += data.ChampionScoreCards[i].QuadraKills * 1;
                        pentaKills += data.ChampionScoreCards[i].PentaKills * 1;

                        $("#championTable tbody").append(dataRows);
                    }

                    var flotData = [[0, doubleKills], [1, tripleKills], [2, quadraKills], [3, pentaKills]];
                    var teemoDeathData = [[0, teemoDeaths]];

                    var plot = $.plot("#graph", [{
                        data: flotData,
                        bars: { show: true, fill: true, align: "center", barWidth: 0.5 },
                        label: "Kills",
                        xaxis: {
                            ticks: [[0, "Double Kills"], [1, "Triple Kills"], [2, "Quadra Kills"], [3, "Penta Kills"]]
                        }
                    }]);

                    var teemoPlot = $.plot("#teemoGraph", [{
                        data: teemoDeathData,
                        bars: { show: true, fill: true, align: "center", barWidth: 0.5 },
                        label: "Deaths",
                        xaxis: {
                            ticks: [[0, "Teemo Deaths"]]
                        }
                    }]);

                    $("#submit").text("Submit").removeAttr("disabled");

                }
            });
        });



        
    });


})