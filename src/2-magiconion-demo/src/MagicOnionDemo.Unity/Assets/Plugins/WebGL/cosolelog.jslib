mergeInto(LibraryManager.library, {
    ConsoleLog: function (message) {
        console.error(UTF8ToString(message));
    },
});