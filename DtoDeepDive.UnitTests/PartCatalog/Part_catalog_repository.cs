using DtoDeepDive.Data.DAL;
using DtoDeepDive.Data.Repository;
using Xunit;
using Xunit.Abstractions;

namespace DtoDeepDive.UnitTests.PartCatalog {
    public class Part_catalog_repository {
        private readonly ITestOutputHelper _output;
        private readonly IPartsCatalogDbContext _db;
        public Part_catalog_repository(ITestOutputHelper output) {
            _output = output;
            _db = new FakePartsCatalogDbContext();
            Fixtures.SeedFakePartDb(_db);
        }
        [Fact]
        public void when_getting_all_parts_from_repository() {
            var repository = new PartRepository(_db);
            var parts = repository.GetAll(x => true);
            _output.WriteLine(parts.ToJson());
        }
        public void Dispose() {
            _db.Dispose();
        }
    }
}