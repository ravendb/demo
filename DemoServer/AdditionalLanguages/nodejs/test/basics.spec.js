describe('Basics', function () {
    it('creates document', () => {
        return require('../demo/basics/createDocument').run({
            companyName: 'aaa',
            companyPhone: '1234',
            contactName: 'John',
            contactTitle: 'Sir'
        });
    });
});
