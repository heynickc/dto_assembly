using System;
using System.Collections.Generic;
using System.Linq;
using DtoDeepDive.Data.DAL;
using DtoDeepDive.Data.DTO;
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
            return _partAssembler.WritePartDto(part);
        }
        public PartCatalogDTO GetPartCatalog() {
            var parts = _partRepository.GetAll(x => true).ToList();
            var partCatalogDto = _partAssembler.WritePartCatalogDTO(parts);
            return partCatalogDto;
        }
        public QuoteDTO GetQuote(PartCatalogDTO partCatalogDto) {
            var quoteDto = _partAssembler.WriteQuoteDTO(partCatalogDto.getSelectedParts());
            return quoteDto;
        }
    }
    public class PartAssembler {
        public PartDTO WritePartDto(Part part) {
            var partDto = new PartDTO();
            partDto.PartNumber = part.PartNumber;
            partDto.UnitOfMeasure = part.UnitOfMeasure;
            partDto.ExtendedDescription = part.ExtendedDescription;
            partDto.PartDescription = part.PartDescription;
            partDto.SalesCode = part.SalesCode;
            var componentsList = part.Components
                .Select(component => new ComponentDTO() {
                     Number = component.ComponentNumber,
                     Description = component.ComponentDescription,
                     Material = component.Material,
                     UnitOfMeasure = component.UnitOfMeasure,
                     QuantityPerAssembly = component.QuantityPerAssembly,
                     CostPerUnit = component.CostPerUnit,
                     QuantityRequired = part.TotalQuantityRequired * component.QuantityPerAssembly,
                     MaterialCost = (decimal)(part.TotalQuantityRequired * component.QuantityPerAssembly)*component.CostPerUnit
                }).ToList();
            var laborSequenceList = part.LaborSequences
                .Select(labor => new LaborSequenceDTO() {
                    SequenceNumber = labor.LaborSequenceNumber,
                    SequenceDescription = labor.LaborSequenceDesc,
                    RunTime = labor.RunTime,
                    LaborRate = labor.LaborRate,
                    LaborCost = ((decimal)labor.RunTime*labor.LaborRate)*labor.Burden
                }).ToList();
            partDto.Components = componentsList;
            partDto.Labor = laborSequenceList;

            partDto.TotalMaterialCost = partDto.Components.Sum(x => x.MaterialCost);
            partDto.TotalLaborCost = partDto.Labor.Sum(x => x.LaborCost);

            return partDto;
        }
        public PartCatalogDTO WritePartCatalogDTO(IEnumerable<Part> parts) {
            var partCatalogDto = new PartCatalogDTO();
            foreach (var part in parts) {
                partCatalogDto.Parts.Add(WritePartDto(part));
            }
            return partCatalogDto;
        }
        public QuoteDTO WriteQuoteDTO(IEnumerable<PartDTO> partDtos) {
            var quoteDto = new QuoteDTO();
            foreach (var part in partDtos) {
                quoteDto.Parts.Add(part);
            }
            quoteDto.TotalMaterialCost = quoteDto.Parts.Sum(x => x.TotalMaterialCost);
            quoteDto.TotalLaborCost = quoteDto.Parts.Sum(x => x.TotalLaborCost);
            quoteDto.TotalCost = quoteDto.TotalMaterialCost + quoteDto.TotalLaborCost;

            return quoteDto;
        }
    }
}
