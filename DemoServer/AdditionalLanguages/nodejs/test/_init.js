before(() => {
    require('../demo/common/docStoreHolder');
});

after(() => {
    require('../demo/common/docStoreHolder').store.dispose();
    require('../demo/common/docStoreHolder').mediaStore.dispose();
});
