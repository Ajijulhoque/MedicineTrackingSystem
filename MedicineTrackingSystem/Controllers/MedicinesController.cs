using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MedicineTrackingSystem.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MedicinesController : ControllerBase
    {
        private readonly ILogger<MedicinesController> _logger;
        private readonly IRepository myRepository;

        public MedicinesController(IRepository repository)
        {
            myRepository = repository;
        }

        [HttpGet]
        public IEnumerable<Medicine> Get()
        {
            return myRepository.AllMedicines;
        }

        [HttpGet("{name}")]
        public Medicine SearchMedicineByName(string name)
        {
            return myRepository.GetMedicine(name);
        }

        [HttpPost]
        public void CreateNewEntry([FromBody] Medicine medicine)
        {
            myRepository.CreateNewEntry(medicine);
        }
    }
}
