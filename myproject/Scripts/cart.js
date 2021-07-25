
$('.amount > input[type="number"]').on('input', updateTotal);

function updateTotal(e) {
    var amount = parseInt(e.target.value);

    if (!amount || amount < 0)
        return;

    $(".cart").find('.amount').val(amount);
    console.log(amount);

    var $parentRow = $(e.target).parent().parent().parent();
    var name = $parentRow.find('.productname').text();

    console.log(name);


    var url = '@Url.Action("Index", "Cart",' + 'new { name =  ' + '"' +name + '"' +', amount = ' + amount + '})';
    console.log(url);
    $.ajax({
        url: 'Cart/Index',
        data: { name: name, amount: amount },
        type: 'POST',
        success: function (data) {
            location.reload();
        }
    });
}



