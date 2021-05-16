using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace MedicineTrackingSystem
{
    public interface IRepository
    {
        IList<Medicine> AllMedicines { get; }

        Medicine GetMedicine(string name);

        void CreateNewEntry(Medicine medicine);
    }

    public class MedicineRepository
    {
        public List<Medicine> Medicines { get; set; } = new List<Medicine>();
    }

    public class Repository : IRepository
    {
        private readonly IJsonReader myJsonReader;
        private readonly IJsonWriter myJsonWriter;
        private MedicineRepository myMedicineRepository;
        private string path = "MedicineRecords.json";
        public Repository(IJsonReader jsonReader, IJsonWriter jsonWriter)
        {
            myJsonReader = jsonReader;
            myJsonWriter = jsonWriter;
            myMedicineRepository = myJsonReader.ReadJsonFile(path);
        }

        public IList<Medicine> AllMedicines => myMedicineRepository.Medicines;

        public Medicine GetMedicine(string name)
        {
            return myMedicineRepository.Medicines.FirstOrDefault(m => string.Equals(m.FullName, name, StringComparison.InvariantCultureIgnoreCase));
        }

        public void CreateNewEntry(Medicine medicine)
        {
            myMedicineRepository.Medicines.Add(medicine);
            myJsonWriter.WriteToJsonFile(path, myMedicineRepository);
        }
    }

    public interface IJsonReader
    {
        MedicineRepository ReadJsonFile(string path);
    }

    public interface IJsonWriter
    {
        void WriteToJsonFile(string path, MedicineRepository medicineRepository);
    }

    public class JsonReader : IJsonReader
    {
        public MedicineRepository ReadJsonFile(string filePath)
        {
            var jsonString = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<MedicineRepository>(jsonString);
        }
    }

    public class JsonWriter : IJsonWriter
    {
        public void WriteToJsonFile(string path, MedicineRepository medicineRepository)
        {
            var jsonData = JsonSerializer.Serialize<MedicineRepository>(medicineRepository);
            File.WriteAllText(path, jsonData);
        }
    }
}
