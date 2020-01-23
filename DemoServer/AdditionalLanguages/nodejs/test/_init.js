before(() => {
    require('../demo/common/docStoreHolder');
});

after(() => {
    require('../demo/common/docStoreHolder').documentStore.dispose();
    require('../demo/common/docStoreHolder').mediaStore.dispose();
});
