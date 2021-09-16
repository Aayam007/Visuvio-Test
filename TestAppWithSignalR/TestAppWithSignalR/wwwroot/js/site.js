
$(() => {
    var connection = new signalR.HubConnectionBuilder().withUrl("/signalRService").build();
    connection.on("ReceiveMessage", function (message) {

        $(".toast-body").empty();
        $(".toast-body").append(message +" joined the group.");
        $('.toast').toast('show');
     
    });
    connection.start();
    connection.on("Rosters", function () {
        LoadProdData();
    })
    LoadProdData();
    function LoadProdData() {
        var tr = '';
        $.ajax({
            url: '/Roster/GetRosters',
            method: 'GET',
            success: (result) => {
                $.each(result, (k, v) => {
                    tr += `<tr>
                        <td>${v.FullName}</td>
                        <td>${v.GroupName}</td>
                        <td>${v.Age}</td>
                        <td>
                        <a href = '../Roster/Edit?id=${v.Id}'>Edit</a>
                        <a href = '../Roster/Details?id=${v.Id}'>Detail</a>
                        <a href = '../Roster/Delete?id=${v.Id}'>Delete</a>
      </td>
</tr>
                        `
                })
                $("#tableBody").html(tr);
            },
            error: (error) => {
                console.log(error)
            }

        });
       
    }
})