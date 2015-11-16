using System.Collections.Generic;
using System.Linq;
using DtoDeepDive.Data.DAL;
using DtoDeepDive.Data.DTO;

namespace DtoDeepDive.Data.DTOAssemblers {
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
                    CostPerUnit = component.CostPerUnit
                }).ToList();
            var laborSequenceList = part.LaborSequences
                .Select(labor => new LaborSequenceDTO() {
                    SequenceNumber = labor.LaborSequenceNumber,
                    SequenceDescription = labor.LaborSequenceDesc,
                    RunTime = labor.RunTime,
                    LaborRate = labor.LaborRate,
                    Burden = labor.Burden
                }).ToList();
            partDto.Components = componentsList;
            partDto.Labor = laborSequenceList;

            return partDto;
        }
        public PartDTO WritePartDto(Part part, double quantity) {
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
                    CostPerUnit = component.CostPerUnit
                }).ToList();
            var laborSequenceList = part.LaborSequences
                .Select(labor => new LaborSequenceDTO() {
                    SequenceNumber = labor.LaborSequenceNumber,
                    SequenceDescription = labor.LaborSequenceDesc,
                    RunTime = labor.RunTime,
                    LaborRate = labor.LaborRate,
                    Burden = labor.Burden
                }).ToList();
            partDto.Components = componentsList;
            partDto.Labor = laborSequenceList;
            partDto.Quantity = quantity;

            return partDto;
        }
        public QuoteDTO WriteQuoteDTO(IEnumerable<PartDTO> partDtos) {
            var quoteDto = new QuoteDTO();
            foreach (var part in partDtos) {
                quoteDto.Parts.Add(part);
            }
            return quoteDto;
        }
    }

}