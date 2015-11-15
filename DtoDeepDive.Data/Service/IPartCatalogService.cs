using DtoDeepDive.Data.DTO;

namespace DtoDeepDive.Data.Service {

    public interface IPartCatalogService {
        PartDTO GetPart(string partNumber);
        PartCatalogDTO GetPartCatalog();
        QuoteDTO GetQuote(PartCatalogDTO selectedParts);
    }

}