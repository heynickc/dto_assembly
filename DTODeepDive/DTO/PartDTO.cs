﻿using System;
using System.Collections.Generic;
using DtoDeepDive.Data.DAL;

namespace DtoDeepDive.Data.DTO {
    public class PartDTO {
        public int Id { get; set; }
        public string PartNumber { get; set; }
        public string ExtendedDescription { get; set; }
        public string PartDescription { get; set; }
        public string SalesCode { get; set; }
        public string UnitOfMeasure { get; set; }
        public List<LaborSequenceDTO> Labor { get; set; }
        public List<ComponentDTO> Components { get; set; }
    }
}