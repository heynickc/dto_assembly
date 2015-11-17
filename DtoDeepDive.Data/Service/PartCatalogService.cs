using System;
using System.Collections.Generic;
using System.Linq;
using DtoDeepDive.Data.DAL;
using DtoDeepDive.Data.DTO;
using DtoDeepDive.Data.DTOAssemblers;
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
        public IEnumerable<PartDTO> GetAllParts() {
            var parts = _partRepository.GetAll(x => true).ToList();
            var partDtos = new List<PartDTO>();
            foreach (var part in parts) {
                partDtos.Add(_partAssembler.WritePartDto(part));
            }
            return partDtos;
        }
        public QuoteDTO GetQuote(List<PartDTO> selectedParts) {
            var partDtos = (from selectedPart in selectedParts
                            let part = _partRepository.Get(p => p.PartNumber == selectedPart.PartNumber)
                            select _partAssembler.WritePartDto(part, selectedPart.Quantity))
                            .ToList();
            var quoteDto = _partAssembler.WriteQuoteDto(partDtos);
            return quoteDto;
        }
    }
}
