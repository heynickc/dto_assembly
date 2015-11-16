using DtoDeepDive.Data.DTO;
using System.Collections.Generic;

namespace DtoDeepDive.Data.Service {

    public interface IPartCatalogService {
        PartDTO GetPart(string partNumber);
        IEnumerable<PartDTO> GetAllParts();
        QuoteDTO GetQuote(Dictionary<string, double> selectedItems);
    }

}