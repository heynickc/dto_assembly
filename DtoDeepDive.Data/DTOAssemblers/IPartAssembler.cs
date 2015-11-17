using System.Collections.Generic;
using DtoDeepDive.Data.DAL;
using DtoDeepDive.Data.DTO;

namespace DtoDeepDive.Data.DTOAssemblers {

    public interface IPartAssembler {
        PartDTO WritePartDto(Part part);

        PartDTO WritePartDto(Part part, double quantity);

        QuoteDTO WriteQuoteDto(IEnumerable<PartDTO> partDtos);
    }

}