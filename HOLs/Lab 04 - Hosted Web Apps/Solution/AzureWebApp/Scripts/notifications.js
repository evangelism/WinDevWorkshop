if (window.Windows) {
    console.log('this printed from the file')

    function createToast(title, message, imgUrl, imgAlt, tag) {
        console.log('create toast triggered')
        // Namespace: Windows.UI.Notifications
        if (typeof Windows !== 'undefined' &&
                typeof Windows.UI !== 'undefined' &&
                typeof Windows.UI.Notifications !== 'undefined') {
            // Setup variables for shorthand
            var notifications = Windows.UI.Notifications,
                templateType = notifications.ToastTemplateType.toastImageAndText02,
                templateContent = notifications.ToastNotificationManager.getTemplateContent(templateType),
                toastMessage = templateContent.getElementsByTagName('text'),
                toastImage = templateContent.getElementsByTagName('image'),
                toastElement = templateContent.selectSingleNode('/toast');

            var launchParams = {
                type: 'toast',
                id: tag || 'demoToast',
                heading: title || 'Demo title',
                body: message || 'Demo message'
            };

            var launchString = JSON.stringify(launchParams);

            // Set message & image in toast template
            toastMessage[0].appendChild(templateContent.createTextNode(message || 'Demo message'));
            toastImage[0].setAttribute('src', imgUrl || 'https://unsplash.it/150/?random');
            toastImage[0].setAttribute('alt', imgAlt || 'Random sample image');
            toastElement.setAttribute('duration', 'long');
            toastElement.setAttribute('launch', launchString); // Optional Launch Parameter



            // Show the toast
            var toast = new notifications.ToastNotification(templateContent);
            var toastNotifier = new notifications.ToastNotificationManager.createToastNotifier();
            toast.tag = 'demoToast';
            console.log(toast);
            toastNotifier.show(toast);



        } else {
            // Fallback if no native notifications are supported
            // In this case revert to alert
            // TODO: Build modal UI for better experience
            var alertText = title || 'Demo Title';

            alert(alertText);
        }
    }

    function notify() {
        // Pull the user input from the page and create the notification
        var title, message, imgUrl, imgAlt, tag

        title = 'Alert'
        message = "Hello from your hosted web app"
        imgUrl = 'http://assets.onestore.ms/cdnfiles/onestorerolling-1509-27004/shell/v2_5/images/logo/icon-windows-logo.png';
        imgAlt = 'this is image alt'
        tag = 'tag'

        createToast(title, message, imgUrl, imgAlt, tag);
    }
}