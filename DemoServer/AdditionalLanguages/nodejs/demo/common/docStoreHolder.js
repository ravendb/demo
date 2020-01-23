const { DocumentStore } = require('ravendb');

const documentStore = new DocumentStore(
    'http://live-test.ravendb.net', 'Demog');
documentStore.initialize();

const mediaStore = new DocumentStore(
    'http://live-test.ravendb.net', 'Media-09e801d1-c2b3-42f4-9e54-8317fe255fe7');
mediaStore.initialize();

module.exports = { documentStore, mediaStore };
