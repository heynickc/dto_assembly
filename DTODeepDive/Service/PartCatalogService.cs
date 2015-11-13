using System.Linq;
using DtoDeepDive.Data.DAL;
using DtoDeepDive.Data.Repository;

namespace DtoDeepDive.Data.Service {
    public class PartCatalogService : IPartCatalogService {
        private readonly IRepository<Part> _partRepository;
        private readonly PartAssembler _partAssembler;
        public PartCatalogService(IRepository<Part> partRepository, PartAssembler partAssembler) {
            _partRepository = partRepository;
            _partAssembler = partAssembler;
        }
        public PartDTO GetPart(string partNumber) {
            var part = _partRepository.Get(x => x.PartNumber == partNumber);
            return _partAssembler.WriteDto(part);
        }
    }
    public class PartAssembler {
        public PartDTO WriteDto(Part part) {
            var partDto = new PartDTO();
            partDto.PartNumber = part.PartNumber;
            partDto.CreatedBy = part.CreatedBy;
            partDto.CreatedOn = part.CreatedOn;
            partDto.UnitOfMeasure = part.UnitOfMeasure;
            partDto.ExtendedDescription = part.ExtendedDescription;
            partDto.PartDescription = part.PartDescription;
            partDto.SalesCode = part.SalesCode;
            var componentsList = part.Components
                .Select(component => component.ComponentNumber)
                .ToList();
            var laborSequenceList = part.LaborSequences
                .Select(labor => labor.LaborSequenceNumber)
                .ToList();
            partDto.ComponentList = componentsList;
            partDto.LaborSequenceList = laborSequenceList;
            return partDto;
        }
    }
}
