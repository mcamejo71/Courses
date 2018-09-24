var AddressBook = function () {
    /* members*/

    var contact = {
        name: ko.observable(),
        phoneNumber: ko.observable()
    };

    var contacts = ko.observableArray();


    /* functions */

    var init = function () {
        ko.applyBindings(AddressBook);
    };

    var addContact = function () {
        contacts.push({ name: contact.name(), phoneNumber: contact.phoneNumber() });
    };


    $(init);

    return {
        contact: contact,
        addContact: addContact,
        contacts: contacts
    };

}();