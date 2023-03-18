

$("#get_product").click(() => {
    var barcode = $("#barcode_value").val();

    let userKey = "0e582248d9a24791b0845dd5ad75087c";
    let apiKey = "765625d4916685d097ceb7ff3a0717e4020ea114fce35d73587a859730523c00";
    var url = "https://www.barkodoku.com/ws/BarkodServis.asmx"
    $.ajax({
        contentType: "application/soap+xml",
        type: "POST",
        url: url,
        apiKey: apiKey,
        barkod: barcode,

        success: (data) => {
            console.log(data);
        }
    })

})