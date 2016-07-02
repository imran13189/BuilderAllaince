function ShowConfirmMessage(message) {
    debugger;
    BootstrapDialog.show({
        title: 'Confirmation',
        message: message,
        buttons: [{
            label: 'OK',
            cssClass: 'btn-primary',
            action: function (dialogItself) {
                dialogItself.close();
            }
        }]
    });

}